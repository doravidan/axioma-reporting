using System.Net;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Tests.Integration;

public class AccountFlowTests
{
  [Fact]
  public async Task Login_WithValidCredentials_SignsInAndShowsLogoutButton()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var loginHtml = await client.GetStringAsync("/Account/Login");
    var response = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.EmployeeIdNumber,
      ["Password"] = TestData.EmployeePassword,
      ["RememberMe"] = "false"
    }));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var body = await response.Content.ReadAsStringAsync();
    body.Should().Contain("התנתק");
    body.Should().Contain("Test Employee");
  }

  [Fact]
  public async Task Login_WithThreeBadPasswords_LocksUser()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    for (var i = 0; i < 3; i++)
    {
      var html = await client.GetStringAsync("/Account/Login");
      await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(html),
        ["IdNumber"] = TestData.EmployeeIdNumber,
        ["Password"] = "WrongPassword1"
      }));
    }

    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var user = db.Users.Single(u => u.IdNumber == TestData.EmployeeIdNumber);
    user.StatusId.Should().Be((int)UserStatusEnum.Locked);
    user.FailedLoginAttempts.Should().Be(3);
  }

  [Fact]
  public async Task ForgotPassword_WithKnownEmail_CreatesTokenAndSendsResetEmail()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var forgotHtml = await client.GetStringAsync("/Account/ForgotPassword");
    var response = await client.PostAsync("/Account/ForgotPassword", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(forgotHtml),
      ["idNumber"] = TestData.EmployeeIdNumber
    }));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    factory.EmailService.Sent.Should().ContainSingle(e => e.TemplateType == "PasswordReset");
    factory.EmailService.Sent.Single().Tokens.Should().ContainKey("ResetLink");

    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.PasswordResetTokens.Should().ContainSingle(t => t.UserId == 1 && t.UsedAt == null);
  }

  [Fact]
  public async Task Login_WhenEmailTfaEnabled_SendsEmailCodeAndShowsTwoFactorPage()
  {
    await using var factory = new CustomWebApplicationFactory(tfaEnabled: true);
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var loginHtml = await client.GetStringAsync("/Account/Login");
    var response = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.EmployeeIdNumber,
      ["Password"] = TestData.EmployeePassword
    }));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var body = await response.Content.ReadAsStringAsync();
    body.Should().Contain("אימות דו-שלבי");
    factory.EmailService.Sent.Should().ContainSingle(e => e.TemplateType == "TwoFactorCode");

    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.TwoFactorCodes.Should().ContainSingle(c => c.UserId == 1 && c.UsedAt == null);
  }
}
