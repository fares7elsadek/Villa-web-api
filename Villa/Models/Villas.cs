namespace VillaApp.Models
{
	public class Villas
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public string Details { get; set; }

		public Double Rate { get; set; }

		public int Sqft { get; set; }

		public int Occupancy { get; set; }

		public string ImageUrl { get; set; }

		public string Amenity { get; set; }
		public DateTime? CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }
	}
}
