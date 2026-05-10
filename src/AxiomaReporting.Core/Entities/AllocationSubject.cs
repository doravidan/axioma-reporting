namespace AxiomaReporting.Core.Entities;

public class AllocationSubject
{
  public int AllocationId { get; set; }
  public Allocation? Allocation { get; set; }
  public int SubjectId { get; set; }
  public Subject? Subject { get; set; }
}
