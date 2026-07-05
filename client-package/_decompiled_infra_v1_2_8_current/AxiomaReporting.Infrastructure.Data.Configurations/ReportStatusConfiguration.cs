using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ReportStatusConfiguration : IEntityTypeConfiguration<ReportStatus>
{
	public void Configure(EntityTypeBuilder<ReportStatus> builder)
	{
		builder.ToTable("ReportStatuses");
		builder.HasKey((ReportStatus e) => e.Id);
		builder.Property((ReportStatus e) => e.Id).ValueGeneratedNever();
		builder.Property((ReportStatus e) => e.Name).HasMaxLength(100).IsRequired();
		builder.Property((ReportStatus e) => e.Description).HasMaxLength(500);
	}
}
