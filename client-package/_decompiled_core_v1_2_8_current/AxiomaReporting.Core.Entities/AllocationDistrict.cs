namespace AxiomaReporting.Core.Entities;

public class AllocationDistrict
{
	public int AllocationId { get; set; }

	public Allocation? Allocation { get; set; }

	public int DistrictId { get; set; }

	public District? District { get; set; }
}
