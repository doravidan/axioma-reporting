using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.FileProviders;

namespace AxiomaReporting.Tests.TestSupport;

internal static class AdminControllerFactory
{
  public static AdminController Create(AppDbContext db)
  {
    var passwordService = new PasswordService();
    var pdfService = new PdfReportService();
    var fakeEmail = new FakeEmailService();
    var branding = new StubBrandingService();
    var hostEnv = new StubWebHostEnvironment();
    var fakeBatch = new StubBatchReportImportService();

    var audit = new FakeAuditLogService();
    var controller = new AdminController(db, passwordService, fakeBatch, pdfService, fakeEmail, branding, audit, hostEnv)
    {
      TempData = new TempDataDictionary(new DefaultHttpContext(), new StubTempDataProvider())
    };
    return controller;
  }
}

internal sealed class StubBrandingService : IBrandingService
{
  public Task<string> GetLogoPathAsync(CancellationToken ct = default) =>
    Task.FromResult(IBrandingService.DefaultLogoPath);
  public Task SetLogoPathAsync(string publicPath, int? updatedByUserId, CancellationToken ct = default) =>
    Task.CompletedTask;
}

internal sealed class StubWebHostEnvironment : IWebHostEnvironment
{
  public string WebRootPath { get; set; } = Path.GetTempPath();
  public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
  public string ApplicationName { get; set; } = "Tests";
  public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
  public string ContentRootPath { get; set; } = Path.GetTempPath();
  public string EnvironmentName { get; set; } = "Testing";
}

internal sealed class FakeAuditLogService : IAuditLogService
{
  public Task LogAsync(string action, string entityType, string? entityId, object? before = null, object? after = null, string? notes = null, CancellationToken ct = default) =>
    Task.CompletedTask;
}

internal sealed class StubBatchReportImportService : IBatchReportImportService
{
  public Task<BatchImportResult> ImportAsync(
    Stream xlsxStream,
    int reportingMonthId,
    int uploaderUserId,
    CancellationToken ct = default,
    string? progressId = null,
    bool previewOnly = false) =>
    Task.FromResult(new BatchImportResult { IsPreview = previewOnly });
}

internal sealed class StubTempDataProvider : ITempDataProvider
{
  private readonly Dictionary<string, object> _store = new();
  public IDictionary<string, object> LoadTempData(HttpContext context) => _store;
  public void SaveTempData(HttpContext context, IDictionary<string, object> values) { }
}
