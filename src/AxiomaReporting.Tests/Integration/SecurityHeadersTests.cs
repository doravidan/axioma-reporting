using System.Net;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;

namespace AxiomaReporting.Tests.Integration;

public class SecurityHeadersTests
{
  [Fact]
  public async Task PublicResponse_HasSecurityHeadersAndAccountNoStore()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var response = await client.GetAsync("/Account/Login");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.Headers.GetValues("Content-Security-Policy").Single().Should().Contain("frame-ancestors 'none'");
    response.Headers.GetValues("X-Content-Type-Options").Single().Should().Be("nosniff");
    response.Headers.GetValues("Referrer-Policy").Single().Should().Be("no-referrer");
    response.Headers.GetValues("Permissions-Policy").Single().Should().Contain("camera=()");
    response.Headers.CacheControl?.NoStore.Should().BeTrue();
  }

  [Fact]
  public async Task LoginPage_UsesOnlyCspCompatibleLocalStylesheets()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var html = await client.GetStringAsync("/Account/Login");

    html.Should().Contain("/lib/bootstrap/dist/css/bootstrap.rtl.min.css");
    html.Should().NotContain("cdn.jsdelivr.net");
    html.Should().NotContain("fonts.googleapis.com");
  }

  [Theory]
  [InlineData("/uploads/attachments/example.pdf")]
  [InlineData("/uploads/employees/example.pdf")]
  [InlineData("/uploads/private/report-attachments/example.pdf")]
  [InlineData("/uploads/excel-errors/example.pdf")]
  public async Task SensitiveUploadDirectories_AreNotPubliclyServed(string path)
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var response = await client.GetAsync(path);

    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
  }
}
