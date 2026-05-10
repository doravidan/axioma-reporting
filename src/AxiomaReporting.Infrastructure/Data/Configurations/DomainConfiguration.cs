using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class DomainConfiguration : IEntityTypeConfiguration<Domain>
{
  public void Configure(EntityTypeBuilder<Domain> builder)
  {
    builder.ToTable("Domains");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.Description).HasMaxLength(500).IsRequired();
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
  }
}
