using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
	public void Configure(EntityTypeBuilder<District> builder)
	{
		builder.ToTable("Districts");
		builder.HasKey((District e) => e.Id);
		builder.Property((District e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((District e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
