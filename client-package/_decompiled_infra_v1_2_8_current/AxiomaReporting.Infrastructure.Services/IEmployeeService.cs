using System.Collections.Generic;
using System.Threading.Tasks;
using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;

namespace AxiomaReporting.Infrastructure.Services;

public interface IEmployeeService
{
	Task<List<User>> GetAllAsync(string? search = null, int? statusId = null, int? roleId = null);

	Task<User?> GetByIdAsync(int id);

	Task<User> CreateAsync(EmployeeDto dto, string createdByIdNumber);

	Task<bool> UpdateAsync(int id, EmployeeDto dto);

	Task<bool> ResetPasswordAsync(int userId, string newPasswordHash);

	Task<List<Allocation>> GetAllocationsAsync(int userId);

	Task<Allocation?> GetAllocationByIdAsync(int allocationId);

	Task<Allocation> CreateAllocationAsync(AllocationDto dto);

	Task<bool> UpdateAllocationAsync(int allocationId, AllocationDto dto);

	Task<bool> DeleteAllocationAsync(int allocationId);
}
