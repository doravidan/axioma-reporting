using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

[RazorCompiledItemMetadata("Identifier", "/Views/MyAllocations/Index.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_MyAllocations_Index : RazorPage<MyAllocationsViewModel>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("class", new HtmlString("myalloc-tile"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("asp-controller", "Report", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("aria-label", new HtmlString("עדכון פעילות חודשית"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("aria-label", new HtmlString("העלאת אקסל חודשי"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("asp-action", "ExportExcel", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("class", new HtmlString("btn btn-success"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("asp-action", "Details", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("class", new HtmlString("btn btn-sm btn-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("aria-label", new HtmlString("צפייה בפרטי הקצאה"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<MyAllocationsViewModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "פעילות חודשית";
		string[] array = new string[13]
		{
			"", "ינואר", "פברואר", "מרץ", "אפריל", "מאי", "יוני", "יולי", "אוגוסט", "ספטמבר",
			"אוקטובר", "נובמבר", "דצמבר"
		};
		string value2 = ((base.Model.ActiveMonth == null) ? "אין חודש דיווח פעיל" : ((!string.IsNullOrWhiteSpace(base.Model.ActiveMonth.Description)) ? base.Model.ActiveMonth.Description : $"{array[base.Model.ActiveMonth.Month]} {base.Model.ActiveMonth.Year}"));
		WriteLiteral("\r\n");
		if (base.Model.ActiveMonth != null)
		{
			WriteLiteral("  <section class=\"myalloc-banner\" role=\"region\" aria-label=\"חודש דיווח פעיל\">\r\n    <h2>");
			Write(value2);
			WriteLiteral("</h2>\r\n    <div class=\"myalloc-deadline\">\r\n      מועד אחרון לדיווח:\r\n      <strong>");
			Write(base.Model.ActiveMonth.LastReportingDate.ToString("dd/MM/yyyy"));
			WriteLiteral("</strong>\r\n    </div>\r\n  </section>\r\n");
		}
		else
		{
			WriteLiteral("  <div class=\"alert alert-warning\" role=\"alert\" aria-live=\"polite\">\r\n    אין כרגע חודש דיווח פעיל במערכת. אנא פנה למנהל הפרויקט.\r\n  </div>\r\n");
		}
		WriteLiteral("\r\n<div class=\"row g-4\">\r\n  <div class=\"col-12 col-md-6\">\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "3a0861d81a3197cb4e941ec9ae664cbe8176bfafb17f8b51903ffeaae75023419295", async delegate
		{
			WriteLiteral("\r\n      <span class=\"myalloc-tile-icon\" aria-hidden=\"true\">\ud83d\udccb</span>\r\n      <span>עדכון פעילות חודשית</span>\r\n    ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
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
		WriteLiteral("\r\n  </div>\r\n\r\n");
		WriteLiteral("  <div class=\"col-12 col-md-6\">\r\n    <a class=\"myalloc-tile\" href=\"/Report/History\" aria-label=\"היסטוריית דיווחים\">\r\n      <span class=\"myalloc-tile-icon\" aria-hidden=\"true\">&#128197;</span>\r\n      <span>היסטוריית דיווחים</span>\r\n    </a>\r\n  </div>\r\n\r\n");
		if (base.Model.AllowExcelUpload)
		{
			WriteLiteral("    <div class=\"col-12 col-md-6\">\r\n      ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "3a0861d81a3197cb4e941ec9ae664cbe8176bfafb17f8b51903ffeaae750234111261", async delegate
			{
				WriteLiteral("\r\n        <span class=\"myalloc-tile-icon\" aria-hidden=\"true\">\ud83d\udce4</span>\r\n        <span>העלאת אקסל חודשי</span>\r\n      ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-allocationId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(base.Model.ExcelUploadAllocationId);
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"] = __tagHelperStringValueBuffer;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-allocationId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["allocationId"], HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n    </div>\r\n");
		}
		WriteLiteral("</div>\r\n\r\n");
		if (base.Model.AllocationCount == 0)
		{
			WriteLiteral("  <div class=\"alert alert-info mt-4\" role=\"alert\" aria-live=\"polite\">\r\n    לא הוגדרו עבורך הקצאות פעילות. אנא פנה למרכז הפרויקט.\r\n  </div>\r\n");
			return;
		}
		WriteLiteral("  <section class=\"mt-4\" aria-labelledby=\"my-allocations-heading\">\r\n    <div class=\"d-flex justify-content-between align-items-center flex-wrap gap-2 mb-2\">\r\n      <h3 id=\"my-allocations-heading\" class=\"mb-0\">ההקצאות שלי</h3>\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "3a0861d81a3197cb4e941ec9ae664cbe8176bfafb17f8b51903ffeaae750234115076", async delegate
		{
			WriteLiteral("יצא לאקסל");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n\r\n    <div class=\"table-responsive\">\r\n      <table class=\"table table-striped table-hover align-middle\">\r\n        <thead class=\"table-dark\">\r\n          <tr>\r\n            <th scope=\"col\"><span class=\"visually-hidden\">פעולות</span></th>\r\n            <th scope=\"col\">פרויקט</th>\r\n            <th scope=\"col\">תוכנית</th>\r\n            <th scope=\"col\">מחוז</th>\r\n            <th scope=\"col\">מגזר</th>\r\n            <th scope=\"col\">היקף פעילות חודשי</th>\r\n            <th scope=\"col\">היקף יומי</th>\r\n            <th scope=\"col\">היקף פעילות שנתי</th>\r\n            <th scope=\"col\">משך תפוקה</th>\r\n            <th scope=\"col\">העלאת אקסל</th>\r\n            <th scope=\"col\">הערות</th>\r\n          </tr>\r\n        </thead>\r\n        <tbody>\r\n");
		foreach (Allocation allocation in base.Model.Allocations)
		{
			WriteLiteral("            <tr>\r\n              <td>\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "3a0861d81a3197cb4e941ec9ae664cbe8176bfafb17f8b51903ffeaae750234117428", async delegate
			{
				WriteLiteral("\r\n                  צפה\r\n                ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(allocation.Id);
			__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
			__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n              </td>\r\n              <td>");
			Write(allocation.Project?.Description);
			WriteLiteral("</td>\r\n              <td>");
			Write(JoinValues(allocation.AllocationPrograms.Select((AllocationProgram x) => x.Program?.Description)));
			WriteLiteral("</td>\r\n              <td>");
			Write(JoinValues(allocation.AllocationDistricts.Select((AllocationDistrict x) => x.District?.Description)));
			WriteLiteral("</td>\r\n              <td>");
			Write(JoinValues(allocation.AllocationSectors.Select((AllocationSector x) => x.Sector?.Description)));
			WriteLiteral("</td>\r\n              <td>");
			Write(Whole(allocation.MonthlyEmploymentScope));
			WriteLiteral("</td>\r\n              <td>");
			Write(Daily(allocation.DailyEmploymentScope));
			WriteLiteral("</td>\r\n              <td>");
			Write(Whole(allocation.AnnualEmploymentScope));
			WriteLiteral("</td>\r\n              <td>");
			Write(string.IsNullOrWhiteSpace(allocation.OutputDuration) ? "-" : allocation.OutputDuration);
			WriteLiteral("</td>\r\n              <td>");
			Write(allocation.AllowExcelUpload ? "כן" : "לא");
			WriteLiteral("</td>\r\n              <td>");
			Write(string.IsNullOrWhiteSpace(allocation.Notes) ? "-" : allocation.Notes);
			WriteLiteral("</td>\r\n            </tr>\r\n");
		}
		WriteLiteral("        </tbody>\r\n      </table>\r\n    </div>\r\n  </section>\r\n");
		static string Daily(decimal? value)
		{
			if (!value.HasValue)
			{
				return "ללא הגבלה";
			}
			return value.Value.ToString("0.##");
		}
		static string JoinValues(IEnumerable<string?> values)
		{
			List<string> list = values.Where((string v) => !string.IsNullOrWhiteSpace(v)).Distinct().ToList();
			if (list.Count != 0)
			{
				return string.Join(", ", list);
			}
			return "-";
		}
		static string Whole(decimal? value)
		{
			if (!value.HasValue)
			{
				return "-";
			}
			return decimal.Truncate(value.Value).ToString("0");
		}
	}
}
