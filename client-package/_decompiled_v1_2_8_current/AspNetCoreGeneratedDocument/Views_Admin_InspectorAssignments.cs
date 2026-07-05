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

[RazorCompiledItemMetadata("Identifier", "/Views/Admin/InspectorAssignments.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Admin_InspectorAssignments : RazorPage<dynamic>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("value", "", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("asp-action", "CreateInspectorAssignment", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_2 = new TagHelperAttribute("method", "post", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_3 = new TagHelperAttribute("class", new HtmlString("card card-body mb-3"), HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_4 = new TagHelperAttribute("asp-action", "DeleteInspectorAssignment", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_5 = new TagHelperAttribute("onsubmit", new HtmlString("return confirm('למחוק שיוך?')"), HtmlAttributeValueStyle.DoubleQuotes);

	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager;

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
		base.ViewData["Title"] = "שיוכי מפקחים";
		List<InspectorAssignment> assignments = (base.ViewBag.Assignments as List<InspectorAssignment>) ?? new List<InspectorAssignment>();
		List<User> inspectors = (base.ViewBag.Inspectors as List<User>) ?? new List<User>();
		List<AxiomaReporting.Core.Entities.Program> programs = (base.ViewBag.Programs as List<AxiomaReporting.Core.Entities.Program>) ?? new List<AxiomaReporting.Core.Entities.Program>();
		List<District> districts = (base.ViewBag.Districts as List<District>) ?? new List<District>();
		List<Sector> sectors = (base.ViewBag.Sectors as List<Sector>) ?? new List<Sector>();
		WriteLiteral("\r\n<div class=\"container-fluid mt-3\">\r\n  <h3>שיוכי מפקחים</h3>\r\n  <p class=\"text-muted\">בתוך שורה אחת התנאים מצטברים ב-AND. ערך ריק הוא wildcard. כמה שורות לאותו מפקח מחוברות ב-OR.</p>\r\n\r\n  ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb16575", async delegate
		{
			WriteLiteral("\r\n    ");
			Write(Html.AntiForgeryToken());
			WriteLiteral("\r\n    <div class=\"row g-2 align-items-end\">\r\n      <div class=\"col-md-3\">\r\n        <label class=\"form-label\">מפקח</label>\r\n        <select name=\"inspectorUserId\" class=\"form-select\" required>\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb17365", async delegate
			{
				WriteLiteral("בחר...");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (User inspector in inspectors)
			{
				WriteLiteral("            ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb18890", async delegate
				{
					Write(inspector.FirstName);
					WriteLiteral(" ");
					Write(inspector.LastName);
					WriteLiteral(" (");
					Write(inspector.EmployeeCode);
					WriteLiteral(", ");
					Write(inspector.IdNumber);
					WriteLiteral(")");
				});
				__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
				__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
				BeginWriteTagHelperAttribute();
				WriteLiteral(inspector.Id);
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
			WriteLiteral("        </select>\r\n      </div>\r\n      <div class=\"col-md-3\">\r\n        <label class=\"form-label\">תוכנית</label>\r\n        <select name=\"programId\" class=\"form-select\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb111883", async delegate
			{
				WriteLiteral("כל התוכניות");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (AxiomaReporting.Core.Entities.Program item4 in programs)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb113386", async delegate
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
			WriteLiteral("        </select>\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\">מחוז</label>\r\n        <select name=\"districtId\" class=\"form-select\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb115733", async delegate
			{
				WriteLiteral("כל המחוזות");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (District item3 in districts)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb117236", async delegate
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
			WriteLiteral("        </select>\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <label class=\"form-label\">מגזר</label>\r\n        <select name=\"sectorId\" class=\"form-select\">\r\n          ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb119581", async delegate
			{
				WriteLiteral("כל המגזרים");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<OptionTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n");
			foreach (Sector item2 in sectors)
			{
				WriteLiteral(" ");
				__tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb121082", async delegate
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
			WriteLiteral("        </select>\r\n      </div>\r\n      <div class=\"col-md-2\">\r\n        <button type=\"submit\" class=\"btn btn-primary w-100\">הוסף שיוך</button>\r\n      </div>\r\n    </div>\r\n  ");
		});
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
		__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
		__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
		__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n\r\n  <div class=\"table-responsive\">\r\n    <table class=\"table table-bordered table-sm align-middle\">\r\n      <thead class=\"table-light\">\r\n        <tr>\r\n          <th>מפקח</th>\r\n          <th>תוכנית</th>\r\n          <th>מחוז</th>\r\n          <th>מגזר</th>\r\n          <th>פעולות</th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n");
		foreach (InspectorAssignment item in assignments)
		{
			WriteLiteral("          <tr>\r\n            <td>");
			Write(item.Inspector?.FirstName);
			WriteLiteral(" ");
			Write(item.Inspector?.LastName);
			WriteLiteral("</td>\r\n            <td>");
			Write(item.Program?.Description ?? "הכל");
			WriteLiteral("</td>\r\n            <td>");
			Write(item.District?.Description ?? "הכל");
			WriteLiteral("</td>\r\n            <td>");
			Write(item.Sector?.Description ?? "הכל");
			WriteLiteral("</td>\r\n            <td>\r\n              ");
			__tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", TagMode.StartTagAndEndTag, "c96658489314ffdf1042567abbc03d2b1bc066d70f1b634cabe890b8656ecdb127003", async delegate
			{
				WriteLiteral("\r\n                ");
				Write(Html.AntiForgeryToken());
				WriteLiteral("\r\n                <input type=\"hidden\" name=\"id\"");
				BeginWriteAttribute("value", " value=\"", 3511, "\"", 3527, 1);
				WriteAttributeValue("", 3519, item.Id, 3519, 8, isLiteral: false);
				EndWriteAttribute();
				WriteLiteral(" />\r\n                <button type=\"submit\" class=\"btn btn-sm btn-outline-danger\">מחק</button>\r\n              ");
			});
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<FormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<RenderAtEndOfFormTagHelper>();
			__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
			__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
			__tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
			__tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
			await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
			if (!__tagHelperExecutionContext.Output.IsContentModified)
			{
				await __tagHelperExecutionContext.SetOutputContentAsync();
			}
			Write(__tagHelperExecutionContext.Output);
			__tagHelperExecutionContext = __tagHelperScopeManager.End();
			WriteLiteral("\r\n            </td>\r\n          </tr>\r\n");
		}
		if (!assignments.Any())
		{
			WriteLiteral("          <tr><td colspan=\"5\" class=\"text-center text-muted\">אין שיוכים</td></tr>\r\n");
		}
		WriteLiteral("      </tbody>\r\n    </table>\r\n  </div>\r\n</div>\r\n");
	}
}
