using System.Collections.Generic;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Infrastructure.Services;

public interface ILookupService<T> where T : LookupEntity, new()
{
	Task<List<T>> GetAllAsync(bool includeInactive = false);

	Task<T?> GetByIdAsync(int id);

	Task<T> CreateAsync(string description);

	Task<bool> UpdateAsync(int id, string description, bool isActive);

	Task<(bool CanDelete, string? Reason)> CanDeleteAsync(int id);

	Task DeleteAsync(int id);

	Task<List<T>> SearchAsync(string query);
}
