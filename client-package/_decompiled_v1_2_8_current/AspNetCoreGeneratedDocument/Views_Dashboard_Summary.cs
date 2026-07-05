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

[RazorCompiledItemMetadata("Identifier", "/Views/Dashboard/Summary.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Dashboard_Summary : RazorPage<dynamic>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "SummaryExportExcel", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-success btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("value", "3", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("value", "4", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("value", "5", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("method", "get", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("class", new HtmlString("row g-2 align-items-end"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("id", new HtmlString("summaryFilterForm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("asp-controller", "Report", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("asp-action", "Approve", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_14 = new TagHelperAttribute("style", new HtmlString("display:inline"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_15 = new TagHelperAttribute("asp-action", "BulkApprove", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_16 = new TagHelperAttribute("id", new HtmlString("bulkForm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_17 = new TagHelperAttribute("asp-action", "Reject", HtmlAttributeValueStyle.DoubleQuotes);

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
		base.ViewData["Title"] = "מסך סיכום ואישור דיווחים";
		List<DashboardReportRow> rows = (base.ViewBag.Rows as List<DashboardReportRow>) ?? new List<DashboardReportRow>();
		DashboardFilter filter = (base.ViewBag.Filter as DashboardFilter) ?? new DashboardFilter();
		List<ReportingMonth> reportingMonths = (base.ViewBag.ReportingMonths as List<ReportingMonth>) ?? new List<ReportingMonth>();
		List<District> districts = (base.ViewBag.Districts as List<District>) ?? new List<District>();
		bool canApprove = base.ViewBag.CanApprove ?? ((object)false);
		int total = base.ViewBag.TotalCount ?? ((object)0);
		int pendingCount = rows.Count((DashboardReportRow r) => r.StatusId == 3);
		string currentUrl = base.Context.Request.PathBase + base.Context.Request.Path + base.Context.Request.QueryString;
		WriteLiteral("\r\n<div class=\"container-fluid mt-3\">\r\n    <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n        <h3>מסך סיכום ואישור דיווחים</h3>\r\n        <div class=\"d-flex gap-2\">\r\n            ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403711153", async delegate
		{
			WriteLiteral("ייצוא סיכום");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-StatusId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
		}
		BeginWriteTagHelperAttribute();
		WriteLiteral(filter.StatusId);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["StatusId"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-StatusId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["StatusId"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(filter.FromMonthId);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["FromMonthId"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-FromMonthId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["FromMonthId"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(filter.ToMonthId);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["ToMonthId"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-ToMonthId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["ToMonthId"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(filter.DistrictId);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["DistrictId"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-DistrictId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["DistrictId"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(filter.EmployeeName);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["EmployeeName"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-EmployeeName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["EmployeeName"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(filter.SortBy);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["SortBy"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-SortBy", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["SortBy"], HtmlAttributeValueStyle.DoubleQuotes);
		BeginWriteTagHelperAttribute();
		WriteLiteral(filter.SortDesc);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["SortDesc"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-SortDesc", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["SortDesc"], HtmlAttributeValueStyle.DoubleQuotes);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n            ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403717808", async delegate
		{
			WriteLiteral("חזרה לדשבורד");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    <!-- Live-filter toast + announcer -->\r\n    <div id=\"filterLiveRegion\" class=\"visually-hidden\" role=\"status\" aria-live=\"polite\" aria-atomic=\"true\"></div>\r\n    <div id=\"filterToast\" class=\"alert alert-warning d-none\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\"></div>\r\n\r\n    <!-- Filter bar -->\r\n    <div class=\"card mb-3\">\r\n        <div class=\"card-body py-2\">\r\n            ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403719515", async delegate
		{
			WriteLiteral("\r\n                <input type=\"hidden\" name=\"SortBy\"");
			BeginWriteAttribute("value", " value=\"", 2530, "\"", 2552, 1);
			WriteAttributeValue("", 2538, filter.SortBy, 2538, 14, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n                <input type=\"hidden\" name=\"SortDesc\"");
			BeginWriteAttribute("value", " value=\"", 2610, "\"", 2634, 1);
			WriteAttributeValue("", 2618, filter.SortDesc, 2618, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n                <div class=\"col-md-3\">\r\n                    <label class=\"form-label form-label-sm\">חודש</label>\r\n                    <select name=\"FromMonthId\" class=\"form-select form-select-sm\" data-live-filter=\"months\">\r\n                        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403721034", async delegate
			{
				WriteLiteral("כל החודשים");
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
			WriteLiteral("\r\n");
			foreach (ReportingMonth i in reportingMonths)
			{
				WriteLiteral("                            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403722582", async delegate
				{
					Write(i.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(i.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 3074, (filter.FromMonthId == i.Id) ? "selected" : null, 3074, 49, isLiteral: false);
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
			WriteLiteral("                    </select>\r\n                    <input type=\"hidden\" name=\"ToMonthId\"");
			BeginWriteAttribute("value", " value=\"", 3265, "\"", 3292, 1);
			WriteAttributeValue("", 3273, filter.FromMonthId, 3273, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n                </div>\r\n                <div class=\"col-md-2\">\r\n                    <label class=\"form-label form-label-sm\">מחוז</label>\r\n                    <select name=\"DistrictId\" class=\"form-select form-select-sm\" data-live-filter=\"districts\">\r\n                        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403726093", async delegate
			{
				WriteLiteral("הכל");
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
			WriteLiteral("\r\n");
			foreach (District d in districts)
			{
				WriteLiteral("                            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403727628", async delegate
				{
					Write(d.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(d.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 3745, (filter.DistrictId == d.Id) ? "selected" : null, 3745, 48, isLiteral: false);
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
			WriteLiteral("                    </select>\r\n                </div>\r\n                <div class=\"col-md-2\">\r\n                    <label class=\"form-label form-label-sm\">סטטוס</label>\r\n                    <select name=\"StatusId\" class=\"form-select form-select-sm\" data-live-filter=\"statuses\" data-preserve-options=\"1\">\r\n                        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403730668", async delegate
			{
				WriteLiteral("הכל");
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
			WriteLiteral("\r\n                        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403731937", async delegate
			{
				WriteLiteral("ממתין לאישור");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 4259, (filter.StatusId.GetValueOrDefault() == 3) ? "selected" : null, 4259, 43, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n                        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403733760", async delegate
			{
				WriteLiteral("מאושר");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 4379, (filter.StatusId.GetValueOrDefault() == 4) ? "selected" : null, 4379, 43, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n                        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403735576", async delegate
			{
				WriteLiteral("הוחזר לתיקון");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_7.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 4492, (filter.StatusId.GetValueOrDefault() == 5) ? "selected" : null, 4492, 43, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n                    </select>\r\n                </div>\r\n                <div class=\"col-md-3\">\r\n                    <label class=\"form-label form-label-sm\">שם / קוד / ת.ז</label>\r\n                    <input name=\"EmployeeName\" class=\"form-control form-control-sm\"");
			BeginWriteAttribute("value", " value=\"", 4822, "\"", 4850, 1);
			WriteAttributeValue("", 4830, filter.EmployeeName, 4830, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" placeholder=\"חיפוש עובד...\" />\r\n                </div>\r\n                <div class=\"col-auto d-flex gap-2 align-items-end\">\r\n                    <button type=\"submit\" class=\"btn btn-primary btn-sm\">הצג</button>\r\n                    <a");
			BeginWriteAttribute("href", " href=\"", 5086, "\"", 5115, 1);
			WriteAttributeValue("", 5093, Url.Action("Summary"), 5093, 22, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"btn btn-outline-secondary btn-sm\">נקה</a>\r\n                </div>\r\n            ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n");
		if (base.TempData["Success"] != null)
		{
			WriteLiteral("        <div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\">\r\n            ");
			Write(base.TempData["Success"]);
			WriteLiteral("\r\n            <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n        </div>\r\n");
		}
		if (base.TempData["Error"] != null)
		{
			WriteLiteral("        <div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\">\r\n            ");
			Write(base.TempData["Error"]);
			WriteLiteral("\r\n            <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n        </div>\r\n");
		}
		WriteLiteral("\r\n    <!-- Summary stats -->\r\n    <div class=\"row g-3 mb-3\">\r\n        <div class=\"col-md-3\">\r\n            <div class=\"card text-center border-secondary\">\r\n                <div class=\"card-body py-2\">\r\n                    <div class=\"h4 mb-0\">");
		Write(total);
		WriteLiteral("</div>\r\n                    <small class=\"text-muted\">סה\"כ דיווחים</small>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n            <div class=\"card text-center border-warning\">\r\n                <div class=\"card-body py-2\">\r\n                    <div class=\"h4 mb-0 text-warning\">");
		Write(rows.Count((DashboardReportRow r) => r.StatusId == 3));
		WriteLiteral("</div>\r\n                    <small class=\"text-muted\">ממתינים לאישור</small>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n            <div class=\"card text-center border-success\">\r\n                <div class=\"card-body py-2\">\r\n                    <div class=\"h4 mb-0 text-success\">");
		Write(rows.Count((DashboardReportRow r) => r.StatusId == 4));
		WriteLiteral("</div>\r\n                    <small class=\"text-muted\">מאושרים</small>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n            <div class=\"card text-center border-danger\">\r\n                <div class=\"card-body py-2\">\r\n                    <div class=\"h4 mb-0 text-danger\">");
		Write(rows.Count((DashboardReportRow r) => r.StatusId == 5));
		WriteLiteral("</div>\r\n                    <small class=\"text-muted\">הוחזרו לתיקון</small>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n");
		if (canApprove && pendingCount > 0)
		{
			WriteLiteral("        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403744940", async delegate
			{
				WriteLiteral("\r\n            ");
				Write(Html.AntiForgeryToken());
				WriteLiteral("\r\n\r\n            <div class=\"d-flex gap-2 mb-2 align-items-center\">\r\n                <button type=\"button\" onclick=\"selectAll(true)\" class=\"btn btn-outline-secondary btn-sm\">בחר הכל</button>\r\n                <button type=\"button\" onclick=\"selectAll(false)\" class=\"btn btn-outline-secondary btn-sm\">בטל בחירה</button>\r\n                <button type=\"submit\" class=\"btn btn-success btn-sm\" id=\"bulkApproveBtn\" disabled>\r\n                    אשר נבחרים (<span id=\"selectedCount\">0</span>)\r\n                </button>\r\n            </div>\r\n\r\n            <div class=\"table-responsive\">\r\n                <table class=\"table table-hover table-bordered table-sm align-middle\">\r\n                    <thead class=\"table-light\">\r\n                        <tr>\r\n                            <th style=\"width:40px\">\r\n                                <input type=\"checkbox\" id=\"selectAllCb\" title=\"בחר הכל\" />\r\n                            </th>\r\n                            <th><a class=\"link-dark\"");
				BeginWriteAttribute("href", " href=\"", 8517, "\"", 8541, 1);
				WriteAttributeValue("", 8524, SortLink("name"), 8524, 17, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">עובד</a></th>\n                            <th><a class=\"link-dark\"");
				BeginWriteAttribute("href", " href=\"", 8609, "\"", 8633, 1);
				WriteAttributeValue("", 8616, SortLink("code"), 8616, 17, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">קוד עובד</a></th>\n                            <th><a class=\"link-dark\"");
				BeginWriteAttribute("href", " href=\"", 8705, "\"", 8732, 1);
				WriteAttributeValue("", 8712, SortLink("project"), 8712, 20, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">פרויקט</a></th>\n                            <th><a class=\"link-dark\"");
				BeginWriteAttribute("href", " href=\"", 8802, "\"", 8827, 1);
				WriteAttributeValue("", 8809, SortLink("month"), 8809, 18, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">חודש</a></th>\n                            <th><a class=\"link-dark\"");
				BeginWriteAttribute("href", " href=\"", 8895, "\"", 8921, 1);
				WriteAttributeValue("", 8902, SortLink("status"), 8902, 19, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">סטטוס</a></th>\r\n                            <th>שורות</th>\r\n                            <th>משך תפוקה</th>\r\n                            <th>יתרת שורות</th>\r\n                            <th><a class=\"link-dark\"");
				BeginWriteAttribute("href", " href=\"", 9132, "\"", 9161, 1);
				WriteAttributeValue("", 9139, SortLink("submitted"), 9139, 22, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">תאריך הגשה</a></th>\r\n                            <th>מסמכים</th>\r\n                            <th>פעולות</th>\r\n                        </tr>\r\n                    </thead>\r\n                    <tbody>\r\n");
				if (!rows.Any())
				{
					WriteLiteral("                            <tr>\r\n                                <td colspan=\"12\" class=\"text-center text-muted py-4\">אין דיווחים להצגה</td>\n                            </tr>\r\n");
				}
				foreach (DashboardReportRow r2 in rows)
				{
					WriteLiteral("                            <tr>\r\n                                <td class=\"text-center\">\r\n");
					if (r2.StatusId == 3)
					{
						WriteLiteral("                                        <input type=\"checkbox\" name=\"reportIds\"");
						BeginWriteAttribute("value", " value=\"", 9939, "\"", 9958, 1);
						WriteAttributeValue("", 9947, r2.ReportId, 9947, 11, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral("\r\n                                               class=\"report-cb\" onchange=\"updateCount()\" />\r\n");
					}
					WriteLiteral("                                </td>\n                                <td>");
					Write(r2.FullName);
					WriteLiteral("</td>\n                                <td>");
					Write(r2.EmployeeCode);
					WriteLiteral("</td>\n                                <td>");
					Write(string.IsNullOrWhiteSpace(r2.ProjectName) ? "—" : r2.ProjectName);
					WriteLiteral("</td>\n                                <td>");
					Write(r2.MonthDescription);
					WriteLiteral("</td>\n                                <td>\r\n                                    <span");
					BeginWriteAttribute("class", " class=\"", 10489, "\"", 10628, 2);
					WriteAttributeValue("", 10497, "badge", 10497, 5, isLiteral: true);
					WriteAttributeValue(" ", 10502, (r2.StatusId == 4) ? "bg-success" : ((r2.StatusId == 5) ? "bg-danger" : ((r2.StatusId == 3) ? "bg-warning text-dark" : "bg-secondary")), 10503, 125, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(">\r\n                                        ");
					Write(r2.StatusName);
					WriteLiteral("\r\n                                    </span>\r\n                                </td>\r\n                                <td>");
					Write(r2.RowCount);
					WriteLiteral("</td>\r\n                                <td>");
					Write(r2.TotalDuration);
					WriteLiteral("</td>\r\n                                <td>\r\n");
					if (r2.MonthlyRowAllocation.HasValue)
					{
						WriteLiteral("                                        <span");
						BeginWriteAttribute("class", " class=\"", 11081, "\"", 11140, 1);
						WriteAttributeValue("", 11089, (r2.RemainingRows < 0) ? "text-danger fw-bold" : "", 11089, 51, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(">");
						Write(r2.RemainingRows);
						WriteLiteral("</span>\r\n");
					}
					else
					{
						WriteLiteral("                                        <span class=\"text-muted\">—</span>\r\n");
					}
					WriteLiteral("                                </td>\r\n                                <td>");
					Write(r2.SubmittedAt.HasValue ? r2.SubmittedAt.Value.ToString("dd/MM/yyyy") : "—");
					WriteLiteral("</td>\r\n                                <td>");
					if (r2.ReportId != 0 && r2.DocumentCount > 0)
					{
						WriteLiteral("<a class=\"btn btn-outline-primary btn-sm\" title=\"פתיחת מסמכי הדיווח\" href=\"/Report?reportId=");
						Write(r2.ReportId);
						WriteLiteral("&allocationId=");
						Write(r2.AllocationId);
						WriteLiteral("#documents\">מסמכים (");
						Write(r2.DocumentCount);
						WriteLiteral(")</a>");
					}
					else
					{
						WriteLiteral("<span class=\"text-muted\">0</span>");
					}
					WriteLiteral("</td>\r\n                                <td>\r\n");
					if (r2.StatusId == 3)
					{
						WriteLiteral("                                        ");
						__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403757446", async delegate
						{
							WriteLiteral("\n                                            ");
							Write(Html.AntiForgeryToken());
							WriteLiteral("\n                                            <input type=\"hidden\" name=\"returnUrl\"");
							BeginWriteAttribute("value", " value=\"", 12053, "\"", 12072, 1);
							WriteAttributeValue("", 12061, currentUrl, 12061, 11, isLiteral: false);
							EndWriteAttribute();
							WriteLiteral(" />\n                                            <button type=\"submit\" class=\"btn btn-sm btn-success\">אשר</button>\n                                        ");
						});
						__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
						__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
						__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
						__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
						__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_11.Value;
						__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
						__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_12.Value;
						__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
						if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
						{
							throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-reportId", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
						}
						BeginWriteTagHelperAttribute();
						WriteLiteral(r2.ReportId);
						__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
						__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["reportId"] = __tagHelperStringValueBuffer;
						__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-reportId", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["reportId"], HtmlAttributeValueStyle.DoubleQuotes);
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
						WriteLiteral("\n                                        <button class=\"btn btn-sm btn-danger ms-1\"");
						BeginWriteAttribute("onclick", " onclick=\"", 12317, "\"", 12350, 3);
						WriteAttributeValue("", 12327, "openReject(", 12327, 11, isLiteral: true);
						WriteAttributeValue("", 12338, r2.ReportId, 12338, 11, isLiteral: false);
						WriteAttributeValue("", 12349, ")", 12349, 1, isLiteral: true);
						EndWriteAttribute();
						WriteLiteral(">דחה</button>\r\n");
					}
					else
					{
						WriteLiteral("                                        <span class=\"text-muted\">—</span>\r\n");
					}
					WriteLiteral("                                </td>\r\n                            </tr>\r\n");
				}
				WriteLiteral("                    </tbody>\r\n                </table>\r\n            </div>\r\n        ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_15.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_13.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_16);
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
			WriteLiteral("        <!-- Read-only table when no approve permission or no pending items -->\r\n        <div class=\"table-responsive\">\r\n            <table class=\"table table-hover table-bordered table-sm align-middle\">\r\n                <thead class=\"table-light\">\r\n                    <tr>\r\n                        <th><a class=\"link-dark\"");
			BeginWriteAttribute("href", " href=\"", 13142, "\"", 13166, 1);
			WriteAttributeValue("", 13149, SortLink("name"), 13149, 17, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">עובד</a></th>\n                        <th><a class=\"link-dark\"");
			BeginWriteAttribute("href", " href=\"", 13230, "\"", 13254, 1);
			WriteAttributeValue("", 13237, SortLink("code"), 13237, 17, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">קוד עובד</a></th>\n                        <th><a class=\"link-dark\"");
			BeginWriteAttribute("href", " href=\"", 13322, "\"", 13349, 1);
			WriteAttributeValue("", 13329, SortLink("project"), 13329, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">פרויקט</a></th>\n                        <th><a class=\"link-dark\"");
			BeginWriteAttribute("href", " href=\"", 13415, "\"", 13440, 1);
			WriteAttributeValue("", 13422, SortLink("month"), 13422, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">חודש</a></th>\n                        <th><a class=\"link-dark\"");
			BeginWriteAttribute("href", " href=\"", 13504, "\"", 13530, 1);
			WriteAttributeValue("", 13511, SortLink("status"), 13511, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">סטטוס</a></th>\r\n                        <th>שורות</th>\r\n                        <th>משך תפוקה</th>\r\n                        <th>יתרת שורות</th>\r\n                        <th><a class=\"link-dark\"");
			BeginWriteAttribute("href", " href=\"", 13725, "\"", 13754, 1);
			WriteAttributeValue("", 13732, SortLink("submitted"), 13732, 22, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">תאריך הגשה</a></th>\r\n                        <th>מסמכים</th>\r\n                    </tr>\r\n                </thead>\r\n                <tbody>\r\n");
			if (!rows.Any())
			{
				WriteLiteral("                        <tr>\r\n                            <td colspan=\"10\" class=\"text-center text-muted py-4\">אין דיווחים להצגה</td>\n                        </tr>\r\n");
			}
			foreach (DashboardReportRow item in rows)
			{
				WriteLiteral("                        <tr>\r\n                            <td>");
				Write(item.FullName);
				WriteLiteral("</td>\n                            <td>");
				Write(item.EmployeeCode);
				WriteLiteral("</td>\n                            <td>");
				Write(string.IsNullOrWhiteSpace(item.ProjectName) ? "—" : item.ProjectName);
				WriteLiteral("</td>\n                            <td>");
				Write(item.MonthDescription);
				WriteLiteral("</td>\n                            <td>\r\n                                <span");
				BeginWriteAttribute("class", " class=\"", 14536, "\"", 14675, 2);
				WriteAttributeValue("", 14544, "badge", 14544, 5, isLiteral: true);
				WriteAttributeValue(" ", 14549, (item.StatusId == 4) ? "bg-success" : ((item.StatusId == 5) ? "bg-danger" : ((item.StatusId == 3) ? "bg-warning text-dark" : "bg-secondary")), 14550, 125, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">\r\n                                    ");
				Write(item.StatusName);
				WriteLiteral("\r\n                                </span>\r\n                            </td>\r\n                            <td>");
				Write(item.RowCount);
				WriteLiteral("</td>\r\n                            <td>");
				Write(item.TotalDuration);
				WriteLiteral("</td>\r\n                            <td>\r\n");
				if (item.MonthlyRowAllocation.HasValue)
				{
					WriteLiteral("                                    <span");
					BeginWriteAttribute("class", " class=\"", 15092, "\"", 15151, 1);
					WriteAttributeValue("", 15100, (item.RemainingRows < 0) ? "text-danger fw-bold" : "", 15100, 51, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(">");
					Write(item.RemainingRows);
					WriteLiteral("</span>\r\n");
				}
				else
				{
					WriteLiteral("                                    <span class=\"text-muted\">—</span>\r\n");
				}
				WriteLiteral("                            </td>\r\n                            <td>");
				Write(item.SubmittedAt.HasValue ? item.SubmittedAt.Value.ToString("dd/MM/yyyy") : "—");
				WriteLiteral("</td>\r\n                            <td>");
				if (item.ReportId != 0 && item.DocumentCount > 0)
				{
					WriteLiteral("<a class=\"btn btn-outline-primary btn-sm\" title=\"פתיחת מסמכי הדיווח\" href=\"/Report?reportId=");
					Write(item.ReportId);
					WriteLiteral("&allocationId=");
					Write(item.AllocationId);
					WriteLiteral("#documents\">מסמכים (");
					Write(item.DocumentCount);
					WriteLiteral(")</a>");
				}
				else
				{
					WriteLiteral("<span class=\"text-muted\">0</span>");
				}
				WriteLiteral("</td>\r\n                        </tr>\r\n");
			}
			WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n<!-- Reject Modal -->\r\n<div class=\"modal fade\" id=\"rejectModal\" tabindex=\"-1\" aria-labelledby=\"rejectModalLabel\" aria-hidden=\"true\">\r\n    <div class=\"modal-dialog\" role=\"dialog\" aria-modal=\"true\">\r\n        <div class=\"modal-content\">\r\n            <div class=\"modal-header\">\r\n                <h5 class=\"modal-title\" id=\"rejectModalLabel\">סיבת דחייה</h5>\r\n                <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור\"></button>\r\n            </div>\r\n            ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "24bff77a1a624cfeff7e4a002eab8b5dceee6acecd578d5180f6431a7610403775267", async delegate
		{
			WriteLiteral("\r\n                ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\n                <input type=\"hidden\" id=\"rejectReportId\" name=\"reportId\" />\n                <input type=\"hidden\" name=\"returnUrl\"");
			BeginWriteAttribute("value", " value=\"", 16408, "\"", 16427, 1);
			WriteAttributeValue("", 16416, currentUrl, 16416, 11, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\n                <div class=\"modal-body\">\r\n                    <label class=\"form-label\">סיבת הדחייה (חובה)</label>\r\n                    <textarea name=\"rejectionReason\" class=\"form-control\" rows=\"4\"\r\n                              placeholder=\"הזן את סיבת הדחייה...\" required></textarea>\r\n                </div>\r\n                <div class=\"modal-footer\">\r\n                    <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n                    <button type=\"submit\" class=\"btn btn-danger\">דחה דיווח</button>\r\n                </div>\r\n            ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_11.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_17.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_17);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_13.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"modal fade\" id=\"dashboardDocumentsModal\" tabindex=\"-1\" aria-labelledby=\"dashboardDocumentsModalLabel\" aria-hidden=\"true\">\r\n    <div class=\"modal-dialog modal-lg\" role=\"dialog\" aria-modal=\"true\">\r\n        <div class=\"modal-content\">\r\n            <div class=\"modal-header\">\r\n                <h5 class=\"modal-title\" id=\"dashboardDocumentsModalLabel\">מסמכי עובד</h5>\r\n                <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור\"></button>\r\n            </div>\r\n            <div class=\"modal-body\">\r\n                <div id=\"dashboardDocumentsMeta\" class=\"mb-3\"></div>\r\n                <div id=\"dashboardDocumentsState\" class=\"text-muted\">טוען מסמכים...</div>\r\n                <div class=\"table-responsive d-none\" id=\"dashboardDocumentsTableWrap\">\r\n                    <table class=\"table table-sm table-bordered align-middle mb-0\">\r\n                        <thead class=\"table-light\">\r\n                            <tr>\r\n                                <th>שם קובץ</th>\r\n                                <th>תיאור</th>\r\n                                <th>תאריך העלאה</th>\r\n                                <th>גודל</th>\r\n                                <th>פעולות</th>\r\n                            </tr>\r\n                        </thead>\r\n                        <tbody id=\"dashboardDocumentsRows\"></tbody>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n            <div class=\"modal-footer\">\r\n                <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">סגור</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
		DefineSection("Scripts", (RenderAsyncDelegate)async delegate
		{
			WriteLiteral("\r\n    <script>\r\n        function selectAll(val) {\r\n            document.querySelectorAll('.report-cb').forEach(function (cb) { cb.checked = val; });\r\n            updateCount();\r\n        }\r\n\r\n        function updateCount() {\r\n            var count = document.querySelectorAll('.report-cb:checked').length;\r\n            document.getElementById('selectedCount').textContent = count;\r\n            document.getElementById('bulkApproveBtn').disabled = count === 0;\r\n        }\r\n\r\n        var selectAllCb = document.getElementById('selectAllCb');\r\n        if (selectAllCb) {\r\n            selectAllCb.addEventListener('change', function () {\r\n                selectAll(this.checked);\r\n            });\r\n        }\r\n\r\n        function openReject(reportId) {\r\n            document.getElementById('rejectReportId').value = reportId;\r\n            var modal = new bootstrap.Modal(document.getElementById('rejectModal'));\r\n            modal.show();\r\n        }\r\n\r\n        (function () {\r\n            var form = document.getElementById('summar");
			WriteLiteral("yFilterForm');\r\n            if (!form) return;\r\n\r\n            var inputToField = {\r\n                'DistrictId': 'DistrictId',\r\n                'StatusId': 'StatusId',\r\n                'FromMonthId': 'FromMonthId',\r\n                'EmployeeName': 'EmployeeName'\r\n            };\r\n\r\n            function collectSelection() {\r\n                var sel = {};\r\n                Object.keys(inputToField).forEach(function (name) {\r\n                    var el = form.querySelector('[name=\"' + name + '\"]');\r\n                    if (!el) return;\r\n                    var v = el.value;\r\n                    if (v === null || v === undefined || v === '') return;\r\n                    if (['DistrictId', 'StatusId', 'FromMonthId'].indexOf(name) >= 0) {\r\n                        var n = parseInt(v, 10);\r\n                        if (!isNaN(n)) {\r\n                            sel[inputToField[name]] = n;\r\n                            if (name === 'FromMonthId') sel['ToMonthId'] = n;\r\n                        }\r\n                    } els");
			WriteLiteral("e {\r\n                        sel[inputToField[name]] = v;\r\n                    }\r\n                });\r\n                return sel;\r\n            }\r\n\r\n            function showToast(msg) {\r\n                var t = document.getElementById('filterToast');\r\n                var live = document.getElementById('filterLiveRegion');\r\n                if (t) {\r\n                    t.textContent = msg;\r\n                    t.classList.remove('d-none');\r\n                    setTimeout(function () { t.classList.add('d-none'); }, 4000);\r\n                }\r\n                if (live) {\r\n                    live.textContent = msg;\r\n                }\r\n            }\r\n\r\n            function repopulate(selectEl, options) {\r\n                var currentValue = selectEl.value;\r\n                var preserve = selectEl.getAttribute('data-preserve-options') === '1';\r\n                if (preserve) {\r\n                    var validIds = {};\r\n                    options.forEach(function (o) { validIds[String(o.id)] = true; });\r\n             ");
			WriteLiteral("       var stillValid = true;\r\n                    Array.prototype.forEach.call(selectEl.options, function (opt) {\r\n                        if (opt.value === '') { opt.disabled = false; return; }\r\n                        var ok = !!validIds[opt.value];\r\n                        opt.disabled = !ok;\r\n                        if (opt.value === currentValue && !ok) stillValid = false;\r\n                    });\r\n                    if (!stillValid) {\r\n                        selectEl.value = '';\r\n                        return false;\r\n                    }\r\n                    return true;\r\n                }\r\n\r\n                var firstOption = selectEl.options[0];\r\n                var placeholder = firstOption && firstOption.value === '' ? firstOption : null;\r\n                selectEl.innerHTML = '';\r\n                if (placeholder) selectEl.appendChild(placeholder);\r\n\r\n                var optionIds = {};\r\n                options.forEach(function (o) {\r\n                    var opt = document.createElement('option')");
			WriteLiteral(";\r\n                    opt.value = String(o.id);\r\n                    opt.textContent = o.name;\r\n                    selectEl.appendChild(opt);\r\n                    optionIds[String(o.id)] = true;\r\n                });\r\n\r\n                if (currentValue && optionIds[currentValue]) {\r\n                    selectEl.value = currentValue;\r\n                    return true;\r\n                }\r\n                if (currentValue) {\r\n                    selectEl.value = '';\r\n                    return false;\r\n                }\r\n                return true;\r\n            }\r\n\r\n            var inFlight = null;\r\n            var debounceId = null;\r\n\r\n            function fetchOptions() {\r\n                var sel = collectSelection();\r\n                var q = encodeURIComponent(JSON.stringify(sel));\r\n                if (inFlight) inFlight.abort();\r\n                inFlight = new AbortController();\r\n                fetch('");
			Write(Url.Action("FilterOptions", "Dashboard"));
			WriteLiteral("' + '?selected=' + q, {\r\n                    headers: { 'Accept': 'application/json' },\r\n                    credentials: 'same-origin',\r\n                    signal: inFlight.signal\r\n                })\r\n                    .then(function (res) { return res.ok ? res.json() : null; })\r\n                    .then(function (data) {\r\n                        if (!data) return;\r\n                        var lostSelection = false;\r\n\r\n                        form.querySelectorAll('[data-live-filter]').forEach(function (sel) {\r\n                            var dim = sel.getAttribute('data-live-filter');\r\n                            var list = data[dim] || [];\r\n                            var ok = repopulate(sel, list);\r\n                            if (!ok) lostSelection = true;\r\n                        });\r\n\r\n                        // Keep ToMonthId hidden mirror in sync\r\n                        var fromMonth = form.querySelector('[name=\"FromMonthId\"]');\r\n                        var toMonth = form.querySelector('[name=\"T");
			WriteLiteral("oMonthId\"]');\r\n                        if (fromMonth && toMonth) toMonth.value = fromMonth.value;\r\n\r\n                        if (lostSelection) {\r\n                            showToast('סינון עודכן — הבחירה הקודמת אינה תואמת');\r\n                        }\r\n                    })\r\n                    .catch(function () { /* ignore */ });\r\n            }\r\n\r\n            function onChangeDebounced() {\r\n                if (debounceId) clearTimeout(debounceId);\r\n                debounceId = setTimeout(fetchOptions, 250);\r\n            }\r\n\r\n            Object.keys(inputToField).forEach(function (name) {\r\n                var el = form.querySelector('[name=\"' + name + '\"]');\r\n                if (!el) return;\r\n                var evt = el.tagName === 'SELECT' ? 'change' : 'input';\r\n                el.addEventListener(evt, onChangeDebounced);\r\n            });\r\n        })();\r\n    </script>");
		});
		WriteLiteral("\r\n<script>\r\n(function () {\r\n    function escapeHtml(value) {\r\n        return String(value || '').replace(/[&<>\"']/g, function (ch) {\r\n            return ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '\"': '&quot;', \"'\": '&#39;' })[ch];\r\n        });\r\n    }\r\n\r\n    function showDashboardDocuments(reportId, allocationId) {\r\n        var modalEl = document.getElementById('dashboardDocumentsModal');\r\n        var meta = document.getElementById('dashboardDocumentsMeta');\r\n        var state = document.getElementById('dashboardDocumentsState');\r\n        var tableWrap = document.getElementById('dashboardDocumentsTableWrap');\r\n        var rows = document.getElementById('dashboardDocumentsRows');\r\n        if (!modalEl || !meta || !state || !tableWrap || !rows) return;\r\n        meta.innerHTML = '';\r\n        rows.innerHTML = '';\r\n        state.textContent = 'טוען מסמכים...';\r\n        state.classList.remove('d-none', 'text-danger');\r\n        tableWrap.classList.add('d-none');\r\n        new bootstrap.Modal(modalEl).show();\r\n\r\n        fetch('/Dashboard/ReportDocuments?reportId=' + encodeURIComponent(reportId) + '&allocationId=' + encodeURIComponent(allocationId), {\r\n            headers: { 'Accept': 'application/json' },\r\n            credentials: 'same-origin'\r\n        })\r\n            .then(function (res) { if (!res.ok) throw new Error('failed'); return res.json(); })\r\n            .then(function (data) {\r\n                meta.innerHTML = '<div class=\"row g-2\">'\r\n                    + '<div class=\"col-md-6\"><strong>עובד:</strong> ' + escapeHtml(data.employeeName) + '</div>'\r\n                    + '<div class=\"col-md-6\"><strong>תעודת זהות:</strong> ' + escapeHtml(data.employeeId) + '</div>'\r\n                    + '<div class=\"col-md-6\"><strong>פרויקט:</strong> ' + escapeHtml(data.projectName) + '</div>'\r\n                    + '<div class=\"col-md-6\"><strong>חודש דיווח:</strong> ' + escapeHtml(data.reportMonth) + '</div>'\r\n                    + '</div>';\r\n                var docs = data.documents || [];\r\n                if (!docs.length) {\r\n                    state.textContent = 'אין מסמכים להצגה';\r\n                    return;\r\n                }\r\n                rows.innerHTML = docs.map(function (doc) {\r\n                    return '<tr>'\r\n                        + '<td>' + escapeHtml(doc.fileName) + '</td>'\r\n                        + '<td>' + escapeHtml(doc.description || '-') + '</td>'\r\n                        + '<td>' + escapeHtml(doc.uploadedAt) + '</td>'\r\n                        + '<td>' + escapeHtml(doc.fileSize) + '</td>'\r\n                        + '<td class=\"text-nowrap\"><a class=\"btn btn-sm btn-outline-primary\" target=\"_blank\" rel=\"noopener\" href=\"' + escapeHtml(doc.viewUrl) + '\">צפייה</a> '\r\n                        + '<a class=\"btn btn-sm btn-outline-secondary\" href=\"' + escapeHtml(doc.downloadUrl) + '\">הורדה</a></td>'\r\n                        + '</tr>';\r\n                }).join('');\r\n                state.classList.add('d-none');\r\n                tableWrap.classList.remove('d-none');\r\n            })\r\n            .catch(function () {\r\n                state.textContent = 'לא ניתן לטעון את המסמכים כרגע';\r\n                state.classList.add('text-danger');\r\n            });\r\n    }\r\n\r\n    document.addEventListener('click', function (e) {\r\n        var link = e.target.closest('a.btn[href*=\"/Report?reportId=\"][href*=\"#documents\"]');\r\n        if (!link) return;\r\n        var url = new URL(link.getAttribute('href'), window.location.origin);\r\n        var reportId = url.searchParams.get('reportId');\r\n        var allocationId = url.searchParams.get('allocationId');\r\n        if (!reportId || !allocationId) return;\r\n        e.preventDefault();\r\n        showDashboardDocuments(reportId, allocationId);\r\n    });\r\n})();\r\n</script>\r\n");
		string SortLink(string key)
		{
			return Url.Action("Summary", new
			{
				Page = 1,
				PageSize = filter.PageSize,
				StatusId = filter.StatusId,
				DistrictId = filter.DistrictId,
				EmployeeName = filter.EmployeeName,
				FromMonthId = filter.FromMonthId,
				ToMonthId = filter.ToMonthId,
				SortBy = key,
				SortDesc = (filter.SortBy == key && !filter.SortDesc)
			}) ?? "#";
		}
	}
}
