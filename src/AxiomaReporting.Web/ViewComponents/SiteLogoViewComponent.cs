using AxiomaReporting.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AxiomaReporting.Web.ViewComponents;

/// <summary>
/// Renders the active site logo image. Invoke as
/// <c>@await Component.InvokeAsync("SiteLogo", new { cssClass = "...", maxHeightPx = 40 })</c>.
/// Resolves the current logo path from <see cref="IBrandingService"/> so Admins
/// can hot-swap the logo from the Branding screen without a redeploy.
/// </summary>
public class SiteLogoViewComponent : ViewComponent
{
  private readonly IBrandingService _branding;

  public SiteLogoViewComponent(IBrandingService branding)
  {
    _branding = branding;
  }

  public async Task<IViewComponentResult> InvokeAsync(
    string cssClass = "",
    int? maxHeightPx = null,
    string alt = "לוגו המערכת")
  {
    var path = await _branding.GetLogoPathAsync();
    var model = new SiteLogoModel
    {
      Path = path,
      CssClass = cssClass,
      MaxHeightPx = maxHeightPx,
      Alt = alt
    };
    return View(model);
  }
}

public class SiteLogoModel
{
  public string Path { get; set; } = string.Empty;
  public string CssClass { get; set; } = string.Empty;
  public int? MaxHeightPx { get; set; }
  public string Alt { get; set; } = "לוגו המערכת";
}
