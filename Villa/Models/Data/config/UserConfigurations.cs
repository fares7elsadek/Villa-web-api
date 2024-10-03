using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VillaApp.Models.Data.config
{
	public class UserConfigurations : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasDefaultValueSql("newid()");

			builder.ToTable("Users");
		}
	}
}
