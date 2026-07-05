using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");
		builder.HasKey((User e) => e.Id);
		builder.Property((User e) => e.EmployeeCode).HasMaxLength(50).IsRequired();
		builder.Property((User e) => e.IdNumber).HasMaxLength(20).IsRequired();
		builder.Property((User e) => e.FirstName).HasMaxLength(100).IsRequired();
		builder.Property((User e) => e.LastName).HasMaxLength(100).IsRequired();
		builder.Property((User e) => e.PasswordHash).HasMaxLength(500).IsRequired();
		builder.Property((User e) => e.Notes).HasMaxLength(1000);
		builder.Property((User e) => e.Email).HasMaxLength(500);
		builder.Property((User e) => e.Phone).HasMaxLength(50);
		builder.Property((User e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.Property((User e) => e.RowVersion).IsRowVersion();
		builder.HasIndex((User e) => e.IdNumber).IsUnique();
		builder.HasOne((User e) => e.Role).WithMany().HasForeignKey((User e) => e.RoleId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((User e) => e.UserRole).WithMany().HasForeignKey((User e) => e.UserRoleId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((User e) => e.Status).WithMany().HasForeignKey((User e) => e.StatusId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne<User>().WithMany().HasForeignKey((User e) => e.CreatedBy)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne<User>().WithMany().HasForeignKey((User e) => e.UpdatedBy)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
