using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public interface IReportStatusService
{
  Task<Report?> GetOrCreateDraftAsync(int userId, int reportingMonthId);
  Task<bool> SubmitReportAsync(int reportId, int submittedByUserId);
  Task<bool> ApproveReportAsync(int reportId, int approvedByUserId);
  Task<bool> RejectReportAsync(int reportId, int rejectedByUserId, string rejectionReason);
  Task<bool> ReopenReportAsync(int reportId, int reopenedByUserId);
  Task<bool> SaveDraftAsync(int reportId);
}

public class ReportStatusService : IReportStatusService
{
  private readonly AppDbContext _db;
  private readonly IEmailService _emailService;
  private readonly IAuditLogService? _auditLog;

  public ReportStatusService(AppDbContext db, IEmailService emailService, IAuditLogService? auditLog = null)
  {
    _db = db;
    _emailService = emailService;
    _auditLog = auditLog;
  }

  public async Task<Report?> GetOrCreateDraftAsync(int userId, int reportingMonthId)
  {
    var existing = await _db.Reports
      .FirstOrDefaultAsync(r => r.UserId == userId && r.ReportingMonthId == reportingMonthId);

    if (existing != null) return existing;

    var report = new Report
    {
      UserId = userId,
      ReportingMonthId = reportingMonthId,
      StatusId = 1, // Draft
      CreatedAt = DateTime.UtcNow
    };
    _db.Reports.Add(report);
    await _db.SaveChangesAsync();
    return report;
  }

  public async Task<bool> SaveDraftAsync(int reportId)
  {
    var report = await _db.Reports.FindAsync(reportId);
    if (report == null) return false;
    if (report.StatusId == 1) // Move from Draft to InEntry when rows are first saved
    {
      var previous = report.StatusId;
      report.StatusId = 2;
      report.UpdatedAt = DateTime.UtcNow;
      await _db.SaveChangesAsync();

      if (_auditLog != null)
        await _auditLog.LogAsync("Report.StatusChange", nameof(Report), report.Id.ToString(),
          before: new { StatusId = previous },
          after: new { StatusId = report.StatusId },
          notes: "draft->in entry");
    }
    return true;
  }

  public async Task<bool> SubmitReportAsync(int reportId, int submittedByUserId)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return false;

    var previousStatus = report.StatusId;
    report.StatusId = 3; // Pending Approval
    report.SubmittedAt = DateTime.UtcNow;
    report.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    if (_auditLog != null)
      await _auditLog.LogAsync("Report.StatusChange", nameof(Report), report.Id.ToString(),
        before: new { StatusId = previousStatus },
        after: new { StatusId = report.StatusId },
        notes: $"submitted by user {submittedByUserId}");

    if (report.User?.Email != null)
    {
      var month = await _db.ReportingMonths.FindAsync(report.ReportingMonthId);
      await _emailService.SendAsync(
        report.User.Email,
        $"{report.User.FirstName} {report.User.LastName}",
        "ReportReceived",
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{report.User.FirstName} {report.User.LastName}",
          ["MonthName"] = month?.Description ?? string.Empty,
          ["Month"] = month?.Month.ToString() ?? string.Empty,
          ["Year"] = month?.Year.ToString() ?? string.Empty,
          ["DeadlineDate"] = month?.LastReportingDate.ToString("dd/MM/yyyy") ?? string.Empty,
          ["Deadline"] = month?.LastReportingDate.ToString("dd/MM/yyyy") ?? string.Empty
        });
    }
    return true;
  }

  public async Task<bool> ApproveReportAsync(int reportId, int approvedByUserId)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return false;

    var previousStatus = report.StatusId;
    report.StatusId = 4; // Approved
    report.ApprovedAt = DateTime.UtcNow;
    report.ApprovedBy = approvedByUserId;
    report.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    if (_auditLog != null)
      await _auditLog.LogAsync("Report.StatusChange", nameof(Report), report.Id.ToString(),
        before: new { StatusId = previousStatus },
        after: new { StatusId = report.StatusId },
        notes: $"approved by user {approvedByUserId}");

    if (report.User?.Email != null)
    {
      var month = await _db.ReportingMonths.FindAsync(report.ReportingMonthId);
      await _emailService.SendAsync(
        report.User.Email,
        $"{report.User.FirstName} {report.User.LastName}",
        "ReportApproved",
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{report.User.FirstName} {report.User.LastName}",
          ["MonthName"] = month?.Description ?? string.Empty,
          ["Month"] = month?.Month.ToString() ?? string.Empty,
          ["Year"] = month?.Year.ToString() ?? string.Empty
        });
    }
    return true;
  }

  public async Task<bool> RejectReportAsync(int reportId, int rejectedByUserId, string rejectionReason)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null) return false;

    var previousStatus = report.StatusId;
    report.StatusId = 5; // Returned for Correction
    report.RejectionReason = rejectionReason;
    report.RejectedAt = DateTime.UtcNow;
    report.RejectedBy = rejectedByUserId;
    report.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    if (_auditLog != null)
      await _auditLog.LogAsync("Report.StatusChange", nameof(Report), report.Id.ToString(),
        before: new { StatusId = previousStatus },
        after: new { StatusId = report.StatusId, rejectionReason },
        notes: $"rejected by user {rejectedByUserId}");

    if (report.User?.Email != null)
    {
      var month = await _db.ReportingMonths.FindAsync(report.ReportingMonthId);
      await _emailService.SendAsync(
        report.User.Email,
        $"{report.User.FirstName} {report.User.LastName}",
        "ReportRejected",
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{report.User.FirstName} {report.User.LastName}",
          ["MonthName"] = month?.Description ?? string.Empty,
          ["Month"] = month?.Month.ToString() ?? string.Empty,
          ["Year"] = month?.Year.ToString() ?? string.Empty,
          ["RejectionReason"] = rejectionReason
        });
    }
    return true;
  }

  /// <summary>
  /// Returns an APPROVED report to the editable הוחזר-לתיקון state so the
  /// employee can keep reporting within the same month (status-override
  /// capability of SystemAdmin/ProjectManager per the role spec).
  /// </summary>
  public async Task<bool> ReopenReportAsync(int reportId, int reopenedByUserId)
  {
    var report = await _db.Reports
      .Include(r => r.User)
      .FirstOrDefaultAsync(r => r.Id == reportId);
    if (report == null || report.StatusId != 4) return false;

    const string reason = "הדיווח נפתח מחדש לעריכה והוספת שורות";
    report.StatusId = 5; // Returned for Correction — editable by the owner
    report.RejectionReason = reason;
    report.ApprovedAt = null;
    report.ApprovedBy = null;
    report.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    if (_auditLog != null)
      await _auditLog.LogAsync("Report.StatusChange", nameof(Report), report.Id.ToString(),
        before: new { StatusId = 4 },
        after: new { StatusId = report.StatusId },
        notes: $"reopened for editing by user {reopenedByUserId}");

    if (report.User?.Email != null)
    {
      var month = await _db.ReportingMonths.FindAsync(report.ReportingMonthId);
      await _emailService.SendAsync(
        report.User.Email,
        $"{report.User.FirstName} {report.User.LastName}",
        "ReportRejected",
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{report.User.FirstName} {report.User.LastName}",
          ["MonthName"] = month?.Description ?? string.Empty,
          ["Month"] = month?.Month.ToString() ?? string.Empty,
          ["Year"] = month?.Year.ToString() ?? string.Empty,
          ["RejectionReason"] = reason
        });
    }
    return true;
  }
}
