using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Report/SelectAllocation.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Report_SelectAllocation : RazorPage<dynamic>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("list-group-item list-group-item-action"), HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

	private TagHelperScopeManager __tagHelperScopeManager
	{
		get
		{
			if (__backed__tagHelperScopeManager == null)
			{
				__backed__tagHelperScopeManager = new TagHelperScopeManager(base.StartTagHelperWritingScope, base.EndTagHelperWritingScope);
			}
			return __backed__tagHelperScopeManager;
		}
	}

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
		base.ViewData["Title"] = "בחירת הקצאה";
		User user = base.ViewBag.Employee as User;
		List<Allocation> list = base.ViewBag.Allocations as List<Allocation>;
		WriteLiteral("<div class=\"container mt-4\">\r\n  <h3>בחירת פרויקט לדיווח - ");
		Write(user?.FirstName);
		WriteLiteral(" ");
		Write(user?.LastName);
		WriteLiteral("</h3>\r\n  <div class=\"d-flex justify-content-end mt-3\">\r\n    <a class=\"btn btn-primary btn-sm\" href=\"/Report/Manual\">\u05d4\u05d5\u05e1\u05e4\u05ea \u05d3\u05d9\u05d5\u05d5\u05d7 \u05d9\u05d3\u05e0\u05d9</a>\r\n  </div>\r\n  <div class=\"list-group mt-3\">\r\n");
		foreach (Allocation alloc in list ?? new List<Allocation>())
		{
			WriteLiteral("      ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "1ef6fa11ef263850575eb677838a07a7948dab81256da94902fabfc829cd7f535163", async delegate
			{
				WriteLiteral("\r\n        ");
				Write(alloc.Project?.Description);
				WriteLiteral("\r\n      ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-allocationId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(alloc.Id);
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"] = __tagHelperStringValueBuffer;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-allocationId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"], HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
		}
		WriteLiteral("  </div>\r\n</div>\r\n");
	}
}
