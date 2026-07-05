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

[RazorCompiledItemMetadata("Identifier", "/Views/MyAllocations/Details.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_MyAllocations_Details : RazorPage<Allocation>
{
	private static readonly TagHelperAttribute __tagHelperAttribute_0 = new TagHelperAttribute("asp-action", "Index", HtmlAttributeValueStyle.DoubleQuotes);

	private static readonly TagHelperAttribute __tagHelperAttribute_1 = new TagHelperAttribute("class", new HtmlString("btn btn-outline-secondary"), HtmlAttributeValueStyle.DoubleQuotes);

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
	public IHtmlHelper<Allocation> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.ViewData["Title"] = "פרטי הקצאה";
		WriteLiteral("\r\n<div class=\"container-fluid py-3\" dir=\"rtl\">\r\n  <div class=\"d-flex justify-content-between align-items-start flex-wrap gap-2 mb-3\">\r\n    <div>\r\n      <h2>פרטי הקצאה</h2>\r\n      <div class=\"text-muted\">\r\n        ");
		Write(base.Model.User?.FirstName);
		WriteLiteral(" ");
		Write(base.Model.User?.LastName);
		WriteLiteral("\r\n");
		if (!string.IsNullOrWhiteSpace(base.Model.User?.EmployeeCode))
		{
			WriteLiteral("          <span> | קוד עובד: ");
			Write(base.Model.User.EmployeeCode);
			WriteLiteral("</span>\r\n");
		}
		WriteLiteral("      </div>\r\n    </div>\r\n    ");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", TagMode.StartTagAndEndTag, "2bfe0ab913545e20bdd61172c483f3ccf5248ee945dadac8539fc377f16875456070", async delegate
		{
			WriteLiteral("חזרה להקצאות");
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
		WriteLiteral("\r\n  </div>\r\n\r\n  <section class=\"card border-success mb-3\" aria-labelledby=\"allocation-details-heading\">\r\n    <div id=\"allocation-details-heading\" class=\"card-header bg-success text-white fw-bold\">פרטי הקצאה</div>\r\n    <div class=\"card-body\">\r\n      <dl class=\"row mb-0\">\r\n        <dt class=\"col-md-2\">פרויקט</dt>\r\n        <dd class=\"col-md-4\">");
		Write(base.Model.Project?.Description);
		WriteLiteral("</dd>\r\n\r\n        <dt class=\"col-md-2\">היקף פעילות חודשי</dt>\r\n        <dd class=\"col-md-4\">");
		Write(Whole(base.Model.MonthlyEmploymentScope));
		WriteLiteral("</dd>\r\n\r\n        <dt class=\"col-md-2\">היקף יומי</dt>\r\n        <dd class=\"col-md-4\">");
		Write(Daily(base.Model.DailyEmploymentScope));
		WriteLiteral("</dd>\r\n\r\n        <dt class=\"col-md-2\">היקף פעילות שנתי</dt>\r\n        <dd class=\"col-md-4\">");
		Write(Whole(base.Model.AnnualEmploymentScope));
		WriteLiteral("</dd>\r\n\r\n        <dt class=\"col-md-2\">מכסת שורות חודשית</dt>\r\n        <dd class=\"col-md-4\">");
		Write(base.Model.MonthlyRowAllocation?.ToString() ?? "-");
		WriteLiteral("</dd>\r\n\r\n        <dt class=\"col-md-2\">מכסת שורות שנתית</dt>\r\n        <dd class=\"col-md-4\">");
		Write(base.Model.AnnualRowAllocation?.ToString() ?? "-");
		WriteLiteral("</dd>\r\n\r\n        <dt class=\"col-md-2\">משך תפוקה</dt>\r\n        <dd class=\"col-md-4\">");
		Write(string.IsNullOrWhiteSpace(base.Model.OutputDuration) ? "-" : base.Model.OutputDuration);
		WriteLiteral("</dd>\r\n\r\n        <dt class=\"col-md-2\">אפשר העלאת אקסל</dt>\r\n        <dd class=\"col-md-4\">");
		Write(base.Model.AllowExcelUpload ? "כן" : "לא");
		WriteLiteral("</dd>\r\n\r\n        <dt class=\"col-md-2\">הערות</dt>\r\n        <dd class=\"col-md-10\">");
		Write(string.IsNullOrWhiteSpace(base.Model.Notes) ? "-" : base.Model.Notes);
		WriteLiteral("</dd>\r\n      </dl>\r\n    </div>\r\n  </section>\r\n\r\n  <section aria-labelledby=\"allocation-scope-heading\">\r\n    <h3 id=\"allocation-scope-heading\" class=\"h5 mb-2\">שיוכים</h3>\r\n    <div class=\"table-responsive\">\r\n      <table class=\"table table-bordered align-middle\">\r\n        <tbody>\r\n          <tr>\r\n            <th scope=\"row\">תוכניות</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationPrograms.Select((AllocationProgram x) => x.Program?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">מחוזות</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationDistricts.Select((AllocationDistrict x) => x.District?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">מגזרים</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationSectors.Select((AllocationSector x) => x.Sector?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">ישובים</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationLocalities.Select((AllocationLocality x) => x.Locality?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">מסגרות</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationFrameworks.Select((AllocationFramework x) => x.Framework?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">תחומים</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationDomains.Select((AllocationDomain x) => x.Domain?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">נושאים</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationSubjects.Select((AllocationSubject x) => x.Subject?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">תוכניות חינוכיות</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationEducationalPrograms.Select((AllocationEducationalProgram x) => x.EducationalProgram?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">קיום דיון</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationDiscussionCodes.Select((AllocationDiscussionCode x) => x.DiscussionCode?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">כיתה</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationClasses.Select((AllocationClass x) => x.SchoolClass?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">שכבה</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationGradeLevels.Select((AllocationGradeLevel x) => x.GradeLevel?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n          <tr>\r\n            <th scope=\"row\">ישובי / מחוזי / ארצי</th>\r\n            <td>");
		Write(JoinValues(base.Model.AllocationLocalityDistrictNationals.Select((AllocationLocalityDistrictNational x) => x.LocalityDistrictNational?.Description)));
		WriteLiteral("</td>\r\n          </tr>\r\n        </tbody>\r\n      </table>\r\n    </div>\r\n  </section>\r\n</div>\r\n");
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
