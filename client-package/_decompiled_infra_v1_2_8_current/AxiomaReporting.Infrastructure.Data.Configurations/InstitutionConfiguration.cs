using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
{
	public void Configure(EntityTypeBuilder<Institution> builder)
	{
		builder.ToTable("Institutions");
		builder.HasKey((Institution e) => e.Id);
		builder.Property((Institution e) => e.Name).HasMaxLength(500).IsRequired();
		builder.Property((Institution e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.HasIndex((Institution e) => new { e.InstitutionSymbol, e.EducationalStageId }).IsUnique();
		builder.HasOne((Institution e) => e.Locality).WithMany().HasForeignKey((Institution e) => e.LocalityId)
			.OnDelete(DeleteBehavior.SetNull);
		builder.HasOne((Institution e) => e.District).WithMany().HasForeignKey((Institution e) => e.DistrictId)
			.OnDelete(DeleteBehavior.SetNull);
		builder.HasOne((Institution e) => e.Sector).WithMany().HasForeignKey((Institution e) => e.SectorId)
			.OnDelete(DeleteBehavior.SetNull);
		builder.HasOne((Institution e) => e.Type).WithMany().HasForeignKey((Institution e) => e.TypeId)
			.OnDelete(DeleteBehavior.SetNull);
		builder.HasOne((Institution e) => e.EducationalStage).WithMany().HasForeignKey((Institution e) => e.EducationalStageId)
			.OnDelete(DeleteBehavior.SetNull);
	}
}
