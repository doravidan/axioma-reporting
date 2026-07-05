using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
{
	public void Configure(EntityTypeBuilder<EmployeeRole> builder)
	{
		builder.ToTable("EmployeeRoles");
		builder.HasKey((EmployeeRole e) => e.Id);
		builder.Property((EmployeeRole e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((EmployeeRole e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
