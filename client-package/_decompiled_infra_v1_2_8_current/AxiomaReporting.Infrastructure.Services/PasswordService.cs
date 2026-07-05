using System;
using System.Linq;
using AxiomaReporting.Core.Interfaces;
using BCrypt.Net;

namespace AxiomaReporting.Infrastructure.Services;

public class PasswordService : IPasswordService
{
	private const int PASSWORD_EXPIRY_DAYS = 90;

	public string HashPassword(string password)
	{
		return BCrypt.Net.BCrypt.HashPassword(password, 12);
	}

	public bool VerifyPassword(string password, string hash)
	{
		return BCrypt.Net.BCrypt.Verify(password, hash);
	}

	public bool IsPasswordStrong(string password)
	{
		if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
		{
			return false;
		}
		bool num = password.Any(char.IsUpper);
		bool flag = password.Any(char.IsLower);
		bool flag2 = password.Any(char.IsDigit);
		bool flag3 = password.Any((char ch) => !char.IsLetterOrDigit(ch));
		bool flag4 = password.Zip(password.Skip(1), (char left, char right) => char.ToUpperInvariant(left) == char.ToUpperInvariant(right)).Any((bool isSame) => isSame);
		if (num && flag && flag2 && flag3)
		{
			return !flag4;
		}
		return false;
	}

	public bool IsPasswordExpired(DateTime? lastPasswordChange)
	{
		if (!lastPasswordChange.HasValue)
		{
			return true;
		}
		return (DateTime.UtcNow - lastPasswordChange.Value).TotalDays > 90.0;
	}
}
