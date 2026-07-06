using System.Net;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Tests.Integration;

/// <summary>
/// Coverage for the auth-engineer mandate (v1.1):
///  - ForgotPassword writes a NotificationLog row in both branches (existing user + missing user)
///    and never leaks account-enumeration through the user-facing message.
///  - First-launch admin (AcceptedTermsOfUse=false, no acceptance row) is forced through Terms.
///  - Admin-initiated EmployeeController.ResetPassword sets MustChangePassword=true,
///    pushes the previous hash into PasswordHistory, clears FailedLoginAttempts, and writes
///    an AuditLog row with Action="User.PasswordReset" and Notes containing reset-by=...
/// </summary>
public class AuthEngineerFlowTests
{
  // ────────────────────────────────────────────────────────────────────────
  // #20 — Forgot-password email
  // ────────────────────────────────────────────────────────────────────────

  [Fact]
  public async Task ForgotPassword_ExistingUser_CallsEmailServiceWithPasswordResetTemplate()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var html = await client.GetStringAsync("/Account/ForgotPassword");
    var response = await client.PostAsync("/Account/ForgotPassword", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(html),
      ["idNumber"] = TestData.EmployeeIdNumber
    }));

    response.IsSuccessStatusCode.Should().BeTrue();
    factory.EmailService.Sent
      .Should().ContainSingle(e => e.TemplateType == "PasswordReset")
      .Which.Tokens.Should().ContainKey("ResetLink");

    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.PasswordResetTokens.Should().ContainSingle(t => t.UserId == 1 && t.UsedAt == null);
  }

  [Fact]
  public async Task ForgotPassword_UnknownUser_DoesNotCallEmailServiceAndWritesSkippedNotificationLog()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var html = await client.GetStringAsync("/Account/ForgotPassword");
    var response = await client.PostAsync("/Account/ForgotPassword", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(html),
      ["idNumber"] = "000000000"
    }));

    response.IsSuccessStatusCode.Should().BeTrue();
    factory.EmailService.Sent.Should().BeEmpty();

    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // No reset token created.
    db.PasswordResetTokens.Should().BeEmpty();

    // A NotificationLog row was written so security audit can prove the request was handled.
    var logs = await db.NotificationLogs.AsNoTracking().ToListAsync();
    logs.Should().ContainSingle();
    logs[0].Status.Should().Be("Skipped");
    logs[0].TemplateType.Should().Be("PasswordReset");
  }

  [Fact]
  public async Task ForgotPassword_BothBranchesRedirectToLogin_NoEnumerationLeakInResponseStatus()
  {
    // The user-facing flow must look identical regardless of whether the IdNumber exists
    // in the database. Both branches return 302 → /Account/Login with the same TempData
    // success message; we verify the redirect target as the canonical equivalence check.
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new()
    {
      BaseAddress = new Uri("https://localhost"),
      AllowAutoRedirect = false
    });

    var html1 = await client.GetStringAsync("/Account/ForgotPassword");
    var resp1 = await client.PostAsync("/Account/ForgotPassword", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(html1),
      ["idNumber"] = TestData.EmployeeIdNumber
    }));

    var html2 = await client.GetStringAsync("/Account/ForgotPassword");
    var resp2 = await client.PostAsync("/Account/ForgotPassword", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(html2),
      ["idNumber"] = "000000000"
    }));

    resp1.StatusCode.Should().Be(HttpStatusCode.Redirect);
    resp2.StatusCode.Should().Be(HttpStatusCode.Redirect);
    resp1.Headers.Location!.ToString().Should().Be(resp2.Headers.Location!.ToString());
    resp1.Headers.Location!.ToString().Should().Contain("/Account/Login");
  }

  // ────────────────────────────────────────────────────────────────────────
  // #2 — Terms of Use forced on first launch
  // ────────────────────────────────────────────────────────────────────────

  [Fact]
  public async Task FreshAdmin_NavigatingToDashboard_IsRedirectedToTermsOfUse_ThenAfterAcceptanceLandsBack()
  {
    // seedTermsVersion=true, acceptLatestTerms=false → admin has not yet accepted v1.
    await using var factory = new CustomWebApplicationFactory(seedTermsVersion: true, acceptLatestTerms: false);
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost"), AllowAutoRedirect = false });

    // 1. Login as admin (no auto-redirect → we follow the chain manually).
    var loginHtml = await client.GetStringAsync("/Account/Login");
    var loginResponse = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.AdminIdNumber,
      ["Password"] = TestData.AdminPassword
    }));
    loginResponse.StatusCode.Should().Be(HttpStatusCode.Redirect);
    loginResponse.Headers.Location!.ToString().Should().Contain("/Account/TermsOfUse");

    // 2. Try to reach /Dashboard directly — RequireTermsAcceptedFilter should bounce us back.
    var dashboardResponse = await client.GetAsync("/Dashboard");
    dashboardResponse.StatusCode.Should().Be(HttpStatusCode.Redirect);
    dashboardResponse.Headers.Location!.ToString().Should().Contain("/Account/TermsOfUse");

    // 3. Accept the Terms.
    var termsHtml = await client.GetStringAsync("/Account/TermsOfUse");
    var acceptResponse = await client.PostAsync("/Account/AcceptTerms", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(termsHtml),
      ["returnUrl"] = "/Dashboard"
    }));
    acceptResponse.StatusCode.Should().Be(HttpStatusCode.Redirect);
    acceptResponse.Headers.Location!.ToString().Should().Contain("/Dashboard");

    // 4. Acceptance row is persisted.
    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var acceptances = await db.TermsOfUseAcceptances.AsNoTracking().ToListAsync();
    acceptances.Should().ContainSingle(a => a.UserId == 2 && a.VersionId == 1);

    // 5. /Dashboard now returns 200.
    var second = await client.GetAsync("/Dashboard");
    second.StatusCode.Should().Be(HttpStatusCode.OK);
  }

  [Fact]
  public async Task RequireTermsFilter_ChangePasswordIsExempt_EvenWhenTermsNotAccepted()
  {
    // Forced-change + no-terms-accepted should NOT loop the user between Terms and ChangePassword.
    await using var factory = new CustomWebApplicationFactory(seedTermsVersion: true, acceptLatestTerms: false);
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost"), AllowAutoRedirect = false });

    var loginHtml = await client.GetStringAsync("/Account/Login");
    await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.AdminIdNumber,
      ["Password"] = TestData.AdminPassword
    }));

    var changePwdResponse = await client.GetAsync("/Account/ChangePassword");
    // Must NOT redirect to /Account/TermsOfUse — the filter exempts ChangePassword.
    changePwdResponse.StatusCode.Should().NotBe(HttpStatusCode.Redirect);
  }

  // ────────────────────────────────────────────────────────────────────────
  // #19 — Admin-initiated password reset forces change on next login
  // ────────────────────────────────────────────────────────────────────────

  [Fact]
  public async Task AdminResetPassword_SetsMustChangePassword_PushesHistory_ClearsFailedAttempts_LogsAudit()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    // Pre-state: bump the target user's failed-login counter so we can assert it gets cleared.
    using (var setupScope = factory.Services.CreateScope())
    {
      var db = setupScope.ServiceProvider.GetRequiredService<AppDbContext>();
      var target = await db.Users.SingleAsync(u => u.IdNumber == TestData.EmployeeIdNumber);
      target.FailedLoginAttempts = 2;
      target.MustChangePassword = false;
      await db.SaveChangesAsync();
    }

    // Login as admin.
    var loginHtml = await client.GetStringAsync("/Account/Login");
    await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.AdminIdNumber,
      ["Password"] = TestData.AdminPassword
    }));

    // Get a page that has the antiforgery cookie + token for a POST to /Employee/ResetPassword/{id}.
    var listHtml = await client.GetStringAsync("/Employee");
    var resp = await client.PostAsync("/Employee/ResetPassword/1", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(listHtml)
    }));
    resp.IsSuccessStatusCode.Should().BeTrue();

    using var scope = factory.Services.CreateScope();
    var db2 = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var user = await db2.Users.AsNoTracking().SingleAsync(u => u.Id == 1);

    user.MustChangePassword.Should().BeTrue();
    user.FailedLoginAttempts.Should().Be(0);
    user.LastPasswordChange.Should().NotBeNull();

    // Password is now BCrypt(IdNumber).
    var pwd = new PasswordService();
    pwd.VerifyPassword(TestData.EmployeeIdNumber, user.PasswordHash).Should().BeTrue();

    // Previous hash is in PasswordHistory.
    var history = await db2.PasswordHistories.AsNoTracking()
      .Where(h => h.UserId == 1).ToListAsync();
    history.Should().NotBeEmpty();

    // AuditLog row written with the canonical Action and the reset-by note.
    var audit = await db2.AuditLogs.AsNoTracking()
      .Where(a => a.Action == "User.PasswordReset" && a.EntityId == "1")
      .OrderByDescending(a => a.Id).FirstOrDefaultAsync();
    audit.Should().NotBeNull();
    audit!.Notes.Should().Contain("reset-by=");
  }

  [Fact]
  public async Task SelfServiceResetPassword_DoesNotForceAnotherChange()
  {
    // Client QA (בדיקת פרויקט Sheet7 #8): after choosing a NEW password via the
    // emailed reset link, the user must NOT be forced to change it again on the
    // next login. (Admin-initiated resets to ת.ז DO force a change — separate path.)
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    // Stage a valid reset token for user 1.
    string tokenPlain;
    using (var scope = factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      tokenPlain = "self-service-token-" + Guid.NewGuid().ToString("N");
      var hashed = HashSha256(tokenPlain);
      db.PasswordResetTokens.Add(new PasswordResetToken
      {
        UserId = 1,
        TokenHash = hashed,
        ExpiresAt = DateTime.UtcNow.AddMinutes(30),
        CreatedAt = DateTime.UtcNow
      });
      await db.SaveChangesAsync();
    }

    var resetHtml = await client.GetStringAsync($"/Account/ResetPassword?token={tokenPlain}");
    var resp = await client.PostAsync("/Account/ResetPassword", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(resetHtml),
      ["token"] = tokenPlain,
      ["newPassword"] = "ResetA1!",
      ["confirmPassword"] = "ResetA1!"
    }));
    resp.IsSuccessStatusCode.Should().BeTrue();

    using var checkScope = factory.Services.CreateScope();
    var db2 = checkScope.ServiceProvider.GetRequiredService<AppDbContext>();
    var user = await db2.Users.AsNoTracking().SingleAsync(u => u.Id == 1);
    user.MustChangePassword.Should().BeFalse();

    var pwd = new PasswordService();
    pwd.VerifyPassword("ResetA1!", user.PasswordHash).Should().BeTrue();
  }

  [Fact]
  public async Task PendingForcedPasswordChange_CannotBeBypassedViaMenuNavigation()
  {
    // Client QA (בדיקת פרויקט Sheet6 #1): a user parked on the forced change-password
    // screen must not be able to reach any other page by clicking menu links.
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new()
    {
      BaseAddress = new Uri("https://localhost"),
      AllowAutoRedirect = false
    });

    // Sign in normally first (MustChangePassword=false at login time).
    var loginHtml = await client.GetStringAsync("/Account/Login");
    var login = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.EmployeeIdNumber,
      ["Password"] = TestData.EmployeePassword
    }));
    login.StatusCode.Should().Be(HttpStatusCode.Redirect);

    // Simulate an admin reset landing mid-session: the flag flips on the server.
    using (var scope = factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      var user = await db.Users.SingleAsync(u => u.Id == 1);
      user.MustChangePassword = true;
      await db.SaveChangesAsync();
    }

    // Any app page must now bounce to ChangePassword — no menu escape.
    var protectedPage = await client.GetAsync("/MyAllocations");
    protectedPage.StatusCode.Should().Be(HttpStatusCode.Redirect);
    protectedPage.Headers.Location!.ToString().Should().Contain("/Account/ChangePassword");

    // The change-password screen itself must stay reachable.
    var changePage = await client.GetAsync("/Account/ChangePassword");
    changePage.StatusCode.Should().Be(HttpStatusCode.OK);
  }

  private static string HashSha256(string value)
  {
    using var sha = System.Security.Cryptography.SHA256.Create();
    var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
    return Convert.ToHexString(bytes);
  }
}
