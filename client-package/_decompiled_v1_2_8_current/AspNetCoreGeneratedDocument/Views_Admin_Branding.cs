using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/Branding.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_Branding : RazorPage<dynamic>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-controller", "Admin", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("asp-action", "SystemConstants", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("asp-action", "UploadLogo", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("enctype", new HtmlString("multipart/form-data"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("aria-label", new HtmlString("טופס העלאת לוגו חדש"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("asp-action", "ResetLogo", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("class", new HtmlString("mt-3"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("aria-label", new HtmlString("טופס איפוס הלוגו לברירת מחדל"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("onsubmit", new HtmlString("return confirm('האם לאפס את הלוגו לברירת המחדל?');"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<dynamic> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "לוגו המערכת";
		string currentLogoPath = (string)(base.ViewBag.CurrentLogoPath ?? "/images/logo.png");
		WriteLiteral("<div class=\"container-fluid mt-3\" dir=\"rtl\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>לוגו המערכת</h3>\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "0884e3222f5185b03bc58c6ee9deaad6f3ba51b10900b5ccdffd7e79ec82626a7577", async delegate
		{
			WriteLiteral("חזרה לקבועי מערכת");
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
		WriteLiteral("\r\n  </div>\r\n\r\n  <p class=\"text-muted\">הלוגו מוצג בדף הכניסה ובסרגל העליון של המערכת. ניתן להחליפו כאן ללא צורך בפריסה מחודשת.</p>\r\n\r\n  <div class=\"card mb-3\" aria-labelledby=\"brandingPreviewHeader\">\r\n    <div class=\"card-body\">\r\n      <h5 id=\"brandingPreviewHeader\" class=\"card-title\">תצוגה מקדימה של הלוגו הנוכחי</h5>\r\n      <div class=\"branding-preview\" role=\"img\" aria-label=\"תצוגה מקדימה של הלוגו הפעיל\">\r\n        <img");
		BeginWriteAttribute("src", " src=\"", 814, "\"", 836, 1);
		WriteAttributeValue("", 820, currentLogoPath, 820, 16, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(" alt=\"הלוגו הפעיל כעת\" />\r\n      </div>\r\n      <p class=\"small text-muted mt-2 mb-0\">נתיב נוכחי: <code>");
		Write(currentLogoPath);
		WriteLiteral("</code></p>\r\n    </div>\r\n  </div>\r\n\r\n  <div class=\"card\">\r\n    <div class=\"card-body\">\r\n      <h5 class=\"card-title\">העלאת לוגו חדש</h5>\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "0884e3222f5185b03bc58c6ee9deaad6f3ba51b10900b5ccdffd7e79ec82626a10412", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"mb-3\">\r\n          <label for=\"logoFile\" class=\"form-label\">קובץ לוגו</label>\r\n          <input id=\"logoFile\" name=\"logoFile\" type=\"file\"\r\n                 accept=\".png,.svg,.jpg,.jpeg\"\r\n                 class=\"form-control\"\r\n                 aria-describedby=\"logoFileHelp\"\r\n                 aria-required=\"true\"\r\n                 required />\r\n          <div id=\"logoFileHelp\" class=\"form-text\">\r\n            סוגי קבצים מותרים: PNG, SVG, JPG. גודל מקסימלי: 2 מ\"ב. מומלץ יחס רוחב/גובה של כ-3:1 לתצוגה מיטבית בסרגל העליון.\r\n          </div>\r\n        </div>\r\n        <div class=\"d-flex gap-2\">\r\n          <button type=\"submit\" class=\"btn btn-primary\">העלה לוגו</button>\r\n        </div>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
		BeginWriteTagHelperAttribute();
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__tagHelperExecutionContext.AddHtmlAttribute("novalidate", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "0884e3222f5185b03bc58c6ee9deaad6f3ba51b10900b5ccdffd7e79ec82626a13601", async delegate
		{
			WriteLiteral("\r\n    ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n    <button type=\"submit\" class=\"btn btn-outline-secondary btn-sm\">אפס לברירת מחדל</button>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_7.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n</div>\r\n");
	}
}
