using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Authorization;
using AxiomaReporting.Web.Security;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AxiomaReporting.Web.Controllers;

[Authorize(Policy = PolicyNames.CanViewDashboard)]
public class DashboardController : Controller
{
  private readonly IDashboardFilterService _filterService;
  private readonly ICurrentUserService _currentUser;
  private readonly IBulkReportActionService _bulkReportActions;
  private readonly AxiomaReporting.Infrastructure.Data.AppDbContext _db;

  public DashboardController(
    IDashboardFilterService filterService,
    ICurrentUserService currentUser,
    IBulkReportActionService bulkReportActions,
    AxiomaReporting.Infrastructure.Data.AppDbContext db)
  {
    _filterService = filterService;
    _currentUser = currentUser;
    _bulkReportActions = bulkReportActions;
    _db = db;
  }

  [HttpGet]
  public async Task<IActionResult> Index(DashboardFilter? filter = null, int show = 0)
  {
    filter ??= new DashboardFilter();
    await PopulateFilterDataAsync(filter);

    // הדשבורד מציג את כל השורות כברירת מחדל; "הצג" רק מחיל סינון
    // (בקשת לקוח 07/2026 — מבטל את השער הקודם של "טבלה ריקה עד הצג").
    ViewBag.ShowData = true;

    var (rows, total) = await _filterService.GetReportRowsAsync(filter, _currentUser.UserId, _currentUser.UserRole);

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

  [HttpGet]
  public async Task<IActionResult> ExportExcel(DashboardFilter filter)
  {
    if (_currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval)
      filter.StatusId = 4;

    var rows = await _filterService.GetAllReportRowsAsync(
      filter, _currentUser.UserId, _currentUser.UserRole);

    using var wb = new XLWorkbook();
    var ws = wb.Worksheets.Add("דיווחים");
    ws.RightToLeft = true;

    // סוג דיווח בעמודה B מימין ל-ת.ז (משוב בטא B32).
    var headers = new[]
    {
      "מס\"ד", "סוג דיווח", "ת.ז", "קוד עובד", "שם מדווח", "חודש דיווח", "פרויקט", "מחוז", "ישוב",
      "מסגרת חינוכית", "תאריך מפגש", "משך מפגש", "תוכנית חינוכית", "תחום", "נושא 1", "נושא 2",
      "קיום דיון", "כיתה", "שכבה", "מסקנות כיתה", "מסקנות מסגרת", "מסקנות ישוב/מחוז/ארצי",
      "מסמכים", "הערות"
    };
    for (var i = 0; i < headers.Length; i++)
      ws.Cell(1, i + 1).Value = headers[i];

    var row = 2;
    foreach (var r in rows)
    {
      ws.Cell(row, 1).Value = r.SequenceNumber;
      ws.Cell(row, 2).Value = r.ReportTypeName;
      ws.Cell(row, 3).Value = r.IdNumber;
      ws.Cell(row, 4).Value = r.EmployeeCode;
      ws.Cell(row, 5).Value = r.FullName;
      ws.Cell(row, 6).Value = r.MonthDescription;
      ws.Cell(row, 7).Value = r.ProjectName;
      ws.Cell(row, 8).Value = r.DistrictName;
      ws.Cell(row, 9).Value = r.LocalityName;
      ws.Cell(row, 10).Value = r.FrameworkName;
      ws.Cell(row, 10).Style.NumberFormat.Format = "@";
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
    var rows = await _filterService.GetAllReportsAsync(
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
      ws.Cell(row, 10).Value = r.HasAttachments ? "כן" : "לא";
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
  public async Task<IActionResult> BulkApprove(List<int> reportIds, string? returnUrl = null)
  {
    var result = await _bulkReportActions.ChangeStatusAsync(
      reportIds, 4, _currentUser.UserId, _currentUser.UserRole, HttpContext.RequestAborted);
    SetBulkResult(result);
    return RedirectBack(returnUrl);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> BulkSubmit(List<int> reportIds, string? returnUrl = null)
  {
    var result = await _bulkReportActions.ChangeStatusAsync(
      reportIds, 3, _currentUser.UserId, _currentUser.UserRole, HttpContext.RequestAborted);
    SetBulkResult(result);
    return RedirectBack(returnUrl);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> BulkDelete(
    List<int> reportIds, string? reason, string? returnUrl = null)
  {
    var result = await _bulkReportActions.ArchiveAsync(
      reportIds, _currentUser.UserId, _currentUser.UserRole, reason, HttpContext.RequestAborted);
    SetBulkResult(result);
    return RedirectBack(returnUrl, nameof(Index));
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> BulkReturnApproved(
    List<int> reportIds,
    string? reason,
    bool selectAllFiltered = false,
    [Bind(Prefix = "filter")] DashboardFilter? filter = null,
    string? returnUrl = null)
  {
    filter ??= new DashboardFilter();
    if (selectAllFiltered)
    {
      if (filter.StatusId != 4)
      {
        SetBulkResult(BulkReportActionResult.Failure(
          "בחירת כל תוצאות החיפוש להחזרה מותרת רק כאשר מסנן הסטטוס הוא מאושר"));
        return RedirectBack(returnUrl);
      }

      filter.IncludeArchived = false;
      var filteredReports = await _filterService.GetAllReportsAsync(
        filter, _currentUser.UserId, _currentUser.UserRole);
      reportIds = filteredReports
        .Where(r => r.StatusId == 4 && r.ReportId > 0)
        .Select(r => r.ReportId)
        .Distinct()
        .OrderBy(id => id)
        .ToList();
    }

    var result = await _bulkReportActions.ReturnApprovedAsync(
      reportIds, reason, _currentUser.UserId, _currentUser.UserRole,
      HttpContext.RequestAborted);
    SetBulkResult(result);
    return RedirectBack(returnUrl);
  }

  private void SetBulkResult(BulkReportActionResult result)
  {
    TempData[result.Succeeded ? "Success" : "Error"] = result.Message;
    if (!result.Succeeded && result.RejectedReportIds.Count > 0)
      TempData["ErrorDetails"] = $"מזהים שלא טופלו: {string.Join(", ", result.RejectedReportIds.Take(20))}";
  }

  private IActionResult RedirectBack(string? returnUrl, string fallbackAction = nameof(Summary)) =>
    !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
      ? Redirect(returnUrl)
      : RedirectToAction(fallbackAction);

  // ── מסמכי דיווח במודאל מהדשבורד (יישור לגרסת השרת) ─────────────────────

  [HttpGet]
  public async Task<IActionResult> ReportDocuments(int reportId)
  {
    if (!await _filterService.CanAccessReportAsync(reportId, _currentUser.UserId, _currentUser.UserRole))
      return Forbid();

    var report = await _db.Reports
      .AsNoTracking()
      .Include(r => r.User)
      .Include(r => r.ReportingMonth)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return NotFound();

    var rowIds = await _db.ReportRows
      .Where(rr => rr.ReportId == reportId)
      .Select(rr => rr.Id)
      .ToListAsync();

    var reportUserId = report.UserId;
    var attachmentsQuery = _db.DocumentAttachments.AsNoTracking().AsQueryable();
    var inspectorRole = _currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval;
    var canReadWholeReport = !inspectorRole || await _filterService.CanManageWholeReportAsync(
      reportId, _currentUser.UserId, _currentUser.UserRole);
    if (canReadWholeReport)
    {
      // Report- and employee-level documents are safe only when the whole report
      // falls inside the inspector's assigned scope.
      attachmentsQuery = attachmentsQuery.Where(a => a.ReportId == reportId ||
        (a.ReportRowId.HasValue && rowIds.Contains(a.ReportRowId.Value)) ||
        (a.UserId == reportUserId && a.ReportId == null && a.ReportRowId == null));
    }
    else
    {
      var scopedAllocationIds = await _filterService.GetAccessibleAllocationIdsAsync(
        _currentUser.UserId, _currentUser.UserRole, includeInactive: true);
      var visibleRowIds = await _db.ReportRows.AsNoTracking()
        .Where(row => row.ReportId == reportId && row.AllocationId.HasValue &&
                      scopedAllocationIds.Contains(row.AllocationId.Value))
        .Select(row => row.Id)
        .ToListAsync();
      attachmentsQuery = attachmentsQuery.Where(attachment =>
        attachment.ReportRowId.HasValue && visibleRowIds.Contains(attachment.ReportRowId.Value));
    }

    var attachments = await attachmentsQuery
      .OrderByDescending(a => a.UploadedAt)
      .ToListAsync();

    return Json(new
    {
      employeeName = $"{report.User?.FirstName} {report.User?.LastName}".Trim(),
      reportMonth = report.ReportingMonth?.Description ?? "",
      documents = attachments.Select(a => new
      {
        id = a.Id,
        fileName = a.FileName,
        description = a.Description,
        uploadedAt = a.UploadedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
        fileSize = FormatFileSize(a.FileSize),
        viewUrl = Url.Action(nameof(DocumentAttachment), "Dashboard", new { attachmentId = a.Id, reportContextId = reportId }),
        downloadUrl = Url.Action(nameof(DocumentAttachment), "Dashboard", new { attachmentId = a.Id, reportContextId = reportId, download = true })
      })
    });
  }

  [HttpGet]
  public async Task<IActionResult> DocumentAttachment(int attachmentId, int? reportContextId = null, bool download = false)
  {
    var attachment = await _db.DocumentAttachments.AsNoTracking()
      .FirstOrDefaultAsync(a => a.Id == attachmentId);
    if (attachment == null) return NotFound();

    var attachmentReportId = attachment.ReportId
      ?? (attachment.ReportRowId.HasValue
        ? await _db.ReportRows.Where(rr => rr.Id == attachment.ReportRowId.Value).Select(rr => (int?)rr.ReportId).FirstOrDefaultAsync()
        : null);
    if (attachmentReportId.HasValue)
    {
      if (!await _filterService.CanAccessReportAsync(
            attachmentReportId.Value, _currentUser.UserId, _currentUser.UserRole))
        return Forbid();

      if (_currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval)
      {
        if (attachment.ReportRowId.HasValue)
        {
          var scopedAllocationIds = await _filterService.GetAccessibleAllocationIdsAsync(
            _currentUser.UserId, _currentUser.UserRole, includeInactive: true);
          var rowIsVisible = await _db.ReportRows.AsNoTracking().AnyAsync(row =>
            row.Id == attachment.ReportRowId.Value && row.AllocationId.HasValue &&
            scopedAllocationIds.Contains(row.AllocationId.Value));
          if (!rowIsVisible) return Forbid();
        }
        else if (!await _filterService.CanManageWholeReportAsync(
                   attachmentReportId.Value, _currentUser.UserId, _currentUser.UserRole))
        {
          return Forbid();
        }
      }
    }
    else
    {
      // Employee-level documents are authorized through the report whose modal
      // exposed the link. A bare attachment ID is never enough.
      if (!attachment.UserId.HasValue || !reportContextId.HasValue ||
          !await _db.Reports.AsNoTracking().AnyAsync(r =>
            r.Id == reportContextId.Value && r.UserId == attachment.UserId.Value) ||
          !await _filterService.CanAccessReportAsync(
            reportContextId.Value, _currentUser.UserId, _currentUser.UserRole))
        return Forbid();
      if ((_currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval) &&
          !await _filterService.CanManageWholeReportAsync(
            reportContextId.Value, _currentUser.UserId, _currentUser.UserRole))
        return Forbid();
    }

    var filePath = AttachmentFileSecurity.ResolveStoredPath(
      Directory.GetCurrentDirectory(), attachment.FilePath);
    if (filePath == null || !System.IO.File.Exists(filePath)) return NotFound();

    await using var stream = System.IO.File.OpenRead(filePath);
    var validation = await AttachmentFileSecurity.ValidateAsync(
      stream, Path.GetExtension(attachment.FileName), HttpContext.RequestAborted);
    var forceDownload = download || !validation.IsValid ||
                        !AttachmentFileSecurity.CanDisplayInline(attachment.FileName);
    var contentType = validation.IsValid ? validation.ContentType : "application/octet-stream";

    Response.Headers["Cache-Control"] = "no-store, private";
    Response.Headers["X-Content-Type-Options"] = "nosniff";
    if (!forceDownload)
      Response.Headers["Content-Security-Policy"] = "sandbox; default-src 'none'; img-src 'self' data:; style-src 'unsafe-inline'";

    return PhysicalFile(filePath, contentType, forceDownload ? attachment.FileName : null);
  }

  private static string FormatFileSize(long bytes) => bytes switch
  {
    >= 1_048_576 => (bytes / 1_048_576m).ToString("0.#") + " MB",
    >= 1_024 => (bytes / 1_024m).ToString("0.#") + " KB",
    _ => bytes + " B"
  };

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
    ViewBag.CanViewDashboardRows = _currentUser.UserRole is UserRoleEnum.InspectorView or UserRoleEnum.InspectorApproval;
    ViewBag.CanApprove = _currentUser.UserRole is UserRoleEnum.SystemAdmin or UserRoleEnum.ProjectManager
      or UserRoleEnum.ProjectCoordinator or UserRoleEnum.InspectorApproval;
    ViewBag.CanBulkDelete = _bulkReportActions.CanArchive(_currentUser.UserRole);
    ViewBag.CanBulkSubmit = _bulkReportActions.CanSubmit(_currentUser.UserRole);
    ViewBag.CanBulkApprove = _bulkReportActions.CanApprove(_currentUser.UserRole);
    ViewBag.CanBulkReturnApproved = _bulkReportActions.CanReturnApproved(_currentUser.UserRole);
    ViewBag.ApprovedReturnTargetStatusId = _bulkReportActions.ApprovedReturnTargetStatusId;

    using var scope = HttpContext.RequestServices.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AxiomaReporting.Infrastructure.Data.AppDbContext>();
    ViewBag.ReportingMonths = await db.ReportingMonths
      .OrderByDescending(m => m.Year).ThenByDescending(m => m.Month)
      .ToListAsync();
    ViewBag.Localities = await db.Localities.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.Frameworks = await db.Frameworks.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.EducationalPrograms = await db.EducationalPrograms.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.Domains = await db.Domains.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.Subjects = await db.Subjects.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.DiscussionCodes = await db.DiscussionCodes.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.Classes = await db.Classes.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.GradeLevels = await db.GradeLevels.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.ConclusionLocations = await db.LocalityDistrictNationals.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.ReportTypes = await db.ReportTypes.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    // Conclusion lookups live in their own tables (see SeparateConclusionLookups migration) —
    // the filter dropdowns must offer the ids that ReportRow conclusion FKs actually store.
    ViewBag.ClassConclusions = await db.ClassConclusions.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
    ViewBag.FrameworkConclusions = await db.FrameworkConclusions.Where(x => x.IsActive).OrderBy(x => x.Description).ToListAsync();
  }
}
