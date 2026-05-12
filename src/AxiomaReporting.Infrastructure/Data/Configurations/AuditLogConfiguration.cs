using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
  public void Configure(EntityTypeBuilder<AuditLog> builder)
  {
    builder.ToTable("AuditLogs");
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Timestamp).HasDefaultValueSql("GETUTCDATE()");
    builder.Property(e => e.Action).HasMaxLength(100).IsRequired();
    builder.Property(e => e.EntityType).HasMaxLength(100).IsRequired();
    builder.Property(e => e.EntityId).HasMaxLength(100);
    builder.Property(e => e.IpAddress).HasMaxLength(64);
    builder.Property(e => e.UserAgent).HasMaxLength(500);
    builder.Property(e => e.Notes).HasMaxLength(1000);

    builder.HasIndex(e => e.Timestamp)
      .HasDatabaseName("IX_AuditLogs_Timestamp");

    builder.HasIndex(e => new { e.EntityType, e.EntityId })
      .HasDatabaseName("IX_AuditLogs_EntityType_EntityId");

    builder.HasIndex(e => new { e.ActorUserId, e.Timestamp })
      .HasDatabaseName("IX_AuditLogs_ActorUserId_Timestamp");

    builder.HasIndex(e => new { e.Action, e.Timestamp })
      .HasDatabaseName("IX_AuditLogs_Action_Timestamp");

    builder.HasOne(e => e.ActorUser)
      .WithMany()
      .HasForeignKey(e => e.ActorUserId)
      .OnDelete(DeleteBehavior.NoAction);
  }
}
