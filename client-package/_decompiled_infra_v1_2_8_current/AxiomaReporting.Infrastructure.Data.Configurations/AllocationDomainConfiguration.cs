using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AllocationDomainConfiguration : IEntityTypeConfiguration<AllocationDomain>
{
	public void Configure(EntityTypeBuilder<AllocationDomain> builder)
	{
		builder.ToTable("AllocationDomains");
		builder.HasKey((AllocationDomain e) => new { e.AllocationId, e.DomainId });
		builder.HasOne((AllocationDomain e) => e.Allocation).WithMany((Allocation a) => a.AllocationDomains).HasForeignKey((AllocationDomain e) => e.AllocationId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((AllocationDomain e) => e.Domain).WithMany().HasForeignKey((AllocationDomain e) => e.DomainId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
