namespace VillaApp.Models
{
	public class VillaNumber
	{
		public Guid Id { get; set; }

		public string SpecialDetails { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public Guid VillaId { get; set; }
		public Villas Villa { get; set; }

	}
}
