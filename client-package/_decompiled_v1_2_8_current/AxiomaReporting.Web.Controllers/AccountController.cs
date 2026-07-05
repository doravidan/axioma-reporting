using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

public class AccountController : Controller
{
	private const int TwoFactorMinutes = 10;

	private const int PasswordResetMinutes = 60;

	private const string StrongPasswordMessage = "הסיסמה חייבת להכיל לפחות 8 תווים, אות גדולה, אות קטנה, ספרה וסימן, ללא אותו תו פעמיים ברצף";

	private const string StrongPasswordMessageHebrew = "הסיסמה חייבת להכיל לפחות 8 תווים, אות גדולה, אות קטנה, ספרה וסימן, ללא אותו תו פעמיים ברצף";

	private readonly IAuthService _authService;

	private readonly IPasswordService _passwordService;

	private readonly IEmailService _emailService;

	private readonly IAuditLogService _auditLog;

	private readonly AppDbContext _db;

	public AccountController(IAuthService authService, IPasswordService passwordService, IEmailService emailService, IAuditLogService auditLog, AppDbContext db)
	{
		_authService = authService;
		_passwordService = passwordService;
		_emailService = emailService;
		_auditLog = auditLog;
		_db = db;
	}

	[HttpGet]
	public IActionResult Login(string? returnUrl = null)
	{
		IIdentity? identity = base.User.Identity;
		if (identity != null && identity.IsAuthenticated)
		{
			return RedirectToAction("Index", "Home");
		}
		base.ViewBag.ReturnUrl = returnUrl;
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginDto model, string? returnUrl = null)
	{
		if (!base.ModelState.IsValid)
		{
			return View(model);
		}
		var (flag, text, user) = await _authService.ValidateLoginAsync(model.IdNumber, model.Password);
		if (!flag || user == null)
		{
			base.ModelState.AddModelError(string.Empty, text ?? "שגיאת כניסה");
			return View(model);
		}
		if (await IsTfaEnabledAsync())
		{
			if (string.IsNullOrWhiteSpace(user.Email))
			{
				base.ModelState.AddModelError(string.Empty, "לא ניתן לבצע אימות דו-שלבי: לא מוגדר מייל למשתמש");
				return View(model);
			}
			await SendTwoFactorCodeAsync(user);
			base.TempData["PendingTfaUserId"] = user.Id.ToString();
			base.TempData["PendingTfaRememberMe"] = (model.RememberMe ? "true" : "false");
			base.TempData["PendingTfaReturnUrl"] = returnUrl ?? string.Empty;
			return RedirectToAction("TwoFactor");
		}
		await SignInAndRedirectAsync(user, model.RememberMe, returnUrl);
		return await PostLoginRedirectAsync(user, returnUrl);
	}

