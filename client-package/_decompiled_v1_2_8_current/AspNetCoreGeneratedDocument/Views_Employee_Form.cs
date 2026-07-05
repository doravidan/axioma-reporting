using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Web.Helpers;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Employee/Form.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Employee_Form : RazorPage<EmployeeDto>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("class", new HtmlString("form-label"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("form-control"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("inputmode", new HtmlString("numeric"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("pattern", new HtmlString("\\d*"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("class", new HtmlString("text-danger small"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("data-val-israeliid", new HtmlString("מספר תעודת זהות אינו תקין"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("type", "email", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("data-val-israeliphone", new HtmlString("מספר טלפון אינו תקין"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("class", new HtmlString("form-select"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("type", "hidden", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("rows", new HtmlString("3"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_14 = new TagHelperAttribute("class", new HtmlString("form-check-input"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_15 = new TagHelperAttribute("type", "checkbox", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_16 = new TagHelperAttribute("id", new HtmlString("isReportingEmployee"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_17 = new TagHelperAttribute("class", new HtmlString("form-check-label"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_18 = new TagHelperAttribute("for", new HtmlString("isReportingEmployee"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_19 = new TagHelperAttribute("id", new HtmlString("allowFutureReporting"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_20 = new TagHelperAttribute("for", new HtmlString("allowFutureReporting"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_21 = new TagHelperAttribute("asp-action", "Allocations", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_22 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-success"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_23 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_24 = new TagHelperAttribute("asp-action", "UploadAttachment", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_25 = new TagHelperAttribute("enctype", new HtmlString("multipart/form-data"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_26 = new TagHelperAttribute("class", new HtmlString("row g-2 align-items-end mb-3"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_27 = new TagHelperAttribute("asp-action", "DeleteAttachment", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_28 = new TagHelperAttribute("onsubmit", new HtmlString("return confirm('למחוק את המסמך?')"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_29 = new TagHelperAttribute("style", new HtmlString("display:inline"), HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;

	private InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;

	private ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;

	private SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;

	private OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;

	private TextAreaTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper;

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
	public IHtmlHelper<EmployeeDto> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		bool isEdit = base.ViewBag.IsEdit == true;
		base.ViewData["Title"] = (isEdit ? "עריכת עובד" : "הוספת עובד");
		List<SelectListItem> restDays = SelectListProviders.RestDayOptionsWithSelection(base.Model.RestDay);
		bool isAdminOrPM = base.ViewBag.IsAdminOrPM == true;
		List<DocumentAttachment> attachments = (base.ViewBag.Attachments as List<DocumentAttachment>) ?? new List<DocumentAttachment>();
		WriteLiteral("\r\n<div class=\"container py-3\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>");
		Write(base.ViewData["Title"]);
		WriteLiteral("</h3>\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76315696", async delegate
		{
			WriteLiteral("חזרה לרשימה");
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
		if (base.TempData["Success"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\">\r\n      ");
			Write(base.TempData["Success"]);
			WriteLiteral("\r\n      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76317941", async delegate
		{
			WriteLiteral("\r\n    ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n\r\n    <div class=\"card border-primary\">\r\n      <div class=\"card-header bg-primary text-white fw-bold\">פרטי עובד</div>\r\n      <div class=\"card-body\">\r\n        <div class=\"row g-3\">\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76318753", async delegate
			{
				WriteLiteral("קוד עובד <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.EmployeeCode);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76320498", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.EmployeeCode);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			BeginWriteTagHelperAttribute();
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76322650", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.EmployeeCode);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76324443", async delegate
			{
				WriteLiteral("מספר זהות <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.IdNumber);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76326185", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.IdNumber);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			BeginWriteTagHelperAttribute();
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76328246", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.IdNumber);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76330035", async delegate
			{
				WriteLiteral("שם פרטי <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.FirstName);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76331776", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.FirstName);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
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
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76333751", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.FirstName);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76335541", async delegate
			{
				WriteLiteral("שם משפחה <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.LastName);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76337282", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.LastName);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
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
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76339256", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.LastName);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76341045", async delegate
			{
				WriteLiteral("דוא\"ל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.Email);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76342744", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.Email);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76344608", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.Email);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76346394", async delegate
			{
				WriteLiteral("טלפון");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.Phone);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76348092", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.Phone);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76349827", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.Phone);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76351613", async delegate
			{
				WriteLiteral("תפקיד <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.RoleId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76353349", async delegate
			{
				WriteLiteral("\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76353656", async delegate
				{
					WriteLiteral("-- בחר תפקיד --");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_10.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n            ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<SelectTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.RoleId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = base.ViewBag.EmployeeRoles;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76356916", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.RoleId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n");
			if (isAdminOrPM)
			{
				WriteLiteral("              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76358939", async delegate
				{
					WriteLiteral("תפקיד מערכת <span class=\"text-danger\">*</span>");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.UserRoleId);
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76360687", async delegate
				{
					WriteLiteral("\r\n                ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76360996", async delegate
					{
						WriteLiteral("-- בחר תפקיד מערכת --");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_10.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n              ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<SelectTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.UserRoleId);
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
				__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = base.ViewBag.UserRoles;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, HtmlAttributeValueStyle.DoubleQuotes);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76364266", async delegate
				{
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.UserRoleId);
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
			}
			else
			{
				WriteLiteral("              <label class=\"form-label\">תפקיד מערכת</label>\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76366310", async delegate
				{
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_12.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
				__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.UserRoleId);
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n              <input type=\"text\" class=\"form-control\" readonly");
				BeginWriteAttribute("value", "\r\n                     value=\"", 4529, "\"", 4707, 1);
				WriteAttributeValue("", 4559, (!(base.ViewBag.UserRoles is SelectList source)) ? "" : source.FirstOrDefault((SelectListItem x) => x.Value == base.Model.UserRoleId.ToString())?.Text, 4559, 148, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\r\n");
			}
			WriteLiteral("          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76369021", async delegate
			{
				WriteLiteral("סטטוס <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.StatusId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76370761", async delegate
			{
				WriteLiteral("\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76371068", async delegate
				{
					WriteLiteral("-- בחר סטטוס --");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_10.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n            ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<SelectTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.StatusId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = base.ViewBag.Statuses;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76374329", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int>(base.ViewData, (EmployeeDto __model) => __model.StatusId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76376120", async delegate
			{
				WriteLiteral("יום מנוחה");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int?>(base.ViewData, (EmployeeDto __model) => __model.RestDay);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76377826", async delegate
			{
				WriteLiteral("\r\n");
				foreach (SelectListItem day in restDays)
				{
					WriteLiteral("                ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76378390", async delegate
					{
						Write(day.Text);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(day.Value);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
					AddHtmlAttributeValue("", 5447, day.Selected, 5447, 13, isLiteral: false);
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
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int?>(base.ViewData, (EmployeeDto __model) => __model.RestDay);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76382565", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, int?>(base.ViewData, (EmployeeDto __model) => __model.RestDay);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-12\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76384353", async delegate
			{
				WriteLiteral("הערות");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.Notes);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("textarea", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76386053", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper = CreateTagHelper<TextAreaTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.Notes);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76387815", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, string>(base.ViewData, (EmployeeDto __model) => __model.Notes);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4 d-flex align-items-center gap-3 mt-2\">\r\n            <div class=\"form-check\">\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76389684", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, bool>(base.ViewData, (EmployeeDto __model) => __model.IsReportingEmployee);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_14);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_15.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_16);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76391657", async delegate
			{
				WriteLiteral("\r\n                עובד מדווח\r\n              ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, bool>(base.ViewData, (EmployeeDto __model) => __model.IsReportingEmployee);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_18);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            </div>\r\n            <div class=\"form-check\">\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76393569", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, bool>(base.ViewData, (EmployeeDto __model) => __model.AllowFutureReporting);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_14);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_15.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_19);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76395543", async delegate
			{
				WriteLiteral("\r\n                אפשר דיווח עתידי\r\n              ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<EmployeeDto, bool>(base.ViewData, (EmployeeDto __model) => __model.AllowFutureReporting);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_20);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            </div>\r\n          </div>\r\n\r\n");
			if (isEdit)
			{
				WriteLiteral("            <div class=\"col-12\">\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb76397711", async delegate
				{
					WriteLiteral("\r\n                ניהול הקצאות\r\n              ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_21.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_21);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(base.Model.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_22);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n            </div>\r\n");
			}
			WriteLiteral("\r\n        </div>\r\n      </div>\r\n      <div class=\"card-footer d-flex gap-2\">\r\n        <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb763100527", async delegate
			{
				WriteLiteral("ביטול");
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
			WriteLiteral("\r\n      </div>\r\n    </div>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_23.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_23);
		BeginWriteTagHelperAttribute();
		WriteLiteral(isEdit ? "Edit" : "Create");
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action, HtmlAttributeValueStyle.DoubleQuotes);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
		}
		BeginWriteTagHelperAttribute();
		WriteLiteral(base.Model.Id);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n\r\n");
		if (isEdit)
		{
			WriteLiteral("    <div class=\"card mt-3\">\r\n      <div class=\"card-header fw-bold\">מסמכי עובד</div>\r\n      <div class=\"card-body\">\r\n        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb763105083", async delegate
			{
				WriteLiteral("\n          ");
				Write(Html.AntiForgeryToken());
				WriteLiteral("\n          <div class=\"col-md-4\">\n            <label for=\"employeeAttachmentFile\" class=\"form-label\">קובץ</label>\n            <input type=\"file\" id=\"employeeAttachmentFile\" name=\"file\" class=\"form-control\" required />\n          </div>\n          <div class=\"col-md-6\">\n            <label for=\"employeeAttachmentDescription\" class=\"form-label\">תיאור</label>\n            <input type=\"text\" id=\"employeeAttachmentDescription\" name=\"description\" class=\"form-control\"\n                   maxlength=\"1000\" />\n          </div>\n          <div class=\"col-md-2\">\n            <button type=\"submit\" class=\"btn btn-outline-primary w-100\">העלה מסמך</button>\n          </div>\n        ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_24.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_24);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(base.Model.Id);
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_23.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_23);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_25);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_26);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n\r\n");
			if (attachments.Any())
			{
				WriteLiteral("          <div class=\"table-responsive\">\r\n            <table class=\"table table-sm align-middle\">\r\n              <thead>\r\n                <tr>\r\n                  <th>שם קובץ</th>\n                  <th>תיאור</th>\n                  <th>גודל</th>\n                  <th>תאריך העלאה</th>\r\n                  <th>פעולות</th>\r\n                </tr>\r\n              </thead>\r\n              <tbody>\r\n");
				foreach (DocumentAttachment file in attachments)
				{
					WriteLiteral("                  <tr>\r\n                    <td><a");
					BeginWriteAttribute("href", " href=\"", 8805, "\"", 8826, 1);
					WriteAttributeValue("", 8812, file.FilePath, 8812, 14, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" target=\"_blank\">");
					Write(file.FileName);
					WriteLiteral("</a></td>\n                    <td>");
					Write(string.IsNullOrWhiteSpace(file.Description) ? "-" : file.Description);
					WriteLiteral("</td>\n                    <td>");
					Write(Math.Round((decimal)file.FileSize / 1024m, 1));
					WriteLiteral(" KB</td>\n                    <td>");
					Write(file.UploadedAt.ToString("dd/MM/yyyy HH:mm"));
					WriteLiteral("</td>\r\n                    <td>\r\n                      ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "8e24b8ec7020c0d1f58d71a24fe5df2a54927b032d11f61c0c1549e5fdbeb763111494", async delegate
					{
						WriteLiteral("\r\n                        ");
						Write(Html.AntiForgeryToken());
						WriteLiteral("\r\n                        <input type=\"hidden\" name=\"attachmentId\"");
						BeginWriteAttribute("value", " value=\"", 9453, "\"", 9469, 1);
						WriteAttributeValue("", 9461, file.Id, 9461, 8, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" />\r\n                        <button type=\"submit\" class=\"btn btn-sm btn-outline-danger\">מחק</button>\r\n                      ");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
					__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
					__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_27.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_27);
					if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
					{
						throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
					}
					BeginWriteTagHelperAttribute();
					WriteLiteral(base.Model.Id);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
					__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_23.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_23);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_28);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_29);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n                    </td>\r\n                  </tr>\r\n");
				}
				WriteLiteral("              </tbody>\r\n            </table>\r\n          </div>\r\n");
			}
			else
			{
				WriteLiteral("          <div class=\"text-muted\">אין מסמכים לעובד.</div>\r\n");
			}
			WriteLiteral("      </div>\r\n    </div>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n");
		DefineSection("Scripts", (RenderAsyncDelegate)async delegate
		{
			WriteLiteral("\r\n  ");
			IHtmlContent value = await Html.PartialAsync("_ValidationScriptsPartial");
			Write(value);
			WriteLiteral("\r\n  <script>\r\n    // Show allocations link when IsReportingEmployee is checked (for new employee after save)\r\n    document.getElementById('isReportingEmployee').addEventListener('change', function () {\r\n      const hint = document.getElementById('allocationHint');\r\n      if (hint) hint.classList.toggle('d-none', !this.checked);\r\n    });\r\n  </script>\r\n");
		});
	}
}
