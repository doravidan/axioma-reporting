using System.Diagnostics;
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

  public HomeController(ILogger<HomeController> logger, AppDbContext db)
  {
    _logger = logger;
    _db = db;
  }

  public IActionResult Index()
  {
    return View();
  }

  // מדיניות פרטיות נגישה לכל המשתמשים — כולל לפני התחברות (משוב בטא B4/B9).
  // אם פורסמה גרסה דרך ניהול מדיניות הפרטיות — היא המוצגת; אחרת נוסח ברירת המחדל.
  [AllowAnonymous]
  public async Task<IActionResult> Privacy()
  {
    ViewBag.PublishedVersion = await _db.PrivacyPolicyVersions
      .AsNoTracking()
      .OrderByDescending(v => v.VersionNumber)
      .FirstOrDefaultAsync();
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
