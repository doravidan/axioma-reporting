namespace AxiomaReporting.Core.Entities;

public class AllocationEducationalProgram
{
  public int AllocationId { get; set; }
  public Allocation? Allocation { get; set; }
  public int EducationalProgramId { get; set; }
  public EducationalProgram? EducationalProgram { get; set; }
}
