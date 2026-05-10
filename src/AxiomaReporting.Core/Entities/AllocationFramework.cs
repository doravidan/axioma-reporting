namespace AxiomaReporting.Core.Entities;

public class AllocationFramework
{
  public int AllocationId { get; set; }
  public Allocation? Allocation { get; set; }
  public int FrameworkId { get; set; }
  public Framework? Framework { get; set; }
}
