using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
  public void Configure(EntityTypeBuilder<EmailTemplate> builder)
  {
    builder.ToTable("EmailTemplates");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.TypeDescription).HasMaxLength(200).IsRequired();
    builder.Property(e => e.Subject).HasMaxLength(500).IsRequired();
    builder.Property(e => e.Body).IsRequired();
    builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
  }
}
