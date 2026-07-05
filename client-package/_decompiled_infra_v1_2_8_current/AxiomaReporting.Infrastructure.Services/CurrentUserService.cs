using System;
using System.Security.Claims;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AxiomaReporting.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

	public int UserId
	{
		get
		{
			if (!int.TryParse(User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value, out var result))
			{
				return 0;
			}
			return result;
		}
	}

	public string IdNumber => User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value ?? string.Empty;

	public string FullName => User?.FindFirst("FullName")?.Value ?? string.Empty;

	public UserRoleEnum UserRole
	{
		get
		{
			if (!Enum.TryParse<UserRoleEnum>(User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value, out var result))
			{
				return UserRoleEnum.Employee;
			}
			return result;
		}
	}

	public bool IsAuthenticated => (User?.Identity?.IsAuthenticated).GetValueOrDefault();

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}
}
