using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class Allocation : BaseEntity
{
  public int UserId { get; set; }
  public User? User { get; set; }
  public int ProjectId { get; set; }
  public Project? Project { get; set; }
  public int? ReportTypeId { get; set; }
  public ReportType? ReportType { get; set; }
  public decimal? AnnualEmploymentScope { get; set; }
  public decimal? MonthlyEmploymentScope { get; set; }
  public decimal? DailyEmploymentScope { get; set; }
  public int? MonthlyRowAllocation { get; set; }
  public int? AnnualRowAllocation { get; set; }
  public string? OutputDuration { get; set; }
  public bool AllowExcelUpload { get; set; } = false;
  public string? Notes { get; set; }
  public bool IsActive { get; set; } = true;
  public byte[] RowVersion { get; set; } = Array.Empty<byte>();

  public ICollection<AllocationDistrict> AllocationDistricts { get; set; } = new List<AllocationDistrict>();
  public ICollection<AllocationProgram> AllocationPrograms { get; set; } = new List<AllocationProgram>();
  public ICollection<AllocationSector> AllocationSectors { get; set; } = new List<AllocationSector>();
  public ICollection<AllocationLocality> AllocationLocalities { get; set; } = new List<AllocationLocality>();
  public ICollection<AllocationFramework> AllocationFrameworks { get; set; } = new List<AllocationFramework>();
  public ICollection<AllocationSubject> AllocationSubjects { get; set; } = new List<AllocationSubject>();
  public ICollection<AllocationDomain> AllocationDomains { get; set; } = new List<AllocationDomain>();
  public ICollection<AllocationEducationalProgram> AllocationEducationalPrograms { get; set; } = new List<AllocationEducationalProgram>();
  public ICollection<AllocationClass> AllocationClasses { get; set; } = new List<AllocationClass>();
  public ICollection<AllocationGradeLevel> AllocationGradeLevels { get; set; } = new List<AllocationGradeLevel>();
  public ICollection<AllocationDiscussionCode> AllocationDiscussionCodes { get; set; } = new List<AllocationDiscussionCode>();
  public ICollection<AllocationLocalityDistrictNational> AllocationLocalityDistrictNationals { get; set; } = new List<AllocationLocalityDistrictNational>();
}
