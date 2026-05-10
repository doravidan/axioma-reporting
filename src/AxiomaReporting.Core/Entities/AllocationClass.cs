namespace AxiomaReporting.Core.Entities;

public class AllocationClass
{
  public int AllocationId { get; set; }
  public Allocation? Allocation { get; set; }
  public int ClassId { get; set; }
  public SchoolClass? SchoolClass { get; set; }
}
