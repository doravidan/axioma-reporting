using AxiomaReporting.Core.Entities.Base;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class GenericLookupService<T> : ILookupService<T> where T : LookupEntity, new()
{
  protected readonly AppDbContext _db;
  protected readonly DbSet<T> _set;

  public GenericLookupService(AppDbContext db)
  {
    _db = db;
    _set = db.Set<T>();
  }

  public async Task<List<T>> GetAllAsync(bool includeInactive = false)
  {
    var q = _set.AsQueryable();
    if (!includeInactive) q = q.Where(x => x.IsActive);
    return await q.OrderBy(x => x.Description).ToListAsync();
  }

  public async Task<T?> GetByIdAsync(int id) =>
    await _set.FindAsync(id);

  public async Task<T> CreateAsync(string description)
  {
    var entity = new T
    {
      Description = description,
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    };
    _set.Add(entity);
    await _db.SaveChangesAsync();
    return entity;
  }

  public async Task<bool> UpdateAsync(int id, string description, bool isActive)
  {
    var entity = await _set.FindAsync(id);
    if (entity == null) return false;
    entity.Description = description;
    entity.IsActive = isActive;
    entity.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();
    return true;
  }

  public virtual async Task<(bool CanDelete, string? Reason)> CanDeleteAsync(int id)
  {
    // Default: allow deletion — subclasses override for specific FK checks
    return await Task.FromResult((true, (string?)null));
  }

  public async Task DeleteAsync(int id)
  {
    var entity = await _set.FindAsync(id);
    if (entity != null)
    {
      _set.Remove(entity);
      await _db.SaveChangesAsync();
    }
  }

  public async Task<List<T>> SearchAsync(string query)
  {
    return await _set
      .Where(x => x.IsActive && EF.Functions.Like(x.Description, $"%{query}%"))
      .OrderBy(x => x.Description)
      .ToListAsync();
  }
}
