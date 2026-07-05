namespace AxiomaReporting.Core.Entities;

public class AllocationDomain
{
	public int AllocationId { get; set; }

	public Allocation? Allocation { get; set; }

	public int DomainId { get; set; }

	public Domain? Domain { get; set; }
}
