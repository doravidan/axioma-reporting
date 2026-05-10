using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Tests.TestSupport;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
  private readonly bool _tfaEnabled;
  private readonly bool _seedTermsVersion;
  private readonly bool _acceptLatestTerms;
  private readonly InMemoryDatabaseRoot _databaseRoot = new();
  private readonly string _databaseName = $"AxiomaReportingTests-{Guid.NewGuid()}";

  public FakeEmailService EmailService { get; } = new();

  public CustomWebApplicationFactory(bool tfaEnabled = false, bool seedTermsVersion = true, bool acceptLatestTerms = true)
  {
    _tfaEnabled = tfaEnabled;
    _seedTermsVersion = seedTermsVersion;
    _acceptLatestTerms = acceptLatestTerms;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.UseEnvironment("Testing");
    builder.ConfigureLogging(logging => logging.ClearProviders());
    builder.ConfigureServices(services =>
    {
      services.RemoveAll<DbContextOptions<AppDbContext>>();
      services.RemoveAll<IHostedService>();
      services.RemoveAll<IEmailService>();

      services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase(_databaseName, _databaseRoot)
          .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
          .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));
      services.AddSingleton<IEmailService>(EmailService);

      using var provider = services.BuildServiceProvider();
      using var scope = provider.CreateScope();
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      db.Database.EnsureDeleted();
      TestData.SeedIdentity(db, _tfaEnabled, _seedTermsVersion, _acceptLatestTerms);
    });
  }
}
