using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Authorization;

public sealed class RequireTermsAcceptedFilter : IAsyncActionFilter, IFilterMetadata
{
	private static readonly HashSet<string> ExemptActions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Login", "Logout", "TwoFactor", "ForgotPassword", "ResetPassword", "ChangePassword", "TermsOfUse", "AcceptTerms", "AccessDenied" };

	private readonly AppDbContext _db;

	public RequireTermsAcceptedFilter(AppDbContext db)
	{
		_db = db;
	}

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		ClaimsPrincipal user = context.HttpContext.User;
		IIdentity? identity = user.Identity;
		if (identity == null || !identity.IsAuthenticated)
		{
			await next();
			return;
		}
		string a2 = (context.RouteData.Values["controller"] as string) ?? string.Empty;
		string item = (context.RouteData.Values["action"] as string) ?? string.Empty;
		if (string.Equals(a2, "Account", StringComparison.OrdinalIgnoreCase) && ExemptActions.Contains(item))
		{
			await next();
			return;
		}
		string s = user.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
		if (!int.TryParse(s, out var userId))
		{
			await next();
			return;
		}
		User currentUser = await _db.Users.AsNoTracking().Where((User u) => u.Id == userId).Select((User u) => new User
		{
			Id = u.Id,
			MustChangePassword = u.MustChangePassword,
			LastPasswordChange = u.LastPasswordChange
		}).FirstOrDefaultAsync();
		if (currentUser != null && (currentUser.MustChangePassword || IsPasswordExpired(currentUser.LastPasswordChange)))
		{
			context.Result = new RedirectToActionResult("ChangePassword", "Account", new RouteValueDictionary { ["forced"] = true });
			return;
		}
		int? latestVersionId = await ((IQueryable<TermsOfUseVersion>)_db.TermsOfUseVersions.OrderByDescending((TermsOfUseVersion v) => v.VersionNumber)).Select((Expression<Func<TermsOfUseVersion, int?>>)((TermsOfUseVersion v) => v.Id)).FirstOrDefaultAsync();
		if (!latestVersionId.HasValue)
		{
			await next();
		}
		else if (!(await _db.TermsOfUseAcceptances.AnyAsync((TermsOfUseAcceptance a) => a.UserId == userId && (int?)a.VersionId == latestVersionId)))
		{
			HttpRequest request = context.HttpContext.Request;
			string value = null;
			if (string.Equals(request.Method, "GET", StringComparison.OrdinalIgnoreCase))
			{
				string text = request.PathBase + request.Path + request.QueryString;
				value = (string.IsNullOrEmpty(text) ? null : text);
			}
			RouteValueDictionary routeValues = (string.IsNullOrWhiteSpace(value) ? null : new RouteValueDictionary { ["returnUrl"] = value });
			context.Result = new RedirectToActionResult("TermsOfUse", "Account", routeValues);
		}
		else
		{
			await next();
		}
	}

	private static bool IsPasswordExpired(DateTime? lastPasswordChange)
	{
		if (!lastPasswordChange.HasValue)
		{
			return true;
		}
		return (DateTime.UtcNow - lastPasswordChange.Value).TotalDays > 90.0;
	}
}
