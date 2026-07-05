using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class AuthorityConfiguration : IEntityTypeConfiguration<Authority>
{
	public void Configure(EntityTypeBuilder<Authority> builder)
	{
		builder.ToTable("Authorities");
		builder.HasKey((Authority e) => e.Id);
		builder.Property((Authority e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((Authority e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
