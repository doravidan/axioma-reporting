using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AxiomaReporting.Core.DTOs;

public class AllocationDto
{
	public int Id { get; set; }

	[Required(ErrorMessage = "שדה חובה")]
	[Display(Name = "עובד")]
	public int UserId { get; set; }

	[Required(ErrorMessage = "שדה חובה")]
	[Display(Name = "פרויקט")]
	public int ProjectId { get; set; }

	[Display(Name = "סוג דיווח")]
	public int? ReportTypeId { get; set; }

	[Range(0, int.MaxValue, ErrorMessage = "יש להזין מספר חיובי")]
	[Display(Name = "היקף פעילות שנתי")]
	[DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
	public decimal? AnnualEmploymentScope { get; set; }

	[Range(0, int.MaxValue, ErrorMessage = "יש להזין מספר חיובי")]
	[Display(Name = "היקף פעילות חודשי")]
	[DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
	public decimal? MonthlyEmploymentScope { get; set; }

	[Range(0, int.MaxValue, ErrorMessage = "יש להזין מספר חיובי")]
	[Display(Name = "היקף פעילות יומי")]
	[DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
	public decimal? DailyEmploymentScope { get; set; }

	[Range(0, int.MaxValue, ErrorMessage = "יש להזין מספר שלם חיובי")]
	[Display(Name = "מכסת שורות חודשית")]
	public int? MonthlyRowAllocation { get; set; }

	[Range(0, int.MaxValue, ErrorMessage = "יש להזין מספר שלם חיובי")]
	[Display(Name = "מכסת שורות שנתית")]
	public int? AnnualRowAllocation { get; set; }

	[StringLength(50, ErrorMessage = "ערך ארוך מדי (מקסימום 50 תווים)")]
	[Display(Name = "משך תפוקה")]
	public string? OutputDuration { get; set; }

	[Display(Name = "אפשר העלאת אקסל")]
	public bool AllowExcelUpload { get; set; }

	[StringLength(2000, ErrorMessage = "הערות ארוכות מדי (מקסימום 2000 תווים)")]
	[Display(Name = "הערות")]
	public string? Notes { get; set; }

	[Display(Name = "פעיל")]
	public bool IsActive { get; set; } = true;


	public List<int> DistrictIds { get; set; } = new List<int>();


	public List<int> ProgramIds { get; set; } = new List<int>();


	public List<int> SectorIds { get; set; } = new List<int>();


	public List<int> LocalityIds { get; set; } = new List<int>();


	public List<int> FrameworkIds { get; set; } = new List<int>();


	public List<int> SubjectIds { get; set; } = new List<int>();


	public List<int> DomainIds { get; set; } = new List<int>();


	public List<int> EducationalProgramIds { get; set; } = new List<int>();


	public List<int> ClassIds { get; set; } = new List<int>();


	public List<int> GradeLevelIds { get; set; } = new List<int>();


	public List<int> DiscussionCodeIds { get; set; } = new List<int>();


	public List<int> LocalityDistrictNationalIds { get; set; } = new List<int>();


	public List<decimal> OutputDurationValues { get; set; } = new List<decimal>();

}
