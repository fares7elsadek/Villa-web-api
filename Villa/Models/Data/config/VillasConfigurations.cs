using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VillaApp.Models.Data.config
{
	public class VillasConfigurations : IEntityTypeConfiguration<Villas>
	{
		public void Configure(EntityTypeBuilder<Villas> builder)
		{
			builder.HasKey(v => v.Id);
			builder.Property(v => v.Id)
				.HasDefaultValueSql("newid()");

			builder.Property(v => v.Name)
				.HasColumnType("varchar")
				.HasMaxLength(255);

			builder.Property(v => v.Details)
				.HasColumnType("text");

			builder.Property(v => v.ImageUrl)
				.HasColumnType("text");

			builder.Property(v => v.Amenity)
				.HasColumnType("text");

			builder.Property(v => v.CreatedAt)
				 .HasColumnType("date")
				.HasDefaultValueSql("GETDATE()");


			builder.HasMany(x => x.VillaNumbers)
				.WithOne(x => x.Villa)
				.HasForeignKey(x => x.VillaId)
				.OnDelete(DeleteBehavior.Cascade);


			//builder.HasData(LoadData());
		}

		private List<Villas> LoadData()
		{
			return new List<Villas>
			{
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "Luxury Villa",
					Details = "A spacious villa with an ocean view and modern amenities.",
					Rate = 500.0,
					Sqft = 1500,
					Occupancy = 6,
					ImageUrl = "https://example.com/luxury-villa.jpg",
					Amenity = "Private pool, Wi-Fi, Air conditioning"
				},
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "Mountain Retreat",
					Details = "A cozy mountain villa surrounded by nature and hiking trails.",
					Rate = 300.0,
					Sqft = 1200,
					Occupancy = 4,
					ImageUrl = "https://example.com/mountain-retreat.jpg",
					Amenity = "Fireplace, Outdoor jacuzzi, Hiking access"
				},
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "City Loft",
					Details = "A modern loft in the heart of the city with stunning skyline views.",
					Rate = 400.0,
					Sqft = 1000,
					Occupancy = 2,
					ImageUrl = "https://example.com/city-loft.jpg",
					Amenity = "Rooftop access, Smart home features, High-speed internet"
				},
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "Beach House",
					Details = "A charming beach house with direct access to the shore.",
					Rate = 450.0,
					Sqft = 1300,
					Occupancy = 5,
					ImageUrl = "https://example.com/beach-house.jpg",
					Amenity = "Beachfront, Barbecue, Sun deck"
				},
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "Countryside Cottage",
					Details = "A peaceful cottage in the countryside, perfect for relaxation.",
					Rate = 280.0,
					Sqft = 900,
					Occupancy = 3,
					ImageUrl = "https://example.com/countryside-cottage.jpg",
					Amenity = "Garden, Fireplace, Nature views"
				},
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "Desert Oasis",
					Details = "A unique villa in the desert with breathtaking views.",
					Rate = 550.0,
					Sqft = 1400,
					Occupancy = 6,
					ImageUrl = "https://example.com/desert-oasis.jpg",
					Amenity = "Infinity pool, Outdoor lounge, Desert tours"
				},
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "Tropical Paradise",
					Details = "A tropical-themed villa surrounded by lush greenery.",
					Rate = 600.0,
					Sqft = 1600,
					Occupancy = 7,
					ImageUrl = "https://example.com/tropical-paradise.jpg",
					Amenity = "Private garden, Outdoor pool, Hammocks"
				},
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "Ski Chalet",
					Details = "A cozy chalet located near the ski slopes.",
					Rate = 480.0,
					Sqft = 1100,
					Occupancy = 5,
					ImageUrl = "https://example.com/ski-chalet.jpg",
					Amenity = "Ski-in/ski-out, Fireplace, Sauna"
				},
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "Forest Cabin",
					Details = "A rustic cabin nestled in the heart of the forest.",
					Rate = 320.0,
					Sqft = 950,
					Occupancy = 4,
					ImageUrl = "https://example.com/forest-cabin.jpg",
					Amenity = "Fire pit, Hiking trails, Wildlife viewing"
				},
				new Villas
				{
					Id = Guid.NewGuid(),
					Name = "Penthouse Suite",
					Details = "A luxurious penthouse suite in a high-rise building.",
					Rate = 800.0,
					Sqft = 2000,
					Occupancy = 8,
					ImageUrl = "https://example.com/penthouse-suite.jpg",
					Amenity = "Private elevator, Rooftop pool, City views"
				}
			};
		}

	}
}
