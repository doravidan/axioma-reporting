using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class EmailTemplate : BaseEntity
{
  public string TypeDescription { get; set; } = string.Empty;
  public string Subject { get; set; } = string.Empty;
  public string Body { get; set; } = string.Empty;
  public bool IsActive { get; set; } = true;
}
