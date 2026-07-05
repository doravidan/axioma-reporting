using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Home/Privacy.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Home_Privacy : RazorPage<dynamic>
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
	public IHtmlHelper<dynamic> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "מדיניות פרטיות";
		WriteLiteral("\n<h1>");
		Write(base.ViewData["Title"]);
		WriteLiteral("</h1>\n\n<p>המערכת שומרת מידע לצורך ניהול דיווחי פעילות, הרשאות משתמשים ובקרת שימוש.</p>\n");
	}
}
