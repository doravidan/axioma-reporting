namespace AxiomaReporting.Web.Models;

public class TermsOfUseVersionListItem
{
  public int Id { get; set; }
  public int VersionNumber { get; set; }
  public DateTime EffectiveFrom { get; set; }
  public DateTime CreatedAt { get; set; }
  public string PublishedByName { get; set; } = string.Empty;
  public int AcceptanceCount { get; set; }
  public string BodyHtml { get; set; } = string.Empty;
}
