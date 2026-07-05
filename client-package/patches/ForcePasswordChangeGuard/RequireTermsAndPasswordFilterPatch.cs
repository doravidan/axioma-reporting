using System.Security.Claims;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.SecurityPatch;

public static class RequireTermsAndPasswordFilterPatch
{
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

    public static async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next, AppDbContext db)
    {
        ClaimsPrincipal user = context.HttpContext.User;
        if (user.Identity is not { IsAuthenticated: true })
        {
            await next();
            return;
        }

        string controller = (context.RouteData.Values["controller"] as string) ?? string.Empty;
        string action = (context.RouteData.Values["action"] as string) ?? string.Empty;
        if (string.Equals(controller, "Account", StringComparison.OrdinalIgnoreCase) && ExemptActions.Contains(action))
        {
            await next();
            return;
        }

        string? userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdValue, out int userId))
        {
            await next();
            return;
        }

        User? currentUser = await db.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new User
            {
                Id = u.Id,
                MustChangePassword = u.MustChangePassword,
                LastPasswordChange = u.LastPasswordChange
            })
            .FirstOrDefaultAsync();

        if (currentUser is { } && (currentUser.MustChangePassword || IsPasswordExpired(currentUser.LastPasswordChange)))
        {
            context.Result = new RedirectToActionResult(
                "ChangePassword",
                "Account",
                new RouteValueDictionary { ["forced"] = true });
            return;
        }

        int? latestVersionId = await db.TermsOfUseVersions
            .OrderByDescending(v => v.VersionNumber)
            .Select(v => (int?)v.Id)
            .FirstOrDefaultAsync();

        if (!latestVersionId.HasValue)
        {
            await next();
            return;
        }

        bool acceptedTerms = await db.TermsOfUseAcceptances.AnyAsync(a => a.UserId == userId && a.VersionId == latestVersionId.Value);
        if (acceptedTerms)
        {
            await next();
            return;
        }

        HttpRequest request = context.HttpContext.Request;
        string? returnUrl = null;
        if (string.Equals(request.Method, "GET", StringComparison.OrdinalIgnoreCase))
        {
            string rawUrl = request.PathBase + request.Path + request.QueryString;
            returnUrl = string.IsNullOrEmpty(rawUrl) ? null : rawUrl;
        }

        RouteValueDictionary? routeValues = string.IsNullOrWhiteSpace(returnUrl)
            ? null
            : new RouteValueDictionary { ["returnUrl"] = returnUrl };
        context.Result = new RedirectToActionResult("TermsOfUse", "Account", routeValues);
    }

    private static bool IsPasswordExpired(DateTime? lastPasswordChange)
    {
        return !lastPasswordChange.HasValue || (DateTime.UtcNow - lastPasswordChange.Value).TotalDays > 90.0;
    }
}
