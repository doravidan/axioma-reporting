using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProgramEntity = AxiomaReporting.Core.Entities.Program;

namespace AxiomaReporting.Tests.Unit;

public class EmployeeServiceTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly PasswordService _passwordService = new();
  private readonly EmployeeService _sut;

  public EmployeeServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _sut = new EmployeeService(_db, _passwordService);
    SeedLookups();
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task CreateAsync_CreatesUserWithDefaultPasswordAndPasswordHistory()
  {
    var dto = EmployeeDto("EMP002", "222222222", "New", "Employee");

    var user = await _sut.CreateAsync(dto, "999999999");

    user.Id.Should().BeGreaterThan(0);
    user.CreatedBy.Should().Be(1);
    user.MustChangePassword.Should().BeTrue();
    _passwordService.VerifyPassword(dto.IdNumber, user.PasswordHash).Should().BeTrue();
    _db.PasswordHistories.Should().ContainSingle(h => h.UserId == user.Id);
  }

  [Fact]
  public async Task GetAllAsync_FiltersBySearchStatusAndRole()
  {
    _db.Users.AddRange(
      User(2, "EMP002", "222", "Alpha", "Worker", (int)UserRoleEnum.Employee, statusId: 1),
      User(3, "EMP003", "333", "Beta", "Manager", (int)UserRoleEnum.ProjectManager, statusId: 1),
      User(4, "EMP004", "444", "Inactive", "Worker", (int)UserRoleEnum.Employee, statusId: 2));
    await _db.SaveChangesAsync();

    var bySearch = await _sut.GetAllAsync("Alpha");
    var byStatus = await _sut.GetAllAsync(statusId: 2);
    var byRole = await _sut.GetAllAsync(roleId: (int)UserRoleEnum.ProjectManager);

    bySearch.Should().ContainSingle(u => u.Id == 2);
    byStatus.Should().ContainSingle(u => u.Id == 4);
    byRole.Should().ContainSingle(u => u.Id == 3);
  }

  [Fact]
  public async Task UpdateAndResetPassword_ReturnFalseForMissingAndUpdateExistingUser()
  {
    _db.Users.Add(User(2, "EMP002", "222", "Old", "Name", (int)UserRoleEnum.Employee, statusId: 1));
    await _db.SaveChangesAsync();

    (await _sut.UpdateAsync(999, EmployeeDto("X", "X", "X", "X"))).Should().BeFalse();
    (await _sut.ResetPasswordAsync(999, "new-hash")).Should().BeFalse();

    var update = EmployeeDto("777", "777", "Updated", "User");
    update.Email = "updated@example.test";
    (await _sut.UpdateAsync(2, update)).Should().BeTrue();
    (await _sut.ResetPasswordAsync(2, "new-hash")).Should().BeTrue();

    var user = await _db.Users.FindAsync(2);
    user!.EmployeeCode.Should().Be("777");
    user.Email.Should().Be("updated@example.test");
    user.PasswordHash.Should().Be("new-hash");
    user.MustChangePassword.Should().BeTrue();
  }

  [Fact]
  public async Task CreateUpdateDeleteAllocation_ManagesAllocationAndJunctions()
  {
    _db.Users.Add(User(2, "EMP002", "222", "A", "B", (int)UserRoleEnum.Employee, statusId: 1));
    await _db.SaveChangesAsync();

    var createDto = AllocationDto(userId: 2, projectId: 1);
    createDto.DistrictIds.Add(1);
    createDto.ProgramIds.Add(1);
    createDto.SectorIds.Add(1);
    createDto.OutputDurationValues.AddRange(new[] { 0.5m, 1m });

    var allocation = await _sut.CreateAllocationAsync(createDto);

    allocation.OutputDuration.Should().Be("0.5,1");
    _db.Set<AllocationDistrict>().Should().ContainSingle(x => x.AllocationId == allocation.Id && x.DistrictId == 1);

    var updateDto = AllocationDto(userId: 2, projectId: 2);
    updateDto.DistrictIds.Add(2);
    updateDto.LocalityDistrictNationalIds.Add(1);
    updateDto.OutputDuration = "2";

    (await _sut.UpdateAllocationAsync(allocation.Id, updateDto)).Should().BeTrue();
    (await _sut.UpdateAllocationAsync(999, updateDto)).Should().BeFalse();

    var loaded = await _sut.GetAllocationByIdAsync(allocation.Id);
    loaded!.ProjectId.Should().Be(2);
    loaded.OutputDuration.Should().Be("2");
    _db.Set<AllocationDistrict>().Should().ContainSingle(x => x.AllocationId == allocation.Id && x.DistrictId == 2);
    _db.Set<AllocationLocalityDistrictNational>().Should()
      .ContainSingle(x => x.AllocationId == allocation.Id && x.LocalityDistrictNationalId == 1);

    (await _sut.GetAllocationsAsync(2)).Should().ContainSingle();
    (await _sut.DeleteAllocationAsync(999, 2)).Should().BeFalse();
    (await _sut.DeleteAllocationAsync(allocation.Id, 999)).Should().BeFalse();
    (await _sut.DeleteAllocationAsync(allocation.Id, 2)).Should().BeTrue();
    (await _sut.GetAllocationsAsync(2)).Should().BeEmpty();
  }

  [Fact]
  public async Task UpdateAllocation_DeduplicatesPostedJunctionIds()
  {
    // Regression: Choices can post the same id twice; adding two junction rows
    // with the same composite key used to crash the EF change tracker.
    _db.Users.Add(User(3, "EMP003", "333", "A", "B", (int)UserRoleEnum.Employee, statusId: 1));
    await _db.SaveChangesAsync();

    var createDto = AllocationDto(userId: 3, projectId: 1);
    createDto.ProgramIds.Add(1);
    var allocation = await _sut.CreateAllocationAsync(createDto);

    var updateDto = AllocationDto(userId: 3, projectId: 1);
    updateDto.ProgramIds.AddRange(new[] { 1, 1, 1 });
    updateDto.EducationalProgramIds.AddRange(new[] { 1, 1 });
    updateDto.DistrictIds.AddRange(new[] { 1, 1, 0 });

    (await _sut.UpdateAllocationAsync(allocation.Id, updateDto)).Should().BeTrue();

    _db.Set<AllocationProgram>().Where(x => x.AllocationId == allocation.Id).Should().HaveCount(1);
    _db.Set<AllocationEducationalProgram>().Where(x => x.AllocationId == allocation.Id).Should().HaveCount(1);
    _db.Set<AllocationDistrict>().Where(x => x.AllocationId == allocation.Id).Should().HaveCount(1);
  }

  private static EmployeeDto EmployeeDto(string code, string idNumber, string first, string last) => new()
  {
    EmployeeCode = code,
    IdNumber = idNumber,
    FirstName = first,
    LastName = last,
    RoleId = 1,
    UserRoleId = (int)UserRoleEnum.Employee,
    StatusId = 1,
    IsReportingEmployee = true,
    Email = $"{code}@example.test"
  };

  private static AllocationDto AllocationDto(int userId, int projectId) => new()
  {
    UserId = userId,
    ProjectId = projectId,
    AnnualEmploymentScope = 100,
    MonthlyEmploymentScope = 10,
    DailyEmploymentScope = 9,
    MonthlyRowAllocation = 20,
    AnnualRowAllocation = 200,
    AllowExcelUpload = true
  };

  private static User User(
    int id,
    string code,
    string idNumber,
    string first,
    string last,
    int role,
    int statusId) => new()
  {
    Id = id,
    EmployeeCode = code,
    IdNumber = idNumber,
    FirstName = first,
    LastName = last,
    PasswordHash = "hash",
    UserRoleId = role,
    RoleId = 1,
    StatusId = statusId,
    CreatedAt = DateTime.UtcNow
  };

  private void SeedLookups()
  {
    _db.UserRoles.AddRange(
      new UserRole { Id = (int)UserRoleEnum.Employee, Name = "Employee" },
      new UserRole { Id = (int)UserRoleEnum.ProjectManager, Name = "ProjectManager" });
    _db.UserStatuses.AddRange(
      new UserStatus { Id = 1, Name = "Active" },
      new UserStatus { Id = 2, Name = "Inactive" });
    _db.Roles.Add(new EmployeeRole { Id = 1, Description = "Teacher", IsActive = true });
    _db.Projects.AddRange(
      new Project { Id = 1, Description = "Project 1", IsActive = true },
      new Project { Id = 2, Description = "Project 2", IsActive = true });
    _db.Districts.AddRange(
      new District { Id = 1, Description = "District 1", IsActive = true },
      new District { Id = 2, Description = "District 2", IsActive = true });
    _db.Programs.Add(new ProgramEntity { Id = 1, Description = "Program 1", IsActive = true });
    _db.Sectors.Add(new Sector { Id = 1, Description = "Sector 1", IsActive = true });
    _db.LocalityDistrictNationals.Add(new LocalityDistrictNational { Id = 1, Description = "National", IsActive = true });
    _db.Users.Add(User(1, "ADMIN", "999999999", "Admin", "User", (int)UserRoleEnum.ProjectManager, statusId: 1));
    _db.SaveChanges();
  }
}
