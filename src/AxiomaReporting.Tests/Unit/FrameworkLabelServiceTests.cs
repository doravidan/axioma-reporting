using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class FrameworkLabelServiceTests
{
  [Fact]
  public async Task BuildLabels_UsesBusinessFieldsAndPreservesLeadingZeroes()
  {
    await using var db = CreateDb();
    db.Localities.Add(new Locality { Id = 1, Description = "רחלים", IsActive = true });
    db.Institutions.Add(new Institution { Id = 1, InstitutionSymbol = "872903", Name = "הילה ישיבה פרי הארץ", LocalityId = 1 });
    db.Frameworks.Add(new Framework { Id = 1, InstitutionSymbol = "0872903", Description = "הילה ישיבה פרי הארץ", IsActive = true });
    await db.SaveChangesAsync();

    var labels = await FrameworkLabelService.BuildLabelsAsync(db, new[] { 1 });

    labels[1].Should().Be("רחלים — 0872903 — הילה ישיבה פרי הארץ");
  }

  [Fact]
  public async Task BuildLabels_OmitsMissingPartsWithoutExtraSeparators()
  {
    await using var db = CreateDb();
    db.Frameworks.AddRange(
      new Framework { Id = 1, InstitutionSymbol = "00123", Description = "מסגרת עברית", IsActive = true },
      new Framework { Id = 2, InstitutionSymbol = "QCAT-X", Description = "ללא סמל", IsActive = true });
    await db.SaveChangesAsync();

    var labels = await FrameworkLabelService.BuildLabelsAsync(db, new[] { 1, 2 });

    labels[1].Should().Be("00123 — מסגרת עברית");
    labels[2].Should().Be("ללא סמל");
    labels.Values.Should().OnlyContain(value => !value.Contains("null") && !value.Contains("—  —"));
  }

  private static AppDbContext CreateDb() => new(new DbContextOptionsBuilder<AppDbContext>()
    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
}
