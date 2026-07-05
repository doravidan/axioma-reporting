using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
	public void Configure(EntityTypeBuilder<Subject> builder)
	{
		builder.ToTable("Subjects");
		builder.HasKey((Subject e) => e.Id);
		builder.Property((Subject e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((Subject e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
