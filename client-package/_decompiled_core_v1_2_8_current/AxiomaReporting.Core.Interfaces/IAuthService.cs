using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Core.Interfaces;

public interface IAuthService
{
	Task<(bool Success, string? ErrorMessage, User? User)> ValidateLoginAsync(string idNumber, string password);

	Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

	Task RecordFailedLoginAsync(string idNumber);

	Task ResetFailedLoginsAsync(int userId);

	Task<bool> IsPasswordInHistoryAsync(int userId, string newPassword);

	Task AddPasswordToHistoryAsync(int userId, string passwordHash);
}
