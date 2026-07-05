using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Web.Models;

/// <summary>
/// View model for the employee picker step of the allocation creation flow
/// (EmployeeController): search fields plus the matching employees.
/// </summary>
public class AllocationEmployeePickerViewModel
{
  public string? IdNumber { get; set; }

  public string? EmployeeCode { get; set; }

  public string? FirstName { get; set; }

  public string? LastName { get; set; }

  public List<User> Employees { get; set; } = new();
}
