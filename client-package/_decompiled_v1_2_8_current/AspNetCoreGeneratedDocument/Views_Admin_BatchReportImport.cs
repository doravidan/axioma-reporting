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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/BatchReportImport.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_BatchReportImport : RazorPage<BatchReportImportFormViewModel>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "DataMigration", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-action", "BatchReportImport", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("enctype", new HtmlString("multipart/form-data"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<BatchReportImportFormViewModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "ייבוא דיווחים מרוכז";
		WriteLiteral("\r\n<div class=\"container mt-3\" dir=\"rtl\">\r\n  <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n    <h3>ייבוא דיווחים מרוכז — קובץ רב-עובדים</h3>\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "1f1492477385e5ca469b4c3806eda9036087c572b5ecfe5e149d05f5b0bcc23a5682", async delegate
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
		WriteLiteral("\r\n  </div>\r\n\r\n");
		if (base.TempData["Error"] != null)
		{
			WriteLiteral("    <div class=\"alert alert-danger\" role=\"alert\" aria-live=\"assertive\">");
			Write(base.TempData["Error"]);
			WriteLiteral("</div>\r\n");
		}
		WriteLiteral("\r\n  <div class=\"alert alert-info\">\r\n    <strong>כלי זה מיועד למנהל מערכת ולמנהל פרויקט בלבד.</strong>\r\n    <ul class=\"mb-0 mt-2 small\">\r\n      <li>קובץ אקסל (xlsx) יחיד המכיל דיווחים של מספר עובדים.</li>\r\n      <li>שורת כותרת מזוהה אוטומטית לפי הכותרת \"קוד עובד\".</li>\r\n      <li>לכל שורה תקינה תיקלט פעילות חודשית לעובד המתאים, וההודעה \"דיווח התקבל\" תישלח לכתובת הדוא\"ל שלו.</li>\r\n      <li>שורות שגויות לא תיקלטנה. רשימת השגיאות תוצג על המסך וניתנת להורדה כ-Excel.</li>\r\n    </ul>\r\n  </div>\r\n\r\n  <div class=\"card\">\r\n    <div class=\"card-body\">\r\n      ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "1f1492477385e5ca469b4c3806eda9036087c572b5ecfe5e149d05f5b0bcc23a8325", async delegate
		{
			WriteLiteral("\r\n        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n        <div class=\"mb-3\">\r\n          <label for=\"reportingMonthId\" class=\"form-label\">חודש דיווח</label>\r\n          <select name=\"reportingMonthId\" id=\"reportingMonthId\" class=\"form-select\" required>\r\n");
			foreach (ReportingMonth reportingMonth in base.Model.ReportingMonths)
			{
				string label = $"{reportingMonth.Description} ({reportingMonth.Month}/{reportingMonth.Year})" + (reportingMonth.IsActive ? " — פעיל" : "");
				if (base.Model.SelectedReportingMonthId == reportingMonth.Id)
				{
					WriteLiteral("                ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1f1492477385e5ca469b4c3806eda9036087c572b5ecfe5e149d05f5b0bcc23a9578", async delegate
					{
						Write(label);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(reportingMonth.Id);
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
					WriteLiteral("\r\n");
				}
				else
				{
					WriteLiteral("                ");
					__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "1f1492477385e5ca469b4c3806eda9036087c572b5ecfe5e149d05f5b0bcc23a12098", async delegate
					{
						Write(label);
					});
					__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
					__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
					BeginWriteTagHelperAttribute();
					WriteLiteral(reportingMonth.Id);
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
			}
			WriteLiteral("          </select>\r\n        </div>\r\n        <div class=\"mb-3\">\r\n          <label for=\"file\" class=\"form-label\">קובץ אקסל (xlsx)</label>\r\n          <input type=\"file\" id=\"file\" name=\"file\" accept=\".xlsx\" class=\"form-control\" required />\r\n        </div>\r\n        <div id=\"batchImportProgressBox\" class=\"d-none mb-3\" aria-live=\"polite\">\r\n          <div class=\"d-flex justify-content-between align-items-center mb-1\">\r\n            <span id=\"batchImportProgressText\" class=\"small text-muted\">מכין ייבוא...</span>\r\n            <span id=\"batchImportProgressPercent\" class=\"small fw-semibold\">0%</span>\r\n          </div>\r\n          <div class=\"progress\" style=\"height: 1rem;\">\r\n            <div id=\"batchImportProgressBar\" class=\"progress-bar progress-bar-striped progress-bar-animated\" role=\"progressbar\" style=\"width: 0%;\" aria-valuemin=\"0\" aria-valuemax=\"100\" aria-valuenow=\"0\"></div>\r\n          </div>\r\n        </div>\r\n        <button type=\"submit\" class=\"btn btn-primary\">ייבא דיווחים</button>\r\n      ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n<script>\r\n(function () {\r\n  const form = document.querySelector('form[method=\"post\"]');\r\n  if (!form) return;\r\n  const box = document.getElementById('batchImportProgressBox');\r\n  const bar = document.getElementById('batchImportProgressBar');\r\n  const text = document.getElementById('batchImportProgressText');\r\n  const percent = document.getElementById('batchImportProgressPercent');\r\n  const button = form.querySelector('button[type=\"submit\"]');\r\n  function setProgress(value, label) {\r\n    const safeValue = Math.max(0, Math.min(100, Number(value) || 0));\r\n    bar.style.width = safeValue + '%';\r\n    bar.setAttribute('aria-valuenow', String(safeValue));\r\n    percent.textContent = safeValue + '%';\r\n    if (label) text.textContent = label;\r\n  }\r\n  form.addEventListener('submit', async function (event) {\r\n    event.preventDefault();\r\n    if (!form.reportValidity()) return;\r\n    const progressId = (window.crypto && crypto.randomUUID) ? crypto.randomUUID() : (Date.now().toString(36) + Math.random().toString(36).slice(2));\r\n    let progressInput = form.querySelector('input[name=\"progressId\"]');\r\n    if (!progressInput) {\r\n      progressInput = document.createElement('input');\r\n      progressInput.type = 'hidden';\r\n      progressInput.name = 'progressId';\r\n      form.appendChild(progressInput);\r\n    }\r\n    progressInput.value = progressId;\r\n    box.classList.remove('d-none');\r\n    button.disabled = true;\r\n    button.textContent = 'מייבא...';\r\n    setProgress(1, 'מעלה קובץ ומתחיל ייבוא...');\r\n    const poll = window.setInterval(async function () {\r\n      try {\r\n        const response = await fetch('/Admin/BatchReportImportProgress?id=' + encodeURIComponent(progressId), { cache: 'no-store' });\r\n        if (!response.ok) return;\r\n        const progress = await response.json();\r\n        const processed = progress.processedRows || progress.ProcessedRows || 0;\r\n        const total = progress.totalRows || progress.TotalRows || 0;\r\n        const progressPercent = progress.percent || progress.Percent || 0;\r\n        const label = total > 0 ? ('מעבד שורה ' + processed + ' מתוך ' + total) : 'מעבד קובץ...';\r\n        setProgress(progressPercent, label);\r\n      } catch (error) {\r\n      }\r\n    }, 700);\r\n    try {\r\n      const response = await fetch(form.action, { method: 'POST', body: new FormData(form), credentials: 'same-origin' });\r\n      const html = await response.text();\r\n      window.clearInterval(poll);\r\n      setProgress(100, 'הייבוא הסתיים, טוען תוצאות...');\r\n      document.open();\r\n      document.write(html);\r\n      document.close();\r\n    } catch (error) {\r\n      window.clearInterval(poll);\r\n      button.disabled = false;\r\n      button.textContent = 'ייבא דיווחים';\r\n      setProgress(0, 'אירעה שגיאה בייבוא. נסה שוב.');\r\n    }\r\n  });\r\n})();\r\n</script>\r\n");
	}
}
