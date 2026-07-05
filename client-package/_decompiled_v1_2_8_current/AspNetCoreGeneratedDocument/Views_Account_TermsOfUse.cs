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

[RazorCompiledItemMetadata("Identifier", "/Views/Account/TermsOfUse.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Account_TermsOfUse : RazorPage<TermsOfUseVersion>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "AcceptTerms", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("id", new HtmlString("acceptTermsForm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("asp-controller", "Account", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("asp-action", "Logout", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("class", new HtmlString("mt-2"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<TermsOfUseVersion> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "תנאי שימוש";
		base.Layout = "_AnonymousLayout";
		string returnUrl = base.ViewBag.ReturnUrl as string;
		WriteLiteral("<div class=\"row justify-content-center py-4\">\r\n  <div class=\"col-lg-10\">\r\n    <div class=\"card shadow-sm\">\r\n      <div class=\"card-header bg-primary text-white\">\r\n        <h1 class=\"h4 mb-0\">תנאי שימוש במערכת</h1>\r\n");
		if (base.Model != null)
		{
			WriteLiteral("          <small class=\"text-white-50\">גרסה ");
			Write(base.Model.VersionNumber);
			WriteLiteral(" · תוקף מ-");
			Write(base.Model.EffectiveFrom.ToString("dd/MM/yyyy"));
			WriteLiteral("</small>\r\n");
		}
		WriteLiteral("      </div>\r\n      <div class=\"card-body\">\r\n");
		if (base.Model == null)
		{
			WriteLiteral("          <p class=\"text-muted\">לא הוגדרה גרסה של תנאי השימוש. נא לפנות למנהל המערכת.</p>\r\n");
		}
		else
		{
			WriteLiteral("          <div class=\"terms-body mb-4\" style=\"max-height:60vh; overflow-y:auto; border:1px solid #dee2e6; border-radius:.25rem; padding:1rem; white-space:pre-wrap;\">\r\n            ");
			Write(Html.Raw(base.Model.BodyHtml));
			WriteLiteral("\r\n          </div>\r\n");
			WriteLiteral("          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "942877021b13bb42d9c3517dd1376560048916a614bdb326d07188f2fd5020787939", async delegate
			{
				WriteLiteral("\r\n            ");
				Write(Html.AntiForgeryToken());
				WriteLiteral("\r\n            <input type=\"hidden\" name=\"returnUrl\"");
				BeginWriteAttribute("value", " value=\"", 1176, "\"", 1194, 1);
				WriteAttributeValue("", 1184, returnUrl, 1184, 10, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\r\n            <div class=\"form-check mb-3\">\r\n              <input class=\"form-check-input\" type=\"checkbox\" id=\"acceptCheckbox\" aria-required=\"true\" />\r\n              <label class=\"form-check-label\" for=\"acceptCheckbox\">\r\n                קראתי ואני מסכים לתנאי השימוש\r\n              </label>\r\n            </div>\r\n            <div class=\"d-flex justify-content-end gap-2\">\r\n              <button type=\"submit\" id=\"acceptBtn\" class=\"btn btn-primary\" disabled>\r\n                אישור והמשך\r\n              </button>\r\n            </div>\r\n          ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "942877021b13bb42d9c3517dd1376560048916a614bdb326d07188f2fd50207811018", async delegate
			{
				WriteLiteral("\r\n            ");
				Write(Html.AntiForgeryToken());
				WriteLiteral("\r\n            <button type=\"submit\" class=\"btn btn-link text-muted\">יציאה</button>\r\n          ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
		}
		WriteLiteral("      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n");
		DefineSection("Scripts", (RenderAsyncDelegate)async delegate
		{
			WriteLiteral("\r\n  <script>\r\n    (function () {\r\n      var chk = document.getElementById('acceptCheckbox');\r\n      var btn = document.getElementById('acceptBtn');\r\n      if (chk && btn) {\r\n        chk.addEventListener('change', function () { btn.disabled = !chk.checked; });\r\n      }\r\n    })();\r\n  </script>\r\n");
		});
	}
}
