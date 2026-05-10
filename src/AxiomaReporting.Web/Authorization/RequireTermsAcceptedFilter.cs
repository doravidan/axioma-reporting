using System.Security.Claims;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Authorization;

/// <summary>
/// Global filter that blocks access to any page until the authenticated user has
/// accepted the latest published Terms of Use version. Account controller actions
/// (login/logout, TFA, password recovery, and the Terms screen itself) are exempt.
/// </summary>
public sealed class RequireTermsAcceptedFilter : IAsyncActionFilter
{
  // Actions on the AccountController that an authenticated-but-unaccepted user
  // is still allowed to hit. Note ChangePassword is included so a forced-change
  // user can rotate their password without first accepting Terms (and vice versa).
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

  public RequireTermsAcceptedFilter(AppDbContext db)
  {
    _db = db;
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

    var latestVersionId = await _db.TermsOfUseVersions
      .OrderByDescending(v => v.VersionNumber)
      .Select(v => (int?)v.Id)
      .FirstOrDefaultAsync();

    if (latestVersionId == null)
    {
      await next();
      return;
    }

    var accepted = await _db.TermsOfUseAcceptances
      .AnyAsync(a => a.UserId == userId && a.VersionId == latestVersionId);

    if (!accepted)
    {
      // Preserve the originally requested URL so the user lands back on it after acceptance.
      var request = context.HttpContext.Request;
      string? returnUrl = null;
      if (string.Equals(request.Method, "GET", StringComparison.OrdinalIgnoreCase))
      {
        var path = request.PathBase + request.Path + request.QueryString;
        returnUrl = string.IsNullOrEmpty(path) ? null : path;
      }

      var routeValues = string.IsNullOrWhiteSpace(returnUrl)
        ? null
        : new Microsoft.AspNetCore.Routing.RouteValueDictionary { ["returnUrl"] = returnUrl };

      context.Result = new RedirectToActionResult("TermsOfUse", "Account", routeValues);
      return;
    }

    await next();
  }
}
