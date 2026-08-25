using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;

namespace AxiomaReporting.Tests.Unit;

public class BulkReportActionServiceTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly Mock<IDashboardFilterService> _scope = new();

  public BulkReportActionServiceTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
    _db.Users.Add(User(1));
    _db.ReportingMonths.Add(new ReportingMonth
    {
      Id = 1, Description = "חודש בדיקה", Month = 8, Year = 2026,
      LastReportingDate = DateTime.Today.AddDays(5), IsActive = true
    });
    _db.SaveChanges();
  }

  public void Dispose() => _db.Dispose();

  [Fact]
  public async Task Archive_DefaultPolicy_DeniesProjectManagerWithoutChangingData()
  {
    AddReport(1, 2);
    var sut = CreateService(new BulkReportActionOptions());

    var result = await sut.ArchiveAsync(new[] { 1 }, 1, UserRoleEnum.ProjectManager);

    result.Succeeded.Should().BeFalse();
    _db.Reports.Single().IsArchived.Should().BeFalse();
  }

  [Fact]
  public async Task Archive_WhenOneIdIsUnauthorized_RejectsEntireSelection()
  {
    AddReport(1, 2);
    AddReport(2, 2);
    _scope.Setup(x => x.GetManageableReportIdsAsync(
        It.IsAny<IReadOnlyCollection<int>>(), 1, UserRoleEnum.ProjectManager, It.IsAny<CancellationToken>()))
      .ReturnsAsync((IReadOnlyCollection<int> ids, int _, UserRoleEnum _, CancellationToken _) =>
        ids.Where(id => id == 1).ToList());
    var sut = CreateService(new BulkReportActionOptions { AllowProjectManagersToDelete = true });

    var result = await sut.ArchiveAsync(new[] { 1, 2 }, 1, UserRoleEnum.ProjectManager, "test deletion");

    result.Succeeded.Should().BeFalse();
    result.RejectedReportIds.Should().Equal(2);
    _db.Reports.Should().OnlyContain(r => !r.IsArchived);
  }

  [Fact]
  public async Task Archive_IsIdempotent_DeduplicatesIdsAndWritesAudit()
  {
    AddReport(1, 2);
    AddReport(2, 2, archived: true);
    AllowAll();
    var sut = CreateService(new BulkReportActionOptions());

    var result = await sut.ArchiveAsync(new[] { 1, 1, 2 }, 1, UserRoleEnum.SystemAdmin, "test deletion");

    result.Succeeded.Should().BeTrue();
    result.UpdatedCount.Should().Be(1);
    result.UnchangedCount.Should().Be(1);
    _db.Reports.Should().OnlyContain(r => r.IsArchived);
    _db.AuditLogs.Should().ContainSingle(a => a.Action == "Report.BulkArchive" && a.EntityId == "1");
  }

  [Fact]
  public async Task Archive_WhenPersistenceFails_PersistsNeitherArchiveFlagNorAudit()
  {
    var failure = new FailNextSaveInterceptor();
    var databaseName = Guid.NewGuid().ToString();
    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName)
      .AddInterceptors(failure)
      .Options;
    await using var db = new AppDbContext(options);
    db.Users.Add(User(1));
    db.ReportingMonths.Add(new ReportingMonth
    {
      Id = 1, Description = "Failure month", Month = 4, Year = 2026,
      LastReportingDate = DateTime.Today.AddDays(5), IsActive = true
    });
    db.Reports.Add(new Report
    {
      Id = 1, UserId = 1, ReportingMonthId = 1, StatusId = 2,
      IsArchived = false, CreatedAt = DateTime.UtcNow
    });
    await db.SaveChangesAsync();

    var scope = new Mock<IDashboardFilterService>();
    scope.Setup(x => x.GetManageableReportIdsAsync(
        It.IsAny<IReadOnlyCollection<int>>(), 1, UserRoleEnum.SystemAdmin,
        It.IsAny<CancellationToken>()))
      .ReturnsAsync(new List<int> { 1 });
    var sut = new BulkReportActionService(
      db, scope.Object, new FakeEmailService(), Options.Create(new BulkReportActionOptions()),
      NullLogger<BulkReportActionService>.Instance);
    failure.FailNextSave = true;

    var result = await sut.ArchiveAsync(
      new[] { 1 }, 1, UserRoleEnum.SystemAdmin, "fault injection");

    result.Succeeded.Should().BeFalse();
    db.ChangeTracker.Clear();
    (await db.Reports.SingleAsync()).IsArchived.Should().BeFalse();
    (await db.AuditLogs.CountAsync()).Should().Be(0);
  }

  [Fact]
  public async Task ChangeStatus_EnforcesEntrySubmittedApprovedSequence()
  {
    AddReport(1, 2);
    AllowAll();
    var sut = CreateService(new BulkReportActionOptions());

    var directApprove = await sut.ChangeStatusAsync(new[] { 1 }, 4, 1, UserRoleEnum.SystemAdmin);
    directApprove.Succeeded.Should().BeFalse();
    _db.Reports.Find(1)!.StatusId.Should().Be(2);

    var submit = await sut.ChangeStatusAsync(new[] { 1 }, 3, 1, UserRoleEnum.SystemAdmin);
    submit.Succeeded.Should().BeTrue();
    _db.Reports.Find(1)!.StatusId.Should().Be(3);

    var approve = await sut.ChangeStatusAsync(new[] { 1 }, 4, 1, UserRoleEnum.SystemAdmin);
    approve.Succeeded.Should().BeTrue();
    _db.Reports.Find(1)!.StatusId.Should().Be(4);
    _db.Reports.Find(1)!.ApprovedBy.Should().Be(1);
  }

  [Fact]
  public async Task ChangeStatus_InvalidMixedSelection_DoesNotPartiallyUpdate()
  {
    AddReport(1, 2);
    AddReport(2, 1);
    AllowAll();
    var sut = CreateService(new BulkReportActionOptions());

    var result = await sut.ChangeStatusAsync(new[] { 1, 2 }, 3, 1, UserRoleEnum.SystemAdmin);

    result.Succeeded.Should().BeFalse();
    result.RejectedReportIds.Should().Equal(2);
    _db.Reports.Find(1)!.StatusId.Should().Be(2);
    _db.Reports.Find(2)!.StatusId.Should().Be(1);
  }

  [Fact]
  public async Task ChangeStatus_EmptyAndNonExistingSelections_AreClearFailures()
  {
    AllowAll();
    var sut = CreateService(new BulkReportActionOptions());

    (await sut.ChangeStatusAsync(Array.Empty<int>(), 3, 1, UserRoleEnum.SystemAdmin))
      .Succeeded.Should().BeFalse();
    var missing = await sut.ChangeStatusAsync(new[] { 999 }, 3, 1, UserRoleEnum.SystemAdmin);
    missing.Succeeded.Should().BeFalse();
    missing.RejectedReportIds.Should().Equal(999);
  }

  [Fact]
  public async Task Archive_RequiresReason_AndLeavesReportUntouchedWhenMissing()
  {
    AddReport(1, 2);
    AllowAll();
    var sut = CreateService(new BulkReportActionOptions());

    var result = await sut.ArchiveAsync(new[] { 1 }, 1, UserRoleEnum.SystemAdmin);

    result.Succeeded.Should().BeFalse();
    _db.Reports.Find(1)!.IsArchived.Should().BeFalse();
  }

  [Fact]
  public async Task ReturnApproved_DefaultPolicy_IsAdminOnlyAndReturnsToSubmittedWithAudit()
  {
    AddReport(1, 4);
    _db.Reports.Find(1)!.ApprovedAt = DateTime.UtcNow;
    _db.Reports.Find(1)!.ApprovedBy = 1;
    _db.SaveChanges();
    AllowAll();
    var sut = CreateService(new BulkReportActionOptions());

    (await sut.ReturnApprovedAsync(new[] { 1 }, "correction required", 1, UserRoleEnum.ProjectManager))
      .Succeeded.Should().BeFalse();
    var result = await sut.ReturnApprovedAsync(
      new[] { 1 }, "correction required", 1, UserRoleEnum.SystemAdmin);

    result.Succeeded.Should().BeTrue();
    var report = _db.Reports.Find(1)!;
    report.StatusId.Should().Be(3);
    report.ApprovedAt.Should().BeNull();
    report.ApprovedBy.Should().BeNull();
    _db.AuditLogs.Should().ContainSingle(a =>
      a.Action == "Report.BulkApprovedReturn" && a.Notes!.Contains("correction required"));
  }

  [Fact]
  public async Task ReturnApproved_MixedInvalidSelection_IsTransactionalAllOrNothing()
  {
    AddReport(1, 4);
    AddReport(2, 3);
    AllowAll();
    var sut = CreateService(new BulkReportActionOptions());

    var result = await sut.ReturnApprovedAsync(
      new[] { 1, 2 }, "correction", 1, UserRoleEnum.SystemAdmin);

    result.Succeeded.Should().BeFalse();
    result.RejectedReportIds.Should().Equal(2);
    _db.Reports.Find(1)!.StatusId.Should().Be(4);
    _db.Reports.Find(2)!.StatusId.Should().Be(3);
  }

  private BulkReportActionService CreateService(BulkReportActionOptions options) => new(
    _db, _scope.Object, new FakeEmailService(), Options.Create(options),
    NullLogger<BulkReportActionService>.Instance);

  private void AllowAll() => _scope
    .Setup(x => x.GetManageableReportIdsAsync(
      It.IsAny<IReadOnlyCollection<int>>(), It.IsAny<int>(), It.IsAny<UserRoleEnum>(),
      It.IsAny<CancellationToken>()))
    .ReturnsAsync((IReadOnlyCollection<int> ids, int _, UserRoleEnum _, CancellationToken _) =>
      ids.Where(id => _db.Reports.Any(r => r.Id == id)).Distinct().ToList());

  private void AddReport(int id, int statusId, bool archived = false)
  {
    _db.Reports.Add(new Report
    {
      Id = id, UserId = 1, ReportingMonthId = 1, StatusId = statusId,
      IsArchived = archived, CreatedAt = DateTime.UtcNow
    });
    _db.SaveChanges();
  }

  private static User User(int id) => new()
  {
    Id = id, EmployeeCode = "TEST", IdNumber = "123456782", FirstName = "בדיקה", LastName = "אוטומטית",
    PasswordHash = "hash", RoleId = 1, UserRoleId = 1, StatusId = 1, CreatedAt = DateTime.UtcNow
  };

  private sealed class FailNextSaveInterceptor : SaveChangesInterceptor
  {
    public bool FailNextSave { get; set; }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
      DbContextEventData eventData,
      InterceptionResult<int> result,
      CancellationToken cancellationToken = default)
    {
      if (!FailNextSave) return base.SavingChangesAsync(eventData, result, cancellationToken);
      FailNextSave = false;
      throw new DbUpdateConcurrencyException("Injected persistence failure");
    }
  }
}
