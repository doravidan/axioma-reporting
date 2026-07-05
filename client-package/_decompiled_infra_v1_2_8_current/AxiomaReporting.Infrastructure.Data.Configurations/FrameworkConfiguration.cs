using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class FrameworkConfiguration : IEntityTypeConfiguration<Framework>
{
	public void Configure(EntityTypeBuilder<Framework> builder)
	{
		builder.ToTable("Frameworks");
		builder.HasKey((Framework e) => e.Id);
		builder.Property((Framework e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((Framework e) => e.InstitutionSymbol).HasMaxLength(100).IsRequired();
		builder.Property((Framework e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.HasIndex((Framework e) => new { e.InstitutionSymbol, e.EducationalStageId }).IsUnique();
		builder.HasOne((Framework e) => e.EducationalStage).WithMany().HasForeignKey((Framework e) => e.EducationalStageId)
			.OnDelete(DeleteBehavior.SetNull);
	}
}
