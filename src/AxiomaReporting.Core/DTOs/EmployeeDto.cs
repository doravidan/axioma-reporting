using System.ComponentModel.DataAnnotations;

namespace AxiomaReporting.Core.DTOs;

public class EmployeeDto
{
  public int Id { get; set; }

  [Required(ErrorMessage = "שדה חובה")]
  [StringLength(50, ErrorMessage = "ערך ארוך מדי (מקסימום 50 תווים)")]
  [RegularExpression(@"^\d+$", ErrorMessage = "קוד עובד חייב להכיל ספרות בלבד")]
  [Display(Name = "קוד עובד")]
  public string EmployeeCode { get; set; } = string.Empty;

  [Required(ErrorMessage = "שדה חובה")]
  [StringLength(100, ErrorMessage = "ערך ארוך מדי (מקסימום 100 תווים)")]
  [Display(Name = "שם פרטי")]
  public string FirstName { get; set; } = string.Empty;

  [Required(ErrorMessage = "שדה חובה")]
  [StringLength(100, ErrorMessage = "ערך ארוך מדי (מקסימום 100 תווים)")]
  [Display(Name = "שם משפחה")]
  public string LastName { get; set; } = string.Empty;

  [Required(ErrorMessage = "שדה חובה")]
  [StringLength(20, ErrorMessage = "ערך ארוך מדי (מקסימום 20 תווים)")]
  [Display(Name = "תעודת זהות")]
  public string IdNumber { get; set; } = string.Empty;

  [Required(ErrorMessage = "שדה חובה")]
  [Display(Name = "תפקיד עובד")]
  public int RoleId { get; set; }

  [Required(ErrorMessage = "שדה חובה")]
  [Display(Name = "הרשאת מערכת")]
  public int UserRoleId { get; set; }

  [Required(ErrorMessage = "שדה חובה")]
  [Display(Name = "סטטוס")]
  public int StatusId { get; set; }

  [Display(Name = "מדווח פעילות")]
  public bool IsReportingEmployee { get; set; }

  // RestDay validation (allowed values 0/5/6) is enforced by FluentValidation
  // / EmployeeService — see CLIENT_FEEDBACK item #8. Avoid duplicating the rule
  // here so the message stays in one place.
  [Display(Name = "יום מנוחה")]
  public int? RestDay { get; set; }

  [Display(Name = "מאפשר דיווח עתידי")]
  public bool AllowFutureReporting { get; set; }

  [StringLength(2000, ErrorMessage = "הערות ארוכות מדי (מקסימום 2000 תווים)")]
  [Display(Name = "הערות")]
  public string? Notes { get; set; }

  [EmailAddress(ErrorMessage = "כתובת דוא\"ל אינה תקינה")]
  [StringLength(200, ErrorMessage = "ערך ארוך מדי (מקסימום 200 תווים)")]
  [Display(Name = "דוא\"ל")]
  public string? Email { get; set; }

  [StringLength(20, ErrorMessage = "ערך ארוך מדי (מקסימום 20 תווים)")]
  [Display(Name = "טלפון")]
  public string? Phone { get; set; }
}
