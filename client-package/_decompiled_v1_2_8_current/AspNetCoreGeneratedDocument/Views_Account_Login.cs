using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Core.DTOs;
using Microsoft.AspNetCore.Html;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Account/Login.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Account_Login : RazorPage<LoginDto>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("class", new HtmlString("form-label"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("form-control"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("placeholder", new HtmlString("הזן מספר ת.ז"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("autocomplete", new HtmlString("username"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("aria-required", new HtmlString("true"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("type", "password", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("placeholder", new HtmlString("הזן סיסמה"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("autocomplete", new HtmlString("current-password"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("type", "checkbox", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("class", new HtmlString("form-check-input"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("class", new HtmlString("form-check-label"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("asp-action", "Login", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("asp-action", "ForgotPassword", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;

	private InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;

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
	public IHtmlHelper<LoginDto> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.Layout = "_AnonymousLayout";
		base.ViewData["Title"] = "כניסה למערכת";
		WriteLiteral("<div class=\"login-card-wrapper\">\r\n  <div class=\"card login-card\">\r\n    <div class=\"card-body p-4\">\r\n      <div class=\"login-logo\">\r\n        ");
		Write(await Component.InvokeAsync("SiteLogo", new
		{
			cssClass = "login-logo-img",
			alt = "סייט וסאונד חינוך"
		}));
		WriteLiteral("\r\n        <p class=\"text-muted mt-2 mb-0\">מערכת דיווח פעילות חודשית</p>\r\n      </div>\r\n");
		if (!base.ViewData.ModelState.IsValid)
		{
			WriteLiteral("        <div id=\"loginErrors\" class=\"alert alert-danger\" role=\"alert\"\r\n             aria-live=\"assertive\" aria-atomic=\"true\">\r\n");
			foreach (ModelError item in base.ViewData.ModelState.Values.SelectMany((ModelStateEntry v) => v.Errors))
			{
				WriteLiteral("            <div>");
				Write(item.ErrorMessage);
				WriteLiteral("</div>\r\n");
			}
			WriteLiteral("        </div>\r\n");
		}
		WriteLiteral("      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "daf4fafaf172437edf7e31540935963924e21ad71e2f58b8239cfffe4b815a5f10594", async delegate
		{
			WriteLiteral("\r\n        <input type=\"hidden\" name=\"returnUrl\"");
			BeginWriteAttribute("value", " value=\"", 1001, "\"", 1027, 1);
			WriteAttributeValue("", 1009, base.ViewBag.ReturnUrl, 1009, 18, false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"mb-3\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "daf4fafaf172437edf7e31540935963924e21ad71e2f58b8239cfffe4b815a5f11679", async delegate
			{
				WriteLiteral("תעודת זהות");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<LoginDto, string>(base.ViewData, (LoginDto __model) => __model.IdNumber);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "daf4fafaf172437edf7e31540935963924e21ad71e2f58b8239cfffe4b815a5f13383", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<LoginDto, string>(base.ViewData, (LoginDto __model) => __model.IdNumber);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        </div>\r\n        <div class=\"mb-3\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "daf4fafaf172437edf7e31540935963924e21ad71e2f58b8239cfffe4b815a5f15343", async delegate
			{
				WriteLiteral("סיסמה");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<LoginDto, string>(base.ViewData, (LoginDto __model) => __model.Password);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "daf4fafaf172437edf7e31540935963924e21ad71e2f58b8239cfffe4b815a5f17042", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<LoginDto, string>(base.ViewData, (LoginDto __model) => __model.Password);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        </div>\r\n        <div class=\"mb-3 form-check\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "daf4fafaf172437edf7e31540935963924e21ad71e2f58b8239cfffe4b815a5f19229", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<LoginDto, bool>(base.ViewData, (LoginDto __model) => __model.RememberMe);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "daf4fafaf172437edf7e31540935963924e21ad71e2f58b8239cfffe4b815a5f21096", async delegate
			{
				WriteLiteral("זכור אותי");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<LoginDto, bool>(base.ViewData, (LoginDto __model) => __model.RememberMe);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        </div>\r\n        <button type=\"submit\" class=\"btn btn-primary w-100\">כניסה</button>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_11.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_12.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
		BeginWriteTagHelperAttribute();
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__tagHelperExecutionContext.AddHtmlAttribute("novalidate", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
		BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "aria-describedby", 1, HtmlAttributeValueStyle.DoubleQuotes);
		AddHtmlAttributeValue("", 900, (!base.ViewData.ModelState.IsValid) ? "loginErrors" : "", 900, 52, isLiteral: false);
		EndAddHtmlAttributeValues(__tagHelperExecutionContext);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n      <div class=\"text-center mt-3\">\r\n        ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "daf4fafaf172437edf7e31540935963924e21ad71e2f58b8239cfffe4b815a5f25178", async delegate
		{
			WriteLiteral("שכחתי סיסמה");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_13.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n");
	}
}