	[HttpGet]
	public IActionResult TwoFactor()
	{
		if (base.TempData.Peek("PendingTfaUserId") == null)
		{
			return RedirectToAction("Login");
		}
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> TwoFactor(string code)
	{
		string s = base.TempData.Peek("PendingTfaUserId")?.ToString();
		if (!int.TryParse(s, out var userId))
		{
			return RedirectToAction("Login");
		}
		string codeHash = HashValue(code?.Trim() ?? string.Empty);
		TwoFactorCode twoFactorCode = await (from c in _db.TwoFactorCodes
			where c.UserId == userId && c.CodeHash == codeHash && c.UsedAt == null && c.ExpiresAt >= DateTime.UtcNow
			orderby c.CreatedAt descending
			select c).FirstOrDefaultAsync();
		if (twoFactorCode == null)
		{
			base.ModelState.AddModelError(string.Empty, "קוד האימות שגוי או פג תוקף");
			return View();
		}
		twoFactorCode.UsedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		User user = await _db.Users.FindAsync(userId);
		if (user == null)
		{
			return RedirectToAction("Login");
		}
		bool rememberMe = base.TempData.Peek("PendingTfaRememberMe")?.ToString() == "true";
		string returnUrl = base.TempData.Peek("PendingTfaReturnUrl")?.ToString();
		base.TempData.Remove("PendingTfaUserId");
		base.TempData.Remove("PendingTfaRememberMe");
		base.TempData.Remove("PendingTfaReturnUrl");
		await SignInAndRedirectAsync(user, rememberMe, returnUrl);
		return await PostLoginRedirectAsync(user, returnUrl);
	}

	[Authorize]
	[HttpGet]
	public async Task<IActionResult> TermsOfUse(string? returnUrl = null)
	{
		TermsOfUseVersion termsOfUseVersion = await _db.TermsOfUseVersions.OrderByDescending((TermsOfUseVersion v) => v.VersionNumber).FirstOrDefaultAsync();
		base.ViewBag.LatestVersion = termsOfUseVersion;
		base.ViewBag.ReturnUrl = (base.Url.IsLocalUrl(returnUrl) ? returnUrl : null);
		return View(termsOfUseVersion);
	}

	[Authorize]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> AcceptTerms(string? returnUrl = null)
	{
		int userId = int.Parse(base.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));
		User user = await _db.Users.FindAsync(userId);
		if (user == null)
		{
			return RedirectToAction("Index", "Home");
		}
		TermsOfUseVersion latest = await _db.TermsOfUseVersions.OrderByDescending((TermsOfUseVersion v) => v.VersionNumber).FirstOrDefaultAsync();
		if (latest != null)
		{
			bool already = await _db.TermsOfUseAcceptances.AnyAsync((TermsOfUseAcceptance a) => a.UserId == userId && a.VersionId == latest.Id);
			if (!already)
			{
				_db.TermsOfUseAcceptances.Add(new TermsOfUseAcceptance
				{
					UserId = userId,
					VersionId = latest.Id,
					AcceptedAt = DateTime.UtcNow,
					IpAddress = base.HttpContext.Connection.RemoteIpAddress?.ToString()
				});
			}
			user.AcceptedTermsOfUse = true;
			user.UpdatedAt = DateTime.UtcNow;
			await _db.SaveChangesAsync();
			if (!already)
			{
				await _auditLog.LogAsync("Terms.Accept", "TermsOfUseVersion", latest.Id.ToString(), null, new
				{
					versionId = latest.Id,
					VersionNumber = latest.VersionNumber,
					userId = userId
				});
			}
		}
		if (user.MustChangePassword || _passwordService.IsPasswordExpired(user.LastPasswordChange))
		{
			return RedirectToAction("ChangePassword", new
			{
				forced = true
			});
		}
		if (base.Url.IsLocalUrl(returnUrl))
		{
			return Redirect(returnUrl);
		}
		return RedirectToAction("Index", "Home");
	}

	[Authorize]
	[HttpGet]
	public IActionResult ChangePassword(bool forced = false)
	{
		base.ViewBag.Forced = forced;
		return View();
	}

