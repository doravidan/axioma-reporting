using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class AuthServiceExtendedTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly PasswordService _passwordService = new();
  private readonly AuthService _sut;

  public AuthServiceExtendedTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _sut = new AuthService(_db, _passwordService);
  }

  public void Dispose() => _db.Dispose();

  // -------------------------------------------------------------------------
  // ChangePasswordAsync
  // -------------------------------------------------------------------------

  [Fact]
  public async Task ChangePasswordAsync_RejectsPasswordInHistory()
  {
    // Arrange – a user whose last 5 passwords already contain "OldPass1"
    SeedLookups();
    var user = CreateActiveUser();
    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    // Put "OldPass1" into the password history
    var oldHash = _passwordService.HashPassword("OldPass1");
    _db.PasswordHistories.Add(new PasswordHistory
    {
      UserId = user.Id,
      PasswordHash = oldHash,
      CreatedAt = DateTime.UtcNow.AddDays(-10)
    });
    await _db.SaveChangesAsync();

    // Act – attempt to change back to "OldPass1"
    var result = await _sut.ChangePasswordAsync(user.Id, "Password123", "OldPass1");

    // Assert
    result.Should().BeFalse("a password that appears in the last 5 history entries must be rejected");
  }

  [Fact]
  public async Task ChangePasswordAsync_RejectsWrongCurrentPassword()
  {
    SeedLookups();
    var user = CreateActiveUser();
    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    var result = await _sut.ChangePasswordAsync(user.Id, "WrongCurrentPass1", "NewPassword1");

    result.Should().BeFalse("the change must be rejected when the current-password verification fails");
    // The stored hash must remain unchanged
    var reloaded = await _db.Users.FindAsync(user.Id);
    _passwordService.VerifyPassword("Password123", reloaded!.PasswordHash).Should().BeTrue();
  }

  [Fact]
  public async Task ChangePasswordAsync_AcceptsAfterHistoryExpires()
  {
    // Arrange – fill history with 5 entries; then the 6th oldest falls out of the sliding
    // window of PASSWORD_HISTORY_COUNT (5), so it should be accepted again.
    SeedLookups();
    var user = CreateActiveUser();
    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    // Seed exactly 5 history entries (all different from "Password123")
    var firstEverHash = _passwordService.HashPassword("VeryFirst1");
    var histories = new[]
    {
      new PasswordHistory { UserId = user.Id, PasswordHash = firstEverHash,                                   CreatedAt = DateTime.UtcNow.AddDays(-50) },
      new PasswordHistory { UserId = user.Id, PasswordHash = _passwordService.HashPassword("Second1pass"),    CreatedAt = DateTime.UtcNow.AddDays(-40) },
      new PasswordHistory { UserId = user.Id, PasswordHash = _passwordService.HashPassword("Third1pass"),     CreatedAt = DateTime.UtcNow.AddDays(-30) },
      new PasswordHistory { UserId = user.Id, PasswordHash = _passwordService.HashPassword("Fourth1pass"),    CreatedAt = DateTime.UtcNow.AddDays(-20) },
      new PasswordHistory { UserId = user.Id, PasswordHash = _passwordService.HashPassword("Fifth1pass"),     CreatedAt = DateTime.UtcNow.AddDays(-10) },
    };
    _db.PasswordHistories.AddRange(histories);
    await _db.SaveChangesAsync();

    // "VeryFirst1" is the oldest entry; with 5 history entries the service will only
    // look at the latest 5 by OrderByDescending. "VeryFirst1" is position 5 (last in
    // the taken window), so it IS still blocked at this point.
    // To make it fall off the window we must advance the current password through one
    // more change, which pushes the current hash into position 1, making "VeryFirst1"
    // the 6th-oldest and therefore outside the window.

    // First, perform an intermediate password change (Password123 -> NewInter1) so that
    // Password123 gets pushed into history and VeryFirst1 falls out of the top-5.
    var intermediate = await _sut.ChangePasswordAsync(user.Id, "Password123", "NewInter1");
    intermediate.Should().BeTrue();

    // Now "VeryFirst1" is position 6 in the history (oldest), outside the 5-entry window.
    var result = await _sut.ChangePasswordAsync(user.Id, "NewInter1", "VeryFirst1");

    result.Should().BeTrue("a password that has fallen off the 5-entry history window must be accepted");
  }

  // -------------------------------------------------------------------------
  // ValidateLoginAsync – status checks
  // -------------------------------------------------------------------------

  [Fact]
  public async Task ValidateLoginAsync_InactiveUser_CannotLogin()
  {
    SeedLookups(includeInactive: true);
    var user = CreateActiveUser();
    user.StatusId = (int)UserStatusEnum.Inactive;
    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    var result = await _sut.ValidateLoginAsync("123456789", "Password123");

    result.Success.Should().BeFalse();
    result.ErrorMessage.Should().NotBeNullOrEmpty();
    result.User.Should().BeNull();
  }

  [Fact]
  public async Task ValidateLoginAsync_LockedUser_CannotLoginEvenWithCorrectPassword()
  {
    SeedLookups();
    var user = CreateActiveUser();
    user.StatusId = (int)UserStatusEnum.Locked;
    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    // Correct password but account is locked
    var result = await _sut.ValidateLoginAsync("123456789", "Password123");

    result.Success.Should().BeFalse();
    result.ErrorMessage.Should().Contain("נעול", "locked accounts must receive the lock message");
    result.User.Should().BeNull();
  }

  // -------------------------------------------------------------------------
  // ValidateLoginAsync – failed-attempt counter behaviour
  // -------------------------------------------------------------------------

  [Fact]
  public async Task ValidateLoginAsync_WrongPassword_DoesNotSignIn_DoesNotResetCount()
  {
    SeedLookups();
    var user = CreateActiveUser(failedLoginAttempts: 1);
    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    var result = await _sut.ValidateLoginAsync("123456789", "WrongPass1");

    result.Success.Should().BeFalse();
    result.User.Should().BeNull();
    var reloaded = await _db.Users.FindAsync(user.Id);
    reloaded!.FailedLoginAttempts.Should().Be(2,
      "each wrong-password attempt must increment the counter, not reset it");
  }

  [Fact]
  public async Task ValidateLoginAsync_AfterLockout_ReturnsLockedMessage()
  {
    // Arrange: drive the account to lockout with 3 bad attempts
    SeedLookups();
    _db.Users.Add(CreateActiveUser());
    await _db.SaveChangesAsync();

    await _sut.ValidateLoginAsync("123456789", "bad1");
    await _sut.ValidateLoginAsync("123456789", "bad2");
    await _sut.ValidateLoginAsync("123456789", "bad3");

    // 4th attempt with the CORRECT password: account is now locked, must still be rejected
    var result = await _sut.ValidateLoginAsync("123456789", "Password123");

    result.Success.Should().BeFalse();
    result.ErrorMessage.Should().NotBeNullOrEmpty("a locked account must return an error message");
    result.User.Should().BeNull();
  }

  [Fact]
  public async Task ValidateLoginAsync_SuccessAfterOneFailure_ResetsCount()
  {
    SeedLookups();
    var user = CreateActiveUser();
    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    // One bad attempt
    await _sut.ValidateLoginAsync("123456789", "WrongPass1");

    var afterFailure = await _db.Users.FindAsync(user.Id);
    afterFailure!.FailedLoginAttempts.Should().Be(1);

    // Correct credentials
    var result = await _sut.ValidateLoginAsync("123456789", "Password123");

    result.Success.Should().BeTrue();
    var afterSuccess = await _db.Users.FindAsync(user.Id);
    afterSuccess!.FailedLoginAttempts.Should().Be(0,
      "a successful login must reset the failed-attempt counter to zero");
  }

  // -------------------------------------------------------------------------
  // Helpers
  // -------------------------------------------------------------------------

  private User CreateActiveUser(int failedLoginAttempts = 0) => new()
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

  private void SeedLookups(bool includeInactive = false)
  {
    _db.UserRoles.Add(new UserRole
    {
      Id = (int)UserRoleEnum.Employee,
      Name = nameof(UserRoleEnum.Employee)
    });
    _db.UserStatuses.AddRange(
      new UserStatus { Id = (int)UserStatusEnum.Active,   Name = nameof(UserStatusEnum.Active) },
      new UserStatus { Id = (int)UserStatusEnum.Locked,   Name = nameof(UserStatusEnum.Locked) });

    if (includeInactive)
    {
      _db.UserStatuses.Add(
        new UserStatus { Id = (int)UserStatusEnum.Inactive, Name = nameof(UserStatusEnum.Inactive) });
    }

    _db.SaveChanges();
  }
}
