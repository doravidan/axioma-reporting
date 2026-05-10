using System.Text.Json;
using System.Text.Json.Serialization;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AxiomaReporting.Infrastructure.Services;

public class AuditLogService : IAuditLogService
{
  private static readonly JsonSerializerOptions SerializerOptions = new()
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

  public AuditLogService(
    AppDbContext db,
    ICurrentUserService currentUser,
    IHttpContextAccessor httpContextAccessor,
    ILogger<AuditLogService> logger)
  {
    _db = db;
    _currentUser = currentUser;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
  }

  public async Task LogAsync(
    string action,
    string entityType,
    string? entityId,
    object? before = null,
    object? after = null,
    string? notes = null,
    CancellationToken ct = default)
  {
    try
    {
      var entry = new AuditLog
      {
        Timestamp = DateTime.UtcNow,
        ActorUserId = _currentUser.IsAuthenticated && _currentUser.UserId > 0
          ? _currentUser.UserId
          : null,
        Action = action,
        EntityType = entityType,
        EntityId = entityId,
        Before = Serialize(before),
        After = Serialize(after),
        IpAddress = GetIpAddress(),
        UserAgent = GetUserAgent(),
        Notes = Truncate(notes, 1000)
      };
      _db.AuditLogs.Add(entry);
      await _db.SaveChangesAsync(ct);
    }
    catch (Exception ex)
    {
      // Audit logging must never break a business action.
      _logger.LogWarning(ex,
        "Failed to write AuditLog entry (action={Action}, entityType={EntityType}, entityId={EntityId})",
        action, entityType, entityId);
    }
  }

  private static string? Serialize(object? payload)
  {
    if (payload == null) return null;
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
    var ctx = _httpContextAccessor.HttpContext;
    if (ctx == null) return null;
    var ip = ctx.Connection.RemoteIpAddress?.ToString();
    return Truncate(ip, 64);
  }

  private string? GetUserAgent()
  {
    var ctx = _httpContextAccessor.HttpContext;
    if (ctx == null) return null;
    var ua = ctx.Request.Headers["User-Agent"].ToString();
    if (string.IsNullOrEmpty(ua)) return null;
    return Truncate(ua, 500);
  }

  private static string? Truncate(string? value, int max) =>
    value == null ? null : (value.Length <= max ? value : value[..max]);
}
