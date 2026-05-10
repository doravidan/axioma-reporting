using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class PasswordHistoryConfiguration : IEntityTypeConfiguration<PasswordHistory>
{
  public void Configure(EntityTypeBuilder<PasswordHistory> builder)
  {
    builder.ToTable("PasswordHistories");
    builder.HasKey(e => e.Id);
    builder.Property(e => e.PasswordHash).HasMaxLength(500).IsRequired();

    builder.HasOne(e => e.User)
      .WithMany(u => u.PasswordHistories)
      .HasForeignKey(e => e.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
