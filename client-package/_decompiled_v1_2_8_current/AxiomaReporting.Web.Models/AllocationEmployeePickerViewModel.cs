using System.Collections.Generic;
using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Web.Models;

public class AllocationEmployeePickerViewModel
{
	public string? IdNumber { get; set; }

	public string? EmployeeCode { get; set; }

	public string? FirstName { get; set; }

	public string? LastName { get; set; }

	public List<User> Employees { get; set; } = new List<User>();
}
