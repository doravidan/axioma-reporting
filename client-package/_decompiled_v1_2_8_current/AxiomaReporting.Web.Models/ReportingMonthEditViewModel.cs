using System;

namespace AxiomaReporting.Web.Models;

public class ReportingMonthEditViewModel
{
	public int Id { get; set; }

	public string Description { get; set; } = string.Empty;


	public int Month { get; set; }

	public int Year { get; set; }

	public DateTime LastReportingDate { get; set; }

	public bool AllowFutureReporting { get; set; }

	public bool IsActive { get; set; }

	public bool LockNonAdminFields { get; set; }
}
