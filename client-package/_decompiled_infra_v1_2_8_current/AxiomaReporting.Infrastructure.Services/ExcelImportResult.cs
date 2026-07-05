using System.Collections.Generic;

namespace AxiomaReporting.Infrastructure.Services;

public class ExcelImportResult
{
	public bool Success => Errors.Count == 0;

	public int ImportedRows { get; set; }

	public List<string> Errors { get; } = new List<string>();

}
