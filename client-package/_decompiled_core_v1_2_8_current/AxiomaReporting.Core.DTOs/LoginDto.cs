using System.ComponentModel.DataAnnotations;

namespace AxiomaReporting.Core.DTOs;

public class LoginDto
{
	[Required(ErrorMessage = "שדה חובה")]
	[Display(Name = "תעודת זהות")]
	public string IdNumber { get; set; } = string.Empty;


	[Required(ErrorMessage = "שדה חובה")]
	[Display(Name = "סיסמה")]
	[DataType(DataType.Password)]
	public string Password { get; set; } = string.Empty;


	[Display(Name = "זכור אותי")]
	public bool RememberMe { get; set; }
}
