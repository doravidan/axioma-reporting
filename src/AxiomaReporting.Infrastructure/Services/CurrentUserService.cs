using System.Security.Claims;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AxiomaReporting.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public CurrentUserService(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

  public int UserId =>
    int.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0;

  public string IdNumber => User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

  public string FullName => User?.FindFirst("FullName")?.Value ?? string.Empty;

  public UserRoleEnum UserRole =>
    Enum.TryParse<UserRoleEnum>(User?.FindFirst(ClaimTypes.Role)?.Value, out var role)
      ? role
      : UserRoleEnum.Employee;

  public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
}
