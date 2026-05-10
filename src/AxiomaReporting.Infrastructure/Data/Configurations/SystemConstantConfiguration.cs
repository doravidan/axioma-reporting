using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class SystemConstantConfiguration : IEntityTypeConfiguration<SystemConstant>
{
  public void Configure(EntityTypeBuilder<SystemConstant> builder)
  {
    builder.ToTable("SystemConstants");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.Key).HasMaxLength(200).IsRequired();
    builder.Property(e => e.Value).HasMaxLength(1000).IsRequired();
    builder.Property(e => e.Description).HasMaxLength(500);
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

    builder.HasIndex(e => e.Key).IsUnique();
  }
}
