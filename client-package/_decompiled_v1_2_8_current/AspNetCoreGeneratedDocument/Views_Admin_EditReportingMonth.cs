using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Web.Models;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/EditReportingMonth.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_EditReportingMonth : RazorPage<ReportingMonthEditViewModel>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "ReportingMonths", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("type", "hidden", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("form-label"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("class", new HtmlString("form-control"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("maxlength", new HtmlString("200"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("class", new HtmlString("text-danger"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("class", new HtmlString("form-select"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("type", "number", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("min", new HtmlString("2020"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("max", new HtmlString("2035"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("class", new HtmlString("btn btn-secondary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("asp-action", "EditReportingMonth", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_14 = new TagHelperAttribute("id", new HtmlString("editReportingMonthForm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_15 = new TagHelperAttribute("name", "_ValidationScriptsPartial", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;

	private LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;

	private ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;

	private SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;

	private OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;

	private PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;

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
	public IHtmlHelper<ReportingMonthEditViewModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "עריכת חודש דיווח";
		bool locked = base.Model.LockNonAdminFields;
		string lockTitle = "ניתן לשינוי רק ע&quot;י מנהל פרויקט או מנהל מערכת";
		WriteLiteral("\r\n<div class=\"container mt-3\" dir=\"rtl\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>עריכת חודש דיווח</h3>\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57210159", async delegate
		{
			WriteLiteral("חזרה");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n  </div>\r\n\r\n");
		if (base.Model.IsActive)
		{
			WriteLiteral("    <div class=\"alert alert-warning\" role=\"alert\" aria-live=\"polite\">\r\n      <strong>שים לב:</strong> זהו חודש דיווח <strong>פעיל</strong>.\r\n");
			if (locked)
			{
				WriteLiteral("        <span>שדות תאריך אחרון לדיווח ודיווח עתידי נעולים — ניתן לשינוי רק ע\"י מנהל פרויקט או מנהל מערכת.</span>\r\n");
			}
			else
			{
				WriteLiteral("        <span>שינויים בשדות אלה עלולים להשפיע על דיווחים פתוחים.</span>\r\n");
			}
			WriteLiteral("    </div>\r\n");
		}
		WriteLiteral("\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57212926", async delegate
		{
			WriteLiteral("\r\n    ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n    ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57213500", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, int>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.Id);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n    ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57215277", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, bool>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.IsActive);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n    ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57217060", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, bool>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.LockNonAdminFields);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n\r\n    <div class=\"card\">\r\n      <div class=\"card-body\">\r\n        <div class=\"mb-3\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57218958", async delegate
			{
				WriteLiteral("תיאור <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, string>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.Description);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57220708", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, string>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.Description);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
			BeginWriteTagHelperAttribute();
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57222781", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, string>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.Description);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        </div>\r\n\r\n        <div class=\"row\">\r\n          <div class=\"col-md-6 mb-3\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57224618", async delegate
			{
				WriteLiteral("חודש <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, int>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.Month);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", TagMode.StartTagAndEndTag, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57226363", async delegate
			{
				WriteLiteral("\r\n");
				int i;
				for (i = 1; i <= 12; i++)
				{
					WriteLiteral("                ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57226936", async delegate
					{
						Write(i);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(i);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
					AddHtmlAttributeValue("", 1899, base.Model.Month == i, 1899, 19, isLiteral: false);
					EndAddHtmlAttributeValues(__tagHelperExecutionContext);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n");
				}
				WriteLiteral("            ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<SelectTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, int>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.Month);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
			BeginWriteTagHelperAttribute();
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n          <div class=\"col-md-6 mb-3\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57231528", async delegate
			{
				WriteLiteral("שנה <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, int>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.Year);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57233271", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<ReportingMonthEditViewModel, int>(base.ViewData, (ReportingMonthEditViewModel __model) => __model.Year);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
			BeginWriteTagHelperAttribute();
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n        </div>\r\n\r\n        <div class=\"mb-3\">\r\n          <label class=\"form-label\" for=\"LastReportingDate\">\r\n            תאריך אחרון לדיווח <span class=\"text-danger\">*</span>\r\n");
			if (locked)
			{
				WriteLiteral("              <span aria-hidden=\"true\">\ud83d\udd12</span>\r\n");
			}
			WriteLiteral("          </label>\r\n");
			if (locked)
			{
				WriteLiteral("            <input type=\"date\" id=\"LastReportingDate\" class=\"form-control\"");
				BeginWriteAttribute("value", "\r\n                   value=\"", 2665, "\"", 2740, 1);
				WriteAttributeValue("", 2693, base.Model.LastReportingDate.ToString("yyyy-MM-dd"), 2693, 47, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral("\r\n                   disabled");
				BeginWriteAttribute("title", "\r\n                   title=\"", 2770, "\"", 2808, 1);
				WriteAttributeValue("", 2798, lockTitle, 2798, 10, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\r\n            <small class=\"text-muted\">השדה נעול — ניתן לשינוי רק ע\"י מנהל פרויקט או מנהל מערכת.</small>\r\n");
			}
			else
			{
				WriteLiteral("            <input type=\"date\" id=\"LastReportingDate\" name=\"LastReportingDate\" class=\"form-control\"");
				BeginWriteAttribute("value", "\r\n                   value=\"", 3060, "\"", 3135, 1);
				WriteAttributeValue("", 3088, base.Model.LastReportingDate.ToString("yyyy-MM-dd"), 3088, 47, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral("\r\n                   required />\r\n");
			}
			WriteLiteral("        </div>\r\n\r\n        <div class=\"mb-3 form-check\">\r\n");
			if (locked)
			{
				WriteLiteral("            <input type=\"checkbox\" id=\"allowFuture\" class=\"form-check-input\"");
				BeginWriteAttribute("checked", "\r\n                   checked=\"", 3353, "\"", 3410, 1);
				WriteAttributeValue("", 3383, base.Model.AllowFutureReporting, 3383, 27, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral("\r\n                   disabled");
				BeginWriteAttribute("title", "\r\n                   title=\"", 3440, "\"", 3478, 1);
				WriteAttributeValue("", 3468, lockTitle, 3468, 10, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\r\n");
			}
			else
			{
				WriteLiteral("            <input type=\"checkbox\" id=\"allowFuture\" name=\"AllowFutureReporting\" value=\"true\" class=\"form-check-input\"");
				BeginWriteAttribute("checked", "\r\n                   checked=\"", 3643, "\"", 3700, 1);
				WriteAttributeValue("", 3673, base.Model.AllowFutureReporting, 3673, 27, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\r\n");
			}
			WriteLiteral("          <label class=\"form-check-label\" for=\"allowFuture\">\r\n            אפשר דיווח עתידי\r\n");
			if (locked)
			{
				WriteLiteral("              <span aria-hidden=\"true\">\ud83d\udd12</span>\r\n");
			}
			WriteLiteral("          </label>\r\n");
			if (locked)
			{
				WriteLiteral("            <div><small class=\"text-muted\">השדה נעול — ניתן לשינוי רק ע\"י מנהל פרויקט או מנהל מערכת.</small></div>\r\n");
			}
			WriteLiteral("        </div>\r\n      </div>\r\n      <div class=\"card-footer d-flex justify-content-end gap-2\">\r\n        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57242656", async delegate
			{
				WriteLiteral("ביטול");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n      </div>\r\n    </div>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_12.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_13.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_14);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n</div>\r\n\r\n");
		DefineSection("Scripts", (RenderAsyncDelegate)async delegate
		{
			WriteLiteral("\r\n  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", TagMode.SelfClosing, "b326975d2528c658c4a9266f74b3e47b7d2a5f974ba312e7b50d068c14bce57245665", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<PartialTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_15.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			if (base.Model.IsActive && !base.Model.LockNonAdminFields)
			{
				WriteLiteral("    <script>\r\n      (function () {\r\n        var form = document.getElementById('editReportingMonthForm');\r\n        if (!form) return;\r\n        form.addEventListener('submit', function (e) {\r\n          if (!confirm('שינוי שדות חודש פעיל יכול להשפיע על דיווחים פתוחים. להמשיך?')) {\r\n            e.preventDefault();\r\n          }\r\n        });\r\n      })();\r\n    </script>\r\n");
			}
		});
	}
}
