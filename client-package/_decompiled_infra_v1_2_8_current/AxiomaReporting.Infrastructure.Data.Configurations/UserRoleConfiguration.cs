using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
	public void Configure(EntityTypeBuilder<UserRole> builder)
	{
		builder.ToTable("UserRoles");
		builder.HasKey((UserRole e) => e.Id);
		builder.Property((UserRole e) => e.Id).ValueGeneratedNever();
		builder.Property((UserRole e) => e.Name).HasMaxLength(100).IsRequired();
		builder.Property((UserRole e) => e.Description).HasMaxLength(500);
		builder.Property((UserRole e) => e.DescriptionHebrew).HasMaxLength(200);
	}
}
