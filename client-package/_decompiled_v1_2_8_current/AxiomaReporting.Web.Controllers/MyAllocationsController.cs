using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AxiomaReporting.Core.Entities;
using AxiomaReporting.Core.Interfaces;
using AxiomaReporting.Infrastructure.Data;
using AxiomaReporting.Web.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Web.Controllers;

[Authorize(Roles = "6")]
public class MyAllocationsController : Controller
{
	private readonly AppDbContext _db;

	private readonly ICurrentUserService _currentUser;

	public MyAllocationsController(AppDbContext db, ICurrentUserService currentUser)
	{
		_db = db;
		_currentUser = currentUser;
	}

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		ReportingMonth activeMonth = await _db.ReportingMonths.AsNoTracking().FirstOrDefaultAsync((ReportingMonth m) => m.IsActive);
		int userId = _currentUser.UserId;
		List<Allocation> list = await (from a in MyAllocationsQuery(userId)
			orderby a.Project.Description, a.Id
			select a).ToListAsync();
		Allocation allocation = list.FirstOrDefault((Allocation a) => a.AllowExcelUpload);
		MyAllocationsViewModel model = new MyAllocationsViewModel
		{
			ActiveMonth = activeMonth,
			AllocationCount = list.Count,
			AllowExcelUpload = (allocation != null),
			ExcelUploadAllocationId = allocation?.Id,
			Allocations = list
		};
		return View(model);
	}

	[HttpGet]
	public async Task<IActionResult> Details(int id)
	{
		Allocation allocation = await MyAllocationsQuery(_currentUser.UserId).FirstOrDefaultAsync((Allocation a) => a.Id == id);
		IActionResult result;
		if (allocation != null)
		{
			IActionResult actionResult = View(allocation);
			result = actionResult;
		}
		else
		{
			IActionResult actionResult = NotFound();
			result = actionResult;
		}
		return result;
	}

	[HttpGet]
	public async Task<IActionResult> ExportExcel()
	{
		List<Allocation> list = await (from a in MyAllocationsQuery(_currentUser.UserId)
			orderby a.Project.Description, a.Id
			select a).ToListAsync();
		using XLWorkbook xLWorkbook = new XLWorkbook();
		IXLWorksheet iXLWorksheet = xLWorkbook.Worksheets.Add("ההקצאות שלי");
		iXLWorksheet.RightToLeft = true;
		string[] array = new string[10] { "פרויקט", "תוכנית", "מחוז", "מגזר", "היקף פעילות חודשי", "היקף פעילות יומי", "היקף פעילות שנתי", "משך תפוקה", "אפשר העלאת אקסל", "הערות" };
		for (int i = 0; i < array.Length; i++)
		{
			iXLWorksheet.Cell(1, i + 1).Value = array[i];
		}
		int num = 2;
		foreach (Allocation item in list)
		{
			iXLWorksheet.Cell(num, 1).Value = item.Project?.Description;
			iXLWorksheet.Cell(num, 2).Value = JoinValues(item.AllocationPrograms.Select((AllocationProgram x) => x.Program?.Description));
			iXLWorksheet.Cell(num, 3).Value = JoinValues(item.AllocationDistricts.Select((AllocationDistrict x) => x.District?.Description));
			iXLWorksheet.Cell(num, 4).Value = JoinValues(item.AllocationSectors.Select((AllocationSector x) => x.Sector?.Description));
			iXLWorksheet.Cell(num, 5).Value = item.MonthlyEmploymentScope;
			iXLWorksheet.Cell(num, 6).Value = item.DailyEmploymentScope?.ToString("0.##") ?? "ללא הגבלה";
			iXLWorksheet.Cell(num, 7).Value = item.AnnualEmploymentScope;
			iXLWorksheet.Cell(num, 8).Value = item.OutputDuration;
			iXLWorksheet.Cell(num, 9).Value = (item.AllowExcelUpload ? "כן" : "לא");
			iXLWorksheet.Cell(num, 10).Value = item.Notes;
			num++;
		}
		iXLWorksheet.Row(1).Style.Font.Bold = true;
		iXLWorksheet.Columns().AdjustToContents();
		using MemoryStream memoryStream = new MemoryStream();
		xLWorkbook.SaveAs(memoryStream);
		return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"my_allocations_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
	}

	private IQueryable<Allocation> MyAllocationsQuery(int userId)
	{
		return from a in _db.Allocations.AsNoTracking().AsSplitQuery().Include((Allocation a) => a.User).Include((Allocation a) => a.Project)
				.Include((Allocation a) => a.AllocationDistricts)
				.ThenInclude((AllocationDistrict x) => x.District)
				.Include((Allocation a) => a.AllocationPrograms)
				.ThenInclude((AllocationProgram x) => x.Program)
				.Include((Allocation a) => a.AllocationSectors)
				.ThenInclude((AllocationSector x) => x.Sector)
				.Include((Allocation a) => a.AllocationLocalities)
				.ThenInclude((AllocationLocality x) => x.Locality)
				.Include((Allocation a) => a.AllocationFrameworks)
				.ThenInclude((AllocationFramework x) => x.Framework)
				.Include((Allocation a) => a.AllocationSubjects)
				.ThenInclude((AllocationSubject x) => x.Subject)
				.Include((Allocation a) => a.AllocationDomains)
				.ThenInclude((AllocationDomain x) => x.Domain)
				.Include((Allocation a) => a.AllocationEducationalPrograms)
				.ThenInclude((AllocationEducationalProgram x) => x.EducationalProgram)
				.Include((Allocation a) => a.AllocationClasses)
				.ThenInclude((AllocationClass x) => x.SchoolClass)
				.Include((Allocation a) => a.AllocationGradeLevels)
				.ThenInclude((AllocationGradeLevel x) => x.GradeLevel)
				.Include((Allocation a) => a.AllocationDiscussionCodes)
				.ThenInclude((AllocationDiscussionCode x) => x.DiscussionCode)
				.Include((Allocation a) => a.AllocationLocalityDistrictNationals)
				.ThenInclude((AllocationLocalityDistrictNational x) => x.LocalityDistrictNational)
			where a.UserId == userId && a.IsActive
			select a;
	}

	private static string JoinValues(IEnumerable<string?> values)
	{
		List<string> list = values.Where((string v) => !string.IsNullOrWhiteSpace(v)).Distinct().ToList();
		if (list.Count != 0)
		{
			return string.Join(", ", list);
		}
		return "-";
	}
}
