namespace AxiomaReporting.Core.Entities;

public class AllocationGradeLevel
{
	public int AllocationId { get; set; }

	public Allocation? Allocation { get; set; }

	public int GradeLevelId { get; set; }

	public GradeLevel? GradeLevel { get; set; }
}
