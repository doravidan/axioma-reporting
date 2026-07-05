using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class InspectorAssignmentConfiguration : IEntityTypeConfiguration<InspectorAssignment>
{
	public void Configure(EntityTypeBuilder<InspectorAssignment> builder)
	{
		builder.ToTable("InspectorAssignments");
		builder.HasKey((InspectorAssignment e) => e.Id);
		builder.HasOne((InspectorAssignment e) => e.Inspector).WithMany().HasForeignKey((InspectorAssignment e) => e.InspectorUserId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasOne((InspectorAssignment e) => e.Program).WithMany().HasForeignKey((InspectorAssignment e) => e.ProgramId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((InspectorAssignment e) => e.District).WithMany().HasForeignKey((InspectorAssignment e) => e.DistrictId)
			.OnDelete(DeleteBehavior.NoAction);
		builder.HasOne((InspectorAssignment e) => e.Sector).WithMany().HasForeignKey((InspectorAssignment e) => e.SectorId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
