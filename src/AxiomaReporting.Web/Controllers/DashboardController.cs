using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Authorization;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace AxiomaReporting.Web.Controllers;

[Authorize(Policy = PolicyNames.CanViewDashboard)]
public class DashboardController : Controller
{
  private readonly IDashboardFilterService _filterService;
  private readonly ICurrentUserService _currentUser;
  private readonly IReportStatusService _reportStatusService;
  private readonly AppDbContext _db;
  private readonly IMemoryCache? _cache;

  // NOTE: `cache` is intentionally optional (nullable with a default value). Program.cs does not
  // currently call `builder.Services.AddMemoryCache()`, so IMemoryCache is not registered in DI.
  // Keeping this parameter optional means the controller still activates correctly (falling back
  // to uncached lookups) if that registration is missing, and starts caching automatically the
  // moment it's added — see PopulateFilterDataAsync / GetCachedLookupAsync below.
  public DashboardController(
    IDashboardFilterService filterService,
    ICurrentUserService currentUser,
    IReportStatusService reportStatusService,
    AppDbContext db,
    IMemoryCache? cache = null)
  {
    _filterService = filterService;
    _currentUser = currentUser;
    _reportStatusService = reportStatusService;
    _db = db;
    _cache = cache;
  }

  [HttpGet]
  public async Task<IActionResult> Index(DashboardFilter? filter = null)
  {
    filter ??= new DashboardFilter();
    await PopulateFilterDataAsync(filter);

    var showData = true;
    ViewBag.ShowData = showData;

    var rows = new List<DashboardReportDetailRow>();
    var total = 0;
    if (showData)
      (rows, total) = await _filterService.GetReportRowsAsync(filter, _currentUser.UserId, _currentUser.UserRole);

    ViewBag.Rows = rows;
    ViewBag.TotalCount = total;
    return View();
  }

  [HttpGet]
  public async Task<IActionResult> FilterOptions(string? selected = null)
  {
    DashboardFilter filter;
    try
    {
      filter = string.IsNullOrWhiteSpace(selected)
        ? new DashboardFilter()
        : JsonSerializer.Deserialize<DashboardFilter>(selected, new JsonSerializerOptions
          {
            PropertyNameCaseInsensitive = true
          }) ?? new DashboardFilter();
    }
    catch (JsonException)
    {
      filter = new DashboardFilter();
    }

    var options = await _filterService.GetCompatibleOptionsAsync(
      filter, _currentUser.UserId, _currentUser.UserRole);

    return Json(options);
  }

  /// <summary>
  /// JSON payload for the documents modal on the dashboard: all attachments related
  /// to a report (report-level, row-level for the given allocation, or general
  /// employee-level documents).
  /// </summary>
  [HttpGet]
  public async Task<IActionResult> ReportDocuments(int reportId, int allocationId)
  {
    if (!await _filterService.CanAccessReportAsync(reportId, _currentUser.UserId, _currentUser.UserRole))
      return NotFound();

    var report = await _db.Reports
      .AsNoTracking()
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    var allocation = await _db.Allocations
      .AsNoTracking()
      .Include(a => a.Project)
      .FirstOrDefaultAsync(a => a.Id == allocationId);

    if (report == null || allocation == null || allocation.UserId != report.UserId)
      return NotFound();

    var reportRowIds = await _db.ReportRows
      .AsNoTracking()
      .Where(rr => rr.ReportId == reportId && rr.AllocationId == allocationId)
      .Select(rr => rr.Id)
      .ToListAsync();

    var attachments = await _db.DocumentAttachments
      .AsNoTracking()
      .Where(a => a.ReportId == reportId
        || (a.ReportRowId.HasValue && reportRowIds.Contains(a.ReportRowId.Value))
        || (a.UserId == report.UserId && a.ReportId == null && a.ReportRowId == null))
      .OrderByDescending(a => a.UploadedAt)
      .ToListAsync();

    return Json(new
    {
      employeeName = $"{report.User?.FirstName} {report.User?.LastName}".Trim(),
      employeeId = report.User?.IdNumber ?? report.User?.EmployeeCode ?? "",
      projectName = allocation.Project?.Description ?? "",
      reportMonth = report.ReportingMonth?.Description
        ?? (report.ReportingMonth != null ? $"{report.ReportingMonth.Month}/{report.ReportingMonth.Year}" : ""),
      documents = attachments.Select(a => new
      {
        id = a.Id,
        fileName = a.FileName,
        description = a.Description,
        uploadedAt = a.UploadedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
        fileSize = FormatFileSize(a.FileSize),
        viewUrl = Url.Action(nameof(DocumentAttachment), "Dashboard", new { attachmentId = a.Id }),
        downloadUrl = Url.Action(nameof(DocumentAttachment), "Dashboard", new { attachmentId = a.Id, download = true })
      })
    });
  }

