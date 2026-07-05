using System.Threading.Tasks;
using AxiomaReporting.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AxiomaReporting.Web.ViewComponents;

public class SiteLogoViewComponent : ViewComponent
{
	private readonly IBrandingService _branding;

	public SiteLogoViewComponent(IBrandingService branding)
	{
		_branding = branding;
	}

	public async Task<IViewComponentResult> InvokeAsync(string cssClass = "", int? maxHeightPx = null, string alt = "לוגו המערכת")
	{
		string path = await _branding.GetLogoPathAsync();
		SiteLogoModel model = new SiteLogoModel
		{
			Path = path,
			CssClass = cssClass,
			MaxHeightPx = maxHeightPx,
			Alt = alt
		};
		return View(model);
	}
}
