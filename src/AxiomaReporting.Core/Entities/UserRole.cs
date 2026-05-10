namespace AxiomaReporting.Core.Entities;

public class UserRole
{
  public int Id { get; set; }
  /// <summary>Internal English code used for lookups (e.g. "SystemAdmin"). Do not display.</summary>
  public string Name { get; set; } = string.Empty;
  /// <summary>Long English description retained for backwards compatibility.</summary>
  public string? Description { get; set; }
  /// <summary>Short Hebrew label used in every UI surface (badges, dropdowns, lists).</summary>
  public string? DescriptionHebrew { get; set; }
}
