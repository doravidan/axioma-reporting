using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Employee/Allocations.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Employee_Allocations : RazorPage<List<Allocation>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "Edit", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("asp-action", "CreateAllocation", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("class", new HtmlString("btn btn-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("asp-controller", "Lookup", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("asp-action", "List", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("asp-route-tableName", "districts", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-primary sticky-nav-button"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("asp-controller", "Admin", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("asp-action", "ProjectPrograms", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("class", new HtmlString("btn btn-sm btn-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("asp-action", "EditAllocation", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_14 = new TagHelperAttribute("asp-action", "ExportAllocationsExcel", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_15 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_16 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_17 = new TagHelperAttribute("asp-action", "UploadAllocationExcel", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_18 = new TagHelperAttribute("enctype", new HtmlString("multipart/form-data"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_19 = new TagHelperAttribute("class", new HtmlString("mt-3 d-flex flex-wrap gap-2 align-items-center"), HtmlAttributeValueStyle.DoubleQuotes);

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
		User employee = (User)base.ViewBag.Employee;
		List<SelectListItem> projects = ((SelectList)base.ViewBag.Projects).ToList();
		List<ReportingMonth> reportingMonths = (base.ViewBag.ReportingMonths as List<ReportingMonth>) ?? new List<ReportingMonth>();
		decimal[] durationOptions = (decimal[])base.ViewBag.OutputDurationOptions;
		List<District> source = (List<District>)base.ViewBag.Districts;
		List<AxiomaReporting.Core.Entities.Program> programs = (List<AxiomaReporting.Core.Entities.Program>)base.ViewBag.Programs;
		List<Sector> source2 = (List<Sector>)base.ViewBag.Sectors;
		List<Locality> source3 = (List<Locality>)base.ViewBag.Localities;
		List<Framework> source4 = (List<Framework>)base.ViewBag.Frameworks;
		List<Subject> source5 = (List<Subject>)base.ViewBag.Subjects;
		List<Domain> source6 = (List<Domain>)base.ViewBag.Domains;
		List<EducationalProgram> source7 = (List<EducationalProgram>)base.ViewBag.EducationalPrograms;
		List<SchoolClass> source8 = (List<SchoolClass>)base.ViewBag.Classes;
		List<GradeLevel> source9 = (List<GradeLevel>)base.ViewBag.GradeLevels;
		List<DiscussionCode> source10 = (List<DiscussionCode>)base.ViewBag.DiscussionCodes;
		List<LocalityDistrictNational> source11 = (List<LocalityDistrictNational>)base.ViewBag.LocalityDistrictNationals;
		int? selectedAllocationId = base.ViewBag.SelectedAllocationId as int?;
		base.ViewData["Title"] = "הקצאות - " + employee.FirstName + " " + employee.LastName;
		Dictionary<string, object> lookupData = new Dictionary<string, object>
		{
			["DistrictIds"] = source.Select((District x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["ProgramIds"] = programs.Select((AxiomaReporting.Core.Entities.Program x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["SectorIds"] = source2.Select((Sector x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["LocalityIds"] = source3.Select((Locality x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["FrameworkIds"] = source4.Select((Framework x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["SubjectIds"] = source5.Select((Subject x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["DomainIds"] = source6.Select((Domain x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["EducationalProgramIds"] = source7.Select((EducationalProgram x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["ClassIds"] = source8.Select((SchoolClass x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["GradeLevelIds"] = source9.Select((GradeLevel x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["DiscussionCodeIds"] = source10.Select((DiscussionCode x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			}),
			["LocalityDistrictNationalIds"] = source11.Select((LocalityDistrictNational x) => new
			{
				id = x.Id.ToString(),
				text = x.Description
			})
		};
		Dictionary<string, string> categoryLabels = new Dictionary<string, string>
		{
			["LocalityIds"] = "יישובים",
			["FrameworkIds"] = "מסגרות",
			["DomainIds"] = "תחומים",
			["SubjectIds"] = "נושאים",
			["DiscussionCodeIds"] = "קיום דיון",
			["ClassIds"] = "כיתה",
			["GradeLevelIds"] = "שכבה",
			["SectorIds"] = "מגזרים",
			["ProgramIds"] = "תוכניות",
			["DistrictIds"] = "מחוזות",
			["EducationalProgramIds"] = "תוכניות חינוכיות",
			["LocalityDistrictNationalIds"] = "יישוב/מחוז/ארצי"
		};
		WriteLiteral("\r\n<div class=\"container-fluid py-3 allocations-page\" dir=\"rtl\">\r\n  <div class=\"d-flex justify-content-between align-items-start mb-3 gap-3 flex-wrap\">\r\n    <div>\r\n      <h2 class=\"mb-1\">הקצאות</h2>\r\n      <h4 class=\"mb-0 fw-bold\">");
		Write(employee.FirstName);
		WriteLiteral(" ");
		Write(employee.LastName);
		WriteLiteral(" קוד עובד ");
		Write(employee.EmployeeCode);
		WriteLiteral(" ת.ז ");
		Write(employee.IdNumber);
		WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"d-flex gap-2 flex-wrap\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da16490", async delegate
		{
			WriteLiteral("חזרה לכרטיס עובד");
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
		WriteLiteral("\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da18767", async delegate
		{
			WriteLiteral("בית");
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
		WriteLiteral("\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da20033", async delegate
		{
			WriteLiteral("הוסף הקצאה");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
		}
		BeginWriteTagHelperAttribute();
		WriteLiteral(employee.Id);
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
		__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n\r\n  <div class=\"allocation-shortcuts mb-3\" aria-label=\"קיצורי טבלאות הקצאה\">\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da22414", async delegate
		{
			WriteLiteral("\r\n      טבלת מחוזות בחירה\r\n    ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
		if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
		{
			throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-tableName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
		}
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["tableName"] = (string)__tagHelperAttribute_8.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da24440", async delegate
		{
			WriteLiteral("\r\n      טבלת תוכניות\r\n    ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_10.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_11.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
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
		if (base.TempData["Error"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\">\r\n      ");
			Write(base.TempData["Error"]);
			WriteLiteral("\r\n      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n");
		foreach (Allocation alloc in base.Model)
		{
			bool durationUnlimited = DurationUnlimited(alloc);
			WriteLiteral("    <section");
			BeginWriteAttribute("id", " id=\"", 6370, "\"", 6395, 2);
			WriteAttributeValue("", 6375, "allocation-", 6375, 11, isLiteral: true);
			WriteAttributeValue("", 6386, alloc.Id, 6386, 9, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("class", "\r\n             class=\"", 6396, "\"", 6528, 5);
			WriteAttributeValue("", 6418, "allocation-card", 6418, 15, isLiteral: true);
			WriteAttributeValue(" ", 6433, "card", 6434, 5, isLiteral: true);
			WriteAttributeValue(" ", 6438, "border-success", 6439, 15, isLiteral: true);
			WriteAttributeValue(" ", 6453, "mb-3", 6454, 5, isLiteral: true);
			WriteAttributeValue(" ", 6458, (selectedAllocationId == alloc.Id) ? "allocation-card-selected" : "", 6459, 69, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral("\n             data-allocation-id=\"");
			Write(alloc.Id);
			WriteLiteral("\">\r\n      ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da29707", async delegate
			{
				WriteLiteral("\r\n        ");
				Write(Html.AntiForgeryToken());
				WriteLiteral("\r\n        <input type=\"hidden\" name=\"Id\"");
				BeginWriteAttribute("value", " value=\"", 6806, "\"", 6823, 1);
				WriteAttributeValue("", 6814, alloc.Id, 6814, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\r\n        <input type=\"hidden\" name=\"UserId\"");
				BeginWriteAttribute("value", " value=\"", 6871, "\"", 6891, 1);
				WriteAttributeValue("", 6879, employee.Id, 6879, 12, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\r\n        <div class=\"allocation-validation text-danger fw-bold mb-2\" role=\"alert\" aria-live=\"assertive\" hidden></div>\r\n\r\n        <div class=\"d-flex justify-content-between align-items-start gap-3\">\r\n          <h5 class=\"fw-bold mb-2\">פרטי הקצאה</h5>\r\n          <div class=\"d-flex flex-column gap-2 allocation-actions\">\r\n            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da31572", async delegate
				{
					WriteLiteral("ערוך");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_13.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(employee.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(alloc.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-allocationId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"], HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\n            <button type=\"submit\" class=\"btn btn-sm btn-primary\">שמור</button>\r\n            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da34794", async delegate
				{
					WriteLiteral("יצא לטבלה");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_14.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_14);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-employeeCode", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(employee.EmployeeCode);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["employeeCode"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-employeeCode", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["employeeCode"], HtmlAttributeValueStyle.DoubleQuotes);
				BeginWriteTagHelperAttribute();
				WriteLiteral(alloc.ProjectId);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["projectId"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-projectId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["projectId"], HtmlAttributeValueStyle.DoubleQuotes);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n          </div>\r\n        </div>\r\n\r\n        <div class=\"row g-3 align-items-end allocation-fields\">\r\n          <div class=\"col-12 col-md-2\">\r\n            <label class=\"form-label fw-bold\"");
				BeginWriteAttribute("for", " for=\"", 7906, "\"", 7929, 2);
				WriteAttributeValue("", 7912, "project-", 7912, 8, isLiteral: true);
				WriteAttributeValue("", 7920, alloc.Id, 7920, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">פרויקט</label>\r\n            <select");
				BeginWriteAttribute("id", " id=\"", 7966, "\"", 7988, 2);
				WriteAttributeValue("", 7971, "project-", 7971, 8, isLiteral: true);
				WriteAttributeValue("", 7979, alloc.Id, 7979, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" name=\"ProjectId\" class=\"form-select form-select-sm allocation-editable\" required>\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da39302", async delegate
				{
					WriteLiteral("בחר פרויקט");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_15.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
				foreach (SelectListItem project in projects)
				{
					WriteLiteral("                ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da40834", async delegate
					{
						Write(project.Text);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(project.Value);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
					AddHtmlAttributeValue("", 8249, project.Value == alloc.ProjectId.ToString(), 8249, 46, isLiteral: false);
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
				WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-2\">\r\n            <label class=\"form-label fw-bold\"");
				BeginWriteAttribute("for", " for=\"", 8467, "\"", 8498, 2);
				WriteAttributeValue("", 8473, "program-summary-", 8473, 16, isLiteral: true);
				WriteAttributeValue("", 8489, alloc.Id, 8489, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">תוכנית</label>\r\n            <select");
				BeginWriteAttribute("id", " id=\"", 8535, "\"", 8565, 2);
				WriteAttributeValue("", 8540, "program-summary-", 8540, 16, isLiteral: true);
				WriteAttributeValue("", 8556, alloc.Id, 8556, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" class=\"form-select form-select-sm allocation-category-shortcut allocation-editable\" data-target-field=\"ProgramIds\" required>\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da44876", async delegate
				{
					WriteLiteral("בחר תוכנית");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_15.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
				foreach (AxiomaReporting.Core.Entities.Program program in programs)
				{
					WriteLiteral("                ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da46408", async delegate
					{
						Write(program.Description);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(program.Id);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
					AddHtmlAttributeValue("", 8866, alloc.AllocationPrograms.Any((AllocationProgram x) => x.ProgramId == program.Id), 8866, 63, isLiteral: false);
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
				WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-2\">\r\n            <label class=\"form-label fw-bold\"");
				BeginWriteAttribute("for", " for=\"", 9108, "\"", 9137, 2);
				WriteAttributeValue("", 9114, "monthly-scope-", 9114, 14, isLiteral: true);
				WriteAttributeValue("", 9128, alloc.Id, 9128, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">היקף פעילות חודשי</label>\r\n            <input");
				BeginWriteAttribute("id", " id=\"", 9184, "\"", 9212, 2);
				WriteAttributeValue("", 9189, "monthly-scope-", 9189, 14, isLiteral: true);
				WriteAttributeValue("", 9203, alloc.Id, 9203, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" name=\"MonthlyEmploymentScope\" type=\"number\" step=\"1\" min=\"0\"");
				BeginWriteAttribute("value", "\r\n                   value=\"", 9274, "\"", 9344, 1);
				WriteAttributeValue("", 9302, DecimalText(alloc.MonthlyEmploymentScope), 9302, 42, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" class=\"form-control form-control-sm allocation-editable\" />\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-2\">\r\n            <label class=\"form-label fw-bold\"");
				BeginWriteAttribute("for", " for=\"", 9513, "\"", 9540, 2);
				WriteAttributeValue("", 9519, "daily-scope-", 9519, 12, isLiteral: true);
				WriteAttributeValue("", 9531, alloc.Id, 9531, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">היקף פעילות יומי</label>\r\n            <select");
				BeginWriteAttribute("id", " id=\"", 9587, "\"", 9613, 2);
				WriteAttributeValue("", 9592, "daily-scope-", 9592, 12, isLiteral: true);
				WriteAttributeValue("", 9604, alloc.Id, 9604, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" name=\"DailyEmploymentScope\" class=\"form-select form-select-sm allocation-editable\">\r\n              ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da52207", async delegate
				{
					WriteLiteral("ללא הגבלה");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_15.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
				BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
				AddHtmlAttributeValue("", 9741, !alloc.DailyEmploymentScope.HasValue, 9741, 39, isLiteral: false);
				EndAddHtmlAttributeValues(__tagHelperExecutionContext);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
				int hour;
				for (hour = 1; hour <= 9; hour++)
				{
					WriteLiteral("                ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da54288", async delegate
					{
						WriteLiteral("עד ");
						Write(hour);
						WriteLiteral(" שעות");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(hour);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "selected", 1, HtmlAttributeValueStyle.DoubleQuotes);
					Views_Employee_Allocations views_Employee_Allocations = this;
					decimal? dailyEmploymentScope = alloc.DailyEmploymentScope;
					decimal num = hour;
					views_Employee_Allocations.AddHtmlAttributeValue("", 9921, (dailyEmploymentScope.GetValueOrDefault() == num) & dailyEmploymentScope.HasValue, 9921, 37, isLiteral: false);
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
				WriteLiteral("            </select>\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-2\">\r\n            <label class=\"form-label fw-bold\"");
				BeginWriteAttribute("for", " for=\"", 10130, "\"", 10158, 2);
				WriteAttributeValue("", 10136, "annual-scope-", 10136, 13, isLiteral: true);
				WriteAttributeValue("", 10149, alloc.Id, 10149, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">היקף פעילות שנתי</label>\r\n            <input");
				BeginWriteAttribute("id", " id=\"", 10204, "\"", 10231, 2);
				WriteAttributeValue("", 10209, "annual-scope-", 10209, 13, isLiteral: true);
				WriteAttributeValue("", 10222, alloc.Id, 10222, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" name=\"AnnualEmploymentScope\" type=\"number\" step=\"1\" min=\"0\"");
				BeginWriteAttribute("value", "\r\n                   value=\"", 10292, "\"", 10361, 1);
				WriteAttributeValue("", 10320, DecimalText(alloc.AnnualEmploymentScope), 10320, 41, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" class=\"form-control form-control-sm allocation-editable\" />\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-2\">\r\n            <div class=\"form-check\">\r\n              <input type=\"hidden\" name=\"AllowExcelUpload\" value=\"false\" />\r\n              <input");
				BeginWriteAttribute("id", " id=\"", 10620, "\"", 10646, 2);
				WriteAttributeValue("", 10625, "allow-excel-", 10625, 12, isLiteral: true);
				WriteAttributeValue("", 10637, alloc.Id, 10637, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" name=\"AllowExcelUpload\" value=\"true\" type=\"checkbox\"\r\n                     class=\"form-check-input allocation-editable\" ");
				Write(alloc.AllowExcelUpload ? "checked" : "");
				WriteLiteral(" />\r\n              <label class=\"form-check-label fw-bold\"");
				BeginWriteAttribute("for", " for=\"", 10868, "\"", 10895, 2);
				WriteAttributeValue("", 10874, "allow-excel-", 10874, 12, isLiteral: true);
				WriteAttributeValue("", 10886, alloc.Id, 10886, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">אפשר העלאת קובץ דיווח</label>\r\n            </div>\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-2\">\r\n            <label class=\"form-label fw-bold\">משך תפוקה</label>\r\n            <div class=\"duration-box\">\r\n");
				decimal[] array = durationOptions;
				foreach (decimal value2 in array)
				{
					string text2 = DurationValue(value2);
					WriteLiteral("                <div class=\"form-check\">\r\n                  <input");
					BeginWriteAttribute("id", " id=\"", 11309, "\"", 11352, 4);
					WriteAttributeValue("", 11314, "dur-", 11314, 4, isLiteral: true);
					WriteAttributeValue("", 11318, alloc.Id, 11318, 9, isLiteral: false);
					WriteAttributeValue("", 11327, "-", 11327, 1, isLiteral: true);
					WriteAttributeValue("", 11328, text2.Replace(".", "_"), 11328, 24, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" name=\"OutputDurationValues\"");
					BeginWriteAttribute("value", " value=\"", 11381, "\"", 11395, 1);
					WriteAttributeValue("", 11389, text2, 11389, 6, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral("\r\n                         type=\"checkbox\" class=\"form-check-input duration-value allocation-editable\"\r\n                         ");
					Write(DurationSelected(alloc, value2) ? "checked" : "");
					WriteLiteral(" />\r\n                  <label class=\"form-check-label\"");
					BeginWriteAttribute("for", " for=\"", 11632, "\"", 11676, 4);
					WriteAttributeValue("", 11638, "dur-", 11638, 4, isLiteral: true);
					WriteAttributeValue("", 11642, alloc.Id, 11642, 9, isLiteral: false);
					WriteAttributeValue("", 11651, "-", 11651, 1, isLiteral: true);
					WriteAttributeValue("", 11652, text2.Replace(".", "_"), 11652, 24, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(">");
					Write(text2);
					WriteLiteral("</label>\r\n                </div>\r\n");
				}
				WriteLiteral("              <div class=\"form-check\">\r\n                <input");
				BeginWriteAttribute("id", " id=\"", 11797, "\"", 11830, 2);
				WriteAttributeValue("", 11802, "duration-unlimited-", 11802, 19, isLiteral: true);
				WriteAttributeValue("", 11821, alloc.Id, 11821, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" type=\"checkbox\" class=\"form-check-input duration-unlimited allocation-editable\"\r\n                       ");
				Write(durationUnlimited ? "checked" : "");
				WriteLiteral(" />\r\n                <label class=\"form-check-label\"");
				BeginWriteAttribute("for", " for=\"", 12025, "\"", 12059, 2);
				WriteAttributeValue("", 12031, "duration-unlimited-", 12031, 19, isLiteral: true);
				WriteAttributeValue("", 12050, alloc.Id, 12050, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">ללא הגבלה</label>\r\n              </div>\r\n              <input type=\"hidden\" name=\"OutputDuration\" class=\"duration-output\"");
				BeginWriteAttribute("value", " value=\"", 12182, "\"", 12239, 1);
				WriteAttributeValue("", 12190, durationUnlimited ? "Unlimited" : string.Empty, 12190, 49, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\r\n            </div>\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-10\">\r\n            <label class=\"form-label fw-bold\"");
				BeginWriteAttribute("for", " for=\"", 12372, "\"", 12393, 2);
				WriteAttributeValue("", 12378, "notes-", 12378, 6, isLiteral: true);
				WriteAttributeValue("", 12384, alloc.Id, 12384, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">הערות</label>\r\n            <textarea");
				BeginWriteAttribute("id", " id=\"", 12431, "\"", 12451, 2);
				WriteAttributeValue("", 12436, "notes-", 12436, 6, isLiteral: true);
				WriteAttributeValue("", 12442, alloc.Id, 12442, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" name=\"Notes\" class=\"form-control allocation-editable\" rows=\"2\">");
				Write(alloc.Notes);
				WriteLiteral("</textarea>\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-2\">\r\n            <label class=\"form-label fw-bold\"");
				BeginWriteAttribute("for", " for=\"", 12647, "\"", 12675, 2);
				WriteAttributeValue("", 12653, "monthly-rows-", 12653, 13, isLiteral: true);
				WriteAttributeValue("", 12666, alloc.Id, 12666, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">מכסת שורות חודשית</label>\r\n            <input");
				BeginWriteAttribute("id", " id=\"", 12722, "\"", 12749, 2);
				WriteAttributeValue("", 12727, "monthly-rows-", 12727, 13, isLiteral: true);
				WriteAttributeValue("", 12740, alloc.Id, 12740, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" name=\"MonthlyRowAllocation\" type=\"number\" min=\"0\"");
				BeginWriteAttribute("value", "\r\n                   value=\"", 12800, "\"", 12855, 1);
				WriteAttributeValue("", 12828, alloc.MonthlyRowAllocation, 12828, 27, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" class=\"form-control form-control-sm allocation-editable\" />\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-2\">\r\n            <label class=\"form-label fw-bold\"");
				BeginWriteAttribute("for", " for=\"", 13024, "\"", 13051, 2);
				WriteAttributeValue("", 13030, "annual-rows-", 13030, 12, isLiteral: true);
				WriteAttributeValue("", 13042, alloc.Id, 13042, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">מכסת שורות שנתית</label>\r\n            <input");
				BeginWriteAttribute("id", " id=\"", 13097, "\"", 13123, 2);
				WriteAttributeValue("", 13102, "annual-rows-", 13102, 12, isLiteral: true);
				WriteAttributeValue("", 13114, alloc.Id, 13114, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" name=\"AnnualRowAllocation\" type=\"number\" min=\"0\"");
				BeginWriteAttribute("value", "\r\n                   value=\"", 13173, "\"", 13227, 1);
				WriteAttributeValue("", 13201, alloc.AnnualRowAllocation, 13201, 26, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" class=\"form-control form-control-sm allocation-editable\" />\r\n          </div>\r\n\r\n          <div class=\"col-12 col-md-8\">\r\n            <div class=\"allocation-picker\">\r\n              <div class=\"row g-3 align-items-start\">\r\n                <div class=\"col-12 col-md-3\">\r\n                  <label class=\"form-label fw-bold text-primary\"");
				BeginWriteAttribute("for", " for=\"", 13562, "\"", 13586, 2);
				WriteAttributeValue("", 13568, "category-", 13568, 9, isLiteral: true);
				WriteAttributeValue("", 13577, alloc.Id, 13577, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">בחר טבלה לעריכה</label>\r\n                  <select");
				BeginWriteAttribute("id", " id=\"", 13638, "\"", 13661, 2);
				WriteAttributeValue("", 13643, "category-", 13643, 9, isLiteral: true);
				WriteAttributeValue("", 13652, alloc.Id, 13652, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" class=\"form-select allocation-category allocation-editable\">\r\n");
				foreach (KeyValuePair<string, string> category in categoryLabels)
				{
					WriteLiteral("                      ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da73265", async delegate
					{
						Write(category.Value);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(category.Key);
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
				WriteLiteral("                  </select>\r\n                </div>\r\n                <div class=\"col-12 col-md-4\">\r\n                  <label class=\"form-label fw-bold allocation-available-label\">בחר ערך להוספה</label>\r\n                  <input type=\"search\" class=\"form-control form-control-sm allocation-search-available allocation-editable mb-2\" placeholder=\"search\" aria-label=\"חיפוש ערכים להוספה\" />\r\n                  <select class=\"form-select allocation-available allocation-editable\" size=\"6\" aria-label=\"ערכים זמינים להוספה\"></select>\r\n                  <button type=\"button\" class=\"btn btn-sm btn-outline-primary mt-2 allocation-add allocation-editable-button\">הוסף</button>\r\n                </div>\r\n                <div class=\"col-12 col-md-4\">\r\n                  <label class=\"form-label fw-bold text-primary\">ערכים שנבחרו</label>\r\n                  <input type=\"search\" class=\"form-control form-control-sm allocation-search-selected allocation-editable mb-2\" placeholder=\"search\" aria-label=\"חיפוש ערכים שנבחרו\" />\r\n           ");
				WriteLiteral("       <div class=\"allocation-selected-list\"></div>\r\n                </div>\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </div>\r\n\r\n        <select class=\"allocation-store d-none\" data-field=\"DistrictIds\" name=\"DistrictIds\" multiple>\r\n");
				foreach (AllocationDistrict item12 in alloc.AllocationDistricts)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da77054", async delegate
					{
						Write(item12.District?.Description ?? item12.DistrictId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item12.DistrictId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"ProgramIds\" name=\"ProgramIds\" multiple>\r\n");
				foreach (AllocationProgram item11 in alloc.AllocationPrograms)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da79960", async delegate
					{
						Write(item11.Program?.Description ?? item11.ProgramId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item11.ProgramId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"SectorIds\" name=\"SectorIds\" multiple>\r\n");
				foreach (AllocationSector item10 in alloc.AllocationSectors)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da82860", async delegate
					{
						Write(item10.Sector?.Description ?? item10.SectorId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item10.SectorId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"LocalityIds\" name=\"LocalityIds\" multiple>\r\n");
				foreach (AllocationLocality item9 in alloc.AllocationLocalities)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da85764", async delegate
					{
						Write(item9.Locality?.Description ?? item9.LocalityId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item9.LocalityId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"FrameworkIds\" name=\"FrameworkIds\" multiple>\r\n");
				foreach (AllocationFramework item8 in alloc.AllocationFrameworks)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da88676", async delegate
					{
						Write(item8.Framework?.Description ?? item8.FrameworkId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item8.FrameworkId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"SubjectIds\" name=\"SubjectIds\" multiple>\r\n");
				foreach (AllocationSubject item7 in alloc.AllocationSubjects)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da91585", async delegate
					{
						Write(item7.Subject?.Description ?? item7.SubjectId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item7.SubjectId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"DomainIds\" name=\"DomainIds\" multiple>\r\n");
				foreach (AllocationDomain item6 in alloc.AllocationDomains)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da94485", async delegate
					{
						Write(item6.Domain?.Description ?? item6.DomainId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item6.DomainId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"EducationalProgramIds\" name=\"EducationalProgramIds\" multiple>\r\n");
				foreach (AllocationEducationalProgram item5 in alloc.AllocationEducationalPrograms)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da97418", async delegate
					{
						Write(item5.EducationalProgram?.Description ?? item5.EducationalProgramId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item5.EducationalProgramId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"ClassIds\" name=\"ClassIds\" multiple>\r\n");
				foreach (AllocationClass item4 in alloc.AllocationClasses)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da100350", async delegate
					{
						Write(item4.SchoolClass?.Description ?? item4.ClassId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item4.ClassId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"GradeLevelIds\" name=\"GradeLevelIds\" multiple>\r\n");
				foreach (AllocationGradeLevel item3 in alloc.AllocationGradeLevels)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da103263", async delegate
					{
						Write(item3.GradeLevel?.Description ?? item3.GradeLevelId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item3.GradeLevelId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"DiscussionCodeIds\" name=\"DiscussionCodeIds\" multiple>\r\n");
				foreach (AllocationDiscussionCode item2 in alloc.AllocationDiscussionCodes)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da106197", async delegate
					{
						Write(item2.DiscussionCode?.Description ?? item2.DiscussionCodeId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item2.DiscussionCodeId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n        <select class=\"allocation-store d-none\" data-field=\"LocalityDistrictNationalIds\" name=\"LocalityDistrictNationalIds\" multiple>\r\n");
				foreach (AllocationLocalityDistrictNational item in alloc.AllocationLocalityDistrictNationals)
				{
					WriteLiteral(" ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da109174", async delegate
					{
						Write(item.LocalityDistrictNational?.Description ?? item.LocalityDistrictNationalId.ToString());
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(item.LocalityDistrictNationalId);
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
					__tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, HtmlAttributeValueStyle.DoubleQuotes);
					BeginWriteTagHelperAttribute();
					__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
					__tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral(" ");
				}
				WriteLiteral("        </select>\r\n      ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_16.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_13.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
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
			WriteLiteral(alloc.Id);
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
			WriteLiteral("\r\n\r\n");
			if (User.IsInRole("1") || User.IsInRole("2"))
			{
				WriteLiteral("        ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da115133", async delegate
				{
					WriteLiteral("\r\n          ");
					Write(Html.AntiForgeryToken());
					WriteLiteral("\r\n          <input type=\"hidden\" name=\"allocationId\"");
					BeginWriteAttribute("value", " value=\"", 19178, "\"", 19195, 1);
					WriteAttributeValue("", 19186, alloc.Id, 19186, 9, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" />\r\n          <select name=\"reportingMonthId\" class=\"form-select form-select-sm\" style=\"max-width:180px\" required>\r\n            ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da116328", async delegate
					{
						WriteLiteral("חודש דיווח");
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_15.Value;
					__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
					await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
					if (!__tagHelperExecutionContext.Output.IsContentModified)
					{
						await __tagHelperExecutionContext.SetOutputContentAsync();
					}
					Write(__tagHelperExecutionContext.Output);
					__tagHelperExecutionContext = __tagHelperScopeManager.End();
					WriteLiteral("\r\n");
					foreach (ReportingMonth month in reportingMonths)
					{
						WriteLiteral("              ");
						__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1fdd7c9f7b85c5c93b54b03ac258a931296e7b40772af5fae2dfa831843954da117862", async delegate
						{
							Write(month.Description);
						});
						__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
						__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
						BeginWriteTagHelperAttribute();
						WriteLiteral(month.Id);
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
					WriteLiteral("          </select>\r\n          <input type=\"file\" name=\"file\" accept=\".xlsx\" class=\"form-control form-control-sm\" style=\"max-width:260px\" required />\r\n          <button type=\"submit\" class=\"btn btn-sm btn-outline-success\"\r\n                  onclick=\"return confirm('ייבוא אקסל יחליף שורות קיימות של ההקצאה בחודש שנבחר. להמשיך?')\">ייבוא קובץ דיווח</button>\r\n        ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_17.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_17);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(employee.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_16.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_16);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_18);
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
			WriteLiteral("    </section>\r\n");
		}
		WriteLiteral("\r\n");
		if (!base.Model.Any())
		{
			WriteLiteral("    <div class=\"text-center text-muted py-4\">אין הקצאות לעובד זה</div>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n<style>\r\n  .allocations-page .allocation-card > form {\n    padding: 0;\n  }\n\n  .allocations-page .allocation-card > form + form {\n    margin-top: 0 !important;\n  }\n\n  .allocations-page .allocation-card-selected {\n    box-shadow: 0 0 0 .18rem rgba(25, 135, 84, .16);\n  }\n\r\n  .allocations-page .allocation-shortcuts {\r\n    display: flex;\r\n    justify-content: flex-start;\r\n    gap: .5rem;\r\n    flex-wrap: wrap;\r\n  }\r\n\r\n  .allocations-page .sticky-nav-button {\r\n    min-width: 160px;\r\n  }\r\n\r\n  .allocations-page .allocation-actions {\n    flex-direction: row !important;\n    flex-wrap: wrap;\n    justify-content: flex-start;\n  }\n\n  .allocations-page .allocation-card > form > .d-flex:first-of-type {\n    align-items: center !important;\n    background: #198754;\n    color: #fff;\n    padding: .75rem 1rem;\n    border-radius: calc(.375rem - 1px) calc(.375rem - 1px) 0 0;\n    margin: 0;\n  }\n\n  .allocations-page .allocation-card > form > .d-flex:first-of-type h5 {\n    margin: 0 !important;\n    color: #fff;\n  }\n\n  .allocat");
		WriteLiteral("ions-page .allocation-card > form > .d-flex:first-of-type .btn-primary {\n    --bs-btn-color: #198754;\n    --bs-btn-bg: #fff;\n    --bs-btn-border-color: #fff;\n    --bs-btn-hover-color: #fff;\n    --bs-btn-hover-bg: transparent;\n    --bs-btn-hover-border-color: #fff;\n  }\n\n  .allocations-page .allocation-fields {\n    padding: 1rem;\n    margin: 0;\n  }\n\n  .allocations-page .allocation-fields > [class*=\"col-\"] {\n    margin-top: 1rem;\n  }\n\n  .allocations-page .allocation-fields .form-control-sm,\n  .allocations-page .allocation-fields .form-select-sm {\n    min-height: calc(2.5rem + 2px);\n    padding: .375rem .75rem;\n    font-size: 1rem;\n    border-radius: .375rem;\n  }\n\n  .allocations-page .duration-box {\n    border: 1px solid #ced4da;\n    border-radius: .375rem;\n    padding: .65rem .8rem;\n    min-height: 122px;\n    background: #fff;\n  }\n\n  .allocations-page .allocation-picker {\n    border: 1px solid #ced4da;\n    border-radius: .375rem;\n    background: #fff;\n    padding: 1rem;\n  }\n\n  .allocations-page .allocation-picke");
		WriteLiteral("r::before {\n    content: \"שיוכים\";\n    display: block;\n    font-size: 1.25rem;\n    font-weight: 700;\n    margin-bottom: 1rem;\n  }\n\r\n  .allocations-page .allocation-selected-list {\r\n    border: 1px solid #ced4da;\r\n    border-radius: .375rem;\r\n    background: #fff;\r\n    min-height: 130px;\r\n    max-height: 180px;\r\n    overflow: auto;\r\n    padding: .35rem;\r\n  }\r\n\r\n  .allocations-page .selected-item {\r\n    display: flex;\r\n    align-items: center;\r\n    gap: .5rem;\r\n    padding: .15rem .25rem;\r\n    font-weight: 600;\r\n  }\r\n\r\n  .allocations-page .remove-selection {\r\n    border: 0;\r\n    background: transparent;\r\n    color: #9b1c1c;\r\n    font-size: 1.25rem;\r\n    line-height: 1;\r\n  }\r\n\r\n  .allocations-page [disabled] {\n    cursor: not-allowed;\n  }\n\n  .allocations-page .allocation-card > form:not(:first-child),\n  .allocations-page .allocation-card > form + form {\n    background: #f8f9fa;\n    border-top: 1px solid #dee2e6;\n    padding: 1rem;\n  }\n\n  ");
		WriteLiteral("@media (min-width: 768px) {\n    .allocations-page .allocation-fields > .col-md-2 {\n      flex: 0 0 auto;\n      width: 33.333333%;\n    }\n\n    .allocations-page .allocation-fields > .col-md-2:nth-child(1),\n    .allocations-page .allocation-fields > .col-md-2:nth-child(2) {\n      width: 50%;\n    }\n\n    .allocations-page .allocation-fields > .col-md-8,\n    .allocations-page .allocation-fields > .col-md-10 {\n      flex: 0 0 auto;\n      width: 100%;\n    }\n  }\n</style>\r\n\r\n");
		DefineSection("Scripts", (RenderAsyncDelegate)async delegate
		{
			WriteLiteral("\r\n  <script>\r\n    const allocationLookupData = ");
			Write(Html.Raw(JsonSerializer.Serialize(lookupData)));
			WriteLiteral(";\n    const allocationCategoryLabels = ");
			Write(Html.Raw(JsonSerializer.Serialize(categoryLabels)));
			WriteLiteral(";\n\n    document.querySelectorAll('.whole-number-field').forEach((input) => {\n      input.addEventListener('input', () => {\n        input.value = input.value.replace(/[^\\d]/g, '');\n      });\n      input.addEventListener('wheel', () => input.blur());\n    });\n\n    document.querySelectorAll('.allocation-card').forEach(card => {\n      const form = card.querySelector('form');\r\n      const validationBox = card.querySelector('.allocation-validation');\r\n      const categorySelect = card.querySelector('.allocation-category');\r\n      const available = card.querySelector('.allocation-available');\r\n      const selectedList = card.querySelector('.allocation-selected-list');\r\n      const availableSearch = card.querySelector('.allocation-search-available');\r\n      const selectedSearch = card.querySelector('.allocation-search-selected');\r\n      const availableLabel = card.querySelector('.allocation-available-label');\r\n\r\n      function currentField() {\r\n        return categorySelect.value;\r\n      }\r\n\r\n      function currentSto");
			WriteLiteral("re() {\r\n        return card.querySelector(`.allocation-store[data-field=\"${currentField()}\"]`);\r\n      }\r\n\r\n      function selectedIds() {\r\n        return new Set(Array.from(currentStore().options).filter(o => o.selected).map(o => o.value));\r\n      }\r\n\r\n      function ensureStoreOption(field, option) {\r\n        const store = card.querySelector(`.allocation-store[data-field=\"${field}\"]`);\r\n        let existing = Array.from(store.options).find(o => o.value === option.id);\r\n        if (!existing) {\r\n          existing = new Option(option.text, option.id, true, true);\r\n          store.add(existing);\r\n        }\r\n        existing.selected = true;\r\n      }\r\n\r\n      function renderPicker() {\r\n        const field = currentField();\r\n        const label = allocationCategoryLabels[field] || 'ערך';\r\n        const allOptions = allocationLookupData[field] || [];\r\n        const picked = selectedIds();\r\n        const availableNeedle = (availableSearch.value || '').trim().toLowerCase();\r\n        const selectedNeedle = (selecte");
			WriteLiteral("dSearch.value || '').trim().toLowerCase();\r\n\r\n        availableLabel.textContent = `בחר ${label} להוספה`;\r\n        available.innerHTML = '';\r\n        allOptions\r\n          .filter(o => !picked.has(String(o.id)))\r\n          .filter(o => !availableNeedle || o.text.toLowerCase().includes(availableNeedle))\r\n          .forEach(o => available.add(new Option(o.text, o.id)));\r\n\r\n        selectedList.innerHTML = '';\r\n        Array.from(currentStore().options)\r\n          .filter(o => o.selected)\r\n          .filter(o => !selectedNeedle || o.text.toLowerCase().includes(selectedNeedle))\r\n          .forEach(o => {\r\n            const row = document.createElement('div');\r\n            row.className = 'selected-item';\r\n            const remove = document.createElement('button');\r\n            remove.type = 'button';\r\n            remove.className = 'remove-selection';\r\n            remove.textContent = '×';\r\n            remove.disabled = card.dataset.editing !== 'true';\r\n            remove.setAttribute('aria-label', `הסר ${o.text");
			WriteLiteral("}`);\r\n            remove.addEventListener('click', () => {\r\n              o.selected = false;\r\n              renderPicker();\r\n            });\r\n            const text = document.createElement('span');\r\n            text.textContent = o.text;\r\n            row.append(remove, text);\r\n            selectedList.append(row);\r\n          });\r\n      }\r\n\r\n      card.querySelector('.allocation-add')?.addEventListener('click', () => {\r\n        if (card.dataset.editing !== 'true') return;\r\n        const field = currentField();\r\n        Array.from(available.selectedOptions).forEach(option => {\r\n          const source = (allocationLookupData[field] || []).find(o => String(o.id) === option.value);\r\n          if (source) ensureStoreOption(field, source);\r\n        });\r\n        renderPicker();\r\n      });\r\n\r\n      available.addEventListener('dblclick', () => card.querySelector('.allocation-add')?.click());\r\n      categorySelect.addEventListener('change', renderPicker);\r\n      availableSearch.addEventListener('input', renderPicker);");
			WriteLiteral("\r\n      selectedSearch.addEventListener('input', renderPicker);\r\n\r\n      card.querySelector('.allocation-category-shortcut')?.addEventListener('change', event => {\r\n        const field = event.currentTarget.dataset.targetField;\r\n        categorySelect.value = field;\r\n        const store = card.querySelector(`.allocation-store[data-field=\"${field}\"]`);\r\n        if (field === 'ProgramIds' && store) {\r\n          Array.from(store.options).forEach(o => o.selected = false);\r\n          const selectedOption = event.currentTarget.selectedOptions[0];\r\n          if (selectedOption?.value) {\r\n            ensureStoreOption(field, { id: selectedOption.value, text: selectedOption.textContent });\r\n          }\r\n        }\r\n        renderPicker();\r\n      });\r\n\r\n      function setEditing(enabled) {\r\n        card.dataset.editing = enabled ? 'true' : 'false';\r\n        card.querySelectorAll('.allocation-editable').forEach(control => control.disabled = !enabled);\r\n        card.querySelectorAll('.allocation-editable-button').forEach(");
			WriteLiteral("button => button.disabled = !enabled);\r\n        renderPicker();\r\n      }\r\n\r\n      function selectedProgramCount() {\r\n        const store = card.querySelector('.allocation-store[data-field=\"ProgramIds\"]');\r\n        return Array.from(store?.options || []).filter(o => o.selected).length;\r\n      }\r\n\r\n      function validateForm() {\r\n        const messages = [];\r\n        const project = card.querySelector('[name=\"ProjectId\"]');\r\n        const monthly = card.querySelector('[name=\"MonthlyEmploymentScope\"]');\r\n        const annual = card.querySelector('[name=\"AnnualEmploymentScope\"]');\r\n        const hasDuration = !!durationUnlimited?.checked || durationValues.some(cb => cb.checked);\r\n\r\n        if (!project?.value || Number(project.value) <= 0) messages.push('יש לבחור פרויקט');\r\n        if (selectedProgramCount() === 0) messages.push('יש לבחור תוכנית');\r\n        if (monthly?.value && Number(monthly.value) < 0) messages.push('היקף פעילות חודשי חייב להיות גדול או שווה לאפס');\r\n        if (annual?.value && Number(annual");
			WriteLiteral(".value) < 0) messages.push('היקף פעילות שנתי חייב להיות גדול או שווה לאפס');\r\n        if (!hasDuration) messages.push('יש לבחור משך תפוקה אחד לפחות');\r\n\r\n        if (messages.length > 0) {\r\n          validationBox.textContent = messages.join(' | ');\r\n          validationBox.hidden = false;\r\n          setEditing(true);\r\n          return false;\r\n        }\r\n\r\n        validationBox.hidden = true;\r\n        validationBox.textContent = '';\r\n        return true;\r\n      }\r\n\r\n      card.querySelector('.allocation-edit')?.addEventListener('click', () => {\r\n        setEditing(true);\r\n        card.querySelector('.allocation-editable')?.focus();\r\n      });\r\n\r\n      const durationUnlimited = card.querySelector('.duration-unlimited');\r\n      const durationOutput = card.querySelector('.duration-output');\r\n      const durationValues = Array.from(card.querySelectorAll('.duration-value'));\r\n\r\n      function syncDurationMode() {\r\n        if (durationUnlimited?.checked) {\r\n          durationValues.forEach(cb => cb.checked = false)");
			WriteLiteral(";\r\n          durationOutput.value = 'Unlimited';\r\n        } else {\r\n          durationOutput.value = '';\r\n        }\r\n      }\r\n\r\n      durationUnlimited?.addEventListener('change', syncDurationMode);\r\n      durationValues.forEach(cb => cb.addEventListener('change', () => {\r\n        if (cb.checked && durationUnlimited) durationUnlimited.checked = false;\r\n        syncDurationMode();\r\n      }));\r\n\r\n      form?.addEventListener('submit', event => {\r\n        if (!validateForm()) {\r\n          event.preventDefault();\r\n          return;\r\n        }\r\n        card.querySelectorAll('.allocation-editable').forEach(control => control.disabled = false);\r\n      });\r\n\r\n      renderPicker();\r\n      syncDurationMode();\r\n      setEditing(false);\r\n    });\r\n\r\n    const selectedCard = document.querySelector('.allocation-card-selected');\r\n    selectedCard?.scrollIntoView({ block: 'start' });\r\n  </script>\r\n");
		});
		static string DecimalText(decimal? value)
		{
			if (!value.HasValue)
			{
				return string.Empty;
			}
			return value.Value.ToString("0.##", CultureInfo.InvariantCulture);
		}
		static bool DurationSelected(Allocation allocation, decimal value)
		{
			string text = allocation.OutputDuration ?? string.Empty;
			decimal result;
			return text.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Any((string t) => decimal.TryParse(t, NumberStyles.Number, CultureInfo.InvariantCulture, out result) && result == value);
		}
		static bool DurationUnlimited(Allocation allocation)
		{
			return (allocation.OutputDuration ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Any((string t) => t.Equals("Unlimited", StringComparison.OrdinalIgnoreCase) || t.Equals("ללא הגבלה", StringComparison.OrdinalIgnoreCase));
		}
		static string DurationValue(decimal value)
		{
			return value.ToString("0.##", CultureInfo.InvariantCulture);
		}
	}
}

