using System.Collections.Generic;

namespace AxiomaReporting.Infrastructure.Services;

public class BatchImportResult
{
	public int TotalRowsRead { get; set; }

	public int RowsImported { get; set; }

	public int ErrorRowsCount { get; set; }

	public int EmployeesAffected { get; set; }

	public List<BatchImportError> Errors { get; set; } = new List<BatchImportError>();


	public List<BatchImportEmployeeSummary> EmployeeSummaries { get; set; } = new List<BatchImportEmployeeSummary>();


	public List<BatchImportRowResult> RowResults { get; set; } = new List<BatchImportRowResult>();

}
