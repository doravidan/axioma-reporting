using System.Net;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;

namespace AxiomaReporting.Tests.Stress;

public class HttpStressTests
{
  [Fact]
  public async Task LoginPage_HandlesConcurrentRequestsWithoutServerErrors()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });

    var responses = await Task.WhenAll(Enumerable.Range(0, 50)
      .Select(_ => client.GetAsync("/Account/Login")));

    responses.Should().OnlyContain(r => r.StatusCode == HttpStatusCode.OK);
  }

  [Fact]
  public async Task PublicPages_StayHealthyUnderMixedConcurrentTraffic()
  {
    await using var factory = new CustomWebApplicationFactory();
    var client = factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });
    var urls = new[] { "/Account/Login", "/Account/ForgotPassword", "/Account/AccessDenied" };

    var responses = await Task.WhenAll(Enumerable.Range(0, 60)
      .Select(i => client.GetAsync(urls[i % urls.Length])));

    responses.Should().OnlyContain(r => (int)r.StatusCode < 500);
    responses.Count(r => r.StatusCode == HttpStatusCode.OK).Should().BeGreaterThan(0);
  }
}
