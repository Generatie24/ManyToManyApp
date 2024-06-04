namespace ManyToManyApp.Models.ViewModels
{
    public class CreateBoekViewModel
    {
        public Boek? Boek { get; set; }
        public int SelectedAuteurId { get; set; }
        public List<int>? SelectedGenres { get; set; }
        public List<Auteur>? Auteurs { get; set; }
        public List<Genre>? Genres { get; set; }

        public  IFormFile? Afbeelding { get; set; }

        public string? AfbeeldingPad { get; set; } = "/images/Default.jpg";
    }
}
