using System.Threading;
using System.Threading.Tasks;

namespace AxiomaReporting.Core.Interfaces;

public interface IAuditLogService
{
	Task LogAsync(string action, string entityType, string? entityId, object? before = null, object? after = null, string? notes = null, CancellationToken ct = default(CancellationToken));
}
