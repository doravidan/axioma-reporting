using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class EducationTypeConfiguration : IEntityTypeConfiguration<EducationType>
{
	public void Configure(EntityTypeBuilder<EducationType> builder)
	{
		builder.ToTable("EducationTypes");
		builder.HasKey((EducationType e) => e.Id);
		builder.Property((EducationType e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((EducationType e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
