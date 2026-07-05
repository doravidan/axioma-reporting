using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
	public void Configure(EntityTypeBuilder<Report> builder)
	{
		builder.ToTable("Reports");
		builder.HasKey((Report e) => e.Id);
		builder.Property((Report e) => e.RejectionReason).HasMaxLength(1000);
		builder.Property((Report e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.Property((Report e) => e.RowVersion).IsRowVersion();
		builder.HasIndex((Report e) => new { e.UserId, e.ReportingMonthId }).IsUnique();
		builder.HasOne((Report e) => e.User).WithMany((User u) => u.Reports).HasForeignKey((Report e) => e.UserId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((Report e) => e.ReportingMonth).WithMany((ReportingMonth rm) => rm.Reports).HasForeignKey((Report e) => e.ReportingMonthId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((Report e) => e.Status).WithMany().HasForeignKey((Report e) => e.StatusId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((Report e) => e.ApprovedByUser).WithMany().HasForeignKey((Report e) => e.ApprovedBy)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((Report e) => e.RejectedByUser).WithMany().HasForeignKey((Report e) => e.RejectedBy)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
