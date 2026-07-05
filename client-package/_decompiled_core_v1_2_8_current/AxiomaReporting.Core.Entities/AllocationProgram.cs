namespace AxiomaReporting.Core.Entities;

public class AllocationProgram
{
	public int AllocationId { get; set; }

	public Allocation? Allocation { get; set; }

	public int ProgramId { get; set; }

	public Program? Program { get; set; }
}
