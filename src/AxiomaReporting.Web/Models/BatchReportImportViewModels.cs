using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Services;

namespace AxiomaReporting.Web.Models;

public class BatchReportImportFormViewModel
{
  public List<ReportingMonth> ReportingMonths { get; set; } = new();
  public int? SelectedReportingMonthId { get; set; }
}

public class BatchReportImportResultViewModel
{
  public string? MonthDescription { get; set; }
  public int? MonthNumber { get; set; }
  public int? Year { get; set; }
  public BatchImportResult Result { get; set; } = new();
}
