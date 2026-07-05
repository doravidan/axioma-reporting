using System.Collections.Generic;
using System.Text.RegularExpressions;
using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Validators;
using FluentValidation;

namespace AxiomaReporting.Infrastructure.Validators;

public class UserValidator : AbstractValidator<EmployeeDto>
{
	private static readonly Regex IsraeliPhoneRegex = new Regex("^0(2|3|4|8|9|5[02-9]|7[2-9])\\d{7}$", RegexOptions.Compiled);

	private static readonly HashSet<int> AllowedRestDays = new HashSet<int> { 0, 5, 6 };

	public UserValidator()
	{
		RuleFor((EmployeeDto u) => u.EmployeeCode).NotEmpty().WithMessage("יש להזין קוד עובד").Matches("^\\d+$")
			.WithMessage("קוד עובד חייב להכיל ספרות בלבד");
		RuleFor((EmployeeDto u) => u.FirstName).NotEmpty().WithMessage("יש להזין שם פרטי");
		RuleFor((EmployeeDto u) => u.LastName).NotEmpty().WithMessage("יש להזין שם משפחה");
		RuleFor((EmployeeDto u) => u.IdNumber).NotEmpty().WithMessage("יש להזין מספר תעודת זהות").Must(IsraeliIdValidator.IsValid)
			.WithMessage("מספר תעודת זהות אינו תקין");
		RuleFor((EmployeeDto u) => u.Phone).Must((string p) => string.IsNullOrWhiteSpace(p) || IsraeliPhoneRegex.IsMatch(p)).WithMessage("מספר טלפון אינו תקין");
		RuleFor((EmployeeDto u) => u.Email).EmailAddress().When<EmployeeDto, string>((EmployeeDto u) => !string.IsNullOrWhiteSpace(u.Email)).WithMessage("כתובת דוא\"ל אינה תקינה");
		RuleFor((EmployeeDto u) => u.RoleId).GreaterThan(0).WithMessage("יש לבחור תפקיד");
		RuleFor((EmployeeDto u) => u.UserRoleId).GreaterThan(0).WithMessage("יש לבחור תפקיד מערכת");
		RuleFor((EmployeeDto u) => u.StatusId).GreaterThan(0).WithMessage("יש לבחור סטטוס");
		RuleFor((EmployeeDto u) => u.RestDay).Must((int? rd) => !rd.HasValue || AllowedRestDays.Contains(rd.Value)).WithMessage("יום מנוחה חייב להיות ראשון, שישי או שבת");
	}
}
