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

// שלושת הסוגים הבאים אינם משתתפים במילוי האוטומטי של ההקצאה (QA #4 —
// מסגרת נקבעת רק לפי הקצאת העובד), אבל כן מנוהלים בעורך השיוכים של
// ניהול תוכניות לפי פרויקט, ליישור עם גרסת השרת שבה הם מאוכלסים.

public class ProjectProgramFramework
{
  public int ProjectId { get; set; }
  public int ProgramId { get; set; }
  public int FrameworkId { get; set; }
  public Framework? Framework { get; set; }
}

public class ProjectProgramGradeLevel
{
  public int ProjectId { get; set; }
  public int ProgramId { get; set; }
  public int GradeLevelId { get; set; }
  public GradeLevel? GradeLevel { get; set; }
}

public class ProjectProgramClass
{
  public int ProjectId { get; set; }
  public int ProgramId { get; set; }
  public int ClassId { get; set; }
  public SchoolClass? SchoolClass { get; set; }
}

public class ProjectProgramLocalityDistrictNational
{
  public int ProjectId { get; set; }
  public int ProgramId { get; set; }
  public int LocalityDistrictNationalId { get; set; }
  public LocalityDistrictNational? LocalityDistrictNational { get; set; }
}
