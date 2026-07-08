namespace AxiomaReporting.Core.DTOs;

/// <summary>
/// Row in the "דיווחים קודמים" list on the monthly-activity screen — a report
/// from a reporting month other than the one currently displayed.
/// </summary>
public record PastReportListItem(
  int ReportId,
  int AllocationId,
  string MonthDescription,
  string StatusDescription,
  int StatusId,
  DateTime? SubmittedAt,
  int RowCount);
