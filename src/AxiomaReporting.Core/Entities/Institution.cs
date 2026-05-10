using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class Institution : BaseEntity
{
  public int InstitutionSymbol { get; set; }
  public string Name { get; set; } = string.Empty;
  public bool IsActive { get; set; } = true;
  public int? LocalityId { get; set; }
  public Locality? Locality { get; set; }
  public int? DistrictId { get; set; }
  public District? District { get; set; }
  public int? SectorId { get; set; }
  public Sector? Sector { get; set; }
  public int? TypeId { get; set; }
  public EducationType? Type { get; set; }
  public int? EducationalStageId { get; set; }
  public EducationalStage? EducationalStage { get; set; }
}
