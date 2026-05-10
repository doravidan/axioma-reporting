namespace AxiomaReporting.Core.Interfaces;

/// <summary>
/// Provides branding assets (currently the site logo path) backed by the
/// <c>SystemConstants</c> table. The returned path is a web-rooted URL, e.g.
/// <c>/images/logo.png</c> or <c>/uploads/branding/site-logo.png</c>.
/// Value is cached for the lifetime of the service instance (per-request scope).
/// </summary>
public interface IBrandingService
{
  /// <summary>
  /// Returns the active site logo path. Falls back to
  /// <see cref="DefaultLogoPath"/> when the <c>SiteLogoPath</c> constant is
  /// missing or empty.
  /// </summary>
  Task<string> GetLogoPathAsync(CancellationToken ct = default);

  /// <summary>
  /// Persists a new logo path to the <c>SiteLogoPath</c> system constant.
  /// Creates the constant if it does not yet exist.
  /// </summary>
  Task SetLogoPathAsync(string publicPath, int? updatedByUserId, CancellationToken ct = default);

  /// <summary>Key used to look up the logo path in SystemConstants.</summary>
  const string LogoPathConstantKey = "SiteLogoPath";

  /// <summary>Default public path served when no override is stored.</summary>
  const string DefaultLogoPath = "/images/logo.png";
}
