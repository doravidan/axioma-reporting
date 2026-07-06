using AxiomaReporting.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AxiomaReporting.Infrastructure.Data;

public static class SeedData
{
  public static void Seed(ModelBuilder modelBuilder)
  {
    SeedReportStatuses(modelBuilder);
    SeedUserStatuses(modelBuilder);
    SeedUserRoles(modelBuilder);
    SeedEmployeeRoles(modelBuilder);
    SeedReportTypes(modelBuilder);
    SeedSystemConstants(modelBuilder);
    SeedEmailTemplates(modelBuilder);
    SeedAdminUser(modelBuilder);
    SeedTermsOfUse(modelBuilder);
  }

  private static void SeedReportTypes(ModelBuilder modelBuilder)
  {
    var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    modelBuilder.Entity<ReportType>().HasData(
      new ReportType { Id = 1, Description = "ארצי מחוזי", IsActive = true, CreatedAt = now },
      new ReportType { Id = 2, Description = "יישובי מוסדי", IsActive = true, CreatedAt = now }
    );
  }

  private static void SeedEmployeeRoles(ModelBuilder modelBuilder)
  {
    var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    modelBuilder.Entity<EmployeeRole>().HasData(
      new EmployeeRole { Id = 1, Description = "מורה", IsActive = true, CreatedAt = now },
      new EmployeeRole { Id = 2, Description = "מנהל", IsActive = true, CreatedAt = now },
      new EmployeeRole { Id = 3, Description = "רכז", IsActive = true, CreatedAt = now },
      new EmployeeRole { Id = 4, Description = "יועץ", IsActive = true, CreatedAt = now },
      new EmployeeRole { Id = 5, Description = "מפקח", IsActive = true, CreatedAt = now }
    );
  }

  private static void SeedReportStatuses(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ReportStatus>().HasData(
      new ReportStatus { Id = 1, Name = "Draft", Description = "טיוטה - הדוח נוצר אך לא הוגש" },
      new ReportStatus { Id = 2, Name = "InEntry", Description = "בהקלדה - הדוח נמצא בתהליך הקלדה" },
      new ReportStatus { Id = 3, Name = "PendingApproval", Description = "ממתין לאישור - הדוח הוגש וממתין לאישור" },
      new ReportStatus { Id = 4, Name = "Approved", Description = "מאושר - הדוח אושר" },
      new ReportStatus { Id = 5, Name = "ReturnedForCorrection", Description = "הוחזר לתיקון - הדוח הוחזר לעובד לתיקון" },
      new ReportStatus { Id = 6, Name = "Locked", Description = "נעול - הדוח נעול ואינו ניתן לעריכה" }
    );
  }

  private static void SeedUserStatuses(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<UserStatus>().HasData(
      new UserStatus { Id = 1, Name = "Active",   DescriptionHebrew = "פעיל" },
      new UserStatus { Id = 2, Name = "Inactive", DescriptionHebrew = "לא פעיל" },
      new UserStatus { Id = 3, Name = "Locked",   DescriptionHebrew = "נעול" }
    );
  }

  private static void SeedUserRoles(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<UserRole>().HasData(
      new UserRole { Id = 1, Name = "SystemAdmin",        DescriptionHebrew = "מנהל מערכת",       Description = "מנהל מערכת - גישה מלאה לכל הפונקציות" },
      new UserRole { Id = 2, Name = "ProjectManager",     DescriptionHebrew = "מנהל פרויקט",      Description = "מנהל פרויקט - ניהול עובדים, הקצאות ופתיחת חודשים" },
      new UserRole { Id = 3, Name = "ProjectCoordinator", DescriptionHebrew = "רכז פרויקט",       Description = "רכז פרויקט - יצירת עובדים, הקצאות ואישור דיווחים" },
      new UserRole { Id = 4, Name = "InspectorView",      DescriptionHebrew = "מפקח-צפייה",       Description = "מפקח צפייה - צפייה בלבד בהיקף מוגדר, ייצוא מאושרים" },
      new UserRole { Id = 5, Name = "InspectorApproval",  DescriptionHebrew = "מפקח-אישור",       Description = "מפקח אישור - צפייה + אישור/דחיית דיווחים" },
      new UserRole { Id = 6, Name = "Employee",           DescriptionHebrew = "עובד",             Description = "עובד - צפייה בנתוניו האישיים ומילוי דיווחים" }
    );
  }

