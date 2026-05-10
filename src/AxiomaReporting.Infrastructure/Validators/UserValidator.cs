using System.Text.RegularExpressions;
using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Validators;
using FluentValidation;

namespace AxiomaReporting.Infrastructure.Validators;

/// <summary>
/// FluentValidation rules for the EmployeeDto (Blue Card).
/// All messages are in Hebrew per client requirement (Fix #7).
/// </summary>
public class UserValidator : AbstractValidator<EmployeeDto>
{
  // Israeli phone numbers: landlines (02/03/04/08/09), mobile prefixes (05x, 07x).
  // Optional field — validated only when supplied.
  private static readonly Regex IsraeliPhoneRegex =
    new(@"^0(2|3|4|8|9|5[02-9]|7[2-9])\d{7}$", RegexOptions.Compiled);

  // Rest day server-side allowlist — only Sunday (0), Friday (5), Saturday (6).
  private static readonly HashSet<int> AllowedRestDays = new() { 0, 5, 6 };

  public UserValidator()
  {
    RuleFor(u => u.EmployeeCode)
      .NotEmpty().WithMessage("יש להזין קוד עובד")
      .Matches(@"^\d+$").WithMessage("קוד עובד חייב להכיל ספרות בלבד");

    RuleFor(u => u.FirstName)
      .NotEmpty().WithMessage("יש להזין שם פרטי");

    RuleFor(u => u.LastName)
      .NotEmpty().WithMessage("יש להזין שם משפחה");

    RuleFor(u => u.IdNumber)
      .NotEmpty().WithMessage("יש להזין מספר תעודת זהות")
      .Must(IsraeliIdValidator.IsValid).WithMessage("מספר תעודת זהות אינו תקין");

    RuleFor(u => u.Phone)
      .Must(p => string.IsNullOrWhiteSpace(p) || IsraeliPhoneRegex.IsMatch(p!))
      .WithMessage("מספר טלפון אינו תקין");

    RuleFor(u => u.Email)
      .EmailAddress().When(u => !string.IsNullOrWhiteSpace(u.Email))
      .WithMessage("כתובת דוא\"ל אינה תקינה");

    RuleFor(u => u.RoleId)
      .GreaterThan(0).WithMessage("יש לבחור תפקיד");

    RuleFor(u => u.UserRoleId)
      .GreaterThan(0).WithMessage("יש לבחור תפקיד מערכת");

    RuleFor(u => u.StatusId)
      .GreaterThan(0).WithMessage("יש לבחור סטטוס");

    RuleFor(u => u.RestDay)
      .Must(rd => !rd.HasValue || AllowedRestDays.Contains(rd.Value))
      .WithMessage("יום מנוחה חייב להיות ראשון, שישי או שבת");
  }
}
