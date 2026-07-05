using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class SectorConfiguration : IEntityTypeConfiguration<Sector>
{
	public void Configure(EntityTypeBuilder<Sector> builder)
	{
		builder.ToTable("Sectors");
		builder.HasKey((Sector e) => e.Id);
		builder.Property((Sector e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((Sector e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
