using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
  public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
  {
    builder.ToTable("PasswordResetTokens");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.TokenHash).HasMaxLength(128).IsRequired();
    builder.HasIndex(e => e.TokenHash).IsUnique();
    builder.HasIndex(e => new { e.UserId, e.ExpiresAt });

    builder.HasOne(e => e.User)
      .WithMany()
      .HasForeignKey(e => e.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
