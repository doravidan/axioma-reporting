namespace AxiomaReporting.Core.Entities;

public class AllocationSector
{
	public int AllocationId { get; set; }

	public Allocation? Allocation { get; set; }

	public int SectorId { get; set; }

	public Sector? Sector { get; set; }
}
