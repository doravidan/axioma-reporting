using AxiomaReporting.Core.Entities;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Tests.TestSupport;
using ClosedXML.Excel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AxiomaReporting.Tests.Integration;

public class DataVerificationExportTests : IDisposable
{
  private readonly CustomWebApplicationFactory _factory = new();

  public void Dispose() => _factory.DisposeAsync().AsTask().GetAwaiter().GetResult();

  [Fact]
  public async Task LookupExport_IncludesEntireRegistryAndFrameworkMetadata()
  {
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();
      var stage = new EducationalStage
      {
        Description = "Verification Stage", IsActive = true, CreatedAt = DateTime.UtcNow
      };
      db.EducationalStages.Add(stage);
      await db.SaveChangesAsync();
      db.Localities.Add(new Locality
      {
        Description = "Verification Locality", IsActive = false, CreatedAt = DateTime.UtcNow
      });
      db.Frameworks.Add(new Framework
      {
        Description = "Verification Framework",
        InstitutionSymbol = "VERIFY-442889",
        EducationalStageId = stage.Id,
        IsActive = false,
        CreatedAt = DateTime.UtcNow
      });
      await db.SaveChangesAsync();
    }

    var client = await SignInAdminAsync();
    var response = await client.GetAsync("/Lookup/ExportAll");
    response.EnsureSuccessStatusCode();

    await using var stream = new MemoryStream(await response.Content.ReadAsByteArrayAsync());
    using var workbook = new XLWorkbook(stream);
    workbook.Worksheets.Should().HaveCount(19, "all 18 registered lookup tables plus frameworks must be exported");
    workbook.Worksheets.Select(x => x.Name).Should().Contain(new[]
    {
      "ישובים", "תוכניות חינוכיות", "מסקנות מסגרת חינוכית", "מסגרות חינוכיות"
    });

    var localitySheet = workbook.Worksheet("ישובים");
    var localityColumns = HeaderColumns(localitySheet);
    localityColumns.Keys.Should().Contain(new[] { "Id", "Description", "IsActive" });
    var localityRow = localitySheet.RowsUsed().Skip(1)
      .Single(row => row.Cell(localityColumns["Description"]).GetString() == "Verification Locality");
    localityRow.Cell(localityColumns["IsActive"]).GetBoolean().Should().BeFalse();

