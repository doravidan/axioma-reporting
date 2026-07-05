using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Infrastructure.Services;

public class AuditLogService : IAuditLogService
{
	private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
	{
		WriteIndented = false,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		ReferenceHandler = ReferenceHandler.IgnoreCycles
	};

	private readonly AppDbContext _db;

	private readonly ICurrentUserService _currentUser;

	private readonly IHttpContextAccessor _httpContextAccessor;

	private readonly ILogger<AuditLogService> _logger;

	public AuditLogService(AppDbContext db, ICurrentUserService currentUser, IHttpContextAccessor httpContextAccessor, ILogger<AuditLogService> logger)
	{
		_db = db;
		_currentUser = currentUser;
		_httpContextAccessor = httpContextAccessor;
		_logger = logger;
	}

	public async Task LogAsync(string action, string entityType, string? entityId, object? before = null, object? after = null, string? notes = null, CancellationToken ct = default(CancellationToken))
	{
		try
		{
			AuditLog entity = new AuditLog
			{
				Timestamp = DateTime.UtcNow,
				ActorUserId = ((_currentUser.IsAuthenticated && _currentUser.UserId > 0) ? new int?(_currentUser.UserId) : null),
				Action = action,
				EntityType = entityType,
				EntityId = entityId,
				Before = Serialize(before),
				After = Serialize(after),
				IpAddress = GetIpAddress(),
				UserAgent = GetUserAgent(),
				Notes = Truncate(notes, 1000)
			};
			_db.AuditLogs.Add(entity);
			await _db.SaveChangesAsync(ct);
		}
		catch (Exception exception)
		{
			_logger.LogWarning(exception, "Failed to write AuditLog entry (action={Action}, entityType={EntityType}, entityId={EntityId})", action, entityType, entityId);
		}
	}

	private static string? Serialize(object? payload)
	{
		if (payload == null)
		{
			return null;
		}
		try
		{
			return JsonSerializer.Serialize(payload, SerializerOptions);
		}
		catch
		{
			return null;
		}
	}

	private string? GetIpAddress()
	{
		HttpContext httpContext = _httpContextAccessor.HttpContext;
		if (httpContext == null)
		{
			return null;
		}
		return Truncate(httpContext.Connection.RemoteIpAddress?.ToString(), 64);
	}

	private string? GetUserAgent()
	{
		HttpContext httpContext = _httpContextAccessor.HttpContext;
		if (httpContext == null)
		{
			return null;
		}
		string value = httpContext.Request.Headers["User-Agent"].ToString();
		if (string.IsNullOrEmpty(value))
		{
			return null;
		}
		return Truncate(value, 500);
	}

	private static string? Truncate(string? value, int max)
	{
		if (value != null)
		{
			if (value.Length > max)
			{
				return value.Substring(0, max);
			}
			return value;
		}
		return null;
	}
}
