using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/ReportingMonths.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_ReportingMonths : RazorPage<List<ReportingMonth>>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-controller", "Lookup", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("asp-action", "EditReportingMonth", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("class", new HtmlString("btn btn-sm btn-outline-primary"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("asp-action", "ActivateReportingMonth", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("style", new HtmlString("display:inline"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("asp-action", "DeactivateReportingMonth", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("asp-action", "CreateReportingMonth", HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<List<ReportingMonth>> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "ניהול חודשי דיווח";
		WriteLiteral("<div class=\"container-fluid mt-3\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>חודשי דיווח — פעילות חודשית</h3>\r\n    <div class=\"d-flex gap-2\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "f4ff0019eb2657f1c538d9d485c1678cf976e908a563918e7d5232dca3d16eb57292", async delegate
		{
			WriteLiteral("חזרה לטבלאות עזר");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n      <button class=\"btn btn-primary btn-sm\" data-bs-toggle=\"modal\" data-bs-target=\"#addModal\">הוסף חודש</button>\r\n    </div>\r\n  </div>\r\n\r\n");
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
		WriteLiteral("\r\n  <div class=\"alert alert-info\">\r\n    <strong>שים לב:</strong> רק חודש אחד יכול להיות פעיל בו-זמנית. הפעלת חודש תשבית אוטומטית את כל שאר החודשים.\r\n  </div>\r\n\r\n  <div class=\"card\">\r\n    <div class=\"card-body p-0\">\r\n      <table class=\"table table-hover table-bordered mb-0\">\r\n        <thead class=\"table-light\">\r\n          <tr>\r\n            <th>תיאור</th>\r\n            <th>חודש</th>\r\n            <th>שנה</th>\r\n            <th>תאריך אחרון לדיווח</th>\r\n            <th>דיווח עתידי</th>\r\n            <th>סטטוס</th>\r\n            <th>פעולות</th>\r\n          </tr>\r\n        </thead>\r\n        <tbody>\r\n");
		if (!base.Model.Any())
		{
			WriteLiteral("            <tr><td colspan=\"7\" class=\"text-center text-muted py-4\">אין חודשי דיווח</td></tr>\r\n");
		}
		foreach (ReportingMonth i in base.Model)
		{
			WriteLiteral("            <tr");
			BeginWriteAttribute("class", " class=\"", 1975, "\"", 2019, 1);
			WriteAttributeValue("", 1983, i.IsActive ? "table-success" : "", 1983, 36, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(">\r\n              <td>");
			Write(i.Description);
			WriteLiteral("</td>\r\n              <td>");
			Write(i.Month);
			WriteLiteral("</td>\r\n              <td>");
			Write(i.Year);
			WriteLiteral("</td>\r\n              <td>");
			Write(i.LastReportingDate.ToString("dd/MM/yyyy"));
			WriteLiteral("</td>\r\n              <td>");
			Write(i.AllowFutureReporting ? "כן" : "לא");
			WriteLiteral("</td>\r\n              <td>\r\n");
			if (i.IsActive)
			{
				WriteLiteral("                  <span class=\"badge bg-success\">פעיל</span>\r\n");
			}
			else
			{
				WriteLiteral("                  <span class=\"badge bg-secondary\">לא פעיל</span>\r\n");
			}
			WriteLiteral("              </td>\r\n              <td>\r\n                ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "f4ff0019eb2657f1c538d9d485c1678cf976e908a563918e7d5232dca3d16eb515066", async delegate
			{
				WriteLiteral("ערוך");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
			if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
			{
				throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
			}
			BeginWriteTagHelperAttribute();
			WriteLiteral(i.Id);
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
			WriteLiteral("\r\n");
			if (!i.IsActive)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "f4ff0019eb2657f1c538d9d485c1678cf976e908a563918e7d5232dca3d16eb517579", async delegate
				{
					WriteLiteral("\r\n                    ");
					Write(Html.AntiForgeryToken());
					WriteLiteral("\r\n                    <button type=\"submit\" class=\"btn btn-sm btn-success\"\r\n                            onclick=\"return confirm('להפעיל חודש זה? חודשים אחרים יושבתו.')\">\r\n                      הפעל\r\n                    </button>\r\n                  ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_5.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(i.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
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
			else
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "f4ff0019eb2657f1c538d9d485c1678cf976e908a563918e7d5232dca3d16eb521147", async delegate
				{
					WriteLiteral("\r\n                    ");
					Write(Html.AntiForgeryToken());
					WriteLiteral("\r\n                    <button type=\"submit\" class=\"btn btn-sm btn-outline-warning\">השבת</button>\r\n                  ");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_8.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
				if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
				{
					throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
				}
				BeginWriteTagHelperAttribute();
				WriteLiteral(i.Id);
				__tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
				__tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], HtmlAttributeValueStyle.DoubleQuotes);
				__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
				__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
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
			WriteLiteral("              </td>\r\n            </tr>\r\n");
		}
		WriteLiteral("        </tbody>\r\n      </table>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<!-- Add Modal -->\r\n<div class=\"modal fade\" id=\"addModal\" tabindex=\"-1\" aria-labelledby=\"addModalTitle\" aria-hidden=\"true\">\r\n  <div class=\"modal-dialog\" role=\"dialog\" aria-modal=\"true\">\r\n    <div class=\"modal-content\">\r\n      <div class=\"modal-header\">\r\n        <h5 class=\"modal-title\" id=\"addModalTitle\">הוסף חודש דיווח</h5>\r\n        <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"סגור חלון\"></button>\r\n      </div>\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "f4ff0019eb2657f1c538d9d485c1678cf976e908a563918e7d5232dca3d16eb525327", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"modal-body\">\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">תיאור <span class=\"text-danger\">*</span></label>\r\n            <input name=\"description\" class=\"form-control\" required placeholder=\"לדוגמה: ינואר 2025\" />\r\n          </div>\r\n          <div class=\"row\">\r\n            <div class=\"col-6 mb-3\">\r\n              <label class=\"form-label\">חודש <span class=\"text-danger\">*</span></label>\r\n              <select name=\"month\" class=\"form-select\" required>\r\n");
			int j;
			for (j = 1; j <= 12; j++)
			{
				WriteLiteral("                  ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "f4ff0019eb2657f1c538d9d485c1678cf976e908a563918e7d5232dca3d16eb526702", async delegate
				{
					Write(j);
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(j);
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
			WriteLiteral("              </select>\r\n            </div>\r\n            <div class=\"col-6 mb-3\">\r\n              <label class=\"form-label\">שנה <span class=\"text-danger\">*</span></label>\r\n              <input name=\"year\" type=\"number\" class=\"form-control\" min=\"2020\" max=\"2035\"");
			BeginWriteAttribute("value", "\r\n                     value=\"", 5102, "\"", 5150, 1);
			WriteAttributeValue("", 5132, DateTime.Now.Year, 5132, 18, isLiteral: false);
			EndWriteAttribute();
			WriteLiteral(" required />\r\n            </div>\r\n          </div>\r\n          <div class=\"mb-3\">\r\n            <label class=\"form-label\">תאריך אחרון לדיווח <span class=\"text-danger\">*</span></label>\r\n            <input name=\"lastReportingDate\" type=\"date\" class=\"form-control\" required />\r\n          </div>\r\n          <div class=\"mb-3 form-check\">\r\n            <input name=\"allowFutureReporting\" type=\"checkbox\" class=\"form-check-input\" id=\"allowFuture\" value=\"true\" />\r\n            <label class=\"form-check-label\" for=\"allowFuture\">אפשר דיווח עתידי</label>\r\n          </div>\r\n        </div>\r\n        <div class=\"modal-footer\">\r\n          <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">ביטול</button>\r\n          <button type=\"submit\" class=\"btn btn-primary\">שמור</button>\r\n        </div>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_9.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n");
	}
}
