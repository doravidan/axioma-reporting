using System.Threading;
using System.Threading.Tasks;

namespace AxiomaReporting.Infrastructure.Services;

public interface ILookupResolver
{
	Task<int?> ResolveDistrictAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveSectorAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveLocalityAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveFrameworkAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveSubjectAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveDomainAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveEducationalProgramAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveProgramAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveProjectAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveClassAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveGradeLevelAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveDiscussionCodeAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveLocalityDistrictNationalAsync(string? value, CancellationToken ct = default(CancellationToken));

	Task<int?> ResolveReportTypeAsync(string? value, CancellationToken ct = default(CancellationToken));
}
