using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
  Task<bool> DeleteAllocationAsync(int allocationId, int userId);
}

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
    var q = _db.Users
      .Include(u => u.Role)
      .Include(u => u.UserRole)
      .Include(u => u.Status)
      .Include(u => u.Allocations.Where(a => a.IsActive)).ThenInclude(a => a.Project)
      .Include(u => u.Allocations.Where(a => a.IsActive)).ThenInclude(a => a.AllocationDistricts).ThenInclude(ad => ad.District)
      .Include(u => u.Allocations.Where(a => a.IsActive)).ThenInclude(a => a.AllocationPrograms).ThenInclude(ap => ap.Program)
      .Include(u => u.Allocations.Where(a => a.IsActive)).ThenInclude(a => a.AllocationSectors).ThenInclude(s => s.Sector)
      .AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
      q = q.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search) ||
                        u.IdNumber.Contains(search) || u.EmployeeCode.Contains(search));

    if (statusId.HasValue) q = q.Where(u => u.StatusId == statusId);
    if (roleId.HasValue) q = q.Where(u => u.UserRoleId == roleId);

    return await q.OrderBy(u => u.LastName).ThenBy(u => u.FirstName).ToListAsync();
  }

  public async Task<User?> GetByIdAsync(int id) =>
    await _db.Users
      .Include(u => u.Role)
      .Include(u => u.UserRole)
      .Include(u => u.Status)
      .FirstOrDefaultAsync(u => u.Id == id);

  public async Task<User> CreateAsync(EmployeeDto dto, string createdByIdNumber)
  {
    var creator = await _db.Users.FirstOrDefaultAsync(u => u.IdNumber == createdByIdNumber);
    // Default password is the employee's ID number
    var defaultHash = _passwordService.HashPassword(dto.IdNumber);

    var user = new User
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
      CreatedBy = creator?.Id
    };

    _db.Users.Add(user);
    await _db.SaveChangesAsync();

    // Seed password history with the default password
    _db.PasswordHistories.Add(new PasswordHistory
    {
      UserId = user.Id,
      PasswordHash = defaultHash,
      CreatedAt = DateTime.UtcNow
    });
    await _db.SaveChangesAsync();

    if (_auditLog != null)
      await _auditLog.LogAsync("Employee.Create", nameof(User), user.Id.ToString(),
        before: null,
        after: Snapshot(user));

    return user;
  }

  private static object Snapshot(User u) => new
  {
    u.Id,
    u.EmployeeCode,
    u.IdNumber,
    u.FirstName,
    u.LastName,
    u.Email,
    u.Phone,
    u.UserRoleId,
    u.StatusId,
    u.IsReportingEmployee,
    u.RestDay,
    u.AllowFutureReporting,
    u.Notes
  };

  public async Task<bool> UpdateAsync(int id, EmployeeDto dto)
  {
    var user = await _db.Users.FindAsync(id);
    if (user == null) return false;

    var previousStatusId = user.StatusId;
    var before = Snapshot(user);

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
      // Detect Deactivate/Reactivate transitions (StatusId 1 = Active, 2 = Inactive)
      if (previousStatusId != user.StatusId && (user.StatusId == 2 || previousStatusId == 2))
      {
        var action = user.StatusId == 2 ? "Employee.Deactivate" : "Employee.Reactivate";
        await _auditLog.LogAsync(action, nameof(User), user.Id.ToString(),
          before: new { StatusId = previousStatusId },
          after: new { StatusId = user.StatusId });
      }
      else
      {
        await _auditLog.LogAsync("Employee.Update", nameof(User), user.Id.ToString(),
          before: before,
          after: Snapshot(user));
      }
    }
    return true;
  }

  public async Task<bool> ResetPasswordAsync(int userId, string newPasswordHash)
  {
    var user = await _db.Users.FindAsync(userId);
    if (user == null) return false;

    user.PasswordHash = newPasswordHash;
    user.MustChangePassword = true;
    user.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();
    return true;
  }

  public async Task<List<Allocation>> GetAllocationsAsync(int userId) =>
    await _db.Allocations
      .AsSplitQuery()
      .Include(a => a.Project)
      .Include(a => a.AllocationDistricts).ThenInclude(ad => ad.District)
      .Include(a => a.AllocationPrograms).ThenInclude(ap => ap.Program)
      .Include(a => a.AllocationSectors).ThenInclude(s => s.Sector)
      .Include(a => a.AllocationLocalities).ThenInclude(x => x.Locality)
      .Include(a => a.AllocationFrameworks).ThenInclude(x => x.Framework)
      .Include(a => a.AllocationSubjects).ThenInclude(x => x.Subject)
      .Include(a => a.AllocationDomains).ThenInclude(x => x.Domain)
      .Include(a => a.AllocationEducationalPrograms).ThenInclude(x => x.EducationalProgram)
      .Include(a => a.AllocationClasses).ThenInclude(x => x.SchoolClass)
      .Include(a => a.AllocationGradeLevels).ThenInclude(x => x.GradeLevel)
      .Include(a => a.AllocationDiscussionCodes).ThenInclude(x => x.DiscussionCode)
      .Include(a => a.AllocationLocalityDistrictNationals).ThenInclude(x => x.LocalityDistrictNational)
      .Where(a => a.UserId == userId && a.IsActive)
      .ToListAsync();

  public async Task<Allocation?> GetAllocationByIdAsync(int allocationId) =>
    await _db.Allocations
      .AsSplitQuery()
      .Include(a => a.Project)
      .Include(a => a.AllocationDistricts).ThenInclude(x => x.District)
      .Include(a => a.AllocationPrograms).ThenInclude(x => x.Program)
      .Include(a => a.AllocationSectors).ThenInclude(x => x.Sector)
      .Include(a => a.AllocationLocalities).ThenInclude(x => x.Locality)
      .Include(a => a.AllocationFrameworks).ThenInclude(x => x.Framework)
      .Include(a => a.AllocationSubjects).ThenInclude(x => x.Subject)
      .Include(a => a.AllocationDomains).ThenInclude(x => x.Domain)
      .Include(a => a.AllocationEducationalPrograms).ThenInclude(x => x.EducationalProgram)
      .Include(a => a.AllocationClasses).ThenInclude(x => x.SchoolClass)
      .Include(a => a.AllocationGradeLevels).ThenInclude(x => x.GradeLevel)
      .Include(a => a.AllocationDiscussionCodes).ThenInclude(x => x.DiscussionCode)
      .Include(a => a.AllocationLocalityDistrictNationals).ThenInclude(x => x.LocalityDistrictNational)
      .FirstOrDefaultAsync(a => a.Id == allocationId);

  public async Task<Allocation> CreateAllocationAsync(AllocationDto dto)
  {
    var allocation = new Allocation
    {
      UserId = dto.UserId,
      ProjectId = dto.ProjectId,
      AnnualEmploymentScope = dto.AnnualEmploymentScope,
      MonthlyEmploymentScope = dto.MonthlyEmploymentScope,
      DailyEmploymentScope = dto.DailyEmploymentScope,
      MonthlyRowAllocation = dto.MonthlyRowAllocation,
      AnnualRowAllocation = dto.AnnualRowAllocation,
      OutputDuration = ComposeOutputDuration(dto),
      ReportTypeId = dto.ReportTypeId,
      AllowExcelUpload = dto.AllowExcelUpload,
      Notes = dto.Notes,
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    };

    _db.Allocations.Add(allocation);
    await _db.SaveChangesAsync();
    await SyncAllocationJunctionsAsync(allocation.Id, dto);

    if (_auditLog != null)
      await _auditLog.LogAsync("Allocation.Create", nameof(Allocation), allocation.Id.ToString(),
        before: null,
        after: new { allocation.Id, allocation.UserId, allocation.ProjectId,
          allocation.AnnualEmploymentScope, allocation.MonthlyEmploymentScope,
          allocation.DailyEmploymentScope, allocation.MonthlyRowAllocation,
          allocation.AnnualRowAllocation, allocation.OutputDuration, allocation.AllowExcelUpload });

    return allocation;
  }

  // משך תפוקה is stored as a comma-separated token list; "ללא הגבלה" is the token
  // the report validator recognizes as "no restriction on the allowed values".
  private static string? ComposeOutputDuration(AllocationDto dto)
  {
    var tokens = dto.OutputDurationValues.Select(v => v.ToString()).ToList();
    if (dto.OutputDurationUnlimited) tokens.Add("ללא הגבלה");
    return tokens.Count > 0 ? string.Join(",", tokens) : dto.OutputDuration;
  }

  public async Task<bool> UpdateAllocationAsync(int allocationId, AllocationDto dto)
  {
    var allocation = await _db.Allocations.FindAsync(allocationId);
    if (allocation == null) return false;

    var before = new { allocation.Id, allocation.UserId, allocation.ProjectId,
      allocation.AnnualEmploymentScope, allocation.MonthlyEmploymentScope,
      allocation.DailyEmploymentScope, allocation.MonthlyRowAllocation,
      allocation.AnnualRowAllocation, allocation.OutputDuration, allocation.AllowExcelUpload };

    allocation.ProjectId = dto.ProjectId;
    allocation.AnnualEmploymentScope = dto.AnnualEmploymentScope;
    allocation.MonthlyEmploymentScope = dto.MonthlyEmploymentScope;
    allocation.DailyEmploymentScope = dto.DailyEmploymentScope;
    allocation.MonthlyRowAllocation = dto.MonthlyRowAllocation;
    allocation.AnnualRowAllocation = dto.AnnualRowAllocation;
    allocation.OutputDuration = ComposeOutputDuration(dto);
    allocation.ReportTypeId = dto.ReportTypeId;
    allocation.AllowExcelUpload = dto.AllowExcelUpload;
    allocation.Notes = dto.Notes;
    allocation.UpdatedAt = DateTime.UtcNow;

    await _db.SaveChangesAsync();
    await SyncAllocationJunctionsAsync(allocationId, dto);

    if (_auditLog != null)
      await _auditLog.LogAsync("Allocation.Update", nameof(Allocation), allocation.Id.ToString(),
        before: before,
        after: new { allocation.Id, allocation.UserId, allocation.ProjectId,
          allocation.AnnualEmploymentScope, allocation.MonthlyEmploymentScope,
          allocation.DailyEmploymentScope, allocation.MonthlyRowAllocation,
          allocation.AnnualRowAllocation, allocation.OutputDuration, allocation.AllowExcelUpload });

    return true;
  }

  public async Task<bool> DeleteAllocationAsync(int allocationId, int userId)
  {
    var allocation = await _db.Allocations
      .FirstOrDefaultAsync(a => a.Id == allocationId && a.UserId == userId && a.IsActive);
    if (allocation == null) return false;

    allocation.IsActive = false;
    allocation.UpdatedAt = DateTime.UtcNow;
    await _db.SaveChangesAsync();

    if (_auditLog != null)
      await _auditLog.LogAsync("Allocation.Delete", nameof(Allocation), allocation.Id.ToString(),
        before: new { allocation.Id, allocation.UserId, allocation.ProjectId, IsActive = true },
        after: new { allocation.Id, IsActive = false });
    return true;
  }

  private async Task SyncAllocationJunctionsAsync(int allocationId, AllocationDto dto)
  {
    // Diff-based sync in a SINGLE SaveChanges: the old delete-all-save-then-add
    // pattern committed the deletions first, so a failure while adding (e.g.
    // duplicate ids posted by a Choices widget) wiped the allocation's scopes.
    // Diffing also tolerates duplicate/zero ids from the form.
    await SyncJunctionSetAsync(allocationId, dto.DistrictIds,
      (AllocationDistrict x) => x.DistrictId, id => new AllocationDistrict { AllocationId = allocationId, DistrictId = id });
    await SyncJunctionSetAsync(allocationId, dto.ProgramIds,
      (AllocationProgram x) => x.ProgramId, id => new AllocationProgram { AllocationId = allocationId, ProgramId = id });
    await SyncJunctionSetAsync(allocationId, dto.SectorIds,
      (AllocationSector x) => x.SectorId, id => new AllocationSector { AllocationId = allocationId, SectorId = id });
    await SyncJunctionSetAsync(allocationId, dto.LocalityIds,
      (AllocationLocality x) => x.LocalityId, id => new AllocationLocality { AllocationId = allocationId, LocalityId = id });
    await SyncJunctionSetAsync(allocationId, dto.FrameworkIds,
      (AllocationFramework x) => x.FrameworkId, id => new AllocationFramework { AllocationId = allocationId, FrameworkId = id });
    await SyncJunctionSetAsync(allocationId, dto.SubjectIds,
      (AllocationSubject x) => x.SubjectId, id => new AllocationSubject { AllocationId = allocationId, SubjectId = id });
    await SyncJunctionSetAsync(allocationId, dto.DomainIds,
      (AllocationDomain x) => x.DomainId, id => new AllocationDomain { AllocationId = allocationId, DomainId = id });
    await SyncJunctionSetAsync(allocationId, dto.EducationalProgramIds,
      (AllocationEducationalProgram x) => x.EducationalProgramId, id => new AllocationEducationalProgram { AllocationId = allocationId, EducationalProgramId = id });
    await SyncJunctionSetAsync(allocationId, dto.ClassIds,
      (AllocationClass x) => x.ClassId, id => new AllocationClass { AllocationId = allocationId, ClassId = id });
    await SyncJunctionSetAsync(allocationId, dto.GradeLevelIds,
      (AllocationGradeLevel x) => x.GradeLevelId, id => new AllocationGradeLevel { AllocationId = allocationId, GradeLevelId = id });
    await SyncJunctionSetAsync(allocationId, dto.DiscussionCodeIds,
      (AllocationDiscussionCode x) => x.DiscussionCodeId, id => new AllocationDiscussionCode { AllocationId = allocationId, DiscussionCodeId = id });
    await SyncJunctionSetAsync(allocationId, dto.LocalityDistrictNationalIds,
      (AllocationLocalityDistrictNational x) => x.LocalityDistrictNationalId, id => new AllocationLocalityDistrictNational { AllocationId = allocationId, LocalityDistrictNationalId = id });

    await _db.SaveChangesAsync();
  }

  private async Task SyncJunctionSetAsync<T>(
    int allocationId,
    IEnumerable<int>? desiredIds,
    Func<T, int> valueId,
    Func<int, T> create) where T : class
  {
    var desired = (desiredIds ?? Enumerable.Empty<int>()).Where(id => id > 0).ToHashSet();

    var existing = await _db.Set<T>()
      .Where(e => EF.Property<int>(e, "AllocationId") == allocationId)
      .ToListAsync();

    _db.Set<T>().RemoveRange(existing.Where(e => !desired.Contains(valueId(e))));

    var present = existing.Select(valueId).ToHashSet();
    foreach (var id in desired.Where(id => !present.Contains(id)))
      _db.Set<T>().Add(create(id));
  }
}