    var frameworkSheet = workbook.Worksheet("מסגרות חינוכיות");
    var frameworkColumns = HeaderColumns(frameworkSheet);
    frameworkColumns.Keys.Should().Contain(new[]
    {
      "Id", "Description", "IsActive", "InstitutionSymbol", "EducationalStage"
    });
    var frameworkRow = frameworkSheet.RowsUsed().Skip(1)
      .Single(row => row.Cell(frameworkColumns["Description"]).GetString() == "Verification Framework");
    frameworkRow.Cell(frameworkColumns["InstitutionSymbol"]).GetString().Should().Be("VERIFY-442889");
    frameworkRow.Cell(frameworkColumns["EducationalStage"]).GetString().Should().Be("Verification Stage");
    frameworkRow.Cell(frameworkColumns["IsActive"]).GetBoolean().Should().BeFalse();
  }

  [Fact]
  public async Task ProjectProgramExport_IncludesEveryConfiguredValueSet()
  {
    using (var setup = _factory.Services.CreateScope())
    {
      var db = setup.ServiceProvider.GetRequiredService<AppDbContext>();
      var now = DateTime.UtcNow;
      var project = new Project { Description = "Verification Project", IsActive = true, CreatedAt = now };
      var program = new Core.Entities.Program { Description = "Verification Program", IsActive = true, CreatedAt = now };
      var subject = new Subject { Description = "Verification Subject", IsActive = true, CreatedAt = now };
      var domain = new Domain { Description = "Verification Domain", IsActive = true, CreatedAt = now };
      var educationalProgram = new EducationalProgram
      {
        Description = "Verification Educational Program", IsActive = true, CreatedAt = now
      };
      var discussionCode = new DiscussionCode
      {
        Description = "Verification Discussion", IsActive = true, CreatedAt = now
      };
      var gradeLevel = new GradeLevel { Description = "Verification Grade", IsActive = true, CreatedAt = now };
      var schoolClass = new SchoolClass { Description = "Verification Class", IsActive = true, CreatedAt = now };
      var locality = new Locality { Description = "Verification Program Locality", IsActive = true, CreatedAt = now };
      var localityScope = new LocalityDistrictNational
      {
        Description = "Verification Locality Scope", IsActive = true, CreatedAt = now
      };
      var framework = new Framework
      {
        Description = "Verification Scope Framework",
        InstitutionSymbol = "VERIFY-SCOPE",
        IsActive = true,
        CreatedAt = now
      };
      db.AddRange(project, program, subject, domain, educationalProgram, discussionCode, gradeLevel,
        schoolClass, locality, localityScope, framework);
      await db.SaveChangesAsync();

      db.ProjectPrograms.Add(new ProjectProgram { ProjectId = project.Id, ProgramId = program.Id });
      db.ProjectProgramSubjects.Add(new ProjectProgramSubject
      {
        ProjectId = project.Id, ProgramId = program.Id, SubjectId = subject.Id
      });
      db.ProjectProgramDomains.Add(new ProjectProgramDomain
      {
        ProjectId = project.Id, ProgramId = program.Id, DomainId = domain.Id
      });
      db.ProjectProgramEducationalPrograms.Add(new ProjectProgramEducationalProgram
      {
        ProjectId = project.Id, ProgramId = program.Id, EducationalProgramId = educationalProgram.Id
      });
      db.ProjectProgramDiscussionCodes.Add(new ProjectProgramDiscussionCode
      {
        ProjectId = project.Id, ProgramId = program.Id, DiscussionCodeId = discussionCode.Id
      });
      db.ProjectProgramGradeLevels.Add(new ProjectProgramGradeLevel
      {
        ProjectId = project.Id, ProgramId = program.Id, GradeLevelId = gradeLevel.Id
      });
      db.ProjectProgramClasses.Add(new ProjectProgramClass
      {
        ProjectId = project.Id, ProgramId = program.Id, ClassId = schoolClass.Id
      });
      db.ProjectProgramLocalities.Add(new ProjectProgramLocality
      {
        ProjectId = project.Id, ProgramId = program.Id, LocalityId = locality.Id
      });
      db.ProjectProgramLocalityDistrictNationals.Add(new ProjectProgramLocalityDistrictNational
      {
        ProjectId = project.Id,
        ProgramId = program.Id,
        LocalityDistrictNationalId = localityScope.Id
      });
      db.ProjectProgramFrameworks.Add(new ProjectProgramFramework
      {
        ProjectId = project.Id, ProgramId = program.Id, FrameworkId = framework.Id
      });
      await db.SaveChangesAsync();
    }

    var client = await SignInAdminAsync();
    var response = await client.GetAsync("/Admin/ProjectPrograms/Export");
    response.EnsureSuccessStatusCode();

    await using var stream = new MemoryStream(await response.Content.ReadAsByteArrayAsync());
    using var workbook = new XLWorkbook(stream);
    var worksheet = workbook.Worksheet("שיוכי ערכים לתוכנית");
    var columns = HeaderColumns(worksheet);
    var row = worksheet.RowsUsed().Skip(1)
      .Single(item => item.Cell(columns["פרויקט"]).GetString() == "Verification Project"
        && item.Cell(columns["תוכנית"]).GetString() == "Verification Program");

    row.Cell(columns["נושאים"]).GetString().Should().Be("Verification Subject");
    row.Cell(columns["תחומים"]).GetString().Should().Be("Verification Domain");
    row.Cell(columns["תוכניות חינוכיות"]).GetString().Should().Be("Verification Educational Program");
    row.Cell(columns["קיום דיון"]).GetString().Should().Be("Verification Discussion");
    row.Cell(columns["שכבות"]).GetString().Should().Be("Verification Grade");
    row.Cell(columns["כיתות"]).GetString().Should().Be("Verification Class");
    row.Cell(columns["יישובים"]).GetString().Should().Be("Verification Program Locality");
    row.Cell(columns["יישוב/מחוז/ארצי"]).GetString().Should().Be("Verification Locality Scope");
    row.Cell(columns["מסגרות חינוכיות"]).GetString().Should().Be("Verification Scope Framework");
  }

  private Task<HttpClient> SignInAdminAsync() =>
    AccessControlTests.SignInAsAsync(_factory, TestData.AdminIdNumber, TestData.AdminPassword);

  private static Dictionary<string, int> HeaderColumns(IXLWorksheet worksheet) =>
    worksheet.Row(1).CellsUsed()
      .ToDictionary(cell => cell.GetString(), cell => cell.Address.ColumnNumber);
}