  private static void SeedSystemConstants(ModelBuilder modelBuilder)
  {
    var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    modelBuilder.Entity<SystemConstant>().HasData(
      new SystemConstant
      {
        Id = 1,
        Key = "ReminderIntervalDays",
        Value = "3",
        Description = "מרווח בין תזכורות בימים",
        CreatedAt = now
      },
      new SystemConstant
      {
        Id = 2,
        Key = "ReminderStartDaysBeforeDeadline",
        Value = "7",
        Description = "כמה ימים לפני הדדליין מתחילות התזכורות",
        CreatedAt = now
      },
      new SystemConstant
      {
        Id = 3,
        Key = "NotesSimilarityThresholdPercent",
        Value = "90",
        Description = "סף אחוז דמיון בהערות (Levenshtein normalized)",
        CreatedAt = now
      },
      new SystemConstant
      {
        Id = 4,
        Key = "MaxDailyHoursDefault",
        Value = "9",
        Description = "מקסימום שעות יומי ברירת מחדל לשורת דיווח",
        CreatedAt = now
      },
      new SystemConstant
      {
        Id = 5,
        Key = "TfaEmailEnabled",
        Value = "false",
        Description = "הפעלת אימות דו-שלבי באמצעות מייל",
        CreatedAt = now
      },
      new SystemConstant
      {
        Id = 6,
        Key = "RequiredReportFields",
        Value = "AllocationId,DistrictId,LocalityId,FrameworkId,EducationalProgramId,DomainId,Subject1Id,MeetingDate,MeetingDuration",
        Description = "Developer-level required report fields. Applies to new validation from the point the value is changed.",
        CreatedAt = now
      },
      new SystemConstant
      {
        Id = 7,
        Key = "ReminderCheckIntervalHours",
        Value = "1",
        Description = "כמה שעות בין כל ריצה של שירות התזכורות",
        CreatedAt = now
      },
      new SystemConstant
      {
        Id = 8,
        Key = "PasswordExpiryWarningDays",
        Value = "14",
        Description = "כמה ימים לפני פקיעת הסיסמה לשלוח אזהרה למשתמש",
        CreatedAt = now
      },
      new SystemConstant
      {
        Id = 9,
        Key = "SiteLogoPath",
        Value = "/images/logo.png",
        Description = "נתיב הלוגו של המערכת (תמונה ב-wwwroot). ניתן להחלפה דרך מסך 'לוגו המערכת'.",
        CreatedAt = now
      }
    );
  }

