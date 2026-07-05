using System;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Account/ChangePassword.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Account_ChangePassword : RazorPage<ChangePasswordDto>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("class", new HtmlString("form-label"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("type", "password", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("class", new HtmlString("form-control"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("asp-action", "ChangePassword", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;

	private InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;

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
	public IHtmlHelper<ChangePasswordDto> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "שינוי סיסמה";
		bool flag = base.ViewBag.Forced ?? ((object)false);
		WriteLiteral("<div class=\"container mt-4\">\r\n  <div class=\"row justify-content-center\">\r\n    <div class=\"col-md-6\">\r\n      <div class=\"card\">\r\n        <div class=\"card-header\">\r\n          <h4>");
		Write(flag ? "נדרש שינוי סיסמה" : "שינוי סיסמה");
		WriteLiteral("</h4>\r\n");
		if (flag)
		{
			WriteLiteral("            <p class=\"text-muted mb-0\">עליך לשנות את סיסמתך לפני המשך השימוש במערכת.</p>\r\n");
		}
		WriteLiteral("        </div>\r\n        <div class=\"card-body\">\r\n");
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
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "66d8e3d5551cdf2d4a5d7ee4916cbf428f1af85de0222af59ccbeee139a8eba37983", async delegate
		{
			WriteLiteral("\r\n            ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n            <div class=\"mb-3\">\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "66d8e3d5551cdf2d4a5d7ee4916cbf428f1af85de0222af59ccbeee139a8eba38609", async delegate
			{
				WriteLiteral("סיסמה נוכחית");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<ChangePasswordDto, string>(base.ViewData, (ChangePasswordDto __model) => __model.CurrentPassword);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "66d8e3d5551cdf2d4a5d7ee4916cbf428f1af85de0222af59ccbeee139a8eba310334", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<ChangePasswordDto, string>(base.ViewData, (ChangePasswordDto __model) => __model.CurrentPassword);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            </div>\r\n            <div class=\"mb-3\">\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "66d8e3d5551cdf2d4a5d7ee4916cbf428f1af85de0222af59ccbeee139a8eba312277", async delegate
			{
				WriteLiteral("סיסמה חדשה (מינימום 8 תווים, אותיות וספרות)");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<ChangePasswordDto, string>(base.ViewData, (ChangePasswordDto __model) => __model.NewPassword);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "66d8e3d5551cdf2d4a5d7ee4916cbf428f1af85de0222af59ccbeee139a8eba314030", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<ChangePasswordDto, string>(base.ViewData, (ChangePasswordDto __model) => __model.NewPassword);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            </div>\r\n            <div class=\"mb-3\">\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "66d8e3d5551cdf2d4a5d7ee4916cbf428f1af85de0222af59ccbeee139a8eba315969", async delegate
			{
				WriteLiteral("אישור סיסמה חדשה");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<ChangePasswordDto, string>(base.ViewData, (ChangePasswordDto __model) => __model.ConfirmPassword);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "66d8e3d5551cdf2d4a5d7ee4916cbf428f1af85de0222af59ccbeee139a8eba317699", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<ChangePasswordDto, string>(base.ViewData, (ChangePasswordDto __model) => __model.ConfirmPassword);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            </div>\r\n            <button type=\"submit\" class=\"btn btn-primary\">שמור סיסמה</button>\r\n          ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-forced", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
		}
		BeginWriteTagHelperAttribute();
		WriteLiteral(flag);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["forced"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-forced", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["forced"], HtmlAttributeValueStyle.DoubleQuotes);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
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
