using System.Security.Cryptography;
using System.Text;
using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Web.Security;

internal static class AuthenticationState
{
  internal const string FingerprintClaim = "AxiomaAuthState";
  internal const string ValidatedAtClaim = "AxiomaAuthValidatedAt";

  internal static string CreateFingerprint(User user) =>
    CreateFingerprint(user.PasswordHash, user.StatusId, user.UserRoleId);

  internal static string CreateFingerprint(string passwordHash, int statusId, int userRoleId)
  {
    var value = $"{passwordHash}\n{statusId}\n{userRoleId}";
    return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
  }
}
