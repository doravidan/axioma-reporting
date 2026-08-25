using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Enums;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Authorization;
using AxiomaReporting.Web.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;
using ClosedXML.Excel;
using System.Security.Claims;
using System.Text.RegularExpressions;

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
builder.Services.AddScoped<AxiomaReporting.Web.Authorization.RequirePasswordChangedFilter>();

builder.Services.AddControllersWithViews(options =>
{
  options.Filters.AddService<AxiomaReporting.Web.Authorization.RequireTermsAcceptedFilter>();
  // Blocks menu-navigation escape from a pending forced password change (client QA).
  options.Filters.AddService<AxiomaReporting.Web.Authorization.RequirePasswordChangedFilter>();
  options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "יש לבחור ערך");
  options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, field) => "הערך שנבחר אינו תקין");
  options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(field => $"חסר ערך חובה: {field}");
  options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "חסר ערך חובה");
  options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(value => "הערך שנבחר אינו תקין");
})
// TempData בצד השרת (session) ולא בעוגיות: רשימת שגיאות ארוכה מייבוא אקסל
// (79 שורות × כמה כללים) הגיעה ל~90KB עוגיות → כל בקשה עוקבת נכשלה ב-431
// Request Header Fields Too Large — "מסך לבן" לכל האתר עד ניקוי עוגיות.
.AddSessionStateTempDataProvider();

builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(
    builder.Configuration.GetValue<int?>("Session:TimeoutMinutes") ?? 30);
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
  options.Cookie.SameSite = SameSiteMode.Lax;
  options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
    ? CookieSecurePolicy.SameAsRequest
    : CookieSecurePolicy.Always;
});

var useDemoInMemory = string.Equals(
  Environment.GetEnvironmentVariable("AXIOMA_DEMO_INMEMORY"),
  "true",
  StringComparison.OrdinalIgnoreCase);
var useTestInMemory = builder.Configuration.GetValue<bool>("AXIOMA_TEST_INMEMORY");
var useInMemory = useDemoInMemory || useTestInMemory;

if (useInMemory)
{
  // Local/E2E hosts must never read the IIS/Production key ring or Windows
  // EventLog. Ephemeral keys also guarantee that test cookies cannot be reused
  // by another process or environment.
  builder.Services.AddDataProtection().UseEphemeralDataProtectionProvider();
  builder.Logging.ClearProviders();
  builder.Logging.AddConsole();
}

var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
AutomatedTestDatabaseGuard.EnsureSafe(
  builder.Environment.EnvironmentName, useInMemory, defaultConnection);

builder.Services.AddDbContext<AppDbContext>(options =>
{
  if (useInMemory)
    options.UseInMemoryDatabase(useDemoInMemory ? "AxiomaReportingDemo" : "AxiomaReportingTestsBootstrap");
  else
    options.UseSqlServer(defaultConnection);
});

// Auth services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<SecurityRequestLimiter>();
builder.Services.AddSingleton<IHtmlContentSanitizer, HtmlContentSanitizer>();

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

// Background jobs must never send reminders or retry notifications in local
// in-memory test hosts.
if (!useInMemory)
{
  builder.Services.AddHostedService<AxiomaReporting.Infrastructure.BackgroundJobs.ReminderService>();
  builder.Services.AddHostedService<AxiomaReporting.Infrastructure.BackgroundJobs.NotificationRetryService>();
}

// Dashboard services (AX-019, AX-020)
builder.Services.AddScoped<IDashboardFilterService, DashboardFilterService>();
builder.Services.Configure<BulkReportActionOptions>(
  builder.Configuration.GetSection(BulkReportActionOptions.SectionName));
builder.Services.AddScoped<IBulkReportActionService, BulkReportActionService>();

// Branding (AX-023 / Gap 8 — site logo from SystemConstants)
builder.Services.AddScoped<IBrandingService, BrandingService>();

