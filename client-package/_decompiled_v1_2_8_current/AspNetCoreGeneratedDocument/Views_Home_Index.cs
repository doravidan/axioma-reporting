using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Home/Index.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Home_Index : RazorPage<dynamic>
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
		base.ViewData["Title"] = "ראשי";
		WriteLiteral("<div class=\"text-center\">\r\n    <h1 class=\"display-4\">ברוכים הבאים למערכת דיווח סייט אנד סאונד</h1>\r\n    <p>מערכת דיווח פעילות חודשית לעובדים</p>\r\n</div>\r\n");
	}
}
