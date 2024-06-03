using ManyToManyApp.Enums;

namespace ManyToManyApp.Models
{
    public class Boek
    {
        public int BoekId { get; set; }
        public string Titel { get; set; }
        public int AuteurId { get; set; }
        public Auteur? Auteur { get; set; }

        public bool IsAvailable { get; set; }
        public bool IsNewRelease { get; set; }
        public bool IsBestSeller { get; set; }
        public BindingType? BindingType { get; set; }
        public string? Afbeeldingpad { get; set; } = "/images/default.jpg";




        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<BoekGenre> BoekGenres { get; set; } = new List<BoekGenre>();
    }
}