using System;

namespace AxiomaReporting.Infrastructure.Services;

public class DashboardFilter
{
	public int? DistrictId { get; set; }

	public int? SectorId { get; set; }

	public int? ProgramId { get; set; }

	public int? LocalityId { get; set; }

	public int? FrameworkId { get; set; }

	public int? EducationalProgramId { get; set; }

	public int? DomainId { get; set; }

	public int? Subject1Id { get; set; }

	public int? Subject2Id { get; set; }

	public int? DiscussionCodeId { get; set; }

	public int? ClassId { get; set; }

	public int? GradeLevelId { get; set; }

	public int? ConclusionClassId { get; set; }

	public int? ConclusionFrameworkId { get; set; }

	public int? ConclusionLocationId { get; set; }

	public int? ReportTypeId { get; set; }

	public DateTime? MeetingDateFrom { get; set; }

	public DateTime? MeetingDateTo { get; set; }

	public decimal? MeetingDuration { get; set; }

	public string? EmployeeCode { get; set; }

	public string? IdNumber { get; set; }

	public string? EmployeeName { get; set; }

	public int? StatusId { get; set; }

	public int? FromMonthId { get; set; }

	public int? ToMonthId { get; set; }

	public int Page { get; set; } = 1;


	public int PageSize { get; set; } = 25;


	public string? SortBy { get; set; }

	public bool SortDesc { get; set; }

	public bool IncludeArchived { get; set; }
}
