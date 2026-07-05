using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
	public void Configure(EntityTypeBuilder<AuditLog> builder)
	{
		builder.ToTable("AuditLogs");
		builder.HasKey((AuditLog e) => e.Id);
		builder.Property((AuditLog e) => e.Timestamp).HasDefaultValueSql("GETUTCDATE()");
		builder.Property((AuditLog e) => e.Action).HasMaxLength(100).IsRequired();
		builder.Property((AuditLog e) => e.EntityType).HasMaxLength(100).IsRequired();
		builder.Property((AuditLog e) => e.EntityId).HasMaxLength(100);
		builder.Property((AuditLog e) => e.IpAddress).HasMaxLength(64);
		builder.Property((AuditLog e) => e.UserAgent).HasMaxLength(500);
		builder.Property((AuditLog e) => e.Notes).HasMaxLength(1000);
		builder.HasIndex((AuditLog e) => e.Timestamp).HasDatabaseName("IX_AuditLogs_Timestamp");
		builder.HasIndex((AuditLog e) => new { e.EntityType, e.EntityId }).HasDatabaseName("IX_AuditLogs_EntityType_EntityId");
		builder.HasIndex((AuditLog e) => new { e.ActorUserId, e.Timestamp }).HasDatabaseName("IX_AuditLogs_ActorUserId_Timestamp");
		builder.HasIndex((AuditLog e) => new { e.Action, e.Timestamp }).HasDatabaseName("IX_AuditLogs_Action_Timestamp");
		builder.HasOne((AuditLog e) => e.ActorUser).WithMany().HasForeignKey((AuditLog e) => e.ActorUserId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
