# מדריך התקנה ועלייה לאוויר — מערכת דיווח עובדים אקסיומא

**גרסה:** 1.0 · **תאריך:** 2026-04-23

---

## תוכן עניינים

1. [דרישות מקדימות](#1-דרישות-מקדימות)
2. [הכנת שרת Windows + IIS](#2-הכנת-שרת-windows--iis)
3. [התקנת SQL Server Express](#3-התקנת-sql-server-express)
4. [יצירת מסד הנתונים והפעלת המיגרציות](#4-יצירת-מסד-הנתונים-והפעלת-המיגרציות)
5. [פרסום האפליקציה](#5-פרסום-האפליקציה)
6. [קונפיגורציה של IIS](#6-קונפיגורציה-של-iis)
7. [קובץ ההגדרות הייצורי](#7-קובץ-ההגדרות-הייצורי)
8. [צעדי הפעלה ראשונה](#8-צעדי-הפעלה-ראשונה)
9. [ייבוא נתוני בסיס (טבלאות עזר ועובדים)](#9-ייבוא-נתוני-בסיס-טבלאות-עזר-ועובדים)
10. [מבנה מסד הנתונים — כלל הטבלאות](#10-מבנה-מסד-הנתונים--כלל-הטבלאות)
11. [קבועי המערכת (SystemConstants)](#11-קבועי-המערכת-systemconstants)
12. [בדיקת תקינות לאחר פריסה](#12-בדיקת-תקינות-לאחר-פריסה)
13. [תמיכה שוטפת](#13-תמיכה-שוטפת)

---

## 1. דרישות מקדימות

| רכיב | גרסה מינימלית | הערות |
|------|--------------|-------|
| Windows Server | 2019 (build 1809) | 2022 מומלץ |
| .NET 8 Hosting Bundle | 8.0.x | כולל ASP.NET Core Runtime |
| IIS | 10 | עם מודול ASP.NET Core Module v2 |
| SQL Server Express | 2019 | Express Edition מספיק לארגון בגודל זה |
| SSL Certificate | כל CA מוכר | Let's Encrypt בחינם דרך win-acme |
| SMTP Relay | — | חשבון דוא"ל ייעודי לשליחת התראות |
| דיסק | 20 GB+ | למסד הנתונים, לוגים, ואחסון קבצים |
| RAM | 4 GB+ | 8 GB מומלץ |

---

## 2. הכנת שרת Windows + IIS

### 2.1 התקנת IIS

פתח PowerShell כמנהל מערכת:

```powershell
# התקנת IIS עם הרכיבים הנדרשים
Install-WindowsFeature -Name Web-Server, Web-Common-Http, Web-Static-Content,
  Web-Default-Doc, Web-Http-Errors, Web-Http-Logging, Web-Filtering,
  Web-Stat-Compression, Web-Windows-Auth, Web-Mgmt-Console -IncludeManagementTools

# אימות שהמודול של ASP.NET Core רשום (לאחר התקנת Hosting Bundle)
& "$env:windir\system32\inetsrv\appcmd.exe" list modules /name:AspNetCoreModuleV2
```

### 2.2 התקנת .NET 8 Hosting Bundle

1. הורד מ: `https://dotnet.microsoft.com/download/dotnet/8.0` — בחר **".NET 8.0 Hosting Bundle"**
2. הרץ את ה-installer כמנהל מערכת
3. הפעל מחדש את IIS: `iisreset /restart`
4. אמת: `dotnet --version` (צריך להחזיר `8.0.x`)

### 2.3 יצירת תיקיית האפליקציה

```powershell
New-Item -ItemType Directory -Path "C:\inetpub\AxiomaReporting"
New-Item -ItemType Directory -Path "C:\inetpub\AxiomaReporting\wwwroot\uploads"
New-Item -ItemType Directory -Path "D:\backups\AxiomaReporting"
```

---

## 3. התקנת SQL Server Express

### 3.1 התקנה

1. הורד SQL Server 2019 Express מהאתר הרשמי של Microsoft
2. בעת ההתקנה בחר: **"Basic"** ← פשוט ומהיר, מתאים לייצור
3. שמור את מחרוזת החיבור שתוצג בסוף ההתקנה
4. הרץ גם את **SQL Server Management Studio (SSMS)** להתקנה נוחה לניהול

### 3.2 אפשור TCP/IP (חובה לחיבור מהאפליקציה)

```
SQL Server Configuration Manager →
  SQL Server Network Configuration →
    Protocols for SQLEXPRESS →
      TCP/IP → Enable → Properties → IPAll → TCP Port = 1433
SQL Server Services → SQL Server (SQLEXPRESS) → Restart
```

### 3.3 יצירת מסד הנתונים

התחבר ל-SSMS כמנהל מערכת (`sa` או Windows Auth) והרץ:

```sql
-- יצירת מסד הנתונים עם Collation עברי
CREATE DATABASE AxiomaReporting
  COLLATE Hebrew_CI_AS;
GO

ALTER DATABASE AxiomaReporting SET RECOVERY SIMPLE;
GO

-- משתמש SQL ייעודי עם הרשאות מינימליות
USE [master];
CREATE LOGIN [AxiomaWeb]
  WITH PASSWORD = N'REPLACE_WITH_STRONG_PASSWORD_MIN16CHARS',
  CHECK_POLICY = ON,
  CHECK_EXPIRATION = OFF;
GO

USE [AxiomaReporting];
CREATE USER [AxiomaWeb] FOR LOGIN [AxiomaWeb];
ALTER ROLE db_datareader ADD MEMBER [AxiomaWeb];
ALTER ROLE db_datawriter ADD MEMBER [AxiomaWeb];
GRANT EXECUTE ON SCHEMA::dbo TO [AxiomaWeb];
GO
```

> **אבטחה:** אל תשתמש ב-`sa`, ב-`db_owner`, או ב-`sysadmin` לחשבון האפליקציה.
> שינויי סכמה (migrations) ירוצו פעם אחת עם חשבון מורם בנפרד.

---

## 4. יצירת מסד הנתונים והפעלת המיגרציות

### 4.1 שיטה א — SQL Script (מומלץ לייצור)

קובץ `database/schema.sql` בחבילת המשלוח מכיל את כל ה-DDL באופן idempotent.

```powershell
# הפעלה מה-PowerShell של השרת
sqlcmd -S .\SQLEXPRESS -d AxiomaReporting -E -i "C:\deploy\schema.sql" -o "C:\deploy\schema_log.txt"
# בדוק את schema_log.txt — לא אמורות להיות שגיאות
```

הסקריפט:
- יוצר את כל 40+ הטבלאות עם האינדקסים, ה-FKs, וה-constraints
- זהו להגיש — ניתן להריץ פעמים ללא נזק (IF NOT EXISTS על כל סעיף)
- כולל את נתוני הבסיס: סטטוסים, תבניות מייל, קבועי מערכת, ומשתמש Admin ראשוני

### 4.2 שיטה ב — EF Migrations (מפתחים / CI)

```powershell
# מהתיקייה שבה נמצאת הסולושן
cd C:\deploy\source
dotnet ef database update `
  --project src\AxiomaReporting.Infrastructure `
  --startup-project src\AxiomaReporting.Web `
  --connection "Server=.\SQLEXPRESS;Database=AxiomaReporting;User Id=AxiomaWeb;Password=REPLACE_ME;TrustServerCertificate=True"
```

---

## 5. פרסום האפליקציה

### 5.1 על מחשב הפיתוח

```powershell
cd "F:\דווח עובדים אקסיומא"

dotnet publish src\AxiomaReporting.Web\AxiomaReporting.Web.csproj `
  --configuration Release `
  --output "C:\deploy\publish" `
  --self-contained false `
  --runtime win-x64
```

### 5.2 העברה לשרת

העתק את כל תוכן `C:\deploy\publish\` ל-`C:\inetpub\AxiomaReporting\` בשרת.

**שים לב:** אל תדרוס את `appsettings.Production.json` אם כבר קיים — זה הקובץ עם הסיסמאות!

### 5.3 הרשאות על התיקיות

```powershell
# החלף IIS_IUSRS בשם ה-Identity של ה-App Pool אם שונה
icacls "C:\inetpub\AxiomaReporting\wwwroot\uploads" /grant "IIS_IUSRS:(OI)(CI)(M)"
icacls "C:\inetpub\AxiomaReporting\wwwroot\fonts"   /grant "IIS_IUSRS:(OI)(CI)(R)"
icacls "C:\inetpub\AxiomaReporting\wwwroot\images"  /grant "IIS_IUSRS:(OI)(CI)(M)"
```

---

## 6. קונפיגורציה של IIS

### 6.1 יצירת Application Pool

ב-IIS Manager (inetmgr):

| הגדרה | ערך |
|-------|-----|
| שם | `AxiomaReporting` |
| .NET CLR version | `No Managed Code` |
| Managed pipeline mode | `Integrated` |
| Start Mode | `AlwaysRunning` |
| Idle Time-out | `0` (נטרל) |
| Regular Time Interval (recycle) | `0` או `03:00` |
| Identity | `ApplicationPoolIdentity` (ברירת מחדל) |

```powershell
# אפשרות PowerShell
Import-Module WebAdministration
New-WebAppPool -Name "AxiomaReporting"
Set-ItemProperty IIS:\AppPools\AxiomaReporting managedRuntimeVersion ""
Set-ItemProperty IIS:\AppPools\AxiomaReporting startMode AlwaysRunning
Set-ItemProperty IIS:\AppPools\AxiomaReporting processModel.idleTimeout ([TimeSpan]::Zero)
```

### 6.2 יצירת ה-Site

```powershell
New-WebSite -Name "AxiomaReporting" `
  -PhysicalPath "C:\inetpub\AxiomaReporting" `
  -ApplicationPool "AxiomaReporting" `
  -Port 443 `
  -Ssl

# הגדרת HTTPS Binding עם האישור שלך
# (ב-IIS Manager: Site → Bindings → Add → HTTPS, בחר אישור)
```

### 6.3 משתנה סביבה

ב-IIS Manager: `AxiomaReporting` (site) → **Configuration Editor** →
`system.webServer/aspNetCore` → `environmentVariables` → הוסף:

| Name | Value |
|------|-------|
| `ASPNETCORE_ENVIRONMENT` | `Production` |

### 6.4 HTTPS Redirect (URL Rewrite)

התקן **URL Rewrite** module של IIS אם לא מותקן, ואז:

בקובץ `web.config` בתיקיית האפליקציה (נוצר אוטומטית בפרסום) — הוסף לפני `</system.webServer>`:

```xml
<rewrite>
  <rules>
    <rule name="HTTP to HTTPS" stopProcessing="true">
      <match url="(.*)" />
      <conditions>
        <add input="{HTTPS}" pattern="^OFF$" />
      </conditions>
      <action type="Redirect" url="https://{HTTP_HOST}/{R:1}"
              redirectType="Permanent" />
    </rule>
  </rules>
</rewrite>
```

---

## 7. קובץ ההגדרות הייצורי

צור בשרת: `C:\inetpub\AxiomaReporting\appsettings.Production.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=AxiomaReporting;User Id=AxiomaWeb;Password=REPLACE_WITH_DB_PASSWORD;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  },
  "AllowedHosts": "your-domain.co.il",
  "Database": {
    "AutoMigrate": false
  }
}
```

> **חשוב:** קובץ זה **לא** נמצא ב-Git ו-**לא** מיועד לשיתוף. הכלל: סיסמאות לא עוברות בקוד.
> 
> הגדרות SMTP (שרת, פורט, משתמש, סיסמה) מאוחסנות **במסד הנתונים** בטבלת `EmailServerSettings`
> ומוגדרות דרך ממשק המנהל, לא בקובץ זה.

---

## 8. צעדי הפעלה ראשונה

לאחר העלאת האפליקציה לאוויר, בצע בסדר הבא:

### שלב 1 — כניסה ראשונה של Admin

- כתובת: `https://your-domain.co.il/Account/Login`
- קוד עובד ברירת מחדל: **`ADMIN`**
- סיסמה ברירת מחדל: **`Admin123`**
- המערכת תדרוש שינוי סיסמה מיידי. בחר סיסמה חזקה (8+ תווים, אותיות + ספרות)

### שלב 2 — קבלת תנאי שימוש

- המערכת תציג את תנאי השימוש לאישור — קרא ואשר

### שלב 3 — הגדרת שרת דוא"ל

- נווט ל: `/Admin/EmailServerSettings`
- מלא: שרת SMTP, פורט, שם משתמש, סיסמה, כתובת שולח, שם שולח
- לחץ "שמור" ואז "שלח מייל בדיקה" — ודא קבלת המייל
- בדוק `/Admin/NotificationLogs` — השורה החדשה צריכה להיות בסטטוס `Sent`

### שלב 4 — העלאת לוגו

- נווט ל: `/Admin/Branding`
- העלה את לוגו הארגון (PNG/SVG, מומלץ 200×50 פיקסל לרוחב)

### שלב 5 — פרסום תנאי שימוש עדכניים

- נווט ל: `/Admin/TermsOfUse`
- ערוך את גוף תנאי השימוש ולחץ "פרסם גרסה חדשה" (התוכן מוצג כטקסט בטוח)
- כל המשתמשים יתבקשו לאשר בכניסה הבאה

### שלב 6 — הגדרת חודש דיווח פעיל

- נווט ל: `/Admin/ReportingMonths`
- לחץ "הוסף חודש" — הזן שנה+חודש ותאריך סגירה לדיווח
- סמן "פעיל" — רק חודש אחד יכול להיות פעיל בכל רגע נתון

### שלב 7 — הגדרת פרויקטים

- נווט ל: `/Admin/Projects` (או `/Lookup?tableName=projects`)
- הוסף את הפרויקטים שהארגון מנהל

### שלב 8 — ייבוא טבלאות עזר (ראה סעיף 9)

### שלב 9 — יצירת משתמשי Admin/PM נוספים

- נווט ל: `/Admin/Users` → "הוסף משתמש"
- הגדר תפקיד Admin/מנהל פרויקט לפי הצורך
- הסיסמה הראשונית היא תעודת הזהות; המשתמש יתבקש לשנות בכניסה ראשונה

---

## 9. ייבוא נתוני בסיס (טבלאות עזר ועובדים)

### 9.1 ייבוא טבלאות עזר מקובץ Excel

ניתן לייבא כל טבלת עזר דרך ממשק המנהל:

1. נווט ל: `/Lookup?tableName=<שם_הטבלה>`
2. לחץ **"ייבוא אקסל"**
3. הקובץ צריך להיות `.xlsx` — שורה ראשונה כותרת, עמודה ראשונה תיאור

**הטבלאות שיש לייבא לפני הוספת עובדים:**

| טבלה (URL) | תיאור |
|-----------|-------|
| `districts` | מחוזות |
| `sectors` | מגזרים |
| `localities` | יישובים (עם קוד ארצי) |
| `educational-stages` | שלבי חינוך |
| `educational-programs` | תוכניות חינוכיות |
| `subjects` | מקצועות |
| `domains` | תחומים |
| `authorities` | רשויות |
| `class-designations` | ייעודי כיתות |
| `grade-levels` | שכבות גיל |
| `discussion-codes` | קודי דיון |
| `programs` | תוכניות |
| `projects` | פרויקטים |
| `institutions` | מוסדות |
| `frameworks` | מסגרות (Institution × EducationalStage — ייחודי) |

### 9.2 ייבוא עובדים מ-Excel

1. נווט ל: `/Admin/BatchReportImport` (ייבוא אקסל מרוכז)
2. לחלופין, הוסף עובדים ידנית: `/Employees/Create`
3. לכל עובד הגדר הקצאה: `/Employees/{id}/Allocations`

### 9.3 ייבוא דוחות היסטוריים (Python, חד-פעמי)

סקריפטים בתיקייה `database/seed-data/`:

```bash
# על שרת עם Python 3.8+
pip install pyodbc pandas openpyxl

# ייבוא טבלאות עזר
python seed_lookups.py --server ".\SQLEXPRESS" --db AxiomaReporting --file "טבלאות.xlsb"

# ייבוא דוחות היסטוריים
python seed_reports.py --server ".\SQLEXPRESS" --db AxiomaReporting --file "BASE DATA.xlsb"
```

---

## 10. מבנה מסד הנתונים — כלל הטבלאות

### 10.1 טבלאות עזר (Lookup Tables) — 17 טבלאות

כל טבלת עזר כוללת: `Id (PK)`, `Description (nvarchar 500)`, `IsActive (bit)`, `CreatedAt`, `UpdatedAt`

| שם טבלה | תיאור | הערות מיוחדות |
|---------|-------|--------------|
| `Authorities` | רשויות | — |
| `ClassDesignations` | ייעודי כיתות | — |
| `DiscussionCodes` | קודי דיון | — |
| `Districts` | מחוזות | — |
| `Domains` | תחומים | — |
| `EducationalPrograms` | תוכניות חינוכיות | — |
| `EducationalStages` | שלבי חינוך | — |
| `GradeLevels` | שכבות גיל | — |
| `Institutions` | מוסדות | — |
| `Localities` | יישובים | + `NationalCode (int?)` |
| `Programs` | תוכניות | — |
| `Projects` | פרויקטים | — |
| `Sectors` | מגזרים | — |
| `Subjects` | מקצועות | — |
| `Frameworks` | מסגרות | + `InstitutionSymbol`, `InstitutionId (FK)`, `EducationalStageId (FK)`; UNIQUE(InstitutionSymbol, EducationalStageId) |
| `ReportTypes` | סוגי דיווח | ערכי ברירת מחדל: "ארצי מחוזי", "יישובי מוסדי" |
| `ProjectPrograms` | קישור פרויקט-תוכנית | PK(ProjectId, ProgramId) |

### 10.2 טבלאות מערכת

| שם טבלה | תיאור |
|---------|-------|
| `Users` | עובדים ומשתמשים (ראה שדות מלאים מטה) |
| `Allocations` | הקצאות עובד לפרויקט (1 הקצאה לעובד לפרויקט) |
| `ReportingMonths` | חודשי דיווח; רק אחד פעיל |
| `Reports` | דוח חודשי לפי עובד+חודש |
| `ReportRows` | שורות פעילות בדוח |
| `EmailServerSettings` | הגדרות שרת SMTP |
| `EmailTemplates` | תבניות דוא"ל (10 סוגים) |
| `SystemConstants` | קבועי מערכת (ראה סעיף 11) |
| `UserStatuses` | סטטוסי משתמש (פעיל/לא פעיל/נעול) |
| `ReportStatuses` | סטטוסי דוח (טיוטה/בהזנה/ממתין/מאושר/הוחזר/נעול) |

### 10.3 טבלאות אבטחה ומעקב

| שם טבלה | תיאור |
|---------|-------|
| `PasswordResetTokens` | טוקני איפוס סיסמה (חד-פעמיים, עם תפוגה) |
| `TwoFactorCodes` | קודי אימות דו-שלבי (TFA) |
| `TermsOfUseVersions` | גרסאות תנאי שימוש |
| `TermsOfUseAcceptances` | רשומות אישור תנאי שימוש לפי משתמש+גרסה |
| `NotificationLogs` | לוג כל הדוא"לים שנשלחו (עם סטטוס ומספר ניסיונות) |
| `AuditLogs` | לוג פעולות מלא (שינויים, כניסות, אישורים) |
| `ReminderLogs` | לוג תזכורות לצורך מניעת שליחה כפולה |
| `InspectorAssignments` | שיוך מפקחים לקבוצות נצפות |

### 10.4 טבלאות צומת (Junction Tables) — הקצאות

| שם טבלה | מחבר בין |
|---------|---------|
| `AllocationDistricts` | Allocation ↔ District |
| `AllocationSectors` | Allocation ↔ Sector |
| `AllocationLocalities` | Allocation ↔ Locality |
| `AllocationFrameworks` | Allocation ↔ Framework |
| `AllocationSubjects` | Allocation ↔ Subject |
| `AllocationDomains` | Allocation ↔ Domain |
| `AllocationEducationalPrograms` | Allocation ↔ EducationalProgram |
| `AllocationClasses` | Allocation ↔ ClassDesignation |
| `AllocationGradeLevels` | Allocation ↔ GradeLevel |
| `AllocationDiscussionCodes` | Allocation ↔ DiscussionCode |
| `AllocationPrograms` | Allocation ↔ Program |
| `AllocationLocalityDistrictNationals` | Allocation ↔ DistrictNational |

### 10.5 שדות מפתח — טבלת `Users`

| שדה | סוג | תיאור |
|-----|-----|-------|
| `Id` | int PK | מזהה פנימי |
| `IdNumber` | nvarchar(20) UNIQUE | תעודת זהות (Username לכניסה) |
| `EmployeeCode` | nvarchar(50) UNIQUE | קוד עובד |
| `FirstName` | nvarchar(100) | שם פרטי |
| `LastName` | nvarchar(100) | שם משפחה |
| `Email` | nvarchar(200) | דוא"ל לקבלת התראות |
| `PasswordHash` | nvarchar(256) | BCrypt hash |
| `RoleId` | int FK → Roles | תפקיד (1-6) |
| `StatusId` | int FK → UserStatuses | 1=פעיל, 2=לא פעיל, 3=נעול |
| `FailedLoginAttempts` | int | מתאפס בכניסה מוצלחת |
| `MustChangePassword` | bit | אילוץ שינוי בכניסה הבאה |
| `LastPasswordChange` | datetime2? | לניהול תפוגת סיסמה (90 יום) |
| `RestDay` | int? | 0=ראשון .. 6=שבת |
| `AllowFutureReporting` | bit | מאפשר דיווח על חודש עתידי |
| `AcceptedTermsOfUse` | bit | האם אישר את גרסת ToU הנוכחית |
| `TfaEnabled` | bit | האם TFA מופעל למשתמש |
| `RowVersion` | rowversion | לניהול concurrency |

### 10.6 שדות מפתח — טבלת `Allocations`

| שדה | סוג | תיאור |
|-----|-----|-------|
| `Id` | int PK | מזהה |
| `UserId` | int FK UNIQUE(+ProjectId) | עובד |
| `ProjectId` | int FK | פרויקט |
| `MonthlyEmploymentScope` | decimal(5,2) | היקף העסקה חודשי |
| `AnnualEmploymentScope` | decimal(5,2) | היקף העסקה שנתי |
| `DailyEmploymentScope` | decimal(5,2)? | היקף יומי (null = ללא הגבלה) |
| `OutputDuration` | nvarchar(200) | ערכי משך תפוקה מורשים (CSV: "0.5,1,1.5") |
| `AllowExcelUpload` | bit | האם מורשה להעלאת Excel |
| `Notes` | nvarchar(1000)? | הערות |
| `RowVersion` | rowversion | לניהול concurrency |

---

## 11. קבועי המערכת (SystemConstants)

ניתן לשנות את כל הקבועים דרך: `/Admin/SystemConstants`

| מפתח | ערך ברירת מחדל | תיאור |
|------|---------------|-------|
| `ReminderIntervalDays` | `3` | ימים בין תזכורות |
| `ReminderStartDaysBeforeDeadline` | `7` | כמה ימים לפני סגירת החודש להתחיל תזכורות |
| `NotesSimilarityThresholdPercent` | `90` | אחוז דמיון מינימלי להתראה על הערות דומות |
| `MaxDailyHoursDefault` | `9` | מקסימום שעות יומי בלי הגבלה מפורשת |
| `TfaEmailEnabled` | `false` | הפעל אימות דו-שלבי לכל המשתמשים |
| `RequiredReportFields` | רשימת שדות | שדות חובה בטופס הדיווח (פסיק מפריד) |
| `SiteLogoPath` | `/images/logo.png` | נתיב ללוגו המוצג בניווט |

### תבניות דוא"ל

המערכת מגיעה עם 10 תבניות מוגדרות מראש בטבלת `EmailTemplates`.
ניתן לערוך אותן ב: `/Admin/EmailTemplates`

| סוג תבנית | תיאור |
|-----------|-------|
| `ReportReceived` | אישור קבלת דוח |
| `ReportApproved` | הדוח אושר |
| `ReportRejected` | הדוח הוחזר לתיקון (עם סיבה) |
| `ReminderNotSubmitted` | תזכורת לא הגיש |
| `ReminderNeedsCorrection` | תזכורת יש להשלים תיקונים |
| `PasswordReset` | קישור לאיפוס סיסמה |
| `TwoFactorCode` | קוד TFA |
| `PasswordExpiryWarning` | אזהרת פג תוקף סיסמה |
| `BatchImportSuccessUploader` | יבוא אקסל הצליח |
| `BatchImportErrors` | יבוא אקסל נכשל (עם `{ErrorList}`) |

**טוקנים זמינים בתבניות:** `{EmployeeName}`, `{EmployeeCode}`, `{MonthName}`, `{ReportDate}`, `{RejectReason}`, `{ResetLink}`, `{TfaCode}`, `{DaysUntilExpiry}`, `{ErrorList}`

---

## 12. בדיקת תקינות לאחר פריסה

בצע את הבדיקות הבאות לאחר כל פריסה (10 דקות):

```
☐ 1. כניסה כ-Admin — המסך נטען ללא שגיאות
☐ 2. /Dashboard — הרשימה מוצגת (אחרי לחיצת "הצג")
☐ 3. /Employees — רשימת עובדים נטענת
☐ 4. /Report — טופס הדיווח נפתח לחודש הפעיל
☐ 5. /Report/UploadExcel — העלאת Excel שגוי → מופיעה רשימת שגיאות + קישור PDF
☐ 6. /Admin/EmailServerSettings → "שלח מייל בדיקה" → ודא קבלה בתיבת הדוא"ל
☐ 7. /Admin/NotificationLogs → השורה החדשה בסטטוס Sent
☐ 8. /Admin/AuditLog → שורות כניסה וכניסה מוצלחת נרשמו
☐ 9. פתח דוח, הוסף שורה, שמור, מחק — ללא שגיאות
☐ 10. יציאה מהמערכת — מנווט לדף הכניסה
```

---

## 13. תמיכה שוטפת

### גיבויים

ראה `docs/OPERATIONS.md` §4 לסקריפט הגיבוי המוכן:
- גיבוי יומי של מסד הנתונים לתיקייה `D:\backups\`
- גיבוי של תיקיית הקבצים (`wwwroot\uploads\`)
- שמור 14 גיבויים יומיים, 12 שבועיים, 12 חודשיים

### ניטור

- **NotificationLogs עם Failed** → בעיית SMTP: `/Admin/NotificationLogs?status=Failed`
- **AuditLog כניסות כושלות** → ניסיון פריצה: `/Admin/AuditLog?action=Auth.LoginFailed`
- **Windows Event Log** → Application log → `IIS AspNetCore Module V2`

### תרחישי חירום נפוצים

| תרחיש | פתרון מהיר |
|-------|----------|
| Admin נעול | UPDATE Users SET StatusId=1, FailedLoginAttempts=0 WHERE IdNumber='ADMIN' |
| TFA חוסם כניסות | UPDATE SystemConstants SET Value='false' WHERE [Key]='TfaEmailEnabled' |
| מייל לא יוצא | בדוק /Admin/EmailServerSettings → שלח בדיקה; ראה /Admin/NotificationLogs |
| רוצה לחזור לגרסה קודמת | עצור App Pool → החלף תיקיית publish → הפעל App Pool |

לתיעוד מלא של כל תרחיש, ניטור, גיבויים, ועדכונים, ראה: **`docs/OPERATIONS.md`**

---

*מדריך זה תקין לגרסה 1.0 של מערכת דיווח עובדים אקסיומא. אקסיומא — 2026.*
