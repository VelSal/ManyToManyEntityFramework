namespace ManyToManyApp.Models
{
    public class BoekGenre
    {
        public int BoekId { get; set; }
        public Boek Boek { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}