	[Authorize]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ChangePassword(ChangePasswordDto model, bool forced = false)
	{
		if (!base.ModelState.IsValid)
		{
			base.ViewBag.Forced = forced;
			return View(model);
		}
		if (model.NewPassword != model.ConfirmPassword)
		{
			base.ModelState.AddModelError("ConfirmPassword", "הסיסמאות אינן תואמות");
			base.ViewBag.Forced = forced;
			return View(model);
		}
		if (!_passwordService.IsPasswordStrong(model.NewPassword))
		{
			base.ModelState.AddModelError("NewPassword", "הסיסמה חייבת להכיל לפחות 8 תווים, אות גדולה, אות קטנה, ספרה וסימן, ללא אותו תו פעמיים ברצף");
			base.ViewBag.Forced = forced;
			return View(model);
		}
		int userId = int.Parse(base.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));
		User user = await _db.Users.FindAsync(userId);
		if (user == null)
		{
			return RedirectToAction("Login");
		}
		if (!_passwordService.VerifyPassword(model.CurrentPassword, user.PasswordHash))
		{
			base.ModelState.AddModelError("CurrentPassword", "הסיסמה הנוכחית שגויה");
			base.ViewBag.Forced = forced;
			return View(model);
		}
		if (_passwordService.VerifyPassword(model.NewPassword, user.PasswordHash))
		{
			base.ModelState.AddModelError("NewPassword", "הסיסמה החדשה חייבת להיות שונה מהסיסמה הנוכחית.");
			base.ViewBag.Forced = forced;
			return View(model);
		}
		if (await _authService.IsPasswordInHistoryAsync(userId, model.NewPassword))
		{
			base.ModelState.AddModelError("NewPassword", "לא ניתן להשתמש בסיסמה זהה או דומה מדי לאחת מ-5 הסיסמאות האחרונות");
			base.ViewBag.Forced = forced;
			return View(model);
		}
		if (!(await _authService.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword)))
		{
			base.ModelState.AddModelError("CurrentPassword", "הסיסמה הנוכחית שגויה");
			base.ViewBag.Forced = forced;
			return View(model);
		}
		base.TempData["Success"] = "הסיסמה שונתה בהצלחה";
		return RedirectToAction("Index", "Home");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout()
	{
		await base.HttpContext.SignOutAsync("Cookies");
		return RedirectToAction("Login");
	}

	[HttpGet]
	public IActionResult ForgotPassword()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ForgotPassword(string idNumber)
	{
		string idNumber2 = idNumber;
		User user2 = ((!string.IsNullOrWhiteSpace(idNumber2)) ? (await _db.Users.FirstOrDefaultAsync((User u) => u.IdNumber == idNumber2)) : null);
		User user = user2;
		if (user != null && !string.IsNullOrWhiteSpace(user.Email))
		{
			string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48)).Replace("+", "-").Replace("/", "_")
				.TrimEnd('=');
			_db.PasswordResetTokens.Add(new PasswordResetToken
			{
				UserId = user.Id,
				TokenHash = HashValue(token),
				ExpiresAt = DateTime.UtcNow.AddMinutes(60.0),
				CreatedAt = DateTime.UtcNow
			});
			await _db.SaveChangesAsync();
			string value = base.Url.Action("ResetPassword", "Account", new { token }, base.Request.Scheme) ?? string.Empty;
			await _emailService.SendAsync(user.Email, user.FirstName + " " + user.LastName, "PasswordReset", new Dictionary<string, string>
			{
				["EmployeeName"] = user.FirstName + " " + user.LastName,
				["ResetLink"] = value,
				["UserId"] = user.Id.ToString()
			});
		}
		else
		{
			_db.NotificationLogs.Add(new NotificationLog
			{
				NotificationType = "Account",
				TemplateType = "PasswordReset",
				RecipientUserId = user?.Id,
				RecipientEmail = (user?.Email ?? string.Empty),
				Subject = "(Skipped) ForgotPassword",
				Body = "ForgotPassword requested for IdNumber='" + idNumber2 + "' - " + ((user == null) ? "no such user" : "user has no email on file"),
				Status = "Skipped",
				AttemptCount = 0,
				CreatedAt = DateTime.UtcNow
			});
			await _db.SaveChangesAsync();
		}
		base.TempData["Success"] = "אם נמצאה כתובת מייל למשתמש, נשלח קישור לאיפוס סיסמה";
		return RedirectToAction("Login");
	}

	[HttpGet]
	public async Task<IActionResult> ResetPassword(string token)
	{
		string tokenHash = HashValue(token ?? string.Empty);
		if (await _db.PasswordResetTokens.FirstOrDefaultAsync((PasswordResetToken t) => t.TokenHash == tokenHash && t.UsedAt == null && t.ExpiresAt >= DateTime.UtcNow) == null)
		{
			base.TempData["Error"] = "קישור איפוס הסיסמה אינו תקין או פג תוקף";
			return RedirectToAction("Login");
		}
		base.ViewBag.Token = token;
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ResetPassword(string token, string newPassword, string confirmPassword)
	{
		if (newPassword != confirmPassword)
		{
			base.ModelState.AddModelError("confirmPassword", "הסיסמאות אינן תואמות");
		}
		if (!_passwordService.IsPasswordStrong(newPassword))
		{
			base.ModelState.AddModelError("newPassword", "הסיסמה חייבת להכיל לפחות 8 תווים, אות גדולה, אות קטנה, ספרה וסימן, ללא אותו תו פעמיים ברצף");
		}
		string tokenHash = HashValue(token ?? string.Empty);
		PasswordResetToken reset = await _db.PasswordResetTokens.FirstOrDefaultAsync((PasswordResetToken t) => t.TokenHash == tokenHash && t.UsedAt == null && t.ExpiresAt >= DateTime.UtcNow);
		if (reset == null)
		{
			base.ModelState.AddModelError(string.Empty, "קישור איפוס הסיסמה אינו תקין או פג תוקף");
		}
		if (!base.ModelState.IsValid)
		{
			base.ViewBag.Token = token;
			return View();
		}
		User user = await _db.Users.FindAsync(reset.UserId);
		if (user == null)
		{
			return RedirectToAction("Login");
		}
		if (_passwordService.VerifyPassword(newPassword, user.PasswordHash))
		{
			base.ModelState.AddModelError("newPassword", "הסיסמה החדשה חייבת להיות שונה מהסיסמה הנוכחית.");
			base.ViewBag.Token = token;
			return View();
		}
		if (await _authService.IsPasswordInHistoryAsync(user.Id, newPassword))
		{
			base.ModelState.AddModelError("newPassword", "לא ניתן להשתמש בסיסמה זהה או דומה מדי לאחת מ-5 הסיסמאות האחרונות");
			base.ViewBag.Token = token;
			return View();
		}
		await _authService.AddPasswordToHistoryAsync(user.Id, user.PasswordHash);
		user.PasswordHash = _passwordService.HashPassword(newPassword);
		user.MustChangePassword = false;
		user.LastPasswordChange = DateTime.UtcNow;
		user.UpdatedAt = DateTime.UtcNow;
		reset.UsedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		await _auditLog.LogAsync("Auth.PasswordReset", "User", user.Id.ToString(), null, null, $"ip={base.HttpContext.Connection.RemoteIpAddress}");
		base.TempData["Success"] = "הסיסמה אופסה בהצלחה";
		return RedirectToAction("Login");
	}

	[HttpGet]
	public IActionResult AccessDenied()
	{
		return View();
	}

	private async Task SendTwoFactorCodeAsync(User user)
	{
		string code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
		_db.TwoFactorCodes.Add(new TwoFactorCode
		{
			UserId = user.Id,
			CodeHash = HashValue(code),
			ExpiresAt = DateTime.UtcNow.AddMinutes(10.0),
			CreatedAt = DateTime.UtcNow
		});
		await _db.SaveChangesAsync();
		await _emailService.SendAsync(user.Email, user.FirstName + " " + user.LastName, "TwoFactorCode", new Dictionary<string, string>
		{
			["EmployeeName"] = user.FirstName + " " + user.LastName,
			["Code"] = code,
			["Minutes"] = 10.ToString()
		});
	}

	private async Task<bool> IsTfaEnabledAsync()
	{
		bool result;
		return bool.TryParse(await (from c in _db.SystemConstants
			where c.Key == "TfaEmailEnabled"
			select c.Value).FirstOrDefaultAsync(), out result) && result;
	}

	private async Task SignInAndRedirectAsync(User user, bool rememberMe, string? returnUrl)
	{
		List<Claim> claims = new List<Claim>
		{
			new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.Id.ToString()),
			new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.IdNumber),
			new Claim("FullName", user.FirstName + " " + user.LastName),
			new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", user.UserRoleId.ToString()),
			new Claim("UserRoleName", ((UserRoleEnum)user.UserRoleId).ToString())
		};
		ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookies");
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);
		AuthenticationProperties properties = new AuthenticationProperties
		{
			IsPersistent = rememberMe,
			ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30.0)
		};
		await base.HttpContext.SignInAsync("Cookies", principal, properties);
	}

	private async Task<IActionResult> PostLoginRedirectAsync(User user, string? returnUrl)
	{
		User user2 = user;
		int? latestVersionId = await ((IQueryable<TermsOfUseVersion>)_db.TermsOfUseVersions.OrderByDescending((TermsOfUseVersion v) => v.VersionNumber)).Select((Expression<Func<TermsOfUseVersion, int?>>)((TermsOfUseVersion v) => v.Id)).FirstOrDefaultAsync();
		if (latestVersionId.HasValue)
		{
			if (!(await _db.TermsOfUseAcceptances.AnyAsync((TermsOfUseAcceptance a) => a.UserId == user2.Id && (int?)a.VersionId == latestVersionId)))
			{
				return RedirectToAction("TermsOfUse");
			}
		}
		else if (!user2.AcceptedTermsOfUse)
		{
			return RedirectToAction("TermsOfUse");
		}
		if (user2.MustChangePassword || _passwordService.IsPasswordExpired(user2.LastPasswordChange))
		{
			return RedirectToAction("ChangePassword", new
			{
				forced = true
			});
		}
		if (!string.IsNullOrEmpty(returnUrl) && base.Url.IsLocalUrl(returnUrl))
		{
			return Redirect(returnUrl);
		}
		return RedirectToAction("Index", "Home");
	}

	private static string HashValue(string value)
	{
		byte[] inArray = SHA256.HashData(Encoding.UTF8.GetBytes(value));
		return Convert.ToHexString(inArray);
	}
}
