using System.Diagnostics;
using System.Net;
using System.Text;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AxiomaReporting.Web.Models;

namespace AxiomaReporting.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly AppDbContext _db;
  private readonly IHtmlSanitizerService _htmlSanitizer;

  public HomeController(ILogger<HomeController> logger, AppDbContext db, IHtmlSanitizerService htmlSanitizer)
  {
    _logger = logger;
    _db = db;
    _htmlSanitizer = htmlSanitizer;
  }

  public IActionResult Index()
  {
    return View();
  }

  [AllowAnonymous]
  public async Task<IActionResult> Privacy()
  {
    var latest = await _db.PrivacyPolicyVersions
      .AsNoTracking()
      .OrderByDescending(v => v.VersionNumber)
      .FirstOrDefaultAsync();

    var sb = new StringBuilder();
    sb.Append("<!doctype html><html lang=\"he\" dir=\"rtl\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><link rel=\"stylesheet\" href=\"/lib/bootstrap/dist/css/bootstrap.min.css\"><title>מדיניות פרטיות</title></head><body><main class=\"container mt-4\"><div class=\"card shadow-sm\"><div class=\"card-header bg-primary text-white\"><h1 class=\"h4 mb-0\">מדיניות פרטיות</h1></div><div class=\"card-body\">");

    if (latest == null)
    {
      sb.Append("<p class=\"text-muted\">לא הוגדרה מדיניות פרטיות. נא לפנות למנהל המערכת.</p>");
    }
    else
    {
      sb.Append("<div class=\"small text-muted mb-3\">גרסה ").Append(latest.VersionNumber).Append(" | בתוקף מתאריך ")
        .Append(WebUtility.HtmlEncode(latest.EffectiveFrom.ToLocalTime().ToString("dd/MM/yyyy HH:mm")))
        .Append("</div>");
      // Defense in depth: AdminController sanitizes on save (see PublishPrivacyPolicy),
      // but this anonymous, publicly reachable page also sanitizes at render time in
      // case older rows predate that fix. See CLAUDE.md security review finding C1.
      sb.Append("<div class=\"border rounded p-3 bg-light\">").Append(_htmlSanitizer.Sanitize(latest.BodyHtml)).Append("</div>");
    }

    sb.Append("</div></div></main></body></html>");
    return Content(sb.ToString(), "text/html; charset=utf-8");
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
