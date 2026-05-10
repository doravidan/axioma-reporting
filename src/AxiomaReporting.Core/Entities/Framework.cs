using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class Framework : LookupEntity
{
  public string InstitutionSymbol { get; set; } = string.Empty;
  public int? EducationalStageId { get; set; }
  public EducationalStage? EducationalStage { get; set; }
}
