using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AxiomaReporting.Infrastructure.Data.Configurations;

public class DiscussionCodeConfiguration : IEntityTypeConfiguration<DiscussionCode>
{
	public void Configure(EntityTypeBuilder<DiscussionCode> builder)
	{
		builder.ToTable("DiscussionCodes");
		builder.HasKey((DiscussionCode e) => e.Id);
		builder.Property((DiscussionCode e) => e.Description).HasMaxLength(500).IsRequired();
		builder.Property((DiscussionCode e) => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
	}
}
