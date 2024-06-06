﻿// <auto-generated />
using System;
using ManyToManyApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManyToManyApp.Migrations
{
    [DbContext(typeof(ManyToManyContext))]
    [Migration("20240606132639_assed cascade")]
    partial class assedcascade
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ManyToManyApp.Models.Auteur", b =>
                {
                    b.Property<int>("AuteurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuteurId"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AuteurId");

                    b.ToTable("Auteurs");

                    b.HasData(
                        new
                        {
                            AuteurId = 1,
                            Naam = "Jan Jansen"
                        },
                        new
                        {
                            AuteurId = 2,
                            Naam = "Sara Smit"
                        },
                        new
                        {
                            AuteurId = 3,
                            Naam = "Kenan Kurda"
                        });
                });

            modelBuilder.Entity("ManyToManyApp.Models.Boek", b =>
                {
                    b.Property<int>("BoekId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoekId"));

                    b.Property<string>("Afbeeldingpad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AuteurId")
                        .HasColumnType("int");

                    b.Property<int?>("BindingType")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBestSeller")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNewRelease")
                        .HasColumnType("bit");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BoekId");

                    b.HasIndex("AuteurId");

                    b.ToTable("Boeken");

                    b.HasData(
                        new
                        {
                            BoekId = 1,
                            Afbeeldingpad = "/images/default.jpg",
                            AuteurId = 1,
                            IsAvailable = true,
                            IsBestSeller = false,
                            IsNewRelease = false,
                            Titel = "De Ruimte Verkenner"
                        },
                        new
                        {
                            BoekId = 2,
                            Afbeeldingpad = "/images/default.jpg",
                            AuteurId = 1,
                            IsAvailable = true,
                            IsBestSeller = false,
                            IsNewRelease = false,
                            Titel = "Werelden Verbinden"
                        },
                        new
                        {
                            BoekId = 3,
                            Afbeeldingpad = "/images/default.jpg",
                            AuteurId = 2,
                            IsAvailable = true,
                            IsBestSeller = false,
                            IsNewRelease = false,
                            Titel = "De Laatste Dag"
                        },
                        new
                        {
                            BoekId = 4,
                            Afbeeldingpad = "/images/default.jpg",
                            AuteurId = 2,
                            IsAvailable = true,
                            IsBestSeller = false,
                            IsNewRelease = false,
                            Titel = "De Laatste Dag 2"
                        },
                        new
                        {
                            BoekId = 5,
                            Afbeeldingpad = "/images/default.jpg",
                            AuteurId = 2,
                            IsAvailable = true,
                            IsBestSeller = false,
                            IsNewRelease = false,
                            Titel = "De Laatste Dag 3"
                        });
                });

            modelBuilder.Entity("ManyToManyApp.Models.BoekGenre", b =>
                {
                    b.Property<int>("BoekId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("BoekId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("BoekGenres");

                    b.HasData(
                        new
                        {
                            BoekId = 1,
                            GenreId = 2
                        },
                        new
                        {
                            BoekId = 2,
                            GenreId = 2
                        },
                        new
                        {
                            BoekId = 3,
                            GenreId = 3
                        },
                        new
                        {
                            BoekId = 4,
                            GenreId = 3
                        },
                        new
                        {
                            BoekId = 5,
                            GenreId = 2
                        });
                });

            modelBuilder.Entity("ManyToManyApp.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<int?>("BoekId")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("GenreId");

                    b.HasIndex("BoekId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Naam = "Fictie"
                        },
                        new
                        {
                            GenreId = 2,
                            Naam = "Science Fiction"
                        },
                        new
                        {
                            GenreId = 3,
                            Naam = "Thriller"
                        },
                        new
                        {
                            GenreId = 4,
                            Naam = "Action"
                        },
                        new
                        {
                            GenreId = 5,
                            Naam = "Comedy"
                        },
                        new
                        {
                            GenreId = 6,
                            Naam = "Romance"
                        });
                });

            modelBuilder.Entity("ManyToManyApp.Models.Boek", b =>
                {
                    b.HasOne("ManyToManyApp.Models.Auteur", "Auteur")
                        .WithMany("Boeken")
                        .HasForeignKey("AuteurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auteur");
                });

            modelBuilder.Entity("ManyToManyApp.Models.BoekGenre", b =>
                {
                    b.HasOne("ManyToManyApp.Models.Boek", "Boek")
                        .WithMany("BoekGenres")
                        .HasForeignKey("BoekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManyToManyApp.Models.Genre", "Genre")
                        .WithMany("BoekGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Boek");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("ManyToManyApp.Models.Genre", b =>
                {
                    b.HasOne("ManyToManyApp.Models.Boek", null)
                        .WithMany("Genres")
                        .HasForeignKey("BoekId");
                });

            modelBuilder.Entity("ManyToManyApp.Models.Auteur", b =>
                {
                    b.Navigation("Boeken");
                });

            modelBuilder.Entity("ManyToManyApp.Models.Boek", b =>
                {
                    b.Navigation("BoekGenres");

                    b.Navigation("Genres");
                });

            modelBuilder.Entity("ManyToManyApp.Models.Genre", b =>
                {
                    b.Navigation("BoekGenres");
                });
#pragma warning restore 612, 618
        }
    }
}
