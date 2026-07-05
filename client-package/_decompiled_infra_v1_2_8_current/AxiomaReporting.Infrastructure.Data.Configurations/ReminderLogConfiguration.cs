using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ReminderLogConfiguration : IEntityTypeConfiguration<ReminderLog>
{
	public void Configure(EntityTypeBuilder<ReminderLog> builder)
	{
		builder.ToTable("ReminderLogs");
		builder.HasKey((ReminderLog e) => e.Id);
		builder.Property((ReminderLog e) => e.TemplateType).HasMaxLength(100).IsRequired();
		builder.HasIndex((ReminderLog e) => new { e.UserId, e.ReportingMonthId, e.TemplateType, e.SentAt });
		builder.HasOne((ReminderLog e) => e.User).WithMany().HasForeignKey((ReminderLog e) => e.UserId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((ReminderLog e) => e.ReportingMonth).WithMany().HasForeignKey((ReminderLog e) => e.ReportingMonthId)
			.IsRequired(required: false)
			.OnDelete(DeleteBehavior.SetNull);
	}
}
