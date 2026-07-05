using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	private readonly AppDbContext _db;

	public HomeController(ILogger<HomeController> logger, AppDbContext db)
	{
		_logger = logger;
		_db = db;
	}

	public IActionResult Index()
	{
		return View();
	}

	[AllowAnonymous]
	public async Task<IActionResult> Privacy()
	{
		var version = await _db.PrivacyPolicyVersions.AsNoTracking().OrderByDescending(v => v.VersionNumber).FirstOrDefaultAsync();
		StringBuilder html = new StringBuilder();
		html.Append("<!doctype html><html lang=\"he\" dir=\"rtl\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link rel=\"stylesheet\" href=\"/lib/bootstrap/dist/css/bootstrap.min.css\"><title>מדיניות פרטיות</title></head><body><main class=\"container mt-4\"><div class=\"card shadow-sm\"><div class=\"card-header bg-primary text-white\"><h1 class=\"h4 mb-0\">מדיניות פרטיות</h1></div><div class=\"card-body\">");
		if (version == null)
		{
			html.Append("<p class=\"text-muted\">לא הוגדרה מדיניות פרטיות. נא לפנות למנהל המערכת.</p>");
		}
		else
		{
			html.Append("<div class=\"small text-muted mb-3\">גרסה ").Append(version.VersionNumber).Append(" | בתוקף מתאריך ").Append(WebUtility.HtmlEncode(version.EffectiveFrom.ToLocalTime().ToString("dd/MM/yyyy HH:mm"))).Append("</div>");
			html.Append("<div class=\"border rounded p-3 bg-light\">").Append(version.BodyHtml).Append("</div>");
		}
		html.Append("</div></div></main></body></html>");
		return Content(html.ToString(), "text/html; charset=utf-8");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel
		{
			RequestId = (Activity.Current?.Id ?? base.HttpContext.TraceIdentifier)
		});
	}
}
