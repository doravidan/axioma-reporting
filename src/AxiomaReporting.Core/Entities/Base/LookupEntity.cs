namespace AxiomaReporting.Core.Entities.Base;

public abstract class LookupEntity : BaseEntity
{
  public string Description { get; set; } = string.Empty;
  public bool IsActive { get; set; } = true;
}
