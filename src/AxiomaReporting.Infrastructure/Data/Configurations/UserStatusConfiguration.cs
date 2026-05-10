using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
{
  public void Configure(EntityTypeBuilder<UserStatus> builder)
  {
    builder.ToTable("UserStatuses");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.Id).ValueGeneratedNever();
    builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
    builder.Property(e => e.DescriptionHebrew).HasMaxLength(200);
  }
}
