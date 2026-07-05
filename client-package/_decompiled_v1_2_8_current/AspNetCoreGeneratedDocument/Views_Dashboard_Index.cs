using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Services;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Dashboard/Index.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Dashboard_Index : RazorPage<dynamic>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "Summary", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-primary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("value", "0", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("value", "1", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("value", "2", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("value", "3", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("value", "4", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("value", "5", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("method", "get", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("id", new HtmlString("filterForm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("class", new HtmlString("btn btn-sm btn-outline-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("asp-controller", "Report", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

	private OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;

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
		base.ViewData["Title"] = "דשבורד דיווחים";
		DashboardFilter filter = (base.ViewBag.Filter as DashboardFilter) ?? new DashboardFilter();
		List<DashboardReportDetailRow> rows = (base.ViewBag.Rows as List<DashboardReportDetailRow>) ?? new List<DashboardReportDetailRow>();
		List<District> districts = (base.ViewBag.Districts as List<District>) ?? new List<District>();
		List<Sector> sectors = (base.ViewBag.Sectors as List<Sector>) ?? new List<Sector>();
		List<AxiomaReporting.Core.Entities.Program> programs = (base.ViewBag.Programs as List<AxiomaReporting.Core.Entities.Program>) ?? new List<AxiomaReporting.Core.Entities.Program>();
		List<ReportingMonth> reportingMonths = (base.ViewBag.ReportingMonths as List<ReportingMonth>) ?? new List<ReportingMonth>();
		List<Locality> localities = (base.ViewBag.Localities as List<Locality>) ?? new List<Locality>();
		List<Framework> frameworks = (base.ViewBag.Frameworks as List<Framework>) ?? new List<Framework>();
		List<EducationalProgram> educationalPrograms = (base.ViewBag.EducationalPrograms as List<EducationalProgram>) ?? new List<EducationalProgram>();
		List<Domain> domains = (base.ViewBag.Domains as List<Domain>) ?? new List<Domain>();
		List<Subject> subjects = (base.ViewBag.Subjects as List<Subject>) ?? new List<Subject>();
		List<DiscussionCode> discussionCodes = (base.ViewBag.DiscussionCodes as List<DiscussionCode>) ?? new List<DiscussionCode>();
		List<SchoolClass> classes = (base.ViewBag.Classes as List<SchoolClass>) ?? new List<SchoolClass>();
		List<GradeLevel> gradeLevels = (base.ViewBag.GradeLevels as List<GradeLevel>) ?? new List<GradeLevel>();
		List<LocalityDistrictNational> conclusionLocations = (base.ViewBag.ConclusionLocations as List<LocalityDistrictNational>) ?? new List<LocalityDistrictNational>();
		List<ReportType> reportTypes = (base.ViewBag.ReportTypes as List<ReportType>) ?? new List<ReportType>();
		int total = base.ViewBag.TotalCount ?? ((object)0);
		bool showEditColumn = base.ViewBag.CanEditDashboardRows ?? ((object)false);
		WriteLiteral("\n<div class=\"container-fluid mt-3\">\n    <div class=\"d-flex justify-content-between align-items-center mb-3\">\n        <h3>דשבורד דיווחים</h3>\n        <div class=\"d-flex gap-2\">\n            <a");
		BeginWriteAttribute("href", " href=\"", 3888, "\"", 3937, 1);
		WriteAttributeValue("", 3895, Url.Action("ExportExcel", RouteValues(1)), 3895, 42, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(" class=\"btn btn-outline-success btn-sm\">ייצוא אקסל</a>\n            ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c12700", async delegate
		{
			WriteLiteral("מסך סיכום ואישור");
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
		WriteLiteral("\n        </div>\n    </div>\n\n");
		if (base.TempData["Success"] != null)
		{
			WriteLiteral("        <div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\">\n            ");
			Write(base.TempData["Success"]);
			WriteLiteral("\n            <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\n        </div>\n");
		}
		if (base.TempData["Error"] != null)
		{
			WriteLiteral("        <div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">\n            ");
			Write(base.TempData["Error"]);
			WriteLiteral("\n            <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\n        </div>\n");
		}
		WriteLiteral("\n    <div class=\"card mb-3\">\n        <div class=\"card-header\"><strong>סינון דיווחים</strong></div>\n        <div class=\"card-body\">\n            ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c15984", async delegate
		{
			WriteLiteral("\n                <input type=\"hidden\" name=\"show\" value=\"1\" />\n                <input type=\"hidden\" name=\"SortBy\"");
			BeginWriteAttribute("value", " value=\"", 4976, "\"", 4998, 1);
			WriteAttributeValue("", 4984, filter.SortBy, 4984, 14, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\n                <input type=\"hidden\" name=\"SortDesc\"");
			BeginWriteAttribute("value", " value=\"", 5055, "\"", 5079, 1);
			WriteAttributeValue("", 5063, filter.SortDesc, 5063, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\n");
			if (base.User.IsInRole("1") || base.User.IsInRole("2") || base.User.IsInRole("3"))
			{
				WriteLiteral("<div class=\"form-check form-switch mb-2\"><input class=\"form-check-input\" type=\"checkbox\" name=\"IncludeArchived\" value=\"true\" id=\"includeArchived\"");
				if (filter.IncludeArchived)
				{
					WriteLiteral(" checked");
				}
				WriteLiteral(" /><label class=\"form-check-label\" for=\"includeArchived\">הצג דיווחים בארכיון</label></div>");
			}
			WriteLiteral("\n                <div class=\"row g-2\">\n                    <div class=\"col-md-1\">\n                        <label class=\"form-label form-label-sm\">ת.ז</label>\n                        <input name=\"IdNumber\" class=\"form-control form-control-sm\"");
			BeginWriteAttribute("value", " value=\"", 5324, "\"", 5348, 1);
			WriteAttributeValue("", 5332, filter.IdNumber, 5332, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\n                    </div>\n                    <div class=\"col-md-1\">\n                        <label class=\"form-label form-label-sm\">קוד עובד</label>\n                        <input name=\"EmployeeCode\" class=\"form-control form-control-sm\"");
			BeginWriteAttribute("value", " value=\"", 5591, "\"", 5619, 1);
			WriteAttributeValue("", 5599, filter.EmployeeCode, 5599, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\n                    </div>\n                    <div class=\"col-md-2\">\n                        <label class=\"form-label form-label-sm\">שם מדווח</label>\n                        <input name=\"EmployeeName\" class=\"form-control form-control-sm\"");
			BeginWriteAttribute("value", " value=\"", 5862, "\"", 5890, 1);
			WriteAttributeValue("", 5870, filter.EmployeeName, 5870, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\n                    </div>\n                    <div class=\"col-md-1\">\n                        <label class=\"form-label form-label-sm\">מחודש</label>\n                        <select name=\"FromMonthId\" class=\"form-select form-select-sm\">\n                            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c19619", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n");
			foreach (ReportingMonth k in reportingMonths)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c21107", async delegate
				{
					Write(k.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(k.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 6286, (filter.FromMonthId == k.Id) ? "selected" : null, 6286, 49, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                        </select>\n                    </div>\n                    <div class=\"col-md-1\">\n                        <label class=\"form-label form-label-sm\">עד חודש</label>\n                        <select name=\"ToMonthId\" class=\"form-select form-select-sm\">\n                            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c24087", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n");
			foreach (ReportingMonth j in reportingMonths)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c25575", async delegate
				{
					Write(j.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(j.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 6788, (filter.ToMonthId == j.Id) ? "selected" : null, 6788, 47, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                        </select>\n                    </div>\n                    <div class=\"col-md-1\">\n                        <label class=\"form-label form-label-sm\">סטטוס</label>\n                        <select name=\"StatusId\" class=\"form-select form-select-sm\">\n                            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c28550", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n                            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c29821", async delegate
			{
				WriteLiteral("טרם דווח");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7243, (filter.StatusId == 0) ? "selected" : null, 7243, 43, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n                            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c31642", async delegate
			{
				WriteLiteral("טיוטה");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7362, (filter.StatusId.GetValueOrDefault() == 1) ? "selected" : null, 7362, 43, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n                            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c33460", async delegate
			{
				WriteLiteral("בהזנה");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7478, (filter.StatusId.GetValueOrDefault() == 2) ? "selected" : null, 7478, 43, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n                            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c35278", async delegate
			{
				WriteLiteral("ממתין לאישור");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7594, (filter.StatusId.GetValueOrDefault() == 3) ? "selected" : null, 7594, 43, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n                            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c37103", async delegate
			{
				WriteLiteral("מאושר");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_7.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7717, (filter.StatusId.GetValueOrDefault() == 4) ? "selected" : null, 7717, 43, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n                            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c38921", async delegate
			{
				WriteLiteral("הוחזר לתיקון");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7833, (filter.StatusId.GetValueOrDefault() == 5) ? "selected" : null, 7833, 43, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n                        </select>\n                    </div>\n                    <div class=\"col-md-1\">\n                        <label class=\"form-label form-label-sm\">מתאריך</label>\n                        <input name=\"MeetingDateFrom\" type=\"date\" class=\"form-control form-control-sm\"");
			BeginWriteAttribute("value", " value=\"", 8185, "\"", 8240, 1);
			WriteAttributeValue("", 8193, filter.MeetingDateFrom?.ToString("yyyy-MM-dd"), 8193, 47, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\n                    </div>\n                    <div class=\"col-md-1\">\n                        <label class=\"form-label form-label-sm\">עד תאריך</label>\n                        <input name=\"MeetingDateTo\" type=\"date\" class=\"form-control form-control-sm\"");
			BeginWriteAttribute("value", " value=\"", 8496, "\"", 8549, 1);
			WriteAttributeValue("", 8504, filter.MeetingDateTo?.ToString("yyyy-MM-dd"), 8504, 45, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\n                    </div>\n                    <div class=\"col-md-1\">\n                        <label class=\"form-label form-label-sm\">משך מפגש</label>\n                        <input name=\"MeetingDuration\" type=\"number\" step=\"0.5\" min=\"0\" class=\"form-control form-control-sm\"");
			BeginWriteAttribute("value", " value=\"", 8828, "\"", 8859, 1);
			WriteAttributeValue("", 8836, filter.MeetingDuration, 8836, 23, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\n                    </div>\n\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">מחוז</label><select name=\"DistrictId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c43121", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (District x16 in districts)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c44568", async delegate
				{
					Write(x16.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x16.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 9140, (filter.DistrictId == x16.Id) ? "selected" : null, 9140, 48, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">מגזר</label><select name=\"SectorId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c47424", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (Sector x15 in sectors)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c48869", async delegate
				{
					Write(x15.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x15.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 9475, (filter.SectorId == x15.Id) ? "selected" : null, 9475, 46, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">תוכנית</label><select name=\"ProgramId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c51726", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (AxiomaReporting.Core.Entities.Program x14 in programs)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c53172", async delegate
				{
					Write(x14.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x14.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 9812, (filter.ProgramId == x14.Id) ? "selected" : null, 9812, 47, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">יישוב</label><select name=\"LocalityId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c56030", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (Locality x13 in localities)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c57478", async delegate
				{
					Write(x13.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x13.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 10152, (filter.LocalityId == x13.Id) ? "selected" : null, 10152, 48, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-2\"><label class=\"form-label form-label-sm\">מסגרת חינוכית</label><select name=\"FrameworkId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c60348", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (Framework x12 in frameworks)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c61796", async delegate
				{
					Write(x12.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x12.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 10502, (filter.FrameworkId == x12.Id) ? "selected" : null, 10502, 49, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-2\"><label class=\"form-label form-label-sm\">תוכנית חינוכית</label><select name=\"EducationalProgramId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c64677", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (EducationalProgram x11 in educationalPrograms)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c66134", async delegate
				{
					Write(x11.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x11.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 10872, (filter.EducationalProgramId == x11.Id) ? "selected" : null, 10872, 58, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">תחום</label><select name=\"DomainId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c69002", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (Domain x10 in domains)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c70447", async delegate
				{
					Write(x10.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x10.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 11217, (filter.DomainId == x10.Id) ? "selected" : null, 11217, 46, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">נושא 1</label><select name=\"Subject1Id\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c73307", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (Subject x9 in subjects)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c74753", async delegate
				{
					Write(x9.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x9.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 11555, (filter.Subject1Id == x9.Id) ? "selected" : null, 11555, 48, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">נושא 2</label><select name=\"Subject2Id\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c77615", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (Subject x8 in subjects)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c79061", async delegate
				{
					Write(x8.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x8.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 11895, (filter.Subject2Id == x8.Id) ? "selected" : null, 11895, 48, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">קיום דיון</label><select name=\"DiscussionCodeId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c81931", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (DiscussionCode x7 in discussionCodes)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c83384", async delegate
				{
					Write(x7.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x7.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 12250, (filter.DiscussionCodeId == x7.Id) ? "selected" : null, 12250, 54, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">כיתה</label><select name=\"ClassId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c86247", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (SchoolClass x6 in classes)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c87692", async delegate
				{
					Write(x6.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x6.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 12590, (filter.ClassId == x6.Id) ? "selected" : null, 12590, 45, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">שכבה</label><select name=\"GradeLevelId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c90551", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (GradeLevel x5 in gradeLevels)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c92000", async delegate
				{
					Write(x5.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x5.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 12930, (filter.GradeLevelId == x5.Id) ? "selected" : null, 12930, 50, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">סוג דיווח</label><select name=\"ReportTypeId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c94869", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (ReportType x4 in reportTypes)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c96318", async delegate
				{
					Write(x4.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x4.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 13280, (filter.ReportTypeId == x4.Id) ? "selected" : null, 13280, 50, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">מסקנה - כיתה</label><select name=\"ConclusionClassId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c99195", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (SchoolClass x3 in classes)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c100640", async delegate
				{
					Write(x3.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x3.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 13634, (filter.ConclusionClassId == x3.Id) ? "selected" : null, 13634, 55, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-2\"><label class=\"form-label form-label-sm\">מסקנה - מסגרת</label><select name=\"ConclusionFrameworkId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c103528", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (Framework x2 in frameworks)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c104977", async delegate
				{
					Write(x2.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x2.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 14001, (filter.ConclusionFrameworkId == x2.Id) ? "selected" : null, 14001, 59, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-2\"><label class=\"form-label form-label-sm\">מסקנה - יישוב/מחוז/ארצי</label><select name=\"ConclusionLocationId\" class=\"form-select form-select-sm\">");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c107878", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			foreach (LocalityDistrictNational x in conclusionLocations)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c109336", async delegate
				{
					Write(x.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(x.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 14390, (filter.ConclusionLocationId == x.Id) ? "selected" : null, 14390, 58, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n                    <div class=\"col-md-1\"><label class=\"form-label form-label-sm\">שורות בעמוד</label><select name=\"PageSize\" class=\"form-select form-select-sm\">");
			int[] array = new int[4] { 10, 25, 50, 100 };
			foreach (int s in array)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c112469", async delegate
				{
					Write(s);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(s);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 14728, (filter.PageSize == s) ? "selected" : null, 14728, 43, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("</select></div>\n\n                    <div class=\"col-auto d-flex align-items-end gap-2\">\n                        <button type=\"submit\" class=\"btn btn-primary btn-sm\">הצג</button>\n                        <a");
			BeginWriteAttribute("href", " href=\"", 14991, "\"", 15018, 1);
			WriteAttributeValue("", 14998, Url.Action("Index"), 14998, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"btn btn-outline-secondary btn-sm\">נקה</a>\n                    </div>\n                </div>\n            ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_9.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\n        </div>\n    </div>\n\n    <div class=\"d-flex justify-content-between align-items-center mb-2\">\n        <p class=\"text-muted mb-0\">נמצאו <strong>");
		Write(total);
		WriteLiteral("</strong> שורות דיווח</p>\n");
		if (total > filter.PageSize)
		{
			int num = (int)Math.Ceiling((double)total / (double)filter.PageSize);
			WriteLiteral("            <nav aria-label=\"דפדוף תוצאות\">\n                <ul class=\"pagination pagination-sm mb-0\">\n");
			for (int i = 1; i <= num; i++)
			{
				WriteLiteral("                        <li");
				BeginWriteAttribute("class", " class=\"", 15663, "\"", 15717, 2);
				WriteAttributeValue("", 15671, "page-item", 15671, 9, isLiteral: true);
				WriteAttributeValue(" ", 15680, (i == filter.Page) ? "active" : "", 15681, 36, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">\n                            <a class=\"page-link\"");
				BeginWriteAttribute("href", " href=\"", 15768, "\"", 15812, 1);
				WriteAttributeValue("", 15775, Url.Action("Index", RouteValues(i)), 15775, 37, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">");
				Write(i);
				WriteLiteral("</a>\n                        </li>\n");
			}
			WriteLiteral("                </ul>\n            </nav>\n");
		}
		WriteLiteral("    </div>\n\n    <div class=\"table-responsive\">\n        <table class=\"table table-hover table-bordered table-sm align-middle text-nowrap\">\n            <thead class=\"table-light\">\n                <tr>\n");
		if (showEditColumn)
		{
			WriteLiteral("                        <th>פעולות</th>\n");
		}
		WriteLiteral("                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 16293, "\"", 16321, 1);
		WriteAttributeValue("", 16300, SortLink("sequence"), 16300, 21, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">מס\"ד</a></th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 16381, "\"", 16409, 1);
		WriteAttributeValue("", 16388, SortLink("idnumber"), 16388, 21, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">ת.ז</a></th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 16468, "\"", 16492, 1);
		WriteAttributeValue("", 16475, SortLink("code"), 16475, 17, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">קוד עובד</a></th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 16556, "\"", 16580, 1);
		WriteAttributeValue("", 16563, SortLink("name"), 16563, 17, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">שם מדווח</a></th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 16644, "\"", 16669, 1);
		WriteAttributeValue("", 16651, SortLink("month"), 16651, 18, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">חודש דיווח</a></th>\n                    <th>פרויקט</th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 16771, "\"", 16799, 1);
		WriteAttributeValue("", 16778, SortLink("district"), 16778, 21, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">מחוז</a></th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 16859, "\"", 16887, 1);
		WriteAttributeValue("", 16866, SortLink("locality"), 16866, 21, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">יישוב</a></th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 16948, "\"", 16977, 1);
		WriteAttributeValue("", 16955, SortLink("framework"), 16955, 22, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">מסגרת חינוכית</a></th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 17046, "\"", 17077, 1);
		WriteAttributeValue("", 17053, SortLink("meetingdate"), 17053, 24, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">תאריך מפגש</a></th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 17143, "\"", 17171, 1);
		WriteAttributeValue("", 17150, SortLink("duration"), 17150, 21, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">משך מפגש</a></th>\n                    <th><a class=\"link-dark\"");
		BeginWriteAttribute("href", " href=\"", 17235, "\"", 17262, 1);
		WriteAttributeValue("", 17242, SortLink("program"), 17242, 20, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">תוכנית חינוכית</a></th>\n                    <th>תחום</th>\n                    <th>נושא 1</th>\n                    <th>נושא 2</th>\n                    <th>קיום דיון</th>\n                    <th>כיתה</th>\n                    <th>שכבה</th>\n                    <th>סוג דיווח</th>\n                    <th>מסקנה - כיתה</th>\n                    <th>מסקנה - מסגרת</th>\n                    <th>מסקנה - יישוב/מחוז/ארצי</th>\n                    <th>מסמכים</th>\n                    <th>הערות</th>\n                </tr>\n            </thead>\n            <tbody>\n");
		if (!rows.Any())
		{
			WriteLiteral("                    <tr>\n                        <td");
			BeginWriteAttribute("colspan", " colspan=\"", 17915, "\"", 17952, 1);
			WriteAttributeValue("", 17925, showEditColumn ? 25 : 24, 17925, 27, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"text-center text-muted py-4\">\n                            לא נמצאו שורות דיווח התואמות את הסינון\n                        </td>\n                    </tr>\n");
		}
		foreach (DashboardReportDetailRow r2 in rows)
		{
			WriteLiteral("                    <tr>\n");
			if (showEditColumn)
			{
				WriteLiteral("                            <td>\n");
				if (r2.ReportId > 0 && r2.ReportRowId > 0)
				{
					WriteLiteral("                                    ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "9c3b3022a4a3bcfd5dc6a3030f9f9b3bf4eeb86e13ae25182cd98d0e2314785c128486", async delegate
					{
						WriteLiteral("ערוך");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_12.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_13.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
					if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
					{
						throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-userId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
					}
					BeginWriteTagHelperAttribute();
					WriteLiteral(r2.UserId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["userId"] = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-userId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["userId"], HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					WriteLiteral(r2.AllocationId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"] = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-allocationId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"], HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					WriteLiteral(r2.ReportId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reportId"] = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-reportId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reportId"], HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					WriteLiteral(base.Context.Request.PathBase + base.Context.Request.Path + base.Context.Request.QueryString);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["returnUrl"] = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-returnUrl", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["returnUrl"], HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					WriteLiteral(r2.ReportRowId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["editRowId"] = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-editRowId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["editRowId"], HtmlAttributeValueStyle.DoubleQuotes);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\n");
				}
				else
				{
					WriteLiteral("                                    <span class=\"text-muted\">—</span>\n");
				}
				WriteLiteral("                            </td>\n");
			}
			WriteLiteral("                        <td>");
			Write((r2.SequenceNumber == 0) ? "—" : ((object)r2.SequenceNumber));
			WriteLiteral("</td>\n                        <td>");
			Write(r2.IdNumber);
			WriteLiteral("</td>\n                        <td>");
			Write(r2.EmployeeCode);
			WriteLiteral("</td>\n                        <td>");
			Write(r2.FullName);
			WriteLiteral("</td>\n                        <td>");
			Write(r2.MonthDescription);
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.ProjectName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.DistrictName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.LocalityName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.FrameworkName));
			WriteLiteral("</td>\n                        <td>");
			Write(DateOrDash(r2));
			WriteLiteral("</td>\n                        <td>");
			Write((r2.StatusId == 0) ? "—" : ((object)r2.MeetingDuration));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.EducationalProgramName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.DomainName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.Subject1Name));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.Subject2Name));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.DiscussionCodeName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.ClassName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.GradeLevelName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.ReportTypeName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.ConclusionClassName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.ConclusionFrameworkName));
			WriteLiteral("</td>\n                        <td>");
			Write(TextOrDash(r2.ConclusionLocationName));
			WriteLiteral("</td>\n                        <td class=\"text-center\">");
			Write(r2.HasAttachments ? "כן" : "לא");
			WriteLiteral("</td>\n                        <td class=\"text-wrap\" style=\"min-width: 220px\">");
			Write(TextOrDash(r2.Notes));
			WriteLiteral("</td>\n                    </tr>\n");
		}
		WriteLiteral("            </tbody>\n        </table>\n    </div>\n\n    <div class=\"d-flex gap-4 mt-2 text-muted\">\n        <span>סה\"כ שורות בעמוד: <strong>");
		Write(rows.Count);
		WriteLiteral("</strong></span>\n        <span>סה\"כ שעות בעמוד: <strong>");
		Write(rows.Where((DashboardReportDetailRow r) => r.StatusId != 0).Sum((DashboardReportDetailRow r) => r.MeetingDuration));
		WriteLiteral("</strong></span>\n    </div>\n</div>\n");
		static string DateOrDash(DashboardReportDetailRow row)
		{
			if (row.StatusId != 0 && !(row.MeetingDate == DateTime.MinValue))
			{
				return row.MeetingDate.ToString("dd/MM/yyyy");
			}
			return "—";
		}
		Dictionary<string, object?> RouteValues(int page, string? sortBy = null)
		{
			return new Dictionary<string, object>
			{
				["show"] = 1,
				["Page"] = page,
				["PageSize"] = filter.PageSize,
				["StatusId"] = filter.StatusId,
				["DistrictId"] = filter.DistrictId,
				["SectorId"] = filter.SectorId,
				["ProgramId"] = filter.ProgramId,
				["LocalityId"] = filter.LocalityId,
				["FrameworkId"] = filter.FrameworkId,
				["EducationalProgramId"] = filter.EducationalProgramId,
				["DomainId"] = filter.DomainId,
				["Subject1Id"] = filter.Subject1Id,
				["Subject2Id"] = filter.Subject2Id,
				["DiscussionCodeId"] = filter.DiscussionCodeId,
				["ClassId"] = filter.ClassId,
				["GradeLevelId"] = filter.GradeLevelId,
				["ConclusionClassId"] = filter.ConclusionClassId,
				["ConclusionFrameworkId"] = filter.ConclusionFrameworkId,
				["ConclusionLocationId"] = filter.ConclusionLocationId,
				["ReportTypeId"] = filter.ReportTypeId,
				["MeetingDateFrom"] = filter.MeetingDateFrom?.ToString("yyyy-MM-dd"),
				["MeetingDateTo"] = filter.MeetingDateTo?.ToString("yyyy-MM-dd"),
				["MeetingDuration"] = filter.MeetingDuration,
				["EmployeeCode"] = filter.EmployeeCode,
				["IdNumber"] = filter.IdNumber,
				["EmployeeName"] = filter.EmployeeName,
				["FromMonthId"] = filter.FromMonthId,
				["ToMonthId"] = filter.ToMonthId,
				["SortBy"] = sortBy ?? filter.SortBy,
				["SortDesc"] = ((sortBy == null) ? filter.SortDesc : (filter.SortBy == sortBy && !filter.SortDesc))
			};
		}
		string SortLink(string key)
		{
			return Url.Action("Index", RouteValues(1, key)) ?? "#";
		}
		static string TextOrDash(string value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				return value;
			}
			return "—";
		}
	}
}
