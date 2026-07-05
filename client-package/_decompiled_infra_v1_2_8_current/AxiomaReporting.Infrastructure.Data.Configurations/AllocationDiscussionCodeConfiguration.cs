using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationDiscussionCodeConfiguration : IEntityTypeConfiguration<AllocationDiscussionCode>
{
	public void Configure(EntityTypeBuilder<AllocationDiscussionCode> builder)
	{
		builder.ToTable("AllocationDiscussionCodes");
		builder.HasKey((AllocationDiscussionCode e) => new { e.AllocationId, e.DiscussionCodeId });
		builder.HasOne((AllocationDiscussionCode e) => e.Allocation).WithMany((Allocation a) => a.AllocationDiscussionCodes).HasForeignKey((AllocationDiscussionCode e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationDiscussionCode e) => e.DiscussionCode).WithMany().HasForeignKey((AllocationDiscussionCode e) => e.DiscussionCodeId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
