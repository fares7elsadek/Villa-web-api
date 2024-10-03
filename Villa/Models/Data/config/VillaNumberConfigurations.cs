using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VillaApp.Models.Data.config
{
	public class VillaNumberConfigurations : IEntityTypeConfiguration<VillaNumber>
	{
		public void Configure(EntityTypeBuilder<VillaNumber> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasDefaultValueSql("newid()");

			builder.Property(x => x.SpecialDetails)
				.HasColumnType("text")
				.IsRequired();

			builder.Property(x => x.CreatedAt)
				.HasColumnType("date")
				.HasDefaultValueSql("GETDATE()");
		}
	}
}
