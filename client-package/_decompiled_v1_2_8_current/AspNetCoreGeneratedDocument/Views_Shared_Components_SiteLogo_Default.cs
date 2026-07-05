using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Web.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Shared/Components/SiteLogo/Default.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Shared_Components_SiteLogo_Default : RazorPage<SiteLogoModel>
{
	[RazorInject]
	public IModelExpressionProvider ModelExpressionProvider { get; private set; }

	[RazorInject]
	public IUrlHelper Url { get; private set; }

	[RazorInject]
	public IViewComponentHelper Component { get; private set; }

	[RazorInject]
	public IJsonHelper Json { get; private set; }

	[RazorInject]
	public IHtmlHelper<SiteLogoModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		string value = (base.Model.Path.StartsWith("~") ? Url.Content(base.Model.Path) : base.Model.Path);
		string text = (base.Model.MaxHeightPx.HasValue ? $"max-height:{base.Model.MaxHeightPx.Value}px; width:auto;" : null);
		if (text == null)
		{
			WriteLiteral("  <img");
			BeginWriteAttribute("src", " src=\"", 374, "\"", 384, 1);
			WriteAttributeValue("", 380, value, 380, 4, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("alt", " alt=\"", 385, "\"", 401, 1);
			WriteAttributeValue("", 391, base.Model.Alt, 391, 10, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("class", " class=\"", 402, "\"", 425, 1);
			WriteAttributeValue("", 410, base.Model.CssClass, 410, 15, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n");
		}
		else
		{
			WriteLiteral("  <img");
			BeginWriteAttribute("src", " src=\"", 449, "\"", 459, 1);
			WriteAttributeValue("", 455, value, 455, 4, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("alt", " alt=\"", 460, "\"", 476, 1);
			WriteAttributeValue("", 466, base.Model.Alt, 466, 10, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("class", " class=\"", 477, "\"", 500, 1);
			WriteAttributeValue("", 485, base.Model.CssClass, 485, 15, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("style", " style=\"", 501, "\"", 515, 1);
			WriteAttributeValue("", 509, text, 509, 6, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n");
		}
	}
}
