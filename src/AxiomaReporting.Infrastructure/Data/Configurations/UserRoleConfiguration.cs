using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.ToTable("UserRoles");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.Id).ValueGeneratedNever();
    builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
    builder.Property(e => e.Description).HasMaxLength(500);
    builder.Property(e => e.DescriptionHebrew).HasMaxLength(200);
  }
}
