using AxiomaReporting.Infrastructure.Services;

namespace AxiomaReporting.Web.Models;

public class BatchReportImportResultViewModel
{
	public string? MonthDescription { get; set; }

	public int? MonthNumber { get; set; }

	public int? Year { get; set; }

	public BatchImportResult Result { get; set; } = new BatchImportResult();

}
