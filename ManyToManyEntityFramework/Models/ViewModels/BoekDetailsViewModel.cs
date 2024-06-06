namespace ManyToManyEntityFramework.Models.ViewModels
{
	public class BoekDetailsViewModel
	{
		public int BoekId { get; set; }
		public string Titel { get; set; }
		public string AuteurNaam { get; set; }
		public bool IsAvailable { get; set; }
		public bool IsNewRelease { get; set; }
		public bool IsBestSeller { get; set; }
		public string BindingType { get; set; }
		public List<string> GenreNamen { get; set; } = new List<string>();
		public string AfbeeldingPad { get; set; }
	}
}
