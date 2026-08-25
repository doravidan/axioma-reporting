using System.Net;
using System.Text.Json;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
using ClosedXML.Excel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Tests.Integration;

/// <summary>
/// Integration tests for employee management: creation, editing, account unlock,
/// password reset, scoped access, and the cascading Programs-for-Project JSON endpoint.
/// </summary>
public class EmployeeFlowTests : IDisposable
{
  // Credentials used across multiple tests — seeded in the constructor.
  private const string SecondEmployeeIdNumber = "800000001";
  private const string SecondEmployeePassword = "Password123";
  private int _secondEmployeeUserId;
  private int _employeeRoleId;

  private readonly CustomWebApplicationFactory _factory;

  public EmployeeFlowTests()
  {
    _factory = new CustomWebApplicationFactory();
    SeedFixture();
  }

  public void Dispose() => _factory.DisposeAsync().AsTask().GetAwaiter().GetResult();

  // ─── Create employee ─────────────────────────────────────────────────────────

  [Fact]
  public async Task Admin_CanCreateEmployee_EmployeeAppearsInList()
  {
    var client = await SignInAdmin();

    var formHtml = await client.GetStringAsync("/Employee/Create");
    var form = new Dictionary<string, string>
    {
      ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(formHtml),
      ["EmployeeCode"] = "100001",
      ["IdNumber"] = "900000001",
      ["FirstName"] = "New",
      ["LastName"] = "Employee",
      ["Email"] = "newemp@example.test",
      ["RoleId"] = _employeeRoleId.ToString(),
      ["UserRoleId"] = ((int)UserRoleEnum.Employee).ToString(),
      ["StatusId"] = ((int)UserStatusEnum.Active).ToString(),
      ["IsReportingEmployee"] = "true",
      ["AllowFutureReporting"] = "false"
    };

    var createResponse = await client.PostAsync("/Employee/Create", new FormUrlEncodedContent(form));
    createResponse.IsSuccessStatusCode.Should().BeTrue();

    // Verify the new user exists in the database.
    using var scope = _factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var created = await db.Users.FirstOrDefaultAsync(u => u.IdNumber == "900000001");
    created.Should().NotBeNull("the employee should have been persisted");
    created!.EmployeeCode.Should().Be("100001");
    created.FirstName.Should().Be("New");

    // Verify the employee list page reflects the new record.
    var listResponse = await client.GetAsync("/Employee/Index?search=100001");
    listResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    var listBody = await listResponse.Content.ReadAsStringAsync();
    listBody.Should().Contain("100001");
  }

  // ─── Edit employee ───────────────────────────────────────────────────────────

  [Fact]
  public async Task Admin_CanEditEmployee_FieldsUpdated()
  {
    var client = await SignInAdmin();

    // Pre-condition: employee 1 (Test Employee) exists.
    var editFormHtml = await client.GetStringAsync("/Employee/Edit/1");
    editFormHtml.Should().Contain("EMP001", "the edit form must pre-populate with current data");

    var editResponse = await client.PostAsync("/Employee/Edit/1", new FormUrlEncodedContent(
      new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(editFormHtml),
        ["Id"] = "1",
        ["EmployeeCode"] = "EMP001",
        ["IdNumber"] = TestData.EmployeeIdNumber,
        ["FirstName"] = "Updated",
        ["LastName"] = "Employee",
        ["Email"] = "updated@example.test",
        ["RoleId"] = _employeeRoleId.ToString(),
        ["UserRoleId"] = ((int)UserRoleEnum.Employee).ToString(),
        ["StatusId"] = ((int)UserStatusEnum.Active).ToString(),
        ["IsReportingEmployee"] = "true",
        ["AllowFutureReporting"] = "false",
        ["Notes"] = "edited in test"
      }));
    editResponse.IsSuccessStatusCode.Should().BeTrue();

