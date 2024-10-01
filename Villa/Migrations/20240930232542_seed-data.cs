using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Villa.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "villas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "villas",
                type: "date",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "Id", "Amenity", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("029289e5-db33-4632-9eb6-3e7f120d8039"), "Garden, Fireplace, Nature views", "A peaceful cottage in the countryside, perfect for relaxation.", "https://example.com/countryside-cottage.jpg", "Countryside Cottage", 3, 280.0, 900, null },
                    { new Guid("2de61ebe-212e-4420-b995-8e431057ba8a"), "Beachfront, Barbecue, Sun deck", "A charming beach house with direct access to the shore.", "https://example.com/beach-house.jpg", "Beach House", 5, 450.0, 1300, null },
                    { new Guid("41b7c09f-d9d6-4a04-bbff-a894380f00b3"), "Fire pit, Hiking trails, Wildlife viewing", "A rustic cabin nestled in the heart of the forest.", "https://example.com/forest-cabin.jpg", "Forest Cabin", 4, 320.0, 950, null },
                    { new Guid("61954aa9-f6e3-4a1f-9e1b-ed99f3854e95"), "Private elevator, Rooftop pool, City views", "A luxurious penthouse suite in a high-rise building.", "https://example.com/penthouse-suite.jpg", "Penthouse Suite", 8, 800.0, 2000, null },
                    { new Guid("678ced43-4e76-486b-b273-e8f76bcf4fc8"), "Ski-in/ski-out, Fireplace, Sauna", "A cozy chalet located near the ski slopes.", "https://example.com/ski-chalet.jpg", "Ski Chalet", 5, 480.0, 1100, null },
                    { new Guid("6d39f48f-923d-41da-8adb-67e524b6f345"), "Rooftop access, Smart home features, High-speed internet", "A modern loft in the heart of the city with stunning skyline views.", "https://example.com/city-loft.jpg", "City Loft", 2, 400.0, 1000, null },
                    { new Guid("811d2d74-c52b-4bd0-a377-cde0b8dbdb00"), "Infinity pool, Outdoor lounge, Desert tours", "A unique villa in the desert with breathtaking views.", "https://example.com/desert-oasis.jpg", "Desert Oasis", 6, 550.0, 1400, null },
                    { new Guid("81fa35cf-15d1-4f6e-8ed7-0c453839b783"), "Private pool, Wi-Fi, Air conditioning", "A spacious villa with an ocean view and modern amenities.", "https://example.com/luxury-villa.jpg", "Luxury Villa", 6, 500.0, 1500, null },
                    { new Guid("cc48e134-0c1e-4107-81d3-b797fc534138"), "Fireplace, Outdoor jacuzzi, Hiking access", "A cozy mountain villa surrounded by nature and hiking trails.", "https://example.com/mountain-retreat.jpg", "Mountain Retreat", 4, 300.0, 1200, null },
                    { new Guid("cf8c51bb-b82d-44c3-850a-903f48c33cc3"), "Private garden, Outdoor pool, Hammocks", "A tropical-themed villa surrounded by lush greenery.", "https://example.com/tropical-paradise.jpg", "Tropical Paradise", 7, 600.0, 1600, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("029289e5-db33-4632-9eb6-3e7f120d8039"));

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("2de61ebe-212e-4420-b995-8e431057ba8a"));

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("41b7c09f-d9d6-4a04-bbff-a894380f00b3"));

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("61954aa9-f6e3-4a1f-9e1b-ed99f3854e95"));

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("678ced43-4e76-486b-b273-e8f76bcf4fc8"));

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("6d39f48f-923d-41da-8adb-67e524b6f345"));

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("811d2d74-c52b-4bd0-a377-cde0b8dbdb00"));

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("81fa35cf-15d1-4f6e-8ed7-0c453839b783"));

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("cc48e134-0c1e-4107-81d3-b797fc534138"));

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: new Guid("cf8c51bb-b82d-44c3-850a-903f48c33cc3"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "villas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "villas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
