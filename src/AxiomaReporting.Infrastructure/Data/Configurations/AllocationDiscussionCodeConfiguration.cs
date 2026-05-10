using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationDiscussionCodeConfiguration : IEntityTypeConfiguration<AllocationDiscussionCode>
{
  public void Configure(EntityTypeBuilder<AllocationDiscussionCode> builder)
  {
    builder.ToTable("AllocationDiscussionCodes");
    builder.HasKey(e => new { e.AllocationId, e.DiscussionCodeId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationDiscussionCodes)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.DiscussionCode)
      .WithMany()
      .HasForeignKey(e => e.DiscussionCodeId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
