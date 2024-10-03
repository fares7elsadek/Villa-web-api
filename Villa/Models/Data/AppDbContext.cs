using Microsoft.EntityFrameworkCore;
using VillaApp.Models.Data.config;

namespace VillaApp.Models.Data
{
	public class AppDbContext: DbContext
	{
		public DbSet<Villas> villas { get; set; }
		public DbSet<VillaNumber> villaNumber { get; set; }

		public DbSet<User> users { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var conn = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
			var constr = conn.GetSection("ConnectionStrings:cs").Value;
			optionsBuilder.UseSqlServer(constr);
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(VillasConfigurations).Assembly); 
		}


	}
}
