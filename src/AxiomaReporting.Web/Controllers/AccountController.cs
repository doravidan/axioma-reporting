using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

public class AccountController : Controller
{
  private const int TwoFactorMinutes = 10;
  private const int PasswordResetMinutes = 60;
  private const string StrongPasswordMessage =
    "הסיסמה חייבת להכיל לפחות 8 תווים, אות גדולה, אות קטנה, ספרה וסימן, ללא אותו תו פעמיים ברצף";

  private const string StrongPasswordMessageHebrew =
    "הסיסמה חייבת להכיל לפחות 8 תווים, אות גדולה, אות קטנה, ספרה וסימן, ללא אותו תו פעמיים ברצף";

  private readonly IAuthService _authService;
  private readonly IPasswordService _passwordService;
  private readonly IEmailService _emailService;
  private readonly IAuditLogService _auditLog;
  private readonly AppDbContext _db;

  public AccountController(
    IAuthService authService,
    IPasswordService passwordService,
    IEmailService emailService,
    IAuditLogService auditLog,
    AppDbContext db)
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
    if (User.Identity?.IsAuthenticated == true)
      return RedirectToAction("Index", "Home");
    ViewBag.ReturnUrl = returnUrl;
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(LoginDto model, string? returnUrl = null)
  {
    if (!ModelState.IsValid) return View(model);

    var (success, error, user) = await _authService.ValidateLoginAsync(model.IdNumber, model.Password);
    if (!success || user == null)
    {
      ModelState.AddModelError(string.Empty, error ?? "שגיאת כניסה");
      return View(model);
    }

    if (await IsTfaEnabledAsync())
    {
      if (string.IsNullOrWhiteSpace(user.Email))
      {
        ModelState.AddModelError(string.Empty, "לא ניתן לבצע אימות דו-שלבי: לא מוגדר מייל למשתמש");
        return View(model);
      }

      await SendTwoFactorCodeAsync(user);
      TempData["PendingTfaUserId"] = user.Id.ToString();
      TempData["PendingTfaRememberMe"] = model.RememberMe ? "true" : "false";
      TempData["PendingTfaReturnUrl"] = returnUrl ?? string.Empty;
      return RedirectToAction(nameof(TwoFactor));
    }

    await SignInAndRedirectAsync(user, model.RememberMe, returnUrl);
    return await PostLoginRedirectAsync(user, returnUrl);
  }

