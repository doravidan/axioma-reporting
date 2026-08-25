using System.Text.Json;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AxiomaReporting.Infrastructure.Services;

public sealed class BulkReportActionOptions
{
  public const string SectionName = "BulkReportActions";

  // The customer has not yet resolved whether bulk actions are Admin-only.
  // Safe defaults keep them Admin-only until an explicit production decision.
  public bool AllowProjectManagersToDelete { get; set; }
  public bool AllowProjectManagersToSubmit { get; set; }
  public bool AllowProjectManagersToApprove { get; set; }
  public bool AllowProjectManagersToReturnApproved { get; set; }
  public bool AllowReportingEmployeesToSubmitOwn { get; set; }
  public bool RequireDeletionReason { get; set; } = true;
  public int ApprovedReturnTargetStatusId { get; set; } = 3;
  public int MaxSelectionCount { get; set; } = 10000;
}

public sealed record BulkReportActionResult(
  bool Succeeded,
  int UpdatedCount,
  int UnchangedCount,
  string Message,
  IReadOnlyList<int> RejectedReportIds)
{
  public static BulkReportActionResult Failure(string message, IReadOnlyList<int>? rejected = null) =>
    new(false, 0, 0, message, rejected ?? Array.Empty<int>());
}

public interface IBulkReportActionService
{
  Task<BulkReportActionResult> ArchiveAsync(
    IReadOnlyCollection<int> reportIds, int actorUserId, UserRoleEnum actorRole,
    string? reason = null, CancellationToken ct = default);

  Task<BulkReportActionResult> ChangeStatusAsync(
    IReadOnlyCollection<int> reportIds, int targetStatusId, int actorUserId,
    UserRoleEnum actorRole, CancellationToken ct = default);

  Task<BulkReportActionResult> ReturnApprovedAsync(
    IReadOnlyCollection<int> reportIds, string? reason, int actorUserId,
    UserRoleEnum actorRole, CancellationToken ct = default);

  bool CanArchive(UserRoleEnum role);
  bool CanSubmit(UserRoleEnum role);
  bool CanApprove(UserRoleEnum role);
  bool CanReturnApproved(UserRoleEnum role);
  int ApprovedReturnTargetStatusId { get; }
}

public sealed class BulkReportActionService : IBulkReportActionService
{
  private readonly AppDbContext _db;
  private readonly IDashboardFilterService _dashboardScope;
  private readonly IEmailService _emailService;
  private readonly BulkReportActionOptions _options;
  private readonly ILogger<BulkReportActionService> _logger;

  public BulkReportActionService(
    AppDbContext db,
    IDashboardFilterService dashboardScope,
    IEmailService emailService,
    IOptions<BulkReportActionOptions> options,
    ILogger<BulkReportActionService> logger)
  {
    _db = db;
    _dashboardScope = dashboardScope;
    _emailService = emailService;
    _options = options.Value;
    _logger = logger;
  }

  public bool CanArchive(UserRoleEnum role) =>
    role == UserRoleEnum.SystemAdmin ||
    role == UserRoleEnum.ProjectManager && _options.AllowProjectManagersToDelete;

  public bool CanSubmit(UserRoleEnum role) =>
    role == UserRoleEnum.SystemAdmin ||
    role == UserRoleEnum.ProjectManager && _options.AllowProjectManagersToSubmit ||
    role == UserRoleEnum.Employee && _options.AllowReportingEmployeesToSubmitOwn;

  public bool CanApprove(UserRoleEnum role) =>
    role == UserRoleEnum.SystemAdmin ||
    role == UserRoleEnum.ProjectManager && _options.AllowProjectManagersToApprove;

  public bool CanReturnApproved(UserRoleEnum role) =>
    _options.ApprovedReturnTargetStatusId is 3 or 5 &&
    (role == UserRoleEnum.SystemAdmin ||
     role == UserRoleEnum.ProjectManager && _options.AllowProjectManagersToReturnApproved);

  public int ApprovedReturnTargetStatusId => _options.ApprovedReturnTargetStatusId;

