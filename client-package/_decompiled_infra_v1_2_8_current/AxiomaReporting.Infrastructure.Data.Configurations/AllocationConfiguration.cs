using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationConfiguration : IEntityTypeConfiguration<Allocation>
{
	public void Configure(EntityTypeBuilder<Allocation> builder)
	{
		builder.ToTable("Allocations");
		builder.HasKey((Allocation e) => e.Id);
		builder.Property((Allocation e) => e.AnnualEmploymentScope).HasPrecision(18, 4);
		builder.Property((Allocation e) => e.MonthlyEmploymentScope).HasPrecision(18, 4);
		builder.Property((Allocation e) => e.DailyEmploymentScope).HasPrecision(18, 4);
		builder.Property((Allocation e) => e.OutputDuration).HasMaxLength(500);
		builder.Property((Allocation e) => e.Notes).HasMaxLength(1000);
		builder.Property((Allocation e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.Property((Allocation e) => e.RowVersion).IsRowVersion();
		builder.HasIndex((Allocation e) => new { e.UserId, e.ProjectId });
		builder.HasOne((Allocation e) => e.User).WithMany((User u) => u.Allocations).HasForeignKey((Allocation e) => e.UserId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((Allocation e) => e.Project).WithMany().HasForeignKey((Allocation e) => e.ProjectId)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne((Allocation e) => e.ReportType).WithMany().HasForeignKey((Allocation e) => e.ReportTypeId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
