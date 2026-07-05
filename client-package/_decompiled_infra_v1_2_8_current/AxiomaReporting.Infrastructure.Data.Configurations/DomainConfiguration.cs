using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class DomainConfiguration : IEntityTypeConfiguration<Domain>
{
	public void Configure(EntityTypeBuilder<Domain> builder)
	{
		builder.ToTable("Domains");
		builder.HasKey((Domain e) => e.Id);
		builder.Property((Domain e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((Domain e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
