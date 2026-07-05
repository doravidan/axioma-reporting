using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class EducationalStageConfiguration : IEntityTypeConfiguration<EducationalStage>
{
	public void Configure(EntityTypeBuilder<EducationalStage> builder)
	{
		builder.ToTable("EducationalStages");
		builder.HasKey((EducationalStage e) => e.Id);
		builder.Property((EducationalStage e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((EducationalStage e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
