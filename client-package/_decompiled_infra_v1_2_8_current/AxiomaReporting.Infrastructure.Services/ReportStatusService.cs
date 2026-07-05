using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

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
		Report report2 = await _db.Reports.FirstOrDefaultAsync((Report r) => r.UserId == userId && r.ReportingMonthId == reportingMonthId);
		if (report2 != null)
		{
			return report2;
		}
		Report report = new Report
		{
			UserId = userId,
			ReportingMonthId = reportingMonthId,
			StatusId = 1,
			CreatedAt = DateTime.UtcNow
		};
		_db.Reports.Add(report);
		await _db.SaveChangesAsync();
		return report;
	}

	public async Task<bool> SaveDraftAsync(int reportId)
	{
		Report report = await _db.Reports.FindAsync(reportId);
		if (report == null)
		{
			return false;
		}
		if (report.StatusId == 1)
		{
			int previous = report.StatusId;
			report.StatusId = 2;
			report.UpdatedAt = DateTime.UtcNow;
			await _db.SaveChangesAsync();
			if (_auditLog != null)
			{
				await _auditLog.LogAsync("Report.StatusChange", "Report", report.Id.ToString(), new
				{
					StatusId = previous
				}, new { report.StatusId }, "draft->in entry");
			}
		}
		return true;
	}

	public async Task<bool> SubmitReportAsync(int reportId, int submittedByUserId)
	{
		Report report = await _db.Reports.Include((Report r) => r.User).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return false;
		}
		int previousStatus = report.StatusId;
		report.StatusId = 3;
		report.SubmittedAt = DateTime.UtcNow;
		report.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		if (_auditLog != null)
		{
			await _auditLog.LogAsync("Report.StatusChange", "Report", report.Id.ToString(), new
			{
				StatusId = previousStatus
			}, new { report.StatusId }, $"submitted by user {submittedByUserId}");
		}
		if (report.User?.Email != null)
		{
			ReportingMonth reportingMonth = await _db.ReportingMonths.FindAsync(report.ReportingMonthId);
			await _emailService.SendAsync(report.User.Email, report.User.FirstName + " " + report.User.LastName, "ReportReceived", new Dictionary<string, string>
			{
				["EmployeeName"] = report.User.FirstName + " " + report.User.LastName,
				["MonthName"] = reportingMonth?.Description ?? string.Empty,
				["Month"] = reportingMonth?.Month.ToString() ?? string.Empty,
				["Year"] = reportingMonth?.Year.ToString() ?? string.Empty,
				["DeadlineDate"] = reportingMonth?.LastReportingDate.ToString("dd/MM/yyyy") ?? string.Empty,
				["Deadline"] = reportingMonth?.LastReportingDate.ToString("dd/MM/yyyy") ?? string.Empty
			});
		}
		return true;
	}

	public async Task<bool> ApproveReportAsync(int reportId, int approvedByUserId)
	{
		Report report = await _db.Reports.Include((Report r) => r.User).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return false;
		}
		int previousStatus = report.StatusId;
		report.StatusId = 4;
		report.ApprovedAt = DateTime.UtcNow;
		report.ApprovedBy = approvedByUserId;
		report.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		if (_auditLog != null)
		{
			await _auditLog.LogAsync("Report.StatusChange", "Report", report.Id.ToString(), new
			{
				StatusId = previousStatus
			}, new { report.StatusId }, $"approved by user {approvedByUserId}");
		}
		if (report.User?.Email != null)
		{
			ReportingMonth reportingMonth = await _db.ReportingMonths.FindAsync(report.ReportingMonthId);
			await _emailService.SendAsync(report.User.Email, report.User.FirstName + " " + report.User.LastName, "ReportApproved", new Dictionary<string, string>
			{
				["EmployeeName"] = report.User.FirstName + " " + report.User.LastName,
				["MonthName"] = reportingMonth?.Description ?? string.Empty,
				["Month"] = reportingMonth?.Month.ToString() ?? string.Empty,
				["Year"] = reportingMonth?.Year.ToString() ?? string.Empty
			});
		}
		return true;
	}

	public async Task<bool> RejectReportAsync(int reportId, int rejectedByUserId, string rejectionReason)
	{
		Report report = await _db.Reports.Include((Report r) => r.User).FirstOrDefaultAsync((Report r) => r.Id == reportId);
		if (report == null)
		{
			return false;
		}
		int previousStatus = report.StatusId;
		report.StatusId = 5;
		report.RejectionReason = rejectionReason;
		report.RejectedAt = DateTime.UtcNow;
		report.RejectedBy = rejectedByUserId;
		report.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		if (_auditLog != null)
		{
			await _auditLog.LogAsync("Report.StatusChange", "Report", report.Id.ToString(), new
			{
				StatusId = previousStatus
			}, new { report.StatusId, rejectionReason }, $"rejected by user {rejectedByUserId}");
		}
		if (report.User?.Email != null)
		{
			ReportingMonth reportingMonth = await _db.ReportingMonths.FindAsync(report.ReportingMonthId);
			await _emailService.SendAsync(report.User.Email, report.User.FirstName + " " + report.User.LastName, "ReportRejected", new Dictionary<string, string>
			{
				["EmployeeName"] = report.User.FirstName + " " + report.User.LastName,
				["MonthName"] = reportingMonth?.Description ?? string.Empty,
				["Month"] = reportingMonth?.Month.ToString() ?? string.Empty,
				["Year"] = reportingMonth?.Year.ToString() ?? string.Empty,
				["RejectionReason"] = rejectionReason
			});
		}
		return true;
	}
}
