using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Round-trip tests for <see cref="BrandingService"/> covering:
///  - default fallback when the <c>SiteLogoPath</c> row is missing,
///  - reading an existing value,
///  - setting a new value and observing it on a fresh instance (persistence),
///  - instance-level caching preventing duplicate DB lookups.
/// </summary>
public class BrandingServiceTests : IDisposable
{
  private readonly AppDbContext _db;

  public BrandingServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task GetLogoPathAsync_WhenConstantMissing_ReturnsDefault()
  {
    var sut = new BrandingService(_db);

    var result = await sut.GetLogoPathAsync();

    result.Should().Be(IBrandingService.DefaultLogoPath);
  }

  [Fact]
  public async Task GetLogoPathAsync_WhenConstantEmpty_ReturnsDefault()
  {
    _db.SystemConstants.Add(new SystemConstant
    {
      Key = IBrandingService.LogoPathConstantKey,
      Value = "   ",
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();
    var sut = new BrandingService(_db);

    var result = await sut.GetLogoPathAsync();

    result.Should().Be(IBrandingService.DefaultLogoPath);
  }

  [Fact]
  public async Task GetLogoPathAsync_ReturnsStoredValue()
  {
    _db.SystemConstants.Add(new SystemConstant
    {
      Key = IBrandingService.LogoPathConstantKey,
      Value = "/uploads/branding/site-logo.svg",
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();
    var sut = new BrandingService(_db);

    var result = await sut.GetLogoPathAsync();

    result.Should().Be("/uploads/branding/site-logo.svg");
  }

  [Fact]
  public async Task SetLogoPathAsync_CreatesConstantWhenMissing_AndRoundTrips()
  {
    var writer = new BrandingService(_db);

    await writer.SetLogoPathAsync("/uploads/branding/site-logo.png", updatedByUserId: 42);

    // Fresh instance, shared context: the stored value is readable and cached path matches.
    var reader = new BrandingService(_db);
    (await reader.GetLogoPathAsync()).Should().Be("/uploads/branding/site-logo.png");

    var row = await _db.SystemConstants.AsNoTracking()
      .FirstAsync(c => c.Key == IBrandingService.LogoPathConstantKey);
    row.Value.Should().Be("/uploads/branding/site-logo.png");
    row.UpdatedBy.Should().Be(42);
  }

  [Fact]
  public async Task SetLogoPathAsync_UpdatesExistingConstant()
  {
    _db.SystemConstants.Add(new SystemConstant
    {
      Key = IBrandingService.LogoPathConstantKey,
      Value = "/images/logo.png",
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();
    var sut = new BrandingService(_db);

    await sut.SetLogoPathAsync("/uploads/branding/site-logo.jpg", updatedByUserId: null);

    var row = await _db.SystemConstants.AsNoTracking()
      .FirstAsync(c => c.Key == IBrandingService.LogoPathConstantKey);
    row.Value.Should().Be("/uploads/branding/site-logo.jpg");
    row.UpdatedAt.Should().NotBeNull();
  }

  [Fact]
  public async Task SetLogoPathAsync_RejectsEmpty()
  {
    var sut = new BrandingService(_db);

    Func<Task> act = () => sut.SetLogoPathAsync("  ", updatedByUserId: null);

    await act.Should().ThrowAsync<ArgumentException>();
  }

  [Fact]
  public async Task GetLogoPathAsync_CachesOnInstance()
  {
    _db.SystemConstants.Add(new SystemConstant
    {
      Key = IBrandingService.LogoPathConstantKey,
      Value = "/images/first.png",
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();
    var sut = new BrandingService(_db);

    var first = await sut.GetLogoPathAsync();

    // Mutate the underlying row directly; the same instance must keep the cached value.
    var row = await _db.SystemConstants.FirstAsync(c => c.Key == IBrandingService.LogoPathConstantKey);
    row.Value = "/images/second.png";
    await _db.SaveChangesAsync();

    var second = await sut.GetLogoPathAsync();

    first.Should().Be("/images/first.png");
    second.Should().Be("/images/first.png"); // cached
  }
}
