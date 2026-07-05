using System.Threading;
using System.Threading.Tasks;

namespace AxiomaReporting.Core.Interfaces;

public interface IBrandingService
{
	const string LogoPathConstantKey = "SiteLogoPath";

	const string DefaultLogoPath = "/images/logo.png";

	Task<string> GetLogoPathAsync(CancellationToken ct = default(CancellationToken));

	Task SetLogoPathAsync(string publicPath, int? updatedByUserId, CancellationToken ct = default(CancellationToken));
}
