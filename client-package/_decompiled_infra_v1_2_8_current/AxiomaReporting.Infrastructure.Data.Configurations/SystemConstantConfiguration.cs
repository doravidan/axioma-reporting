using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class SystemConstantConfiguration : IEntityTypeConfiguration<SystemConstant>
{
	public void Configure(EntityTypeBuilder<SystemConstant> builder)
	{
		builder.ToTable("SystemConstants");
		builder.HasKey((SystemConstant e) => e.Id);
		builder.Property((SystemConstant e) => e.Key).HasMaxLength(200).IsRequired();
		builder.Property((SystemConstant e) => e.Value).HasMaxLength(1000).IsRequired();
		builder.Property((SystemConstant e) => e.Description).HasMaxLength(500);
		builder.Property((SystemConstant e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.HasIndex((SystemConstant e) => e.Key).IsUnique();
	}
}
