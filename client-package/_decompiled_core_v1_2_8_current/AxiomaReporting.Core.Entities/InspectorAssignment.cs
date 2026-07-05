namespace AxiomaReporting.Core.Entities;

public class InspectorAssignment
{
	public int Id { get; set; }

	public int InspectorUserId { get; set; }

	public User? Inspector { get; set; }

	public int? ProgramId { get; set; }

	public Program? Program { get; set; }

	public int? DistrictId { get; set; }

	public District? District { get; set; }

	public int? SectorId { get; set; }

	public Sector? Sector { get; set; }
}
