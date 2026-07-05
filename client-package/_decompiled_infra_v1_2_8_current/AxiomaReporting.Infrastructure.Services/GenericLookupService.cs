using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		IQueryable<T> source = _set.AsQueryable();
		if (!includeInactive)
		{
			source = source.Where((T x) => x.IsActive);
		}
		return await source.OrderBy((T x) => x.Description).ToListAsync();
	}

	public async Task<T?> GetByIdAsync(int id)
	{
		return await _set.FindAsync(id);
	}

	public async Task<T> CreateAsync(string description)
	{
		T entity = new T
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
		T val = await _set.FindAsync(id);
		if (val == null)
		{
			return false;
		}
		val.Description = description;
		val.IsActive = isActive;
		val.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		return true;
	}

	public virtual async Task<(bool CanDelete, string? Reason)> CanDeleteAsync(int id)
	{
		return await Task.FromResult<(bool, string)>((true, null));
	}

	public async Task DeleteAsync(int id)
	{
		T val = await _set.FindAsync(id);
		if (val != null)
		{
			_set.Remove(val);
			await _db.SaveChangesAsync();
		}
	}

	public async Task<List<T>> SearchAsync(string query)
	{
		string query2 = query;
		return await (from x in _set
			where x.IsActive && EF.Functions.Like(x.Description, $"%{query2}%")
			orderby x.Description
			select x).ToListAsync();
	}
}
