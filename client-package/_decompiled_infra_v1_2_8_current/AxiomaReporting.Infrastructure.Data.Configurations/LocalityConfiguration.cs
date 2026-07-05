using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class LocalityConfiguration : IEntityTypeConfiguration<Locality>
{
	public void Configure(EntityTypeBuilder<Locality> builder)
	{
		builder.ToTable("Localities");
		builder.HasKey((Locality e) => e.Id);
		builder.Property((Locality e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((Locality e) => e.NationalCode);
		builder.Property((Locality e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