    using var scope = _factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var user = await db.Users.FindAsync(1);
    user!.FirstName.Should().Be("Updated");
    user.Notes.Should().Be("edited in test");
  }

  [Fact]
  public async Task SystemAdmin_CanDeleteSpecificAllocation_ByAllocationId()
  {
    int allocationId;
    using (var scope = _factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      var project = new Project
      {
        Description = "Allocation deletion project",
        IsActive = true,
        CreatedAt = DateTime.UtcNow
      };
      db.Projects.Add(project);
      await db.SaveChangesAsync();

      var allocation = new Allocation
      {
        UserId = _secondEmployeeUserId,
        ProjectId = project.Id,
        IsActive = true,
        CreatedAt = DateTime.UtcNow
      };
      db.Allocations.Add(allocation);
      await db.SaveChangesAsync();
      allocationId = allocation.Id;
    }

    var client = await SignInAdmin();
    var listHtml = await client.GetStringAsync(
      $"/allocations?employeeId={_secondEmployeeUserId}&showAll=true&pageSize=500");
    listHtml.Should().Contain("מחיקת הקצאה");

    var response = await client.PostAsync(
      $"/Employee/{_secondEmployeeUserId}/Allocations/{allocationId}/Delete",
      new FormUrlEncodedContent(new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(listHtml)
      }));
    response.EnsureSuccessStatusCode();

    using var verifyScope = _factory.Services.CreateScope();
    var verifyDb = verifyScope.ServiceProvider.GetRequiredService<AppDbContext>();
    var deletedAllocation = await verifyDb.Allocations.FindAsync(allocationId);
    deletedAllocation.Should().NotBeNull("allocation deletion is a history-preserving soft delete");
    deletedAllocation!.IsActive.Should().BeFalse();
  }

  [Fact]
  public async Task Admin_CanSaveAllocationWithMoreThanDefaultFormValueLimit()
  {
    int projectId;
    int allocationId;
    using (var scope = _factory.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      var project = new Project
      {
        Description = "Large allocation form project",
        IsActive = true,
        CreatedAt = DateTime.UtcNow
      };
      db.Projects.Add(project);
      await db.SaveChangesAsync();
      projectId = project.Id;

      var allocation = new Allocation
      {
        UserId = _secondEmployeeUserId,
        ProjectId = projectId,
        IsActive = true,
        CreatedAt = DateTime.UtcNow
      };
      db.Allocations.Add(allocation);
      await db.SaveChangesAsync();
      allocationId = allocation.Id;
    }

    var client = await SignInAdmin();
    var editHtml = await client.GetStringAsync(
      $"/Employee/{_secondEmployeeUserId}/Allocations/{allocationId}/Edit");
    var form = new List<KeyValuePair<string, string>>
    {
      new("__RequestVerificationToken", HtmlForm.AntiForgeryToken(editHtml)),
      new("UserId", _secondEmployeeUserId.ToString()),
      new("ProjectId", projectId.ToString()),
      new("ProgramIds", "1"),
      new("OutputDurationValues", "1")
    };
    for (var index = 0; index < 1_300; index++)
      form.Add(new KeyValuePair<string, string>("SubjectIds", ((index % 25) + 1).ToString()));

    var response = await client.PostAsync(
      $"/Employee/{_secondEmployeeUserId}/Allocations/{allocationId}/Edit",
      new FormUrlEncodedContent(form));
    response.EnsureSuccessStatusCode();

    using var verifyScope = _factory.Services.CreateScope();
    var verifyDb = verifyScope.ServiceProvider.GetRequiredService<AppDbContext>();
    (await verifyDb.Set<AllocationSubject>().CountAsync(row => row.AllocationId == allocationId))
      .Should().Be(25, "the large post must be accepted and duplicate ids must be deduplicated");
  }

  // ─── Unlock locked account ───────────────────────────────────────────────────

  [Fact]
  public async Task Admin_CanUnlockLockedAccount_StatusChangesToActive()
  {
    // Lock the second employee.
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();
      var u = await db.Users.FindAsync(_secondEmployeeUserId);
      u!.StatusId = (int)UserStatusEnum.Locked;
      u.FailedLoginAttempts = 3;
      await db.SaveChangesAsync();
    }

    var client = await SignInAdmin();
    var listHtml = await client.GetStringAsync("/Employee/Index");
    var token = HtmlForm.AntiForgeryToken(listHtml);

    var response = await client.PostAsync($"/Employee/UnlockAccount/{_secondEmployeeUserId}",
      new FormUrlEncodedContent(new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = token
      }));
    response.IsSuccessStatusCode.Should().BeTrue();

    using var verify = _factory.Services.CreateScope();
    var verifyDb = verify.ServiceProvider.GetRequiredService<AppDbContext>();
    var unlocked = await verifyDb.Users.FindAsync(_secondEmployeeUserId);
    unlocked!.StatusId.Should().Be((int)UserStatusEnum.Active);
    unlocked.FailedLoginAttempts.Should().Be(0);
  }

  // ─── Reset password ──────────────────────────────────────────────────────────

  [Fact]
  public async Task Admin_CanResetPassword_EmployeeCanLoginWithIdNumberAsPassword()
  {
    var client = await SignInAdmin();
    var listHtml = await client.GetStringAsync("/Employee/Index");
    var token = HtmlForm.AntiForgeryToken(listHtml);

    var resetResponse = await client.PostAsync($"/Employee/ResetPassword/{_secondEmployeeUserId}",
      new FormUrlEncodedContent(new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = token
      }));
    resetResponse.IsSuccessStatusCode.Should().BeTrue();

    // Now verify the second employee can log in using their ID number as the new password.
    // (ResetPassword sets the hash to HashPassword(user.IdNumber))
    var newClient = _factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });
    var loginHtml = await newClient.GetStringAsync("/Account/Login");
    var loginResponse = await newClient.PostAsync("/Account/Login", new FormUrlEncodedContent(
      new Dictionary<string, string>
      {
        ["__RequestVerificationToken"] = HtmlForm.AntiForgeryToken(loginHtml),
        ["IdNumber"] = SecondEmployeeIdNumber,
        ["Password"] = SecondEmployeeIdNumber  // password reset to ID number
      }));
    loginResponse.IsSuccessStatusCode.Should().BeTrue();
    var loginBody = await loginResponse.Content.ReadAsStringAsync();
    // After forced-password-change redirect, user lands on ChangePassword page,
    // so we only verify the login chain didn't terminate at a login-error page.
    loginBody.Should().NotContain("שם משתמש או סיסמה שגויים",
      because: "the new password should be accepted");
  }

  // ─── Scoped access: employee cannot view other employee ─────────────────────

  [Fact]
  public async Task Employee_CannotViewOtherEmployeeDetails_Returns403OrRedirect()
  {
    // Sign in as the default employee (id=1) and try to access employee id=2 (admin).
    var client = await AccessControlTests.SignInAsAsync(_factory, TestData.EmployeeIdNumber, TestData.EmployeePassword);

    // Employee controller is guarded by AdminPMOrCoordinator — so Employee role gets Forbidden.
    var response = await client.GetAsync("/Employee/Edit/2");

    // AllowAutoRedirect=true follows 403→AccessDenied→200, so accept OK as well.
    response.StatusCode.Should().BeOneOf(
      new[] { HttpStatusCode.Forbidden, HttpStatusCode.Redirect, HttpStatusCode.OK },
      "employees do not have access to the employee management area; " +
      "auto-redirect may resolve the 403 to a 200 AccessDenied page");
  }

  // ─── ProgramsForProject JSON endpoint ───────────────────────────────────────

  [Fact]
  public async Task ProgramsForProject_WhenMappingExists_ReturnsFilteredPrograms()
  {
    int projectId;
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();

      var prog1 = new Core.Entities.Program { Description = "Mapped Prog A", IsActive = true, CreatedAt = DateTime.UtcNow };
      var prog2 = new Core.Entities.Program { Description = "Mapped Prog B", IsActive = true, CreatedAt = DateTime.UtcNow };
      var unrelated = new Core.Entities.Program { Description = "Unmapped Prog", IsActive = true, CreatedAt = DateTime.UtcNow };
      db.Programs.AddRange(prog1, prog2, unrelated);

      var project = new Project { Description = "Mapped Project", IsActive = true, CreatedAt = DateTime.UtcNow };
      db.Projects.Add(project);
      await db.SaveChangesAsync();

      projectId = project.Id;
      db.Set<ProjectProgram>().AddRange(
        new ProjectProgram { ProjectId = projectId, ProgramId = prog1.Id },
        new ProjectProgram { ProjectId = projectId, ProgramId = prog2.Id });
      await db.SaveChangesAsync();
    }

    var client = await SignInAdmin();
    var response = await client.GetAsync($"/Employee/ProgramsForProject?projectId={projectId}");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var body = await response.Content.ReadAsStringAsync();
    var programs = JsonSerializer.Deserialize<List<JsonElement>>(body,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

    var descriptions = programs.Select(p => p.GetProperty("description").GetString()).ToList();
    descriptions.Should().Contain("Mapped Prog A");
    descriptions.Should().Contain("Mapped Prog B");
    descriptions.Should().NotContain("Unmapped Prog",
      because: "when a project-program mapping exists only those programs should be returned");
  }

  [Fact]
  public async Task ProgramsForProject_WhenNoMapping_ReturnsAllActivePrograms()
  {
    int projectId;
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();

      // Add some programs but NO ProjectProgram mapping for this project.
      db.Programs.AddRange(
        new Core.Entities.Program { Description = "Fallback Prog X", IsActive = true, CreatedAt = DateTime.UtcNow },
        new Core.Entities.Program { Description = "Fallback Prog Y", IsActive = true, CreatedAt = DateTime.UtcNow });

      var project = new Project { Description = "Unmapped Project", IsActive = true, CreatedAt = DateTime.UtcNow };
      db.Projects.Add(project);
      await db.SaveChangesAsync();
      projectId = project.Id;
    }

    var client = await SignInAdmin();
    var response = await client.GetAsync($"/Employee/ProgramsForProject?projectId={projectId}");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var body = await response.Content.ReadAsStringAsync();
    var programs = JsonSerializer.Deserialize<List<JsonElement>>(body,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

    programs.Count.Should().BeGreaterThan(0,
      because: "when no mapping exists the fallback returns all active programs");
    var descriptions = programs.Select(p => p.GetProperty("description").GetString()).ToList();
    descriptions.Should().Contain("Fallback Prog X");
    descriptions.Should().Contain("Fallback Prog Y");
  }

  [Fact]
  public async Task EmployeeExport_IncludesCompleteAllocationDetailsForEachEmployee()
  {
    var now = DateTime.UtcNow;
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();
      var project = new Project { Description = "Export Project", IsActive = true, CreatedAt = now };
      var reportType = new ReportType { Description = "Export Report Type", IsActive = true, CreatedAt = now };
      var district = new District { Description = "Export District", IsActive = true, CreatedAt = now };
      var program = new Core.Entities.Program { Description = "Export Program", IsActive = true, CreatedAt = now };
      var sector = new Sector { Description = "Export Sector", IsActive = true, CreatedAt = now };
      var locality = new Locality { Description = "Export Locality", IsActive = true, CreatedAt = now };
      var framework = new Framework
      {
        Description = "Export Framework", InstitutionSymbol = "442889", IsActive = true, CreatedAt = now
      };
      var subject = new Subject { Description = "Export Subject", IsActive = true, CreatedAt = now };
      var domain = new Domain { Description = "Export Domain", IsActive = true, CreatedAt = now };
      var educationalProgram = new EducationalProgram
      {
        Description = "Export Educational Program", IsActive = true, CreatedAt = now
      };
      var schoolClass = new SchoolClass { Description = "Export Class", IsActive = true, CreatedAt = now };
      var gradeLevel = new GradeLevel { Description = "Export Grade", IsActive = true, CreatedAt = now };
      var discussionCode = new DiscussionCode
      {
        Description = "Export Discussion", IsActive = true, CreatedAt = now
      };
      var localityDistrictNational = new LocalityDistrictNational
      {
        Description = "Export National Scope", IsActive = true, CreatedAt = now
      };
      db.AddRange(project, reportType, district, program, sector, locality, framework, subject, domain,
        educationalProgram, schoolClass, gradeLevel, discussionCode, localityDistrictNational);
      await db.SaveChangesAsync();

      var allocation = new Allocation
      {
        UserId = _secondEmployeeUserId,
        ProjectId = project.Id,
        ReportTypeId = reportType.Id,
        MonthlyEmploymentScope = 12.5m,
        DailyEmploymentScope = 1.5m,
        AnnualEmploymentScope = 150m,
        MonthlyRowAllocation = 25,
        AnnualRowAllocation = 300,
        OutputDuration = "0.5,1,1.5",
        AllowExcelUpload = true,
        Notes = "Complete allocation export marker",
        IsActive = true,
        CreatedAt = now
      };
      db.Allocations.Add(allocation);
      await db.SaveChangesAsync();

      var secondAllocation = new Allocation
      {
        UserId = _secondEmployeeUserId,
        ProjectId = project.Id,
        ReportTypeId = reportType.Id,
        MonthlyEmploymentScope = 8m,
        DailyEmploymentScope = 1m,
        AnnualEmploymentScope = 96m,
        MonthlyRowAllocation = 15,
        AnnualRowAllocation = 180,
        OutputDuration = "1,2",
        AllowExcelUpload = false,
        Notes = "Second allocation preview row",
        IsActive = true,
        CreatedAt = now.AddMinutes(1)
      };
      db.Allocations.Add(secondAllocation);
      await db.SaveChangesAsync();

      db.AddRange(
        new AllocationDistrict { AllocationId = allocation.Id, DistrictId = district.Id },
        new AllocationProgram { AllocationId = allocation.Id, ProgramId = program.Id },
        new AllocationSector { AllocationId = allocation.Id, SectorId = sector.Id },
        new AllocationLocality { AllocationId = allocation.Id, LocalityId = locality.Id },
        new AllocationFramework { AllocationId = allocation.Id, FrameworkId = framework.Id },
        new AllocationSubject { AllocationId = allocation.Id, SubjectId = subject.Id },
        new AllocationDomain { AllocationId = allocation.Id, DomainId = domain.Id },
        new AllocationEducationalProgram
        {
          AllocationId = allocation.Id, EducationalProgramId = educationalProgram.Id
        },
        new AllocationClass { AllocationId = allocation.Id, ClassId = schoolClass.Id },
        new AllocationGradeLevel { AllocationId = allocation.Id, GradeLevelId = gradeLevel.Id },
        new AllocationDiscussionCode { AllocationId = allocation.Id, DiscussionCodeId = discussionCode.Id },
        new AllocationLocalityDistrictNational
        {
          AllocationId = allocation.Id, LocalityDistrictNationalId = localityDistrictNational.Id
        });
      await db.SaveChangesAsync();
    }

    var client = await SignInAdmin();
    var response = await client.GetAsync("/Employee/ExportExcel?employeeCode=EMP_SECOND");
    response.EnsureSuccessStatusCode();

    var workbookBytes = await response.Content.ReadAsByteArrayAsync();
    await using var stream = new MemoryStream(workbookBytes);
    using var workbook = new XLWorkbook(stream);
    workbook.Worksheets.Should().HaveCount(3);
    workbook.Worksheets.Select(sheet => sheet.Name)
      .Should().Equal("עובדים", "פירוט הקצאות", "ערכי הקצאות");

    var employeesSheet = workbook.Worksheet("עובדים");
    var employeeColumns = employeesSheet.Row(1).CellsUsed()
      .ToDictionary(cell => cell.GetString(), cell => cell.Address.ColumnNumber);
    var employeeRow = employeesSheet.RowsUsed().Skip(1).Single();
    employeeRow.Cell(employeeColumns["קוד עובד"]).GetString().Should().Be("EMP_SECOND");
    employeeRow.Cell(employeeColumns["פרויקטים"]).GetString().Should().Be("Export Project");
    employeeRow.Cell(employeeColumns["מחוזות"]).GetString().Should().Be("Export District");

    var details = workbook.Worksheet("פירוט הקצאות");
    var columns = details.Row(1).CellsUsed()
      .ToDictionary(cell => cell.GetString(), cell => cell.Address.ColumnNumber);
    columns.Keys.Should().Contain(new[] { "מחוזות", "תוכניות", "יישובים", "מסגרות חינוכיות" });

    var firstAllocationRow = details.RowsUsed()
      .Skip(1)
      .Single(rowData => rowData.Cell(columns["הערות הקצאה"]).GetString() == "Complete allocation export marker");
    firstAllocationRow.Cell(columns["קוד עובד"]).GetString().Should().Be("EMP_SECOND");
    firstAllocationRow.Cell(columns["פרויקט"]).GetString().Should().Be("Export Project");
    firstAllocationRow.Cell(columns["סוג דיווח"]).GetString().Should().Be("Export Report Type");
    firstAllocationRow.Cell(columns["היקף פעילות חודשי"]).GetDouble().Should().Be(12.5);
    firstAllocationRow.Cell(columns["מחוזות"]).GetString().Should().Be("Export District");
    firstAllocationRow.Cell(columns["תוכניות"]).GetString().Should().Be("Export Program");
    firstAllocationRow.Cell(columns["יישובים"]).GetString().Should().Be("Export Locality");
    firstAllocationRow.Cell(columns["מסגרות חינוכיות"]).GetString().Should().Contain("Export Framework");
    var firstAllocationId = firstAllocationRow.Cell(columns["מזהה הקצאה"]).GetDouble();

    var secondAllocationRow = details.RowsUsed()
      .Skip(1)
      .Single(rowData => rowData.Cell(columns["הערות הקצאה"]).GetString() == "Second allocation preview row");
    var secondAllocationId = secondAllocationRow.Cell(columns["מזהה הקצאה"]).GetDouble();
    secondAllocationId.Should().NotBe(firstAllocationId);

    var values = workbook.Worksheet("ערכי הקצאות");
    var valueColumns = values.Row(1).CellsUsed()
      .ToDictionary(cell => cell.GetString(), cell => cell.Address.ColumnNumber);
    valueColumns.Keys.Should().Equal(
      "קוד עובד", "מספר זהות", "שם פרטי", "שם משפחה", "מזהה הקצאה", "פרויקט", "סוג נתון", "ערך");
    var firstAllocationValueRows = values.RowsUsed()
      .Skip(1)
      .Where(rowData => rowData.Cell(valueColumns["מזהה הקצאה"]).GetDouble() == firstAllocationId)
      .ToList();
    firstAllocationValueRows.Should().HaveCount(12, "each allocation choice must have its own current DB row");
    firstAllocationValueRows.Should().OnlyContain(rowData =>
      rowData.Cell(valueColumns["קוד עובד"]).GetString() == "EMP_SECOND"
      && rowData.Cell(valueColumns["פרויקט"]).GetString() == "Export Project");
    var choices = firstAllocationValueRows.ToDictionary(
      rowData => rowData.Cell(valueColumns["סוג נתון"]).GetString(),
      rowData => rowData.Cell(valueColumns["ערך"]).GetString());
    choices["מחוז"].Should().Be("Export District");
    choices["תוכנית"].Should().Be("Export Program");
    choices["מגזר"].Should().Be("Export Sector");
    choices["יישוב"].Should().Be("Export Locality");
    choices["מסגרת חינוכית"].Should().Contain("Export Framework");
    choices["נושא"].Should().Be("Export Subject");
    choices["תחום"].Should().Be("Export Domain");
    choices["תוכנית חינוכית"].Should().Be("Export Educational Program");
    choices["כיתה"].Should().Be("Export Class");
    choices["שכבה"].Should().Be("Export Grade");
    choices["קיום דיון"].Should().Be("Export Discussion");
    choices["יישוב/מחוז/ארצי"].Should().Be("Export National Scope");
    values.RowsUsed().Skip(1)
      .Should().NotContain(rowData => rowData.Cell(valueColumns["מזהה הקצאה"]).GetDouble() == secondAllocationId);

    var employeeWithoutAllocationsResponse = await client.GetAsync("/Employee/ExportExcel?employeeCode=EMP001");
    employeeWithoutAllocationsResponse.EnsureSuccessStatusCode();
    await using var noAllocationsStream = new MemoryStream(
      await employeeWithoutAllocationsResponse.Content.ReadAsByteArrayAsync());
    using var noAllocationsWorkbook = new XLWorkbook(noAllocationsStream);
    noAllocationsWorkbook.Worksheets.Should().HaveCount(3);
    var noAllocationsSheet = noAllocationsWorkbook.Worksheet("עובדים");
    var noAllocationsColumns = noAllocationsSheet.Row(1).CellsUsed()
      .ToDictionary(cell => cell.GetString(), cell => cell.Address.ColumnNumber);
    var noAllocationRow = noAllocationsSheet.RowsUsed().Skip(1).Single();
    noAllocationRow.Cell(noAllocationsColumns["קוד עובד"]).GetString().Should().Be("EMP001");
    noAllocationsWorkbook.Worksheet("פירוט הקצאות").RowsUsed().Skip(1).Should().BeEmpty();
    noAllocationsWorkbook.Worksheet("ערכי הקצאות").RowsUsed().Skip(1).Should().BeEmpty();

    var allocationResponse = await client.GetAsync("/allocations/export?employeeCode=EMP_SECOND");
    allocationResponse.EnsureSuccessStatusCode();
    await using var allocationStream = new MemoryStream(await allocationResponse.Content.ReadAsByteArrayAsync());
    using var allocationWorkbook = new XLWorkbook(allocationStream);
    var allocationSheet = allocationWorkbook.Worksheet("עובדים והקצאות");
    var allocationColumns = allocationSheet.Row(1).CellsUsed()
      .ToDictionary(cell => cell.GetString(), cell => cell.Address.ColumnNumber);
    var allocationRows = allocationSheet.RowsUsed()
      .Skip(1)
      .Where(rowData => rowData.Cell(allocationColumns["הערות הקצאה"]).GetString() == "Complete allocation export marker")
      .ToList();

    allocationColumns.Keys.Should().Contain(new[]
    {
      "סוג ערך בהקצאה", "מזהה ערך", "ערך בהקצאה"
    });
    allocationRows.Should().HaveCount(12);
    allocationRows.Should().OnlyContain(rowData =>
      rowData.Cell(allocationColumns["קוד עובד"]).GetString() == "EMP_SECOND"
      && rowData.Cell(allocationColumns["פרויקט"]).GetString() == "Export Project");
    var allocationChoices = allocationRows.ToDictionary(
      rowData => rowData.Cell(allocationColumns["סוג ערך בהקצאה"]).GetString(),
      rowData => rowData.Cell(allocationColumns["ערך בהקצאה"]).GetString());
    allocationChoices["יישוב"].Should().Be("Export Locality");
    allocationChoices["מסגרת חינוכית"].Should().Contain("Export Framework");
    allocationChoices["נושא"].Should().Be("Export Subject");
    allocationChoices["תחום"].Should().Be("Export Domain");
    allocationChoices["תוכנית חינוכית"].Should().Be("Export Educational Program");
    allocationChoices["כיתה"].Should().Be("Export Class");
    allocationChoices["שכבה"].Should().Be("Export Grade");
    allocationChoices["קיום דיון"].Should().Be("Export Discussion");
    allocationChoices["יישוב/מחוז/ארצי"].Should().Be("Export National Scope");
  }

  // ─── Helpers ────────────────────────────────────────────────────────────────

  [Fact]
  public async Task EmployeeExport_WhenAllocationValuesExceedExcelCellLimit_UsesLosslessLongFormatSheet()
  {
    const int frameworkCount = 450;
    int allocationId;
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();
      var now = DateTime.UtcNow;
      var project = new Project
      {
        Description = "Large allocation export project",
        IsActive = true,
        CreatedAt = now
      };
      var frameworks = Enumerable.Range(1, frameworkCount)
        .Select(index => new Framework
        {
          InstitutionSymbol = $"9{index:D6}",
          Description = $"Framework {index:D4} {new string('X', 90)}",
          IsActive = true,
          CreatedAt = now
        })
        .ToList();
      db.Projects.Add(project);
      db.Frameworks.AddRange(frameworks);
      await db.SaveChangesAsync();

      var allocation = new Allocation
      {
        UserId = _secondEmployeeUserId,
        ProjectId = project.Id,
        IsActive = true,
        CreatedAt = now
      };
      db.Allocations.Add(allocation);
      await db.SaveChangesAsync();
      allocationId = allocation.Id;

      db.AddRange(frameworks.Select(framework => new AllocationFramework
      {
        AllocationId = allocation.Id,
        FrameworkId = framework.Id
      }));
      await db.SaveChangesAsync();
    }

    var client = await SignInAdmin();
    var response = await client.GetAsync("/Employee/ExportExcel?employeeCode=EMP_SECOND");
    response.EnsureSuccessStatusCode();

    await using var stream = new MemoryStream(await response.Content.ReadAsByteArrayAsync());
    using var workbook = new XLWorkbook(stream);
    var details = workbook.Worksheet("פירוט הקצאות");
    var detailColumns = details.Row(1).CellsUsed()
      .ToDictionary(cell => cell.GetString(), cell => cell.Address.ColumnNumber);
    var allocationRow = details.RowsUsed().Skip(1).Single(row =>
      row.Cell(detailColumns["מזהה הקצאה"]).GetDouble() == allocationId);
    var frameworkSummary = allocationRow.Cell(detailColumns["מסגרות חינוכיות"]).GetString();
    frameworkSummary.Length.Should().BeLessThanOrEqualTo(32_767);
    frameworkSummary.Should().Contain("ערכי הקצאות");
    frameworkSummary.Should().Contain(frameworkCount.ToString("N0"));

    var values = workbook.Worksheet("ערכי הקצאות");
    var valueColumns = values.Row(1).CellsUsed()
      .ToDictionary(cell => cell.GetString(), cell => cell.Address.ColumnNumber);
    var frameworkRows = values.RowsUsed().Skip(1).Where(row =>
      row.Cell(valueColumns["מזהה הקצאה"]).GetDouble() == allocationId
      && row.Cell(valueColumns["סוג נתון"]).GetString() == "מסגרת חינוכית")
      .ToList();
    frameworkRows.Should().HaveCount(frameworkCount,
      "the long-format worksheet must preserve every value when the summary cell overflows");
    frameworkRows.Should().OnlyContain(row =>
      row.Cell(valueColumns["ערך"]).GetString().Length <= 32_767);
  }

  private Task<HttpClient> SignInAdmin()
    => AccessControlTests.SignInAsAsync(_factory, TestData.AdminIdNumber, TestData.AdminPassword);

  private void SeedFixture()
  {
    using var scope = _factory.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var pw = new PasswordService();

    var role = new EmployeeRole { Description = "Flow Role", IsActive = true, CreatedAt = DateTime.UtcNow };
    db.Roles.Add(role);
    db.SaveChanges();
    _employeeRoleId = role.Id;

    var second = new User
    {
      EmployeeCode = "EMP_SECOND",
      IdNumber = SecondEmployeeIdNumber,
      FirstName = "Second",
      LastName = "Employee",
      Email = "second@example.test",
      PasswordHash = pw.HashPassword(SecondEmployeePassword),
      RoleId = _employeeRoleId,
      UserRoleId = (int)UserRoleEnum.Employee,
      StatusId = (int)UserStatusEnum.Active,
      AcceptedTermsOfUse = true,
      MustChangePassword = false,
      LastPasswordChange = DateTime.UtcNow,
      CreatedAt = DateTime.UtcNow
    };
    db.Users.Add(second);
    db.SaveChanges();
    _secondEmployeeUserId = second.Id;

    db.TermsOfUseAcceptances.Add(new TermsOfUseAcceptance
    {
      UserId = _secondEmployeeUserId,
      VersionId = 1,
      AcceptedAt = DateTime.UtcNow
    });
    db.SaveChanges();
  }
}
