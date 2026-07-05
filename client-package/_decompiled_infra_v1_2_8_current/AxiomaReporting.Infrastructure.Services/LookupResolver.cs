using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Entities.Base;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class LookupResolver : ILookupResolver
{
	private readonly AppDbContext _db;

	private Dictionary<string, int>? _districts;

	private Dictionary<string, int>? _sectors;

	private Dictionary<string, int>? _localities;

	private Dictionary<string, int>? _frameworks;

	private Dictionary<string, int>? _frameworkSymbols;

	private Dictionary<string, int>? _subjects;

	private Dictionary<string, int>? _domains;

	private Dictionary<string, int>? _educationalPrograms;

	private Dictionary<string, int>? _programs;

	private Dictionary<string, int>? _projects;

	private Dictionary<string, int>? _classes;

	private Dictionary<string, int>? _gradeLevels;

	private Dictionary<string, int>? _discussionCodes;

	private Dictionary<string, int>? _localityDistrictNationals;

	private Dictionary<string, int>? _reportTypes;

	public LookupResolver(AppDbContext db)
	{
		_db = db;
	}

	public Task<int?> ResolveDistrictAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _districts, delegate(Dictionary<string, int> d)
		{
			_districts = d;
		}, _db.Districts);
	}

	public Task<int?> ResolveSectorAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _sectors, delegate(Dictionary<string, int> d)
		{
			_sectors = d;
		}, _db.Sectors);
	}

	public Task<int?> ResolveLocalityAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _localities, delegate(Dictionary<string, int> d)
		{
			_localities = d;
		}, _db.Localities);
	}

	public async Task<int?> ResolveFrameworkAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return null;
		}
		string trimmed = value.Trim();
		if (_frameworks == null || _frameworkSymbols == null)
		{
			var source = await (from f in _db.Frameworks.AsNoTracking()
				select new { f.Id, f.Description, f.InstitutionSymbol }).ToListAsync(ct);
			_frameworks = (from x in source
				where !string.IsNullOrWhiteSpace(x.Description)
				group x by x.Description.Trim().ToLowerInvariant()).ToDictionary(g => g.Key, g => g.First().Id);
			_frameworkSymbols = (from x in source
				where !string.IsNullOrWhiteSpace(x.InstitutionSymbol)
				group x by x.InstitutionSymbol.Trim()).ToDictionary(g => g.Key, g => g.First().Id);
		}
		if (int.TryParse(trimmed, out var _) && _frameworkSymbols.TryGetValue(trimmed, out var value2))
		{
			return value2;
		}
		if (_frameworks.TryGetValue(trimmed.ToLowerInvariant(), out var value3))
		{
			return value3;
		}
		string embeddedSymbol = string.Join(" ", trimmed.Select((char c) => char.IsDigit(c) ? c : ' ')).Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault((string token) => token.Length >= 3);
		if (!string.IsNullOrWhiteSpace(embeddedSymbol) && _frameworkSymbols.TryGetValue(embeddedSymbol, out var value4))
		{
			return value4;
		}
		return null;
	}

	public Task<int?> ResolveSubjectAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _subjects, delegate(Dictionary<string, int> d)
		{
			_subjects = d;
		}, _db.Subjects);
	}

	public Task<int?> ResolveDomainAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _domains, delegate(Dictionary<string, int> d)
		{
			_domains = d;
		}, _db.Domains);
	}

	public Task<int?> ResolveEducationalProgramAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _educationalPrograms, delegate(Dictionary<string, int> d)
		{
			_educationalPrograms = d;
		}, _db.EducationalPrograms);
	}

	public Task<int?> ResolveProgramAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _programs, delegate(Dictionary<string, int> d)
		{
			_programs = d;
		}, _db.Programs);
	}

	public Task<int?> ResolveProjectAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _projects, delegate(Dictionary<string, int> d)
		{
			_projects = d;
		}, _db.Projects);
	}

	public Task<int?> ResolveClassAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _classes, delegate(Dictionary<string, int> d)
		{
			_classes = d;
		}, _db.Classes);
	}

	public Task<int?> ResolveGradeLevelAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _gradeLevels, delegate(Dictionary<string, int> d)
		{
			_gradeLevels = d;
		}, _db.GradeLevels);
	}

	public Task<int?> ResolveDiscussionCodeAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _discussionCodes, delegate(Dictionary<string, int> d)
		{
			_discussionCodes = d;
		}, _db.DiscussionCodes);
	}

	public Task<int?> ResolveLocalityDistrictNationalAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _localityDistrictNationals, delegate(Dictionary<string, int> d)
		{
			_localityDistrictNationals = d;
		}, _db.LocalityDistrictNationals);
	}

	public Task<int?> ResolveReportTypeAsync(string? value, CancellationToken ct = default(CancellationToken))
	{
		return ResolveAsync(value, ct, () => _reportTypes, delegate(Dictionary<string, int> d)
		{
			_reportTypes = d;
		}, _db.ReportTypes);
	}

	private static async Task<int?> ResolveAsync<T>(string? value, CancellationToken ct, Func<Dictionary<string, int>?> cacheGetter, Action<Dictionary<string, int>> cacheSetter, IQueryable<T> source) where T : LookupEntity
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return null;
		}
		string trimmed = value.Trim();
		Dictionary<string, int> dictionary = cacheGetter();
		if (dictionary == null)
		{
			dictionary = (from x in await (from e in source.AsNoTracking()
					select new { e.Id, e.Description }).ToListAsync(ct)
				where !string.IsNullOrWhiteSpace(x.Description)
				group x by x.Description.Trim().ToLowerInvariant()).ToDictionary(g => g.Key, g => g.First().Id);
			cacheSetter(dictionary);
		}
		if (int.TryParse(trimmed, out var result) && dictionary.Values.Contains(result))
		{
			return result;
		}
		int value2;
		return dictionary.TryGetValue(trimmed.ToLowerInvariant(), out value2) ? new int?(value2) : null;
	}
}
