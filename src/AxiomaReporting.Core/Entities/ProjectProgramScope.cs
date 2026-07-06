namespace AxiomaReporting.Core.Entities;

// שיוך ערכי טבלאות קוד לפרויקט+תוכנית (K10 / משוב בטא B21, Sheet7 #3):
// בעת בחירת תוכנית בהקצאה, ערכי השדות מתמלאים אוטומטית מהשיוכים האלה.
// מסגרות מוחרגות בכוונה — לפי QA #4 מסגרת נקבעת רק לפי הקצאת העובד.

public class ProjectProgramSubject
{
  public int ProjectId { get; set; }
  public int ProgramId { get; set; }
  public int SubjectId { get; set; }
  public Subject? Subject { get; set; }
}

public class ProjectProgramDomain
{
  public int ProjectId { get; set; }
  public int ProgramId { get; set; }
  public int DomainId { get; set; }
  public Domain? Domain { get; set; }
}

public class ProjectProgramEducationalProgram
{
  public int ProjectId { get; set; }
  public int ProgramId { get; set; }
  public int EducationalProgramId { get; set; }
  public EducationalProgram? EducationalProgram { get; set; }
}

public class ProjectProgramDiscussionCode
{
  public int ProjectId { get; set; }
  public int ProgramId { get; set; }
  public int DiscussionCodeId { get; set; }
  public DiscussionCode? DiscussionCode { get; set; }
}
