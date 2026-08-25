using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
  public void Configure(EntityTypeBuilder<Report> builder)
  {
    builder.ToTable("Reports");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.RejectionReason).HasMaxLength(1000);
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    builder.Property(e => e.RowVersion).IsRowVersion();

    // A logically deleted report remains available for audit/history, but must
    // not block a fresh active report for the same employee and month.
    builder.HasIndex(e => new { e.UserId, e.ReportingMonthId })
      .IsUnique()
      .HasFilter("[IsArchived] = 0");

    builder.HasOne(e => e.User)
      .WithMany(u => u.Reports)
      .HasForeignKey(e => e.UserId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(e => e.ReportingMonth)
      .WithMany(rm => rm.Reports)
      .HasForeignKey(e => e.ReportingMonthId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(e => e.Status)
      .WithMany()
      .HasForeignKey(e => e.StatusId)
      .OnDelete(DeleteBehavior.Restrict);

    // Self-referencing FKs with NoAction to avoid multiple cascade paths
    builder.HasOne(e => e.ApprovedByUser)
      .WithMany()
      .HasForeignKey(e => e.ApprovedBy)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.RejectedByUser)
      .WithMany()
      .HasForeignKey(e => e.RejectedBy)
      .OnDelete(DeleteBehavior.NoAction);
  }
}
