using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class NotificationLogConfiguration : IEntityTypeConfiguration<NotificationLog>
{
	public void Configure(EntityTypeBuilder<NotificationLog> builder)
	{
		builder.ToTable("NotificationLogs");
		builder.HasKey((NotificationLog e) => e.Id);
		builder.Property((NotificationLog e) => e.NotificationType).HasMaxLength(50).IsRequired();
		builder.Property((NotificationLog e) => e.TemplateType).HasMaxLength(100).IsRequired();
		builder.Property((NotificationLog e) => e.RecipientEmail).HasMaxLength(500).IsRequired();
		builder.Property((NotificationLog e) => e.Subject).HasMaxLength(500).IsRequired();
		builder.Property((NotificationLog e) => e.Body).IsRequired();
		builder.Property((NotificationLog e) => e.Status).HasMaxLength(20).IsRequired()
			.HasDefaultValue("Pending");
		builder.Property((NotificationLog e) => e.AttemptCount).HasDefaultValue(0);
		builder.Property((NotificationLog e) => e.FailureReason).HasMaxLength(2000);
		builder.Property((NotificationLog e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.HasIndex((NotificationLog e) => new { e.Status, e.NextRetryAt }).HasDatabaseName("IX_NotificationLogs_Status_NextRetryAt");
		builder.HasIndex((NotificationLog e) => new { e.RecipientUserId, e.CreatedAt }).HasDatabaseName("IX_NotificationLogs_RecipientUserId_CreatedAt");
		builder.HasIndex((NotificationLog e) => new { e.TemplateType, e.CreatedAt }).HasDatabaseName("IX_NotificationLogs_TemplateType_CreatedAt");
		builder.HasOne((NotificationLog e) => e.RecipientUser).WithMany().HasForeignKey((NotificationLog e) => e.RecipientUserId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((NotificationLog e) => e.RelatedReport).WithMany().HasForeignKey((NotificationLog e) => e.RelatedReportId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((NotificationLog e) => e.RelatedReportingMonth).WithMany().HasForeignKey((NotificationLog e) => e.RelatedReportingMonthId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
