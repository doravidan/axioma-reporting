using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class ProgramConfiguration : IEntityTypeConfiguration<Program>
{
	public void Configure(EntityTypeBuilder<Program> builder)
	{
		builder.ToTable("Programs");
		builder.HasKey((Program e) => e.Id);
		builder.Property((Program e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((Program e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
