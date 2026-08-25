using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.DataProtection;
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
  private readonly IReadOnlyDictionary<string, string?> _settings;
  private readonly InMemoryDatabaseRoot _databaseRoot = new();
  private readonly string _databaseName = $"AxiomaReportingTests-{Guid.NewGuid()}";

  public FakeEmailService EmailService { get; } = new();

  public CustomWebApplicationFactory(
    bool tfaEnabled = false,
    bool seedTermsVersion = true,
    bool acceptLatestTerms = true,
    IReadOnlyDictionary<string, string?>? settings = null)
  {
    _tfaEnabled = tfaEnabled;
    _seedTermsVersion = seedTermsVersion;
    _acceptLatestTerms = acceptLatestTerms;
    _settings = settings ?? new Dictionary<string, string?>();
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.UseEnvironment("Testing");
    builder.UseSetting("AXIOMA_TEST_INMEMORY", "true");
    foreach (var setting in _settings)
      builder.UseSetting(setting.Key, setting.Value);
    builder.ConfigureLogging(logging => logging.ClearProviders());
    builder.ConfigureServices(services =>
    {
      services.RemoveAll<DbContextOptions<AppDbContext>>();
      services.RemoveAll<IHostedService>();
      services.RemoveAll<IEmailService>();

      // Test hosts must never read or persist the IIS/production key ring.
      services.AddDataProtection().UseEphemeralDataProtectionProvider();

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
