using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Infrastructure.BackgroundJobs;

public class ReminderService : BackgroundService
{
	private readonly IServiceProvider _services;

	private readonly ILogger<ReminderService> _logger;

	public ReminderService(IServiceProvider services, ILogger<ReminderService> logger)
	{
		_services = services;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("ReminderService started");
		while (!stoppingToken.IsCancellationRequested)
		{
			int num;
			try
			{
				num = (await RunReminderCycleAsync(stoppingToken)).Item1;
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				_logger.LogError(ex, "ReminderService: Unhandled error in reminder cycle");
				num = 1;
			}
			await Task.Delay(TimeSpan.FromHours(num), stoppingToken);
		}
	}

	private async Task<(int checkIntervalHours, int processed)> RunReminderCycleAsync(CancellationToken stoppingToken)
	{
		using IServiceScope scope = _services.CreateScope();
		AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		IEmailService emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
		List<SystemConstant> constants = await db.SystemConstants.ToListAsync(stoppingToken);
		int reminderInterval = GetConstant(constants, "ReminderIntervalDays", 3);
		int startDaysBefore = GetConstant(constants, "ReminderStartDaysBeforeDeadline", 7);
		int checkIntervalHours = GetConstant(constants, "ReminderCheckIntervalHours", 1);
		int constant = GetConstant(constants, "PasswordExpiryWarningDays", 14);
		await SendPasswordExpiryWarningsAsync(db, emailService, constant, stoppingToken);
		ReportingMonth activeMonth = await db.ReportingMonths.FirstOrDefaultAsync((ReportingMonth m) => m.IsActive, stoppingToken);
		if (activeMonth == null)
		{
			_logger.LogDebug("ReminderService: No active reporting month");
			return (checkIntervalHours: checkIntervalHours, processed: 0);
		}
		DateTime today = DateTime.Today;
		DateTime deadline = activeMonth.LastReportingDate.Date;
		if ((deadline - today).Days > startDaysBefore)
		{
			_logger.LogDebug("ReminderService: Not yet within reminder window");
			return (checkIntervalHours: checkIntervalHours, processed: 0);
		}
		List<User> reportingEmployees = await db.Users.Where((User u) => u.IsReportingEmployee && u.StatusId == 1 && u.Email != null).ToListAsync(stoppingToken);
		List<int> submittedUserIds = await (from r in db.Reports
			where r.ReportingMonthId == activeMonth.Id && r.StatusId >= 3 && r.StatusId != 5
			select r.UserId).ToListAsync(stoppingToken);
		List<int> returnedUserIds = await (from r in db.Reports
			where r.ReportingMonthId == activeMonth.Id && r.StatusId == 5
			select r.UserId).ToListAsync(stoppingToken);
		foreach (User employee in reportingEmployees)
		{
			if (stoppingToken.IsCancellationRequested)
			{
				break;
			}
			string templateType;
			if (returnedUserIds.Contains(employee.Id))
			{
				templateType = "ReminderNeedsCorrection";
			}
			else
			{
				if (submittedUserIds.Contains(employee.Id))
				{
					continue;
				}
				templateType = "ReminderNotSubmitted";
			}
			if (await ShouldSendReminderAsync(db, employee.Id, activeMonth.Id, templateType, reminderInterval, stoppingToken))
			{
				await emailService.SendAsync(employee.Email, employee.FirstName + " " + employee.LastName, templateType, new Dictionary<string, string>
				{
					["EmployeeName"] = employee.FirstName + " " + employee.LastName,
					["MonthName"] = activeMonth.Description,
					["Month"] = activeMonth.Month.ToString(),
					["Year"] = activeMonth.Year.ToString(),
					["DeadlineDate"] = deadline.ToString("dd/MM/yyyy"),
					["Deadline"] = deadline.ToString("dd/MM/yyyy")
				}, null, stoppingToken);
				db.ReminderLogs.Add(new ReminderLog
				{
					UserId = employee.Id,
					ReportingMonthId = activeMonth.Id,
					TemplateType = templateType,
					SentAt = DateTime.UtcNow
				});
				await db.SaveChangesAsync(stoppingToken);
				_logger.LogInformation("ReminderService: Sent {Template} to {Email}", templateType, employee.Email);
			}
		}
		return (checkIntervalHours: checkIntervalHours, processed: reportingEmployees.Count);
	}

	private async Task SendPasswordExpiryWarningsAsync(AppDbContext db, IEmailService emailService, int warningDays, CancellationToken stoppingToken)
	{
		DateTime today = DateTime.UtcNow.Date;
		DateTime expiryThreshold = today.AddDays(warningDays);
		foreach (User user in await db.Users.Where((User u) => u.StatusId == 1 && u.Email != null && u.LastPasswordChange != null && u.LastPasswordChange.Value.AddDays(90.0) <= expiryThreshold && u.LastPasswordChange.Value.AddDays(90.0) > today).ToListAsync(stoppingToken))
		{
			if (!stoppingToken.IsCancellationRequested)
			{
				DateTime expiryDate = user.LastPasswordChange.Value.AddDays(90.0);
				int daysLeft = (expiryDate.Date - today).Days;
				if (!(await db.ReminderLogs.AnyAsync((ReminderLog l) => l.UserId == user.Id && l.TemplateType == "PasswordExpiryWarning" && l.SentAt >= ((DateTime)today).AddDays(-1.0), stoppingToken)))
				{
					await emailService.SendAsync(user.Email, user.FirstName + " " + user.LastName, "PasswordExpiryWarning", new Dictionary<string, string>
					{
						["EmployeeName"] = user.FirstName + " " + user.LastName,
						["DaysLeft"] = daysLeft.ToString(),
						["ExpiryDate"] = expiryDate.ToString("dd/MM/yyyy")
					}, null, stoppingToken);
					db.ReminderLogs.Add(new ReminderLog
					{
						UserId = user.Id,
						ReportingMonthId = null,
						TemplateType = "PasswordExpiryWarning",
						SentAt = DateTime.UtcNow
					});
					await db.SaveChangesAsync(stoppingToken);
					_logger.LogInformation("ReminderService: Sent PasswordExpiryWarning to {Email} ({DaysLeft} days left)", user.Email, daysLeft);
				}
				continue;
			}
			break;
		}
	}

	private static async Task<bool> ShouldSendReminderAsync(AppDbContext db, int userId, int reportingMonthId, string templateType, int reminderIntervalDays, CancellationToken cancellationToken)
	{
		string templateType2 = templateType;
		DateTime? dateTime = await ((IQueryable<ReminderLog>)(from l in db.ReminderLogs
			where l.UserId == userId && l.ReportingMonthId == (int?)reportingMonthId && l.TemplateType == templateType2
			orderby l.SentAt descending
			select l)).Select((Expression<Func<ReminderLog, DateTime?>>)((ReminderLog l) => l.SentAt)).FirstOrDefaultAsync(cancellationToken);
		return !dateTime.HasValue || dateTime.Value.Date <= DateTime.UtcNow.Date.AddDays(-reminderIntervalDays);
	}

	private static int GetConstant(IEnumerable<SystemConstant> constants, string key, int fallback)
	{
		string key2 = key;
		if (!int.TryParse(constants.FirstOrDefault((SystemConstant c) => c.Key == key2)?.Value, out var result))
		{
			return fallback;
		}
		return result;
	}
}
