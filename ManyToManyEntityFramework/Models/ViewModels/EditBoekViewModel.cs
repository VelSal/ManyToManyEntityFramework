using ManyToManyApp.Enums;
using ManyToManyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ManyToManyEntityFramework.Models.ViewModels
{
	public class EditBoekViewModel
	{
		public int BoekId { get; set; }
		[Required]
		public string Titel { get; set; }
		public int SelectedAuteurId { get; set; }
		public List<int> SelectedGenres { get; set; }
		public bool IsAvailable { get; set; }
		public bool IsNewRelease { get; set; }
		public bool IsBestSeller { get; set; }
		public BindingType? BindingType { get; set; }
		public List<Auteur>? Auteurs { get; set; }
		public List<Genre>? Genres { get; set; }
		public IFormFile? Afbeelding { get; set; }
		public string? AfbeeldingPad { get; set; } = "/images/Default.png";
	}
}
