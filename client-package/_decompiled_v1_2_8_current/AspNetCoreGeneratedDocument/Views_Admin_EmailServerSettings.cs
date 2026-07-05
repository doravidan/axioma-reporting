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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/EmailServerSettings.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_EmailServerSettings : RazorPage<EmailServerSetting?>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-controller", "Lookup", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("asp-action", "SaveEmailServerSettings", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

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
	public IHtmlHelper<EmailServerSetting?> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "הגדרות SMTP";
		WriteLiteral("<div class=\"container mt-3\" style=\"max-width:700px\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>הגדרות שרת דוא\"ל (SMTP)</h3>\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "68f22f45a07a2a499d0c7cf294029b3c3947d460d94bb4376a409fb4534cc6d85486", async delegate
		{
			WriteLiteral("חזרה לטבלאות עזר");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n  </div>\r\n\r\n");
		if (base.TempData["Success"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\">\r\n      ");
			Write(base.TempData["Success"]);
			WriteLiteral("\r\n      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n  <div class=\"card\">\r\n    <div class=\"card-body\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "68f22f45a07a2a499d0c7cf294029b3c3947d460d94bb4376a409fb4534cc6d88040", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n\r\n        <div class=\"row mb-3\">\r\n          <div class=\"col-md-8\">\r\n            <label class=\"form-label\">שרת SMTP <span class=\"text-danger\">*</span></label>\r\n            <input name=\"smtpServer\" class=\"form-control\" required");
			BeginWriteAttribute("value", "\r\n                   value=\"", 1079, "\"", 1125, 1);
			WriteAttributeValue("", 1107, base.Model?.SmtpServer, 1107, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" placeholder=\"smtp.example.com\" />\r\n          </div>\r\n          <div class=\"col-md-4\">\r\n            <label class=\"form-label\">פורט <span class=\"text-danger\">*</span></label>\r\n            <input name=\"port\" type=\"number\" class=\"form-control\" required");
			BeginWriteAttribute("value", "\r\n                   value=\"", 1375, "\"", 1442, 1);
			EmailServerSetting? model = base.Model;
			WriteAttributeValue("", 1403, (model != null && model.Port == 0) ? new int?(587) : base.Model?.Port, 1403, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" min=\"1\" max=\"65535\" />\r\n          </div>\r\n        </div>\r\n\r\n        <div class=\"mb-3\">\r\n          <label class=\"form-label\">שם משתמש <span class=\"text-danger\">*</span></label>\r\n          <input name=\"username\" class=\"form-control\" required");
			BeginWriteAttribute("value", "\r\n                 value=\"", 1683, "\"", 1725, 1);
			WriteAttributeValue("", 1709, base.Model?.Username, 1709, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" autocomplete=\"username\" />\r\n        </div>\r\n\r\n        <div class=\"mb-3\">\r\n          <label class=\"form-label\">סיסמה</label>\r\n          <input name=\"password\" type=\"password\" class=\"form-control\"");
			BeginWriteAttribute("placeholder", "\r\n                 placeholder=\"", 1921, "\"", 2011, 1);
			WriteAttributeValue("", 1953, (base.Model != null) ? "השאר ריק לשמור על הסיסמה הנוכחית" : "", 1953, 58, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral("\r\n                 autocomplete=\"new-password\" />\r\n        </div>\r\n\r\n        <div class=\"row mb-3\">\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">כתובת שולח <span class=\"text-danger\">*</span></label>\r\n            <input name=\"fromAddress\" type=\"email\" class=\"form-control\" required");
			BeginWriteAttribute("value", "\r\n                   value=\"", 2320, "\"", 2367, 1);
			WriteAttributeValue("", 2348, base.Model?.FromAddress, 2348, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" placeholder=\"noreply@example.com\" />\r\n          </div>\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">שם שולח</label>\r\n            <input name=\"fromName\" class=\"form-control\"");
			BeginWriteAttribute("value", "\r\n                   value=\"", 2569, "\"", 2613, 1);
			WriteAttributeValue("", 2597, base.Model?.FromName, 2597, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" placeholder=\"מערכת סייט אנד סאונד\" />\r\n          </div>\r\n        </div>\r\n\r\n        <div class=\"mb-4 form-check\">\r\n          <input name=\"useSsl\" type=\"checkbox\" class=\"form-check-input\" id=\"useSsl\"\r\n                 value=\"true\" ");
			Write((base.Model?.UseSsl ?? true) ? "checked" : "");
			WriteLiteral(" />\r\n          <label class=\"form-check-label\" for=\"useSsl\">השתמש ב-SSL/TLS</label>\r\n        </div>\r\n\r\n        <div class=\"d-flex gap-2\">\r\n          <button type=\"submit\" class=\"btn btn-primary\">שמור הגדרות</button>\r\n        </div>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n");
	}
}
