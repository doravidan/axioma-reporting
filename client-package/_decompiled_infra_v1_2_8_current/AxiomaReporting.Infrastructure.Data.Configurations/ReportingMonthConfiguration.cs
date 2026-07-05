using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ReportingMonthConfiguration : IEntityTypeConfiguration<ReportingMonth>
{
	public void Configure(EntityTypeBuilder<ReportingMonth> builder)
	{
		builder.ToTable("ReportingMonths");
		builder.HasKey((ReportingMonth e) => e.Id);
		builder.Property((ReportingMonth e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((ReportingMonth e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
