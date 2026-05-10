using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class LocalityDistrictNationalConfiguration : IEntityTypeConfiguration<LocalityDistrictNational>
{
  public void Configure(EntityTypeBuilder<LocalityDistrictNational> builder)
  {
    builder.ToTable("LocalityDistrictNationals");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.Description).HasMaxLength(500).IsRequired();
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
  }
}
