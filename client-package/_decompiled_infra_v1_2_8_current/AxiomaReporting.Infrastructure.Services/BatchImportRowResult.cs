namespace AxiomaReporting.Infrastructure.Services;

public class BatchImportRowResult
{
	public int FileRowNumber { get; set; }

	public string? EmployeeCode { get; set; }

	public string? ReporterName { get; set; }

	public BatchImportRowOutcome Outcome { get; set; }

	public string ResultDescription { get; set; } = string.Empty;

}
