using AxiomaReporting.Core.Entities;
using AxiomaReporting.Web.Controllers;
using AxiomaReporting.Web.Models;
using FluentAssertions;
using ProgramEntity = AxiomaReporting.Core.Entities.Program;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Verifies that each filter on EmployeeListFilterModel narrows the in-memory employee list
/// (client Fix #15 — employee list filter parity with allocation list).
/// </summary>
public class EmployeeListFilterTests
{
  private static List<User> BuildSeed()
  {
    var alice = new User
    {
      Id = 1, EmployeeCode = "E1", IdNumber = "111", FirstName = "Alice", LastName = "Smith",
      RoleId = 1, UserRoleId = 6, StatusId = 1, IsReportingEmployee = true,
      RestDay = 6, AllowFutureReporting = true, Notes = "alpha note", FailedLoginAttempts = 0
    };
    var bob = new User
    {
      Id = 2, EmployeeCode = "E2", IdNumber = "222", FirstName = "Bob", LastName = "Jones",
      RoleId = 1, UserRoleId = 6, StatusId = 1, RestDay = 5, AllowFutureReporting = false,
      Notes = "beta note", FailedLoginAttempts = 0
    };
    var carol = new User
    {
      Id = 3, EmployeeCode = "E3", IdNumber = "333", FirstName = "Carol", LastName = "Smith",
      RoleId = 2, UserRoleId = 6, StatusId = 3, RestDay = 0, FailedLoginAttempts = 5
    };

    alice.Allocations.Add(new Allocation
    {
      Id = 100, UserId = 1, ProjectId = 1, IsActive = true,
      Project = new Project { Id = 1, Description = "Zoo Project" },
      AllocationDistricts = new List<AllocationDistrict> { new() { AllocationId = 100, DistrictId = 10, District = new District { Id = 10, Description = "North" } } },
      AllocationPrograms = new List<AllocationProgram> { new() { AllocationId = 100, ProgramId = 20, Program = new ProgramEntity { Id = 20, Description = "Zoo Program" } } },
      AllocationSectors = new List<AllocationSector> { new() { AllocationId = 100, SectorId = 30, Sector = new Sector { Id = 30, Description = "State" } } }
    });
    bob.Allocations.Add(new Allocation
    {
      Id = 101, UserId = 2, ProjectId = 2, IsActive = true,
      Project = new Project { Id = 2, Description = "Alpha Project" },
      AllocationDistricts = new List<AllocationDistrict> { new() { AllocationId = 101, DistrictId = 11, District = new District { Id = 11, Description = "Central" } } },
      AllocationPrograms = new List<AllocationProgram> { new() { AllocationId = 101, ProgramId = 21, Program = new ProgramEntity { Id = 21, Description = "Alpha Program" } } },
      AllocationSectors = new List<AllocationSector> { new() { AllocationId = 101, SectorId = 31, Sector = new Sector { Id = 31, Description = "Arab" } } }
    });
    // Carol has no allocations.

    return new List<User> { alice, bob, carol };
  }

  private static List<User> Apply(EmployeeListFilterModel f)
  {
    f.Normalize();
    return EmployeeController.ApplyEmployeeFilters(BuildSeed(), f);
  }

  [Fact]
  public void IdNumberFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { IdNumber = "222" });
    r.Should().ContainSingle().Which.FirstName.Should().Be("Bob");
  }

  [Fact]
  public void EmployeeCodeFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { EmployeeCode = "E1" });
    r.Should().ContainSingle().Which.Id.Should().Be(1);
  }

  [Fact]
  public void FirstNameFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { FirstName = "Alice" });
    r.Should().ContainSingle().Which.Id.Should().Be(1);
  }

  [Fact]
  public void LastNameFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { LastName = "Smith" });
    r.Select(u => u.Id).Should().BeEquivalentTo(new[] { 1, 3 });
  }

  [Fact]
  public void NotesFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { Notes = "alpha" });
    r.Should().ContainSingle().Which.Id.Should().Be(1);
  }

  [Fact]
  public void RestDayFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { RestDay = 6 });
    r.Should().ContainSingle().Which.Id.Should().Be(1);
  }

  [Fact]
  public void AllowFutureReportingTrue_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { AllowFutureReporting = true });
    r.Should().ContainSingle().Which.Id.Should().Be(1);
  }

  [Fact]
  public void AllowFutureReportingFalse_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { AllowFutureReporting = false });
    r.Select(u => u.Id).Should().BeEquivalentTo(new[] { 2, 3 });
  }

  [Fact]
  public void LockedOnly_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { LockedOnly = true });
    r.Should().ContainSingle().Which.Id.Should().Be(3);
  }

  [Fact]
  public void HasAllocationsTrue_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { HasAllocations = true });
    r.Select(u => u.Id).Should().BeEquivalentTo(new[] { 1, 2 });
  }

  [Fact]
  public void HasAllocationsFalse_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { HasAllocations = false });
    r.Should().ContainSingle().Which.Id.Should().Be(3);
  }

  [Fact]
  public void ProjectIdFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { ProjectId = 1 });
    r.Should().ContainSingle().Which.Id.Should().Be(1);
  }

  [Fact]
  public void DistrictIdsFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { DistrictIds = new() { 11 } });
    r.Should().ContainSingle().Which.Id.Should().Be(2);
  }

  [Fact]
  public void ProgramIdsFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { ProgramIds = new() { 20 } });
    r.Should().ContainSingle().Which.Id.Should().Be(1);
  }

  [Fact]
  public void SectorIdsFilter_NarrowsResults()
  {
    var r = Apply(new EmployeeListFilterModel { SectorIds = new() { 31 } });
    r.Should().ContainSingle().Which.Id.Should().Be(2);
  }

  [Theory]
  [InlineData("projects")]
  [InlineData("districts")]
  [InlineData("programs")]
  [InlineData("sectors")]
  public void AllocationDerivedColumns_AreSortable(string sortBy)
  {
    var sorted = EmployeeController.ApplyEmployeeSort(BuildSeed(), sortBy, sortDesc: false);

    sorted.Where(u => u.Allocations.Any(a => a.IsActive))
      .Select(u => u.Id)
      .Should().Equal(2, 1);
  }
}
