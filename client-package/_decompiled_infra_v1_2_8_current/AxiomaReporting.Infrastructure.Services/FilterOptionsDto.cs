using System.Collections.Generic;

namespace AxiomaReporting.Infrastructure.Services;

public class FilterOptionsDto
{
	public List<IdName> Districts { get; set; } = new List<IdName>();


	public List<IdName> Sectors { get; set; } = new List<IdName>();


	public List<IdName> Programs { get; set; } = new List<IdName>();


	public List<IdName> Employees { get; set; } = new List<IdName>();


	public List<IdName> Projects { get; set; } = new List<IdName>();


	public List<IdName> Months { get; set; } = new List<IdName>();


	public List<IdName> Statuses { get; set; } = new List<IdName>();

}
