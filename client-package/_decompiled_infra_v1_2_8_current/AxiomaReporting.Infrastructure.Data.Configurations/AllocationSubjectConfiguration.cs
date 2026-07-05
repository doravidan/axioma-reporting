using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationSubjectConfiguration : IEntityTypeConfiguration<AllocationSubject>
{
	public void Configure(EntityTypeBuilder<AllocationSubject> builder)
	{
		builder.ToTable("AllocationSubjects");
		builder.HasKey((AllocationSubject e) => new { e.AllocationId, e.SubjectId });
		builder.HasOne((AllocationSubject e) => e.Allocation).WithMany((Allocation a) => a.AllocationSubjects).HasForeignKey((AllocationSubject e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationSubject e) => e.Subject).WithMany().HasForeignKey((AllocationSubject e) => e.SubjectId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
