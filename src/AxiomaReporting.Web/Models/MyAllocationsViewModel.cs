using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.DTOs;

namespace AxiomaReporting.Web.Models;

/// <summary>
/// View model for /MyAllocations — the Employee submenu landing page.
/// </summary>
public class MyAllocationsViewModel
{
  public ReportingMonth? ActiveMonth { get; set; }
  public int AllocationCount { get; set; }
  public bool AllowExcelUpload { get; set; }
  public int? ExcelUploadAllocationId { get; set; }
  public List<Allocation> Allocations { get; set; } = new();
  public List<PastReportListItem> HistoryReports { get; set; } = new();
}
