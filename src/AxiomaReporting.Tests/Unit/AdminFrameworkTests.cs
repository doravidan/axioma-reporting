using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class AdminFrameworkTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly AdminController _sut;

  public AdminFrameworkTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _sut = new AdminController(
      _db,
      passwordService: null!,
      batchImportService: null!,
      pdfReportService: null!,
      emailService: null!,
      brandingService: null!,
      auditLog: new TestSupport.FakeAuditLogService(),
      hostEnvironment: null!)
    {
      TempData = new TempDataDictionary(new DefaultHttpContext(), new NoOpTempDataProvider()),
      ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
    };

    _db.EducationalStages.Add(new EducationalStage { Id = 1, Description = "יסודי", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.EducationalStages.Add(new EducationalStage { Id = 2, Description = "תיכון", IsActive = true, CreatedAt = DateTime.UtcNow });
    _db.SaveChanges();
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task CreateFramework_PositivePath_AddsRow()
  {
    var result = await _sut.CreateFramework("גן נוף", "12345", 1);

    result.Should().BeOfType<RedirectToActionResult>();
    var framework = await _db.Frameworks.SingleAsync();
    framework.Description.Should().Be("גן נוף");
    framework.InstitutionSymbol.Should().Be("12345");
    framework.EducationalStageId.Should().Be(1);
    _sut.TempData["Success"].Should().Be("מסגרת נוצרה");
  }

  [Fact]
  public async Task CreateFramework_DuplicateSymbolAndStage_IsRejected()
  {
    _db.Frameworks.Add(new Framework
    {
      Description = "מסגרת א",
      InstitutionSymbol = "55555",
      EducationalStageId = 2,
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    var result = await _sut.CreateFramework("מסגרת ב", "55555", 2);

    result.Should().BeOfType<RedirectToActionResult>();
    (await _db.Frameworks.CountAsync()).Should().Be(1);
    _sut.TempData["Error"].Should().Be("סמל מוסד זה כבר קיים עבור שלב חינוך זה");
  }

  [Fact]
  public async Task CreateFramework_SameSymbolDifferentStage_IsAllowed()
  {
    _db.Frameworks.Add(new Framework
    {
      Description = "א",
      InstitutionSymbol = "777",
      EducationalStageId = 1,
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    await _sut.CreateFramework("ב", "777", 2);

    (await _db.Frameworks.CountAsync()).Should().Be(2);
  }

  private sealed class NoOpTempDataProvider : ITempDataProvider
  {
    public IDictionary<string, object> LoadTempData(HttpContext context) => new Dictionary<string, object>();
    public void SaveTempData(HttpContext context, IDictionary<string, object> values) { }
  }
}
