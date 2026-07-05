using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class SchoolClassConfiguration : IEntityTypeConfiguration<SchoolClass>
{
	public void Configure(EntityTypeBuilder<SchoolClass> builder)
	{
		builder.ToTable("SchoolClasses");
		builder.HasKey((SchoolClass e) => e.Id);
		builder.Property((SchoolClass e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((SchoolClass e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
