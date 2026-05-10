namespace AxiomaReporting.Core.Entities;

public class AllocationLocalityDistrictNational
{
  public int AllocationId { get; set; }
  public Allocation? Allocation { get; set; }
  public int LocalityDistrictNationalId { get; set; }
  public LocalityDistrictNational? LocalityDistrictNational { get; set; }
}
