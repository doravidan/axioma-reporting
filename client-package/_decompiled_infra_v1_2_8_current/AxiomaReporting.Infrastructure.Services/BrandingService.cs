using System;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class BrandingService : IBrandingService
{
	private readonly AppDbContext _db;

	private string? _cachedPath;

	public BrandingService(AppDbContext db)
	{
		_db = db;
	}

	public async Task<string> GetLogoPathAsync(CancellationToken ct = default(CancellationToken))
	{
		if (_cachedPath != null)
		{
			return _cachedPath;
		}
		string text = (await _db.SystemConstants.AsNoTracking().FirstOrDefaultAsync((SystemConstant c) => c.Key == "SiteLogoPath", ct))?.Value;
		_cachedPath = (string.IsNullOrWhiteSpace(text) ? "/images/logo.png" : text);
		return _cachedPath;
	}

	public async Task SetLogoPathAsync(string publicPath, int? updatedByUserId, CancellationToken ct = default(CancellationToken))
	{
		if (string.IsNullOrWhiteSpace(publicPath))
		{
			throw new ArgumentException("publicPath must not be empty.", "publicPath");
		}
		SystemConstant systemConstant = await _db.SystemConstants.FirstOrDefaultAsync((SystemConstant c) => c.Key == "SiteLogoPath", ct);
		if (systemConstant == null)
		{
			systemConstant = new SystemConstant
			{
				Key = "SiteLogoPath",
				Value = publicPath,
				Description = "נתיב הלוגו של המערכת (תמונה ב-wwwroot)",
				CreatedAt = DateTime.UtcNow,
				UpdatedBy = updatedByUserId
			};
			_db.SystemConstants.Add(systemConstant);
		}
		else
		{
			systemConstant.Value = publicPath;
			systemConstant.UpdatedAt = DateTime.UtcNow;
			systemConstant.UpdatedBy = updatedByUserId;
		}
		await _db.SaveChangesAsync(ct);
		_cachedPath = publicPath;
	}
}
