using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
	public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
	{
		builder.ToTable("PasswordResetTokens");
		builder.HasKey((PasswordResetToken e) => e.Id);
		builder.Property((PasswordResetToken e) => e.TokenHash).HasMaxLength(128).IsRequired();
		builder.HasIndex((PasswordResetToken e) => e.TokenHash).IsUnique();
		builder.HasIndex((PasswordResetToken e) => new { e.UserId, e.ExpiresAt });
		builder.HasOne((PasswordResetToken e) => e.User).WithMany().HasForeignKey((PasswordResetToken e) => e.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
