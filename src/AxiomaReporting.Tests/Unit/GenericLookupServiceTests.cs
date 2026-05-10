using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class GenericLookupServiceTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly GenericLookupService<District> _sut;

  public GenericLookupServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _sut = new GenericLookupService<District>(_db);
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task CreateAndGetAllAsync_ReturnsOnlyActiveSortedRowsByDefault()
  {
    await _sut.CreateAsync("Beta");
    await _sut.CreateAsync("Alpha");
    _db.Districts.Add(new District { Description = "Inactive", IsActive = false, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var active = await _sut.GetAllAsync();
    var all = await _sut.GetAllAsync(includeInactive: true);

    active.Select(x => x.Description).Should().Equal("Alpha", "Beta");
    all.Select(x => x.Description).Should().Equal("Alpha", "Beta", "Inactive");
  }

  [Fact]
  public async Task UpdateAndDeleteAsync_ModifiesExistingRowsAndIgnoresMissingRows()
  {
    var created = await _sut.CreateAsync("Old");

    (await _sut.UpdateAsync(created.Id, "New", false)).Should().BeTrue();
    (await _sut.UpdateAsync(999, "Missing", true)).Should().BeFalse();
    (await _sut.GetByIdAsync(created.Id))!.Description.Should().Be("New");
    (await _sut.GetByIdAsync(created.Id))!.IsActive.Should().BeFalse();

    await _sut.DeleteAsync(created.Id);
    await _sut.DeleteAsync(999);

    (await _sut.GetByIdAsync(created.Id)).Should().BeNull();
  }

  [Fact]
  public async Task CanDeleteAsync_DefaultsToAllowed()
  {
    var result = await _sut.CanDeleteAsync(1);

    result.CanDelete.Should().BeTrue();
    result.Reason.Should().BeNull();
  }
}
