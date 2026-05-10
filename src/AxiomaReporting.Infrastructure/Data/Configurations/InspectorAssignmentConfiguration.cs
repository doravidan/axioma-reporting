using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class InspectorAssignmentConfiguration : IEntityTypeConfiguration<InspectorAssignment>
{
  public void Configure(EntityTypeBuilder<InspectorAssignment> builder)
  {
    builder.ToTable("InspectorAssignments");
    builder.HasKey(e => e.Id);

    // All FKs are nullable (NULL = wildcard for scoping)
    builder.HasOne(e => e.Inspector)
      .WithMany()
      .HasForeignKey(e => e.InspectorUserId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Program)
      .WithMany()
      .HasForeignKey(e => e.ProgramId)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.District)
      .WithMany()
      .HasForeignKey(e => e.DistrictId)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.Sector)
      .WithMany()
      .HasForeignKey(e => e.SectorId)
      .OnDelete(DeleteBehavior.NoAction);
  }
}