  public async Task<BulkReportActionResult> ArchiveAsync(
    IReadOnlyCollection<int> reportIds, int actorUserId, UserRoleEnum actorRole,
    string? reason = null, CancellationToken ct = default)
  {
    if (!CanArchive(actorRole))
      return BulkReportActionResult.Failure("אין הרשאה למחיקה מרובה של דיווחים");
    var normalizedReason = NormalizeReason(reason);
    if (_options.RequireDeletionReason && normalizedReason == null)
      return BulkReportActionResult.Failure("יש לציין סיבת מחיקה");

    var normalized = Normalize(reportIds);
    var validation = await ValidateSelectionAsync(normalized, actorUserId, actorRole, ct);
    if (validation != null) return validation;

    var reports = await LoadReportsAsync(normalized, includeDetails: false, ct);
    var changed = reports.Where(r => !r.IsArchived).ToList();
    var unchanged = reports.Count - changed.Count;

    await using var transaction = await BeginTransactionIfSupportedAsync(ct);
    try
    {
      var now = DateTime.UtcNow;
      foreach (var report in changed)
      {
        report.IsArchived = true;
        report.UpdatedAt = now;
        AddAudit(report.Id, actorUserId, "Report.BulkArchive",
          new { IsArchived = false }, new { IsArchived = true },
          $"bulk logical deletion; reason: {normalizedReason ?? "not supplied under configured policy"}");
      }

      await _db.SaveChangesAsync(ct);
      if (transaction != null) await transaction.CommitAsync(ct);
      return new BulkReportActionResult(
        true, changed.Count, unchanged,
        $"{changed.Count} דיווחים נמחקו (הועברו לארכיון); {unchanged} כבר היו בארכיון",
        Array.Empty<int>());
    }
    catch (DbUpdateConcurrencyException ex)
    {
      if (transaction != null) await transaction.RollbackAsync(ct);
      _logger.LogWarning(ex, "Bulk report archive failed because data changed concurrently");
      return BulkReportActionResult.Failure("הדיווחים השתנו במקביל. לא בוצעה מחיקה; יש לרענן ולנסות שוב");
    }
    catch
    {
      if (transaction != null) await transaction.RollbackAsync(ct);
      throw;
    }
  }

  public async Task<BulkReportActionResult> ChangeStatusAsync(
    IReadOnlyCollection<int> reportIds, int targetStatusId, int actorUserId,
    UserRoleEnum actorRole, CancellationToken ct = default)
  {
    if (targetStatusId is not (3 or 4))
      return BulkReportActionResult.Failure("סטטוס היעד אינו נתמך");
    if (targetStatusId == 3 && !CanSubmit(actorRole) || targetStatusId == 4 && !CanApprove(actorRole))
      return BulkReportActionResult.Failure("אין הרשאה לפעולת הסטטוס שנבחרה");

    var normalized = Normalize(reportIds);
    var validation = await ValidateSelectionAsync(normalized, actorUserId, actorRole, ct);
    if (validation != null) return validation;

    var reports = await LoadReportsAsync(normalized, includeDetails: true, ct);

    var invalid = reports
      .Where(r => r.IsArchived ||
                  r.StatusId != targetStatusId && !IsLegalTransition(r.StatusId, targetStatusId))
      .Select(r => r.Id)
      .ToList();
    if (invalid.Count > 0)
      return BulkReportActionResult.Failure(
        $"לא בוצע שינוי: {invalid.Count} דיווחים אינם במצב שמאפשר את המעבר המבוקש",
        invalid);

    var changed = reports.Where(r => r.StatusId != targetStatusId).ToList();
    var unchanged = reports.Count - changed.Count;

    await using var transaction = await BeginTransactionIfSupportedAsync(ct);
    try
    {
      var now = DateTime.UtcNow;
      foreach (var report in changed)
      {
        var before = report.StatusId;
        report.StatusId = targetStatusId;
        report.UpdatedAt = now;
        if (targetStatusId == 3)
          report.SubmittedAt = now;
        else
        {
          report.ApprovedAt = now;
          report.ApprovedBy = actorUserId;
        }

        AddAudit(report.Id, actorUserId, "Report.BulkStatusChange",
          new { StatusId = before }, new { StatusId = targetStatusId },
          $"bulk status change by user {actorUserId}");
      }

      await _db.SaveChangesAsync(ct);
      if (transaction != null) await transaction.CommitAsync(ct);
    }
    catch (DbUpdateConcurrencyException ex)
    {
      if (transaction != null) await transaction.RollbackAsync(ct);
      _logger.LogWarning(ex, "Bulk report status change failed because data changed concurrently");
      return BulkReportActionResult.Failure("הדיווחים השתנו במקביל. לא בוצע שינוי; יש לרענן ולנסות שוב");
    }
    catch
    {
      if (transaction != null) await transaction.RollbackAsync(ct);
      throw;
    }

    foreach (var report in changed)
      await SendStatusNotificationSafelyAsync(report, targetStatusId);

    var successMessage = targetStatusId == 3
      ? $"{changed.Count} דיווחים הועברו להוגש בהצלחה"
      : $"{changed.Count} דיווחים אושרו בהצלחה";
    return new BulkReportActionResult(
      true, changed.Count, unchanged,
      $"{successMessage}; {unchanged} כבר היו בסטטוס היעד",
      Array.Empty<int>());
  }

