using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

/// <summary>
/// גרסה מפורסמת של מדיניות הפרטיות. הגרסה עם המספר הגבוה ביותר היא
/// המוצגת למשתמשים במסך מדיניות הפרטיות (יישור לגרסת השרת).
/// </summary>
public class PrivacyPolicyVersion : BaseEntity
{
  public int VersionNumber { get; set; }
  public string BodyHtml { get; set; } = string.Empty;
  public DateTime EffectiveFrom { get; set; }
  public int PublishedByUserId { get; set; }
  public User? PublishedByUser { get; set; }
}
