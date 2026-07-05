using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class TwoFactorCodeConfiguration : IEntityTypeConfiguration<TwoFactorCode>
{
	public void Configure(EntityTypeBuilder<TwoFactorCode> builder)
	{
		builder.ToTable("TwoFactorCodes");
		builder.HasKey((TwoFactorCode e) => e.Id);
		builder.Property((TwoFactorCode e) => e.CodeHash).HasMaxLength(128).IsRequired();
		builder.HasIndex((TwoFactorCode e) => new { e.UserId, e.ExpiresAt });
		builder.HasOne((TwoFactorCode e) => e.User).WithMany().HasForeignKey((TwoFactorCode e) => e.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
