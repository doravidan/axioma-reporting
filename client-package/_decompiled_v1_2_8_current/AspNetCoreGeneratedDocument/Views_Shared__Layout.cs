using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Shared/_Layout.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Shared__Layout : RazorPage<dynamic>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("rel", new HtmlString("stylesheet"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("href", "~/css/site.css", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("href", "~/lib/choices.js/choices.min.css", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("href", "~/css/theme.css", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("href", "~/AxiomaReporting.Web.styles.css", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("class", new HtmlString("navbar-brand p-0 app-navbar-brand"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("asp-controller", "Home", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("aria-label", new HtmlString("דף הבית - סייט אנד סאונד"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("class", new HtmlString("nav-link"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("asp-controller", "MyAllocations", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("asp-controller", "Report", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("asp-controller", "Dashboard", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("asp-controller", "Allocations", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_14 = new TagHelperAttribute("asp-controller", "Employee", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_15 = new TagHelperAttribute("asp-controller", "Admin", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_16 = new TagHelperAttribute("asp-action", "ReportingMonths", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_17 = new TagHelperAttribute("class", new HtmlString("dropdown-item"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_18 = new TagHelperAttribute("asp-controller", "Lookup", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_19 = new TagHelperAttribute("asp-action", "Frameworks", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_20 = new TagHelperAttribute("asp-action", "Institutions", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_21 = new TagHelperAttribute("asp-action", "InspectorAssignments", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_22 = new TagHelperAttribute("asp-action", "SystemConstants", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_23 = new TagHelperAttribute("asp-action", "Branding", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_24 = new TagHelperAttribute("asp-action", "TermsOfUse", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_25 = new TagHelperAttribute("asp-action", "EmailTemplates", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_26 = new TagHelperAttribute("asp-action", "EmailServerSettings", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_27 = new TagHelperAttribute("asp-action", "NotificationLogs", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_28 = new TagHelperAttribute("asp-action", "AuditLog", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_29 = new TagHelperAttribute("asp-action", "ProjectPrograms", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_30 = new TagHelperAttribute("asp-action", "DataMigration", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_31 = new TagHelperAttribute("asp-controller", "Account", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_32 = new TagHelperAttribute("asp-action", "ChangePassword", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_33 = new TagHelperAttribute("asp-action", "Logout", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_34 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_35 = new TagHelperAttribute("class", new HtmlString("m-0"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_36 = new TagHelperAttribute("src", new HtmlString("~/lib/jquery/dist/jquery.min.js"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_37 = new TagHelperAttribute("src", new HtmlString("~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_38 = new TagHelperAttribute("src", new HtmlString("~/lib/jquery-validation/dist/jquery.validate.min.js"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_39 = new TagHelperAttribute("src", new HtmlString("~/lib/jquery-validation/dist/localization/messages_he.js"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_40 = new TagHelperAttribute("src", new HtmlString("~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_41 = new TagHelperAttribute("src", "~/lib/choices.js/choices.min.js", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_42 = new TagHelperAttribute("src", "~/js/site.js", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;

	private UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;

	private LinkTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper;

	private BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;

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
		WriteLiteral("<!DOCTYPE html>\r\n<html lang=\"he\" dir=\"rtl\">\r\n");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a18112", async delegate
		{
			WriteLiteral("\r\n  <meta charset=\"utf-8\" />\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n  <title>");
			Write(base.ViewData["Title"]);
			WriteLiteral(" - סייט אנד סאונד</title>\r\n  <link rel=\"stylesheet\" href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.rtl.min.css\" />\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", TagMode.SelfClosing, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a18935", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper = CreateTagHelper<LinkTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.Href = (string)__tagHelperAttribute_1.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", TagMode.SelfClosing, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a21023", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper = CreateTagHelper<LinkTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.Href = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", TagMode.SelfClosing, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a23111", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper = CreateTagHelper<LinkTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.Href = (string)__tagHelperAttribute_3.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", TagMode.SelfClosing, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a25201", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper = CreateTagHelper<LinkTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.Href = (string)__tagHelperAttribute_4.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
		});
		__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<HeadTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a27997", async delegate
		{
			WriteLiteral("\r\n  <a b-bo2dbjxgkc href=\"#main-content\" class=\"skip-link\">דלג לתוכן הראשי</a>\r\n  <header b-bo2dbjxgkc>\r\n    <nav b-bo2dbjxgkc class=\"navbar navbar-expand-sm navbar-toggleable-sm navbar-dark app-navbar border-bottom box-shadow mb-3\"\r\n         aria-label=\"ניווט ראשי\">\r\n      <div b-bo2dbjxgkc class=\"container-fluid\">\r\n        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a28614", async delegate
			{
				WriteLiteral("\r\n          ");
				Write(await Component.InvokeAsync("SiteLogo", new
				{
					cssClass = "app-navbar-logo",
					maxHeightPx = 36,
					alt = "סייט אנד סאונד"
				}));
				WriteLiteral("\r\n        ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        <button b-bo2dbjxgkc class=\"navbar-toggler\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\".navbar-collapse\"\r\n                aria-controls=\"navbarSupportedContent\" aria-expanded=\"false\" aria-label=\"פתח תפריט ניווט\">\r\n          <span b-bo2dbjxgkc class=\"navbar-toggler-icon\"></span>\r\n        </button>\r\n        <div b-bo2dbjxgkc class=\"navbar-collapse collapse d-sm-inline-flex justify-content-between app-navbar-content\">\r\n");
			if (User.Identity?.IsAuthenticated ?? false)
			{
				WriteLiteral("            <ul b-bo2dbjxgkc class=\"navbar-nav app-main-nav\">\r\n              <li b-bo2dbjxgkc class=\"nav-item\">\r\n                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a31503", async delegate
				{
					WriteLiteral("ראשי");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n              </li>\r\n");
				if (User.IsInRole("6"))
				{
					WriteLiteral("                <li b-bo2dbjxgkc class=\"nav-item\">\r\n                  ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a33387", async delegate
					{
						WriteLiteral("פעילות חודשית");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_10.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n                </li>\r\n");
					WriteLiteral("                <li b-bo2dbjxgkc class=\"nav-item\">\r\n                  <a class=\"nav-link\" href=\"/Report/History\">היסטוריית דיווחים</a>\r\n                </li>\r\n");
				}
				else
				{
					WriteLiteral("                <li b-bo2dbjxgkc class=\"nav-item\">\r\n                  ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a35295", async delegate
					{
						WriteLiteral("פעילות חודשית");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_11.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n                </li>\r\n");
				}
				if (!User.IsInRole("6"))
				{
					WriteLiteral("                <li b-bo2dbjxgkc class=\"nav-item\">\r\n                  ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a37376", async delegate
					{
						WriteLiteral("דש בורד דיווחים");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_12.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\n                </li>\r\n");
				}
				WriteLiteral("              <li b-bo2dbjxgkc class=\"nav-item\">\r\n                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a39242", async delegate
				{
					WriteLiteral("הקצאות");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_13.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n              </li>\r\n");
				if (User.IsInRole("1") || User.IsInRole("2") || User.IsInRole("3"))
				{
					WriteLiteral("                <li b-bo2dbjxgkc class=\"nav-item\">\r\n                  ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a41174", async delegate
					{
						WriteLiteral("עובדים");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_14.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_14);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n                </li>\r\n");
				}
				if (User.IsInRole("1") || User.IsInRole("2"))
				{
					WriteLiteral("                <li b-bo2dbjxgkc class=\"nav-item\">\r\n                  ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a43269", async delegate
					{
						WriteLiteral("חודשי דיווח");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_16.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n                </li>\r\n");
				}
				if (User.IsInRole("1"))
				{
					WriteLiteral("                <li b-bo2dbjxgkc class=\"nav-item dropdown\">\r\n                  <a b-bo2dbjxgkc class=\"nav-link dropdown-toggle\" href=\"#\" data-bs-toggle=\"dropdown\">ניהול</a>\r\n                  <ul b-bo2dbjxgkc class=\"dropdown-menu\">\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a45556", async delegate
					{
						WriteLiteral("טבלאות עזר");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_18.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_18);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc><a b-bo2dbjxgkc class=\"dropdown-item\" href=\"/Admin/PrivacyPolicy\">מדיניות פרטיות</a></li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a47150", async delegate
					{
						WriteLiteral("מסגרות");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_19.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_19);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a48742", async delegate
					{
						WriteLiteral("מוסדות");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_20.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_20);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc><hr b-bo2dbjxgkc class=\"dropdown-divider\"></li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a50424", async delegate
					{
						WriteLiteral("שיוכי מפקחים");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_21.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_21);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc><hr b-bo2dbjxgkc class=\"dropdown-divider\"></li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a52112", async delegate
					{
						WriteLiteral("קבועי מערכת");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_22.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_22);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a53709", async delegate
					{
						WriteLiteral("לוגו המערכת");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_23.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_23);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a55306", async delegate
					{
						WriteLiteral("תנאי שימוש");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_24.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_24);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a56902", async delegate
					{
						WriteLiteral("תבניות מייל");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_25.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_25);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a58499", async delegate
					{
						WriteLiteral("הגדרות שרת מייל");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_26.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_26);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a60100", async delegate
					{
						WriteLiteral("יומן התראות");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_27.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_27);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a61697", async delegate
					{
						WriteLiteral("יומן ביקורת");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_28.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_28);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc><hr b-bo2dbjxgkc class=\"dropdown-divider\"></li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a63384", async delegate
					{
						WriteLiteral("ניהול תוכניות לפי פרויקט");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_29.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_29);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                    <li b-bo2dbjxgkc>");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a64994", async delegate
					{
						WriteLiteral("ייבוא נתונים ראשוני");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_30.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_30);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("</li>\r\n                  </ul>\r\n                </li>\r\n");
				}
				WriteLiteral("            </ul>\r\n            <ul b-bo2dbjxgkc class=\"navbar-nav me-auto app-user-nav\">\r\n              <li b-bo2dbjxgkc class=\"nav-item dropdown\">\r\n                <a b-bo2dbjxgkc class=\"nav-link dropdown-toggle\" href=\"#\" data-bs-toggle=\"dropdown\">\r\n                  שלום, ");
				Write(User.FindFirst("FullName")?.Value);
				WriteLiteral("\r\n                </a>\r\n                <ul b-bo2dbjxgkc class=\"dropdown-menu dropdown-menu-start\">\r\n                  <li b-bo2dbjxgkc>");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a67542", async delegate
				{
					WriteLiteral("שינוי סיסמה");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_31.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_31);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_32.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_32);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("</li>\r\n                  <li b-bo2dbjxgkc><hr b-bo2dbjxgkc class=\"dropdown-divider\"></li>\r\n                  <li b-bo2dbjxgkc>\r\n                    ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a69249", async delegate
				{
					WriteLiteral("\r\n                      ");
					Write(Html.AntiForgeryToken());
					WriteLiteral("\r\n                      <button b-bo2dbjxgkc type=\"submit\" class=\"dropdown-item\">יציאה</button>\r\n                    ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_31.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_31);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_33.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_33);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_34.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_34);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n                  </li>\r\n                </ul>\r\n              </li>\r\n              <li b-bo2dbjxgkc class=\"nav-item d-flex align-items-center\">\r\n                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a71805", async delegate
				{
					WriteLiteral("\r\n                  ");
					Write(Html.AntiForgeryToken());
					WriteLiteral("\r\n                  <button b-bo2dbjxgkc type=\"submit\" class=\"btn btn-light btn-sm app-logout-btn\">התנתק</button>\r\n                ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_31.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_31);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_33.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_33);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_34.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_34);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_35);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n              </li>\r\n            </ul>\r\n");
			}
			WriteLiteral("        </div>\r\n      </div>\r\n    </nav>\r\n  </header>\r\n  <div b-bo2dbjxgkc class=\"container\">\r\n");
			if (base.TempData["Success"] != null)
			{
				WriteLiteral("      <div b-bo2dbjxgkc class=\"alert alert-success alert-dismissible fade show\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\">\r\n        ");
				Write(base.TempData["Success"]);
				WriteLiteral("\r\n        <button b-bo2dbjxgkc type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n      </div>\r\n");
			}
			if (base.TempData["Error"] != null)
			{
				WriteLiteral("      <div b-bo2dbjxgkc class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\">\r\n        ");
				Write(base.TempData["Error"]);
				WriteLiteral("\r\n        <button b-bo2dbjxgkc type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n      </div>\r\n");
			}
			WriteLiteral("    <main b-bo2dbjxgkc id=\"main-content\" role=\"main\" class=\"pb-3\">\r\n      ");
			Write(RenderBody());
			WriteLiteral("\r\n    </main>\r\n  </div>\r\n\r\n  <footer b-bo2dbjxgkc class=\"border-top footer text-muted mt-4\">\r\n    <div b-bo2dbjxgkc class=\"container py-2 d-flex gap-3 flex-wrap\">\r\n      <span>&copy; 2026 סייט אנד סאונד - מערכת דיווח פעילות חודשית</span>\r\n      <a href=\"/Home/Privacy\">מדיניות פרטיות</a>\r\n    </div>\r\n  </footer>\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a77264", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_36);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a78387", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_37);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a79510", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_38);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a80633", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_39);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a81756", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_40);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a82879", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<ScriptTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_41.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_41);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", TagMode.StartTagAndEndTag, "2bfc404c8d83f4494e9d9eec8ebda3efc9c6d9cbaf038eb02a38fe6eb63a977a84905", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<UrlResolutionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<ScriptTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_42.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_42);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n  ");
			Write(await RenderSectionAsync("Scripts", required: false));
			WriteLiteral("\r\n  <script>\r\n    // Choices.js global init (UI/UX polish — AX-023, client-feedback #18).\r\n    // Attaches to every <select multiple> on the page so multi-selects render\r\n    // as tag chips with type-to-search, regardless of which view rendered them.\r\n    // Defensive: no-op if Choices.js asset hasn't loaded.\r\n    (function () {\r\n      if (typeof window.Choices === 'undefined') { return; }\r\n      document.querySelectorAll('select[multiple]').forEach(function (el) {\r\n        if (el.dataset.choicesInit) { return; }\r\n        try {\r\n          new window.Choices(el, {\r\n            removeItemButton: true,\r\n            searchPlaceholderValue: 'חיפוש…',\r\n            noResultsText: 'לא נמצאו תוצאות',\r\n            shouldSort: false,\r\n            itemSelectText: ''\r\n          });\r\n          el.dataset.choicesInit = '1';\r\n        } catch (e) { /* swallow — fallback to native widget */ }\r\n      });\r\n    })();\r\n  </script>\r\n");
		});
		__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<BodyTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n</html>\r\n");
	}
}