  public async Task<BulkReportActionResult> ReturnApprovedAsync(
    IReadOnlyCollection<int> reportIds, string? reason, int actorUserId,
    UserRoleEnum actorRole, CancellationToken ct = default)
  {
    if (!CanReturnApproved(actorRole))
      return BulkReportActionResult.Failure("אין הרשאה להחזרת דיווחים מאושרים");

    var normalizedReason = NormalizeReason(reason);
    if (normalizedReason == null)
      return BulkReportActionResult.Failure("יש לציין סיבת החזרה");

    var targetStatusId = _options.ApprovedReturnTargetStatusId;
    if (targetStatusId is not (3 or 5))
      return BulkReportActionResult.Failure("סטטוס היעד להחזרת דיווח מאושר אינו מוגדר באופן תקין");

    var normalized = Normalize(reportIds);
    var validation = await ValidateSelectionAsync(normalized, actorUserId, actorRole, ct);
    if (validation != null) return validation;

    var reports = await LoadReportsAsync(normalized, includeDetails: true, ct);
    var invalid = reports
      .Where(r => r.IsArchived || r.StatusId != 4)
      .Select(r => r.Id)
      .ToList();
    if (invalid.Count > 0)
      return BulkReportActionResult.Failure(
        $"לא בוצעה החזרה: {invalid.Count} דיווחים אינם בסטטוס מאושר או שנמחקו",
        invalid);

    await using var transaction = await BeginTransactionIfSupportedAsync(ct);
    try
    {
      var now = DateTime.UtcNow;
      foreach (var report in reports)
      {
        var previousApprovedAt = report.ApprovedAt;
        var previousApprovedBy = report.ApprovedBy;
        report.StatusId = targetStatusId;
        report.ApprovedAt = null;
        report.ApprovedBy = null;
        report.UpdatedAt = now;
        if (targetStatusId == 5)
        {
          report.RejectionReason = normalizedReason;
          report.RejectedAt = now;
          report.RejectedBy = actorUserId;
        }
        else
        {
          report.RejectionReason = null;
          report.RejectedAt = null;
          report.RejectedBy = null;
        }

        AddAudit(report.Id, actorUserId, "Report.BulkApprovedReturn",
          new { StatusId = 4, ApprovedAt = previousApprovedAt, ApprovedBy = previousApprovedBy },
          new { StatusId = targetStatusId, ApprovedAt = (DateTime?)null, ApprovedBy = (int?)null },
          $"approved report returned; reason: {normalizedReason}");
      }

      await _db.SaveChangesAsync(ct);
      if (transaction != null) await transaction.CommitAsync(ct);
    }
    catch (DbUpdateConcurrencyException ex)
    {
      if (transaction != null) await transaction.RollbackAsync(ct);
      _logger.LogWarning(ex, "Bulk approved-report return failed because data changed concurrently");
      return BulkReportActionResult.Failure(
        "הדיווחים השתנו במקביל. לא בוצעה החזרה; יש לרענן ולנסות שוב");
    }
    catch
    {
      if (transaction != null) await transaction.RollbackAsync(ct);
      throw;
    }

    return new BulkReportActionResult(
      true, reports.Count, 0,
      $"{reports.Count} דיווחים הוחזרו מסטטוס מאושר לסטטוס {TargetStatusText(targetStatusId)}",
      Array.Empty<int>());
  }

