using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;

// QuestPDF Community license (free for commercial use under 1M ARR)
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Register Hebrew font for QuestPDF once at startup
var hebrewFontPath = Path.Combine(builder.Environment.WebRootPath ?? Path.Combine(AppContext.BaseDirectory, "wwwroot"),
  "fonts", "NotoSansHebrew-Regular.ttf");
if (File.Exists(hebrewFontPath))
{
  using var fontStream = File.OpenRead(hebrewFontPath);
  FontManager.RegisterFont(fontStream);
}
else
{
  // Fail-soft: log a warning so builds still succeed when the TTF is not checked in
  Console.Error.WriteLine(
    $"[AxiomaReporting] Warning: Hebrew font not found at '{hebrewFontPath}'. PDF error reports will fall back to the default QuestPDF font. See wwwroot/fonts/README.md.");
}

builder.Services.AddScoped<AxiomaReporting.Web.Authorization.RequireTermsAcceptedFilter>();

builder.Services.AddControllersWithViews(options =>
{
  options.Filters.AddService<AxiomaReporting.Web.Authorization.RequireTermsAcceptedFilter>();
  options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "יש לבחור ערך");
  options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, field) => "הערך שנבחר אינו תקין");
  options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(field => $"חסר ערך חובה: {field}");
  options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "חסר ערך חובה");
  options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(value => "הערך שנבחר אינו תקין");
});

var useDemoInMemory = string.Equals(
  Environment.GetEnvironmentVariable("AXIOMA_DEMO_INMEMORY"),
  "true",
  StringComparison.OrdinalIgnoreCase);

builder.Services.AddDbContext<AppDbContext>(options =>
{
  if (useDemoInMemory)
    options.UseInMemoryDatabase("AxiomaReportingDemo");
  else
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Auth services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddHttpContextAccessor();

// Reporting engine services (AX-015, AX-016, AX-017)
builder.Services.AddScoped<IReportValidationService, ReportValidationService>();
builder.Services.AddScoped<IReportStatusService, ReportStatusService>();
builder.Services.AddScoped<ISmtpSender, SmtpSender>();
builder.Services.AddScoped<EmailTemplateRenderer>();
builder.Services.AddScoped<IEmailService, NotificationDispatcher>();
builder.Services.AddScoped<IReportExcelImportService, ReportExcelImportService>();
builder.Services.AddScoped<IPdfReportService, PdfReportService>();
builder.Services.AddScoped<ILookupResolver, LookupResolver>();
builder.Services.AddScoped<IBatchReportImportService, BatchReportImportService>();

// Background services (AX-021)
builder.Services.AddHostedService<AxiomaReporting.Infrastructure.BackgroundJobs.ReminderService>();
builder.Services.AddHostedService<AxiomaReporting.Infrastructure.BackgroundJobs.NotificationRetryService>();

// Dashboard services (AX-019, AX-020)
builder.Services.AddScoped<IDashboardFilterService, DashboardFilterService>();

// Branding (AX-023 / Gap 8 — site logo from SystemConstants)
builder.Services.AddScoped<IBrandingService, BrandingService>();

// Cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
  });

// Authorization policies
builder.Services.AddAuthorization(options =>
{
  options.AddPolicy(PolicyNames.AdminOnly, policy =>
    policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "1"));

  options.AddPolicy(PolicyNames.AdminOrPM, policy =>
    policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "1", "2"));

  options.AddPolicy(PolicyNames.AdminPMOrCoordinator, policy =>
    policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "1", "2", "3"));

  options.AddPolicy(PolicyNames.CanApproveReports, policy =>
    policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "1", "2", "3", "5"));

  options.AddPolicy(PolicyNames.CanViewDashboard, policy =>
    policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "1", "2", "3", "4", "5"));

  options.AddPolicy(PolicyNames.CanManageLookups, policy =>
    policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "1"));
});

var app = builder.Build();

