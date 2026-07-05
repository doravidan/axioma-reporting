using System;

namespace AxiomaReporting.Infrastructure.Services;

public class DashboardReportDetailRow
{
	public int ReportId { get; set; }

	public int ReportRowId { get; set; }

	public int UserId { get; set; }

	public int? AllocationId { get; set; }

	public bool CanEdit { get; set; }

	public int SequenceNumber { get; set; }

	public string IdNumber { get; set; } = string.Empty;


	public string EmployeeCode { get; set; } = string.Empty;


	public string FullName { get; set; } = string.Empty;


	public string StatusName { get; set; } = string.Empty;


	public int StatusId { get; set; }

	public string MonthDescription { get; set; } = string.Empty;


	public int MonthYear { get; set; }

	public int MonthMonth { get; set; }

	public string ProjectName { get; set; } = string.Empty;


	public string DistrictName { get; set; } = string.Empty;


	public string LocalityName { get; set; } = string.Empty;


	public string FrameworkName { get; set; } = string.Empty;


	public string EducationalProgramName { get; set; } = string.Empty;


	public string DomainName { get; set; } = string.Empty;


	public string Subject1Name { get; set; } = string.Empty;


	public string Subject2Name { get; set; } = string.Empty;


	public string DiscussionCodeName { get; set; } = string.Empty;


	public string ClassName { get; set; } = string.Empty;


	public string GradeLevelName { get; set; } = string.Empty;


	public string ConclusionClassName { get; set; } = string.Empty;


	public string ConclusionFrameworkName { get; set; } = string.Empty;


	public string ConclusionLocationName { get; set; } = string.Empty;


	public string ReportTypeName { get; set; } = string.Empty;


	public DateTime MeetingDate { get; set; }

	public decimal MeetingDuration { get; set; }

	public string Notes { get; set; } = string.Empty;


	public bool HasAttachments { get; set; }
}