  private async Task<BulkReportActionResult?> ValidateSelectionAsync(
    List<int> ids, int actorUserId, UserRoleEnum actorRole, CancellationToken ct)
  {
    if (ids.Count == 0)
      return BulkReportActionResult.Failure("לא נבחרו דיווחים");
    var max = Math.Clamp(_options.MaxSelectionCount, 1, 10000);
    if (ids.Count > max)
      return BulkReportActionResult.Failure($"ניתן לבצע פעולה על עד {max} דיווחים בכל פעם");

    var manageableIds = await _dashboardScope.GetManageableReportIdsAsync(
      ids, actorUserId, actorRole, ct);
    var manageableSet = manageableIds.ToHashSet();
    var rejected = ids.Where(id => !manageableSet.Contains(id)).ToList();

    return rejected.Count == 0
      ? null
      : BulkReportActionResult.Failure(
        $"לא בוצעה פעולה: {rejected.Count} מזהים אינם קיימים או אינם מורשים",
        rejected);
  }

  private static List<int> Normalize(IReadOnlyCollection<int>? reportIds) =>
    reportIds?.Where(id => id > 0).Distinct().OrderBy(id => id).ToList() ?? new List<int>();

  private static string? NormalizeReason(string? reason)
  {
    var value = reason?.Trim();
    if (string.IsNullOrWhiteSpace(value)) return null;
    return value.Length <= 1700 ? value : value[..1700];
  }

  private async Task<List<Report>> LoadReportsAsync(
    IReadOnlyCollection<int> reportIds, bool includeDetails, CancellationToken ct)
  {
    var reports = new List<Report>();
    foreach (var chunk in reportIds.Chunk(500))
    {
      var batch = chunk.ToArray();
      IQueryable<Report> query = _db.Reports.Where(r => batch.Contains(r.Id));
      if (includeDetails)
        query = query.Include(r => r.User).Include(r => r.ReportingMonth);
      reports.AddRange(await query.OrderBy(r => r.Id).ToListAsync(ct));
    }
    return reports.OrderBy(r => r.Id).ToList();
  }

  private static string TargetStatusText(int statusId) => statusId == 3 ? "הוגש" : "הוחזר לתיקון";

  private static bool IsLegalTransition(int sourceStatusId, int targetStatusId) =>
    targetStatusId switch
    {
      3 => sourceStatusId is 2 or 5, // בהזנה/הוחזר לתיקון -> הוגש
      4 => sourceStatusId == 3,      // הוגש -> אושר
      _ => false
    };

  private async Task<IDbContextTransaction?> BeginTransactionIfSupportedAsync(CancellationToken ct) =>
    _db.Database.IsRelational() ? await _db.Database.BeginTransactionAsync(ct) : null;

  private void AddAudit(
    int reportId, int actorUserId, string action, object before, object after, string notes)
  {
    _db.AuditLogs.Add(new AuditLog
    {
      Timestamp = DateTime.UtcNow,
      ActorUserId = actorUserId,
      Action = action,
      EntityType = nameof(Report),
      EntityId = reportId.ToString(),
      Before = JsonSerializer.Serialize(before),
      After = JsonSerializer.Serialize(after),
      Notes = notes
    });
  }

  private async Task SendStatusNotificationSafelyAsync(Report report, int targetStatusId)
  {
    if (string.IsNullOrWhiteSpace(report.User?.Email)) return;
    try
    {
      var values = new Dictionary<string, string>
      {
        ["EmployeeName"] = $"{report.User.FirstName} {report.User.LastName}".Trim(),
        ["MonthName"] = report.ReportingMonth?.Description ?? string.Empty,
        ["Month"] = report.ReportingMonth?.Month.ToString() ?? string.Empty,
        ["Year"] = report.ReportingMonth?.Year.ToString() ?? string.Empty,
        ["DeadlineDate"] = report.ReportingMonth?.LastReportingDate.ToString("dd/MM/yyyy") ?? string.Empty,
        ["Deadline"] = report.ReportingMonth?.LastReportingDate.ToString("dd/MM/yyyy") ?? string.Empty
      };
      await _emailService.SendAsync(
        report.User.Email,
        values["EmployeeName"],
        targetStatusId == 3 ? "ReportReceived" : "ReportApproved",
        values);
    }
    catch (Exception ex)
    {
      _logger.LogWarning(ex, "Report {ReportId} changed status but its notification could not be queued", report.Id);
    }
  }
}
