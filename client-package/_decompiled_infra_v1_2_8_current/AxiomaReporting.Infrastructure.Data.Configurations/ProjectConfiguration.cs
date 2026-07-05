using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
	public void Configure(EntityTypeBuilder<Project> builder)
	{
		builder.ToTable("Projects");
		builder.HasKey((Project e) => e.Id);
		builder.Property((Project e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((Project e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
