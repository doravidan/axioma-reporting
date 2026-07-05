namespace AxiomaReporting.Infrastructure.Services;

public class BatchImportEmployeeSummary
{
	public int? UserId { get; set; }

	public string EmployeeCode { get; set; } = "";


	public string ReporterName { get; set; } = "";


	public int RowsImported { get; set; }

	public int RowsRejected { get; set; }
}
