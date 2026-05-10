namespace AxiomaReporting.Core.Entities;

public class UserStatus
{
  public int Id { get; set; }
  /// <summary>Internal English code used for lookups (e.g. "Active"). Do not display.</summary>
  public string Name { get; set; } = string.Empty;
  /// <summary>Hebrew label rendered in the UI.</summary>
  public string? DescriptionHebrew { get; set; }
}
