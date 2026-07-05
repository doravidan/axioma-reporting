namespace AxiomaReporting.Infrastructure.Services;

public class BatchImportError
{
	public int FileRowNumber { get; set; }

	public string? EmployeeCode { get; set; }

	public string? ReporterName { get; set; }

	public string ErrorMessage { get; set; } = string.Empty;


	public string? RawValues { get; set; }
}
