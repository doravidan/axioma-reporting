using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.EmployeeCode).HasMaxLength(50).IsRequired();
    builder.Property(e => e.IdNumber).HasMaxLength(20).IsRequired();
    builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
    builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();
    builder.Property(e => e.PasswordHash).HasMaxLength(500).IsRequired();
    builder.Property(e => e.Notes).HasMaxLength(1000);
    builder.Property(e => e.Email).HasMaxLength(500);
    builder.Property(e => e.Phone).HasMaxLength(50);
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
    builder.Property(e => e.RowVersion).IsRowVersion();

    builder.HasIndex(e => e.IdNumber).IsUnique();

    // FK to EmployeeRole (lookup role)
    builder.HasOne(e => e.Role)
      .WithMany()
      .HasForeignKey(e => e.RoleId)
      .OnDelete(DeleteBehavior.Restrict);

    // FK to UserRole (system role)
    builder.HasOne(e => e.UserRole)
      .WithMany()
      .HasForeignKey(e => e.UserRoleId)
      .OnDelete(DeleteBehavior.Restrict);

    // FK to UserStatus
    builder.HasOne(e => e.Status)
      .WithMany()
      .HasForeignKey(e => e.StatusId)
      .OnDelete(DeleteBehavior.Restrict);

    // Self-referencing FKs — use NoAction to avoid multiple cascade paths
    builder.HasOne<User>()
      .WithMany()
      .HasForeignKey(e => e.CreatedBy)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne<User>()
      .WithMany()
      .HasForeignKey(e => e.UpdatedBy)
      .OnDelete(DeleteBehavior.NoAction);

    // Reports navigation (User → Reports) configured in ReportConfiguration
  }
}
