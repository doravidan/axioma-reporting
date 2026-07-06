using System.Security.Claims;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Authorization;

/// <summary>
/// Global filter that prevents an authenticated user whose password must be changed
/// (first login / admin reset / 90-day expiry) from reaching any page other than the
/// change-password screen. Without this, a user sitting on the forced ChangePassword
/// page could simply click a menu link and bypass the requirement (client QA finding).
/// </summary>
public sealed class RequirePasswordChangedFilter : IAsyncActionFilter
{
  // Account actions that remain reachable while a password change is pending.
  private static readonly HashSet<string> ExemptActions = new(StringComparer.OrdinalIgnoreCase)
  {
    "Login",
    "Logout",
    "TwoFactor",
    "ForgotPassword",
    "ResetPassword",
    "ChangePassword",
    "TermsOfUse",
    "AcceptTerms",
    "AccessDenied"
  };

  private readonly AppDbContext _db;
  private readonly IPasswordService _passwordService;

  public RequirePasswordChangedFilter(AppDbContext db, IPasswordService passwordService)
  {
    _db = db;
    _passwordService = passwordService;
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    var user = context.HttpContext.User;
    if (user.Identity?.IsAuthenticated != true)
    {
      await next();
      return;
    }

    var controller = (context.RouteData.Values["controller"] as string) ?? string.Empty;
    var action = (context.RouteData.Values["action"] as string) ?? string.Empty;

    if (string.Equals(controller, "Account", StringComparison.OrdinalIgnoreCase) &&
        ExemptActions.Contains(action))
    {
      await next();
      return;
    }

    var userIdRaw = user.FindFirstValue(ClaimTypes.NameIdentifier);
    if (!int.TryParse(userIdRaw, out var userId))
    {
      await next();
      return;
    }

    var flags = await _db.Users
      .Where(u => u.Id == userId)
      .Select(u => new { u.MustChangePassword, u.LastPasswordChange })
      .FirstOrDefaultAsync();

    if (flags != null &&
        (flags.MustChangePassword || _passwordService.IsPasswordExpired(flags.LastPasswordChange)))
    {
      context.Result = new RedirectToActionResult("ChangePassword", "Account", null);
      return;
    }

    await next();
  }
}
