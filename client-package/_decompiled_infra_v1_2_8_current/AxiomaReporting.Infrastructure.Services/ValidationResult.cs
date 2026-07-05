using System.Collections.Generic;
using System.Linq;

namespace AxiomaReporting.Infrastructure.Services;

public class ValidationResult
{
	public bool IsValid => !Errors.Any();

	public List<string> Errors { get; } = new List<string>();


	public List<string> Warnings { get; } = new List<string>();


	public void AddError(string msg)
	{
		Errors.Add(msg);
	}

	public void AddWarning(string msg)
	{
		Warnings.Add(msg);
	}
}
