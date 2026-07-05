using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AxiomaReporting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Views/Report/Manual.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Views_Report_Manual : RazorPage<ManualReportViewModel>
{
	[RazorInject]
	public IModelExpressionProvider ModelExpressionProvider { get; private set; }

	[RazorInject]
	public IUrlHelper Url { get; private set; }

	[RazorInject]
	public IViewComponentHelper Component { get; private set; }

	[RazorInject]
	public IJsonHelper Json { get; private set; }

	[RazorInject]
	public IHtmlHelper<ManualReportViewModel> Html { get; private set; }

	public override async Task ExecuteAsync()
	{
		base.Layout = "_Layout";
		base.ViewData["Title"] = "הוספת דיווח ידני";
		WriteLiteral("<div class=\"container mt-4\" dir=\"rtl\"><div class=\"d-flex justify-content-between align-items-center mb-3 flex-wrap gap-2\"><h3 class=\"mb-0\">הוספת דיווח ידני</h3><a class=\"btn btn-outline-secondary\" href=\"/Report\">חזרה לדיווח</a></div>");
		if (TempData["ManualError"] != null)
		{
			WriteLiteral("<div class=\"alert alert-danger\" role=\"alert\">");
			Write(TempData["ManualError"]);
			WriteLiteral("</div>");
		}
		WriteLiteral("<form method=\"get\" action=\"/Report/ManualOpen\" class=\"card card-body\"><input type=\"hidden\" id=\"manualUserId\" name=\"userId\" required /><div class=\"row g-3 align-items-end\"><div class=\"col-md-3\"><label class=\"form-label\" for=\"manualIdNumber\">ת.ז</label><input id=\"manualIdNumber\" class=\"form-control manual-employee-filter\" autocomplete=\"off\" /></div><div class=\"col-md-3\"><label class=\"form-label\" for=\"manualEmployeeCode\">קוד</label><input id=\"manualEmployeeCode\" class=\"form-control manual-employee-filter\" autocomplete=\"off\" /></div><div class=\"col-md-3\"><label class=\"form-label\" for=\"manualFirstName\">שם פרטי</label><input id=\"manualFirstName\" class=\"form-control manual-employee-filter\" autocomplete=\"off\" /></div><div class=\"col-md-3\"><label class=\"form-label\" for=\"manualLastName\">שם משפחה</label><input id=\"manualLastName\" class=\"form-control manual-employee-filter\" autocomplete=\"off\" /></div><div class=\"col-12\"><div id=\"manualEmployeeResults\" class=\"list-group small\"></div></div><div class=\"col-md-5\"><label class=\"form-label\" for=\"manualAllocationSelect\">הקצאה</label><select id=\"manualAllocationSelect\" name=\"allocationId\" class=\"form-select\" required disabled></select></div><div class=\"col-md-5\"><label class=\"form-label\" for=\"manualReportingMonth\">חודש דיווח</label><select id=\"manualReportingMonth\" name=\"reportingMonthId\" class=\"form-select\" required>");
		foreach (var month in Model.ReportingMonths)
		{
			WriteLiteral("<option value=\"");
			Write(month.Id);
			WriteLiteral("\">");
			Write(month.Description);
			WriteLiteral("</option>");
		}
		WriteLiteral("</select></div><div class=\"col-md-2\"><button id=\"manualOpenButton\" type=\"submit\" class=\"btn btn-primary w-100\" disabled>פתח</button></div></div><div id=\"manualNoAllocations\" class=\"text-danger small mt-2\" style=\"display:none\">אין הקצאה פעילה לעובד שנבחר</div></form></div>");
	}
}
