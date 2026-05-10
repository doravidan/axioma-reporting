using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationConfiguration : IEntityTypeConfiguration<Allocation>
{
  public void Configure(EntityTypeBuilder<Allocation> builder)
  {
    builder.ToTable("Allocations");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.AnnualEmploymentScope).HasPrecision(18, 4);
    builder.Property(e => e.MonthlyEmploymentScope).HasPrecision(18, 4);
    builder.Property(e => e.DailyEmploymentScope).HasPrecision(18, 4);
    // OutputDuration stored as NVARCHAR (comma-separated decimal values e.g. "0.5,1,1.5,2")
    builder.Property(e => e.OutputDuration).HasMaxLength(500);
    builder.Property(e => e.Notes).HasMaxLength(1000);
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    builder.Property(e => e.RowVersion).IsRowVersion();

    // Unique constraint: one allocation per employee per project
    builder.HasIndex(e => new { e.UserId, e.ProjectId }).IsUnique();

    builder.HasOne(e => e.User)
      .WithMany(u => u.Allocations)
      .HasForeignKey(e => e.UserId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(e => e.Project)
      .WithMany()
      .HasForeignKey(e => e.ProjectId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
