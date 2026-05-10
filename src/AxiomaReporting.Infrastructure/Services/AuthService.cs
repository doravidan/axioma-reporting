using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class AuthService : IAuthService
{
  private readonly AppDbContext _db;
  private readonly IPasswordService _passwordService;
  private readonly IAuditLogService? _auditLog;
  private const int MAX_FAILED_ATTEMPTS = 3;
  private const int PASSWORD_HISTORY_COUNT = 5;

  public AuthService(AppDbContext db, IPasswordService passwordService, IAuditLogService? auditLog = null)
  {
    _db = db;
    _passwordService = passwordService;
    _auditLog = auditLog;
  }

  public async Task<(bool Success, string? ErrorMessage, User? User)> ValidateLoginAsync(
    string idNumber, string password)
  {
    var user = await _db.Users
      .Include(u => u.UserRole)
      .Include(u => u.Status)
      .FirstOrDefaultAsync(u => u.IdNumber == idNumber);

    if (user == null)
      return (false, "פרטי הכניסה שגויים", null);

    if (user.StatusId == (int)UserStatusEnum.Locked)
      return (false, "חשבון זה נעול. פנה למנהל המערכת.", null);

    if (user.StatusId == (int)UserStatusEnum.Inactive)
      return (false, "חשבון זה אינו פעיל.", null);

    if (!_passwordService.VerifyPassword(password, user.PasswordHash))
    {
      await RecordFailedLoginAsync(idNumber);
      var updatedUser = await _db.Users.FirstOrDefaultAsync(u => u.IdNumber == idNumber);
      if (_auditLog != null)
        await _auditLog.LogAsync("Auth.LoginFailed", nameof(User), idNumber,
          notes: $"failed attempts={updatedUser?.FailedLoginAttempts}");
      if (updatedUser?.FailedLoginAttempts >= MAX_FAILED_ATTEMPTS)
      {
        if (_auditLog != null)
          await _auditLog.LogAsync("Auth.Lockout", nameof(User), updatedUser.Id.ToString(),
            notes: $"locked after {updatedUser.FailedLoginAttempts} failed attempts");
        return (false, "חשבון זה ננעל לאחר מספר ניסיונות כושלים. פנה למנהל המערכת.", null);
      }
      return (false, "פרטי הכניסה שגויים", null);
    }

    await ResetFailedLoginsAsync(user.Id);
    if (_auditLog != null)
      await _auditLog.LogAsync("Auth.LoginSucceeded", nameof(User), user.Id.ToString());
    return (true, null, user);
  }

  public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
  {
    var user = await _db.Users.FindAsync(userId);
    if (user == null) return false;

    if (!_passwordService.VerifyPassword(currentPassword, user.PasswordHash))
      return false;

    if (await IsPasswordInHistoryAsync(userId, newPassword))
      return false;

    var newHash = _passwordService.HashPassword(newPassword);
    await AddPasswordToHistoryAsync(userId, user.PasswordHash);

    user.PasswordHash = newHash;
    user.LastPasswordChange = DateTime.UtcNow;
    user.MustChangePassword = false;
    user.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    if (_auditLog != null)
      await _auditLog.LogAsync("Auth.PasswordChanged", nameof(User), user.Id.ToString(),
        notes: "self-service password change");
    return true;
  }

  public async Task RecordFailedLoginAsync(string idNumber)
  {
    var user = await _db.Users.FirstOrDefaultAsync(u => u.IdNumber == idNumber);
    if (user == null) return;

    user.FailedLoginAttempts++;
    if (user.FailedLoginAttempts >= MAX_FAILED_ATTEMPTS)
      user.StatusId = (int)UserStatusEnum.Locked;

    user.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();
  }

  public async Task ResetFailedLoginsAsync(int userId)
  {
    var user = await _db.Users.FindAsync(userId);
    if (user == null) return;
    user.FailedLoginAttempts = 0;
    user.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();
  }

  public async Task<bool> IsPasswordInHistoryAsync(int userId, string newPassword)
  {
    var history = await _db.PasswordHistories
      .Where(ph => ph.UserId == userId)
      .OrderByDescending(ph => ph.CreatedAt)
      .Take(PASSWORD_HISTORY_COUNT)
      .ToListAsync();

    return history.Any(ph => BCrypt.Net.BCrypt.Verify(newPassword, ph.PasswordHash));
  }

  public async Task AddPasswordToHistoryAsync(int userId, string passwordHash)
  {
    _db.PasswordHistories.Add(new PasswordHistory
    {
      UserId = userId,
      PasswordHash = passwordHash,
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    var oldEntries = await _db.PasswordHistories
      .Where(ph => ph.UserId == userId)
      .OrderByDescending(ph => ph.CreatedAt)
      .Skip(PASSWORD_HISTORY_COUNT)
      .ToListAsync();

    if (oldEntries.Count == 0) return;

    _db.PasswordHistories.RemoveRange(oldEntries);
    await _db.SaveChangesAsync();
  }
}
