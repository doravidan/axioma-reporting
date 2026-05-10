using System.Net;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Tests.Integration;

public class TermsOfUseFlowTests
{
  [Fact]
  public async Task Login_UserWithoutAcceptance_IsRedirectedToTermsOfUse()
  {
    // Version exists but no acceptance rows — every user must accept.
    await using var factory = new CustomWebApplicationFactory(seedTermsVersion: true, acceptLatestTerms: false);
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost"), AllowAutoRedirect = false });

    var loginHtml = await client.GetStringAsync("/Account/Login");
    var response = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.EmployeeIdNumber,
      ["Password"] = TestData.EmployeePassword
    }));

    response.StatusCode.Should().Be(HttpStatusCode.Redirect);
    response.Headers.Location!.ToString().Should().Contain("/Account/TermsOfUse");
  }

  [Fact]
  public async Task Login_UserAcceptedOlderVersion_WhenNewerPublished_IsRedirectedToTerms()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost"), AllowAutoRedirect = false });

    // Publish a new (higher) version. Existing acceptance is for version #1 only.
    using (var scope = factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      db.TermsOfUseVersions.Add(new TermsOfUseVersion
      {
        VersionNumber = 2,
        BodyHtml = "<p>גרסה חדשה</p>",
        EffectiveFrom = DateTime.UtcNow,
        PublishedByUserId = 2,
        CreatedAt = DateTime.UtcNow
      });
      await db.SaveChangesAsync();
    }

    var loginHtml = await client.GetStringAsync("/Account/Login");
    var response = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.EmployeeIdNumber,
      ["Password"] = TestData.EmployeePassword
    }));

    response.StatusCode.Should().Be(HttpStatusCode.Redirect);
    response.Headers.Location!.ToString().Should().Contain("/Account/TermsOfUse");
  }

  [Fact]
  public async Task Login_UserAcceptedLatestVersion_ProceedsToHome()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost"), AllowAutoRedirect = false });

    var loginHtml = await client.GetStringAsync("/Account/Login");
    var response = await client.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.EmployeeIdNumber,
      ["Password"] = TestData.EmployeePassword
    }));

    response.StatusCode.Should().Be(HttpStatusCode.Redirect);
    var location = response.Headers.Location!.ToString();
    location.Should().NotContain("/Account/TermsOfUse");
    location.Should().NotContain("/Account/Login");
  }

  [Fact]
  public async Task PublishTermsOfUse_ResetsAcceptedFlagForAllUsers()
  {
    await using var factory = new CustomWebApplicationFactory();
    var adminClient = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    // Sign in as admin.
    var loginHtml = await adminClient.GetStringAsync("/Account/Login");
    await adminClient.PostAsync("/Account/Login", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
      ["IdNumber"] = TestData.AdminIdNumber,
      ["Password"] = TestData.AdminPassword
    }));

    // Publish a new version.
    var termsPage = await adminClient.GetStringAsync("/Admin/TermsOfUse");
    var publishResponse = await adminClient.PostAsync("/Admin/PublishTermsOfUse", new FormUrlEncodedContent(new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(termsPage),
      ["bodyHtml"] = "<p>גרסה חדשה לבדיקה</p>",
      ["effectiveFrom"] = DateTime.Today.ToString("yyyy-MM-dd")
    }));

    publishResponse.IsSuccessStatusCode.Should().BeTrue();

    using var scope = factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // All users are flipped back to AcceptedTermsOfUse = false.
    var users = await db.Users.AsNoTracking().ToListAsync();
    users.Should().NotBeEmpty();
    users.Should().OnlyContain(u => u.AcceptedTermsOfUse == false);

    // A new version row was added.
    var maxVersion = await db.TermsOfUseVersions.MaxAsync(v => v.VersionNumber);
    maxVersion.Should().Be(2);
  }
}
