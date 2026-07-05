using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Employee/AllocationList.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Employee_AllocationList : RazorPage<List<Allocation>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-controller", "Employee", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("asp-action", "CreateAllocation", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("class", new HtmlString("btn btn-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("asp-action", "Edit", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("value", "Unlimited", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("method", "get", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("class", new HtmlString("card card-body mb-3 allocation-filter"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("class", new HtmlString("btn btn-sm btn-primary detail-icon"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("title", new HtmlString("פרטי הקצאה"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("aria-label", new HtmlString("פרטי הקצאה"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<List<Allocation>> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "הקצאות עובדים";
		AllocationListFilterModel filter = (AllocationListFilterModel)base.ViewBag.Filter;
		int page = (int)base.ViewBag.Page;
		int totalPages = (int)base.ViewBag.TotalPages;
		int num = (int)base.ViewBag.PageSize;
		List<Project> projects = (base.ViewBag.Projects as List<Project>) ?? new List<Project>();
		List<AxiomaReporting.Core.Entities.Program> allPrograms = (base.ViewBag.AllPrograms as List<AxiomaReporting.Core.Entities.Program>) ?? new List<AxiomaReporting.Core.Entities.Program>();
		List<District> allDistricts = (base.ViewBag.AllDistricts as List<District>) ?? new List<District>();
		List<Sector> allSectors = (base.ViewBag.AllSectors as List<Sector>) ?? new List<Sector>();
		decimal[] durationOptions = (decimal[])base.ViewBag.OutputDurationOptions;
		string sortBy = base.ViewBag.SortBy as string;
		bool sortDesc = base.ViewBag.SortDesc ?? ((object)false);
		string listController = (base.ViewBag.AllocationListController as string) ?? "Allocations";
		string listAction = (base.ViewBag.AllocationListAction as string) ?? "Index";
		string exportAction = (base.ViewBag.AllocationExportAction as string) ?? "ExportExcel";
		string detailController = (base.ViewBag.AllocationDetailController as string) ?? "Allocations";
		string detailAction = (base.ViewBag.AllocationDetailAction as string) ?? "Details";
		User employeeContext = base.ViewBag.EmployeeContext as User;
		Dictionary<string, object?> baseRouteValues = new Dictionary<string, object>
		{
			["search"] = filter.Search,
			["employeeId"] = filter.EmployeeId,
			["projectId"] = filter.ProjectId,
			["idNumber"] = filter.IdNumber,
			["employeeCode"] = filter.EmployeeCode,
			["firstName"] = filter.FirstName,
			["lastName"] = filter.LastName,
			["monthlyEmploymentScope"] = filter.MonthlyEmploymentScope,
			["annualEmploymentScope"] = filter.AnnualEmploymentScope,
			["notes"] = filter.Notes,
			["showAll"] = filter.ShowAll,
			["pageSize"] = num
		};
		List<KeyValuePair<string, string>> extraRouteValues = new List<KeyValuePair<string, string>>();
		foreach (int programId in filter.ProgramIds)
		{
			extraRouteValues.Add(new KeyValuePair<string, string>("programIds", programId.ToString()));
		}
		foreach (int districtId in filter.DistrictIds)
		{
			extraRouteValues.Add(new KeyValuePair<string, string>("districtIds", districtId.ToString()));
		}
		foreach (int sectorId in filter.SectorIds)
		{
			extraRouteValues.Add(new KeyValuePair<string, string>("sectorIds", sectorId.ToString()));
		}
		foreach (string outputDuration in filter.OutputDurations)
		{
			extraRouteValues.Add(new KeyValuePair<string, string>("outputDurations", outputDuration));
		}
		WriteLiteral("\r\n<div class=\"container-fluid employee-allocations-list py-3\" dir=\"rtl\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3 flex-wrap gap-2\">\r\n    <div>\n      <h2 class=\"mb-0\">הקצאות עובדים</h2>\n");
		if (employeeContext != null)
		{
			WriteLiteral("        <div class=\"text-muted fw-bold\">");
			Write(employeeContext.FirstName);
			WriteLiteral(" ");
			Write(employeeContext.LastName);
			WriteLiteral(" קוד עובד ");
			Write(employeeContext.EmployeeCode);
			WriteLiteral(" ת.ז ");
			Write(DisplayIdNumber(employeeContext.IdNumber));
			WriteLiteral("</div>\n");
		}
		WriteLiteral("    </div>\n    <div class=\"d-flex gap-2 flex-wrap\">\n");
		if (employeeContext != null)
		{
			WriteLiteral("        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50415151", async delegate
			{
				WriteLiteral("הוסף הקצאה");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(employeeContext.Id);
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\n        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50417641", async delegate
			{
				WriteLiteral("חזרה לכרטיס עובד");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(employeeContext.Id);
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
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
			WriteLiteral("        <a href=\"/allocations/create\" class=\"btn btn-primary\">הוסף הקצאה</a>\n");
		}
		WriteLiteral("      <a");
		BeginWriteAttribute("href", " href=\"", 6139, "\"", 6159, 1);
		WriteAttributeValue("", 6146, ExportLink(), 6146, 13, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(" class=\"btn btn-success\">יצא לאקסל</a>\n    </div>\n  </div>\r\n\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50420832", async delegate
		{
			WriteLiteral("\r\n    <div class=\"filter-title h5 mb-3\">סנן לפי</div>\r\n    <div class=\"filter-grid\">\r\n      <div class=\"filter-field notes-field\">\r\n        <label for=\"f-notes\">הערות</label>\r\n        <input id=\"f-notes\" type=\"text\" name=\"notes\"");
			BeginWriteAttribute("value", " value=\"", 6517, "\"", 6538, 1);
			WriteAttributeValue("", 6525, filter.Notes, 6525, 13, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n      </div>\r\n\r\n      <div class=\"filter-field duration-field\">\r\n        <label for=\"f-durations\">משך תפוקה</label>\r\n        <select id=\"f-durations\" name=\"outputDurations\" multiple size=\"3\">\r\n");
			decimal[] array = durationOptions;
			for (int j = 0; j < array.Length; j++)
			{
				decimal d2 = array[j];
				string text2 = d2.ToString(CultureInfo.InvariantCulture);
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50422372", async delegate
				{
					Write(d2);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(text2);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 6924, filter.OutputDurations.Contains(text2), 6924, 38, isLiteral: false);
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
			WriteLiteral("          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50425071", async delegate
			{
				WriteLiteral("ללא הגבלה");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7036, filter.OutputDurations.Contains("Unlimited"), 7036, 47, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        </select>\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-annual\">היקף פעילות שנתי</label>\r\n        <input id=\"f-annual\" type=\"number\" step=\"1\" name=\"annualEmploymentScope\"");
			BeginWriteAttribute("value", "\r\n               value=\"", 7310, "\"", 7450, 1);
			WriteAttributeValue("", 7334, filter.AnnualEmploymentScope.HasValue ? decimal.Truncate(filter.AnnualEmploymentScope.Value).ToString("0") : null, 7334, 116, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-monthly\">היקף פעילות חודשי</label>\r\n        <input id=\"f-monthly\" type=\"number\" step=\"1\" name=\"monthlyEmploymentScope\"");
			BeginWriteAttribute("value", "\r\n               value=\"", 7646, "\"", 7788, 1);
			WriteAttributeValue("", 7670, filter.MonthlyEmploymentScope.HasValue ? decimal.Truncate(filter.MonthlyEmploymentScope.Value).ToString("0") : null, 7670, 118, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-last\">שם משפחה</label>\r\n        <input id=\"f-last\" type=\"text\" name=\"lastName\"");
			BeginWriteAttribute("value", " value=\"", 7944, "\"", 7968, 1);
			WriteAttributeValue("", 7952, filter.LastName, 7952, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-first\">שם פרטי</label>\r\n        <input id=\"f-first\" type=\"text\" name=\"firstName\"");
			BeginWriteAttribute("value", " value=\"", 8126, "\"", 8151, 1);
			WriteAttributeValue("", 8134, filter.FirstName, 8134, 17, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-code\">קוד עובד</label>\r\n        <input id=\"f-code\" type=\"text\" name=\"employeeCode\"");
			BeginWriteAttribute("value", " value=\"", 8311, "\"", 8339, 1);
			WriteAttributeValue("", 8319, filter.EmployeeCode, 8319, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-id\">ת.ז</label>\r\n        <input id=\"f-id\" type=\"text\" name=\"idNumber\"");
			BeginWriteAttribute("value", " value=\"", 8486, "\"", 8510, 1);
			WriteAttributeValue("", 8494, filter.IdNumber, 8494, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-sectors\">מגזר</label>\r\n        <select id=\"f-sectors\" name=\"sectorIds\" multiple size=\"3\">\r\n");
			foreach (Sector s in allSectors)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50431320", async delegate
				{
					Write(s.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(s.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 8778, filter.SectorIds.Contains(s.Id), 8778, 34, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-districts\">מחוז</label>\r\n        <select id=\"f-districts\" name=\"districtIds\" multiple size=\"3\">\r\n");
			foreach (District d in allDistricts)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50434500", async delegate
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
				AddHtmlAttributeValue("", 9141, filter.DistrictIds.Contains(d.Id), 9141, 36, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-programs\">תוכנית</label>\r\n        <select id=\"f-programs\" name=\"programIds\" multiple size=\"3\">\r\n");
			foreach (AxiomaReporting.Core.Entities.Program p3 in allPrograms)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50437680", async delegate
				{
					Write(p3.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(p3.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 9504, filter.ProgramIds.Contains(p3.Id), 9504, 35, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"filter-field\">\r\n        <label for=\"f-project\">פרויקט</label>\r\n        <select id=\"f-project\" name=\"projectId\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50440576", async delegate
			{
				WriteLiteral("הכל");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (Project p2 in projects)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50442088", async delegate
				{
					Write(p2.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(p2.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 9883, filter.ProjectId == p2.Id, 9883, 27, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n    </div>\r\n\r\n    <div class=\"filter-actions\">\r\n      <label class=\"show-all\">\r\n        <input type=\"checkbox\" name=\"showAll\" value=\"true\" ");
			Write(filter.ShowAll ? "checked" : "");
			WriteLiteral(" />\r\n        הצג הכל\r\n      </label>\r\n      <button type=\"submit\" class=\"btn btn-primary\">הצג</button>\r\n      <a");
			BeginWriteAttribute("href", " href=\"", 10268, "\"", 10288, 1);
			WriteAttributeValue("", 10275, ExportLink(), 10275, 13, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"btn btn-success\">יצא לאקסל</a>\r\n      <a");
			BeginWriteAttribute("href", " href=\"", 10337, "\"", 10383, 1);
			WriteAttributeValue("", 10344, Url.Action(listAction, listController), 10344, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"btn btn-outline-secondary\">נקה</a>\r\n    </div>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n\r\n  <div class=\"allocation-state allocation-loading\" hidden role=\"status\">טוען הקצאות...</div>\r\n  <div class=\"allocation-state allocation-error\" hidden role=\"alert\">אירעה שגיאה בטעינת ההקצאות</div>\r\n\r\n  <div class=\"table-responsive alloc-grid-wrap\">\r\n    <table class=\"table table-striped table-hover table-bordered align-middle alloc-grid\">\r\n      <thead class=\"table-dark\">\r\n        <tr>\r\n          <th class=\"icon-col\"></th>\r\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 10896, "\"", 10923, 1);
		WriteAttributeValue("", 10903, SortLink("project"), 10903, 20, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">פרויקט</a></th>\r\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 10958, "\"", 10986, 1);
		WriteAttributeValue("", 10965, SortLink("programs"), 10965, 21, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">תוכנית</a></th>\r\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11021, "\"", 11050, 1);
		WriteAttributeValue("", 11028, SortLink("districts"), 11028, 22, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">מחוז</a></th>\r\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11083, "\"", 11110, 1);
		WriteAttributeValue("", 11090, SortLink("sectors"), 11090, 20, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">מגזר</a></th>\r\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11143, "\"", 11171, 1);
		WriteAttributeValue("", 11150, SortLink("idnumber"), 11150, 21, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">ת.ז</a></th>\r\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11203, "\"", 11227, 1);
		WriteAttributeValue("", 11210, SortLink("code"), 11210, 17, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">קוד עובד</a></th>\r\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11264, "\"", 11293, 1);
		WriteAttributeValue("", 11271, SortLink("firstname"), 11271, 22, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">שם פרטי</a></th>\r\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11329, "\"", 11357, 1);
		WriteAttributeValue("", 11336, SortLink("lastname"), 11336, 21, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">שם משפחה</a></th>\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11393, "\"", 11425, 1);
		WriteAttributeValue("", 11400, SortLink("monthlyscope"), 11400, 25, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">היקף פעילות חודשי</a></th>\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11470, "\"", 11500, 1);
		WriteAttributeValue("", 11477, SortLink("dailyscope"), 11477, 23, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">היקף פעילות יומי</a></th>\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11544, "\"", 11575, 1);
		WriteAttributeValue("", 11551, SortLink("annualscope"), 11551, 24, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">היקף פעילות שנתי</a></th>\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11619, "\"", 11650, 1);
		WriteAttributeValue("", 11626, SortLink("monthlyrows"), 11626, 24, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">הקצאת שורות חודשית</a></th>\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11696, "\"", 11726, 1);
		WriteAttributeValue("", 11703, SortLink("annualrows"), 11703, 23, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">הקצאת שורות שנתית</a></th>\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11771, "\"", 11805, 1);
		WriteAttributeValue("", 11778, SortLink("outputduration"), 11778, 27, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">משך תפוקה</a></th>\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11842, "\"", 11878, 1);
		WriteAttributeValue("", 11849, SortLink("allowexcelupload"), 11849, 29, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">העלאת אקסל</a></th>\n          <th><a");
		BeginWriteAttribute("href", " href=\"", 11916, "\"", 11941, 1);
		WriteAttributeValue("", 11923, SortLink("notes"), 11923, 18, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(">הערות</a></th>\n        </tr>\n      </thead>\n      <tbody>\r\n");
		foreach (Allocation a in base.Model)
		{
			WriteLiteral("          <tr>\r\n            <td class=\"icon-col\">\r\n");
			if (a.UserId > 0)
			{
				WriteLiteral("                ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "682e0199922ac1bdfb193fe80c0afdb07f0ce251b34ac4db5c258c91748ee50456062", async delegate
				{
					WriteLiteral("פרטים");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(detailController);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(detailAction);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, HtmlAttributeValueStyle.DoubleQuotes);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-allocationId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(a.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-allocationId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"], HtmlAttributeValueStyle.DoubleQuotes);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
			}
			WriteLiteral("            </td>\r\n            <td><span>");
			Write(a.Project?.Description);
			WriteLiteral("</span></td>\r\n            <td><span>");
			Write(FilteredJoin<AllocationProgram>(a.AllocationPrograms, (AllocationProgram x) => x.ProgramId, (AllocationProgram x) => x.Program?.Description, filter.ProgramIds));
			WriteLiteral("</span></td>\r\n            <td><span>");
			Write(FilteredJoin<AllocationDistrict>(a.AllocationDistricts, (AllocationDistrict x) => x.DistrictId, (AllocationDistrict x) => x.District?.Description, filter.DistrictIds));
			WriteLiteral("</span></td>\r\n            <td><span>");
			Write(FilteredJoin<AllocationSector>(a.AllocationSectors, (AllocationSector x) => x.SectorId, (AllocationSector x) => x.Sector?.Description, filter.SectorIds));
			WriteLiteral("</span></td>\r\n            <td><span>");
			Write(DisplayIdNumber(a.User?.IdNumber));
			WriteLiteral("</span></td>\n            <td><span>");
			Write(a.User?.EmployeeCode);
			WriteLiteral("</span></td>\r\n            <td><span>");
			Write(a.User?.FirstName);
			WriteLiteral("</span></td>\r\n            <td><span>");
			Write(a.User?.LastName);
			WriteLiteral("</span></td>\n            <td><span>");
			Write(Whole(a.MonthlyEmploymentScope));
			WriteLiteral("</span></td>\n            <td><span>");
			Write(Daily(a.DailyEmploymentScope));
			WriteLiteral("</span></td>\n            <td><span>");
			Write(Whole(a.AnnualEmploymentScope));
			WriteLiteral("</span></td>\n            <td><span>");
			Write(Int(a.MonthlyRowAllocation));
			WriteLiteral("</span></td>\n            <td><span>");
			Write(Int(a.AnnualRowAllocation));
			WriteLiteral("</span></td>\n            <td><span>");
			Write(FilteredDuration(a.OutputDuration));
			WriteLiteral("</span></td>\n            <td><span>");
			Write(YesNo(a.AllowExcelUpload));
			WriteLiteral("</span></td>\n            <td><span>");
			Write(string.IsNullOrWhiteSpace(a.Notes) ? "" : a.Notes);
			WriteLiteral("</span></td>\n          </tr>\n");
		}
		if (!base.Model.Any())
		{
			WriteLiteral("          <tr>\n            <td colspan=\"17\" class=\"empty-row text-center text-muted py-4\">לא נמצאו הקצאות עובדים</td>\n          </tr>\n");
		}
		WriteLiteral("      </tbody>\r\n    </table>\r\n  </div>\r\n\r\n");
		if (totalPages > 1)
		{
			WriteLiteral("    <nav aria-label=\"עמוד\" class=\"mt-3\">\r\n      <ul class=\"pagination justify-content-center\">\r\n        <li");
			BeginWriteAttribute("class", " class=\"", 14043, "\"", 14091, 2);
			WriteAttributeValue("", 14051, "page-item", 14051, 9, isLiteral: true);
			WriteAttributeValue(" ", 14060, (page <= 1) ? "disabled" : "", 14061, 30, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n          <a class=\"page-link\"");
			BeginWriteAttribute("href", " href=\"", 14125, "\"", 14151, 1);
			WriteAttributeValue("", 14132, PageLink(page - 1), 14132, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">הקודם</a>\r\n        </li>\r\n");
			for (int i = Math.Max(1, page - 2); i <= Math.Min(totalPages, page + 2); i++)
			{
				WriteLiteral("          <li");
				BeginWriteAttribute("class", " class=\"", 14291, "\"", 14337, 2);
				WriteAttributeValue("", 14299, "page-item", 14299, 9, isLiteral: true);
				WriteAttributeValue(" ", 14308, (i == page) ? "active" : "", 14309, 28, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">\r\n            <a class=\"page-link\"");
				BeginWriteAttribute("href", " href=\"", 14373, "\"", 14392, 1);
				WriteAttributeValue("", 14380, PageLink(i), 14380, 12, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">");
				Write(i);
				WriteLiteral("</a>\r\n          </li>\r\n");
			}
			WriteLiteral("        <li");
			BeginWriteAttribute("class", " class=\"", 14441, "\"", 14498, 2);
			WriteAttributeValue("", 14449, "page-item", 14449, 9, isLiteral: true);
			WriteAttributeValue(" ", 14458, (page >= totalPages) ? "disabled" : "", 14459, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n          <a class=\"page-link\"");
			BeginWriteAttribute("href", " href=\"", 14532, "\"", 14558, 1);
			WriteAttributeValue("", 14539, PageLink(page + 1), 14539, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">הבא</a>\r\n        </li>\r\n      </ul>\r\n    </nav>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n<style>\r\n  .allocation-filter .filter-title {\r\n    font-weight: 700;\r\n  }\r\n\r\n  .filter-grid {\r\n    display: grid;\r\n    grid-template-columns: repeat(4, minmax(180px, 1fr));\r\n    gap: 1rem;\r\n    align-items: end;\r\n  }\r\n\r\n  .filter-field label {\r\n    display: block;\r\n    margin-bottom: .35rem;\r\n    font-weight: 600;\r\n  }\r\n\r\n  .filter-field input,\r\n  .filter-field select {\r\n    width: 100%;\r\n    display: block;\r\n    padding: .375rem .75rem;\r\n    font-size: 1rem;\r\n    font-weight: 400;\r\n    line-height: 1.5;\r\n    color: #212529;\r\n    background-color: #fff;\r\n    background-clip: padding-box;\r\n    border: 1px solid #ced4da;\r\n    border-radius: .375rem;\r\n  }\r\n\r\n  .filter-actions {\r\n    display: flex;\r\n    justify-content: flex-start;\r\n    align-items: center;\r\n    gap: .75rem;\r\n    margin-top: 1rem;\r\n    flex-wrap: wrap;\r\n  }\r\n\r\n  .show-all {\r\n    display: inline-flex;\r\n    align-items: center;\r\n    gap: .5rem;\r\n    font-weight: 500;\r\n  }\r\n\r\n  .alloc-grid-wrap {\r\n    max-height: 60vh;\r\n    overflow: auto;");
		WriteLiteral("\r\n  }\r\n\r\n  .alloc-grid {\n    min-width: 1560px;\n  }\n\r\n  .alloc-grid th a,\r\n  .alloc-grid td span {\r\n    color: inherit;\r\n    text-decoration: none;\r\n  }\r\n\r\n  .alloc-grid .icon-col {\r\n    width: 82px;\r\n    white-space: nowrap;\r\n  }\r\n\r\n  .detail-icon {\r\n    min-width: 62px;\r\n  }\r\n\r\n  .empty-row {\r\n    text-align: center;\r\n    padding: 2rem !important;\r\n    color: #64748b;\r\n  }\r\n\r\n  .allocation-state {\r\n    text-align: center;\r\n    font-weight: 800;\r\n    padding: 1rem;\r\n    color: #475569;\r\n  }\r\n\r\n  ");
		WriteLiteral("@media (max-width: 1200px) {\r\n    .filter-grid {\r\n      grid-template-columns: repeat(2, minmax(0, 1fr));\r\n    }\r\n  }\r\n\r\n  ");
		WriteLiteral("@media (max-width: 767.98px) {\r\n    .filter-grid {\r\n      grid-template-columns: 1fr;\r\n    }\r\n  }\r\n</style>\r\n\r\n<script>\r\n  document.querySelector('.allocation-filter')?.addEventListener('submit', () => {\r\n    const loading = document.querySelector('.allocation-loading');\r\n    if (loading) loading.hidden = false;\r\n  });\r\n</script>\r\n");
		string BuildUrl(string action, IDictionary<string, object?> overrides)
		{
			List<string> list2 = new List<string>();
			Dictionary<string, object> dictionary = new Dictionary<string, object>(baseRouteValues);
			foreach (KeyValuePair<string, object> @override in overrides)
			{
				dictionary[@override.Key] = @override.Value;
			}
			foreach (KeyValuePair<string, object> item in dictionary)
			{
				if (item.Value != null)
				{
					string text = item.Value.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						object value2 = item.Value;
						if (!(value2 is bool) || (bool)value2 || !(item.Key != "sortDesc") || !(item.Key != "showAll"))
						{
							list2.Add(Uri.EscapeDataString(item.Key) + "=" + Uri.EscapeDataString(text));
						}
					}
				}
			}
			foreach (KeyValuePair<string, string> item2 in extraRouteValues)
			{
				list2.Add(Uri.EscapeDataString(item2.Key) + "=" + Uri.EscapeDataString(item2.Value));
			}
			return Url.Action(action, listController) + ((list2.Count > 0) ? ("?" + string.Join("&", list2)) : "");
		}
		static string Daily(decimal? value)
		{
			if (!value.HasValue)
			{
				return "ללא הגבלה";
			}
			return value.Value.ToString("0.##");
		}
		static string DisplayIdNumber(string? value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				if (!Regex.IsMatch(value, "[A-Za-z]"))
				{
					return value;
				}
				return "מנהל מערכת";
			}
			return "";
		}
		string ExportLink()
		{
			return BuildUrl(exportAction, new Dictionary<string, object>
			{
				["sortBy"] = sortBy,
				["sortDesc"] = sortDesc
			});
		}
		string FilteredDuration(string? raw)
		{
			if (string.IsNullOrWhiteSpace(raw))
			{
				return "";
			}
			List<string> source = raw.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
			if (!filter.ShowAll)
			{
				List<string> outputDurations = filter.OutputDurations;
				if (outputDurations != null && outputDurations.Count > 0)
				{
					source = source.Where((string t) => filter.OutputDurations.Contains(t)).ToList();
				}
			}
			source = source.Select((string t) => (!t.Equals("Unlimited", StringComparison.OrdinalIgnoreCase)) ? t : "ללא הגבלה").ToList();
			if (source.Count != 0)
			{
				return string.Join(", ", source);
			}
			return "";
		}
		string FilteredJoin<T>(IEnumerable<T> items, Func<T, int> idSelector, Func<T, string?> descSelector, List<int> selectedIds) where T : notnull
		{
			List<int> selectedIds2 = selectedIds;
			Func<T, int> idSelector2 = idSelector;
			if (!filter.ShowAll && selectedIds2 != null && selectedIds2.Count > 0)
			{
				items = items.Where((T x) => selectedIds2.Contains(idSelector2(x)));
			}
			return JoinValues(items.Select(descSelector));
		}
		static string Int(int? value)
		{
			if (!value.HasValue)
			{
				return "";
			}
			return value.Value.ToString();
		}
		static string JoinValues(IEnumerable<string?> values)
		{
			List<string> list = values.Where((string v) => !string.IsNullOrWhiteSpace(v)).Distinct().ToList();
			if (list.Count != 0)
			{
				if (list.Count > 4)
				{
					return string.Join(", ", list.Take(4)) + $" (+{list.Count - 4})";
				}
				return string.Join(", ", list);
			}
			return "";
		}
		string PageLink(int p)
		{
			return BuildUrl(listAction, new Dictionary<string, object>
			{
				["sortBy"] = sortBy,
				["sortDesc"] = sortDesc,
				["page"] = p
			});
		}
		string SortLink(string key)
		{
			return BuildUrl(listAction, new Dictionary<string, object>
			{
				["sortBy"] = key,
				["sortDesc"] = sortBy == key && !sortDesc,
				["page"] = 1
			});
		}
		static string Whole(decimal? value)
		{
			if (!value.HasValue)
			{
				return "";
			}
			return decimal.Truncate(value.Value).ToString("0");
		}
		static string YesNo(bool value)
		{
			if (!value)
			{
				return "לא";
			}
			return "כן";
		}
	}
}

