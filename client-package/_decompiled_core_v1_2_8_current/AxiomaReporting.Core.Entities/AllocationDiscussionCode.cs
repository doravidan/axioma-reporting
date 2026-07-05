namespace AxiomaReporting.Core.Entities;

public class AllocationDiscussionCode
{
	public int AllocationId { get; set; }

	public Allocation? Allocation { get; set; }

	public int DiscussionCodeId { get; set; }

	public DiscussionCode? DiscussionCode { get; set; }
}
