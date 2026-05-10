using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Tests.TestSupport;
using AxiomaReporting.Web.Controllers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Tests.Unit;

public class LookupControllerFkCheckTests : IDisposable
{
  private readonly AppDbContext _db;
  private readonly LookupController _sut;

  public LookupControllerFkCheckTests()
  {
    _db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(Guid.NewGuid().ToString())
      .Options);
    _sut = new LookupController(_db, new FakeAuditLogService());
  }

  public void Dispose() => _db.Dispose();

  // --- districts — also referenced by Institutions & InspectorAssignments ---

  [Fact]
  public async Task CanDelete_Districts_BlockedWhenUsedByInstitution()
  {
    _db.Institutions.Add(new Institution { InstitutionSymbol = 1, Name = "x", DistrictId = 42, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("districts", 42);

    can.Should().BeFalse();
    reason.Should().Contain("מוסדות");
  }

  [Fact]
  public async Task CanDelete_Districts_BlockedWhenUsedByInspectorAssignment()
  {
    _db.InspectorAssignments.Add(new InspectorAssignment { InspectorUserId = 1, DistrictId = 7 });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("districts", 7);

    can.Should().BeFalse();
    reason.Should().Contain("שיוכי פיקוח");
  }

  // --- sectors ---

  [Fact]
  public async Task CanDelete_Sectors_BlockedWhenUsedByInstitution()
  {
    _db.Institutions.Add(new Institution { InstitutionSymbol = 1, Name = "x", SectorId = 5, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var (can, _) = await _sut.CanDeleteItemAsync("sectors", 5);

    can.Should().BeFalse();
  }

  // --- localities — Institutions.LocalityId ---

  [Fact]
  public async Task CanDelete_Localities_BlockedWhenUsedByInstitution()
  {
    _db.Institutions.Add(new Institution { InstitutionSymbol = 2, Name = "x", LocalityId = 9, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("localities", 9);

    can.Should().BeFalse();
    reason.Should().Contain("מוסדות");
  }

  // --- authorities — no FK references → always allowed ---

  [Fact]
  public async Task CanDelete_Authorities_AllowedWhenNoUsage()
  {
    _db.Authorities.Add(new Authority { Id = 50, Description = "Auth", IsActive = true, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("authorities", 50);

    can.Should().BeTrue();
    reason.Should().BeNull();
  }

  // --- frameworks ---

  [Fact]
  public async Task CanDelete_Frameworks_BlockedWhenReferencedByReportRow()
  {
    SeedReportRowWithFramework(frameworkId: 100, conclusionFrameworkId: null);

    var (can, reason) = await _sut.CanDeleteItemAsync("frameworks", 100);

    can.Should().BeFalse();
    reason.Should().Contain("דיווחים");
  }

  [Fact]
  public async Task CanDelete_Frameworks_BlockedWhenReferencedByConclusionFramework()
  {
    SeedReportRowWithFramework(frameworkId: 1, conclusionFrameworkId: 200);

    var (can, reason) = await _sut.CanDeleteItemAsync("frameworks", 200);

    can.Should().BeFalse();
    reason.Should().Contain("סיכום");
  }

  [Fact]
  public async Task CanDelete_Frameworks_BlockedWhenReferencedByAllocationFramework()
  {
    _db.Set<AllocationFramework>().Add(new AllocationFramework { AllocationId = 1, FrameworkId = 300 });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("frameworks", 300);

    can.Should().BeFalse();
    reason.Should().Contain("הקצאות");
  }

  [Fact]
  public async Task CanDelete_Frameworks_AllowedWhenNoUsage()
  {
    var (can, _) = await _sut.CanDeleteItemAsync("frameworks", 999);
    can.Should().BeTrue();
  }

  // --- institutions — referenced by Frameworks via (symbol, stage) ---

  [Fact]
  public async Task CanDelete_Institutions_BlockedWhenFrameworkUsesSameSymbolAndStage()
  {
    _db.Institutions.Add(new Institution { Id = 77, InstitutionSymbol = 1234, EducationalStageId = 3, Name = "Inst", CreatedAt = DateTime.UtcNow });
    _db.Frameworks.Add(new Framework { Description = "FX", InstitutionSymbol = "1234", EducationalStageId = 3, IsActive = true, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("institutions", 77);

    can.Should().BeFalse();
    reason.Should().Contain("מסגרות");
  }

  [Fact]
  public async Task CanDelete_Institutions_AllowedWhenNoMatchingFramework()
  {
    _db.Institutions.Add(new Institution { Id = 78, InstitutionSymbol = 5678, EducationalStageId = 4, Name = "Inst2", CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var (can, _) = await _sut.CanDeleteItemAsync("institutions", 78);

    can.Should().BeTrue();
  }

  // --- educationalstages ---

  [Fact]
  public async Task CanDelete_EducationalStages_BlockedByFramework()
  {
    _db.Frameworks.Add(new Framework { Description = "F", InstitutionSymbol = "1", EducationalStageId = 11, IsActive = true, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("educationalstages", 11);

    can.Should().BeFalse();
    reason.Should().Contain("מסגרות");
  }

  [Fact]
  public async Task CanDelete_EducationalStages_BlockedByInstitution()
  {
    _db.Institutions.Add(new Institution { InstitutionSymbol = 1, Name = "x", EducationalStageId = 12, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("educationalstages", 12);

    can.Should().BeFalse();
    reason.Should().Contain("מוסדות");
  }

  [Fact]
  public async Task CanDelete_EducationalStages_AllowedWhenNoUsage()
  {
    var (can, _) = await _sut.CanDeleteItemAsync("educationalstages", 999);
    can.Should().BeTrue();
  }

  // --- educationtypes ---

  [Fact]
  public async Task CanDelete_EducationTypes_BlockedByInstitution()
  {
    _db.Institutions.Add(new Institution { InstitutionSymbol = 1, Name = "x", TypeId = 20, CreatedAt = DateTime.UtcNow });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("educationtypes", 20);

    can.Should().BeFalse();
    reason.Should().Contain("מוסדות");
  }

  [Fact]
  public async Task CanDelete_EducationTypes_AllowedWhenNoUsage()
  {
    var (can, _) = await _sut.CanDeleteItemAsync("educationtypes", 20);
    can.Should().BeTrue();
  }

  // --- localitydistrictnational ---

  [Fact]
  public async Task CanDelete_LocalityDistrictNational_BlockedByReportRow()
  {
    SeedReportRowConclusionLocation(55);

    var (can, reason) = await _sut.CanDeleteItemAsync("localitydistrictnational", 55);

    can.Should().BeFalse();
    reason.Should().Contain("דיווחים");
  }

  [Fact]
  public async Task CanDelete_LocalityDistrictNational_BlockedByAllocationJunction()
  {
    _db.Set<AllocationLocalityDistrictNational>().Add(new AllocationLocalityDistrictNational
    {
      AllocationId = 1,
      LocalityDistrictNationalId = 66
    });
    await _db.SaveChangesAsync();

    var (can, reason) = await _sut.CanDeleteItemAsync("localitydistrictnational", 66);

    can.Should().BeFalse();
    reason.Should().Contain("הקצאות");
  }

  [Fact]
  public async Task CanDelete_LocalityDistrictNational_AllowedWhenNoUsage()
  {
    var (can, _) = await _sut.CanDeleteItemAsync("localitydistrictnational", 404);
    can.Should().BeTrue();
  }

  // --- helpers ---

  private void SeedReportRowWithFramework(int frameworkId, int? conclusionFrameworkId)
  {
    _db.Reports.Add(new Report { Id = 1, UserId = 1, ReportingMonthId = 1, StatusId = 1, CreatedAt = DateTime.UtcNow });
    _db.ReportRows.Add(new ReportRow
    {
      Id = 1,
      ReportId = 1,
      SequenceNumber = 1,
      MeetingDate = DateTime.UtcNow.Date,
      MeetingDuration = 1m,
      DistrictId = 1,
      LocalityId = 1,
      FrameworkId = frameworkId,
      EducationalProgramId = 1,
      DomainId = 1,
      Subject1Id = 1,
      ConclusionFrameworkId = conclusionFrameworkId,
      CreatedAt = DateTime.UtcNow
    });
    _db.SaveChanges();
  }

  private void SeedReportRowConclusionLocation(int locationId)
  {
    _db.Reports.Add(new Report { Id = 2, UserId = 1, ReportingMonthId = 1, StatusId = 1, CreatedAt = DateTime.UtcNow });
    _db.ReportRows.Add(new ReportRow
    {
      Id = 2,
      ReportId = 2,
      SequenceNumber = 1,
      MeetingDate = DateTime.UtcNow.Date,
      MeetingDuration = 1m,
      DistrictId = 1,
      LocalityId = 1,
      FrameworkId = 1,
      EducationalProgramId = 1,
      DomainId = 1,
      Subject1Id = 1,
      ConclusionLocationId = locationId,
      CreatedAt = DateTime.UtcNow
    });
    _db.SaveChanges();
  }
}
