using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
{
	public void Configure(EntityTypeBuilder<UserStatus> builder)
	{
		builder.ToTable("UserStatuses");
		builder.HasKey((UserStatus e) => e.Id);
		builder.Property((UserStatus e) => e.Id).ValueGeneratedNever();
		builder.Property((UserStatus e) => e.Name).HasMaxLength(100).IsRequired();
		builder.Property((UserStatus e) => e.DescriptionHebrew).HasMaxLength(200);
	}
}
