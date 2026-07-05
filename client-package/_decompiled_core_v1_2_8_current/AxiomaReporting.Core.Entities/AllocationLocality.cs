namespace AxiomaReporting.Core.Entities;

public class AllocationLocality
{
	public int AllocationId { get; set; }

	public Allocation? Allocation { get; set; }

	public int LocalityId { get; set; }

	public Locality? Locality { get; set; }
}
