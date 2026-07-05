using System;

namespace AxiomaReporting.Infrastructure.Services;

public class DashboardReportRow
{
	public int ReportId { get; set; }

	public int UserId { get; set; }

	public int? AllocationId { get; set; }

	public string ProjectName { get; set; } = string.Empty;


	public string EmployeeCode { get; set; } = string.Empty;


	public string IdNumber { get; set; } = string.Empty;


	public string FullName { get; set; } = string.Empty;


	public string MonthDescription { get; set; } = string.Empty;


	public int MonthYear { get; set; }

	public int MonthMonth { get; set; }

	public string StatusName { get; set; } = string.Empty;


	public int StatusId { get; set; }

	public int RowCount { get; set; }

	public decimal TotalDuration { get; set; }

	public int? MonthlyRowAllocation { get; set; }

	public int RemainingRows
	{
		get
		{
			if (!MonthlyRowAllocation.HasValue)
			{
				return 0;
			}
			return MonthlyRowAllocation.Value - RowCount;
		}
	}

	public bool HasAttachments { get; set; }

	public int DocumentCount { get; set; }

	public DateTime? SubmittedAt { get; set; }
}
