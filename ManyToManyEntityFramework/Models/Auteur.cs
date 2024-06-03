using System.ComponentModel.DataAnnotations;

namespace ManyToManyApp.Models
{
    public class Auteur
    {
        public int AuteurId { get; set; }
        public string Naam { get; set; }
        public ICollection<Boek> Boeken { get; set; }
    }
}