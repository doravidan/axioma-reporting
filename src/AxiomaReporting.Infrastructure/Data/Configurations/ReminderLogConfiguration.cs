using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ReminderLogConfiguration : IEntityTypeConfiguration<ReminderLog>
{
  public void Configure(EntityTypeBuilder<ReminderLog> builder)
  {
    builder.ToTable("ReminderLogs");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.TemplateType).HasMaxLength(100).IsRequired();
    builder.HasIndex(e => new { e.UserId, e.ReportingMonthId, e.TemplateType, e.SentAt });

    builder.HasOne(e => e.User)
      .WithMany()
      .HasForeignKey(e => e.UserId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.ReportingMonth)
      .WithMany()
      .HasForeignKey(e => e.ReportingMonthId)
      .IsRequired(false)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
