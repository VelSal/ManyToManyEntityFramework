namespace ManyToManyApp.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Naam { get; set; }
        public ICollection<BoekGenre> BoekGenres { get; set; } = new List<BoekGenre>();
    }
}