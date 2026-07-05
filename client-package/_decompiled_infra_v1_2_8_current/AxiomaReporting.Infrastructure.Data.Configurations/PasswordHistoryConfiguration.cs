using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class PasswordHistoryConfiguration : IEntityTypeConfiguration<PasswordHistory>
{
	public void Configure(EntityTypeBuilder<PasswordHistory> builder)
	{
		builder.ToTable("PasswordHistories");
		builder.HasKey((PasswordHistory e) => e.Id);
		builder.Property((PasswordHistory e) => e.PasswordHash).HasMaxLength(500).IsRequired();
		builder.HasOne((PasswordHistory e) => e.User).WithMany((User u) => u.PasswordHistories).HasForeignKey((PasswordHistory e) => e.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