  private static void SeedEmailTemplates(ModelBuilder modelBuilder)
  {
    var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    modelBuilder.Entity<EmailTemplate>().HasData(
      new EmailTemplate
      {
        Id = 1,
        TypeDescription = "ReportReceived",
        Subject = "דיווח פעילות חודשית התקבל",
        Body = "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} התקבל בהצלחה.\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      },
      new EmailTemplate
      {
        Id = 2,
        TypeDescription = "ReportApproved",
        Subject = "דיווח פעילות חודשית אושר",
        Body = "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} אושר.\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      },
      new EmailTemplate
      {
        Id = 3,
        TypeDescription = "ReportRejected",
        Subject = "דיווח פעילות חודשית הוחזר לתיקון",
        Body = "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} הוחזר לתיקון.\n\nסיבת ההחזרה: {{RejectionReason}}\n\nנא לתקן ולהגיש מחדש.\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      },
      new EmailTemplate
      {
        Id = 4,
        TypeDescription = "ReminderNotSubmitted",
        Subject = "תזכורת: דיווח פעילות חודשית טרם הוגש",
        Body = "שלום {{EmployeeName}},\n\nנא לשים לב שדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} טרם הוגש.\n\nהמועד האחרון להגשה: {{Deadline}}.\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      },
      new EmailTemplate
      {
        Id = 5,
        TypeDescription = "ReminderNeedsCorrection",
        Subject = "תזכורת: דיווח פעילות חודשית ממתין לתיקון",
        Body = "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} הוחזר לתיקון וטרם תוקן.\n\nנא לתקן ולהגיש לפני: {{Deadline}}.\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      },
      new EmailTemplate
      {
        Id = 6,
        TypeDescription = "PasswordReset",
        Subject = "איפוס סיסמה",
        Body = "שלום {{EmployeeName}},\n\nלאיפוס הסיסמה לחץ על הקישור הבא:\n{{ResetLink}}\n\nהקישור תקף לזמן מוגבל.\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      },
      new EmailTemplate
      {
        Id = 7,
        TypeDescription = "TwoFactorCode",
        Subject = "קוד אימות לכניסה למערכת",
        Body = "שלום {{EmployeeName}},\n\nקוד האימות שלך הוא: {{Code}}\n\nהקוד תקף ל-{{Minutes}} דקות.\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      },
      new EmailTemplate
      {
        Id = 8,
        TypeDescription = "PasswordExpiryWarning",
        Subject = "התראה: סיסמתך עומדת לפוג",
        Body = "שלום {{EmployeeName}},\n\nסיסמתך תפוג בעוד {{DaysLeft}} ימים (בתאריך {{ExpiryDate}}).\n\nנא להתחבר למערכת ולשנות את הסיסמה לפני מועד הפקיעה.\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      },
      new EmailTemplate
      {
        Id = 9,
        TypeDescription = "BatchImportSuccessUploader",
        Subject = "קובץ דיווח מרוכז נקלט בהצלחה",
        Body = "שלום {{UploaderName}},\n\nקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נקלט בהצלחה.\n\nסה\"כ דיווחים שנקלטו: {{RowsImported}}\nסה\"כ עובדים: {{EmployeesCount}}\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      },
      new EmailTemplate
      {
        Id = 10,
        TypeDescription = "BatchImportErrors",
        Subject = "שגיאות בקובץ דיווח מרוכז",
        Body = "שלום {{UploaderName}},\n\nבקובץ הדיווח המרוכז לחודש {{Month}}/{{Year}} נמצאו {{ErrorsCount}} שגיאות.\nשורות תקינות נקלטו למערכת; שורות שגויות לא נקלטו.\n\nשורות שלא עברו בדיקת תקינות:\n{{ErrorList}}\n\nרשימת השגיאות המפורטת מצורפת גם כקובץ PDF.\n\nבברכה,\nמערכת סייט&סאונד חינוך",
        IsActive = true,
        CreatedAt = now
      }
    );
  }

  private static void SeedAdminUser(ModelBuilder modelBuilder)
  {
    // BCrypt hash of "admin1234" with work factor 12
    // Pre-computed to avoid runtime dependency in migrations
    const string adminPasswordHash = "$2a$12$4MIlxeD2MhS0aLHvy9Gx5.on9xw87chJAN76m8ifdsBb7FvNuMw36";

    var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    modelBuilder.Entity<User>().HasData(
      new User
      {
        Id = 1,
        EmployeeCode = "ADMIN001",
        IdNumber = "admin",
        FirstName = "מנהל",
        LastName = "מערכת",
        PasswordHash = adminPasswordHash,
        RoleId = 1,
        UserRoleId = 1,
        StatusId = 1,
        IsReportingEmployee = false,
        MustChangePassword = true,
        FailedLoginAttempts = 0,
        AcceptedTermsOfUse = false,
        CreatedAt = now
      }
    );
  }

  private static void SeedTermsOfUse(ModelBuilder modelBuilder)
  {
    // Migration date (matches other seed rows). HasData is inherently idempotent —
    // EF only applies the row when its primary key is not already in the table.
    var now = new DateTime(2026, 4, 23, 0, 0, 0, DateTimeKind.Utc);

    // Placeholder Hebrew text. The client will publish the official Terms via
    // /Admin/TermsOfUse, which creates a new version row and resets every user's
    // AcceptedTermsOfUse flag, forcing re-acceptance.
    const string defaultBodyHtml = "<p>ברוכים הבאים למערכת דיווח הפעילות החודשית של סייט&amp;סאונד חינוך.</p>" +
      "<p>השימוש במערכת מותנה בהסכמה לתנאי השימוש הבאים. אנא קראו אותם בעיון לפני האישור.</p>" +
      "<p>1. השימוש במערכת מיועד לעובדים מורשים בלבד, לצורך דיווח פעילות חודשית בלבד. " +
      "אין להעביר את פרטי הכניסה לאדם אחר ואין להשתמש במערכת בשם משתמש שאינו שלך.</p>" +
      "<p>2. כל הנתונים המוזנים במערכת מהווים דיווח רשמי. עליך לוודא שכל המידע המוזן נכון, " +
      "מדויק ומשקף את הפעילות שבוצעה בפועל. דיווח כוזב מהווה הפרה של נהלי הארגון.</p>" +
      "<p>3. הארגון רשאי לבצע ביקורת על הדיווחים בכל עת. דיווחים אשר אושרו ננעלים לעריכה " +
      "ולא ניתן יהיה לשנותם ללא אישור מנהל.</p>" +
      "<p>4. המערכת שומרת יומן ביקורת של כל הפעולות. הגישה למידע מותנית בהרשאות " +
      "ובהתאם לתפקיד המוגדר במערכת.</p>" +
      "<p>5. הסיסמה שלך אישית וסודית. יש להחליפה כל 90 יום ולא לחזור על 5 הסיסמאות האחרונות. " +
      "לאחר 3 ניסיונות כניסה כושלים החשבון יינעל אוטומטית.</p>" +
      "<p>גרסה זו של תנאי השימוש מהווה גרסת ביניים — הגרסה המחייבת תפורסם על ידי " +
      "מנהל המערכת דרך מסך 'תנאי שימוש' תחת תפריט הניהול.</p>";

    modelBuilder.Entity<TermsOfUseVersion>().HasData(
      new TermsOfUseVersion
      {
        Id = 1,
        VersionNumber = 1,
        BodyHtml = defaultBodyHtml,
        EffectiveFrom = now,
        PublishedByUserId = 1,
        CreatedAt = now
      }
    );

    // Note: deliberately NO TermsOfUseAcceptance seeded for the admin user.
    // First-launch admin must accept the Terms of Use before reaching the dashboard
    // (see RequireTermsAcceptedFilter and the seeded admin's AcceptedTermsOfUse=false).
  }
}
