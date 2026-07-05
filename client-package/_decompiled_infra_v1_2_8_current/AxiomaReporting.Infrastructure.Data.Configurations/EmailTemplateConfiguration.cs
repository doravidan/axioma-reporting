using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
	public void Configure(EntityTypeBuilder<EmailTemplate> builder)
	{
		builder.ToTable("EmailTemplates");
		builder.HasKey((EmailTemplate e) => e.Id);
		builder.Property((EmailTemplate e) => e.TypeDescription).HasMaxLength(200).IsRequired();
		builder.Property((EmailTemplate e) => e.Subject).HasMaxLength(500).IsRequired();
		builder.Property((EmailTemplate e) => e.Body).IsRequired();
		builder.Property((EmailTemplate e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
