using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class NotificationLogConfiguration : IEntityTypeConfiguration<NotificationLog>
{
  public void Configure(EntityTypeBuilder<NotificationLog> builder)
  {
    builder.ToTable("NotificationLogs");
    builder.HasKey(e => e.Id);

    builder.Property(e => e.NotificationType).HasMaxLength(50).IsRequired();
    builder.Property(e => e.TemplateType).HasMaxLength(100).IsRequired();
    builder.Property(e => e.RecipientEmail).HasMaxLength(500).IsRequired();
    builder.Property(e => e.Subject).HasMaxLength(500).IsRequired();
    builder.Property(e => e.Body).IsRequired();
    builder.Property(e => e.Status).HasMaxLength(20).IsRequired().HasDefaultValue("Pending");
    builder.Property(e => e.AttemptCount).HasDefaultValue(0);
    builder.Property(e => e.FailureReason).HasMaxLength(2000);
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

    builder.HasIndex(e => new { e.Status, e.NextRetryAt })
      .HasDatabaseName("IX_NotificationLogs_Status_NextRetryAt");

    builder.HasIndex(e => new { e.RecipientUserId, e.CreatedAt })
      .IsDescending(false, true)
      .HasDatabaseName("IX_NotificationLogs_RecipientUserId_CreatedAt");

    builder.HasIndex(e => new { e.TemplateType, e.CreatedAt })
      .IsDescending(false, true)
      .HasDatabaseName("IX_NotificationLogs_TemplateType_CreatedAt");

    builder.HasOne(e => e.RecipientUser)
      .WithMany()
      .HasForeignKey(e => e.RecipientUserId)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.RelatedReport)
      .WithMany()
      .HasForeignKey(e => e.RelatedReportId)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.RelatedReportingMonth)
      .WithMany()
      .HasForeignKey(e => e.RelatedReportingMonthId)
      .OnDelete(DeleteBehavior.NoAction);
  }
}