// Cookie authentication.
// Inactivity timeout is configurable (client requirement: 30-minute auto-logout,
// with the value itself a parameter) — override via "Session:TimeoutMinutes".
var sessionTimeoutMinutes = builder.Configuration.GetValue<int?>("Session:TimeoutMinutes") ?? 30;
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(sessionTimeoutMinutes);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
      ? CookieSecurePolicy.SameAsRequest
      : CookieSecurePolicy.Always;
    options.Events.OnValidatePrincipal = async context =>
    {
      var identity = context.Principal?.Identity as ClaimsIdentity;
      var userIdValue = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
      if (identity == null || !int.TryParse(userIdValue, out var userId))
      {
        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return;
      }

      var now = DateTimeOffset.UtcNow;
      var validatedAtValue = context.Principal?.FindFirstValue(AuthenticationState.ValidatedAtClaim);
      if (long.TryParse(validatedAtValue, out var validatedAtUnix) &&
          now - DateTimeOffset.FromUnixTimeSeconds(validatedAtUnix) < TimeSpan.FromMinutes(5))
        return;

      using var scope = context.HttpContext.RequestServices.CreateScope();
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      var user = await db.Users.AsNoTracking()
        .Where(u => u.Id == userId)
        .Select(u => new { u.PasswordHash, u.StatusId, u.UserRoleId })
        .FirstOrDefaultAsync();

      if (user == null || user.StatusId != (int)UserStatusEnum.Active)
      {
        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return;
      }

      if (!string.Equals(
            context.Principal?.FindFirstValue(ClaimTypes.Role),
            user.UserRoleId.ToString(),
            StringComparison.Ordinal))
      {
        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return;
      }

      var expectedFingerprint = AuthenticationState.CreateFingerprint(
        user.PasswordHash, user.StatusId, user.UserRoleId);
      var ticketFingerprint = context.Principal?.FindFirstValue(AuthenticationState.FingerprintClaim);
      if (ticketFingerprint != null &&
          !string.Equals(ticketFingerprint, expectedFingerprint, StringComparison.Ordinal))
      {
        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return;
      }

      foreach (var claim in identity.FindAll(AuthenticationState.FingerprintClaim).ToList())
        identity.RemoveClaim(claim);
      foreach (var claim in identity.FindAll(AuthenticationState.ValidatedAtClaim).ToList())
        identity.RemoveClaim(claim);
      identity.AddClaim(new Claim(AuthenticationState.FingerprintClaim, expectedFingerprint));
      identity.AddClaim(new Claim(AuthenticationState.ValidatedAtClaim, now.ToUnixTimeSeconds().ToString()));
      context.ShouldRenew = true;
    };
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
app.Use(async (context, next) =>
{
  // Legacy files remain readable through authorized controller actions, but
  // are never served directly by StaticFiles. Branding stays public.
  context.Response.OnStarting(() =>
  {
    var headers = context.Response.Headers;
    headers.TryAdd("Content-Security-Policy",
      "default-src 'self'; object-src 'none'; base-uri 'self'; frame-ancestors 'none'; form-action 'self'; img-src 'self' data:; font-src 'self' data:; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline'");
    headers.TryAdd("X-Content-Type-Options", "nosniff");
    headers.TryAdd("Referrer-Policy", "no-referrer");
    headers.TryAdd("Permissions-Policy", "camera=(), microphone=(), geolocation=(), payment=(), usb=()");
    headers.TryAdd("X-Frame-Options", "DENY");

    if (context.User.Identity?.IsAuthenticated == true ||
        context.Request.Path.StartsWithSegments("/Account"))
    {
      headers["Cache-Control"] = "no-store, private";
      headers["Pragma"] = "no-cache";
      headers["Expires"] = "0";
    }

    return Task.CompletedTask;
  });

  var path = context.Request.Path;
  if (path.StartsWithSegments("/uploads/attachments") ||
      path.StartsWithSegments("/uploads/employees") ||
      path.StartsWithSegments("/uploads/private") ||
      path.StartsWithSegments("/uploads/excel-errors"))
  {
    context.Response.StatusCode = StatusCodes.Status404NotFound;
    return;
  }

  await next();
});
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
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

  // Optional local/staging inspector identity for browser tests. Credentials are
  // supplied at runtime and are never embedded in source or used against SQL.
  var inspectorIdNumber = Environment.GetEnvironmentVariable("AXIOMA_TEST_INSPECTOR_USERNAME")
    ?? "inspector";
  var inspectorPassword = Environment.GetEnvironmentVariable("AXIOMA_TEST_INSPECTOR_PASSWORD")
    ?? "InspectorTest123!";
  var inspector = db.Users.FirstOrDefault(u => u.IdNumber == inspectorIdNumber);
  if (inspector == null)
  {
    inspector = new User
    {
      EmployeeCode = "INSPECTOR-E2E",
      IdNumber = inspectorIdNumber,
      FirstName = "אלעד",
      LastName = "מפקח בדיקות",
      PasswordHash = password.HashPassword(inspectorPassword),
      RoleId = 1,
      UserRoleId = (int)UserRoleEnum.InspectorView,
      StatusId = 1,
      IsReportingEmployee = false,
      MustChangePassword = false,
      AcceptedTermsOfUse = true,
      LastPasswordChange = now,
      CreatedAt = now
    };
    db.Users.Add(inspector);
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
  var frameworkB = EnsureLookup(db.Frameworks, "מסגרת ב", now);
  var domain = EnsureLookup(db.Domains, "תחום א", now);
  var domainB = EnsureLookup(db.Domains, "תחום ב", now);
  var subject = EnsureLookup(db.Subjects, "נושא א", now);
  var subjectB = EnsureLookup(db.Subjects, "נושא ב", now);
  var educationalProgram = EnsureLookup(db.EducationalPrograms, "תוכנית חינוכית א", now);
  var educationalProgramB = EnsureLookup(db.EducationalPrograms, "תוכנית חינוכית ב", now);
  var schoolClass = EnsureLookup(db.Classes, "כיתה א", now);
  var schoolClassB = EnsureLookup(db.Classes, "כיתה ב", now);
  var gradeLevel = EnsureLookup(db.GradeLevels, "שכבה א", now);
  var gradeLevelB = EnsureLookup(db.GradeLevels, "שכבה ב", now);
  var discussionCode = EnsureLookup(db.DiscussionCodes, "קיום דיון", now);
  var discussionCodeB = EnsureLookup(db.DiscussionCodes, "דיון ב", now);
  var localityDistrictNational = EnsureLookup(db.LocalityDistrictNationals, "ישובי", now);
  var localityDistrictNationalB = EnsureLookup(db.LocalityDistrictNationals, "מחוזי", now);
  db.SaveChanges();

  if (!db.ProjectPrograms.Any(x => x.ProjectId == project.Id && x.ProgramId == programA.Id))
    db.ProjectPrograms.Add(new ProjectProgram { ProjectId = project.Id, ProgramId = programA.Id });
  if (!db.ProjectPrograms.Any(x => x.ProjectId == project.Id && x.ProgramId == programB.Id))
    db.ProjectPrograms.Add(new ProjectProgram { ProjectId = project.Id, ProgramId = programB.Id });

  if (!db.ProjectProgramSubjects.Any(x => x.ProjectId == project.Id && x.ProgramId == programA.Id && x.SubjectId == subject.Id))
    db.ProjectProgramSubjects.Add(new ProjectProgramSubject { ProjectId = project.Id, ProgramId = programA.Id, SubjectId = subject.Id });
  if (!db.ProjectProgramSubjects.Any(x => x.ProjectId == project.Id && x.ProgramId == programB.Id && x.SubjectId == subjectB.Id))
    db.ProjectProgramSubjects.Add(new ProjectProgramSubject { ProjectId = project.Id, ProgramId = programB.Id, SubjectId = subjectB.Id });
  if (!db.ProjectProgramDomains.Any(x => x.ProjectId == project.Id && x.ProgramId == programA.Id && x.DomainId == domain.Id))
    db.ProjectProgramDomains.Add(new ProjectProgramDomain { ProjectId = project.Id, ProgramId = programA.Id, DomainId = domain.Id });
  if (!db.ProjectProgramDomains.Any(x => x.ProjectId == project.Id && x.ProgramId == programB.Id && x.DomainId == domainB.Id))
    db.ProjectProgramDomains.Add(new ProjectProgramDomain { ProjectId = project.Id, ProgramId = programB.Id, DomainId = domainB.Id });
  if (!db.ProjectProgramEducationalPrograms.Any(x => x.ProjectId == project.Id && x.ProgramId == programA.Id && x.EducationalProgramId == educationalProgram.Id))
    db.ProjectProgramEducationalPrograms.Add(new ProjectProgramEducationalProgram { ProjectId = project.Id, ProgramId = programA.Id, EducationalProgramId = educationalProgram.Id });
  if (!db.ProjectProgramEducationalPrograms.Any(x => x.ProjectId == project.Id && x.ProgramId == programB.Id && x.EducationalProgramId == educationalProgramB.Id))
    db.ProjectProgramEducationalPrograms.Add(new ProjectProgramEducationalProgram { ProjectId = project.Id, ProgramId = programB.Id, EducationalProgramId = educationalProgramB.Id });
  if (!db.ProjectProgramDiscussionCodes.Any(x => x.ProjectId == project.Id && x.ProgramId == programA.Id && x.DiscussionCodeId == discussionCode.Id))
    db.ProjectProgramDiscussionCodes.Add(new ProjectProgramDiscussionCode { ProjectId = project.Id, ProgramId = programA.Id, DiscussionCodeId = discussionCode.Id });
  if (!db.ProjectProgramDiscussionCodes.Any(x => x.ProjectId == project.Id && x.ProgramId == programB.Id && x.DiscussionCodeId == discussionCodeB.Id))
    db.ProjectProgramDiscussionCodes.Add(new ProjectProgramDiscussionCode { ProjectId = project.Id, ProgramId = programB.Id, DiscussionCodeId = discussionCodeB.Id });
  if (!db.ProjectProgramFrameworks.Any(x => x.ProjectId == project.Id && x.ProgramId == programA.Id && x.FrameworkId == framework.Id))
    db.ProjectProgramFrameworks.Add(new ProjectProgramFramework { ProjectId = project.Id, ProgramId = programA.Id, FrameworkId = framework.Id });
  if (!db.ProjectProgramFrameworks.Any(x => x.ProjectId == project.Id && x.ProgramId == programB.Id && x.FrameworkId == frameworkB.Id))
    db.ProjectProgramFrameworks.Add(new ProjectProgramFramework { ProjectId = project.Id, ProgramId = programB.Id, FrameworkId = frameworkB.Id });
  if (!db.ProjectProgramGradeLevels.Any(x => x.ProjectId == project.Id && x.ProgramId == programA.Id && x.GradeLevelId == gradeLevel.Id))
    db.ProjectProgramGradeLevels.Add(new ProjectProgramGradeLevel { ProjectId = project.Id, ProgramId = programA.Id, GradeLevelId = gradeLevel.Id });
  if (!db.ProjectProgramGradeLevels.Any(x => x.ProjectId == project.Id && x.ProgramId == programB.Id && x.GradeLevelId == gradeLevelB.Id))
    db.ProjectProgramGradeLevels.Add(new ProjectProgramGradeLevel { ProjectId = project.Id, ProgramId = programB.Id, GradeLevelId = gradeLevelB.Id });
  if (!db.ProjectProgramClasses.Any(x => x.ProjectId == project.Id && x.ProgramId == programA.Id && x.ClassId == schoolClass.Id))
    db.ProjectProgramClasses.Add(new ProjectProgramClass { ProjectId = project.Id, ProgramId = programA.Id, ClassId = schoolClass.Id });
  if (!db.ProjectProgramClasses.Any(x => x.ProjectId == project.Id && x.ProgramId == programB.Id && x.ClassId == schoolClassB.Id))
    db.ProjectProgramClasses.Add(new ProjectProgramClass { ProjectId = project.Id, ProgramId = programB.Id, ClassId = schoolClassB.Id });
  if (!db.ProjectProgramLocalityDistrictNationals.Any(x => x.ProjectId == project.Id && x.ProgramId == programA.Id && x.LocalityDistrictNationalId == localityDistrictNational.Id))
    db.ProjectProgramLocalityDistrictNationals.Add(new ProjectProgramLocalityDistrictNational { ProjectId = project.Id, ProgramId = programA.Id, LocalityDistrictNationalId = localityDistrictNational.Id });
  if (!db.ProjectProgramLocalityDistrictNationals.Any(x => x.ProjectId == project.Id && x.ProgramId == programB.Id && x.LocalityDistrictNationalId == localityDistrictNationalB.Id))
    db.ProjectProgramLocalityDistrictNationals.Add(new ProjectProgramLocalityDistrictNational { ProjectId = project.Id, ProgramId = programB.Id, LocalityDistrictNationalId = localityDistrictNationalB.Id });
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

  var workbookFixtureFiles = (Environment.GetEnvironmentVariable("AXIOMA_TEST_WORKBOOK_FILES") ?? string.Empty)
    .Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    .Where(File.Exists)
    .Distinct(StringComparer.OrdinalIgnoreCase)
    .ToList();
  if (workbookFixtureFiles.Count > 0)
    SeedClientWorkbookFixtures(db, employee, now, workbookFixtureFiles);

  // Browser/E2E-only fixture data. SeedDemoData is invoked exclusively when
  // AXIOMA_DEMO_INMEMORY=true; these records can never reach a SQL database.
  framework.InstitutionSymbol = "0872903";
  if (!db.Institutions.Any(i => i.InstitutionSymbol == "872903"))
  {
    db.Institutions.Add(new Institution
    {
      InstitutionSymbol = "872903",
      Name = "הילה ישיבה פרי הארץ",
      LocalityId = locality.Id,
      DistrictId = districtA.Id,
      SectorId = sectorA.Id,
      IsActive = true,
      CreatedAt = now
    });
  }
  db.SaveChanges();

  var activeMonth = db.ReportingMonths.First(m => m.IsActive);
  var demoAllocation = db.Allocations.First(a => a.UserId == employee.Id && a.ProjectId == project.Id);
  var demoReport = db.Reports.FirstOrDefault(r =>
    r.UserId == employee.Id && r.ReportingMonthId == activeMonth.Id);
  if (demoReport == null)
  {
    demoReport = new Report
    {
      UserId = employee.Id,
      ReportingMonthId = activeMonth.Id,
      StatusId = 2,
      CreatedAt = now
    };
    db.Reports.Add(demoReport);
    db.SaveChanges();
  }

  if (!db.ReportRows.Any(r => r.ReportId == demoReport.Id))
  {
    for (var sequence = 1; sequence <= 12; sequence++)
    {
      db.ReportRows.Add(new ReportRow
      {
        ReportId = demoReport.Id,
        AllocationId = demoAllocation.Id,
        SequenceNumber = sequence,
        MeetingDate = DateTime.Today.AddDays(-sequence),
        MeetingDuration = 1,
        DistrictId = districtA.Id,
        LocalityId = locality.Id,
        FrameworkId = framework.Id,
        EducationalProgramId = educationalProgram.Id,
        DomainId = domain.Id,
        Subject1Id = subject.Id,
        DiscussionCodeId = discussionCode.Id,
        ClassId = schoolClass.Id,
        GradeLevelId = gradeLevel.Id,
        Notes = $"E2E dashboard row {sequence}",
        CreatedAt = now
      });
    }
    db.SaveChanges();
  }
}

