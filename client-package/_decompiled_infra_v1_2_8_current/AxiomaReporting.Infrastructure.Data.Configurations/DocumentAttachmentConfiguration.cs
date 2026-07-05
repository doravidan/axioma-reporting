using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class DocumentAttachmentConfiguration : IEntityTypeConfiguration<DocumentAttachment>
{
	public void Configure(EntityTypeBuilder<DocumentAttachment> builder)
	{
		builder.ToTable("DocumentAttachments");
		builder.HasKey((DocumentAttachment e) => e.Id);
		builder.Property((DocumentAttachment e) => e.FileName).HasMaxLength(500).IsRequired();
		builder.Property((DocumentAttachment e) => e.Description).HasMaxLength(1000);
		builder.Property((DocumentAttachment e) => e.FilePath).HasMaxLength(1000).IsRequired();
		builder.Property((DocumentAttachment e) => e.MimeType).HasMaxLength(200).IsRequired();
		builder.HasOne((DocumentAttachment e) => e.User).WithMany().HasForeignKey((DocumentAttachment e) => e.UserId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((DocumentAttachment e) => e.ReportRow).WithMany().HasForeignKey((DocumentAttachment e) => e.ReportRowId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((DocumentAttachment e) => e.Report).WithMany().HasForeignKey((DocumentAttachment e) => e.ReportId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((DocumentAttachment e) => e.UploadedByUser).WithMany().HasForeignKey((DocumentAttachment e) => e.UploadedBy)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
