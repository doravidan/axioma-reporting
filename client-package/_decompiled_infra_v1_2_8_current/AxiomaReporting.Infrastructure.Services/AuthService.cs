using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using BCrypt.Net;
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

	public async Task<(bool Success, string? ErrorMessage, User? User)> ValidateLoginAsync(string idNumber, string password)
	{
		string idNumber2 = idNumber;
		User user = await _db.Users.Include((User u) => u.UserRole).Include((User u) => u.Status).FirstOrDefaultAsync((User u) => u.IdNumber == idNumber2);
		if (user == null)
		{
			return (Success: false, ErrorMessage: "פרטי הכניסה שגויים", User: null);
		}
		if (user.StatusId == 3)
		{
			return (Success: false, ErrorMessage: "חשבון זה נעול. פנה למנהל המערכת.", User: null);
		}
		if (user.StatusId == 2)
		{
			return (Success: false, ErrorMessage: "חשבון זה אינו פעיל.", User: null);
		}
		if (!_passwordService.VerifyPassword(password, user.PasswordHash))
		{
			await RecordFailedLoginAsync(idNumber2);
			User updatedUser = await _db.Users.FirstOrDefaultAsync((User u) => u.IdNumber == idNumber2);
			if (_auditLog != null)
			{
				await _auditLog.LogAsync("Auth.LoginFailed", "User", idNumber2, null, null, $"failed attempts={updatedUser?.FailedLoginAttempts}");
			}
			if (updatedUser != null && updatedUser.FailedLoginAttempts >= 3)
			{
				if (_auditLog != null)
				{
					await _auditLog.LogAsync("Auth.Lockout", "User", updatedUser.Id.ToString(), null, null, $"locked after {updatedUser.FailedLoginAttempts} failed attempts");
				}
				return (Success: false, ErrorMessage: "חשבון זה ננעל לאחר מספר ניסיונות כושלים. פנה למנהל המערכת.", User: null);
			}
			return (Success: false, ErrorMessage: "פרטי הכניסה שגויים", User: null);
		}
		await ResetFailedLoginsAsync(user.Id);
		if (_auditLog != null)
		{
			await _auditLog.LogAsync("Auth.LoginSucceeded", "User", user.Id.ToString());
		}
		return (Success: true, ErrorMessage: null, User: user);
	}

	public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
	{
		User user = await _db.Users.FindAsync(userId);
		if (user == null)
		{
			return false;
		}
		if (!_passwordService.VerifyPassword(currentPassword, user.PasswordHash))
		{
			return false;
		}
		if (await IsPasswordInHistoryAsync(userId, newPassword))
		{
			return false;
		}
		string newHash = _passwordService.HashPassword(newPassword);
		await AddPasswordToHistoryAsync(userId, user.PasswordHash);
		user.PasswordHash = newHash;
		user.LastPasswordChange = DateTime.UtcNow;
		user.MustChangePassword = false;
		user.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		if (_auditLog != null)
		{
			await _auditLog.LogAsync("Auth.PasswordChanged", "User", user.Id.ToString(), null, null, "self-service password change");
		}
		return true;
	}

	public async Task RecordFailedLoginAsync(string idNumber)
	{
		string idNumber2 = idNumber;
		User user = await _db.Users.FirstOrDefaultAsync((User u) => u.IdNumber == idNumber2);
		if (user != null)
		{
			user.FailedLoginAttempts++;
			if (user.FailedLoginAttempts >= 3)
			{
				user.StatusId = 3;
			}
			user.UpdatedAt = DateTime.UtcNow;
			await _db.SaveChangesAsync();
		}
	}

	public async Task ResetFailedLoginsAsync(int userId)
	{
		User user = await _db.Users.FindAsync(userId);
		if (user != null)
		{
			user.FailedLoginAttempts = 0;
			user.UpdatedAt = DateTime.UtcNow;
			await _db.SaveChangesAsync();
		}
	}

	public async Task<bool> IsPasswordInHistoryAsync(int userId, string newPassword)
	{
		string newPassword2 = newPassword;
		return (await (from ph in _db.PasswordHistories
			where ph.UserId == userId
			orderby ph.CreatedAt descending
			select ph).Take(5).ToListAsync()).Any((PasswordHistory ph) => BCrypt.Net.BCrypt.Verify(newPassword2, ph.PasswordHash));
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
		List<PasswordHistory> list = await (from ph in _db.PasswordHistories
			where ph.UserId == userId
			orderby ph.CreatedAt descending
			select ph).Skip(5).ToListAsync();
		if (list.Count != 0)
		{
			_db.PasswordHistories.RemoveRange(list);
			await _db.SaveChangesAsync();
		}
	}
}
