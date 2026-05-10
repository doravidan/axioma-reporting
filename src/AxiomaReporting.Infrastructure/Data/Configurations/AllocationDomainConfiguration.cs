using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationDomainConfiguration : IEntityTypeConfiguration<AllocationDomain>
{
  public void Configure(EntityTypeBuilder<AllocationDomain> builder)
  {
    builder.ToTable("AllocationDomains");
    builder.HasKey(e => new { e.AllocationId, e.DomainId });

    builder.HasOne(e => e.Allocation)
      .WithMany(a => a.AllocationDomains)
      .HasForeignKey(e => e.AllocationId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Domain)
      .WithMany()
      .HasForeignKey(e => e.DomainId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
