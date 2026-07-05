using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Infrastructure.Services;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/BatchReportImportResult.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_BatchReportImportResult : RazorPage<BatchReportImportResultViewModel>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "BatchReportImport", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-action", "BatchReportImportErrorsExcel", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-danger btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

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
	public IHtmlHelper<BatchReportImportResultViewModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "תוצאות ייבוא דיווחים מרוכז";
		BatchImportResult r = base.Model.Result;
		WriteLiteral("\r\n<div class=\"container mt-3\" dir=\"rtl\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>תוצאות ייבוא דיווחים מרוכז</h3>\r\n    <div class=\"d-flex gap-2\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "6d7fd898a4799e6eef5ddd49b9e9bc9d1d74404fb0d99bc954423f839c12999b5092", async delegate
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
		WriteLiteral("\r\n");
		if (r.Errors.Any())
		{
			WriteLiteral("        ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "6d7fd898a4799e6eef5ddd49b9e9bc9d1d74404fb0d99bc954423f839c12999b6602", async delegate
			{
				WriteLiteral("הורד רשימת שגיאות (Excel)");
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
			WriteLiteral("\r\n");
		}
		WriteLiteral("    </div>\r\n  </div>\r\n\r\n  <div class=\"card mb-3\">\r\n    <div class=\"card-body\">\r\n      <div class=\"row g-3\">\r\n        <div class=\"col-md-3\">\r\n          <div class=\"text-muted small\">חודש דיווח</div>\r\n          <div class=\"fs-5\">");
		Write(base.Model.MonthDescription);
		WriteLiteral(" (");
		Write(base.Model.MonthNumber);
		WriteLiteral("/");
		Write(base.Model.Year);
		WriteLiteral(")</div>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <div class=\"text-muted small\">סך כל השורות שנקראו</div>\r\n          <div class=\"fs-5\">");
		Write(r.TotalRowsRead);
		WriteLiteral("</div>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <div class=\"text-muted small\">שורות שנקלטו</div>\r\n          <div class=\"fs-5 text-success\">");
		Write(r.RowsImported);
		WriteLiteral("</div>\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n          <div class=\"text-muted small\">שורות שגויות</div>\r\n          <div class=\"fs-5 text-danger\">");
		Write(r.ErrorRowsCount);
		WriteLiteral("</div>\r\n        </div>\r\n        <div class=\"col-md-3\">\r\n          <div class=\"text-muted small\">מספר עובדים בדיווח</div>\r\n          <div class=\"fs-5\">");
		Write(r.EmployeesAffected);
		WriteLiteral("</div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n\r\n");
		if (r.RowResults.Any())
		{
			WriteLiteral("    <div class=\"card mb-3\">\r\n      <div class=\"card-header\"><strong>פירוט שורות מהקובץ</strong></div>\r\n      <div class=\"card-body p-0\">\r\n        <table class=\"table table-sm table-bordered mb-0\">\r\n          <thead class=\"table-light\">\r\n            <tr>\r\n              <th scope=\"col\">מס' שורה בקובץ</th>\r\n              <th scope=\"col\">קוד עובד</th>\r\n              <th scope=\"col\">שם המדווח</th>\r\n              <th scope=\"col\">תיאור</th>\r\n            </tr>\r\n          </thead>\r\n          <tbody>\r\n");
			foreach (BatchImportRowResult item in r.RowResults.OrderBy((BatchImportRowResult x) => x.FileRowNumber))
			{
				string value = item.Outcome switch
				{
					BatchImportRowOutcome.Added => "table-success", 
					BatchImportRowOutcome.Updated => "table-info", 
					BatchImportRowOutcome.Skipped => "table-warning", 
					BatchImportRowOutcome.Rejected => "table-danger", 
					_ => string.Empty, 
				};
				WriteLiteral("              <tr");
				BeginWriteAttribute("class", " class=\"", 2802, "\"", 2819, 1);
				WriteAttributeValue("", 2810, value, 2810, 9, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">\r\n                <td>");
				Write(item.FileRowNumber);
				WriteLiteral("</td>\r\n                <td>");
				Write(item.EmployeeCode);
				WriteLiteral("</td>\r\n                <td>");
				Write(item.ReporterName);
				WriteLiteral("</td>\r\n                <td>");
				Write(item.ResultDescription);
				WriteLiteral("</td>\r\n              </tr>\r\n");
			}
			WriteLiteral("          </tbody>\r\n        </table>\r\n      </div>\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n");
		if (r.EmployeeSummaries.Any())
		{
			WriteLiteral("    <div class=\"card mb-3\">\r\n      <div class=\"card-header\"><strong>פירוט לפי עובד</strong></div>\r\n      <div class=\"card-body p-0\">\r\n        <table class=\"table table-sm table-striped mb-0\">\r\n          <thead class=\"table-light\">\r\n            <tr>\r\n              <th scope=\"col\">קוד עובד</th>\r\n              <th scope=\"col\">שם המדווח</th>\r\n              <th scope=\"col\">שורות שנקלטו</th>\r\n              <th scope=\"col\">שורות שנדחו</th>\r\n            </tr>\r\n          </thead>\r\n          <tbody>\r\n");
			foreach (BatchImportEmployeeSummary item2 in r.EmployeeSummaries.OrderBy((BatchImportEmployeeSummary s) => s.EmployeeCode))
			{
				WriteLiteral("              <tr>\r\n                <td>");
				Write(item2.EmployeeCode);
				WriteLiteral("</td>\r\n                <td>");
				Write(item2.ReporterName);
				WriteLiteral("</td>\r\n                <td class=\"text-success\">");
				Write(item2.RowsImported);
				WriteLiteral("</td>\r\n                <td");
				BeginWriteAttribute("class", " class=\"", 3927, "\"", 3977, 1);
				WriteAttributeValue("", 3935, (item2.RowsRejected > 0) ? "text-danger" : "", 3935, 42, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(">");
				Write(item2.RowsRejected);
				WriteLiteral("</td>\r\n              </tr>\r\n");
			}
			WriteLiteral("          </tbody>\r\n        </table>\r\n      </div>\r\n    </div>\r\n");
		}
		WriteLiteral("\r\n");
		if (r.Errors.Any())
		{
			WriteLiteral("    <div class=\"card border-danger\">\r\n      <div class=\"card-header bg-danger text-white\"><strong>רשימת שגיאות</strong></div>\r\n      <div class=\"card-body p-0\">\r\n        <table class=\"table table-sm table-bordered mb-0\">\r\n          <thead class=\"table-light\">\r\n            <tr>\r\n              <th scope=\"col\">מס' שורה בקובץ</th>\r\n              <th scope=\"col\">קוד עובד</th>\r\n              <th scope=\"col\">שם המדווח</th>\r\n              <th scope=\"col\">שגיאה</th>\r\n            </tr>\r\n          </thead>\r\n          <tbody>\r\n");
			foreach (BatchImportError item3 in r.Errors.OrderBy((BatchImportError e) => e.FileRowNumber))
			{
				WriteLiteral("              <tr>\r\n                <td>");
				Write(item3.FileRowNumber);
				WriteLiteral("</td>\r\n                <td>");
				Write(item3.EmployeeCode);
				WriteLiteral("</td>\r\n                <td>");
				Write(item3.ReporterName);
				WriteLiteral("</td>\r\n                <td>");
				Write(item3.ErrorMessage);
				WriteLiteral("</td>\r\n              </tr>\r\n");
			}
			WriteLiteral("          </tbody>\r\n        </table>\r\n      </div>\r\n    </div>\r\n");
		}
		else if (r.RowsImported > 0)
		{
			WriteLiteral("    <div class=\"alert alert-success\" role=\"alert\" aria-live=\"polite\">\r\n      הייבוא הושלם בהצלחה ללא שגיאות. הודעה על קליטת הדיווח נשלחה לכל עובד מדווח.\r\n    </div>\r\n");
		}
		WriteLiteral("</div>\r\n");
	}
}
