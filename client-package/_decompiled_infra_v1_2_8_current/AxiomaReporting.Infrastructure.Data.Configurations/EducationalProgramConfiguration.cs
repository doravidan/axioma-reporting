using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class EducationalProgramConfiguration : IEntityTypeConfiguration<EducationalProgram>
{
	public void Configure(EntityTypeBuilder<EducationalProgram> builder)
	{
		builder.ToTable("EducationalPrograms");
		builder.HasKey((EducationalProgram e) => e.Id);
		builder.Property((EducationalProgram e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((EducationalProgram e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
