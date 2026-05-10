using AxiomaReporting.Core.Enums;

namespace AxiomaReporting.Core.Interfaces;

public interface ICurrentUserService
{
  int UserId { get; }
  string IdNumber { get; }
  string FullName { get; }
  UserRoleEnum UserRole { get; }
  bool IsAuthenticated { get; }
}
