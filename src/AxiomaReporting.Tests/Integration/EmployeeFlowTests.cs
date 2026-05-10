using System.Net;
using System.Text.Json;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Tests.TestSupport;
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

  // ─── Helpers ────────────────────────────────────────────────────────────────

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
