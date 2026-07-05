using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Entities.Base;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.BackgroundJobs;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Infrastructure.Services;
using AxiomaReporting.Web.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;

public class Program
{
	public static void Main(string[] args)
	{
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		Settings.License = LicenseType.Community;
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
		builder.Logging.ClearProviders();
		builder.Logging.AddConsole();
		string text = Path.Combine(builder.Environment.WebRootPath ?? Path.Combine(AppContext.BaseDirectory, "wwwroot"), "fonts", "NotoSansHebrew-Regular.ttf");
		if (File.Exists(text))
		{
			using FileStream stream = File.OpenRead(text);
			FontManager.RegisterFont(stream);
		}
		else
		{
			Console.Error.WriteLine("[AxiomaReporting] Warning: Hebrew font not found at '" + text + "'. PDF error reports will fall back to the default QuestPDF font. See wwwroot/fonts/README.md.");
		}
		builder.Services.AddScoped<RequireTermsAcceptedFilter>();
		builder.Services.AddControllersWithViews(delegate(MvcOptions options)
		{
			options.Filters.AddService<RequireTermsAcceptedFilter>();
			options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((string _) => "יש לבחור ערך");
			options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((string value, string field) => "הערך שנבחר אינו תקין");
			options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((string field) => "חסר ערך חובה: " + field);
			options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "חסר ערך חובה");
			options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((string value) => "הערך שנבחר אינו תקין");
		}).AddSessionStateTempDataProvider();
		builder.Services.AddDistributedMemoryCache();
		builder.Services.AddSession(delegate(SessionOptions options)
		{
			options.IdleTimeout = TimeSpan.FromMinutes(30.0);
			options.Cookie.HttpOnly = true;
			options.Cookie.IsEssential = true;
		});
		bool useDemoInMemory = string.Equals(Environment.GetEnvironmentVariable("AXIOMA_DEMO_INMEMORY"), "true", StringComparison.OrdinalIgnoreCase);
		builder.Services.AddDbContext<AppDbContext>(delegate(DbContextOptionsBuilder options)
		{
			if (useDemoInMemory)
			{
				options.UseInMemoryDatabase("AxiomaReportingDemo");
			}
			else
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			}
		});
		builder.Services.AddScoped<IAuthService, AuthService>();
		builder.Services.AddScoped<IPasswordService, PasswordService>();
		builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
		builder.Services.AddScoped<IEmployeeService, EmployeeService>();
		builder.Services.AddScoped<IAuditLogService, AuditLogService>();
		builder.Services.AddHttpContextAccessor();
		builder.Services.AddScoped<IReportValidationService, ReportValidationService>();
		builder.Services.AddScoped<IReportStatusService, ReportStatusService>();
		builder.Services.AddScoped<ISmtpSender, SmtpSender>();
		builder.Services.AddScoped<EmailTemplateRenderer>();
		builder.Services.AddScoped<IEmailService, NotificationDispatcher>();
		builder.Services.AddScoped<IReportExcelImportService, ReportExcelImportService>();
		builder.Services.AddScoped<IPdfReportService, PdfReportService>();
		builder.Services.AddScoped<ILookupResolver, LookupResolver>();
		builder.Services.AddScoped<IBatchReportImportService, BatchReportImportService>();
		builder.Services.AddHostedService<ReminderService>();
		builder.Services.AddHostedService<NotificationRetryService>();
		builder.Services.AddScoped<IDashboardFilterService, DashboardFilterService>();
		builder.Services.AddScoped<IBrandingService, BrandingService>();
		builder.Services.AddAuthentication("Cookies").AddCookie(delegate(CookieAuthenticationOptions options)
		{
			options.LoginPath = "/Account/Login";
			options.LogoutPath = "/Account/Logout";
			options.AccessDeniedPath = "/Account/AccessDenied";
			options.ExpireTimeSpan = TimeSpan.FromMinutes(30.0);
			options.SlidingExpiration = true;
		});
		builder.Services.AddAuthorization(delegate(AuthorizationOptions options)
		{
			options.AddPolicy("AdminOnly", delegate(AuthorizationPolicyBuilder policy)
			{
				policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "1");
			});
			options.AddPolicy("AdminOrPM", delegate(AuthorizationPolicyBuilder policy)
			{
				policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "1", "2");
			});
			options.AddPolicy("AdminPMOrCoordinator", delegate(AuthorizationPolicyBuilder policy)
			{
				policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "1", "2", "3");
			});
			options.AddPolicy("CanApproveReports", delegate(AuthorizationPolicyBuilder policy)
			{
				policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "1", "2", "3", "5");
			});
			options.AddPolicy("CanViewDashboard", delegate(AuthorizationPolicyBuilder policy)
			{
				policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "1", "2", "3", "4", "5");
			});
			options.AddPolicy("CanManageLookups", delegate(AuthorizationPolicyBuilder policy)
			{
				policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "1");
			});
		});
		WebApplication webApplication = builder.Build();
		if (useDemoInMemory)
		{
			using IServiceScope serviceScope = webApplication.Services.CreateScope();
			AppDbContext requiredService = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
			requiredService.Database.EnsureCreated();
			SeedDemoData(requiredService);
		}
		if (!webApplication.Environment.IsDevelopment())
		{
			webApplication.UseExceptionHandler("/Home/Error");
			webApplication.UseHsts();
		}
		webApplication.UseHttpsRedirection();
		webApplication.UseStaticFiles();
		webApplication.UseRouting();
		webApplication.Use(async delegate(HttpContext context, Func<Task> next)
		{
			Stream originalBody = context.Response.Body;
			await using MemoryStream bufferedBody = new MemoryStream();
			context.Response.Body = bufferedBody;
			await next();
			context.Response.Body = originalBody;
			if (IsHtmlResponse(context.Response.ContentType))
			{
				bufferedBody.Position = 0L;
				using StreamReader reader = new StreamReader(bufferedBody, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
				string html = await reader.ReadToEndAsync();
				string repaired = RepairHebrewMojibake(html);
				byte[] output = Encoding.UTF8.GetBytes(repaired);
				context.Response.Headers.ContentLength = output.Length;
				await originalBody.WriteAsync(output, 0, output.Length);
			}
			else
			{
				bufferedBody.Position = 0L;
				await bufferedBody.CopyToAsync(originalBody);
			}
		});
		webApplication.UseSession();
		webApplication.UseAuthentication();
		webApplication.UseAuthorization();
		webApplication.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
		webApplication.Run();
		static bool IsHtmlResponse(string contentType)
		{
			return !string.IsNullOrWhiteSpace(contentType) && contentType.IndexOf("text/html", StringComparison.OrdinalIgnoreCase) >= 0;
		}
		static string RepairHebrewMojibake(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			StringBuilder repaired = new StringBuilder(value.Length);
			StringBuilder span = new StringBuilder();
			bool spanHasMojibake = false;
			foreach (char ch in value)
			{
				if (IsMojibakeCandidateChar(ch) || (span.Length > 0 && IsMojibakeSpanGlue(ch)))
				{
					span.Append(ch);
					spanHasMojibake = spanHasMojibake || IsMojibakeMarker(ch);
					continue;
				}
				FlushMojibakeSpan(repaired, span, spanHasMojibake);
				spanHasMojibake = false;
				repaired.Append(ch);
			}
			FlushMojibakeSpan(repaired, span, spanHasMojibake);
			return repaired.ToString();
		}
		static void FlushMojibakeSpan(StringBuilder output, StringBuilder span, bool spanHasMojibake)
		{
			if (span.Length == 0)
			{
				return;
			}
			string text = span.ToString();
			output.Append(spanHasMojibake ? RepairHebrewMojibakeSpan(text) : text);
			span.Clear();
		}
		static string RepairHebrewMojibakeSpan(string value)
		{
			Encoding hebrewEncoding = Encoding.GetEncoding(1255);
			string best = value;
			int bestScore = HebrewTextScore(value);
			string candidate = value;
			for (int i = 0; i < 6; i++)
			{
				candidate = Encoding.UTF8.GetString(hebrewEncoding.GetBytes(candidate));
				int score = HebrewTextScore(candidate);
				if (score > bestScore)
				{
					best = candidate;
					bestScore = score;
				}
			}
			return best;
		}
		static bool IsMojibakeCandidateChar(char ch)
		{
			return ch >= '\u0080' && ch != 'א' && ch != 'ב' && ch != 'ג' && ch != 'ד' && ch != 'ה' && ch != 'ו' && ch != 'ז' && ch != 'ח' && ch != 'ט' && ch != 'י' && ch != 'כ' && ch != 'ך' && ch != 'ל' && ch != 'מ' && ch != 'ם' && ch != 'נ' && ch != 'ן' && ch != 'ס' && ch != 'ע' && ch != 'פ' && ch != 'ף' && ch != 'צ' && ch != 'ץ' && ch != 'ק' && ch != 'ר' && ch != 'ש' && ch != 'ת';
		}
		static bool IsMojibakeSpanGlue(char ch)
		{
			return ch == ' ' || ch == '-' || ch == ':' || ch == '/' || ch == '(' || ch == ')' || ch == ',' || ch == '.';
		}
		static bool IsMojibakeMarker(char ch)
		{
			return ch == '\u05F3' || ch == '\u00D7' || ch == '\u00C2' || ch == '\u05B2' || ch == '\u05F2' || ch == '\u20AC' || ch == '\u2122' || ch == '\u201C' || ch == '\u201D' || ch == '\u0090' || ch == '\u009D' || ch == '\u009E';
		}
		static int HebrewTextScore(string value)
		{
			int score = 0;
			foreach (char ch in value)
			{
				if (ch >= 'א' && ch <= 'ת')
				{
					score += 4;
				}
				else if (ch == '\uFFFD')
				{
					score -= 30;
				}
				else if (IsMojibakeMarker(ch))
				{
					score -= 6;
				}
				else if (char.IsControl(ch) && ch != '\r' && ch != '\n' && ch != '\t')
				{
					score -= 20;
				}
			}
			return score;
		}
		static T EnsureLookup<T>(DbSet<T> set, string description, DateTime now) where T : notnull, LookupEntity, new()
		{
			string description2 = description;
			T val = set.FirstOrDefault((T x) => x.Description == description2);
			if (val != null)
			{
				return val;
			}
			T val2 = new T
			{
				Description = description2,
				IsActive = true,
				CreatedAt = now
			};
			set.Add(val2);
			return val2;
		}
		static void SeedDemoData(AppDbContext db)
		{
			DateTime utcNow = DateTime.UtcNow;
			PasswordService passwordService = new PasswordService();
			User user = db.Users.FirstOrDefault((User u) => u.IdNumber == "admin");
			if (user != null)
			{
				user.MustChangePassword = false;
				user.AcceptedTermsOfUse = false;
				user.LastPasswordChange = utcNow;
				user.PasswordHash = passwordService.HashPassword("admin1234");
			}
			User employee = db.Users.FirstOrDefault((User u) => u.IdNumber == "111111111");
			if (employee == null)
			{
				employee = new User
				{
					EmployeeCode = "4343343",
					IdNumber = "111111111",
					FirstName = "אורן",
					LastName = "לוינסון",
					PasswordHash = passwordService.HashPassword("Password123"),
					RoleId = 1,
					UserRoleId = 6,
					StatusId = 1,
					IsReportingEmployee = true,
					MustChangePassword = false,
					AcceptedTermsOfUse = true,
					LastPasswordChange = utcNow,
					CreatedAt = utcNow
				};
				db.Users.Add(employee);
				db.SaveChanges();
			}
			if (!db.ReportingMonths.Any((ReportingMonth m) => m.IsActive))
			{
				db.ReportingMonths.Add(new ReportingMonth
				{
					Month = DateTime.Today.Month,
					Year = DateTime.Today.Year,
					Description = $"{DateTime.Today:MM/yyyy}",
					LastReportingDate = DateTime.Today.AddDays(20.0),
					IsActive = true,
					CreatedAt = utcNow
				});
			}
			Project project = EnsureLookup<Project>(db.Projects, "נוער בסיכון", utcNow);
			AxiomaReporting.Core.Entities.Program program = EnsureLookup<AxiomaReporting.Core.Entities.Program>(db.Programs, "תוכנית א", utcNow);
			AxiomaReporting.Core.Entities.Program program2 = EnsureLookup<AxiomaReporting.Core.Entities.Program>(db.Programs, "תוכנית ב", utcNow);
			District district = EnsureLookup<District>(db.Districts, "מחוז מרכז", utcNow);
			District district2 = EnsureLookup<District>(db.Districts, "מחוז צפון", utcNow);
			Sector sector = EnsureLookup<Sector>(db.Sectors, "ממלכתי", utcNow);
			Sector sector2 = EnsureLookup<Sector>(db.Sectors, "ממלכתי דתי", utcNow);
			Locality locality = EnsureLookup<Locality>(db.Localities, "ירושלים", utcNow);
			Framework framework = EnsureLookup<Framework>(db.Frameworks, "מסגרת א", utcNow);
			Domain domain = EnsureLookup<Domain>(db.Domains, "תחום א", utcNow);
			Subject subject = EnsureLookup<Subject>(db.Subjects, "נושא א", utcNow);
			EducationalProgram educationalProgram = EnsureLookup<EducationalProgram>(db.EducationalPrograms, "תוכנית חינוכית א", utcNow);
			SchoolClass schoolClass = EnsureLookup<SchoolClass>(db.Classes, "כיתה א", utcNow);
			GradeLevel gradeLevel = EnsureLookup<GradeLevel>(db.GradeLevels, "שכבה א", utcNow);
			DiscussionCode discussionCode = EnsureLookup<DiscussionCode>(db.DiscussionCodes, "קיום דיון", utcNow);
			LocalityDistrictNational localityDistrictNational = EnsureLookup<LocalityDistrictNational>(db.LocalityDistrictNationals, "ישובי", utcNow);
			db.SaveChanges();
			if (!db.Allocations.Any((Allocation a) => a.UserId == employee.Id && a.ProjectId == project.Id))
			{
				Allocation allocation = new Allocation
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
					CreatedAt = utcNow
				};
				allocation.AllocationPrograms.Add(new AllocationProgram
				{
					Allocation = allocation,
					ProgramId = program.Id
				});
				allocation.AllocationPrograms.Add(new AllocationProgram
				{
					Allocation = allocation,
					ProgramId = program2.Id
				});
				allocation.AllocationDistricts.Add(new AllocationDistrict
				{
					Allocation = allocation,
					DistrictId = district.Id
				});
				allocation.AllocationDistricts.Add(new AllocationDistrict
				{
					Allocation = allocation,
					DistrictId = district2.Id
				});
				allocation.AllocationSectors.Add(new AllocationSector
				{
					Allocation = allocation,
					SectorId = sector.Id
				});
				allocation.AllocationSectors.Add(new AllocationSector
				{
					Allocation = allocation,
					SectorId = sector2.Id
				});
				allocation.AllocationLocalities.Add(new AllocationLocality
				{
					Allocation = allocation,
					LocalityId = locality.Id
				});
				allocation.AllocationFrameworks.Add(new AllocationFramework
				{
					Allocation = allocation,
					FrameworkId = framework.Id
				});
				allocation.AllocationDomains.Add(new AllocationDomain
				{
					Allocation = allocation,
					DomainId = domain.Id
				});
				allocation.AllocationSubjects.Add(new AllocationSubject
				{
					Allocation = allocation,
					SubjectId = subject.Id
				});
				allocation.AllocationEducationalPrograms.Add(new AllocationEducationalProgram
				{
					Allocation = allocation,
					EducationalProgramId = educationalProgram.Id
				});
				allocation.AllocationClasses.Add(new AllocationClass
				{
					Allocation = allocation,
					ClassId = schoolClass.Id
				});
				allocation.AllocationGradeLevels.Add(new AllocationGradeLevel
				{
					Allocation = allocation,
					GradeLevelId = gradeLevel.Id
				});
				allocation.AllocationDiscussionCodes.Add(new AllocationDiscussionCode
				{
					Allocation = allocation,
					DiscussionCodeId = discussionCode.Id
				});
				allocation.AllocationLocalityDistrictNationals.Add(new AllocationLocalityDistrictNational
				{
					Allocation = allocation,
					LocalityDistrictNationalId = localityDistrictNational.Id
				});
				db.Allocations.Add(allocation);
			}
			db.SaveChanges();
		}
	}
}
