using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class AuthServiceTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly PasswordService _passwordService = new();
  private readonly AuthService _sut;

  public AuthServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _sut = new AuthService(_db, _passwordService);
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task ValidateLoginAsync_ValidCredentials_ResetsFailuresAndReturnsUser()
  {
    SeedLookups();
    var user = CreateUser(failedLoginAttempts: 2);
    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    var result = await _sut.ValidateLoginAsync("123456789", "Password123");

    result.Success.Should().BeTrue();
    result.User.Should().NotBeNull();
    (await _db.Users.FindAsync(user.Id))!.FailedLoginAttempts.Should().Be(0);
  }

  [Fact]
  public async Task ValidateLoginAsync_ThreeBadPasswords_LocksAccount()
  {
    SeedLookups();
    _db.Users.Add(CreateUser());
    await _db.SaveChangesAsync();

    await _sut.ValidateLoginAsync("123456789", "bad1");
    await _sut.ValidateLoginAsync("123456789", "bad2");
    var third = await _sut.ValidateLoginAsync("123456789", "bad3");

    third.Success.Should().BeFalse();
    var user = await _db.Users.SingleAsync();
    user.FailedLoginAttempts.Should().Be(3);
    user.StatusId.Should().Be((int)UserStatusEnum.Locked);
  }

  [Fact]
  public async Task ChangePasswordAsync_AddsOldPasswordToHistoryAndUpdatesUser()
  {
    SeedLookups();
    var user = CreateUser();
    var oldHash = user.PasswordHash;
    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    var changed = await _sut.ChangePasswordAsync(user.Id, "Password123", "NewPassword1");

    changed.Should().BeTrue();
    var reloaded = await _db.Users.FindAsync(user.Id);
    _passwordService.VerifyPassword("NewPassword1", reloaded!.PasswordHash).Should().BeTrue();
    _db.PasswordHistories.Should().ContainSingle(h => h.UserId == user.Id && h.PasswordHash == oldHash);
  }

  private User CreateUser(int failedLoginAttempts = 0) => new()
  {
    Id = 1,
    EmployeeCode = "EMP001",
    IdNumber = "123456789",
    FirstName = "Test",
    LastName = "User",
    PasswordHash = _passwordService.HashPassword("Password123"),
    UserRoleId = (int)UserRoleEnum.Employee,
    StatusId = (int)UserStatusEnum.Active,
    FailedLoginAttempts = failedLoginAttempts,
    CreatedAt = DateTime.UtcNow
  };

  private void SeedLookups()
  {
    _db.UserRoles.Add(new UserRole
    {
      Id = (int)UserRoleEnum.Employee,
      Name = nameof(UserRoleEnum.Employee)
    });
    _db.UserStatuses.AddRange(
      new UserStatus { Id = (int)UserStatusEnum.Active, Name = nameof(UserStatusEnum.Active) },
      new UserStatus { Id = (int)UserStatusEnum.Locked, Name = nameof(UserStatusEnum.Locked) });
    _db.SaveChanges();
  }
}
