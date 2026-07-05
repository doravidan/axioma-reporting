using System;
using System.Collections.Generic;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Report/Index.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Report_Index : RazorPage<List<ReportRow>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-action", "Submit", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("asp-action", "DownloadExcelTemplate", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("asp-action", "UploadExcel", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("enctype", new HtmlString("multipart/form-data"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("class", new HtmlString("d-flex gap-2 align-items-center"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("id", new HtmlString("rowForm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("aria-label", new HtmlString("טופס שורת דיווח"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<List<ReportRow>> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "דיווח חודשי";
		User user = base.ViewBag.Employee as User;
		ReportingMonth reportingMonth = base.ViewBag.ActiveMonth as ReportingMonth;
		Report report = base.ViewBag.Report as Report;
		Allocation allocation = base.ViewBag.Allocation as Allocation;
		bool canEdit = base.ViewBag.CanEdit ?? ((object)false);
		int allocationId = base.ViewBag.AllocationId ?? ((object)0);
		_ = (bool)(base.ViewBag.DeadlinePassed ?? ((object)false));
		bool flag = base.ViewBag.DeadlineOverrideActive ?? ((object)false);
		int? editRowId = base.ViewBag.EditRowId as int?;
		string returnUrl = base.ViewBag.ReturnUrl as string;
		string value = base.ViewBag.DeadlineBlockMessage as string;
		string reportRowVersion = ((report?.RowVersion != null) ? Convert.ToBase64String(report.RowVersion) : string.Empty);
		List<DocumentAttachment> reportAttachments = (base.ViewBag.ReportAttachments as List<DocumentAttachment>) ?? new List<DocumentAttachment>();
		List<District> districts = (from x in allocation?.AllocationDistricts
			select x.District into x
			where x != null
			select (x)).ToList() ?? new List<District>();
		List<Locality> localities = (from x in allocation?.AllocationLocalities
			select x.Locality into x
			where x != null
			select (x)).ToList() ?? new List<Locality>();
		List<Locality> manualLocalities = (base.ViewData["ManualLocalities"] as List<Locality>) ?? new List<Locality>();
		if (manualLocalities.Count > 0)
		{
			localities = manualLocalities;
		}
		string[] nonCityLocalityTokens = new[]
		{
			"בית ספר", "בתי ספר", "בי\"ס", "בי'ס", "אולפנ", "אורט", "מח\"ט", "מועדונית", "מרכז נוער", "מרכזי חינוך", "מרכזים לגיל הרך", "מרכז לגיל הרך", "גיל הרך",
			"עוגנים", "מסגרות", "כיתות", "על יסודי", "תיכון", "ישיבה", "ישיבת", "תורה", "תלמוד", "חינוך", "אמי\"ת", "אמי״ת", "עמל", "הילה ", "בית חם",
			"תעשית", "חברה וטבע", "ברסלב", "לצעירים", "מדרשה", "מכנובקא", "ק.הרצוג", "ברנקו", "משכן", "אהבת", "באר אברהם", "בית דוד", "בית אליהו",
			"בית צבי", "בית רבן", "בני אהרון", "אמרי", "אקרא", "היכל"
		};
		Func<string?, bool> isCityLocalityText = delegate(string? value)
		{
			string text = value?.Trim() ?? string.Empty;
			return !string.IsNullOrWhiteSpace(text) && !int.TryParse(text, out var _) && !nonCityLocalityTokens.Any((string token) => text.Contains(token, StringComparison.OrdinalIgnoreCase));
		};
		localities = localities.Where((Locality x) => isCityLocalityText(x.Description)).OrderBy((Locality x) => x.Description).ToList();
		List<Framework> frameworks = (from x in allocation?.AllocationFrameworks
			select x.Framework into x
			where x != null
			select (x)).ToList() ?? new List<Framework>();
		List<Framework> conclusionFrameworks = frameworks.Where((Framework x) => !int.TryParse(x.InstitutionSymbol?.Trim(), out var _)).OrderBy((Framework x) => x.Description).ToList();
		List<Framework> institutionFrameworks = frameworks.Where((Framework x) => int.TryParse(x.InstitutionSymbol?.Trim(), out var _)).OrderBy((Framework x) => x.Description).ToList();
		if (institutionFrameworks.Count > 0)
		{
			frameworks = institutionFrameworks;
		}
		List<EducationalProgram> edPrograms = (from x in allocation?.AllocationEducationalPrograms
			select x.EducationalProgram into x
			where x != null
			select (x)).ToList() ?? new List<EducationalProgram>();
		List<Domain> domains = (from x in allocation?.AllocationDomains
			select x.Domain into x
			where x != null
			select (x)).ToList() ?? new List<Domain>();
		List<Subject> subjects = (from x in allocation?.AllocationSubjects
			select x.Subject into x
			where x != null
			select (x)).ToList() ?? new List<Subject>();
		List<DiscussionCode> discCodes = (from x in allocation?.AllocationDiscussionCodes
			select x.DiscussionCode into x
			where x != null
			select (x)).ToList() ?? new List<DiscussionCode>();
		List<GradeLevel> gradeLevels = (from x in allocation?.AllocationGradeLevels
			select x.GradeLevel into x
			where x != null
			select (x)).ToList() ?? new List<GradeLevel>();
		List<SchoolClass> classes = (from x in allocation?.AllocationClasses
			select x.SchoolClass into x
			where x != null
			select (x)).ToList() ?? new List<SchoolClass>();
		List<SchoolClass> conclusionClasses = classes.Where((SchoolClass x) => !int.TryParse(x.Description?.Trim(), out var _)).OrderBy((SchoolClass x) => x.Description).ToList();
		List<SchoolClass> numericClasses = classes.Where((SchoolClass x) => int.TryParse(x.Description?.Trim(), out var _)).OrderBy((SchoolClass x) => int.TryParse(x.Description?.Trim(), out int value) ? value : int.MaxValue).ThenBy((SchoolClass x) => x.Description).ToList();
		if (numericClasses.Count > 0)
		{
			classes = numericClasses;
		}
		List<LocalityDistrictNational> locDist = (from x in allocation?.AllocationLocalityDistrictNationals
			select x.LocalityDistrictNational into x
			where x != null
			select (x)).ToList() ?? new List<LocalityDistrictNational>();
		HashSet<string> requiredFields = (base.ViewBag.RequiredReportFields as HashSet<string>) ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		string[] source = (allocation?.OutputDuration ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		decimal result;
		List<decimal> durationOptions = (from v in source.Where((string t) => decimal.TryParse(t, out result)).Select(decimal.Parse)
			orderby v
			select v).ToList();
		bool durationUnlimited = source.Any((string t) => t.Equals("Unlimited", StringComparison.OrdinalIgnoreCase) || t.Equals("ללא הגבלה", StringComparison.OrdinalIgnoreCase));
		Dictionary<string, object> inlineLookupData = new Dictionary<string, object>
		{
			["DistrictId"] = districts.Select((District x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["LocalityId"] = localities.Select((Locality x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["FrameworkId"] = frameworks.Select((Framework x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["EducationalProgramId"] = edPrograms.Select((EducationalProgram x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["DomainId"] = domains.Select((Domain x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["Subject1Id"] = subjects.Select((Subject x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["Subject2Id"] = subjects.Select((Subject x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["DiscussionCodeId"] = discCodes.Select((DiscussionCode x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["ConclusionClassId"] = conclusionClasses.Select((SchoolClass x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["ConclusionFrameworkId"] = conclusionFrameworks.Select((Framework x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["ConclusionLocationId"] = locDist.Select((LocalityDistrictNational x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["GradeLevelId"] = gradeLevels.Select((GradeLevel x) => new
			{
				id = x.Id,
				text = x.Description
			}),
			["ClassId"] = classes.Select((SchoolClass x) => new
			{
				id = x.Id,
				text = x.Description
			})
		};
		WriteLiteral("\n");
		if (canEdit)
		{
			WriteLiteral("  <style>\n    #reportTable td.editable-cell {\n      cursor: pointer;\n      position: relative;\n    }\n\n    #reportTable td.editable-cell:hover {\n      background: #fff7ed;\n      box-shadow: inset 0 0 0 1px #e06a2f;\n    }\n\n    #reportTable td.cell-editing {\n      min-width: 180px;\n      background: #fff7ed;\n    }\n\n    #reportTable .cell-edit-actions {\n      display: flex;\n      gap: .25rem;\n      margin-top: .25rem;\n      justify-content: flex-start;\n    }\n\n    #reportTable .cell-edit-error {\n      color: #b42318;\n      font-weight: 700;\n      font-size: .8rem;\n      margin-top: .25rem;\n      white-space: normal;\n    }\n  </style>\n");
		}
		WriteLiteral("\r\n<div class=\"container-fluid mt-3\">\n  <div class=\"d-flex justify-content-end gap-2 mb-3 flex-wrap\">\n    <a class=\"btn btn-outline-secondary btn-sm\" href=\"/Report/History");
		if (user != null)
		{
			WriteLiteral("?userId=");
			Write(user.Id);
		}
		WriteLiteral("\">היסטוריית דיווחים</a>\n    <a class=\"btn btn-primary btn-sm\" href=\"/Report/Manual\">\u05d4\u05d5\u05e1\u05e4\u05ea \u05d3\u05d9\u05d5\u05d5\u05d7 \u05d9\u05d3\u05e0\u05d9</a>\n  </div>\n");
		if (!string.IsNullOrWhiteSpace(returnUrl))
		{
			WriteLiteral("    <div class=\"mb-3\">\n      <a class=\"btn btn-outline-secondary btn-sm\"");
			BeginWriteAttribute("href", " href=\"", 6282, "\"", 6299, 1);
			WriteAttributeValue("", 6289, returnUrl, 6289, 10, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">חזרה לדשבורד דיווחים</a>\n    </div>\n");
		}
		WriteLiteral("\n");
		if (base.TempData["Success"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\">\r\n      ");
			Write(base.TempData["Success"]);
			WriteLiteral("\r\n      <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"סגור הודעה\"></button>\r\n    </div>\r\n");
		}
		if (base.TempData["Errors"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-danger\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\">\r\n      ");
			Write(Html.Raw(string.Join("<br/>", (base.TempData["Errors"].ToString() ?? "").Split('|'))));
			WriteLiteral("\r\n");
			if (base.TempData["ExcelErrorFile"] != null)
			{
				WriteLiteral("        <div class=\"mt-2\">\r\n          <a");
				BeginWriteAttribute("href", " href=\"", 6971, "\"", 7004, 1);
				WriteAttributeValue("", 6978, base.TempData["ExcelErrorFile"], 6978, 26, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" target=\"_blank\" class=\"btn btn-sm btn-outline-danger\">הורד Excel שגיאות</a>\r\n        </div>\r\n");
			}
			WriteLiteral("    </div>\r\n");
		}
		WriteLiteral("\r\n");
		if (!string.IsNullOrEmpty(value))
		{
			WriteLiteral("    <div class=\"alert alert-warning\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\">\r\n      ");
			Write(value);
			WriteLiteral("\r\n    </div>\r\n");
		}
		else if (flag)
		{
			WriteLiteral("    <div class=\"alert alert-info\" role=\"alert\" aria-live=\"polite\" aria-atomic=\"true\">\r\n      מצב דריסה — המועד עבר\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n  <div class=\"card mb-3\">\r\n    <div class=\"card-body\">\r\n      <div class=\"row g-2\">\r\n        <div class=\"col-md-3\"><strong>עובד:</strong> ");
		Write(user?.FirstName);
		WriteLiteral(" ");
		Write(user?.LastName);
		WriteLiteral("</div>\r\n        <div class=\"col-md-3\"><strong>ת.ז:</strong> ");
		Write(user?.IdNumber);
		WriteLiteral("</div>\r\n        <div class=\"col-md-3\"><strong>קוד עובד:</strong> ");
		Write(user?.EmployeeCode);
		WriteLiteral("</div>\r\n        <div class=\"col-md-3\"><strong>חודש:</strong> ");
		Write(reportingMonth?.Description);
		WriteLiteral("</div>\r\n        <div class=\"col-md-3\"><strong>סטטוס:</strong> ");
		Write(StatusText(report?.StatusId, report?.Status?.Name));
		WriteLiteral("</div>\n        <div class=\"col-md-3\"><strong>שורות בהקצאה:</strong> ");
		Write(base.Model.Count);
		WriteLiteral(" / ");
		Write(allocation?.MonthlyRowAllocation);
		WriteLiteral("</div>\r\n");
		if (report != null)
		{
			WriteLiteral("        <div class=\"col-md-3\"><a class=\"btn btn-sm btn-outline-success\" href=\"/Report/ExportReportMonth?reportId=");
			Write(report.Id);
			WriteLiteral("\">ייצוא אקסל חודשי</a></div>\r\n");
		}
		WriteLiteral("        <div class=\"col-md-3 text-start\">\r\n");
		if (base.ViewBag.Allocations is List<Allocation> { Count: >1 })
		{
			WriteLiteral("            ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f22009", async delegate
			{
				WriteLiteral("שנה פרויקט");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-userId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(user?.Id);
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["userId"] = __tagHelperStringValueBuffer;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-userId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["userId"], HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
		}
		WriteLiteral("        </div>\r\n      </div>\r\n    </div>\r\n  </div>\n\n  <div class=\"card mb-3\">\n    <div class=\"card-header\">\n      <h5 class=\"mb-0\">מסמכי הדיווח</h5>\n    </div>\n    <div class=\"card-body\">\n");
		if (canEdit)
		{
			WriteLiteral("        <div class=\"row g-2 align-items-end mb-3\">\n          <div class=\"col-md-5\">\n            <label class=\"form-label\" for=\"report-file-description\">תיאור מסמך</label>\n            <input type=\"text\" id=\"report-file-description\" class=\"form-control\"\n                   maxlength=\"1000\" placeholder=\"תיאור המסמך\" />\n          </div>\n          <div class=\"col-md-5\">\n            <label class=\"form-label\" for=\"report-file-upload\">בחר מסמך</label>\n            <input type=\"file\" id=\"report-file-upload\" class=\"form-control\"\n                   accept=\".pdf,.doc,.docx,.xls,.xlsx,application/pdf,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet\" />\n          </div>\n          <div class=\"col-md-2\">\n            <button type=\"button\" class=\"btn btn-primary w-100\" onclick=\"uploadAttachment()\">העלה מסמך</button>\n          </div>\n        </div>\n");
		}
		WriteLiteral("\n");
		if (reportAttachments.Any())
		{
			WriteLiteral("        <div class=\"table-responsive\">\n          <table class=\"table table-sm table-bordered mb-0\">\n            <thead class=\"table-light\">\n              <tr>\n                <th>שם קובץ</th>\n                <th>תיאור</th>\n                <th>תאריך העלאה</th>\n");
			if (canEdit)
			{
				WriteLiteral(" <th>פעולות</th> ");
			}
			WriteLiteral("              </tr>\n            </thead>\n            <tbody>\n");
			foreach (DocumentAttachment item14 in reportAttachments)
			{
				WriteLiteral("                <tr>\n                  <td><a");
				BeginWriteAttribute("href", " href=\"", 10267, "\"", 10288, 1);
				WriteAttributeValue("", 10274, item14.FilePath, 10274, 14, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" target=\"_blank\">");
				Write(item14.FileName);
				WriteLiteral("</a></td>\n                  <td>");
				Write(item14.Description);
				WriteLiteral("</td>\n                  <td>");
				Write(item14.UploadedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"));
				WriteLiteral("</td>\n");
				if (canEdit)
				{
					WriteLiteral("                    <td>\n                      <button type=\"button\" class=\"btn btn-sm btn-outline-danger\"");
					BeginWriteAttribute("onclick", "\n                              onclick=\"", 10620, "\"", 10686, 3);
					WriteAttributeValue("", 10660, "deleteAttachment(", 10660, 17, isLiteral: true);
					WriteAttributeValue("", 10677, item14.Id, 10677, 8, isLiteral: false);
					WriteAttributeValue("", 10685, ")", 10685, 1, isLiteral: true);
					EndWriteAttribute();
					WriteLiteral(">מחק</button>\n                    </td>\n");
				}
				WriteLiteral("                </tr>\n");
			}
			WriteLiteral("            </tbody>\n          </table>\n        </div>\n");
		}
		else
		{
			WriteLiteral("        <div class=\"text-muted\">אין מסמכים מצורפים לדיווח</div>\n");
		}
		WriteLiteral("    </div>\n  </div>\n\n  <div class=\"card\">\n    <div class=\"card-header d-flex justify-content-between align-items-center\">\r\n      <h5 class=\"mb-0\" id=\"reportTableCaption\">שורות דיווח</h5>\r\n");
		if (canEdit)
		{
			WriteLiteral("        <button class=\"btn btn-primary btn-sm\" onclick=\"addRow()\" aria-label=\"הוסף שורת דיווח חדשה\">+ הוסף שורה</button>\r\n");
		}
		WriteLiteral("    </div>\r\n    <div class=\"card-body p-0\">\r\n      <div class=\"table-responsive\">\r\n        <table class=\"table table-bordered table-sm mb-0\" id=\"reportTable\" aria-labelledby=\"reportTableCaption\">\r\n          <thead class=\"table-light\">\r\n            <tr>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">מס\"ד</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">סוג דיווח</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">תאריך</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">משך</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">מחוז</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">ישוב</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">מסגרת</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">תוכנית</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">תחום</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">נושא 1</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">נושא 2</th>\r\n             ");
		WriteLiteral(" <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">קיום דיון</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">מסקנה-כיתה</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">מסקנה-מסגרת</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">מסקנה-מיקום</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">שכבה</th>\r\n              <th scope=\"col\" class=\"sortable\" aria-sort=\"none\">כיתה</th>\r\n              <th scope=\"col\">הערות</th>\n");
		if (canEdit)
		{
			WriteLiteral(" <th scope=\"col\">פעולות</th> ");
		}
		WriteLiteral("            </tr>\r\n          </thead>\r\n          <tbody>\r\n");
		foreach (ReportRow item15 in base.Model)
		{
			string value2 = ((item15.RowVersion != null) ? Convert.ToBase64String(item15.RowVersion) : "");
			WriteLiteral("              <tr data-row-id=\"");
			Write(item15.Id);
			WriteLiteral("\" data-row-version=\"");
			Write(value2);
			WriteLiteral("\">\r\n                <td>");
			Write(item15.SequenceNumber);
			WriteLiteral("</td>\r\n                <td>");
			Write(item15.ReportType?.Description ?? allocation?.ReportType?.Description);
			WriteLiteral("</td>\r\n                <td ");
			Write(Html.Raw(FieldAttrs("MeetingDate", "date")));
			WriteLiteral(">");
			Write(item15.MeetingDate.ToString("dd/MM/yyyy"));
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("MeetingDuration", "duration")));
			WriteLiteral(">");
			Write(item15.MeetingDuration);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("DistrictId")));
			WriteLiteral(">");
			Write(item15.District?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("LocalityId")));
			WriteLiteral(">");
			Write(item15.Locality?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("FrameworkId")));
			WriteLiteral(">");
			Write(item15.Framework?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("EducationalProgramId")));
			WriteLiteral(">");
			Write(item15.EducationalProgram?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("DomainId")));
			WriteLiteral(">");
			Write(item15.Domain?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("Subject1Id")));
			WriteLiteral(">");
			Write(item15.Subject1?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("Subject2Id", "select", optional: true)));
			WriteLiteral(">");
			Write(item15.Subject2?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("DiscussionCodeId", "select", optional: true)));
			WriteLiteral(">");
			Write(item15.DiscussionCode?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("ConclusionClassId", "select", optional: true)));
			WriteLiteral(">");
			Write(item15.ConclusionClass?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("ConclusionFrameworkId", "select", optional: true)));
			WriteLiteral(">");
			Write(item15.ConclusionFramework?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("ConclusionLocationId", "select", optional: true)));
			WriteLiteral(">");
			Write(item15.ConclusionLocation?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("GradeLevelId", "select", optional: true)));
			WriteLiteral(">");
			Write(item15.GradeLevel?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("ClassId", "select", optional: true)));
			WriteLiteral(">");
			Write(item15.Class?.Description);
			WriteLiteral("</td>\n                <td ");
			Write(Html.Raw(FieldAttrs("Notes", "textarea", optional: true)));
			WriteLiteral(">");
			Write(item15.Notes);
			WriteLiteral("</td>\n");
			if (canEdit)
			{
				WriteLiteral("                  <td>\r\n                    <button class=\"btn btn-sm btn-outline-secondary\"");
				BeginWriteAttribute("onclick", " onclick=\"", 14991, "\"", 15017, 3);
				WriteAttributeValue("", 15001, "editRow(", 15001, 8, isLiteral: true);
				WriteAttributeValue("", 15009, item15.Id, 15009, 7, isLiteral: false);
				WriteAttributeValue("", 15016, ")", 15016, 1, isLiteral: true);
				EndWriteAttribute();
				BeginWriteAttribute("aria-label", "\r\n                            aria-label=\"", 15018, "\"", 15089, 3);
				WriteAttributeValue("", 15060, "ערוך", 15060, 4, isLiteral: true);
				WriteAttributeValue(" ", 15064, "שורה", 15065, 5, isLiteral: true);
				WriteAttributeValue(" ", 15069, item15.SequenceNumber, 15070, 19, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">ערוך</button>\r\n                    <button class=\"btn btn-sm btn-outline-dark\"");
				BeginWriteAttribute("onclick", " onclick=\"", 15169, "\"", 15201, 3);
				WriteAttributeValue("", 15179, "inlineEditRow(", 15179, 14, isLiteral: true);
				WriteAttributeValue("", 15193, item15.Id, 15193, 7, isLiteral: false);
				WriteAttributeValue("", 15200, ")", 15200, 1, isLiteral: true);
				EndWriteAttribute();
				BeginWriteAttribute("aria-label", "\r\n                            aria-label=\"", 15202, "\"", 15279, 4);
				WriteAttributeValue("", 15244, "ערוך", 15244, 4, isLiteral: true);
				WriteAttributeValue(" ", 15248, "שורה", 15249, 5, isLiteral: true);
				WriteAttributeValue(" ", 15253, item15.SequenceNumber, 15254, 19, isLiteral: false);
				WriteAttributeValue(" ", 15273, "בטבלה", 15274, 6, isLiteral: true);
				EndWriteAttribute();
				WriteLiteral(">עריכה בשורה</button>\r\n                    <button class=\"btn btn-sm btn-outline-primary\"");
				BeginWriteAttribute("onclick", " onclick=\"", 15369, "\"", 15400, 3);
				WriteAttributeValue("", 15379, "duplicateRow(", 15379, 13, isLiteral: true);
				WriteAttributeValue("", 15392, item15.Id, 15392, 7, isLiteral: false);
				WriteAttributeValue("", 15399, ")", 15399, 1, isLiteral: true);
				EndWriteAttribute();
				BeginWriteAttribute("aria-label", "\r\n                            aria-label=\"", 15401, "\"", 15472, 3);
				WriteAttributeValue("", 15443, "שכפל", 15443, 4, isLiteral: true);
				WriteAttributeValue(" ", 15447, "שורה", 15448, 5, isLiteral: true);
				WriteAttributeValue(" ", 15452, item15.SequenceNumber, 15453, 19, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">שכפל</button>\r\n                    <button class=\"btn btn-sm btn-outline-danger\"");
				BeginWriteAttribute("onclick", " onclick=\"", 15554, "\"", 15582, 3);
				WriteAttributeValue("", 15564, "deleteRow(", 15564, 10, isLiteral: true);
				WriteAttributeValue("", 15574, item15.Id, 15574, 7, isLiteral: false);
				WriteAttributeValue("", 15581, ")", 15581, 1, isLiteral: true);
				EndWriteAttribute();
				BeginWriteAttribute("aria-label", "\r\n                            aria-label=\"", 15583, "\"", 15653, 3);
				WriteAttributeValue("", 15625, "מחק", 15625, 3, isLiteral: true);
				WriteAttributeValue(" ", 15628, "שורה", 15629, 5, isLiteral: true);
				WriteAttributeValue(" ", 15633, item15.SequenceNumber, 15634, 19, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">מחק</button>\r\n                  </td>\r\n");
			}
			WriteLiteral("              </tr>\r\n");
		}
		WriteLiteral("          </tbody>\r\n        </table>\r\n      </div>\r\n    </div>\r\n  </div>\r\n\r\n");
		bool flag2 = canEdit;
		bool flag3 = flag2;
		if (flag3)
		{
			bool flag4;
			switch (report?.StatusId)
			{
			case 1:
			case 2:
			case 5:
				flag4 = true;
				break;
			default:
				flag4 = false;
				break;
			}
			flag3 = flag4;
		}
		if (flag3)
		{
			WriteLiteral("    <div class=\"mt-3 d-flex flex-wrap gap-2\">\r\n      ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f49914", async delegate
			{
				WriteLiteral("\r\n        ");
				Write(Html.AntiForgeryToken());
				WriteLiteral("\r\n        <input type=\"hidden\" name=\"allocationId\"");
				BeginWriteAttribute("value", " value=\"", 16092, "\"", 16113, 1);
				WriteAttributeValue("", 16100, allocationId, 16100, 13, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\n        <input type=\"hidden\" name=\"rowVersion\"");
				BeginWriteAttribute("value", " value=\"", 16164, "\"", 16189, 1);
				WriteAttributeValue("", 16172, reportRowVersion, 16172, 17, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\n        <input type=\"hidden\" name=\"returnUrl\"");
				BeginWriteAttribute("value", " value=\"", 16239, "\"", 16257, 1);
				WriteAttributeValue("", 16247, returnUrl, 16247, 10, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\n        <button type=\"submit\" class=\"btn btn-success\"\r\n                onclick=\"return confirm('האם להגיש את הדיווח?')\">הגשת דיווח</button>\r\n      ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-reportId", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(report?.Id);
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["reportId"] = __tagHelperStringValueBuffer;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-reportId", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["reportId"], HtmlAttributeValueStyle.DoubleQuotes);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			if (allocation?.AllowExcelUpload ?? false)
			{
				WriteLiteral("        ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f54711", async delegate
				{
					WriteLiteral("הורד תבנית אקסל");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n        ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f55991", async delegate
				{
					WriteLiteral("\r\n          ");
					Write(Html.AntiForgeryToken());
					WriteLiteral("\r\n          <input type=\"hidden\" name=\"reportId\"");
					BeginWriteAttribute("value", " value=\"", 16792, "\"", 16811, 1);
					WriteAttributeValue("", 16800, report?.Id, 16800, 11, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" />\n          <input type=\"hidden\" name=\"allocationId\"");
					BeginWriteAttribute("value", " value=\"", 16866, "\"", 16887, 1);
					WriteAttributeValue("", 16874, allocationId, 16874, 13, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" />\n          <input type=\"hidden\" name=\"returnUrl\"");
					BeginWriteAttribute("value", " value=\"", 16939, "\"", 16957, 1);
					WriteAttributeValue("", 16947, returnUrl, 16947, 10, isLiteral: false);
					EndWriteAttribute();
					WriteLiteral(" />\n          <input type=\"file\" name=\"file\" accept=\".xlsx\" class=\"form-control form-control-sm\" required />\r\n          <button type=\"submit\" class=\"btn btn-outline-primary btn-sm\"\r\n                  onclick=\"return confirm('העלאת אקסל תחליף את שורות ההקצאה הנוכחיות. להמשיך?')\">ייבוא אקסל</button>\r\n        ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
				__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
			}
			WriteLiteral("    </div>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n");
		if (!canEdit)
		{
			return;
		}
		WriteLiteral("  <div class=\"modal fade\" id=\"rowModal\" tabindex=\"-1\"\r\n       aria-labelledby=\"rowModalTitle\" aria-modal=\"true\" role=\"dialog\">\r\n    <div class=\"modal-dialog modal-xl\" role=\"dialog\" aria-modal=\"true\">\r\n      <div class=\"modal-content\">\r\n        <div class=\"modal-header\">\r\n          <h5 class=\"modal-title\" id=\"rowModalTitle\">עריכת שורת דיווח</h5>\r\n          <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון עריכה\"></button>\r\n        </div>\r\n        <div class=\"modal-body\">\r\n          ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f61058", async delegate
		{
			WriteLiteral("\r\n            ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n            <input type=\"hidden\" id=\"rowId\" name=\"row.Id\" value=\"0\" />\r\n            <input type=\"hidden\" id=\"rowRowVersion\" name=\"rowVersion\"");
			BeginWriteAttribute("value", " value=\"", 18091, "\"", 18099, 0);
			EndWriteAttribute();
			WriteLiteral(" />\r\n            <input type=\"hidden\" name=\"reportId\"");
			BeginWriteAttribute("value", " value=\"", 18153, "\"", 18172, 1);
			WriteAttributeValue("", 18161, report?.Id, 18161, 11, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n            <input type=\"hidden\" name=\"allocationId\"");
			BeginWriteAttribute("value", " value=\"", 18230, "\"", 18251, 1);
			WriteAttributeValue("", 18238, allocationId, 18238, 13, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" />\r\n            <div class=\"row g-2\">\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldDate\">תאריך");
			Write(Star("MeetingDate"));
			WriteLiteral("</label>\r\n                <input type=\"date\" id=\"fieldDate\" name=\"row.MeetingDate\" class=\"form-control\"");
			BeginWriteAttribute("required", "\r\n                       required=\"", 18518, "\"", 18572, 1);
			WriteAttributeValue("", 18553, Req("MeetingDate"), 18553, 19, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 18573, "\"", 18629, 1);
			WriteAttributeValue("", 18589, Req("MeetingDate") ? "true" : "false", 18589, 40, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral("\r\n                       aria-describedby=\"rowErrors\" />\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldDuration\">משך תפוקה");
			Write(Star("MeetingDuration"));
			WriteLiteral("</label>\r\n");
			if (durationOptions.Any() && !durationUnlimited)
			{
				WriteLiteral("                  <select id=\"fieldDuration\" name=\"row.MeetingDuration\" class=\"form-select\"");
				BeginWriteAttribute("required", "\r\n                          required=\"", 19032, "\"", 19093, 1);
				WriteAttributeValue("", 19070, Req("MeetingDuration"), 19070, 23, isLiteral: false);
				EndWriteAttribute();
				BeginWriteAttribute("aria-required", " aria-required=\"", 19094, "\"", 19154, 1);
				WriteAttributeValue("", 19110, Req("MeetingDuration") ? "true" : "false", 19110, 44, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral("\r\n                          aria-describedby=\"rowErrors\">\r\n                    ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f66173", async delegate
				{
					WriteLiteral("בחר...");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral("\r\n");
				foreach (decimal value3 in durationOptions)
				{
					WriteLiteral("                      ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f67708", async delegate
					{
						Write(value3);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(value3);
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
				WriteLiteral("                  </select>\r\n");
			}
			else
			{
				WriteLiteral("                  <input type=\"number\" id=\"fieldDuration\" name=\"row.MeetingDuration\" class=\"form-control\"\r\n                         step=\"0.5\" min=\"0.5\"");
				BeginWriteAttribute("required", " required=\"", 19678, "\"", 19712, 1);
				WriteAttributeValue("", 19689, Req("MeetingDuration"), 19689, 23, isLiteral: false);
				EndWriteAttribute();
				BeginWriteAttribute("aria-required", "\r\n                         aria-required=\"", 19713, "\"", 19799, 1);
				WriteAttributeValue("", 19755, Req("MeetingDuration") ? "true" : "false", 19755, 44, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral("\r\n                         aria-describedby=\"rowErrors\" />\r\n");
			}
			WriteLiteral("              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldDistrict\">מחוז");
			Write(Star("DistrictId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldDistrict\" name=\"row.DistrictId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 20120, "\"", 20174, 1);
			WriteAttributeValue("", 20156, Req("DistrictId"), 20156, 18, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 20175, "\"", 20230, 1);
			WriteAttributeValue("", 20191, Req("DistrictId") ? "true" : "false", 20191, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f72893", async delegate
			{
				WriteLiteral("בחר...");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (District item13 in districts)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f74380", async delegate
				{
					Write(item13.Description);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(item13.Id);
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
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldLocality\">ישוב");
			Write(Star("LocalityId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldLocality\" name=\"row.LocalityId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 20662, "\"", 20716, 1);
			WriteAttributeValue("", 20698, Req("LocalityId"), 20698, 18, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 20717, "\"", 20772, 1);
			WriteAttributeValue("", 20733, Req("LocalityId") ? "true" : "false", 20733, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f77942", async delegate
			{
				WriteLiteral("בחר...");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (Locality item12 in localities)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f79430", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldFramework\">מסגרת");
			Write(Star("FrameworkId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldFramework\" name=\"row.FrameworkId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 21210, "\"", 21265, 1);
			WriteAttributeValue("", 21246, Req("FrameworkId"), 21246, 19, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 21266, "\"", 21322, 1);
			WriteAttributeValue("", 21282, Req("FrameworkId") ? "true" : "false", 21282, 40, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f82999", async delegate
			{
				WriteLiteral("בחר...");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (Framework item11 in frameworks)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f84487", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldEduProgram\">תוכנית");
			Write(Star("EducationalProgramId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldEduProgram\" name=\"row.EducationalProgramId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 21781, "\"", 21845, 1);
			WriteAttributeValue("", 21817, Req("EducationalProgramId"), 21817, 28, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 21846, "\"", 21911, 1);
			WriteAttributeValue("", 21862, Req("EducationalProgramId") ? "true" : "false", 21862, 49, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f88096", async delegate
			{
				WriteLiteral("בחר...");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (EducationalProgram item10 in edPrograms)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f89584", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldDomain\">תחום");
			Write(Star("DomainId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldDomain\" name=\"row.DomainId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 22336, "\"", 22388, 1);
			WriteAttributeValue("", 22372, Req("DomainId"), 22372, 16, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 22389, "\"", 22442, 1);
			WriteAttributeValue("", 22405, Req("DomainId") ? "true" : "false", 22405, 37, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f93134", async delegate
			{
				WriteLiteral("בחר...");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (Domain item9 in domains)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f94619", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldSubject1\">נושא 1");
			Write(Star("Subject1Id"));
			WriteLiteral("</label>\r\n                <select id=\"fieldSubject1\" name=\"row.Subject1Id\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 22874, "\"", 22928, 1);
			WriteAttributeValue("", 22910, Req("Subject1Id"), 22910, 18, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 22929, "\"", 22984, 1);
			WriteAttributeValue("", 22945, Req("Subject1Id") ? "true" : "false", 22945, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f98183", async delegate
			{
				WriteLiteral("בחר...");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (Subject item8 in subjects)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f99669", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldSubject2\">נושא 2");
			Write(Star("Subject2Id"));
			WriteLiteral("</label>\r\n                <select id=\"fieldSubject2\" name=\"row.Subject2Id\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 23417, "\"", 23471, 1);
			WriteAttributeValue("", 23453, Req("Subject2Id"), 23453, 18, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 23472, "\"", 23527, 1);
			WriteAttributeValue("", 23488, Req("Subject2Id") ? "true" : "false", 23488, 39, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f103233", async delegate
			{
				WriteLiteral("---");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (Subject item7 in subjects)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f104717", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldDiscussion\">קיום דיון");
			Write(Star("DiscussionCodeId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldDiscussion\" name=\"row.DiscussionCodeId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 23975, "\"", 24035, 1);
			WriteAttributeValue("", 24011, Req("DiscussionCodeId"), 24011, 24, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 24036, "\"", 24097, 1);
			WriteAttributeValue("", 24052, Req("DiscussionCodeId") ? "true" : "false", 24052, 45, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f108312", async delegate
			{
				WriteLiteral("---");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (DiscussionCode item6 in discCodes)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f109797", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldConclusionClass\">מסקנה - כיתה");
			Write(Star("ConclusionClassId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldConclusionClass\" name=\"row.ConclusionClassId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 24562, "\"", 24623, 1);
			WriteAttributeValue("", 24598, Req("ConclusionClassId"), 24598, 25, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 24624, "\"", 24686, 1);
			WriteAttributeValue("", 24640, Req("ConclusionClassId") ? "true" : "false", 24640, 46, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f113411", async delegate
			{
				WriteLiteral("---");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (SchoolClass item5 in conclusionClasses)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f114894", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldConclusionFramework\">מסקנה - מסגרת");
			Write(Star("ConclusionFrameworkId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldConclusionFramework\" name=\"row.ConclusionFrameworkId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 25166, "\"", 25231, 1);
			WriteAttributeValue("", 25202, Req("ConclusionFrameworkId"), 25202, 29, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 25232, "\"", 25298, 1);
			WriteAttributeValue("", 25248, Req("ConclusionFrameworkId") ? "true" : "false", 25248, 50, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f118533", async delegate
			{
				WriteLiteral("---");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (Framework item4 in conclusionFrameworks)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f120019", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldConclusionLocation\">מסקנה - מיקום");
			Write(Star("ConclusionLocationId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldConclusionLocation\" name=\"row.ConclusionLocationId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 25777, "\"", 25841, 1);
			WriteAttributeValue("", 25813, Req("ConclusionLocationId"), 25813, 28, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 25842, "\"", 25907, 1);
			WriteAttributeValue("", 25858, Req("ConclusionLocationId") ? "true" : "false", 25858, 49, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f123652", async delegate
			{
				WriteLiteral("---");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (LocalityDistrictNational item3 in locDist)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f125135", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldGradeLevel\">שכבה");
			Write(Star("GradeLevelId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldGradeLevel\" name=\"row.GradeLevelId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 26342, "\"", 26398, 1);
			WriteAttributeValue("", 26378, Req("GradeLevelId"), 26378, 20, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 26399, "\"", 26456, 1);
			WriteAttributeValue("", 26415, Req("GradeLevelId") ? "true" : "false", 26415, 41, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f128710", async delegate
			{
				WriteLiteral("---");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (GradeLevel item2 in gradeLevels)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f130197", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-md-2\">\r\n                <label class=\"form-label\" for=\"fieldClass\">כיתה");
			Write(Star("ClassId"));
			WriteLiteral("</label>\r\n                <select id=\"fieldClass\" name=\"row.ClassId\" class=\"form-select\"");
			BeginWriteAttribute("required", "\r\n                        required=\"", 26875, "\"", 26926, 1);
			WriteAttributeValue("", 26911, Req("ClassId"), 26911, 15, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 26927, "\"", 26979, 1);
			WriteAttributeValue("", 26943, Req("ClassId") ? "true" : "false", 26943, 36, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n                  ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f133742", async delegate
			{
				WriteLiteral("---");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_8.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (SchoolClass item in classes)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "16b32be9ac3ee292380475f2c242fa127ce22ae6ea571710c5225e78d3b7f17f135225", async delegate
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
				await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
				if (!__tagHelperExecutionContext.Output.IsContentModified)
				{
					await __tagHelperExecutionContext.SetOutputContentAsync();
				}
				Write(__tagHelperExecutionContext.Output);
				__tagHelperExecutionContext = __tagHelperScopeManager.End();
				WriteLiteral(" ");
			}
			WriteLiteral("                </select>\r\n              </div>\r\n              <div class=\"col-12\">\r\n                <label class=\"form-label\" for=\"fieldNotes\">הערות");
			Write(Star("Notes"));
			WriteLiteral("</label>\r\n                <textarea id=\"fieldNotes\" name=\"row.Notes\" class=\"form-control\" rows=\"2\"");
			BeginWriteAttribute("required", "\r\n                          required=\"", 27401, "\"", 27452, 1);
			WriteAttributeValue("", 27439, Req("Notes"), 27439, 13, isLiteral: false);
			EndWriteAttribute();
			BeginWriteAttribute("aria-required", " aria-required=\"", 27453, "\"", 27503, 1);
			WriteAttributeValue("", 27469, Req("Notes") ? "true" : "false", 27469, 34, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral("></textarea>\r\n              </div>\r\n            </div>\r\n            <div id=\"rowErrors\" class=\"alert alert-danger mt-2\" role=\"alert\"\r\n                 aria-live=\"assertive\" aria-atomic=\"true\" style=\"display:none\"></div>\r\n          ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
		BeginWriteTagHelperAttribute();
		__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
		__tagHelperExecutionContext.AddHtmlAttribute("novalidate", Html.Raw(__tagHelperStringValueBuffer), HtmlAttributeValueStyle.Minimized);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n        </div>\r\n        <div class=\"modal-footer\">\r\n          <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n          <button type=\"button\" class=\"btn btn-primary\" onclick=\"saveRow()\">שמור שורה</button>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n");
		WriteLiteral("  <script>\r\n    let rowModal = null;\r\n    const inlineLookupData = ");
		Write(Html.Raw(JsonSerializer.Serialize(inlineLookupData)));
		WriteLiteral(";\r\n    const inlineDurationOptions = ");
		Write(Html.Raw(JsonSerializer.Serialize(durationOptions)));
		WriteLiteral(";\r\n    const inlineDurationUnlimited = ");
		Write(Json.Serialize(durationUnlimited));
		WriteLiteral(";\r\n    const inlineReportId = ");
		Write(report?.Id);
		WriteLiteral(";\r\n    const inlineAllocationId = ");
		Write(allocationId);
		WriteLiteral(";\r\n\r\n    function htmlEscape(value) {\r\n      return String(value ?? '')\r\n        .replace(/&/g, '&amp;')\r\n        .replace(/</g, '&lt;')\r\n        .replace(/>/g, '&gt;')\r\n        .replace(/\"/g, '&quot;')\r\n        .replace(/'/g, '&#039;');\r\n    }\r\n\r\n    function selectHtml(name, value, optional) {\r\n      const items = inlineLookupData[name] || [];\r\n      const emptyText = optional ? '---' : 'בחר...';\r\n      const options = [`<option value=\"\">${emptyText}</option>`].concat(items.map(item => {\r\n        const selected = String(item.id) === String(value ?? '') ? ' selected' : '';\r\n        return `<option value=\"${htmlEscape(item.id)}\"${selected}>${htmlEscape(item.text)}</option>`;\r\n      }));\r\n      return `<select class=\"form-select form-select-sm inline-field\" data-name=\"row.${name}\">${options.join('')}</select>`;\r\n    }\r\n\r\n    function durationHtml(value) {\n      if (inlineDurationOptions.length && !inlineDurationUnlimited) {\n        const options = ['<option value=\"\">בחר...</option>'].concat(inlineDurationOptio");
		WriteLiteral("ns.map(item => {\n          const selected = String(item) === String(value ?? '') ? ' selected' : '';\n          return `<option value=\"${htmlEscape(item)}\"${selected}>${htmlEscape(item)}</option>`;\n        }));\r\n        return `<select class=\"form-select form-select-sm inline-field\" data-name=\"row.MeetingDuration\">${options.join('')}</select>`;\r\n      }\r\n\n      return `<input type=\"number\" step=\"0.5\" min=\"0.5\" class=\"form-control form-control-sm inline-field\" data-name=\"row.MeetingDuration\" value=\"${htmlEscape(value ?? '')}\" />`;\n    }\n\n    function getLookupText(field, value) {\n      if (!value) return '';\n      const item = (inlineLookupData[field] || []).find(x => String(x.id) === String(value));\n      return item ? item.text : '';\n    }\n\n    function formatCellValue(field, value) {\n      if (field === 'MeetingDate') {\n        if (!value) return '';\n        const parts = String(value).split('-');\n        return parts.length === 3 ? `${parts[2]}/${parts[1]}/${parts[0]}` : value;\n      }\n\n      if (field === ");
		WriteLiteral("'MeetingDuration' || field === 'Notes') {\n        return value || '';\n      }\n\n      return getLookupText(field, value);\n    }\n\n    function cellEditorHtml(field, type, optional, value) {\n      if (type === 'date') {\n        return `<input type=\"date\" class=\"form-control form-control-sm cell-field\" value=\"${htmlEscape(value || '')}\" />`;\n      }\n\n      if (type === 'duration') {\n        if (inlineDurationOptions.length && !inlineDurationUnlimited) {\n          const options = ['<option value=\"\">בחר...</option>'].concat(inlineDurationOptions.map(item => {\n            const selected = String(item) === String(value ?? '') ? ' selected' : '';\n            return `<option value=\"${htmlEscape(item)}\"${selected}>${htmlEscape(item)}</option>`;\n          }));\n          return `<select class=\"form-select form-select-sm cell-field\">${options.join('')}</select>`;\n        }\n\n        return `<input type=\"number\" step=\"0.5\" min=\"0.5\" class=\"form-control form-control-sm cell-field\" value=\"${htmlEscape(value ?? '')}\" />`;\n     ");
		WriteLiteral(" }\n\n      if (type === 'textarea') {\n        return `<textarea class=\"form-control form-control-sm cell-field\" rows=\"2\">${htmlEscape(value || '')}</textarea>`;\n      }\n\n      const items = inlineLookupData[field] || [];\n      const emptyText = optional ? '---' : 'בחר...';\n      const options = [`<option value=\"\">${emptyText}</option>`].concat(items.map(item => {\n        const selected = String(item.id) === String(value ?? '') ? ' selected' : '';\n        return `<option value=\"${htmlEscape(item.id)}\"${selected}>${htmlEscape(item.text)}</option>`;\n      }));\n      const autocompleteAttr = (field === 'Subject1Id' || field === 'Subject2Id') ? ' data-subject-autocomplete=\"1\"' : '';\n      return `<select class=\"form-select form-select-sm cell-field\"${autocompleteAttr}>${options.join('')}</select>`;\n    }\n\n    function appendRowFormData(data, row) {\n      data.append('row.Id', row.id);\n      data.append('row.MeetingDate', row.meetingDate || '');\n      data.append('row.MeetingDuration', row.meetingDuration || '');\n      data.append('row.DistrictId', row.districtId || '');\n      data.append('row.LocalityId', row.localityId || '');\n      data.append('row.Fra");
		WriteLiteral("meworkId', row.frameworkId || '');\n      data.append('row.EducationalProgramId', row.educationalProgramId || '');\n      data.append('row.DomainId', row.domainId || '');\n      data.append('row.Subject1Id', row.subject1Id || '');\n      data.append('row.Subject2Id', row.subject2Id || '');\n      data.append('row.DiscussionCodeId', row.discussionCodeId || '');\n      data.append('row.ConclusionClassId', row.conclusionClassId || '');\n      data.append('row.ConclusionFrameworkId', row.conclusionFrameworkId || '');\n      data.append('row.ConclusionLocationId', row.conclusionLocationId || '');\n      data.append('row.GradeLevelId', row.gradeLevelId || '');\n      data.append('row.ClassId', row.classId || '');\n      data.append('row.Notes', row.notes || '');\n    }\n\n    function setRowField(row, field, value) {\n      const map = {\n        MeetingDate: 'meetingDate',\n        MeetingDuration: 'meetingDuration',\n        DistrictId: 'districtId',\n        LocalityId: 'localityId',\n        FrameworkId: 'frameworkId',\n        Edu");
		WriteLiteral("cationalProgramId: 'educationalProgramId',\n        DomainId: 'domainId',\n        Subject1Id: 'subject1Id',\n        Subject2Id: 'subject2Id',\n        DiscussionCodeId: 'discussionCodeId',\n        ConclusionClassId: 'conclusionClassId',\n        ConclusionFrameworkId: 'conclusionFrameworkId',\n        ConclusionLocationId: 'conclusionLocationId',\n        GradeLevelId: 'gradeLevelId',\n        ClassId: 'classId',\n        Notes: 'notes'\n      };\n      row[map[field]] = value;\n    }\n\n    async function editCell(cell) {\n      if (!cell?.classList.contains('editable-cell') || cell.classList.contains('cell-editing')) return;\n      const tr = cell.closest('tr[data-row-id]');\n      if (!tr || tr.dataset.inlineEditing === 'true') return;\n\n      document.querySelectorAll('#reportTable td.cell-editing').forEach(cancelCellEdit);\n      const field = cell.dataset.editField;\n      const type = cell.dataset.editType || 'select';\n      const optional = cell.dataset.editOptional === 'true';\n      const rowId = tr.dataset.rowId;\n   ");
		WriteLiteral("   const resp = await fetch('/Report/GetRow?rowId=' + rowId);\n      if (!resp.ok) {\n        alert('לא ניתן לטעון את התא לעריכה');\n        return;\n      }\n\n      const row = await resp.json();\n      const key = {\n        MeetingDate: 'meetingDate',\n        MeetingDuration: 'meetingDuration',\n        DistrictId: 'districtId',\n        LocalityId: 'localityId',\n        FrameworkId: 'frameworkId',\n        EducationalProgramId: 'educationalProgramId',\n        DomainId: 'domainId',\n        Subject1Id: 'subject1Id',\n        Subject2Id: 'subject2Id',\n        DiscussionCodeId: 'discussionCodeId',\n        ConclusionClassId: 'conclusionClassId',\n        ConclusionFrameworkId: 'conclusionFrameworkId',\n        ConclusionLocationId: 'conclusionLocationId',\n        GradeLevelId: 'gradeLevelId',\n        ClassId: 'classId',\n        Notes: 'notes'\n      }[field];\n\n      cell.dataset.originalHtml = cell.innerHTML;\n      cell.classList.add('cell-editing');\n      cell.innerHTML = `\n        ${cellEditorHtml(field, type, optional, r");
		WriteLiteral("ow[key])}\n        <div class=\"cell-edit-actions\">\n          <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"saveCellEdit(this)\">שמור</button>\n          <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"cancelCellEdit(this.closest('td'))\">ביטול</button>\n        </div>\n        <div class=\"cell-edit-error\" role=\"alert\"></div>`;\n      cell.querySelector('.cell-field')?.focus();\n    }\n\n    function cancelCellEdit(cell) {\n      if (!cell?.dataset.originalHtml) return;\n      cell.innerHTML = cell.dataset.originalHtml;\n      cell.classList.remove('cell-editing');\n      delete cell.dataset.originalHtml;\n    }\n\n    async function saveCellEdit(button) {\n      const cell = button.closest('td');\n      const tr = cell.closest('tr[data-row-id]');\n      const field = cell.dataset.editField;\n      const input = cell.querySelector('.cell-field');\n      const rowId = tr.dataset.rowId;\n      const resp = await fetch('/Report/GetRow?rowId=' + rowId);\n      if (!resp.ok) {\n        cell.querySele");
		WriteLiteral("ctor('.cell-edit-error').textContent = 'לא ניתן לטעון את השורה לשמירה';\n        return;\n      }\n\n      const row = await resp.json();\n      const value = input.value || '';\n      setRowField(row, field, value);\n\n      const token = document.querySelector('[name=__RequestVerificationToken]').value;\n      const data = new FormData();\n      data.append('__RequestVerificationToken', token);\n      data.append('reportId', inlineReportId);\n      data.append('allocationId', inlineAllocationId);\n      data.append('rowVersion', tr.getAttribute('data-row-version') || row.rowVersion || '');\n      appendRowFormData(data, row);\n\n      button.disabled = true;\n      const saveResp = await fetch('/Report/SaveRow', { method: 'POST', body: data });\n      const result = await saveResp.json().catch(() => ({ success: false, error: 'שגיאה בשמירת התא' }));\n      if (!result.success) {\n        button.disabled = false;\n        cell.querySelector('.cell-edit-error').innerHTML = (result.errors || [result.error || 'שגיאה בשמירת התא']).jo");
		WriteLiteral("in('<br/>');\n        return;\n      }\n\n      if (result.rowVersion) {\n        tr.setAttribute('data-row-version', result.rowVersion);\n      }\n      cell.classList.remove('cell-editing');\n      delete cell.dataset.originalHtml;\n      cell.textContent = formatCellValue(field, value) || '---';\n    }\n\n    document.querySelectorAll('#reportTable td.editable-cell').forEach(cell => {\n      cell.addEventListener('dblclick', () => editCell(cell));\n      cell.addEventListener('click', () => editCell(cell));\n    });\n\n    async function inlineEditRow(id) {\n      const tr = document.querySelector(`tr[data-row-id=\"${id}\"]`);\n      if (!tr || tr.dataset.inlineEditing === 'true') return;\n\r\n      const resp = await fetch('/Report/GetRow?rowId=' + id);\r\n      if (!resp.ok) {\r\n        alert('לא ניתן לטעון את השורה לעריכה');\r\n        return;\r\n      }\r\n\n      const row = await resp.json();\n      tr.dataset.inlineEditing = 'true';\n      document.querySelector(`tr[data-detail-for=\"${id}\"]`)?.remove();\n\n      const detail = document.");
		WriteLiteral("createElement('tr');\n      detail.className = 'table-warning-subtle';\n      detail.dataset.detailFor = id;\n      detail.innerHTML = `\n        <td colspan=\"${tr.children.length}\">\n          <div class=\"row g-2 align-items-end\">\n            <div class=\"col-md-2\"><label class=\"form-label\">תאריך</label><input type=\"date\" class=\"form-control form-control-sm inline-field\" data-name=\"row.MeetingDate\" value=\"${htmlEscape(row.meetingDate)}\" /></div>\n            <div class=\"col-md-2\"><label class=\"form-label\">משך</label>${durationHtml(row.meetingDuration)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">מחוז</label>${selectHtml('DistrictId', row.districtId, false)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">ישוב</label>${selectHtml('LocalityId', row.localityId, false)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">מסגרת</label>${selectHtml('FrameworkId', row.frameworkId, false)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">תוכנית</label>$");
		WriteLiteral("{selectHtml('EducationalProgramId', row.educationalProgramId, false)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">תחום</label>${selectHtml('DomainId', row.domainId, false)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">נושא 1</label>${selectHtml('Subject1Id', row.subject1Id, false)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">נושא 2</label>${selectHtml('Subject2Id', row.subject2Id, true)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">קיום דיון</label>${selectHtml('DiscussionCodeId', row.discussionCodeId, true)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">מסקנה - כיתה</label>${selectHtml('ConclusionClassId', row.conclusionClassId, true)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">מסקנה - מסגרת</label>${selectHtml('ConclusionFrameworkId', row.conclusionFrameworkId, true)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">מסקנה - מיקום</label>${selectHtml('ConclusionLoca");
		WriteLiteral("tionId', row.conclusionLocationId, true)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">שכבה</label>${selectHtml('GradeLevelId', row.gradeLevelId, true)}</div>\n            <div class=\"col-md-2\"><label class=\"form-label\">כיתה</label>${selectHtml('ClassId', row.classId, true)}</div>\n            <div class=\"col-md-6\"><label class=\"form-label\">הערות</label><textarea class=\"form-control form-control-sm inline-field\" data-name=\"row.Notes\" rows=\"2\">${htmlEscape(row.notes || '')}</textarea></div>\n            <div class=\"col-md-4 d-flex gap-2\">\n              <button type=\"button\" class=\"btn btn-sm btn-primary\" onclick=\"saveInlineRow(${id})\">שמור</button>\n              <button type=\"button\" class=\"btn btn-sm btn-outline-secondary\" onclick=\"cancelInlineRow(${id})\">ביטול</button>\n            </div>\n            <div class=\"col-12 inline-row-errors text-danger small fw-bold\" role=\"alert\"></div>\n          </div>\n        </td>`;\n      tr.after(detail);\n    }\n\n    function cancelInlineRow(id) {\n      const");
		WriteLiteral(" tr = document.querySelector(`tr[data-row-id=\"${id}\"]`);\n      document.querySelector(`tr[data-detail-for=\"${id}\"]`)?.remove();\n      if (tr) tr.dataset.inlineEditing = 'false';\n    }\n\n    async function saveInlineRow(id) {\n      const tr = document.querySelector(`tr[data-row-id=\"${id}\"]`);\n      const detail = document.querySelector(`tr[data-detail-for=\"${id}\"]`);\n      if (!tr || !detail) return;\n\r\n      const token = document.querySelector('[name=__RequestVerificationToken]').value;\r\n      const data = new FormData();\r\n      data.append('__RequestVerificationToken', token);\r\n      data.append('reportId', inlineReportId);\r\n      data.append('allocationId', inlineAllocationId);\r\n      data.append('row.Id', id);\r\n      data.append('rowVersion', tr.getAttribute('data-row-version') || '');\r\n      detail.querySelectorAll('.inline-field').forEach(field => {\n        data.append(field.dataset.name, field.value || '');\n      });\n\r\n      const resp = await fetch('/Report/SaveRow', { method: 'POST', body: data });\r\n  ");
		WriteLiteral("    const result = await resp.json();\r\n      if (result.success) {\r\n        location.reload();\r\n        return;\r\n      }\r\n\r\n      const errorBox = detail.querySelector('.inline-row-errors');\n      if (errorBox) {\r\n        errorBox.innerHTML = (result.errors || [result.error || 'שגיאה בשמירת השורה']).join('<br/>');\r\n      } else {\r\n        alert(result.error || 'שגיאה בשמירת השורה');\r\n      }\r\n    }\r\n\r\n    document.querySelectorAll('#reportTable th.sortable').forEach((header, index) => {\r\n      header.style.cursor = 'pointer';\r\n      header.setAttribute('tabindex', '0');\r\n      header.setAttribute('title', 'לחץ למיון לפי עמודה זו');\r\n      header.addEventListener('click', () => sortReportTable(index, header));\r\n      header.addEventListener('keydown', e => {\r\n        if (e.key === 'Enter' || e.key === ' ') {\r\n          e.preventDefault();\r\n          sortReportTable(index, header);\r\n        }\r\n      });\r\n    });\r\n\r\n    function sortReportTable(index, header) {\r\n      const tbody = document.querySelector('#repor");
		WriteLiteral("tTable tbody');\r\n      document.querySelectorAll('tr[data-detail-for]').forEach(row => row.remove());\n      document.querySelectorAll('tr[data-row-id]').forEach(row => row.dataset.inlineEditing = 'false');\n      const rows = Array.from(tbody.querySelectorAll('tr[data-row-id]'));\n      const descending = header.dataset.sortDir !== 'desc';\r\n      document.querySelectorAll('#reportTable th.sortable').forEach(th => {\r\n        th.dataset.sortDir = '';\r\n        th.setAttribute('aria-sort', 'none');\r\n      });\r\n      header.dataset.sortDir = descending ? 'desc' : 'asc';\r\n      header.setAttribute('aria-sort', descending ? 'descending' : 'ascending');\r\n\r\n      rows.sort((a, b) => {\r\n        const left = a.children[index]?.innerText.trim() || '';\r\n        const right = b.children[index]?.innerText.trim() || '';\r\n        const leftNum = Number(left.replace(',', '.'));\r\n        const rightNum = Number(right.replace(',', '.'));\r\n        const compare = Number.isNaN(leftNum) || Number.isNaN(rightNum)\r\n          ? left.loc");
		WriteLiteral("aleCompare(right, 'he')\r\n          : leftNum - rightNum;\r\n        return descending ? -compare : compare;\r\n      });\r\n      rows.forEach(row => tbody.appendChild(row));\r\n    }\r\n\r\n    function setSelect(id, value) {\r\n      const select = document.getElementById(id);\r\n      if (!select) return;\r\n      select.value = value || '';\r\n      if (window.refreshSubjectAutocomplete) window.refreshSubjectAutocomplete(select);\r\n    }\r\n\r\n    function addRow() {\r\n      document.getElementById('rowForm').reset();\r\n      document.getElementById('rowId').value = '0';\r\n      document.getElementById('rowRowVersion').value = '';\r\n      document.getElementById('rowErrors').style.display = 'none';\r\n      rowModal = rowModal || new bootstrap.Modal(document.getElementById('rowModal'));\r\n      rowModal.show();\r\n    }\r\n\r\n    async function editRow(id) {\r\n      document.getElementById('rowErrors').style.display = 'none';\r\n      const resp = await fetch('/Report/GetRow?rowId=' + id);\r\n      if (!resp.ok) {\r\n        alert('לא ניתן לטעון את השורה לעריכה');\r\n        return;\r\n      }\r\n\r\n      const row = await resp.json();\r\n      document.getElementById('rowId').value = row.id;\r\n      ");
		WriteLiteral("document.getElementById('rowRowVersion').value = row.rowVersion || '';\r\n      document.getElementById('fieldDate').value = row.meetingDate;\r\n      document.getElementById('fieldDuration').value = row.meetingDuration;\r\n      setSelect('fieldDistrict', row.districtId);\r\n      setSelect('fieldLocality', row.localityId);\r\n      setSelect('fieldFramework', row.frameworkId);\r\n      setSelect('fieldEduProgram', row.educationalProgramId);\r\n      setSelect('fieldDomain', row.domainId);\r\n      setSelect('fieldSubject1', row.subject1Id);\r\n      setSelect('fieldSubject2', row.subject2Id);\r\n      setSelect('fieldDiscussion', row.discussionCodeId);\r\n      setSelect('fieldConclusionClass', row.conclusionClassId);\r\n      setSelect('fieldConclusionFramework', row.conclusionFrameworkId);\r\n      setSelect('fieldConclusionLocation', row.conclusionLocationId);\r\n      setSelect('fieldGradeLevel', row.gradeLevelId);\r\n      setSelect('fieldClass', row.classId);\r\n      document.getElementById('fieldNotes').value = row.notes || '';\r\n ");
		WriteLiteral("     rowModal = rowModal || new bootstrap.Modal(document.getElementById('rowModal'));\r\n      rowModal.show();\r\n    }\r\n\r\n    async function duplicateRow(id) {\r\n      document.getElementById('rowErrors').style.display = 'none';\r\n      const resp = await fetch('/Report/GetRow?rowId=' + id);\r\n      if (!resp.ok) {\r\n        alert('לא ניתן לטעון את השורה לשכפול');\r\n        return;\r\n      }\r\n\r\n      const row = await resp.json();\r\n      document.getElementById('rowId').value = '0';\r\n      document.getElementById('rowRowVersion').value = '';\r\n      document.getElementById('fieldDate').value = row.meetingDate;\r\n      document.getElementById('fieldDuration').value = row.meetingDuration;\r\n      setSelect('fieldDistrict', row.districtId);\r\n      setSelect('fieldLocality', row.localityId);\r\n      setSelect('fieldFramework', row.frameworkId);\r\n      setSelect('fieldEduProgram', row.educationalProgramId);\r\n      setSelect('fieldDomain', row.domainId);\r\n      setSelect('fieldSubject1', row.subject1Id);\r\n      setSelect('fiel");
		WriteLiteral("dSubject2', row.subject2Id);\r\n      setSelect('fieldDiscussion', row.discussionCodeId);\r\n      setSelect('fieldConclusionClass', row.conclusionClassId);\r\n      setSelect('fieldConclusionFramework', row.conclusionFrameworkId);\r\n      setSelect('fieldConclusionLocation', row.conclusionLocationId);\r\n      setSelect('fieldGradeLevel', row.gradeLevelId);\r\n      setSelect('fieldClass', row.classId);\r\n      document.getElementById('fieldNotes').value = row.notes || '';\r\n      rowModal = rowModal || new bootstrap.Modal(document.getElementById('rowModal'));\r\n      rowModal.show();\r\n    }\r\n\r\n    async function saveRow() {\r\n      const form = document.getElementById('rowForm');\r\n      const data = new FormData(form);\r\n      const errDiv = document.getElementById('rowErrors');\r\n      errDiv.style.display = 'none';\r\n\r\n      const resp = await fetch('/Report/SaveRow', { method: 'POST', body: data });\r\n      const result = await resp.json();\r\n      if (result.success) {\r\n        location.reload();\r\n      } else {\r\n        e");
		WriteLiteral("rrDiv.innerHTML = (result.errors || [result.error]).join('<br/>');\r\n        errDiv.style.display = 'block';\r\n      }\r\n    }\r\n\r\n    async function deleteRow(rowId) {\r\n      if (!confirm('האם למחוק שורה זו?')) return;\r\n      const token = document.querySelector('[name=__RequestVerificationToken]').value;\r\n      const tr = document.querySelector('tr[data-row-id=\"' + rowId + '\"]');\r\n      const rv = tr ? (tr.getAttribute('data-row-version') || '') : '';\r\n      const data = new FormData();\r\n      data.append('rowId', rowId);\r\n      data.append('rowVersion', rv);\r\n      data.append('__RequestVerificationToken', token);\r\n      const resp = await fetch('/Report/DeleteRow', { method: 'POST', body: data });\r\n      const result = await resp.json().catch(() => ({ success: false }));\r\n      if (result.success) {\r\n        location.reload();\r\n      } else {\r\n        alert(result.error || 'שגיאה במחיקת שורה');\r\n      }\r\n    }\r\n\r\n    async function uploadAttachment() {\n      const input = document.getElementById('report-file-");
		WriteLiteral("upload');\n      if (!input.files.length) return;\n      const token = document.querySelector('[name=__RequestVerificationToken]').value;\n      const data = new FormData();\n      data.append('reportId', inlineReportId);\n      data.append('file', input.files[0]);\n      const descriptionInput = document.getElementById('report-file-description');\n      data.append('description', descriptionInput ? descriptionInput.value : '');\n      data.append('__RequestVerificationToken', token);\n      const resp = await fetch('/Report/UploadAttachment', { method: 'POST', body: data });\r\n      const result = await resp.json();\r\n      if (result.success) location.reload();\r\n      else alert(result.error || 'שגיאה בהעלאת מסמך');\r\n    }\r\n\r\n    async function deleteAttachment(attachmentId) {\n      if (!confirm('האם למחוק מסמך זה?')) return;\r\n      const token = document.querySelector('[name=__RequestVerificationToken]').value;\r\n      const data = new FormData();\r\n      data.append('attachmentId', attachmentId);\r\n      data.append('_");
		WriteLiteral("_RequestVerificationToken', token);\r\n      const resp = await fetch('/Report/DeleteAttachment', { method: 'POST', body: data });\r\n      const result = await resp.json();\r\n      if (result.success) location.reload();\n      else alert(result.error || 'שגיאה במחיקת מסמך');\n    }\n\n    document.addEventListener('DOMContentLoaded', function () {\n      const editRowId = ");
		Write(editRowId.HasValue ? editRowId.Value.ToString() : "0");
		WriteLiteral(";\n      if (editRowId > 0) {\n        editRow(editRowId);\n        const url = new URL(window.location.href);\n        url.searchParams.delete('editRowId');\n        window.history.replaceState({}, '', url);\n      }\n    });\n  </script>\n");
		string FieldAttrs(string field, string type = "select", bool optional = false)
		{
			if (canEdit)
			{
				return $" class=\"editable-cell\" data-edit-field=\"{field}\" data-edit-type=\"{type}\" data-edit-optional=\"{optional.ToString().ToLowerInvariant()}\" title=\"לחץ לעריכת התא\"";
			}
			return string.Empty;
		}
		bool Req(string field)
		{
			return requiredFields.Contains(field);
		}
		string Star(string field)
		{
			if (!Req(field))
			{
				return "";
			}
			return " *";
		}
		static string StatusText(int? statusId, string? name)
		{
			return statusId switch
			{
				1 => "טיוטה", 
				2 => "בהזנה", 
				3 => "ממתין לאישור", 
				4 => "מאושר", 
				5 => "הוחזר לתיקון", 
				_ => name ?? "", 
			};
		}
	}
}
