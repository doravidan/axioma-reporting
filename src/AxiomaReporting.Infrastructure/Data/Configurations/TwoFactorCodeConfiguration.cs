using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class TwoFactorCodeConfiguration : IEntityTypeConfiguration<TwoFactorCode>
{
  public void Configure(EntityTypeBuilder<TwoFactorCode> builder)
  {
    builder.ToTable("TwoFactorCodes");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.CodeHash).HasMaxLength(128).IsRequired();
    builder.HasIndex(e => new { e.UserId, e.ExpiresAt });

    builder.HasOne(e => e.User)
      .WithMany()
      .HasForeignKey(e => e.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
