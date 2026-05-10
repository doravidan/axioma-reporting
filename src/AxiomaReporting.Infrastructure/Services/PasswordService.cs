using AxiomaReporting.Core.Interfaces;

namespace AxiomaReporting.Infrastructure.Services;

public class PasswordService : IPasswordService
{
  private const int PASSWORD_EXPIRY_DAYS = 90;

  public string HashPassword(string password) =>
    BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);

  public bool VerifyPassword(string password, string hash) =>
    BCrypt.Net.BCrypt.Verify(password, hash);

  public bool IsPasswordStrong(string password)
  {
    if (string.IsNullOrWhiteSpace(password) || password.Length < 8) return false;

    var hasUppercase = password.Any(char.IsUpper);
    var hasLowercase = password.Any(char.IsLower);
    var hasDigit = password.Any(char.IsDigit);
    var hasSymbol = password.Any(ch => !char.IsLetterOrDigit(ch));
    var hasRepeatedAdjacentChar = password
      .Zip(password.Skip(1), (left, right) => char.ToUpperInvariant(left) == char.ToUpperInvariant(right))
      .Any(isSame => isSame);

    return hasUppercase && hasLowercase && hasDigit && hasSymbol && !hasRepeatedAdjacentChar;
  }

  public bool IsPasswordExpired(DateTime? lastPasswordChange)
  {
    if (lastPasswordChange == null) return true;
    return (DateTime.UtcNow - lastPasswordChange.Value).TotalDays > PASSWORD_EXPIRY_DAYS;
  }
}
