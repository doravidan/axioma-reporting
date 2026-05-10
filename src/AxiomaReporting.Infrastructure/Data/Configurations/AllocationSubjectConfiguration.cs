using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationSubjectConfiguration : IEntityTypeConfiguration<AllocationSubject>
{
  public void Configure(EntityTypeBuilder<AllocationSubject> builder)
  {
    builder.ToTable("AllocationSubjects");
    builder.HasKey(e => new { e.AllocationId, e.SubjectId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationSubjects)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Subject)
      .WithMany()
      .HasForeignKey(e => e.SubjectId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
