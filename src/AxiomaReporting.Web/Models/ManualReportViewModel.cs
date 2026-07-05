using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Web.Models;

/// <summary>
/// View model for the manual-report entry flow (ReportController.Manual):
/// lets privileged roles pick a reporting month before filling a report on
/// behalf of an employee.
/// </summary>
public class ManualReportViewModel
{
  public List<ReportingMonth> ReportingMonths { get; set; } = new();
}
