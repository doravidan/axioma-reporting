using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Tests for allocation CRUD constraints enforced via EmployeeService:
/// - Multiple allocations are allowed for the same employee+project
/// - Soft-delete: DeleteAllocationAsync sets IsActive = false
/// - GetAllocationsAsync returns only active allocations
/// </summary>
public class AllocationConstraintTests : IDisposable
{
    private readonly AppDbContext _db;
    private readonly IEmployeeService _sut;

    public AllocationConstraintTests()
    {
        _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);

        var pwdMock = new Mock<IPasswordService>();
        pwdMock.Setup(p => p.HashPassword(It.IsAny<string>())).Returns("hashed");

        _sut = new EmployeeService(_db, pwdMock.Object);

        SeedBase();
    }

    public void Dispose() => _db.Dispose();

    // -----------------------------------------------------------------------
    // Seed helpers
    // -----------------------------------------------------------------------

    private void SeedBase()
    {
        // Use Id = 100 to avoid colliding with the admin user seeded at Id = 1 via HasData.
        _db.Users.Add(new User
        {
            Id = 100,
            EmployeeCode = "EMP100",
            IdNumber = "111111111",
            FirstName = "Test",
            LastName = "Employee",
            PasswordHash = "hash",
            StatusId = 1,
            UserRoleId = 6,  // Employee role (seeded by HasData)
            CreatedAt = DateTime.UtcNow
        });

        // Seed Project entities that allocations will reference.
        // GetAllocationsAsync does .Include(a => a.Project); in EF in-memory the related
        // entity must exist to be loaded — the allocation row itself still returns but
        // some EF versions require the FK target to exist.
        _db.Projects.AddRange(
            new Project { Id = 100, Description = "Test Project A", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Project { Id = 200, Description = "Test Project B", IsActive = true, CreatedAt = DateTime.UtcNow });

        _db.SaveChanges();
    }

    private AllocationDto MakeDto(int userId = 100, int projectId = 100) => new AllocationDto
    {
        UserId = userId,
        ProjectId = projectId,
        AllowExcelUpload = false,
        IsActive = true
    };

    // -----------------------------------------------------------------------
    // Multiple allocations per employee+project rule
    // -----------------------------------------------------------------------

    [Fact]
    public async Task CreateAllocationAsync_SecondAllocationSameProject_Succeeds()
    {
        var first = await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 100));
        var second = await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 100));

        first.Should().NotBeNull();
        second.Should().NotBeNull();
        second.ProjectId.Should().Be(first.ProjectId);

        var count = await _db.Allocations.CountAsync(a => a.UserId == 100 && a.ProjectId == 100);
        count.Should().Be(2);
    }

    [Fact]
    public async Task CreateAllocationAsync_SecondAllocationDifferentProject_Succeeds()
    {
        // Arrange — first allocation for project 100
        var first = await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 100));

        // Act — second allocation for the same employee but a different project
        var second = await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 200));

        // Assert — both allocations exist and are active
        first.Should().NotBeNull();
        second.Should().NotBeNull();
        first.ProjectId.Should().Be(100);
        second.ProjectId.Should().Be(200);
        second.IsActive.Should().BeTrue();

        var allocations = await _db.Allocations
            .Where(a => a.UserId == 100 && a.IsActive)
            .ToListAsync();
        allocations.Should().HaveCount(2);
        allocations.Select(a => a.ProjectId).Should().BeEquivalentTo(new[] { 100, 200 });
    }

    // -----------------------------------------------------------------------
    // Update existing allocation
    // -----------------------------------------------------------------------

    [Fact]
    public async Task UpdateAllocationAsync_CanUpdateExistingAllocation()
    {
        // Arrange
        var created = await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 100));

        var updateDto = MakeDto(userId: 100, projectId: 100);
        updateDto.MonthlyRowAllocation = 20;
        updateDto.MonthlyEmploymentScope = 100;
        updateDto.Notes = "updated notes";
        updateDto.AllowExcelUpload = true;

        // Act
        var updated = await _sut.UpdateAllocationAsync(created.Id, updateDto);

        // Assert
        updated.Should().BeTrue();
        var saved = await _db.Allocations.FindAsync(created.Id);
        saved.Should().NotBeNull();
        saved!.MonthlyRowAllocation.Should().Be(20);
        saved.MonthlyEmploymentScope.Should().Be(100);
        saved.Notes.Should().Be("updated notes");
        saved.AllowExcelUpload.Should().BeTrue();
        saved.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAllocationAsync_NonExistentAllocation_ReturnsFalse()
    {
        var result = await _sut.UpdateAllocationAsync(999, MakeDto());
        result.Should().BeFalse();
    }

    // -----------------------------------------------------------------------
    // Soft delete
    // -----------------------------------------------------------------------

    [Fact]
    public async Task DeleteAllocationAsync_SoftDeletesAllocation()
    {
        // Arrange
        var created = await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 100));
        created.IsActive.Should().BeTrue();

        // Act
        var deleted = await _sut.DeleteAllocationAsync(created.Id);

        // Assert — IsActive = false (soft delete), record still present
        deleted.Should().BeTrue();
        var record = await _db.Allocations.FindAsync(created.Id);
        record.Should().NotBeNull("soft-delete must not remove the record from the database");
        record!.IsActive.Should().BeFalse();
        record.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteAllocationAsync_NonExistentAllocation_ReturnsFalse()
    {
        var result = await _sut.DeleteAllocationAsync(999);
        result.Should().BeFalse();
    }

    // -----------------------------------------------------------------------
    // GetAllocationsAsync only returns active allocations
    // -----------------------------------------------------------------------

    [Fact]
    public async Task GetAllocationsAsync_OnlyReturnsActiveAllocations()
    {
        // Arrange — create two allocations then soft-delete one
        var a1 = await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 100));
        var a2 = await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 200));
        await _sut.DeleteAllocationAsync(a1.Id);  // soft-delete a1

        // Act
        var active = await _sut.GetAllocationsAsync(userId: 100);

        // Assert — only the non-deleted allocation is returned
        active.Should().ContainSingle(a => a.Id == a2.Id);
        active.Should().NotContain(a => a.Id == a1.Id,
            "soft-deleted allocations must not appear in the active list");
    }

    [Fact]
    public async Task GetAllocationsAsync_ReturnsAllActiveAllocationsForUser()
    {
        // Arrange
        await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 100));
        await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 200));

        // Act
        var allocations = await _sut.GetAllocationsAsync(userId: 100);

        // Assert
        allocations.Should().HaveCount(2);
        allocations.All(a => a.UserId == 100 && a.IsActive).Should().BeTrue();
    }

    [Fact]
    public async Task GetAllocationsAsync_DoesNotReturnOtherUsersAllocations()
    {
        // Arrange — add an allocation for a second user (Id=200 to avoid conflicts)
        _db.Users.Add(new User
        {
            Id = 200,
            EmployeeCode = "EMP200",
            IdNumber = "222222222",
            FirstName = "Other",
            LastName = "User",
            PasswordHash = "hash",
            StatusId = 1,
            UserRoleId = 6,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        await _sut.CreateAllocationAsync(MakeDto(userId: 100, projectId: 100));
        await _sut.CreateAllocationAsync(new AllocationDto { UserId = 200, ProjectId = 100, AllowExcelUpload = false, IsActive = true });

        // Act
        var user100Allocations = await _sut.GetAllocationsAsync(userId: 100);

        // Assert — only user 100's allocations returned
        user100Allocations.Should().OnlyContain(a => a.UserId == 100);
        user100Allocations.Should().HaveCount(1);
    }
}