  [HttpGet]
  public async Task<IActionResult> DocumentAttachment(int attachmentId, bool download = false)
  {
    var attachment = await _db.DocumentAttachments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == attachmentId);
    if (attachment == null) return NotFound();

    // Per-record authorization (IDOR guard): scope the attachment to the report/employee
    // it belongs to and reuse the same scoping rules other dashboard actions use, rather
    // than relying solely on the controller-level [Authorize(CanViewDashboard)] which is
    // shared by ALL inspector/PM/coordinator roles regardless of their assignment scope.
    if (!await CanAccessAttachmentAsync(attachment))
      return NotFound();

    // Path-traversal guard: the resolved file must stay under wwwroot.
    var webRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
    var relativePath = attachment.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
    var fullPath = Path.GetFullPath(Path.Combine(webRoot, relativePath));
    if (!fullPath.StartsWith(webRoot, StringComparison.OrdinalIgnoreCase) || !System.IO.File.Exists(fullPath))
      return NotFound();

    var contentType = string.IsNullOrWhiteSpace(attachment.MimeType)
      ? "application/octet-stream"
      : attachment.MimeType;

    return PhysicalFile(fullPath, contentType, download ? attachment.FileName : null);
  }

  /// <summary>
  /// Resolves which Report (directly, or via its ReportRow) or employee a
  /// <see cref="DocumentAttachment"/> belongs to, then defers to the shared
  /// <see cref="IDashboardFilterService"/> scoping checks used elsewhere in this controller.
  /// </summary>
  private async Task<bool> CanAccessAttachmentAsync(DocumentAttachment attachment)
  {
    var reportId = attachment.ReportId;
    if (!reportId.HasValue && attachment.ReportRowId.HasValue)
    {
      reportId = await _db.ReportRows
        .AsNoTracking()
        .Where(rr => rr.Id == attachment.ReportRowId.Value)
        .Select(rr => (int?)rr.ReportId)
        .FirstOrDefaultAsync();
    }

    if (reportId.HasValue)
      return await _filterService.CanAccessReportAsync(reportId.Value, _currentUser.UserId, _currentUser.UserRole);

    if (attachment.UserId.HasValue)
      return await _filterService.CanAccessEmployeeAsync(attachment.UserId.Value, _currentUser.UserId, _currentUser.UserRole);

    // No owning report or employee to scope against — deny by default.
    return false;
  }

  private static string FormatFileSize(long bytes)
  {
    if (bytes >= 1048576) return ((decimal)bytes / 1048576m).ToString("0.#") + " MB";
    if (bytes >= 1024) return ((decimal)bytes / 1024m).ToString("0.#") + " KB";
    return bytes + " B";
  }

  /// <summary>
  /// Business rule: "Inspector-View can ONLY export approved reports" (also applies to
  /// Inspector-Approval for exports). Clamping StatusId to 4 (Approved) here — and calling
  /// this from every EXPORT entry point (ExportExcel, SummaryExportExcel) — prevents them
  /// from drifting out of sync and re-introducing an unapproved-export bypass for inspector
  /// roles. Deliberately NOT applied to the interactive Summary action: InspectorApproval
  /// (see PolicyNames.CanApproveReports / ViewBag.CanApprove below) must still see and act
  /// on PendingApproval (StatusId 3) reports there in order to approve/reject them — clamping
  /// that screen to approved-only would silently break their entire approval workflow.
  /// </summary>
  private void ApplyInspectorApprovedOnlyClamp(DashboardFilter filter)
  {
    if (_currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval)
      filter.StatusId = 4;
  }

  [HttpGet]
  public async Task<IActionResult> ExportExcel(DashboardFilter filter)
  {
    ApplyInspectorApprovedOnlyClamp(filter);

    filter.Page = 1;
    filter.PageSize = 10000;

    var (rows, _) = await _filterService.GetReportRowsAsync(
      filter, _currentUser.UserId, _currentUser.UserRole);

    using var wb = new XLWorkbook();
    var ws = wb.Worksheets.Add("דיווחים");
    ws.RightToLeft = true;

    // "סוג דיווח" (report type) moved up to column 3, next to the employee identity.
    var headers = new[]
    {
      "מס\"ד", "ת.ז", "סוג דיווח", "קוד עובד", "שם מדווח", "חודש דיווח", "פרויקט", "מחוז", "ישוב", "מסגרת חינוכית",
      "תאריך מפגש", "משך מפגש", "תוכנית חינוכית", "תחום", "נושא 1", "נושא 2", "קיום דיון", "כיתה", "שכבה", "מסקנות כיתה",
      "מסקנות מסגרת", "מסקנות ישוב/מחוז/ארצי", "מסמכים", "הערות"
    };
    for (var i = 0; i < headers.Length; i++)
      ws.Cell(1, i + 1).Value = headers[i];

    var row = 2;
    foreach (var r in rows)
    {
      ws.Cell(row, 1).Value = r.SequenceNumber;
      ws.Cell(row, 2).Value = r.IdNumber;
      ws.Cell(row, 3).Value = r.ReportTypeName;
      ws.Cell(row, 4).Value = r.EmployeeCode;
      ws.Cell(row, 5).Value = r.FullName;
      ws.Cell(row, 6).Value = r.MonthDescription;
      ws.Cell(row, 7).Value = r.ProjectName;
      ws.Cell(row, 8).Value = r.DistrictName;
      ws.Cell(row, 9).Value = r.LocalityName;
      ws.Cell(row, 10).Value = r.FrameworkName;
      ws.Cell(row, 11).Value = r.MeetingDate.ToString("dd/MM/yyyy");
      ws.Cell(row, 12).Value = (double)r.MeetingDuration;
      ws.Cell(row, 13).Value = r.EducationalProgramName;
      ws.Cell(row, 14).Value = r.DomainName;
      ws.Cell(row, 15).Value = r.Subject1Name;
      ws.Cell(row, 16).Value = r.Subject2Name;
      ws.Cell(row, 17).Value = r.DiscussionCodeName;
      ws.Cell(row, 18).Value = r.ClassName;
      ws.Cell(row, 19).Value = r.GradeLevelName;
      ws.Cell(row, 20).Value = r.ConclusionClassName;
      ws.Cell(row, 21).Value = r.ConclusionFrameworkName;
      ws.Cell(row, 22).Value = r.ConclusionLocationName;
      ws.Cell(row, 23).Value = r.HasAttachments ? "כן" : "לא";
      ws.Cell(row, 24).Value = r.Notes;
      row++;
    }

    ws.Columns().AdjustToContents();

    using var ms = new MemoryStream();
    wb.SaveAs(ms);
    return File(
      ms.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      $"reports_{DateTime.Now:yyyyMMdd}.xlsx");
  }

  [HttpGet]
  public async Task<IActionResult> SummaryExportExcel(DashboardFilter filter)
  {
    ApplyInspectorApprovedOnlyClamp(filter);

    filter.Page = 1;
    filter.PageSize = 10000;

    var (rows, _) = await _filterService.GetReportsAsync(
      filter, _currentUser.UserId, _currentUser.UserRole);

    using var wb = new XLWorkbook();
    var ws = wb.Worksheets.Add("סיכום");
    ws.RightToLeft = true;

    var headers = new[]
    {
      "קוד עובד", "ת.ז", "שם עובד", "פרויקט", "חודש", "סטטוס", "מס' שורות", "סך משך תפוקה",
      "יתרת שורות", "מסמכים", "תאריך הגשה"
    };
    for (var i = 0; i < headers.Length; i++)
      ws.Cell(1, i + 1).Value = headers[i];

    var row = 2;
    foreach (var r in rows)
    {
      ws.Cell(row, 1).Value = r.EmployeeCode;
      ws.Cell(row, 2).Value = r.IdNumber;
      ws.Cell(row, 3).Value = r.FullName;
      ws.Cell(row, 4).Value = r.ProjectName;
      ws.Cell(row, 5).Value = r.MonthDescription;
      ws.Cell(row, 6).Value = r.StatusName;
      ws.Cell(row, 7).Value = r.RowCount;
      ws.Cell(row, 8).Value = (double)r.TotalDuration;
      ws.Cell(row, 9).Value = r.MonthlyRowAllocation.HasValue ? r.RemainingRows : string.Empty;
      ws.Cell(row, 10).Value = r.DocumentCount;
      ws.Cell(row, 11).Value = r.SubmittedAt?.ToString("dd/MM/yyyy") ?? string.Empty;
      row++;
    }

    ws.Columns().AdjustToContents();

    using var ms = new MemoryStream();
    wb.SaveAs(ms);
    return File(
      ms.ToArray(),
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      $"summary_{DateTime.Now:yyyyMMdd}.xlsx");
  }

  [HttpGet]
  public async Task<IActionResult> Summary(DashboardFilter? filter = null)
  {
    filter ??= new DashboardFilter();
    await PopulateFilterDataAsync(filter);

    var (rows, total) = await _filterService.GetReportsAsync(
      filter, _currentUser.UserId, _currentUser.UserRole);

    ViewBag.Rows = rows;
    ViewBag.TotalCount = total;
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  [Authorize(Policy = PolicyNames.CanApproveReports)]
  public async Task<IActionResult> BulkApprove(List<int> reportIds)
  {
    var approved = 0;
    foreach (var id in reportIds.Distinct())
    {
      if (!await _filterService.CanAccessReportAsync(id, _currentUser.UserId, _currentUser.UserRole))
        continue;

      if (await _reportStatusService.ApproveReportAsync(id, _currentUser.UserId))
        approved++;
    }

    TempData["Success"] = $"{approved} דיווחים אושרו בהצלחה";
    return RedirectToAction(nameof(Summary));
  }

  private async Task PopulateFilterDataAsync(DashboardFilter filter)
  {
    ViewBag.Filter = filter;
    ViewBag.Districts = await _filterService.GetFilteredDistrictsAsync(
      _currentUser.UserId, _currentUser.UserRole);
    ViewBag.Sectors = await _filterService.GetFilteredSectorsAsync(
      _currentUser.UserId, _currentUser.UserRole, filter.DistrictId);
    ViewBag.Programs = await _filterService.GetFilteredProgramsAsync(
      _currentUser.UserId, _currentUser.UserRole, filter.DistrictId);
    ViewBag.IsInspector = _currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval;
    ViewBag.CanEditDashboardRows = _currentUser.UserRole == UserRoleEnum.SystemAdmin;
    ViewBag.CanApprove = _currentUser.UserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager
      or UserRoleEnum.ProjectCoordinator or UserRoleEnum.InspectorApproval;

    using var scope = HttpContext.RequestServices.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AxiomaReporting.Infrastructure.Data.AppDbContext>();
    ViewBag.ReportingMonths = await db.ReportingMonths
      .AsNoTracking()
      .OrderByDescending(m => m.Year).ThenByDescending(m => m.Month)
      .ToListAsync();

    // These lookup tables (~1,340 localities, ~1,300 frameworks, etc.) change rarely but were
    // previously reloaded in full on every single dashboard/summary render. Cache them with a
    // short TTL — see LookupCacheDuration / GetCachedLookupAsync.
    ViewBag.Localities = await GetCachedLookupAsync(LocalitiesCacheKey,
      () => db.Localities.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
    ViewBag.Frameworks = await GetCachedLookupAsync(FrameworksCacheKey,
      () => db.Frameworks.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
    ViewBag.EducationalPrograms = await GetCachedLookupAsync(EducationalProgramsCacheKey,
      () => db.EducationalPrograms.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
    ViewBag.Domains = await GetCachedLookupAsync(DomainsCacheKey,
      () => db.Domains.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
    ViewBag.Subjects = await GetCachedLookupAsync(SubjectsCacheKey,
      () => db.Subjects.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
    ViewBag.DiscussionCodes = await GetCachedLookupAsync(DiscussionCodesCacheKey,
      () => db.DiscussionCodes.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
    ViewBag.Classes = await GetCachedLookupAsync(ClassesCacheKey,
      () => db.Classes.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
    ViewBag.GradeLevels = await GetCachedLookupAsync(GradeLevelsCacheKey,
      () => db.GradeLevels.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
    ViewBag.ConclusionLocations = await GetCachedLookupAsync(ConclusionLocationsCacheKey,
      () => db.LocalityDistrictNationals.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
    ViewBag.ReportTypes = await GetCachedLookupAsync(ReportTypesCacheKey,
      () => db.ReportTypes.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync());
  }

  private static readonly TimeSpan LookupCacheDuration = TimeSpan.FromMinutes(15);
  private const string LocalitiesCacheKey = "dash:localities";
  private const string FrameworksCacheKey = "dash:frameworks";
  private const string EducationalProgramsCacheKey = "dash:educationalPrograms";
  private const string DomainsCacheKey = "dash:domains";
  private const string SubjectsCacheKey = "dash:subjects";
  private const string DiscussionCodesCacheKey = "dash:discussionCodes";
  private const string ClassesCacheKey = "dash:classes";
  private const string GradeLevelsCacheKey = "dash:gradeLevels";
  private const string ConclusionLocationsCacheKey = "dash:conclusionLocations";
  private const string ReportTypesCacheKey = "dash:reportTypes";

  /// <summary>
  /// TTL-only cache (no CRUD invalidation — acceptable for rarely-changing lookup tables).
  /// Falls back to querying directly, uncached, when IMemoryCache is not registered in DI
  /// (see the constructor note on <see cref="_cache"/>).
  /// </summary>
  private async Task<List<T>> GetCachedLookupAsync<T>(string cacheKey, Func<Task<List<T>>> loadAsync)
  {
    if (_cache == null)
      return await loadAsync();

    if (_cache.TryGetValue(cacheKey, out List<T>? cached) && cached != null)
      return cached;

    var value = await loadAsync();
    _cache.Set(cacheKey, value, LookupCacheDuration);
    return value;
  }
}