if (useDemoInMemory)
{
  using var scope = app.Services.CreateScope();
  var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  db.Database.EnsureCreated();
  SeedDemoData(db);
}

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void SeedDemoData(AppDbContext db)
{
  var now = DateTime.UtcNow;
  var password = new PasswordService();

  var admin = db.Users.FirstOrDefault(u => u.IdNumber == "admin");
  if (admin != null)
  {
    admin.MustChangePassword = false;
    admin.AcceptedTermsOfUse = false;
    admin.LastPasswordChange = now;
    admin.PasswordHash = password.HashPassword("admin1234");
  }

  var employee = db.Users.FirstOrDefault(u => u.IdNumber == "111111111");
  if (employee == null)
  {
    employee = new User
    {
      EmployeeCode = "4343343",
      IdNumber = "111111111",
      FirstName = "אורן",
      LastName = "לוינסון",
      PasswordHash = password.HashPassword("Password123"),
      RoleId = 1,
      UserRoleId = (int)UserRoleEnum.Employee,
      StatusId = 1,
      IsReportingEmployee = true,
      MustChangePassword = false,
      AcceptedTermsOfUse = true,
      LastPasswordChange = now,
      CreatedAt = now
    };
    db.Users.Add(employee);
    db.SaveChanges();
  }

  if (!db.ReportingMonths.Any(m => m.IsActive))
  {
    db.ReportingMonths.Add(new ReportingMonth
    {
      Month = DateTime.Today.Month,
      Year = DateTime.Today.Year,
      Description = $"{DateTime.Today:MM/yyyy}",
      LastReportingDate = DateTime.Today.AddDays(20),
      IsActive = true,
      CreatedAt = now
    });
  }

  var project = EnsureLookup(db.Projects, "נוער בסיכון", now);
  var programA = EnsureLookup(db.Programs, "תוכנית א", now);
  var programB = EnsureLookup(db.Programs, "תוכנית ב", now);
  var districtA = EnsureLookup(db.Districts, "מחוז מרכז", now);
  var districtB = EnsureLookup(db.Districts, "מחוז צפון", now);
  var sectorA = EnsureLookup(db.Sectors, "ממלכתי", now);
  var sectorB = EnsureLookup(db.Sectors, "ממלכתי דתי", now);
  var locality = EnsureLookup(db.Localities, "ירושלים", now);
  var framework = EnsureLookup(db.Frameworks, "מסגרת א", now);
  var domain = EnsureLookup(db.Domains, "תחום א", now);
  var subject = EnsureLookup(db.Subjects, "נושא א", now);
  var educationalProgram = EnsureLookup(db.EducationalPrograms, "תוכנית חינוכית א", now);
  var schoolClass = EnsureLookup(db.Classes, "כיתה א", now);
  var gradeLevel = EnsureLookup(db.GradeLevels, "שכבה א", now);
  var discussionCode = EnsureLookup(db.DiscussionCodes, "קיום דיון", now);
  var localityDistrictNational = EnsureLookup(db.LocalityDistrictNationals, "ישובי", now);
  db.SaveChanges();

  if (!db.Allocations.Any(a => a.UserId == employee.Id && a.ProjectId == project.Id))
  {
    var allocation = new Allocation
    {
      UserId = employee.Id,
      ProjectId = project.Id,
      MonthlyEmploymentScope = 180,
      DailyEmploymentScope = 9,
      AnnualEmploymentScope = 1800,
      MonthlyRowAllocation = 180,
      AnnualRowAllocation = 1800,
      OutputDuration = "0.5,1,1.5,2,2.5,3",
      AllowExcelUpload = true,
      Notes = "הקצאת דמו לצפייה מקומית",
      IsActive = true,
      CreatedAt = now
    };

    allocation.AllocationPrograms.Add(new AllocationProgram { Allocation = allocation, ProgramId = programA.Id });
    allocation.AllocationPrograms.Add(new AllocationProgram { Allocation = allocation, ProgramId = programB.Id });
    allocation.AllocationDistricts.Add(new AllocationDistrict { Allocation = allocation, DistrictId = districtA.Id });
    allocation.AllocationDistricts.Add(new AllocationDistrict { Allocation = allocation, DistrictId = districtB.Id });
    allocation.AllocationSectors.Add(new AllocationSector { Allocation = allocation, SectorId = sectorA.Id });
    allocation.AllocationSectors.Add(new AllocationSector { Allocation = allocation, SectorId = sectorB.Id });
    allocation.AllocationLocalities.Add(new AllocationLocality { Allocation = allocation, LocalityId = locality.Id });
    allocation.AllocationFrameworks.Add(new AllocationFramework { Allocation = allocation, FrameworkId = framework.Id });
    allocation.AllocationDomains.Add(new AllocationDomain { Allocation = allocation, DomainId = domain.Id });
    allocation.AllocationSubjects.Add(new AllocationSubject { Allocation = allocation, SubjectId = subject.Id });
    allocation.AllocationEducationalPrograms.Add(new AllocationEducationalProgram { Allocation = allocation, EducationalProgramId = educationalProgram.Id });
    allocation.AllocationClasses.Add(new AllocationClass { Allocation = allocation, ClassId = schoolClass.Id });
    allocation.AllocationGradeLevels.Add(new AllocationGradeLevel { Allocation = allocation, GradeLevelId = gradeLevel.Id });
    allocation.AllocationDiscussionCodes.Add(new AllocationDiscussionCode { Allocation = allocation, DiscussionCodeId = discussionCode.Id });
    allocation.AllocationLocalityDistrictNationals.Add(new AllocationLocalityDistrictNational { Allocation = allocation, LocalityDistrictNationalId = localityDistrictNational.Id });
    db.Allocations.Add(allocation);
  }

  db.SaveChanges();
}

static T EnsureLookup<T>(DbSet<T> set, string description, DateTime now) where T : AxiomaReporting.Core.Entities.Base.LookupEntity, new()
{
  var existing = set.FirstOrDefault(x => x.Description == description);
  if (existing != null) return existing;

  var entity = new T
  {
    Description = description,
    IsActive = true,
    CreatedAt = now
  };
  set.Add(entity);
  return entity;
}

public partial class Program { }
