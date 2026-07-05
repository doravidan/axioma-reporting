using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Shared/Error.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Shared_Error : RazorPage<ErrorViewModel>
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
	public IHtmlHelper<ErrorViewModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "שגיאה";
		WriteLiteral("\n<div class=\"alert alert-danger mt-3\" role=\"alert\" aria-live=\"assertive\">\n  <h1 class=\"h4 mb-2\">אירעה שגיאה</h1>\n  <p class=\"mb-0\">לא ניתן להשלים את הפעולה כרגע. אם הבעיה חוזרת, יש לפנות למנהל המערכת.</p>\n</div>\n\n");
		if (base.Model.ShowRequestId)
		{
			WriteLiteral("    <p class=\"text-muted\">\n        <strong>מזהה בקשה:</strong> <code>");
			Write(base.Model.RequestId);
			WriteLiteral("</code>\n    </p>\n");
		}
	}
}
