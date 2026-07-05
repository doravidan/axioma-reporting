using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ReportTypeConfiguration : IEntityTypeConfiguration<ReportType>
{
	public void Configure(EntityTypeBuilder<ReportType> builder)
	{
		builder.ToTable("ReportTypes");
		builder.HasKey((ReportType e) => e.Id);
		builder.Property((ReportType e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((ReportType e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
