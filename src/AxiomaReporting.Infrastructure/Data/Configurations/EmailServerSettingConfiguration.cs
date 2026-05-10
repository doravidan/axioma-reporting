using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class EmailServerSettingConfiguration : IEntityTypeConfiguration<EmailServerSetting>
{
  public void Configure(EntityTypeBuilder<EmailServerSetting> builder)
  {
    builder.ToTable("EmailServerSettings");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.SmtpServer).HasMaxLength(500).IsRequired();
    builder.Property(e => e.Username).HasMaxLength(500).IsRequired();
    builder.Property(e => e.Password).HasMaxLength(500).IsRequired();
    builder.Property(e => e.FromAddress).HasMaxLength(500).IsRequired();
    builder.Property(e => e.FromName).HasMaxLength(500);
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
  }
}
