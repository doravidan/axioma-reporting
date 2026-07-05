using System.ComponentModel.DataAnnotations;

namespace AxiomaReporting.Core.DTOs;

public class ChangePasswordDto
{
	[Required(ErrorMessage = "שדה חובה")]
	[DataType(DataType.Password)]
	[Display(Name = "סיסמה נוכחית")]
	public string CurrentPassword { get; set; } = string.Empty;


	[Required(ErrorMessage = "שדה חובה")]
	[DataType(DataType.Password)]
	[StringLength(100, MinimumLength = 8, ErrorMessage = "הסיסמה חייבת להכיל לפחות 8 תווים")]
	[Display(Name = "סיסמה חדשה")]
	public string NewPassword { get; set; } = string.Empty;


	[Required(ErrorMessage = "שדה חובה")]
	[DataType(DataType.Password)]
	[Compare("NewPassword", ErrorMessage = "הסיסמאות אינן תואמות")]
	[Display(Name = "אישור סיסמה")]
	public string ConfirmPassword { get; set; } = string.Empty;

}
