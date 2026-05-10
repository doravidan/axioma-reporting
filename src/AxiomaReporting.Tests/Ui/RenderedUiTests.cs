using System.Net;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;

namespace AxiomaReporting.Tests.Ui;

public class RenderedUiTests
{
  [Fact]
  public async Task LoginPage_RendersHebrewRtlFormAndForgotPasswordLink()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var html = await client.GetStringAsync("/Account/Login");

    html.Should().Contain("dir=\"rtl\"");
    html.Should().Contain("תעודת זהות");
    html.Should().Contain("סיסמה");
    html.Should().Contain("שכחתי סיסמה");
    html.Should().Contain("__RequestVerificationToken");
  }

  [Fact]
  public async Task ForgotPasswordPage_RendersUsableForm()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var html = await client.GetStringAsync("/Account/ForgotPassword");

    html.Should().Contain("שחזור סיסמה");
    html.Should().Contain("name=\"idNumber\"");
    html.Should().Contain("שלח קישור");
  }

  [Fact]
  public async Task ProtectedReportPage_RedirectsAnonymousUserToLogin()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new()
    {
      BaseAddress = new Uri("https://localhost"),
      AllowAutoRedirect = false
    });

    var response = await client.GetAsync("/Report");

    response.StatusCode.Should().Be(HttpStatusCode.Redirect);
    response.Headers.Location!.ToString().Should().Contain("/Account/Login");
  }
}
