using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Core.DTOs;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Employee/AllocationForm.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Employee_AllocationForm : RazorPage<AllocationDto>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "Allocations", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("type", "hidden", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("form-label"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("id", new HtmlString("projectIdSelect"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("class", new HtmlString("form-select"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("class", new HtmlString("text-danger small"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("type", "number", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("step", new HtmlString("1"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("min", new HtmlString("0"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("inputmode", new HtmlString("numeric"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("pattern", new HtmlString("\\d*"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("class", new HtmlString("form-control whole-number-field"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_14 = new TagHelperAttribute("data-val-wholenumber", new HtmlString("יש להזין מספר שלם"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_15 = new TagHelperAttribute("id", new HtmlString("dailyScope"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_16 = new TagHelperAttribute("class", new HtmlString("form-check-input"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_17 = new TagHelperAttribute("type", "checkbox", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_18 = new TagHelperAttribute("class", new HtmlString("form-check-label"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_19 = new TagHelperAttribute("class", new HtmlString("form-control"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_20 = new TagHelperAttribute("rows", new HtmlString("2"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_21 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;

	private LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;

	private SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;

	private OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;

	private ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;

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
	public IHtmlHelper<AllocationDto> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		User employee = (User)base.ViewBag.Employee;
		bool isEdit = base.ViewBag.IsEdit == true;
		base.ViewData["Title"] = (isEdit ? "עריכת הקצאה" : "הוספת הקצאה");
		decimal[] durationOptions = (decimal[])base.ViewBag.OutputDurationOptions;
		List<District> districts = (List<District>)base.ViewBag.Districts;
		List<AxiomaReporting.Core.Entities.Program> programs = (List<AxiomaReporting.Core.Entities.Program>)base.ViewBag.Programs;
		List<Sector> sectors = (List<Sector>)base.ViewBag.Sectors;
		List<Locality> localities = (List<Locality>)base.ViewBag.Localities;
		List<Framework> frameworks = (List<Framework>)base.ViewBag.Frameworks;
		List<Subject> subjects = (List<Subject>)base.ViewBag.Subjects;
		List<Domain> domains = (List<Domain>)base.ViewBag.Domains;
		List<EducationalProgram> educationalPrograms = (List<EducationalProgram>)base.ViewBag.EducationalPrograms;
		List<SchoolClass> classes = (List<SchoolClass>)base.ViewBag.Classes;
		List<GradeLevel> gradeLevels = (List<GradeLevel>)base.ViewBag.GradeLevels;
		List<DiscussionCode> discussionCodes = (List<DiscussionCode>)base.ViewBag.DiscussionCodes;
		List<LocalityDistrictNational> localityDistrictNationals = (List<LocalityDistrictNational>)base.ViewBag.LocalityDistrictNationals;
		WriteLiteral("\r\n<div class=\"container-fluid py-3\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <div>\r\n      <h3>");
		Write(base.ViewData["Title"]);
		WriteLiteral("</h3>\r\n      <h5 class=\"text-muted\">");
		Write(employee.FirstName);
		WriteLiteral(" ");
		Write(employee.LastName);
		WriteLiteral(" — קוד: ");
		Write(employee.EmployeeCode);
		WriteLiteral("</h5>\r\n    </div>\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e14520", async delegate
		{
			WriteLiteral("חזרה להקצאות");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
		}
		BeginWriteTagHelperAttribute();
		WriteLiteral(employee.Id);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n  </div>\r\n\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e16808", async delegate
		{
			WriteLiteral("\r\n    ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n    ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e17381", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int>(base.ViewData, (AllocationDto __model) => __model.UserId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n    <input type=\"hidden\" id=\"allocationReportTypeId\" name=\"ReportTypeId\"");
			BeginWriteAttribute("value", " value=\"", 0, "\"", 0, 1);
			WriteAttributeValue("", 0, Model.ReportTypeId, 0, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />");
			WriteLiteral("\r\n\r\n    <!-- Basic allocation info -->\r\n    <div class=\"card mb-3 border-success\">\r\n      <div class=\"card-header bg-success text-white fw-bold\">פרטי הקצאה</div>\r\n      <div class=\"card-body\">\r\n        <div class=\"row g-3\">\r\n\r\n          <div class=\"col-md-6\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e19437", async delegate
			{
				WriteLiteral("פרויקט <span class=\"text-danger\">*</span>");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int>(base.ViewData, (AllocationDto __model) => __model.ProjectId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e21187", async delegate
			{
				WriteLiteral("\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e21494", async delegate
				{
					WriteLiteral("-- בחר פרויקט --");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
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
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int>(base.ViewData, (AllocationDto __model) => __model.ProjectId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = base.ViewBag.Projects;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e24857", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int>(base.ViewData, (AllocationDto __model) => __model.ProjectId);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-3\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e26657", async delegate
			{
				WriteLiteral("היקף פעילות שנתי");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, decimal?>(base.ViewData, (AllocationDto __model) => __model.AnnualEmploymentScope);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e28392", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, decimal?>(base.ViewData, (AllocationDto __model) => __model.AnnualEmploymentScope);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_14);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e30722", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, decimal?>(base.ViewData, (AllocationDto __model) => __model.AnnualEmploymentScope);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-3\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e32534", async delegate
			{
				WriteLiteral("היקף פעילות חודשי");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, decimal?>(base.ViewData, (AllocationDto __model) => __model.MonthlyEmploymentScope);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e34271", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, decimal?>(base.ViewData, (AllocationDto __model) => __model.MonthlyEmploymentScope);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_14);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e36602", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, decimal?>(base.ViewData, (AllocationDto __model) => __model.MonthlyEmploymentScope);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            <label class=\"form-label\">היקף פעילות יומי</label>\r\n            <div class=\"input-group\">\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e38528", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, decimal?>(base.ViewData, (AllocationDto __model) => __model.DailyEmploymentScope);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_15);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_14);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n              <div class=\"input-group-text\">\r\n                <input type=\"checkbox\" id=\"unlimitedDaily\" class=\"form-check-input mt-0\"\r\n                       ");
			Write((!base.Model.DailyEmploymentScope.HasValue) ? "checked" : "");
			WriteLiteral(" />\r\n                <label for=\"unlimitedDaily\" class=\"form-check-label ms-1\">ללא הגבלה</label>\r\n              </div>\r\n            </div>\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e41571", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, decimal?>(base.ViewData, (AllocationDto __model) => __model.DailyEmploymentScope);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n          </div>\n\n          <div class=\"col-md-4\">\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e43374", async delegate
			{
				WriteLiteral("הקצאת שורות חודשית");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int?>(base.ViewData, (AllocationDto __model) => __model.MonthlyRowAllocation);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e45110", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int?>(base.ViewData, (AllocationDto __model) => __model.MonthlyRowAllocation);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e47349", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int?>(base.ViewData, (AllocationDto __model) => __model.MonthlyRowAllocation);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-md-4\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e49160", async delegate
			{
				WriteLiteral("הקצאת שורות שנתית");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int?>(base.ViewData, (AllocationDto __model) => __model.AnnualRowAllocation);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e50894", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int?>(base.ViewData, (AllocationDto __model) => __model.AnnualRowAllocation);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e53132", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<ValidationMessageTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, int?>(base.ViewData, (AllocationDto __model) => __model.AnnualRowAllocation);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n          <div class=\"col-12\">\r\n            <label class=\"form-label\">משך תפוקה (ניתן לבחור מספר ערכים)</label>\r\n            <div class=\"d-flex flex-wrap gap-3\">\r\n");
			decimal[] array = durationOptions;
			for (int i = 0; i < array.Length; i++)
			{
				decimal num = array[i];
				WriteLiteral("                <div class=\"form-check\">\r\n                  <input type=\"checkbox\" name=\"OutputDurationValues\"");
				BeginWriteAttribute("value", " value=\"", 5742, "\"", 5754, 1);
				WriteAttributeValue("", 5750, num, 5750, 4, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral("\r\n                         class=\"form-check-input\"");
				BeginWriteAttribute("id", "\r\n                         id=\"", 5806, "\"", 5874, 2);
				WriteAttributeValue("", 5837, "dur_", 5837, 4, isLiteral: true);
				WriteAttributeValue("", 5841, num.ToString().Replace(".", "_"), 5841, 33, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral("\r\n                         ");
				Write(base.Model.OutputDurationValues.Contains(num) ? "checked" : "");
				WriteLiteral(" />\r\n                  <label class=\"form-check-label\"");
				BeginWriteAttribute("for", "\r\n                         for=\"", 6016, "\"", 6085, 2);
				WriteAttributeValue("", 6048, "dur_", 6048, 4, isLiteral: true);
				WriteAttributeValue("", 6052, num.ToString().Replace(".", "_"), 6052, 33, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">");
				Write(num);
				WriteLiteral("</label>\r\n                </div>\r\n");
			}
			WriteLiteral("            </div>\r\n          </div>\r\n\r\n          <div class=\"col-12\">\r\n            <div class=\"form-check\">\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", TagMode.SelfClosing, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e58085", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<InputTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, bool>(base.ViewData, (AllocationDto __model) => __model.AllowExcelUpload);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_16);
			__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_17.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_17);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e59977", async delegate
			{
				WriteLiteral("אפשר העלאת אקסל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, bool>(base.ViewData, (AllocationDto __model) => __model.AllowExcelUpload);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_18);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            </div>\r\n          </div>\r\n\r\n          <div class=\"col-12\">\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e61791", async delegate
			{
				WriteLiteral("הערות");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<LabelTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, string>(base.ViewData, (AllocationDto __model) => __model.Notes);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("textarea", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e63501", async delegate
			{
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper = CreateTagHelper<TextAreaTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For = ModelExpressionProvider.CreateModelExpression<AllocationDto, string>(base.ViewData, (AllocationDto __model) => __model.Notes);
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For, HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_19);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_20);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          </div>\r\n\r\n        </div>\r\n      </div>\r\n    </div>\r\n\r\n    <!-- Multi-select fields -->\r\n    <div class=\"card mb-3\">\r\n      <div class=\"card-header fw-bold\">שיוכים</div>\r\n      <div class=\"card-body\">\r\n        <div class=\"row g-3\">\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">מחוזות</label>\r\n            <select name=\"DistrictIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (District item12 in districts)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e65969", async delegate
				{
					Write(item12.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item12.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 7217, base.Model.DistrictIds.Contains(item12.Id), 7217, 38, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\" for=\"programIdsSelect\">תוכניות</label>\r\n            <select name=\"ProgramIds\" id=\"programIdsSelect\" multiple class=\"form-select\" size=\"5\"\r\n                    data-initial-selected=\"");
			Write(string.Join(",", base.Model.ProgramIds));
			WriteLiteral("\"\r\n                    aria-describedby=\"programsHelp\">\r\n");
			foreach (AxiomaReporting.Core.Entities.Program item11 in programs)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e69629", async delegate
				{
					Write(item11.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item11.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 7831, base.Model.ProgramIds.Contains(item11.Id), 7831, 37, isLiteral: false);
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
			WriteLiteral("            </select>\r\n            <small id=\"programsHelp\" class=\"form-text text-muted\">\r\n              התוכניות יסוננו לפי הפרויקט הנבחר. אם לא הוגדרו תוכניות לפרויקט, הרשימה תישאר ריקה.\r\n            </small>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">מגזרים</label>\r\n            <select name=\"SectorIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (Sector item10 in sectors)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e73018", async delegate
				{
					Write(item10.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item10.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 8439, base.Model.SectorIds.Contains(item10.Id), 8439, 36, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">ישובים</label>\r\n            <select name=\"LocalityIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (Locality item9 in localities)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e76241", async delegate
				{
					Write(item9.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item9.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 8874, base.Model.LocalityIds.Contains(item9.Id), 8874, 38, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">מסגרות</label>\r\n            <select name=\"FrameworkIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (Framework item8 in frameworks)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e79467", async delegate
				{
					Write(item8.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item8.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 9312, base.Model.FrameworkIds.Contains(item8.Id), 9312, 39, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">נושאים</label>\r\n            <select name=\"SubjectIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (Subject item7 in subjects)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e82690", async delegate
				{
					Write(item7.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item7.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 9747, base.Model.SubjectIds.Contains(item7.Id), 9747, 37, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">תחומים</label>\r\n            <select name=\"DomainIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (Domain item6 in domains)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e85909", async delegate
				{
					Write(item6.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item6.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 10178, base.Model.DomainIds.Contains(item6.Id), 10178, 36, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">תוכניות חינוכיות</label>\r\n            <select name=\"EducationalProgramIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (EducationalProgram item5 in educationalPrograms)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e89163", async delegate
				{
					Write(item5.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item5.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 10642, base.Model.EducationalProgramIds.Contains(item5.Id), 10642, 48, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">כיתות</label>\r\n            <select name=\"ClassIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (SchoolClass item4 in classes)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e92394", async delegate
				{
					Write(item4.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item4.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 11082, base.Model.ClassIds.Contains(item4.Id), 11082, 35, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">שכבות</label>\r\n            <select name=\"GradeLevelIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (GradeLevel item3 in gradeLevels)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e95620", async delegate
				{
					Write(item3.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item3.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 11518, base.Model.GradeLevelIds.Contains(item3.Id), 11518, 40, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">קיום דיון</label>\r\n            <select name=\"DiscussionCodeIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (DiscussionCode item2 in discussionCodes)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e98863", async delegate
				{
					Write(item2.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item2.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 11971, base.Model.DiscussionCodeIds.Contains(item2.Id), 11971, 44, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-md-6\">\r\n            <label class=\"form-label\">ישוב/מחוז/ארצי</label>\r\n            <select name=\"LocalityDistrictNationalIds\" multiple class=\"form-select\" size=\"5\">\r\n");
			foreach (LocalityDistrictNational item in localityDistrictNationals)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e102135", async delegate
				{
					Write(item.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 12453, base.Model.LocalityDistrictNationalIds.Contains(item.Id), 12453, 54, isLiteral: false);
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
			WriteLiteral("            </select>\r\n          </div>\r\n\r\n        </div>\r\n        <p class=\"text-muted small mt-2\">לבחירה מרובה יש להחזיק את מקש הבחירה המרובה במקלדת וללחוץ על הפריטים.</p>\n      </div>\r\n    </div>\r\n\r\n    <div class=\"d-flex gap-2\">\r\n      <button type=\"submit\" class=\"btn btn-primary\">שמור הקצאה</button>\r\n");
			if (!isEdit)
			{
				WriteLiteral("        <button type=\"submit\" name=\"continueAdding\" value=\"true\" class=\"btn btn-success\">שמור והוסף עוד</button>\n");
			}
			WriteLiteral("      ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "35933c2532bf835af453fd16d451db0a6a8e4f67c83ec22448e889e374de351e105765", async delegate
			{
				WriteLiteral("ביטול");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(employee.Id);
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n    </div>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_21.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_21);
		BeginWriteTagHelperAttribute();
		WriteLiteral(isEdit ? "EditAllocation" : "CreateAllocation");
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action, HtmlAttributeValueStyle.DoubleQuotes);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
		}
		BeginWriteTagHelperAttribute();
		WriteLiteral(employee.Id);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(base.Model.Id);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["allocationId"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-allocationId", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["allocationId"], HtmlAttributeValueStyle.DoubleQuotes);
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
			IHtmlContent value = await Html.PartialAsync("_ValidationScriptsPartial");
			Write(value);
			WriteLiteral("\r\n  <script>\r\n    (function () {\r\n      if (typeof window.Choices === 'undefined') return;\r\n      const frameworkSelect = document.querySelector('select[name=\"FrameworkIds\"]');\r\n      if (!frameworkSelect || frameworkSelect.dataset.choicesInit) return;\r\n      try {\r\n        const frameworkChoices = new window.Choices(frameworkSelect, {\r\n          removeItemButton: true,\r\n          searchPlaceholderValue: 'חפש לפי יישוב, סמל מוסד או שם מסגרת',\r\n          noResultsText: 'לא נמצאו תוצאות',\r\n          shouldSort: false,\r\n          itemSelectText: ''\r\n        });\r\n        frameworkSelect.choicesInstance = frameworkChoices;\r\n        const frameworkInput = frameworkChoices.input && frameworkChoices.input.element;\r\n        if (frameworkInput) frameworkInput.setAttribute('placeholder', 'חפש לפי יישוב, סמל מוסד או שם מסגרת');\r\n        frameworkSelect.dataset.choicesInit = '1';\r\n      } catch (e) { }\r\n    })();\r\n  </script>\r\n  <script>\r\n    // Toggle daily scope input when \"unlimited\" checkbox is clicked\r\n    const unlimitedDaily = document.getElementById('unlimitedDaily');\r\n    const dailyScope = document.getElementById('dailyScope');\r\n\r\n    function toggleDailyScope() {\r\n      if (unlimitedDaily.checked) {\r\n        dailyScope.value = '';\r\n        dailyScope.disabled = true;\r\n      } else {\r\n        dailyScope.disabled = false;\r\n      }\r\n    }\r\n\r\n    unlimitedDaily.addEventListener('change', toggleDailyScope);\n    toggleDailyScope(); // initialize on page load\n\n    document.querySelectorAll('.whole-number-field').forEach((input) => {\n      input.addEventListener('input', () => {\n        input.value = input.value.replace(/[^\\d]/g, '');\n      });\n      input.addEventListener('wheel', () => input.blur());\n    });\n\n    // --- Cascading Project -> Programs ---\n    const projectSelect = document.getElementById('projectIdSelect');\r\n    const programsSelect = document.getElementById('programIdsSelect');\r\n    const programsUrl = '");
			Write(Url.Action("ProgramsForProject", "Employee"));
			WriteLiteral("';\r\n\r\n    async function reloadPrograms(projectId) {\r\n      if (!projectSelect || !programsSelect) return;\r\n      const previouslySelected = new Set(Array.from(programsSelect.selectedOptions).map(o => o.value));\r\n\r\n      if (!projectId) {\r\n        programsSelect.innerHTML = '';\r\n        programsSelect.dispatchEvent(new Event('change', { bubbles: true }));\r\n        return;\r\n      }\r\n\r\n      try {\r\n        const resp = await fetch(programsUrl + '?projectId=' + encodeURIComponent(projectId), {\r\n          headers: { 'Accept': 'application/json' }\r\n        });\r\n        if (!resp.ok) throw new Error('network');\r\n        const data = await resp.json();\r\n\r\n        programsSelect.innerHTML = '';\r\n        for (const item of data) {\r\n          const opt = document.createElement('option');\r\n          opt.value = item.id;\r\n          opt.textContent = item.description;\r\n          opt.selected = previouslySelected.has(String(item.id));\r\n          programsSelect.appendChild(opt);\r\n        }\r\n        programsSelect.dispatchEvent(new Event('change', { bubbles: true }));\r\n      } catch (err) {\r\n        // Graceful failure: lea");
			WriteLiteral("ve current options in place so the user isn't stuck.\r\n        console.warn('ProgramsForProject fetch failed:', err);\r\n      }\r\n    }\r\n\r\n    const scopedLookupsUrl = '/allocations/ScopedLookups';\r\n    const scopedSelectNames = ['SubjectIds','DomainIds','FrameworkIds','EducationalProgramIds','DiscussionCodeIds','GradeLevelIds','ClassIds'];\r\n    const scopedKeys = { SubjectIds: 'subjects', DomainIds: 'domains', FrameworkIds: 'frameworks', EducationalProgramIds: 'educationalPrograms', DiscussionCodeIds: 'discussionCodes', GradeLevelIds: 'gradeLevels', ClassIds: 'classes' };\r\n\r\n    function rebuildScopedSelect(select, items, selectAll) {\r\n      select.innerHTML = '';\r\n      for (const item of items || []) {\r\n        const opt = document.createElement('option');\r\n        opt.value = item.id;\r\n        opt.textContent = item.description || item.text || '';\r\n        opt.selected = !!selectAll;\r\n        select.appendChild(opt);\r\n      }\r\n      const choicesInstance = select.choicesInstance;\r\n      if (choicesInstance && typeof choicesInstance.clearStore === 'function' && typeof choicesInstance.setChoices === 'function') {\r\n        const choices = Array.from(select.options).map(opt => ({ value: opt.value, label: opt.textContent || '', selected: opt.selected, disabled: opt.disabled }));\r\n        choicesInstance.clearStore();\r\n        choicesInstance.setChoices(choices, 'value', 'label', true);\r\n        if (selectAll && typeof choicesInstance.setChoiceByValue === 'function') {\r\n          choicesInstance.setChoiceByValue(choices.map(choice => choice.value));\r\n        }\r\n      }\r\n    }\r\n\r\n    async function reloadScopedLookups() {\r\n      const selects = scopedSelectNames.map(name => document.querySelector('select[name=\"' + name + '\"]')).filter(Boolean);\r\n      const programIds = Array.from(programsSelect?.selectedOptions || []).map(o => o.value).join(',');\r\n      if (!projectSelect || !projectSelect.value || !programIds) {\r\n        selects.forEach(s => {\r\n          s.innerHTML = '';\r\n          const choicesInstance = s.choicesInstance;\r\n          if (choicesInstance && typeof choicesInstance.clearStore === 'function') choicesInstance.clearStore();\r\n        });\r\n        return;\r\n      }\r\n      try {\r\n        selects.forEach(s => s.disabled = true);\r\n        const url = scopedLookupsUrl + '?projectId=' + encodeURIComponent(projectSelect.value) + '&programIds=' + encodeURIComponent(programIds);\r\n        const resp = await fetch(url, { headers: { 'Accept': 'application/json' } });\r\n        if (!resp.ok) throw new Error('network');\r\n        const data = await resp.json();\r\n        for (const name of scopedSelectNames) {\r\n          const select = document.querySelector('select[name=\"' + name + '\"]');\r\n          if (select) rebuildScopedSelect(select, data[scopedKeys[name]], true);\r\n        }\r\n      } catch (err) {\r\n        console.warn('ScopedLookups fetch failed:', err);\r\n      } finally {\r\n        selects.forEach(s => s.disabled = false);\r\n      }\r\n    }\r\n\r\n    if (projectSelect) {\r\n      projectSelect.addEventListener('change', async (e) => {\r\n        await reloadPrograms(e.target.value);\r\n      });\r\n    }\r\n    if (programsSelect) {\r\n      programsSelect.addEventListener('change', reloadScopedLookups);\r\n    }\r\n    if (!projectSelect?.value && programsSelect) programsSelect.innerHTML = '';\r\n  </script>\r\n\r\n");
			WriteLiteral("\r\n  <script>\r\n    (function () {\r\n      const autoSelectAllocationDefaults = ");
			Write("false");
			WriteLiteral(";\r\n      const projectSelect = document.getElementById('projectIdSelect');\r\n      const programsSelect = document.getElementById('programIdsSelect');\r\n      const programsUrl = '");
			Write(Url.Action("ProgramsForProject", "Employee"));
			WriteLiteral("';\r\n      const scopedLookupsUrl = '/allocations/ScopedLookups';\r\n      const scopedSelectNames = ['SubjectIds','DomainIds','FrameworkIds','EducationalProgramIds','DiscussionCodeIds','GradeLevelIds','ClassIds'];\r\n      const scopedKeys = { SubjectIds: 'subjects', DomainIds: 'domains', FrameworkIds: 'frameworks', EducationalProgramIds: 'educationalPrograms', DiscussionCodeIds: 'discussionCodes', GradeLevelIds: 'gradeLevels', ClassIds: 'classes' };\r\n\r\n      if (!autoSelectAllocationDefaults || !projectSelect || !programsSelect) return;\r\n\r\n      if (window.Choices && !window.Choices.__allocationStoreWrapped) {\r\n        const OriginalChoices = window.Choices;\r\n        const WrappedChoices = function () {\r\n          const instance = Reflect.construct(OriginalChoices, arguments);\r\n          if (arguments[0]) arguments[0].choicesInstance = instance;\r\n          return instance;\r\n        };\r\n        WrappedChoices.prototype = OriginalChoices.prototype;\r\n        Object.keys(OriginalChoices).forEach(k => { WrappedChoices[k] = OriginalChoices[k]; });\r\n        WrappedChoices.__allocationStoreWrapped = true;\r\n        window.Choices = WrappedChoices;\r\n      }\r\n\r\n      function toChoice(item) {\r\n        return { value: String(item.id), label: item.description || item.text || '', selected: true, disabled: false };\r\n      }\r\n\r\n      function setMultipleOptions(select, items) {\r\n        const choices = Array.from(items || []).map(toChoice);\r\n        select.innerHTML = '';\r\n        for (const choice of choices) {\r\n          const opt = document.createElement('option');\r\n          opt.value = choice.value;\r\n          opt.textContent = choice.label;\r\n          opt.selected = true;\r\n          select.appendChild(opt);\r\n        }\r\n\r\n        const choicesInstance = select.choicesInstance;\r\n        if (choicesInstance && typeof choicesInstance.clearStore === 'function' && typeof choicesInstance.setChoices === 'function') {\r\n          choicesInstance.clearStore();\r\n          choicesInstance.setChoices(choices, 'value', 'label', true);\r\n          if (typeof choicesInstance.setChoiceByValue === 'function') {\r\n            choicesInstance.setChoiceByValue(choices.map(choice => choice.value));\r\n          }\r\n        }\r\n\r\n        select.dispatchEvent(new Event('change', { bubbles: true }));\r\n      }\r\n\r\n      async function fetchJson(url) {\r\n        const resp = await fetch(url, { headers: { 'Accept': 'application/json' } });\r\n        if (!resp.ok) throw new Error('network');\r\n        return resp.json();\r\n      }\r\n\r\n      async function selectAllScopedLookups() {\r\n        if (!projectSelect.value) return;\r\n        const programIds = Array.from(programsSelect.selectedOptions || []).map(o => o.value).join(',');\r\n        const url = scopedLookupsUrl + '?projectId=' + encodeURIComponent(projectSelect.value) + '&programIds=' + encodeURIComponent(programIds);\r\n        const data = await fetchJson(url);\r\n        for (const name of scopedSelectNames) {\r\n          const select = document.querySelector('select[name=\"' + name + '\"]');\r\n          if (select) setMultipleOptions(select, data[scopedKeys[name]] || []);\r\n        }\r\n      }\r\n\r\n      projectSelect.addEventListener('change', async function () {\r\n        if (!projectSelect.value) return;\r\n        try {\r\n          const programs = await fetchJson(programsUrl + '?projectId=' + encodeURIComponent(projectSelect.value));\r\n          setMultipleOptions(programsSelect, programs);\r\n          await selectAllScopedLookups();\r\n        } catch (err) {\r\n          console.warn('Allocation defaults fetch failed:', err);\r\n        }\r\n      });\r\n\r\n      programsSelect.addEventListener('change', async function () {\r\n        if (!projectSelect.value) return;\r\n        try {\r\n          await selectAllScopedLookups();\r\n        } catch (err) {\r\n          console.warn('Allocation scoped defaults fetch failed:', err);\r\n        }\r\n      });\r\n    })();\r\n  </script>\r\n");

		});
	}
}