  [HttpGet]
  public IActionResult TwoFactor()
  {
    if (TempData.Peek("PendingTfaUserId") == null)
      return RedirectToAction(nameof(Login));
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> TwoFactor(string code)
  {
    var userIdRaw = TempData.Peek("PendingTfaUserId")?.ToString();
    if (!int.TryParse(userIdRaw, out var userId))
      return RedirectToAction(nameof(Login));

    var codeHash = HashValue(code?.Trim() ?? string.Empty);
    var entry = await _db.TwoFactorCodes
      .Where(c => c.UserId == userId &&
                  c.CodeHash == codeHash &&
                  c.UsedAt == null &&
                  c.ExpiresAt >= DateTime.UtcNow)
      .OrderByDescending(c => c.CreatedAt)
      .FirstOrDefaultAsync();

    if (entry == null)
    {
      ModelState.AddModelError(string.Empty, "קוד האימות שגוי או פג תוקף");
      return View();
    }

    entry.UsedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    var user = await _db.Users.FindAsync(userId);
    if (user == null) return RedirectToAction(nameof(Login));

    var rememberMe = TempData.Peek("PendingTfaRememberMe")?.ToString() == "true";
    var returnUrl = TempData.Peek("PendingTfaReturnUrl")?.ToString();
    TempData.Remove("PendingTfaUserId");
    TempData.Remove("PendingTfaRememberMe");
    TempData.Remove("PendingTfaReturnUrl");

    await SignInAndRedirectAsync(user, rememberMe, returnUrl);
    return await PostLoginRedirectAsync(user, returnUrl);
  }

  [Authorize]
  [HttpGet]
  public async Task<IActionResult> TermsOfUse(string? returnUrl = null)
  {
    var latest = await _db.TermsOfUseVersions
      .OrderByDescending(v => v.VersionNumber)
      .FirstOrDefaultAsync();

    ViewBag.LatestVersion = latest;
    ViewBag.ReturnUrl = (Url.IsLocalUrl(returnUrl) ? returnUrl : null);
    return View(latest);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> AcceptTerms(string? returnUrl = null)
  {
    var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    var user = await _db.Users.FindAsync(userId);
    if (user == null) return RedirectToAction("Index", "Home");

    var latest = await _db.TermsOfUseVersions
      .OrderByDescending(v => v.VersionNumber)
      .FirstOrDefaultAsync();

    if (latest != null)
    {
      var already = await _db.TermsOfUseAcceptances
        .AnyAsync(a => a.UserId == userId && a.VersionId == latest.Id);
      if (!already)
      {
        _db.TermsOfUseAcceptances.Add(new TermsOfUseAcceptance
        {
          UserId = userId,
          VersionId = latest.Id,
          AcceptedAt = DateTime.UtcNow,
          IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
        });
      }
      user.AcceptedTermsOfUse = true;
      user.UpdatedAt = DateTime.UtcNow;
      await _db.SaveChangesAsync();

      if (!already)
        await _auditLog.LogAsync("Terms.Accept", nameof(TermsOfUseVersion), latest.Id.ToString(),
          after: new { versionId = latest.Id, latest.VersionNumber, userId });
    }

    if (user.MustChangePassword || _passwordService.IsPasswordExpired(user.LastPasswordChange))
      return RedirectToAction(nameof(ChangePassword), new { forced = true });

    if (Url.IsLocalUrl(returnUrl))
      return Redirect(returnUrl!);

    return RedirectToAction("Index", "Home");
  }

  [Authorize]
  [HttpGet]
  public IActionResult ChangePassword(bool forced = false)
  {
    ViewBag.Forced = forced;
    return View();
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ChangePassword(ChangePasswordDto model, bool forced = false)
  {
    if (!ModelState.IsValid)
    {
      ViewBag.Forced = forced;
      return View(model);
    }

    if (model.NewPassword != model.ConfirmPassword)
    {
      ModelState.AddModelError(nameof(model.ConfirmPassword), "הסיסמאות אינן תואמות");
      ViewBag.Forced = forced;
      return View(model);
    }

    if (!_passwordService.IsPasswordStrong(model.NewPassword))
    {
      ModelState.AddModelError(nameof(model.NewPassword), StrongPasswordMessageHebrew);
      ViewBag.Forced = forced;
      return View(model);
    }

    var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    var user = await _db.Users.FindAsync(userId);
    if (user == null)
      return RedirectToAction(nameof(Login));

    if (!_passwordService.VerifyPassword(model.CurrentPassword, user.PasswordHash))
    {
      ModelState.AddModelError(nameof(model.CurrentPassword), "הסיסמה הנוכחית שגויה");
      ViewBag.Forced = forced;
      return View(model);
    }

    if (_passwordService.VerifyPassword(model.NewPassword, user.PasswordHash))
    {
      ModelState.AddModelError(nameof(model.NewPassword), "הסיסמה החדשה חייבת להיות שונה מהסיסמה הנוכחית.");
      ViewBag.Forced = forced;
      return View(model);
    }

    if (await _authService.IsPasswordInHistoryAsync(userId, model.NewPassword))
    {
      ModelState.AddModelError(nameof(model.NewPassword), "לא ניתן להשתמש בסיסמה זהה או דומה מדי לאחת מ-5 הסיסמאות האחרונות");
      ViewBag.Forced = forced;
      return View(model);
    }

    var changed = await _authService.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword);
    if (!changed)
    {
      ModelState.AddModelError(nameof(model.CurrentPassword), "הסיסמה הנוכחית שגויה");
      ViewBag.Forced = forced;
      return View(model);
    }

    TempData["Success"] = "הסיסמה שונתה בהצלחה";
    return RedirectToAction("Index", "Home");
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout()
  {
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction(nameof(Login));
  }

  [HttpGet]
  public IActionResult ForgotPassword() => View();

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ForgotPassword(string idNumber)
  {
    // Identical user-facing message regardless of whether the user exists -
    // do not leak account-enumeration through ForgotPassword timing or response.
    const string GenericConfirmation =
      "אם נמצאה כתובת מייל למשתמש, נשלח קישור לאיפוס סיסמה";

    var user = string.IsNullOrWhiteSpace(idNumber)
      ? null
      : await _db.Users.FirstOrDefaultAsync(u => u.IdNumber == idNumber);

    if (user != null && !string.IsNullOrWhiteSpace(user.Email))
    {
      var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48))
        .Replace("+", "-").Replace("/", "_").TrimEnd('=');
      _db.PasswordResetTokens.Add(new PasswordResetToken
      {
        UserId = user.Id,
        TokenHash = HashValue(token),
        ExpiresAt = DateTime.UtcNow.AddMinutes(PasswordResetMinutes),
        CreatedAt = DateTime.UtcNow
      });
      await _db.SaveChangesAsync();

      var resetLink = Url.Action(nameof(ResetPassword), "Account", new { token }, Request.Scheme) ?? string.Empty;
      // NotificationDispatcher writes a NotificationLog row with the SMTP outcome.
      await _emailService.SendAsync(
        user.Email,
        $"{user.FirstName} {user.LastName}",
        "PasswordReset",
        new Dictionary<string, string>
        {
          ["EmployeeName"] = $"{user.FirstName} {user.LastName}",
          ["ResetLink"] = resetLink,
          ["UserId"] = user.Id.ToString()
        });
    }
    else
    {
      // Unknown user OR user with no email - record a Skipped audit row but never send mail.
      // Status='Skipped' is recognized by the retry service as a terminal state.
      _db.NotificationLogs.Add(new NotificationLog
      {
        NotificationType = "Account",
        TemplateType = "PasswordReset",
        RecipientUserId = user?.Id,
        RecipientEmail = user?.Email ?? string.Empty,
        Subject = "(Skipped) ForgotPassword",
        Body = $"ForgotPassword requested for IdNumber='{idNumber}' - {(user == null ? "no such user" : "user has no email on file")}",
        Status = "Skipped",
        AttemptCount = 0,
        CreatedAt = DateTime.UtcNow
      });
      await _db.SaveChangesAsync();
    }

    TempData["Success"] = GenericConfirmation;
    return RedirectToAction(nameof(Login));
  }

  [HttpGet]
  public async Task<IActionResult> ResetPassword(string token)
  {
    var tokenHash = HashValue(token ?? string.Empty);
    var reset = await _db.PasswordResetTokens
      .FirstOrDefaultAsync(t => t.TokenHash == tokenHash && t.UsedAt == null && t.ExpiresAt >= DateTime.UtcNow);
    if (reset == null)
    {
      TempData["Error"] = "קישור איפוס הסיסמה אינו תקין או פג תוקף";
      return RedirectToAction(nameof(Login));
    }

    ViewBag.Token = token;
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ResetPassword(string token, string newPassword, string confirmPassword)
  {
    if (newPassword != confirmPassword)
      ModelState.AddModelError(nameof(confirmPassword), "הסיסמאות אינן תואמות");
    if (!_passwordService.IsPasswordStrong(newPassword))
      ModelState.AddModelError(nameof(newPassword), StrongPasswordMessageHebrew);

    var tokenHash = HashValue(token ?? string.Empty);
    var reset = await _db.PasswordResetTokens
      .FirstOrDefaultAsync(t => t.TokenHash == tokenHash && t.UsedAt == null && t.ExpiresAt >= DateTime.UtcNow);
    if (reset == null)
      ModelState.AddModelError(string.Empty, "קישור איפוס הסיסמה אינו תקין או פג תוקף");

    if (!ModelState.IsValid)
    {
      ViewBag.Token = token;
      return View();
    }

    var user = await _db.Users.FindAsync(reset!.UserId);
    if (user == null) return RedirectToAction(nameof(Login));

    if (_passwordService.VerifyPassword(newPassword, user.PasswordHash))
    {
      ModelState.AddModelError(nameof(newPassword), "הסיסמה החדשה חייבת להיות שונה מהסיסמה הנוכחית.");
      ViewBag.Token = token;
      return View();
    }

    if (await _authService.IsPasswordInHistoryAsync(user.Id, newPassword))
    {
      ModelState.AddModelError(nameof(newPassword), "לא ניתן להשתמש בסיסמה זהה או דומה מדי לאחת מ-5 הסיסמאות האחרונות");
      ViewBag.Token = token;
      return View();
    }

    await _authService.AddPasswordToHistoryAsync(user.Id, user.PasswordHash);
    user.PasswordHash = _passwordService.HashPassword(newPassword);
    user.MustChangePassword = false;
    user.LastPasswordChange = DateTime.UtcNow;
    user.UpdatedAt = DateTime.UtcNow;
    reset.UsedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    await _auditLog.LogAsync("Auth.PasswordReset", "User", user.Id.ToString(), null, null,
      $"ip={HttpContext.Connection.RemoteIpAddress}");

    TempData["Success"] = "הסיסמה אופסה בהצלחה";
    return RedirectToAction(nameof(Login));
  }

  [HttpGet]
  public IActionResult AccessDenied() => View();

  private async Task SendTwoFactorCodeAsync(User user)
  {
    var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
    _db.TwoFactorCodes.Add(new TwoFactorCode
    {
      UserId = user.Id,
      CodeHash = HashValue(code),
      ExpiresAt = DateTime.UtcNow.AddMinutes(TwoFactorMinutes),
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    await _emailService.SendAsync(
      user.Email!,
      $"{user.FirstName} {user.LastName}",
      "TwoFactorCode",
      new Dictionary<string, string>
      {
        ["EmployeeName"] = $"{user.FirstName} {user.LastName}",
        ["Code"] = code,
        ["Minutes"] = TwoFactorMinutes.ToString()
      });
  }

  private async Task<bool> IsTfaEnabledAsync()
  {
    var value = await _db.SystemConstants
      .Where(c => c.Key == "TfaEmailEnabled")
      .Select(c => c.Value)
      .FirstOrDefaultAsync();
    return bool.TryParse(value, out var enabled) && enabled;
  }

  private async Task SignInAndRedirectAsync(User user, bool rememberMe, string? returnUrl)
  {
    var claims = new List<Claim>
    {
      new(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new(ClaimTypes.Name, user.IdNumber),
      new("FullName", $"{user.FirstName} {user.LastName}"),
      new(ClaimTypes.Role, user.UserRoleId.ToString()),
      new("UserRoleName", ((UserRoleEnum)user.UserRoleId).ToString())
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);
    var authProps = new AuthenticationProperties
    {
      IsPersistent = rememberMe,
      ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
    };

    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProps);
  }

  private async Task<IActionResult> PostLoginRedirectAsync(User user, string? returnUrl)
  {
    var latestVersionId = await _db.TermsOfUseVersions
      .OrderByDescending(v => v.VersionNumber)
      .Select(v => (int?)v.Id)
      .FirstOrDefaultAsync();

    if (latestVersionId != null)
    {
      var acceptedLatest = await _db.TermsOfUseAcceptances
        .AnyAsync(a => a.UserId == user.Id && a.VersionId == latestVersionId);
      if (!acceptedLatest)
        return RedirectToAction(nameof(TermsOfUse));
    }
    else if (!user.AcceptedTermsOfUse)
    {
      return RedirectToAction(nameof(TermsOfUse));
    }

    if (user.MustChangePassword || _passwordService.IsPasswordExpired(user.LastPasswordChange))
      return RedirectToAction(nameof(ChangePassword), new { forced = true });

    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
      return Redirect(returnUrl);

    return RedirectToAction("Index", "Home");
  }

  private static string HashValue(string value)
  {
    var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
    return Convert.ToHexString(bytes);
  }
}
