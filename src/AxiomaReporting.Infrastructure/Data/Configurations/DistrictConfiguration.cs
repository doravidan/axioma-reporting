using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
  public void Configure(EntityTypeBuilder<District> builder)
  {
    builder.ToTable("Districts");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.Description).HasMaxLength(500).IsRequired();
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
  }
}
