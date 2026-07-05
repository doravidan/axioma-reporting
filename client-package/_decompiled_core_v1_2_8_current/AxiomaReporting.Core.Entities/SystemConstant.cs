using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class SystemConstant : BaseEntity
{
	public string Key { get; set; } = string.Empty;


	public string Value { get; set; } = string.Empty;


	public string? Description { get; set; }

	public int? UpdatedBy { get; set; }
}
