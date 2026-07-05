using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Web.Helpers;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Employee/Index.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Employee_Index : RazorPage<List<User>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "Create", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-controller", "Allocations", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("value", "true", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("value", "false", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("method", "get", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("class", new HtmlString("card card-body mb-3"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("asp-action", "Edit", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("class", new HtmlString("btn btn-sm btn-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("title", new HtmlString("עריכת כרטיס"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("asp-action", "Allocations", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_14 = new TagHelperAttribute("class", new HtmlString("btn btn-sm btn-success"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_15 = new TagHelperAttribute("title", new HtmlString("הקצאות"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_16 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_17 = new TagHelperAttribute("style", new HtmlString("display:inline"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_18 = new TagHelperAttribute("onsubmit", new HtmlString("return confirm('לאפס סיסמה למספר הזהות?')"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_19 = new TagHelperAttribute("onsubmit", new HtmlString("return confirm('לשחרר את נעילת החשבון?')"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_20 = new TagHelperAttribute("onsubmit", new HtmlString("return confirm('להשבית עובד זה?')"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_21 = new TagHelperAttribute("asp-action", "BulkAction", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_22 = new TagHelperAttribute("id", new HtmlString("bulkForm"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<List<User>> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "ניהול עובדים";
		EmployeeListFilterModel filter = (base.ViewBag.Filter as EmployeeListFilterModel) ?? new EmployeeListFilterModel();
		int page = (int)base.ViewBag.Page;
		int totalPages = (int)base.ViewBag.TotalPages;
		int pageSize = (int)base.ViewBag.PageSize;
		List<UserStatus> statuses = (List<UserStatus>)base.ViewBag.Statuses;
		List<UserRole> userRoles = (List<UserRole>)base.ViewBag.UserRoles;
		List<Project> projects = (base.ViewBag.Projects as List<Project>) ?? new List<Project>();
		List<District> allDistricts = (base.ViewBag.AllDistricts as List<District>) ?? new List<District>();
		List<AxiomaReporting.Core.Entities.Program> allPrograms = (base.ViewBag.AllPrograms as List<AxiomaReporting.Core.Entities.Program>) ?? new List<AxiomaReporting.Core.Entities.Program>();
		List<Sector> allSectors = (base.ViewBag.AllSectors as List<Sector>) ?? new List<Sector>();
		string sortBy = base.ViewBag.SortBy as string;
		bool sortDesc = base.ViewBag.SortDesc ?? ((object)false);
		Dictionary<string, object?> baseRouteValues = new Dictionary<string, object>
		{
			["search"] = filter.Search,
			["idNumber"] = filter.IdNumber,
			["employeeCode"] = filter.EmployeeCode,
			["firstName"] = filter.FirstName,
			["lastName"] = filter.LastName,
			["notes"] = filter.Notes,
			["statusId"] = filter.StatusId,
			["roleId"] = filter.RoleId,
			["restDay"] = filter.RestDay,
			["allowFutureReporting"] = filter.AllowFutureReporting,
			["hasAllocations"] = filter.HasAllocations,
			["lockedOnly"] = (filter.LockedOnly ? "true" : null),
			["projectId"] = filter.ProjectId,
			["pageSize"] = pageSize
		};
		List<KeyValuePair<string, string>> extraRouteValues = new List<KeyValuePair<string, string>>();
		foreach (int districtId in filter.DistrictIds)
		{
			extraRouteValues.Add(new KeyValuePair<string, string>("districtIds", districtId.ToString()));
		}
		foreach (int programId in filter.ProgramIds)
		{
			extraRouteValues.Add(new KeyValuePair<string, string>("programIds", programId.ToString()));
		}
		foreach (int sectorId in filter.SectorIds)
		{
			extraRouteValues.Add(new KeyValuePair<string, string>("sectorIds", sectorId.ToString()));
		}
		WriteLiteral("\r\n<div class=\"container-fluid py-3\" dir=\"rtl\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h2>ניהול עובדים</h2>\r\n    <div class=\"d-flex gap-2 flex-wrap\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79515539", async delegate
		{
			WriteLiteral("+ הוסף עובד");
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
		WriteLiteral("\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79516813", async delegate
		{
			WriteLiteral("רשימת הקצאות");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\n      <a");
		BeginWriteAttribute("href", " href=\"", 4266, "\"", 4286, 1);
		WriteAttributeValue("", 4273, ExportLink(), 4273, 13, isLiteral: false);
		EndWriteAttribute();
		WriteLiteral(" class=\"btn btn-success\">ייצוא אקסל</a>\r\n    </div>\r\n  </div>\r\n\r\n");
		if (base.TempData["Success"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\" aria-live=\"polite\">\r\n      ");
			Write(base.TempData["Success"]);
			WriteLiteral("\r\n      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n    </div>\r\n");
		}
		if (base.TempData["Error"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\" aria-live=\"polite\">\r\n      ");
			Write(base.TempData["Error"]);
			WriteLiteral("\r\n      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79520622", async delegate
		{
			WriteLiteral("\r\n    <div class=\"row g-2 align-items-end\">\r\n\r\n      <div class=\"col-md-3\">\r\n        <label class=\"form-label\" for=\"f-search\">חיפוש כללי</label>\r\n        <input id=\"f-search\" type=\"text\" name=\"search\"");
			BeginWriteAttribute("value", " value=\"", 5180, "\"", 5202, 1);
			WriteAttributeValue("", 5188, filter.Search, 5188, 14, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control\"\r\n               placeholder=\"שם, ת.ז., קוד עובד...\" />\r\n      </div>\r\n\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-id\">ת.ז.</label>\r\n        <input id=\"f-id\" type=\"text\" name=\"idNumber\"");
			BeginWriteAttribute("value", " value=\"", 5438, "\"", 5462, 1);
			WriteAttributeValue("", 5446, filter.IdNumber, 5446, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control\" />\r\n      </div>\r\n\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-code\">קוד עובד</label>\r\n        <input id=\"f-code\" type=\"text\" name=\"employeeCode\"");
			BeginWriteAttribute("value", " value=\"", 5658, "\"", 5686, 1);
			WriteAttributeValue("", 5666, filter.EmployeeCode, 5666, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control\" />\r\n      </div>\r\n\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-first\">שם פרטי</label>\r\n        <input id=\"f-first\" type=\"text\" name=\"firstName\"");
			BeginWriteAttribute("value", " value=\"", 5880, "\"", 5905, 1);
			WriteAttributeValue("", 5888, filter.FirstName, 5888, 17, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control\" />\r\n      </div>\r\n\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-last\">שם משפחה</label>\r\n        <input id=\"f-last\" type=\"text\" name=\"lastName\"");
			BeginWriteAttribute("value", " value=\"", 6097, "\"", 6121, 1);
			WriteAttributeValue("", 6105, filter.LastName, 6105, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control\" />\r\n      </div>\r\n\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-status\">סטטוס</label>\r\n        <select id=\"f-status\" name=\"statusId\" class=\"form-select\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79524402", async delegate
			{
				WriteLiteral("-- כל הסטטוסים --");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (UserStatus s3 in statuses)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79525919", async delegate
				{
					Write(s3.DescriptionHebrew ?? s3.Name);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(s3.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 6478, filter.StatusId == s3.Id, 6478, 26, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-role\">תפקיד מערכת</label>\r\n        <select id=\"f-role\" name=\"roleId\" class=\"form-select\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79528822", async delegate
			{
				WriteLiteral("-- כל התפקידים --");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (UserRole r in userRoles)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79530340", async delegate
				{
					Write(r.DescriptionHebrew ?? r.Name);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(r.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 6912, filter.RoleId == r.Id, 6912, 24, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-restday\">יום מנוחה</label>\r\n        <select id=\"f-restday\" name=\"restDay\" class=\"form-select\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79533246", async delegate
			{
				WriteLiteral("-- הכל --");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (SelectListItem rd in SelectListProviders.RestDayOptions.Skip(1))
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79534790", async delegate
				{
					Write(rd.Text);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(rd.Value);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 7379, filter.RestDay?.ToString() == rd.Value, 7379, 41, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-future\">דיווח עתידי</label>\r\n        <select id=\"f-future\" name=\"allowFutureReporting\" class=\"form-select\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79537708", async delegate
			{
				WriteLiteral("-- הכל --");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7706, !filter.AllowFutureReporting.HasValue, 7706, 40, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79539510", async delegate
			{
				WriteLiteral("כן");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7809, filter.AllowFutureReporting.GetValueOrDefault(), 7809, 38, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79541303", async delegate
			{
				WriteLiteral("לא");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_7.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 7904, filter.AllowFutureReporting == false, 7904, 39, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\" for=\"f-hasalloc\">יש הקצאות</label>\r\n        <select id=\"f-hasalloc\" name=\"hasAllocations\" class=\"form-select\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79543332", async delegate
			{
				WriteLiteral("-- הכל --");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 8206, !filter.HasAllocations.HasValue, 8206, 34, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79545128", async delegate
			{
				WriteLiteral("כן");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 8303, filter.HasAllocations.GetValueOrDefault(), 8303, 32, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79546915", async delegate
			{
				WriteLiteral("לא");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_7.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
			BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
			AddHtmlAttributeValue("", 8392, filter.HasAllocations == false, 8392, 33, isLiteral: false);
			EndAddHtmlAttributeValues(__tagHelperExecutionContext);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-3\">\r\n        <label class=\"form-label\" for=\"f-project\">פרויקט</label>\r\n        <select id=\"f-project\" name=\"projectId\" class=\"form-select\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79548928", async delegate
			{
				WriteLiteral("-- כל הפרויקטים --");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (Project p4 in projects)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79550446", async delegate
				{
					Write(p4.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(p4.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 8794, filter.ProjectId == p4.Id, 8794, 27, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-3\">\r\n        <label class=\"form-label\" for=\"f-districts\">מחוז</label>\r\n        <select id=\"f-districts\" name=\"districtIds\" multiple class=\"form-select\" size=\"3\"\r\n                aria-label=\"סינון לפי מחוז (בחירה מרובה)\">\r\n");
			foreach (District d in allDistricts)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79553663", async delegate
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
				AddHtmlAttributeValue("", 9244, filter.DistrictIds.Contains(d.Id), 9244, 36, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-3\">\r\n        <label class=\"form-label\" for=\"f-programs\">תוכנית</label>\r\n        <select id=\"f-programs\" name=\"programIds\" multiple class=\"form-select\" size=\"3\"\r\n                aria-label=\"סינון לפי תוכנית (בחירה מרובה)\">\r\n");
			foreach (AxiomaReporting.Core.Entities.Program p3 in allPrograms)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79556889", async delegate
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
				AddHtmlAttributeValue("", 9703, filter.ProgramIds.Contains(p3.Id), 9703, 35, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-3\">\r\n        <label class=\"form-label\" for=\"f-sectors\">מגזר</label>\r\n        <select id=\"f-sectors\" name=\"sectorIds\" multiple class=\"form-select\" size=\"3\"\r\n                aria-label=\"סינון לפי מגזר (בחירה מרובה)\">\r\n");
			foreach (Sector s2 in allSectors)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79560106", async delegate
				{
					Write(s2.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(s2.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 10153, filter.SectorIds.Contains(s2.Id), 10153, 34, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-3\">\r\n        <label class=\"form-label\" for=\"f-notes\">הערות</label>\r\n        <input id=\"f-notes\" type=\"text\" name=\"notes\"");
			BeginWriteAttribute("value", " value=\"", 10407, "\"", 10428, 1);
			WriteAttributeValue("", 10415, filter.Notes, 10415, 13, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"form-control\" />\r\n      </div>\r\n\r\n      <div class=\"col-md-2 d-flex align-items-center pt-3\">\r\n        <div class=\"form-check\">\r\n          <input id=\"f-locked\" class=\"form-check-input\" type=\"checkbox\"\r\n                 name=\"lockedOnly\" value=\"true\" ");
			Write(filter.LockedOnly ? "checked" : "");
			WriteLiteral(" />\r\n          <label class=\"form-check-label\" for=\"f-locked\">רק חשבונות נעולים</label>\r\n        </div>\r\n      </div>\r\n\r\n      <div class=\"col-md-1\">\r\n        <label class=\"form-label\" for=\"f-pageSize\">שורות</label>\r\n        <select id=\"f-pageSize\" name=\"pageSize\" class=\"form-select\">\r\n");
			int[] array = new int[4] { 10, 25, 50, 100 };
			foreach (int size in array)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79564538", async delegate
				{
					Write(size);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(size);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 11128, pageSize == size, 11128, 19, isLiteral: false);
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
			WriteLiteral("        </select>\r\n      </div>\r\n\r\n      <div class=\"col-md-2 d-flex gap-2\">\r\n        <button type=\"submit\" class=\"btn btn-secondary w-100\">חפש</button>\r\n        <a");
			BeginWriteAttribute("href", " href=\"", 11342, "\"", 11381, 1);
			WriteAttributeValue("", 11349, Url.Action("Index", "Employee"), 11349, 32, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"btn btn-outline-secondary w-100\">נקה</a>\n      </div>\r\n    </div>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79569147", async delegate
		{
			WriteLiteral("\r\n    ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n");
			WriteLiteral("    <input type=\"hidden\" name=\"search\"");
			BeginWriteAttribute("value", " value=\"", 11692, "\"", 11714, 1);
			WriteAttributeValue("", 11700, filter.Search, 11700, 14, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n    <input type=\"hidden\" name=\"idNumber\"");
			BeginWriteAttribute("value", " value=\"", 11760, "\"", 11784, 1);
			WriteAttributeValue("", 11768, filter.IdNumber, 11768, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n    <input type=\"hidden\" name=\"employeeCode\"");
			BeginWriteAttribute("value", " value=\"", 11834, "\"", 11862, 1);
			WriteAttributeValue("", 11842, filter.EmployeeCode, 11842, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n    <input type=\"hidden\" name=\"firstName\"");
			BeginWriteAttribute("value", " value=\"", 11909, "\"", 11934, 1);
			WriteAttributeValue("", 11917, filter.FirstName, 11917, 17, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n    <input type=\"hidden\" name=\"lastName\"");
			BeginWriteAttribute("value", " value=\"", 11980, "\"", 12004, 1);
			WriteAttributeValue("", 11988, filter.LastName, 11988, 16, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n    <input type=\"hidden\" name=\"notes\"");
			BeginWriteAttribute("value", " value=\"", 12047, "\"", 12068, 1);
			WriteAttributeValue("", 12055, filter.Notes, 12055, 13, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n");
			if (filter.StatusId.HasValue)
			{
				WriteLiteral(" <input type=\"hidden\" name=\"statusId\"");
				BeginWriteAttribute("value", " value=\"", 12147, "\"", 12171, 1);
				WriteAttributeValue("", 12155, filter.StatusId, 12155, 16, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" /> ");
			}
			if (filter.RoleId.HasValue)
			{
				WriteLiteral(" <input type=\"hidden\" name=\"roleId\"");
				BeginWriteAttribute("value", " value=\"", 12248, "\"", 12270, 1);
				WriteAttributeValue("", 12256, filter.RoleId, 12256, 14, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" /> ");
			}
			if (filter.RestDay.HasValue)
			{
				WriteLiteral(" <input type=\"hidden\" name=\"restDay\"");
				BeginWriteAttribute("value", " value=\"", 12349, "\"", 12372, 1);
				WriteAttributeValue("", 12357, filter.RestDay, 12357, 15, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" /> ");
			}
			if (filter.AllowFutureReporting.HasValue)
			{
				WriteLiteral(" <input type=\"hidden\" name=\"allowFutureReporting\"");
				BeginWriteAttribute("value", " value=\"", 12477, "\"", 12549, 1);
				WriteAttributeValue("", 12485, filter.AllowFutureReporting.Value.ToString().ToLowerInvariant(), 12485, 64, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" /> ");
			}
			if (filter.HasAllocations.HasValue)
			{
				WriteLiteral(" <input type=\"hidden\" name=\"hasAllocations\"");
				BeginWriteAttribute("value", " value=\"", 12642, "\"", 12708, 1);
				WriteAttributeValue("", 12650, filter.HasAllocations.Value.ToString().ToLowerInvariant(), 12650, 58, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" /> ");
			}
			if (filter.LockedOnly)
			{
				WriteLiteral(" <input type=\"hidden\" name=\"lockedOnly\" value=\"true\" /> ");
			}
			WriteLiteral("\r\n    <div class=\"card mb-3 d-none\" id=\"bulkBar\">\r\n      <div class=\"card-body d-flex align-items-center gap-3 flex-wrap\">\r\n        <span class=\"fw-bold\">פעולה קבוצתית:</span>\r\n        <select name=\"newStatusId\" class=\"form-select w-auto\">\r\n");
			foreach (UserStatus s in statuses)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79578172", async delegate
				{
					Write(s.DescriptionHebrew ?? s.Name);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(s.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
			}
			WriteLiteral("        </select>\r\n        <button type=\"submit\" class=\"btn btn-warning\">עדכון סטטוס</button>\r\n        <select name=\"bulkProjectId\" class=\"form-select w-auto\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79580501", async delegate
			{
				WriteLiteral("בחר פרויקט להקצאה");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
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
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc79582018", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
			}
			WriteLiteral("        </select>\r\n        <button type=\"submit\"");
			BeginWriteAttribute("formaction", " formaction=\"", 13577, "\"", 13622, 1);
			WriteAttributeValue("", 13590, Url.Action("BulkAddAllocation"), 13590, 32, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" class=\"btn btn-outline-success\">\r\n          הוסף הקצאה\r\n        </button>\r\n        <span id=\"selectedCount\" class=\"text-muted\"></span>\r\n      </div>\r\n    </div>\r\n\r\n    <div class=\"table-responsive\">\r\n      <table class=\"table table-striped table-hover align-middle\">\r\n        <thead class=\"table-dark\">\r\n          <tr>\r\n");
			WriteLiteral("            <th scope=\"col\" aria-sort=\"none\">\r\n              <span class=\"visually-hidden\">פעולות</span>\r\n            </th>\r\n            <th scope=\"col\"><input type=\"checkbox\" id=\"selectAll\" aria-label=\"בחר הכל\" /></th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 14290, "\"", 14327, 1);
			WriteAttributeValue("", 14302, AriaSort("employeecode"), 14302, 25, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 14366, "\"", 14398, 1);
			WriteAttributeValue("", 14373, SortLink("employeecode"), 14373, 25, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">קוד עובד</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 14460, "\"", 14493, 1);
			WriteAttributeValue("", 14472, AriaSort("idnumber"), 14472, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 14532, "\"", 14560, 1);
			WriteAttributeValue("", 14539, SortLink("idnumber"), 14539, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">ת.ז.</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 14618, "\"", 14652, 1);
			WriteAttributeValue("", 14630, AriaSort("firstname"), 14630, 22, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 14691, "\"", 14720, 1);
			WriteAttributeValue("", 14698, SortLink("firstname"), 14698, 22, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">שם פרטי</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 14781, "\"", 14814, 1);
			WriteAttributeValue("", 14793, AriaSort("lastname"), 14793, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 14853, "\"", 14881, 1);
			WriteAttributeValue("", 14860, SortLink("lastname"), 14860, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">שם משפחה</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 14943, "\"", 14980, 1);
			WriteAttributeValue("", 14955, AriaSort("employeerole"), 14955, 25, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 15019, "\"", 15051, 1);
			WriteAttributeValue("", 15026, SortLink("employeerole"), 15026, 25, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">תפקיד</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 15110, "\"", 15143, 1);
			WriteAttributeValue("", 15122, AriaSort("userrole"), 15122, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 15182, "\"", 15210, 1);
			WriteAttributeValue("", 15189, SortLink("userrole"), 15189, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">תפקיד מערכת</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 15275, "\"", 15306, 1);
			WriteAttributeValue("", 15287, AriaSort("status"), 15287, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 15345, "\"", 15371, 1);
			WriteAttributeValue("", 15352, SortLink("status"), 15352, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">סטטוס</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 15430, "\"", 15474, 1);
			WriteAttributeValue("", 15442, AriaSort("isreportingemployee"), 15442, 32, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 15513, "\"", 15552, 1);
			WriteAttributeValue("", 15520, SortLink("isreportingemployee"), 15520, 32, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">עובד מדווח</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 15616, "\"", 15647, 1);
			WriteAttributeValue("", 15628, AriaSort("locked"), 15628, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 15686, "\"", 15712, 1);
			WriteAttributeValue("", 15693, SortLink("locked"), 15693, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">נעול</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 15770, "\"", 15800, 1);
			WriteAttributeValue("", 15782, AriaSort("email"), 15782, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 15839, "\"", 15864, 1);
			WriteAttributeValue("", 15846, SortLink("email"), 15846, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">מייל</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 15922, "\"", 15952, 1);
			WriteAttributeValue("", 15934, AriaSort("phone"), 15934, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 15991, "\"", 16016, 1);
			WriteAttributeValue("", 15998, SortLink("phone"), 15998, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">טלפון</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 16075, "\"", 16107, 1);
			WriteAttributeValue("", 16087, AriaSort("restday"), 16087, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 16146, "\"", 16173, 1);
			WriteAttributeValue("", 16153, SortLink("restday"), 16153, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">יום מנוחה</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 16236, "\"", 16281, 1);
			WriteAttributeValue("", 16248, AriaSort("allowfuturereporting"), 16248, 33, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 16320, "\"", 16360, 1);
			WriteAttributeValue("", 16327, SortLink("allowfuturereporting"), 16327, 33, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">דיווח עתידי</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 16425, "\"", 16458, 1);
			WriteAttributeValue("", 16437, AriaSort("projects"), 16437, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 16497, "\"", 16525, 1);
			WriteAttributeValue("", 16504, SortLink("projects"), 16504, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">פרויקטים</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 16587, "\"", 16621, 1);
			WriteAttributeValue("", 16599, AriaSort("districts"), 16599, 22, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 16660, "\"", 16689, 1);
			WriteAttributeValue("", 16667, SortLink("districts"), 16667, 22, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">מחוזות</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 16749, "\"", 16782, 1);
			WriteAttributeValue("", 16761, AriaSort("programs"), 16761, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 16821, "\"", 16849, 1);
			WriteAttributeValue("", 16828, SortLink("programs"), 16828, 21, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">תוכניות</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 16910, "\"", 16942, 1);
			WriteAttributeValue("", 16922, AriaSort("sectors"), 16922, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 16981, "\"", 17008, 1);
			WriteAttributeValue("", 16988, SortLink("sectors"), 16988, 20, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">מגזרים</a>\r\n            </th>\r\n            <th scope=\"col\"");
			BeginWriteAttribute("aria-sort", " aria-sort=\"", 17068, "\"", 17098, 1);
			WriteAttributeValue("", 17080, AriaSort("notes"), 17080, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <a class=\"link-light\"");
			BeginWriteAttribute("href", " href=\"", 17137, "\"", 17162, 1);
			WriteAttributeValue("", 17144, SortLink("notes"), 17144, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">הערות עובד</a>\r\n            </th>\r\n            <th scope=\"col\">הערות הקצאה</th>\r\n          </tr>\r\n        </thead>\r\n        <tbody>\r\n");
			foreach (User emp in base.Model)
			{
				List<Allocation> activeAllocations = emp.Allocations.Where((Allocation a) => a.IsActive).ToList();
				bool isLocked = emp.StatusId == 3 || emp.FailedLoginAttempts >= 3;
				WriteLiteral("            <tr>\r\n              <td>\r\n                <div class=\"d-flex gap-1 flex-wrap\">\r\n                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc795103578", async delegate
				{
					WriteLiteral("עריכה");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_10.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(emp.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc795106047", async delegate
				{
					WriteLiteral("הקצאות");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_13.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(emp.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_14);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_15);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc795108517", async delegate
				{
					WriteLiteral("\r\n                    ");
					Write(Html.AntiForgeryToken());
					WriteLiteral("\r\n");
					WriteLiteral("                    <input type=\"hidden\" name=\"search\"");
					BeginWriteAttribute("value", " value=\"", 18346, "\"", 18368, 1);
					WriteAttributeValue("", 18354, filter.Search, 18354, 14, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" />\r\n                    <input type=\"hidden\" name=\"idNumber\"");
					BeginWriteAttribute("value", " value=\"", 18430, "\"", 18454, 1);
					WriteAttributeValue("", 18438, filter.IdNumber, 18438, 16, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" />\r\n                    <input type=\"hidden\" name=\"employeeCode\"");
					BeginWriteAttribute("value", " value=\"", 18520, "\"", 18548, 1);
					WriteAttributeValue("", 18528, filter.EmployeeCode, 18528, 20, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" />\r\n                    <input type=\"hidden\" name=\"firstName\"");
					BeginWriteAttribute("value", " value=\"", 18611, "\"", 18636, 1);
					WriteAttributeValue("", 18619, filter.FirstName, 18619, 17, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" />\r\n                    <input type=\"hidden\" name=\"lastName\"");
					BeginWriteAttribute("value", " value=\"", 18698, "\"", 18722, 1);
					WriteAttributeValue("", 18706, filter.LastName, 18706, 16, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" />\r\n");
					if (filter.StatusId.HasValue)
					{
						WriteLiteral(" <input type=\"hidden\" name=\"statusId\"");
						BeginWriteAttribute("value", " value=\"", 18817, "\"", 18841, 1);
						WriteAttributeValue("", 18825, filter.StatusId, 18825, 16, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" /> ");
					}
					if (filter.RoleId.HasValue)
					{
						WriteLiteral(" <input type=\"hidden\" name=\"roleId\"");
						BeginWriteAttribute("value", " value=\"", 18934, "\"", 18956, 1);
						WriteAttributeValue("", 18942, filter.RoleId, 18942, 14, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" /> ");
					}
					if (filter.RestDay.HasValue)
					{
						WriteLiteral(" <input type=\"hidden\" name=\"restDay\"");
						BeginWriteAttribute("value", " value=\"", 19051, "\"", 19074, 1);
						WriteAttributeValue("", 19059, filter.RestDay, 19059, 15, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" /> ");
					}
					if (filter.LockedOnly)
					{
						WriteLiteral(" <input type=\"hidden\" name=\"lockedOnly\" value=\"true\" /> ");
					}
					WriteLiteral("                    <button type=\"submit\" class=\"btn btn-sm btn-outline-warning\">אפס סיסמה</button>\r\n                  ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_16.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 2, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 17947, Url.Action("ResetPassword", "Employee", new
				{
					id = emp.Id
				}), 17947, 61, isLiteral: false);
				AddHtmlAttributeValue("", 18008, base.Context.Request.QueryString, 18008, 28, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_18);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
				if (isLocked)
				{
					WriteLiteral("                    ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc795117617", async delegate
					{
						WriteLiteral("\r\n                      ");
						Write(Html.AntiForgeryToken());
						WriteLiteral("\r\n                      <input type=\"hidden\" name=\"search\"");
						BeginWriteAttribute("value", " value=\"", 19743, "\"", 19765, 1);
						WriteAttributeValue("", 19751, filter.Search, 19751, 14, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" />\r\n                      <input type=\"hidden\" name=\"firstName\"");
						BeginWriteAttribute("value", " value=\"", 19830, "\"", 19855, 1);
						WriteAttributeValue("", 19838, filter.FirstName, 19838, 17, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" />\r\n                      <input type=\"hidden\" name=\"lastName\"");
						BeginWriteAttribute("value", " value=\"", 19919, "\"", 19943, 1);
						WriteAttributeValue("", 19927, filter.LastName, 19927, 16, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" />\r\n");
						if (filter.LockedOnly)
						{
							WriteLiteral(" <input type=\"hidden\" name=\"lockedOnly\" value=\"true\" /> ");
						}
						WriteLiteral("                      <button type=\"submit\" class=\"btn btn-sm btn-outline-success\">שחרור נעילה</button>\r\n                    ");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
					__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
					__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_16.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
					BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 2, HtmlAttributeValueStyle.DoubleQuotes);
					AddHtmlAttributeValue("", 19417, Url.Action("UnlockAccount", "Employee", new
					{
						id = emp.Id
					}), 19417, 61, isLiteral: false);
					AddHtmlAttributeValue("", 19478, base.Context.Request.QueryString, 19478, 28, isLiteral: false);
					EndAddHtmlAttributeValues(__tagHelperExecutionContext);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_19);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n");
				}
				if (emp.StatusId != 2)
				{
					WriteLiteral("                    ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "a174878c27cc4188cb9bdd630de69cdce666e7772820e42b32de9393b07fc795123134", async delegate
					{
						WriteLiteral("\r\n                      ");
						Write(Html.AntiForgeryToken());
						WriteLiteral("\r\n                      <input type=\"hidden\" name=\"search\"");
						BeginWriteAttribute("value", " value=\"", 20642, "\"", 20664, 1);
						WriteAttributeValue("", 20650, filter.Search, 20650, 14, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" />\r\n                      <input type=\"hidden\" name=\"firstName\"");
						BeginWriteAttribute("value", " value=\"", 20729, "\"", 20754, 1);
						WriteAttributeValue("", 20737, filter.FirstName, 20737, 17, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" />\r\n                      <input type=\"hidden\" name=\"lastName\"");
						BeginWriteAttribute("value", " value=\"", 20818, "\"", 20842, 1);
						WriteAttributeValue("", 20826, filter.LastName, 20826, 16, isLiteral: false);
						EndWriteAttribute();
						WriteLiteral(" />\r\n                      <button type=\"submit\" class=\"btn btn-sm btn-outline-danger\">לא פעיל</button>\r\n                    ");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
					__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
					__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_16.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
					BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 2, HtmlAttributeValueStyle.DoubleQuotes);
					AddHtmlAttributeValue("", 20322, Url.Action("DeleteEmployee", "Employee", new
					{
						id = emp.Id
					}), 20322, 62, isLiteral: false);
					AddHtmlAttributeValue("", 20384, base.Context.Request.QueryString, 20384, 28, isLiteral: false);
					EndAddHtmlAttributeValues(__tagHelperExecutionContext);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_17);
					__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_20);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n");
				}
				WriteLiteral("                </div>\r\n              </td>\r\n              <td><input type=\"checkbox\" name=\"selectedIds\"");
				BeginWriteAttribute("value", " value=\"", 21100, "\"", 21115, 1);
				WriteAttributeValue("", 21108, emp.Id, 21108, 7, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" class=\"row-check\" aria-label=\"בחר עובד\" /></td>\r\n              <td>");
				Write(emp.EmployeeCode);
				WriteLiteral("</td>\r\n              <td>");
				Write(DisplayIdNumber(emp.IdNumber));
				WriteLiteral("</td>\n              <td>");
				Write(emp.FirstName);
				WriteLiteral("</td>\r\n              <td>");
				Write(emp.LastName);
				WriteLiteral("</td>\r\n              <td>");
				Write(emp.Role?.Description ?? "-");
				WriteLiteral("</td>\r\n              <td>");
				Write(emp.UserRole?.DescriptionHebrew ?? emp.UserRole?.Name ?? "-");
				WriteLiteral("</td>\r\n              <td>\r\n");
				string value2 = emp.StatusId switch
				{
					1 => "success", 
					2 => "secondary", 
					3 => "danger", 
					_ => "secondary", 
				};
				WriteLiteral("                <span");
				BeginWriteAttribute("class", " class=\"", 21813, "\"", 21842, 3);
				WriteAttributeValue("", 21821, "badge", 21821, 5, isLiteral: true);
				WriteAttributeValue(" ", 21826, "bg-", 21827, 4, isLiteral: true);
				WriteAttributeValue("", 21830, value2, 21830, 12, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">");
				Write(emp.Status?.DescriptionHebrew ?? emp.Status?.Name ?? "-");
				WriteLiteral("</span>\r\n              </td>\r\n              <td>");
				Write(emp.IsReportingEmployee ? "כן" : "לא");
				WriteLiteral("</td>\r\n              <td>");
				Write(isLocked ? "כן" : "לא");
				WriteLiteral("</td>\r\n              <td>");
				Write(string.IsNullOrWhiteSpace(emp.Email) ? "-" : emp.Email);
				WriteLiteral("</td>\r\n              <td>");
				Write(string.IsNullOrWhiteSpace(emp.Phone) ? "-" : emp.Phone);
				WriteLiteral("</td>\r\n              <td>\r\n");
				SelectListItem selectListItem = SelectListProviders.RestDayOptions.FirstOrDefault((SelectListItem o) => o.Value == (emp.RestDay?.ToString() ?? string.Empty));
				Write(emp.RestDay.HasValue ? (selectListItem?.Text ?? emp.RestDay.ToString()) : "-");
				WriteLiteral("              </td>\r\n              <td>");
				Write(emp.AllowFutureReporting ? "כן" : "לא");
				WriteLiteral("</td>\r\n              <td>");
				Write(Values(activeAllocations.Select((Allocation a) => a.Project?.Description)));
				WriteLiteral("</td>\r\n              <td>");
				Write(Values(from x in activeAllocations.SelectMany((Allocation a) => a.AllocationDistricts)
					select x.District?.Description));
				WriteLiteral("</td>\r\n              <td>");
				Write(Values(from x in activeAllocations.SelectMany((Allocation a) => a.AllocationPrograms)
					select x.Program?.Description));
				WriteLiteral("</td>\r\n              <td>");
				Write(Values(from x in activeAllocations.SelectMany((Allocation a) => a.AllocationSectors)
					select x.Sector?.Description));
				WriteLiteral("</td>\r\n              <td>");
				Write(string.IsNullOrWhiteSpace(emp.Notes) ? "-" : emp.Notes);
				WriteLiteral("</td>\r\n              <td>");
				Write(Values(activeAllocations.Select((Allocation a) => a.Notes)));
				WriteLiteral("</td>\r\n            </tr>\r\n");
			}
			if (!base.Model.Any())
			{
				WriteLiteral("            <tr>\r\n              <td colspan=\"22\" class=\"text-center text-muted py-4\">לא נמצאו עובדים</td>\r\n            </tr>\r\n");
			}
			WriteLiteral("        </tbody>\r\n      </table>\r\n    </div>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_16.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_21.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_21);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_22);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n\r\n");
		if (totalPages > 1)
		{
			WriteLiteral("    <nav aria-label=\"עימוד\">\r\n      <ul class=\"pagination justify-content-center\">\r\n        <li");
			BeginWriteAttribute("class", " class=\"", 23606, "\"", 23654, 2);
			WriteAttributeValue("", 23614, "page-item", 23614, 9, isLiteral: true);
			WriteAttributeValue(" ", 23623, (page <= 1) ? "disabled" : "", 23624, 30, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n          <a class=\"page-link\"");
			BeginWriteAttribute("href", " href=\"", 23688, "\"", 23714, 1);
			WriteAttributeValue("", 23695, PageLink(page - 1), 23695, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">הקודם</a>\r\n        </li>\r\n");
			for (int i = Math.Max(1, page - 2); i <= Math.Min(totalPages, page + 2); i++)
			{
				WriteLiteral("          <li");
				BeginWriteAttribute("class", " class=\"", 23854, "\"", 23900, 2);
				WriteAttributeValue("", 23862, "page-item", 23862, 9, isLiteral: true);
				WriteAttributeValue(" ", 23871, (i == page) ? "active" : "", 23872, 28, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">\r\n            <a class=\"page-link\"");
				BeginWriteAttribute("href", " href=\"", 23936, "\"", 23955, 1);
				WriteAttributeValue("", 23943, PageLink(i), 23943, 12, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">");
				Write(i);
				WriteLiteral("</a>\r\n          </li>\r\n");
			}
			WriteLiteral("        <li");
			BeginWriteAttribute("class", " class=\"", 24004, "\"", 24061, 2);
			WriteAttributeValue("", 24012, "page-item", 24012, 9, isLiteral: true);
			WriteAttributeValue(" ", 24021, (page >= totalPages) ? "disabled" : "", 24022, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n          <a class=\"page-link\"");
			BeginWriteAttribute("href", " href=\"", 24095, "\"", 24121, 1);
			WriteAttributeValue("", 24102, PageLink(page + 1), 24102, 19, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">הבא</a>\r\n        </li>\r\n      </ul>\r\n    </nav>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n");
		DefineSection("Scripts", (RenderAsyncDelegate)async delegate
		{
			WriteLiteral("\r\n  <script>\r\n    const selectAll = document.getElementById('selectAll');\r\n    const checkboxes = document.querySelectorAll('.row-check');\r\n    const bulkBar = document.getElementById('bulkBar');\r\n    const selectedCount = document.getElementById('selectedCount');\r\n\r\n    function updateBulkBar() {\r\n      const checked = document.querySelectorAll('.row-check:checked').length;\r\n      if (checked > 0) {\r\n        bulkBar.classList.remove('d-none');\r\n        selectedCount.textContent = `נבחרו ${checked} עובדים`;\r\n      } else {\r\n        bulkBar.classList.add('d-none');\r\n      }\r\n    }\r\n\r\n    selectAll?.addEventListener('change', function () {\r\n      checkboxes.forEach(cb => cb.checked = this.checked);\r\n      updateBulkBar();\r\n    });\r\n\r\n    checkboxes.forEach(cb => cb.addEventListener('change', updateBulkBar));\r\n  </script>\r\n");
		});
		string AriaSort(string key)
		{
			if (!(sortBy == key))
			{
				return "none";
			}
			if (!sortDesc)
			{
				return "ascending";
			}
			return "descending";
		}
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
						list2.Add(Uri.EscapeDataString(item.Key) + "=" + Uri.EscapeDataString(text));
					}
				}
			}
			foreach (KeyValuePair<string, string> item2 in extraRouteValues)
			{
				list2.Add(Uri.EscapeDataString(item2.Key) + "=" + Uri.EscapeDataString(item2.Value));
			}
			return Url.Action(action, "Employee") + ((list2.Count > 0) ? ("?" + string.Join("&", list2)) : "");
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
			return BuildUrl("ExportExcel", new Dictionary<string, object>
			{
				["sortBy"] = sortBy,
				["sortDesc"] = (sortDesc ? "true" : null)
			});
		}
		string PageLink(int p)
		{
			return BuildUrl("Index", new Dictionary<string, object>
			{
				["sortBy"] = sortBy,
				["sortDesc"] = (sortDesc ? "true" : null),
				["page"] = p
			});
		}
		string SortLink(string key)
		{
			return BuildUrl("Index", new Dictionary<string, object>
			{
				["sortBy"] = key,
				["sortDesc"] = ((!(sortBy == key)) ? null : ((!sortDesc) ? "true" : null)),
				["page"] = 1
			});
		}
		static string Values(IEnumerable<string?> values)
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
			return "-";
		}
	}
}
