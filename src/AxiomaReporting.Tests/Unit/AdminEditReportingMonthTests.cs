using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Tests.TestSupport;
using AxiomaReporting.Web.Controllers;
using AxiomaReporting.Web.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Verifies client-feedback fix #3: only Admin (role 1) and PM (role 2) may
/// change <c>LastReportingDate</c> / <c>AllowFutureReporting</c> on an active
/// reporting month. Coordinator (role 3) edits silently drop those fields.
/// </summary>
public class AdminEditReportingMonthTests : IDisposable
{
  private readonly AppDbContext _db;

  public AdminEditReportingMonthTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);

    _db.ReportingMonths.Add(new ReportingMonth
    {
      Id = 100,
      Description = "ינואר 2026",
      Month = 1,
      Year = 2026,
      LastReportingDate = new DateTime(2026, 1, 31),
      AllowFutureReporting = false,
      IsActive = true,
      CreatedAt = DateTime.UtcNow.AddDays(-10)
    });
    _db.SaveChanges();
  }

  public void Dispose() => _db.Dispose();

  private AdminController CreateControllerForRole(string roleId)
  {
    var controller = new AdminController(
      _db,
      passwordService: null!,
      batchImportService: null!,
      pdfReportService: null!,
      emailService: null!,
      brandingService: null!,
      auditLog: new FakeAuditLogService(),
      hostEnvironment: null!,
      antiforgery: new StubAntiforgery(),
      htmlSanitizer: new AxiomaReporting.Infrastructure.Services.HtmlSanitizerService())
    {
      TempData = new TempDataDictionary(new DefaultHttpContext(), new NoOpTempDataProviderAdminEditTests())
    };

    var identity = new ClaimsIdentity(new[]
    {
      new Claim(ClaimTypes.NameIdentifier, "42"),
      new Claim(ClaimTypes.Role, roleId)
    }, "Test");

    var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) };
    controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
    return controller;
  }

  [Fact]
  public async Task EditReportingMonth_Get_AsCoordinator_LocksNonAdminFields()
  {
    var sut = CreateControllerForRole("3"); // Coordinator

    var result = await sut.EditReportingMonth(100);

    var view = result.Should().BeOfType<ViewResult>().Subject;
    var vm = view.Model.Should().BeOfType<ReportingMonthEditViewModel>().Subject;
    vm.LockNonAdminFields.Should().BeTrue();
    vm.IsActive.Should().BeTrue();
  }

  [Fact]
  public async Task EditReportingMonth_Get_AsAdmin_DoesNotLockFields()
  {
    var sut = CreateControllerForRole("1"); // Admin

    var result = await sut.EditReportingMonth(100);

    var view = result.Should().BeOfType<ViewResult>().Subject;
    var vm = view.Model.Should().BeOfType<ReportingMonthEditViewModel>().Subject;
    vm.LockNonAdminFields.Should().BeFalse();
  }

  [Fact]
  public async Task EditReportingMonth_Post_AsCoordinator_DoesNotPersistLastReportingDateChange()
  {
    var sut = CreateControllerForRole("3");
    var originalDate = new DateTime(2026, 1, 31);
    var attemptedDate = new DateTime(2026, 12, 31);

    var input = new ReportingMonthEditViewModel
    {
      Id = 100,
      Description = "ינואר 2026",
      Month = 1,
      Year = 2026,
      LastReportingDate = attemptedDate,
      AllowFutureReporting = true,
      IsActive = true
    };

    var result = await sut.EditReportingMonth(input);

    result.Should().BeOfType<RedirectToActionResult>();

    // Reload from DB and confirm the locked fields were ignored.
    var saved = await _db.ReportingMonths.AsNoTracking().FirstAsync(m => m.Id == 100);
    saved.LastReportingDate.Date.Should().Be(originalDate.Date);
    saved.AllowFutureReporting.Should().BeFalse();
  }

  [Fact]
  public async Task EditReportingMonth_Post_AsAdmin_PersistsLastReportingDateChange()
  {
    var sut = CreateControllerForRole("1");
    var attemptedDate = new DateTime(2026, 12, 31);

    var input = new ReportingMonthEditViewModel
    {
      Id = 100,
      Description = "ינואר 2026 — מעודכן",
      Month = 1,
      Year = 2026,
      LastReportingDate = attemptedDate,
      AllowFutureReporting = true,
      IsActive = true
    };

    var result = await sut.EditReportingMonth(input);

    result.Should().BeOfType<RedirectToActionResult>();

    var saved = await _db.ReportingMonths.AsNoTracking().FirstAsync(m => m.Id == 100);
    saved.LastReportingDate.Date.Should().Be(attemptedDate.Date);
    saved.AllowFutureReporting.Should().BeTrue();
    saved.Description.Should().Be("ינואר 2026 — מעודכן");
  }
}

internal sealed class NoOpTempDataProviderAdminEditTests : ITempDataProvider
{
  public IDictionary<string, object> LoadTempData(HttpContext context) => new Dictionary<string, object>();
  public void SaveTempData(HttpContext context, IDictionary<string, object> values) { }
}
