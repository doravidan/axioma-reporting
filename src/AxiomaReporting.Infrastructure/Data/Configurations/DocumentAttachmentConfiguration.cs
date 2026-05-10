using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class DocumentAttachmentConfiguration : IEntityTypeConfiguration<DocumentAttachment>
{
  public void Configure(EntityTypeBuilder<DocumentAttachment> builder)
  {
    builder.ToTable("DocumentAttachments");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.FileName).HasMaxLength(500).IsRequired();
    builder.Property(e => e.FilePath).HasMaxLength(1000).IsRequired();
    builder.Property(e => e.MimeType).HasMaxLength(200).IsRequired();

    builder.HasOne(e => e.User)
      .WithMany()
      .HasForeignKey(e => e.UserId)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(e => e.ReportRow)
      .WithMany()
      .HasForeignKey(e => e.ReportRowId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.UploadedByUser)
      .WithMany()
      .HasForeignKey(e => e.UploadedBy)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
