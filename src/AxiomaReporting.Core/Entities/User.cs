using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class User : BaseEntity
{
  public string EmployeeCode { get; set; } = string.Empty;
  public string IdNumber { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
  public int RoleId { get; set; }
  public EmployeeRole? Role { get; set; }
  public int UserRoleId { get; set; }
  public UserRole? UserRole { get; set; }
  public int StatusId { get; set; }
  public UserStatus? Status { get; set; }
  public bool IsReportingEmployee { get; set; } = false;
  public int? RestDay { get; set; }
  public bool AllowFutureReporting { get; set; } = false;
  public string? Notes { get; set; }
  public string? Email { get; set; }
  public string? Phone { get; set; }
  public bool MustChangePassword { get; set; } = true;
  public int FailedLoginAttempts { get; set; } = 0;
  public DateTime? LastPasswordChange { get; set; }
  public bool AcceptedTermsOfUse { get; set; } = false;
  public int? CreatedBy { get; set; }
  public int? UpdatedBy { get; set; }
  public byte[] RowVersion { get; set; } = Array.Empty<byte>();

  public ICollection<PasswordHistory> PasswordHistories { get; set; } = new List<PasswordHistory>();
  public ICollection<Allocation> Allocations { get; set; } = new List<Allocation>();
  public ICollection<Report> Reports { get; set; } = new List<Report>();
}
