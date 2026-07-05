using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Account/ResetPassword.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Account_ResetPassword : RazorPage<dynamic>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "ResetPassword", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

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
		base.ViewData["Title"] = "איפוס סיסמה";
		string token = base.ViewBag.Token as string;
		WriteLiteral("\r\n<div class=\"container mt-4\">\r\n  <div class=\"row justify-content-center\">\r\n    <div class=\"col-md-5\">\r\n      <div class=\"card\">\r\n        <div class=\"card-header\"><h4>איפוס סיסמה</h4></div>\r\n        <div class=\"card-body\">\r\n");
		if (!base.ViewData.ModelState.IsValid)
		{
			WriteLiteral("            <div class=\"alert alert-danger\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\">\r\n");
			foreach (ModelError item in base.ViewData.ModelState.Values.SelectMany((ModelStateEntry v) => v.Errors))
			{
				WriteLiteral("                <div>");
				Write(item.ErrorMessage);
				WriteLiteral("</div>\r\n");
			}
			WriteLiteral("            </div>\r\n");
		}
		WriteLiteral("          ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "30090977f766c96612a8a7a5e60a37d2f2dd7c18bce0ae84c1039ffa6215ccb45833", async delegate
		{
			WriteLiteral("\r\n            ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n            <input type=\"hidden\" name=\"token\"");
			BeginWriteAttribute("value", " value=\"", 817, "\"", 831, 1);
			WriteAttributeValue("", 825, token, 825, 6, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n            <div class=\"mb-3\">\r\n              <label class=\"form-label\">סיסמה חדשה</label>\r\n              <input name=\"newPassword\" type=\"password\" class=\"form-control\" autocomplete=\"new-password\" required />\r\n            </div>\r\n            <div class=\"mb-3\">\r\n              <label class=\"form-label\">אישור סיסמה חדשה</label>\r\n              <input name=\"confirmPassword\" type=\"password\" class=\"form-control\" autocomplete=\"new-password\" required />\r\n            </div>\r\n            <button type=\"submit\" class=\"btn btn-primary\">אפס סיסמה</button>\r\n          ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n");
	}
}
