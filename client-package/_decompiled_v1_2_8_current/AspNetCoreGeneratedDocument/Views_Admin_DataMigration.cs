using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/DataMigration.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_DataMigration : RazorPage<dynamic>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "ProjectPrograms", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-primary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("asp-action", "ReportingMonths", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("asp-action", "ImportLookups", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_6 = new TagHelperAttribute("enctype", new HtmlString("multipart/form-data"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_7 = new TagHelperAttribute("asp-action", "ImportEmployees", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_8 = new TagHelperAttribute("asp-action", "ImportInstitutions", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_9 = new TagHelperAttribute("asp-action", "ImportAllocations", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_10 = new TagHelperAttribute("asp-action", "ImportQuestionnaireCatalog", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_11 = new TagHelperAttribute("asp-action", "ImportClientLookupXlsb", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_12 = new TagHelperAttribute("asp-action", "BatchReportImport", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_13 = new TagHelperAttribute("class", new HtmlString("btn btn-primary btn-sm"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_14 = new TagHelperAttribute("asp-controller", "Employee", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_15 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

	private AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;

	private FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;

	private RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;

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
		base.ViewData["Title"] = "ייבוא נתונים ראשוני (AX-024)";
		string[] results = ((base.TempData["ImportResults"] as string) ?? "").Split('|', StringSplitOptions.RemoveEmptyEntries);
		WriteLiteral("\r\n<div class=\"container mt-3\">\r\n    <div class=\"d-flex justify-content-between align-items-center mb-3\">\r\n        <h3>ייבוא נתונים ראשוני</h3>\r\n        <div class=\"d-flex gap-2\">\r\n            ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f39164", async delegate
		{
			WriteLiteral("ניהול תוכניות לפי פרויקט");
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
		WriteLiteral("\r\n            ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f310456", async delegate
		{
			WriteLiteral("חזרה");
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
		WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"alert alert-info\">\r\n        <strong>כלי זה מיועד לייבוא ראשוני בלבד.</strong>\r\n        רשומות קיימות (לפי תיאור / ת.ז) לא יידרסו — יידלגו.\r\n        קבצי xlsx נטענים דרך הטפסים הרגילים. קובץ טבלאות xlsb של הלקוח נטען דרך הטופס הייעודי למטה.\r\n    </div>\r\n\r\n");
		if (results.Any())
		{
			WriteLiteral("        <div class=\"alert alert-secondary\">\r\n            <strong>תוצאות הייבוא האחרון:</strong>\r\n            <ul class=\"mb-0 mt-1\">\r\n");
			string[] array = results;
			foreach (string value in array)
			{
				WriteLiteral("                    <li>");
				Write(value);
				WriteLiteral("</li>\r\n");
			}
			WriteLiteral("            </ul>\r\n        </div>\r\n");
		}
		WriteLiteral("\r\n    <div class=\"row g-4\">\r\n\r\n        <!-- Lookup Tables Import -->\r\n        <div class=\"col-md-6\">\r\n            <div class=\"card h-100\">\r\n                <div class=\"card-header bg-primary text-white\">\r\n                    <strong>ייבוא טבלאות עזר</strong>\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <p class=\"text-muted small\">\r\n                        העלה קובץ Excel עם גיליונות בעלי השמות הבאים.\r\n                        שורה 1 = כותרת, עמודה A = תיאור.\r\n                    </p>\r\n                    <table class=\"table table-sm table-bordered small mb-3\">\r\n                        <thead class=\"table-light\">\r\n                            <tr><th>שם גיליון</th><th>תוכן</th></tr>\r\n                        </thead>\r\n                        <tbody>\r\n                            <tr><td>מחוזות</td><td>מחוזות</td></tr>\r\n                            <tr><td>מגזרים</td><td>מגזרים</td></tr>\r\n                            <tr><td>ישובים</td><td>ישובים</td></tr>\r\n                   ");
		WriteLiteral("         <tr><td>רשויות</td><td>רשויות</td></tr>\r\n                            <tr><td>פרויקטים</td><td>פרויקטים</td></tr>\r\n                            <tr><td>תוכניות</td><td>תוכניות</td></tr>\r\n                            <tr><td>מסגרות</td><td>מסגרות: A=שם, B=סמל מוסד, C=שלב חינוך</td></tr>\r\n                            <tr><td>נושאים</td><td>נושאי דיווח</td></tr>\r\n                            <tr><td>תחומים</td><td>תחומי דיווח</td></tr>\r\n                            <tr><td>תוכניות חינוכיות</td><td>תוכניות חינוכיות</td></tr>\r\n                            <tr><td>שכבות</td><td>שכבות גיל</td></tr>\r\n                            <tr><td>כיתות</td><td>כיתות</td></tr>\r\n                            <tr><td>קיום דיון</td><td>קיום דיון</td></tr>\r\n                            <tr><td>ישוב מחוז ארצי</td><td>איתור ישוב/מחוז/ארצי</td></tr>\r\n                            <tr><td>שלבי חינוך</td><td>שלבי חינוך</td></tr>\r\n                            <tr><td>רמות השכלה</td><td>רמות השכלה</td></tr>\r\n                        </tbody>\r\n ");
		WriteLiteral("                   </table>\r\n                    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f315569", async delegate
		{
			WriteLiteral("\r\n                        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n                        <div class=\"mb-3\">\r\n                            <label class=\"form-label\">קובץ Excel (xlsx)</label>\r\n                            <input type=\"file\" name=\"file\" accept=\".xlsx\" class=\"form-control form-control-sm\" required />\r\n                        </div>\r\n                        <button type=\"submit\" class=\"btn btn-primary btn-sm\">ייבא טבלאות עזר</button>\r\n                    ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n        <!-- Employee Import -->\r\n        <div class=\"col-md-6\">\r\n            <div class=\"card h-100\">\r\n                <div class=\"card-header bg-success text-white\">\r\n                    <strong>ייבוא עובדים</strong>\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <p class=\"text-muted small\">\r\n                        העלה קובץ Excel עם גיליון אחד. שורה 1 = כותרת.\r\n                        סיסמת ברירת מחדל = מספר ת.ז. המשתמש יתבקש לשנות סיסמה בהתחברות ראשונה.\r\n                    </p>\r\n                    <table class=\"table table-sm table-bordered small mb-3\">\r\n                        <thead class=\"table-light\">\r\n                            <tr><th>עמודה</th><th>תוכן</th><th>חובה</th></tr>\r\n                        </thead>\r\n                        <tbody>\r\n                            <tr><td>A</td><td>קוד עובד</td><td></td></tr>\r\n                            <tr><td>B</td><td>ת.ז</td><td class=\"fw-bold text-d");
		WriteLiteral("anger\">✓</td></tr>\r\n                            <tr><td>C</td><td>שם פרטי</td><td class=\"fw-bold text-danger\">✓</td></tr>\r\n                            <tr><td>D</td><td>שם משפחה</td><td></td></tr>\r\n                            <tr><td>E</td><td>מייל</td><td></td></tr>\r\n                            <tr><td>F</td><td>טלפון</td><td></td></tr>\r\n                            <tr><td>G</td><td>תפקיד עובד (תיאור)</td><td></td></tr>\r\n                            <tr><td>H</td><td>הערות</td><td></td></tr>\r\n                        </tbody>\r\n                    </table>\r\n                    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f319683", async delegate
		{
			WriteLiteral("\r\n                        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n                        <div class=\"mb-3\">\r\n                            <label class=\"form-label\">קובץ Excel (xlsx)</label>\r\n                            <input type=\"file\" name=\"file\" accept=\".xlsx\" class=\"form-control form-control-sm\" required />\r\n                        </div>\r\n                        <button type=\"submit\" class=\"btn btn-success btn-sm\">ייבא עובדים</button>\r\n                    ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_7.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n        <!-- Institution Import -->\r\n        <div class=\"col-md-6\">\r\n            <div class=\"card h-100\">\r\n                <div class=\"card-header bg-info text-white\">\r\n                    <strong>ייבוא מוסדות</strong>\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <p class=\"text-muted small\">\r\n                        שורה 1 = כותרת. ערכי בחירה יכולים להיות קוד מספרי או תיאור מדויק.\r\n                    </p>\r\n                    <table class=\"table table-sm table-bordered small mb-3\">\r\n                        <tbody>\r\n                            <tr><td>A</td><td>סמל מוסד</td><td class=\"fw-bold text-danger\">✓</td></tr>\r\n                            <tr><td>B</td><td>שם מוסד</td><td class=\"fw-bold text-danger\">✓</td></tr>\r\n                            <tr><td>C</td><td>ישוב</td><td></td></tr>\r\n                            <tr><td>D</td><td>מחוז</td><td></td></tr>\r\n                            <tr><td>E</td><td>מג");
		WriteLiteral("זר</td><td></td></tr>\r\n                            <tr><td>F</td><td>סוג חינוך</td><td></td></tr>\r\n                            <tr><td>G</td><td>שלב חינוך</td><td></td></tr>\r\n                        </tbody>\r\n                    </table>\r\n                    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f323470", async delegate
		{
			WriteLiteral("\r\n                        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n                        <div class=\"mb-3\">\r\n                            <label class=\"form-label\">קובץ Excel (xlsx)</label>\r\n                            <input type=\"file\" name=\"file\" accept=\".xlsx\" class=\"form-control form-control-sm\" required />\r\n                        </div>\r\n                        <button type=\"submit\" class=\"btn btn-info btn-sm text-white\">ייבא מוסדות</button>\r\n                    ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_8.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n        <!-- Allocation Import -->\r\n        <div class=\"col-md-6\">\r\n            <div class=\"card h-100\">\r\n                <div class=\"card-header bg-warning\">\r\n                    <strong>ייבוא הקצאות</strong>\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <p class=\"text-muted small\">\r\n                        ערכים מרובים מופרדים בנקודה-פסיק (;). ערכי בחירה יכולים להיות קוד מספרי או תיאור מדויק.\r\n                    </p>\r\n                    <table class=\"table table-sm table-bordered small mb-3\">\r\n                        <tbody>\r\n                            <tr><td>A</td><td>ת.ז עובד</td><td class=\"fw-bold text-danger\">✓</td></tr>\r\n                            <tr><td>B</td><td>פרויקט</td><td class=\"fw-bold text-danger\">✓</td></tr>\r\n                            <tr><td>C-J</td><td>היקפים, שורות, משך תפוקה, העלאת אקסל, הערות</td><td></td></tr>\r\n                            <tr><td>K-V</td><td>מחוזות, תוכניות, מ");
		WriteLiteral("גזרים, ישובים, מסגרות, נושאים, תחומים, תוכניות חינוכיות, כיתות, שכבות, קיום דיון, ישוב/מחוז/ארצי</td><td></td></tr>\r\n                        </tbody>\r\n                    </table>\r\n                    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f327212", async delegate
		{
			WriteLiteral("\r\n                        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n                        <div class=\"mb-3\">\r\n                            <label class=\"form-label\">קובץ Excel (xlsx)</label>\r\n                            <input type=\"file\" name=\"file\" accept=\".xlsx\" class=\"form-control form-control-sm\" required />\r\n                        </div>\r\n                        <button type=\"submit\" class=\"btn btn-warning btn-sm\">ייבא הקצאות</button>\r\n                    ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_9.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n        <!-- Questionnaire Catalog Import -->\r\n        <div class=\"col-md-6\">\r\n            <div class=\"card h-100\">\r\n                <div class=\"card-header bg-secondary text-white\">\r\n                    <strong>ייבוא קטלוג שאלונים</strong>\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <p class=\"text-muted small\">\r\n                        מתאים לקובץ \"קובץ משותף שאלונים לכל התוכניות\". הייבוא קורא את הגיליון \"כללי - מאוחד\"\r\n                        ומוסיף ערכי בחירה חסרים לטבלאות הרלוונטיות.\r\n                    </p>\r\n                    <table class=\"table table-sm table-bordered small mb-3\">\r\n                        <tbody>\r\n                            <tr><td>A</td><td>פרויקט</td></tr>\r\n                            <tr><td>B</td><td>תוכנית חינוכית</td></tr>\r\n                            <tr><td>C</td><td>תחום</td></tr>\r\n                            <tr><td>D-E</td><td>נושא 1 / נושא 2</td></tr>\r\n              ");
		WriteLiteral("              <tr><td>F</td><td>קיום דיון</td></tr>\r\n                            <tr><td>G,K</td><td>כיתות</td></tr>\r\n                            <tr><td>H</td><td>מסגרת חינוכית</td></tr>\r\n                            <tr><td>I</td><td>ישוב/מחוז/ארצי</td></tr>\r\n                            <tr><td>J</td><td>שכבה</td></tr>\r\n                        </tbody>\r\n                    </table>\r\n                    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f331146", async delegate
		{
			WriteLiteral("\r\n                        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n                        <div class=\"mb-3\">\r\n                            <label class=\"form-label\">קובץ שאלונים (xlsx)</label>\r\n                            <input type=\"file\" name=\"file\" accept=\".xlsx\" class=\"form-control form-control-sm\" required />\r\n                        </div>\r\n                        <button type=\"submit\" class=\"btn btn-secondary btn-sm\">ייבא קטלוג שאלונים</button>\r\n                    ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_10.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n        <!-- Client XLSB Lookup Import -->\r\n        <div class=\"col-md-6\">\r\n            <div class=\"card h-100\">\r\n                <div class=\"card-header bg-dark text-white\">\r\n                    <strong>ייבוא טבלאות לקוח xlsb</strong>\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <p class=\"text-muted small\">\r\n                        מתאים לקובץ \"טבלאות.xlsb\". הייבוא קורא את הגיליונות \"גיליון מרכז רשימות לפי שדות\",\r\n                        \"יישוב\" ו-\"מוסדות\".\r\n                    </p>\r\n                    <ul class=\"small text-muted\">\r\n                        <li>טבלאות בחירה: מחוזות, מגזרים, שלבי חינוך, פרויקטים, תוכניות חינוכיות, תחומים, נושאים, קודי דיון וכיתות.</li>\r\n                        <li>ישובים נטענים מהגיליון \"יישוב\".</li>\r\n                        <li>מוסדות ורמות השכלה נטענים מהגיליון \"מוסדות\".</li>\r\n                    </ul>\r\n                    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f334615", async delegate
		{
			WriteLiteral("\r\n                        ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n                        <div class=\"mb-3\">\r\n                            <label class=\"form-label\">קובץ טבלאות לקוח (xlsb)</label>\r\n                            <input type=\"file\" name=\"file\" accept=\".xlsb\" class=\"form-control form-control-sm\" required />\r\n                        </div>\r\n                        <button type=\"submit\" class=\"btn btn-dark btn-sm\">ייבא טבלאות xlsb</button>\r\n                    ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_11.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n\r\n    <!-- Batch Report Import (multi-employee) -->\r\n    <div class=\"card mt-4 border-primary\">\r\n        <div class=\"card-header bg-primary text-white\">\r\n            <strong>ייבוא דיווחים מרוכז — קובץ רב-עובדים</strong>\r\n        </div>\r\n        <div class=\"card-body\">\r\n            <p class=\"mb-2\">\r\n                העלאת קובץ אקסל יחיד המכיל דיווחים של מספר עובדים (מאות שורות). שורות תקינות נקלטות למערכת;\r\n                שגיאות מוצגות על המסך וניתנות להורדה כ-Excel. הודעת קליטה תישלח לכל עובד מדווח.\r\n            </p>\r\n            ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f337699", async delegate
		{
			WriteLiteral("פתיחת טופס ייבוא מרוכז");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_12.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    <!-- Download Template -->\r\n    <div class=\"card mt-4\">\r\n        <div class=\"card-header\">\r\n            <strong>הוראות שימוש</strong>\r\n        </div>\r\n        <div class=\"card-body small\">\r\n            <ol>\r\n                <li>צור קובץ Excel חדש עבור <strong>טבלאות עזר</strong>. לכל טבלה — גיליון נפרד עם שם מדויק כמו בטבלה למעלה. שורה 1 = כותרת (\"תיאור\"), שורה 2 ואילך = ערכים.</li>\r\n                <li>צור קובץ Excel נפרד עבור <strong>עובדים</strong>. גיליון אחד, שורה 1 = כותרת, שורה 2 ואילך = נתוני עובד לפי העמודות.</li>\r\n                <li>לקטלוג השאלונים ניתן להעלות את קובץ xlsx של השאלונים כפי שסופק, כל עוד קיים בו גיליון \"כללי - מאוחד\".</li>\r\n                <li>לאחר ייבוא העובדים, ניתן לייבא הקצאות מקובץ או להוסיף אותן דרך ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f339765", async delegate
		{
			WriteLiteral("ניהול עובדים");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_14.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_14);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_15.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral(".</li>\r\n                <li>לאחר ייבוא הנתונים, פתח חודש דיווח פעיל דרך ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "4d4f2e81a9f29a2e148e3417825e7e592b55207ffb5866d9bc99861a5ee693f341231", async delegate
		{
			WriteLiteral("חודשי דיווח");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<AnchorTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral(".</li>\r\n            </ol>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
	}
}
