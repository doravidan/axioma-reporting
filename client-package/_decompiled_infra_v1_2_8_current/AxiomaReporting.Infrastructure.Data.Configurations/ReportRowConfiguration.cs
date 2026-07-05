using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ReportRowConfiguration : IEntityTypeConfiguration<ReportRow>
{
	public void Configure(EntityTypeBuilder<ReportRow> builder)
	{
		builder.ToTable("ReportRows");
		builder.HasKey((ReportRow e) => e.Id);
		builder.Property((ReportRow e) => e.MeetingDuration).HasPrecision(18, 4);
		builder.Property((ReportRow e) => e.Notes).HasMaxLength(2000);
		builder.Property((ReportRow e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.Property((ReportRow e) => e.RowVersion).IsRowVersion();
		builder.HasOne((ReportRow e) => e.Report).WithMany((Report r) => r.ReportRows).HasForeignKey((ReportRow e) => e.ReportId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((ReportRow e) => e.Allocation).WithMany().HasForeignKey((ReportRow e) => e.AllocationId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((ReportRow e) => e.District).WithMany().HasForeignKey((ReportRow e) => e.DistrictId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((ReportRow e) => e.Locality).WithMany().HasForeignKey((ReportRow e) => e.LocalityId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((ReportRow e) => e.Framework).WithMany().HasForeignKey((ReportRow e) => e.FrameworkId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((ReportRow e) => e.EducationalProgram).WithMany().HasForeignKey((ReportRow e) => e.EducationalProgramId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((ReportRow e) => e.Domain).WithMany().HasForeignKey((ReportRow e) => e.DomainId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((ReportRow e) => e.Subject1).WithMany().HasForeignKey((ReportRow e) => e.Subject1Id)
			.HasConstraintName("FK_ReportRows_Subjects_Subject1Id")
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((ReportRow e) => e.Subject2).WithMany().HasForeignKey((ReportRow e) => e.Subject2Id)
			.HasConstraintName("FK_ReportRows_Subjects_Subject2Id")
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((ReportRow e) => e.DiscussionCode).WithMany().HasForeignKey((ReportRow e) => e.DiscussionCodeId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((ReportRow e) => e.ConclusionClass).WithMany().HasForeignKey((ReportRow e) => e.ConclusionClassId)
			.HasConstraintName("FK_ReportRows_SchoolClasses_ConclusionClassId")
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((ReportRow e) => e.ConclusionFramework).WithMany().HasForeignKey((ReportRow e) => e.ConclusionFrameworkId)
			.HasConstraintName("FK_ReportRows_Frameworks_ConclusionFrameworkId")
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((ReportRow e) => e.ConclusionLocation).WithMany().HasForeignKey((ReportRow e) => e.ConclusionLocationId)
			.HasConstraintName("FK_ReportRows_LocalityDistrictNational_ConclusionLocationId")
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((ReportRow e) => e.Class).WithMany().HasForeignKey((ReportRow e) => e.ClassId)
			.HasConstraintName("FK_ReportRows_SchoolClasses_ClassId")
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((ReportRow e) => e.GradeLevel).WithMany().HasForeignKey((ReportRow e) => e.GradeLevelId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((ReportRow e) => e.ReportType).WithMany().HasForeignKey((ReportRow e) => e.ReportTypeId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
