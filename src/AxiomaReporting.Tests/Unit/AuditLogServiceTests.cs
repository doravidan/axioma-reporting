using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace AxiomaReporting.Tests.Unit;

public class AuditLogServiceTests : IDisposable
{
  private readonly AppDbContext _db;

  public AuditLogServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task AuditLogService_LogAsync_WritesRow_WithActorAndSerializedPayload()
  {
    var ctxAccessor = new HttpContextAccessor
    {
      HttpContext = new DefaultHttpContext()
    };
    ctxAccessor.HttpContext!.Request.Headers["User-Agent"] = "xunit";

    var sut = new AuditLogService(
      _db,
      new FakeCurrentUserService(userId: 42),
      ctxAccessor,
      NullLogger<AuditLogService>.Instance);

    await sut.LogAsync(
      action: "Employee.Create",
      entityType: "User",
      entityId: "99",
      before: null,
      after: new { Id = 99, Name = "Alice", Secret = (string?)null },
      notes: "created by admin");

    var rows = await _db.AuditLogs.ToListAsync();
    rows.Should().ContainSingle();
    var row = rows[0];
    row.Action.Should().Be("Employee.Create");
    row.EntityType.Should().Be("User");
    row.EntityId.Should().Be("99");
    row.ActorUserId.Should().Be(42);
    row.UserAgent.Should().Be("xunit");
    row.Notes.Should().Be("created by admin");
    row.Before.Should().BeNull();
    row.After.Should().NotBeNull();
    row.After!.Should().Contain("\"id\":99");
    row.After.Should().Contain("\"name\":\"Alice\"");
    // WhenWritingNull skips Secret
    row.After.Should().NotContain("secret");
  }

  [Fact]
  public async Task AuditLogService_DbFailure_DoesNotThrow()
  {
    _db.Dispose(); // force a broken DbContext so SaveChangesAsync throws

    var sut = new AuditLogService(
      _db,
      new FakeCurrentUserService(userId: 1),
      new HttpContextAccessor(),
      NullLogger<AuditLogService>.Instance);

    var act = async () => await sut.LogAsync("X", "Y", "1");
    await act.Should().NotThrowAsync();
  }

  [Fact]
  public async Task AuditLogService_LogAsync_UnauthenticatedActor_StoresNullUserId()
  {
    var sut = new AuditLogService(
      _db,
      new FakeCurrentUserService(userId: 0, authenticated: false),
      new HttpContextAccessor(),
      NullLogger<AuditLogService>.Instance);

    await sut.LogAsync("Auth.LoginFailed", "User", "unknown");

    var row = await _db.AuditLogs.SingleAsync();
    row.ActorUserId.Should().BeNull();
  }

  private sealed class FakeCurrentUserService : ICurrentUserService
  {
    public FakeCurrentUserService(int userId, bool authenticated = true)
    {
      UserId = userId;
      IsAuthenticated = authenticated;
    }
    public int UserId { get; }
    public string IdNumber => "TEST";
    public string FullName => "Test User";
    public UserRoleEnum UserRole => UserRoleEnum.SystemAdmin;
    public bool IsAuthenticated { get; }
  }
}