static T EnsureLookup<T>(DbSet<T> set, string description, DateTime now) where T : AxiomaReporting.Core.Entities.Base.LookupEntity, new()
{
  var existing = set.Local.FirstOrDefault(x => x.Description == description) ??
    set.FirstOrDefault(x => x.Description == description);
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

static void SeedClientWorkbookFixtures(
  AppDbContext db,
  User employee,
  DateTime now,
  IReadOnlyCollection<string> workbookFiles)
{
  // This helper is called only from SeedDemoData while AXIOMA_DEMO_INMEMORY=true.
  // The fixture paths are discovered by the test host and passed at runtime; the
  // application has no machine-specific attachment path or production fallback.
  var testProject = EnsureLookup(db.Projects, "בדיקות קובצי לקוח", now);
  db.SaveChanges();

  foreach (var workbookFile in workbookFiles)
  {
    using var workbook = new XLWorkbook(workbookFile);
    var worksheet = workbook.Worksheets.FirstOrDefault();
    var used = worksheet?.RangeUsed();
    if (worksheet == null || used == null) continue;

    var headerRow = used.RangeAddress.FirstAddress.RowNumber;
    var lastRow = used.RangeAddress.LastAddress.RowNumber;
    IEnumerable<string> Values(int column) => Enumerable.Range(
        headerRow + 1,
        Math.Max(0, lastRow - headerRow))
      .Select(rowNumber => worksheet.Row(rowNumber).Cell(column).GetFormattedString().Trim())
      .Where(value => !string.IsNullOrWhiteSpace(value))
      .Distinct(StringComparer.OrdinalIgnoreCase);

    foreach (var value in Values(3)) EnsureLookup(db.Districts, value, now);
    foreach (var value in Values(4)) EnsureLookup(db.Localities, value, now);
    foreach (var value in Values(6)) EnsureLookup(db.EducationalPrograms, value, now);
    foreach (var value in Values(7)) EnsureLookup(db.Domains, value, now);
    foreach (var value in Values(8).Concat(Values(9)).Distinct(StringComparer.OrdinalIgnoreCase))
      EnsureLookup(db.Subjects, value, now);
    foreach (var value in Values(10)) EnsureLookup(db.DiscussionCodes, value, now);
    foreach (var value in Values(11)) EnsureLookup(db.ClassConclusions, value, now);
    foreach (var value in Values(12)) EnsureLookup(db.FrameworkConclusions, value, now);
    foreach (var value in Values(13)) EnsureLookup(db.LocalityDistrictNationals, value, now);
    foreach (var value in Values(14)) EnsureLookup(db.GradeLevels, value, now);
    foreach (var value in Values(15)) EnsureLookup(db.Classes, value, now);

    foreach (var compositeValue in Values(5))
    {
      var symbol = Regex.Match(compositeValue, @"\d{3,}").Value;
      var existingFramework = db.Frameworks.Local.FirstOrDefault(item =>
          (!string.IsNullOrWhiteSpace(symbol) && item.InstitutionSymbol == symbol) ||
          item.Description == compositeValue) ??
        db.Frameworks.FirstOrDefault(item =>
          (!string.IsNullOrWhiteSpace(symbol) && item.InstitutionSymbol == symbol) ||
          item.Description == compositeValue);
      if (existingFramework != null) continue;

      var description = compositeValue.Split('—').LastOrDefault()?.Trim();
      db.Frameworks.Add(new Framework
      {
        Description = string.IsNullOrWhiteSpace(description) ? compositeValue : description,
        InstitutionSymbol = symbol,
        IsActive = true,
        CreatedAt = now
      });
    }

    db.SaveChanges();

    var fileName = Path.GetFileName(workbookFile);
    var programDescription = fileName.Contains("ארגואן", StringComparison.OrdinalIgnoreCase)
      ? "תוכנית שמיים"
      : fileName.Contains("יוסף", StringComparison.OrdinalIgnoreCase)
        ? "כיתות שח\"ר"
        : Path.GetFileNameWithoutExtension(fileName);
    var program = EnsureLookup(db.Programs, programDescription, now);
    db.SaveChanges();

    var alreadySeeded = db.Allocations
      .Include(item => item.AllocationPrograms)
      .Any(item => item.UserId == employee.Id && item.ProjectId == testProject.Id &&
                   item.AllocationPrograms.Any(link => link.ProgramId == program.Id));
    if (alreadySeeded) continue;

    var allocation = new Allocation
    {
      UserId = employee.Id,
      ProjectId = testProject.Id,
      MonthlyEmploymentScope = 2000,
      AnnualEmploymentScope = 20000,
      MonthlyRowAllocation = 2000,
      AnnualRowAllocation = 20000,
      OutputDuration = "Unlimited",
      AllowExcelUpload = true,
      Notes = $"E2E workbook fixture: {fileName}",
      IsActive = true,
      CreatedAt = now
    };
    allocation.AllocationPrograms.Add(new AllocationProgram
    {
      Allocation = allocation,
      ProgramId = program.Id
    });
    db.Allocations.Add(allocation);
    db.SaveChanges();
  }

  var similarityThreshold = db.SystemConstants.FirstOrDefault(item =>
    item.Key == "NotesSimilarityThresholdPercent");
  if (similarityThreshold == null)
  {
    db.SystemConstants.Add(new SystemConstant
    {
      Key = "NotesSimilarityThresholdPercent",
      Value = "101",
      Description = "E2E workbook fixture: disable similarity rejection",
      CreatedAt = now
    });
  }
  else
  {
    similarityThreshold.Value = "101";
  }
  db.SaveChanges();
}

public partial class Program { }
