using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ReportRowConfiguration : IEntityTypeConfiguration<ReportRow>
{
  public void Configure(EntityTypeBuilder<ReportRow> builder)
  {
    builder.ToTable("ReportRows");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.MeetingDuration).HasPrecision(18, 4);
    builder.Property(e => e.Notes).HasMaxLength(2000);
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    builder.Property(e => e.RowVersion).IsRowVersion();

    builder.HasOne(e => e.Report)
      .WithMany(r => r.ReportRows)
      .HasForeignKey(e => e.ReportId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Allocation)
      .WithMany()
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.District)
      .WithMany()
      .HasForeignKey(e => e.DistrictId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(e => e.Locality)
      .WithMany()
      .HasForeignKey(e => e.LocalityId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(e => e.Framework)
      .WithMany()
      .HasForeignKey(e => e.FrameworkId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(e => e.EducationalProgram)
      .WithMany()
      .HasForeignKey(e => e.EducationalProgramId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(e => e.Domain)
      .WithMany()
      .HasForeignKey(e => e.DomainId)
      .OnDelete(DeleteBehavior.Restrict);

    // Two FKs to Subjects table — must specify explicit FK names
    builder.HasOne(e => e.Subject1)
      .WithMany()
      .HasForeignKey(e => e.Subject1Id)
      .HasConstraintName("FK_ReportRows_Subjects_Subject1Id")
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(e => e.Subject2)
      .WithMany()
      .HasForeignKey(e => e.Subject2Id)
      .HasConstraintName("FK_ReportRows_Subjects_Subject2Id")
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.DiscussionCode)
      .WithMany()
      .HasForeignKey(e => e.DiscussionCodeId)
      .OnDelete(DeleteBehavior.NoAction);

    // Conclusion lookups live in their own tables (מסקנות כיתה / מסקנות מסגרת),
    // separate from the base SchoolClasses / Frameworks so the dropdowns don't mix.
    builder.HasOne(e => e.ConclusionClass)
      .WithMany()
      .HasForeignKey(e => e.ConclusionClassId)
      .HasConstraintName("FK_ReportRows_ClassConclusions_ConclusionClassId")
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.ConclusionFramework)
      .WithMany()
      .HasForeignKey(e => e.ConclusionFrameworkId)
      .HasConstraintName("FK_ReportRows_FrameworkConclusions_ConclusionFrameworkId")
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.ConclusionLocation)
      .WithMany()
      .HasForeignKey(e => e.ConclusionLocationId)
      .HasConstraintName("FK_ReportRows_LocalityDistrictNational_ConclusionLocationId")
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.Class)
      .WithMany()
      .HasForeignKey(e => e.ClassId)
      .HasConstraintName("FK_ReportRows_SchoolClasses_ClassId")
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.GradeLevel)
      .WithMany()
      .HasForeignKey(e => e.GradeLevelId)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.ReportType)
      .WithMany()
      .HasForeignKey(e => e.ReportTypeId)
      .OnDelete(DeleteBehavior.NoAction);
  }
}
