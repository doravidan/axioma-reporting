namespace AxiomaReporting.Web.Authorization;

public static class PolicyNames
{
	public const string AdminOnly = "AdminOnly";

	public const string AdminOrPM = "AdminOrPM";

	public const string AdminPMOrCoordinator = "AdminPMOrCoordinator";

	public const string CanApproveReports = "CanApproveReports";

	public const string CanViewDashboard = "CanViewDashboard";

	public const string CanManageLookups = "CanManageLookups";
}
