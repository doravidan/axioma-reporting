using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
	private readonly AppDbContext _db;

	private readonly IPasswordService _passwordService;

	private readonly IAuditLogService? _auditLog;

	public EmployeeService(AppDbContext db, IPasswordService passwordService, IAuditLogService? auditLog = null)
	{
		_db = db;
		_passwordService = passwordService;
		_auditLog = auditLog;
	}

	public async Task<List<User>> GetAllAsync(string? search = null, int? statusId = null, int? roleId = null)
	{
		string search2 = search;
		IQueryable<User> source = _db.Users.Include((User u) => u.Role).Include((User u) => u.UserRole).Include((User u) => u.Status)
			.Include((User u) => u.Allocations.Where((Allocation a) => a.IsActive))
			.ThenInclude((Allocation a) => a.Project)
			.Include((User u) => u.Allocations.Where((Allocation a) => a.IsActive))
			.ThenInclude((Allocation a) => a.AllocationDistricts)
			.ThenInclude((AllocationDistrict ad) => ad.District)
			.Include((User u) => u.Allocations.Where((Allocation a) => a.IsActive))
			.ThenInclude((Allocation a) => a.AllocationPrograms)
			.ThenInclude((AllocationProgram ap) => ap.Program)
			.Include((User u) => u.Allocations.Where((Allocation a) => a.IsActive))
			.ThenInclude((Allocation a) => a.AllocationSectors)
			.ThenInclude((AllocationSector s) => s.Sector)
			.AsSplitQuery()
			.AsQueryable();
		if (!string.IsNullOrWhiteSpace(search2))
		{
			source = source.Where((User u) => u.FirstName.Contains(search2) || u.LastName.Contains(search2) || u.IdNumber.Contains(search2) || u.EmployeeCode.Contains(search2));
		}
		if (statusId.HasValue)
		{
			source = source.Where((User u) => (int?)u.StatusId == statusId);
		}
		if (roleId.HasValue)
		{
			source = source.Where((User u) => (int?)u.UserRoleId == roleId);
		}
		return await (from u in source
			orderby u.LastName, u.FirstName
			select u).ToListAsync();
	}

	public async Task<User?> GetByIdAsync(int id)
	{
		return await _db.Users.Include((User u) => u.Role).Include((User u) => u.UserRole).Include((User u) => u.Status)
			.FirstOrDefaultAsync((User u) => u.Id == id);
	}

	public async Task<User> CreateAsync(EmployeeDto dto, string createdByIdNumber)
	{
		string createdByIdNumber2 = createdByIdNumber;
		User user2 = await _db.Users.FirstOrDefaultAsync((User u) => u.IdNumber == createdByIdNumber2);
		string defaultHash = _passwordService.HashPassword(dto.IdNumber);
		User user = new User
		{
			EmployeeCode = dto.EmployeeCode,
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			IdNumber = dto.IdNumber,
			PasswordHash = defaultHash,
			RoleId = dto.RoleId,
			UserRoleId = dto.UserRoleId,
			StatusId = dto.StatusId,
			IsReportingEmployee = dto.IsReportingEmployee,
			RestDay = dto.RestDay,
			AllowFutureReporting = dto.AllowFutureReporting,
			Notes = dto.Notes,
			Email = dto.Email,
			Phone = dto.Phone,
			MustChangePassword = true,
			CreatedAt = DateTime.UtcNow,
			CreatedBy = user2?.Id
		};
		_db.Users.Add(user);
		await _db.SaveChangesAsync();
		_db.PasswordHistories.Add(new PasswordHistory
		{
			UserId = user.Id,
			PasswordHash = defaultHash,
			CreatedAt = DateTime.UtcNow
		});
		await _db.SaveChangesAsync();
		if (_auditLog != null)
		{
			await _auditLog.LogAsync("Employee.Create", "User", user.Id.ToString(), null, Snapshot(user));
		}
		return user;
	}

	private static object Snapshot(User u)
	{
		return new
		{
			u.Id, u.EmployeeCode, u.IdNumber, u.FirstName, u.LastName, u.Email, u.Phone, u.UserRoleId, u.StatusId, u.IsReportingEmployee,
			u.RestDay, u.AllowFutureReporting, u.Notes
		};
	}

	public async Task<bool> UpdateAsync(int id, EmployeeDto dto)
	{
		User user = await _db.Users.FindAsync(id);
		if (user == null)
		{
			return false;
		}
		int previousStatusId = user.StatusId;
		object before = Snapshot(user);
		user.EmployeeCode = dto.EmployeeCode;
		user.FirstName = dto.FirstName;
		user.LastName = dto.LastName;
		user.IdNumber = dto.IdNumber;
		user.RoleId = dto.RoleId;
		user.UserRoleId = dto.UserRoleId;
		user.StatusId = dto.StatusId;
		user.IsReportingEmployee = dto.IsReportingEmployee;
		user.RestDay = dto.RestDay;
		user.AllowFutureReporting = dto.AllowFutureReporting;
		user.Notes = dto.Notes;
		user.Email = dto.Email;
		user.Phone = dto.Phone;
		user.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		if (_auditLog != null)
		{
			if (previousStatusId == user.StatusId || (user.StatusId != 2 && previousStatusId != 2))
			{
				await _auditLog.LogAsync("Employee.Update", "User", user.Id.ToString(), before, Snapshot(user));
			}
			else
			{
				string action = ((user.StatusId == 2) ? "Employee.Deactivate" : "Employee.Reactivate");
				await _auditLog.LogAsync(action, "User", user.Id.ToString(), new
				{
					StatusId = previousStatusId
				}, new { user.StatusId });
			}
		}
		return true;
	}

	public async Task<bool> ResetPasswordAsync(int userId, string newPasswordHash)
	{
		User user = await _db.Users.FindAsync(userId);
		if (user == null)
		{
			return false;
		}
		user.PasswordHash = newPasswordHash;
		user.MustChangePassword = true;
		user.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		return true;
	}

	public async Task<List<Allocation>> GetAllocationsAsync(int userId)
	{
		return await (from a in _db.Allocations.Include((Allocation a) => a.Project).Include((Allocation a) => a.AllocationDistricts).ThenInclude((AllocationDistrict ad) => ad.District)
				.Include((Allocation a) => a.AllocationPrograms)
				.ThenInclude((AllocationProgram ap) => ap.Program)
				.Include((Allocation a) => a.AllocationSectors)
				.ThenInclude((AllocationSector s) => s.Sector)
				.Include((Allocation a) => a.AllocationLocalities)
				.ThenInclude((AllocationLocality x) => x.Locality)
				.Include((Allocation a) => a.AllocationFrameworks)
				.ThenInclude((AllocationFramework x) => x.Framework)
				.Include((Allocation a) => a.AllocationSubjects)
				.ThenInclude((AllocationSubject x) => x.Subject)
				.Include((Allocation a) => a.AllocationDomains)
				.ThenInclude((AllocationDomain x) => x.Domain)
				.Include((Allocation a) => a.AllocationEducationalPrograms)
				.ThenInclude((AllocationEducationalProgram x) => x.EducationalProgram)
				.Include((Allocation a) => a.AllocationClasses)
				.ThenInclude((AllocationClass x) => x.SchoolClass)
				.Include((Allocation a) => a.AllocationGradeLevels)
				.ThenInclude((AllocationGradeLevel x) => x.GradeLevel)
				.Include((Allocation a) => a.AllocationDiscussionCodes)
				.ThenInclude((AllocationDiscussionCode x) => x.DiscussionCode)
				.Include((Allocation a) => a.AllocationLocalityDistrictNationals)
				.ThenInclude((AllocationLocalityDistrictNational x) => x.LocalityDistrictNational)
				.AsSplitQuery()
			where a.UserId == userId && a.IsActive
			select a).ToListAsync();
	}

	public async Task<Allocation?> GetAllocationByIdAsync(int allocationId)
	{
		return await _db.Allocations.Include((Allocation a) => a.Project).Include((Allocation a) => a.AllocationDistricts).ThenInclude((AllocationDistrict x) => x.District)
			.Include((Allocation a) => a.AllocationPrograms)
			.ThenInclude((AllocationProgram x) => x.Program)
			.Include((Allocation a) => a.AllocationSectors)
			.ThenInclude((AllocationSector x) => x.Sector)
			.Include((Allocation a) => a.AllocationLocalities)
			.ThenInclude((AllocationLocality x) => x.Locality)
			.Include((Allocation a) => a.AllocationFrameworks)
			.ThenInclude((AllocationFramework x) => x.Framework)
			.Include((Allocation a) => a.AllocationSubjects)
			.ThenInclude((AllocationSubject x) => x.Subject)
			.Include((Allocation a) => a.AllocationDomains)
			.ThenInclude((AllocationDomain x) => x.Domain)
			.Include((Allocation a) => a.AllocationEducationalPrograms)
			.ThenInclude((AllocationEducationalProgram x) => x.EducationalProgram)
			.Include((Allocation a) => a.AllocationClasses)
			.ThenInclude((AllocationClass x) => x.SchoolClass)
			.Include((Allocation a) => a.AllocationGradeLevels)
			.ThenInclude((AllocationGradeLevel x) => x.GradeLevel)
			.Include((Allocation a) => a.AllocationDiscussionCodes)
			.ThenInclude((AllocationDiscussionCode x) => x.DiscussionCode)
			.Include((Allocation a) => a.AllocationLocalityDistrictNationals)
			.ThenInclude((AllocationLocalityDistrictNational x) => x.LocalityDistrictNational)
			.AsSplitQuery()
			.FirstOrDefaultAsync((Allocation a) => a.Id == allocationId);
	}

	public async Task<Allocation> CreateAllocationAsync(AllocationDto dto)
	{
		Allocation allocation = new Allocation
		{
			UserId = dto.UserId,
			ProjectId = dto.ProjectId,
			ReportTypeId = dto.ReportTypeId,
			AnnualEmploymentScope = dto.AnnualEmploymentScope,
			MonthlyEmploymentScope = dto.MonthlyEmploymentScope,
			DailyEmploymentScope = dto.DailyEmploymentScope,
			MonthlyRowAllocation = dto.MonthlyRowAllocation,
			AnnualRowAllocation = dto.AnnualRowAllocation,
			OutputDuration = ((dto.OutputDurationValues.Count > 0) ? string.Join(",", dto.OutputDurationValues) : dto.OutputDuration),
			AllowExcelUpload = dto.AllowExcelUpload,
			Notes = dto.Notes,
			IsActive = true,
			CreatedAt = DateTime.UtcNow
		};
		_db.Allocations.Add(allocation);
		await _db.SaveChangesAsync();
		await SyncAllocationJunctionsAsync(allocation.Id, dto);
		if (_auditLog != null)
		{
			await _auditLog.LogAsync("Allocation.Create", "Allocation", allocation.Id.ToString(), null, new { allocation.Id, allocation.UserId, allocation.ProjectId, allocation.ReportTypeId, allocation.AnnualEmploymentScope, allocation.MonthlyEmploymentScope, allocation.DailyEmploymentScope, allocation.MonthlyRowAllocation, allocation.AnnualRowAllocation, allocation.OutputDuration, allocation.AllowExcelUpload });
		}
		return allocation;
	}

	public async Task<bool> UpdateAllocationAsync(int allocationId, AllocationDto dto)
	{
		Allocation allocation = await _db.Allocations.FindAsync(allocationId);
		if (allocation == null)
		{
			return false;
		}
		var before = new { allocation.Id, allocation.UserId, allocation.ProjectId, allocation.ReportTypeId, allocation.AnnualEmploymentScope, allocation.MonthlyEmploymentScope, allocation.DailyEmploymentScope, allocation.MonthlyRowAllocation, allocation.AnnualRowAllocation, allocation.OutputDuration, allocation.AllowExcelUpload };
		allocation.ProjectId = dto.ProjectId;
		allocation.ReportTypeId = dto.ReportTypeId;
		allocation.AnnualEmploymentScope = dto.AnnualEmploymentScope;
		allocation.MonthlyEmploymentScope = dto.MonthlyEmploymentScope;
		allocation.DailyEmploymentScope = dto.DailyEmploymentScope;
		allocation.MonthlyRowAllocation = dto.MonthlyRowAllocation;
		allocation.AnnualRowAllocation = dto.AnnualRowAllocation;
		allocation.OutputDuration = ((dto.OutputDurationValues.Count > 0) ? string.Join(",", dto.OutputDurationValues) : dto.OutputDuration);
		allocation.AllowExcelUpload = dto.AllowExcelUpload;
		allocation.Notes = dto.Notes;
		allocation.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		await SyncAllocationJunctionsAsync(allocationId, dto);
		if (_auditLog != null)
		{
			await _auditLog.LogAsync("Allocation.Update", "Allocation", allocation.Id.ToString(), before, new { allocation.Id, allocation.UserId, allocation.ProjectId, allocation.ReportTypeId, allocation.AnnualEmploymentScope, allocation.MonthlyEmploymentScope, allocation.DailyEmploymentScope, allocation.MonthlyRowAllocation, allocation.AnnualRowAllocation, allocation.OutputDuration, allocation.AllowExcelUpload });
		}
		return true;
	}

	public async Task<bool> DeleteAllocationAsync(int allocationId)
	{
		Allocation allocation = await _db.Allocations.FindAsync(allocationId);
		if (allocation == null)
		{
			return false;
		}
		allocation.IsActive = false;
		allocation.UpdatedAt = DateTime.UtcNow;
		await _db.SaveChangesAsync();
		if (_auditLog != null)
		{
			await _auditLog.LogAsync("Allocation.Delete", "Allocation", allocation.Id.ToString(), new
			{
				Id = allocation.Id,
				UserId = allocation.UserId,
				ProjectId = allocation.ProjectId,
				IsActive = true
			}, new
			{
				Id = allocation.Id,
				IsActive = false
			});
		}
		return true;
	}

	private async Task SyncAllocationJunctionsAsync(int allocationId, AllocationDto dto)
	{
		_db.Set<AllocationDistrict>().RemoveRange(from x in _db.Set<AllocationDistrict>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationProgram>().RemoveRange(from x in _db.Set<AllocationProgram>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationSector>().RemoveRange(from x in _db.Set<AllocationSector>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationLocality>().RemoveRange(from x in _db.Set<AllocationLocality>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationFramework>().RemoveRange(from x in _db.Set<AllocationFramework>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationSubject>().RemoveRange(from x in _db.Set<AllocationSubject>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationDomain>().RemoveRange(from x in _db.Set<AllocationDomain>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationEducationalProgram>().RemoveRange(from x in _db.Set<AllocationEducationalProgram>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationClass>().RemoveRange(from x in _db.Set<AllocationClass>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationGradeLevel>().RemoveRange(from x in _db.Set<AllocationGradeLevel>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationDiscussionCode>().RemoveRange(from x in _db.Set<AllocationDiscussionCode>()
			where x.AllocationId == allocationId
			select x);
		_db.Set<AllocationLocalityDistrictNational>().RemoveRange(from x in _db.Set<AllocationLocalityDistrictNational>()
			where x.AllocationId == allocationId
			select x);
		await _db.SaveChangesAsync();
		foreach (int districtId in dto.DistrictIds)
		{
			_db.Set<AllocationDistrict>().Add(new AllocationDistrict
			{
				AllocationId = allocationId,
				DistrictId = districtId
			});
		}
		foreach (int programId in dto.ProgramIds)
		{
			_db.Set<AllocationProgram>().Add(new AllocationProgram
			{
				AllocationId = allocationId,
				ProgramId = programId
			});
		}
		foreach (int sectorId in dto.SectorIds)
		{
			_db.Set<AllocationSector>().Add(new AllocationSector
			{
				AllocationId = allocationId,
				SectorId = sectorId
			});
		}
		foreach (int localityId in dto.LocalityIds)
		{
			_db.Set<AllocationLocality>().Add(new AllocationLocality
			{
				AllocationId = allocationId,
				LocalityId = localityId
			});
		}
		foreach (int frameworkId in dto.FrameworkIds)
		{
			_db.Set<AllocationFramework>().Add(new AllocationFramework
			{
				AllocationId = allocationId,
				FrameworkId = frameworkId
			});
		}
		foreach (int subjectId in dto.SubjectIds)
		{
			_db.Set<AllocationSubject>().Add(new AllocationSubject
			{
				AllocationId = allocationId,
				SubjectId = subjectId
			});
		}
		foreach (int domainId in dto.DomainIds)
		{
			_db.Set<AllocationDomain>().Add(new AllocationDomain
			{
				AllocationId = allocationId,
				DomainId = domainId
			});
		}
		foreach (int educationalProgramId in dto.EducationalProgramIds)
		{
			_db.Set<AllocationEducationalProgram>().Add(new AllocationEducationalProgram
			{
				AllocationId = allocationId,
				EducationalProgramId = educationalProgramId
			});
		}
		foreach (int classId in dto.ClassIds)
		{
			_db.Set<AllocationClass>().Add(new AllocationClass
			{
				AllocationId = allocationId,
				ClassId = classId
			});
		}
		foreach (int gradeLevelId in dto.GradeLevelIds)
		{
			_db.Set<AllocationGradeLevel>().Add(new AllocationGradeLevel
			{
				AllocationId = allocationId,
				GradeLevelId = gradeLevelId
			});
		}
		foreach (int discussionCodeId in dto.DiscussionCodeIds)
		{
			_db.Set<AllocationDiscussionCode>().Add(new AllocationDiscussionCode
			{
				AllocationId = allocationId,
				DiscussionCodeId = discussionCodeId
			});
		}
		foreach (int localityDistrictNationalId in dto.LocalityDistrictNationalIds)
		{
			_db.Set<AllocationLocalityDistrictNational>().Add(new AllocationLocalityDistrictNational
			{
				AllocationId = allocationId,
				LocalityDistrictNationalId = localityDistrictNationalId
			});
		}
		await _db.SaveChangesAsync();
	}
}
