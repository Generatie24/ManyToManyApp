﻿using ManyToManyApp.Data;
using ManyToManyApp.Models;
using ManyToManyApp.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace ManyToManyApp.Controllers
{
    public class BoekenController : Controller
    {
        private readonly ManyToManyContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BoekenController(ManyToManyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var boeken = await _context.Boeken
                .Include(b => b.Auteur)
                .Include(b => b.BoekGenres)
                    .ThenInclude(bg => bg.Genre)
                .ToListAsync();

            ViewBag.Count = boeken.Count();
            if (boeken == null || !boeken.Any())
            {
                return NotFound();
            }

            var viewModel = boeken.Select(b => new BoekenIndexViewModel
            {
                BoekId = b.BoekId,
                Titel = b.Titel,
                AuteurNaam = b.Auteur.Naam,
                GenreNamen = b.BoekGenres.Select(bg => bg.Genre.Naam).ToList(),
                IsAvailable = b.IsAvailable,
                IsNewRelease = b.IsNewRelease,
                IsBestSeller = b.IsBestSeller,
                BindingType = b.BindingType.HasValue ? b.BindingType.Value.ToString() : "onbekend"

            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var viewModel = new CreateBoekViewModel
            {
                Auteurs = await _context.Auteurs.ToListAsync(),
                Genres = await _context.Genres.ToListAsync(),
                SelectedGenres = new List<int>()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBoekViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string? afbeeldingpad = viewModel.Afbeelding != null && viewModel.Afbeelding.Length > 0
                     ? await UploadFile(viewModel.Afbeelding)
                     : "/images/default.jpg";
                var newBoek = new Boek
                {
                    Titel = viewModel.Boek.Titel,
                    AuteurId = viewModel.SelectedAuteurId,
                    IsAvailable = viewModel.Boek.IsAvailable,
                    IsNewRelease = viewModel.Boek.IsNewRelease,
                    IsBestSeller = viewModel.Boek.IsBestSeller,
                    BindingType = viewModel.Boek.BindingType,
                    Afbeeldingpad = afbeeldingpad
                };

                _context.Boeken.Add(newBoek);
                await _context.SaveChangesAsync();

                if (viewModel.SelectedGenres != null)
                {
                    foreach (var genreId in viewModel.SelectedGenres)
                    {
                        var boekGenres = new BoekGenre
                        {
                            BoekId = newBoek.BoekId,
                            GenreId = genreId
                        };
                        _context.BoekGenres.Add(boekGenres);
                    }

                    await _context.SaveChangesAsync();
                }

            }
            return RedirectToAction(nameof(Index));

        }
        private async Task<string> UploadFile(IFormFile afbeelding)
        {
            if (afbeelding == null || afbeelding.Length == 0)
            {
                return null;
            }

            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + afbeelding.FileName;
            string filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await afbeelding.CopyToAsync(fileStream);
            }

            return "/images/" + uniqueFileName;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boek = await _context.Boeken
                .Include(b => b.BoekGenres)
                .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.BoekId == id);

            if (boek == null)
            {
                return NotFound();
            }

            var viewModel = new EditBoekViewModel
            {
                BoekId = boek.BoekId,
                Titel = boek.Titel,
                SelectedAuteurId = boek.AuteurId,
                SelectedGenres = boek.BoekGenres.Select(bg => bg.GenreId).ToList(),
                IsAvailable = boek.IsAvailable, 
                IsNewRelease = boek.IsNewRelease,
                IsBestSeller = boek.IsBestSeller,
                BindingType = boek.BindingType,
                Auteurs = await _context.Auteurs.ToListAsync(),
                Genres = await _context.Genres.ToListAsync(),
                AfbeeldingPad = boek.Afbeeldingpad

            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBoekViewModel viewModel)
        {
            if (id != viewModel.BoekId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var boek = await _context.Boeken
                    .Include(b => b.BoekGenres)
                    .FirstOrDefaultAsync(b => b.BoekId == id);
                if (boek == null)
                {
                    return NotFound();
                }
                // selecteer image
                string? afbeeldingPad = boek.Afbeeldingpad;
                if (viewModel.Afbeelding != null && viewModel.Afbeelding.Length > 0)
                {
                    afbeeldingPad = await UploadFile(viewModel.Afbeelding);
                }

                // update boek
                boek.Titel = viewModel.Titel;
                boek.AuteurId = viewModel.SelectedAuteurId;
                boek.IsAvailable = viewModel.IsAvailable;
                boek.IsNewRelease = viewModel.IsNewRelease;
                boek.IsBestSeller = viewModel.IsBestSeller;
                boek.BindingType = viewModel.BindingType;
                boek.Afbeeldingpad = afbeeldingPad;

                //update Genres
                boek.BoekGenres.Clear();
                if (viewModel.SelectedGenres !=null)
                {
                    foreach (var item in viewModel.SelectedGenres)
                    {
                        boek.BoekGenres.Add(new BoekGenre { BoekId = boek.BoekId, GenreId = item });
                    }
                    
                }
                _context.Update(boek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                viewModel.Auteurs = await _context.Auteurs.ToListAsync();
                viewModel.Genres = await _context.Genres.ToListAsync();
                return View(viewModel);
            }

           
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var boek = await _context.Boeken
                   .Include(b => b.Auteur)
                   .Include(b => b.BoekGenres)
                   .ThenInclude(bg => bg.Genre)
                   .FirstOrDefaultAsync(b => b.BoekId == id);
            if (boek == null)
            {
                return NotFound();
            }
            var viewModel = new BoekDetailsViewModel
            {
                BoekId = boek.BoekId,
                Titel = boek.Titel,
                AuteurNaam = boek.Auteur?.Naam,
                IsAvailable = boek.IsAvailable,
                IsNewRelease = boek.IsNewRelease,
                IsBestSeller = boek.IsBestSeller,
                BindingType = boek.BindingType?.ToString(),
                GenreNamen = boek.BoekGenres.Select(bg => bg.Genre.Naam).ToList(),
               AfbeeldingPad = boek.Afbeeldingpad

            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boek = await _context.Boeken.FindAsync(id);
            if (boek==null)
            {
                return NotFound();
            }

            // write method to delete image
            VerwijderAfbeelding(boek.Afbeeldingpad);

            _context.Boeken.Remove(boek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void VerwijderAfbeelding(string afbeeldingpad)
        {
            if (!string.IsNullOrEmpty(afbeeldingpad))
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", afbeeldingpad.TrimStart('/'));
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boek = await _context.Boeken
                .Include(b => b.Auteur)
                .Include(b => b.BoekGenres)
                    .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.BoekId == id);

            if (boek == null)
            {
                return NotFound();
            }

            var viewModel = new BoekDetailsViewModel
            {
                BoekId = boek.BoekId,
                Titel = boek.Titel,
                AuteurNaam = boek.Auteur?.Naam,
                IsAvailable = boek.IsAvailable,
                IsNewRelease = boek.IsNewRelease,
                IsBestSeller = boek.IsBestSeller,
                BindingType = boek.BindingType?.ToString(),
                GenreNamen = boek.BoekGenres.Select(bg => bg.Genre.Naam).ToList(),
                AfbeeldingPad = boek.Afbeeldingpad
            };

            return View(viewModel);
        }
    }
}
