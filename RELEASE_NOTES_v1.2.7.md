# Axioma Reporting - הערות גרסה v1.2.7

תאריך הכנה: 12/05/2026

## חבילת פריסה

קובץ:

`C:\axioma-reporting\AxiomaReporting-Delivery-v1.2.7.zip`

SHA256:

`612FE9237AEA4FF5411C7759A0198EC73942399BB51F9B951035524AC695A007`

## מטרת הגרסה

גרסה זו מטפלת בנושא העברית במערכת: הסרת ג׳יבריש, הסרת טקסטים באנגלית ממסכים גלויים, ותיקון תצוגה עברית מלאה במסכים המרכזיים ובמסכי הניהול.

## מה תוקן

### עברית וג׳יבריש

- תוקנו מחרוזות ג׳יבריש בקוד ובמסכים גלויים.
- תוקן דף `Dashboard` כך שכל הכותרות, הפילטרים, עמודות הטבלה, הודעות ריקות וכפתורים מוצגים בעברית תקינה.
- תוקנו הודעות מערכת מתוך Controllers, כולל הודעות הצלחה, שגיאות ולוגיקת סיסמאות.
- הוחלף דף השגיאה הכללי של ASP.NET מדף באנגלית לדף שגיאה בעברית.
- הוחלף דף Privacy הישן מטקסט ברירת מחדל באנגלית לטקסט עברי.
- תוקנו טקסטים טכניים שנראו למשתמשים במסכי ניהול:
  - סוגי התראות
  - סטטוסי התראות
  - שמות תבניות מייל
  - פעולות ביומן ביקורת
  - סוגי ישויות ביומן ביקורת
- תוקן טקסט עזרה לבחירה מרובה שהציג `Ctrl / Windows / Mac`.
- תוקנה תצוגת `Unlimited` בטבלאות הקצאות כך שמוצג `ללא הגבלה`.
- במסכי עובדים/ביקורת הוסתרו מזהים טכניים באנגלית שנחשפו למשתמש, למשל `admin`, והוחלפו בטקסט עברי מתאים.

### נתוני דמו מקומיים

בסביבת הבדיקה המקומית עודכנו נתוני דמו שהופיעו באנגלית:

- `Demo Employee` הוחלף לשם עובד בעברית.
- `Demo Project` הוחלף לשם פרויקט בעברית.
- `May 2026` הוחלף ל־`מאי 2026`.
- הערות הקצאת דמו באנגלית הוחלפו בהערות בעברית.

אם בשרת קיימים אותם נתוני דמו באנגלית, יש להריץ את ה־SQL שמופיע בהמשך תחת "עדכון נתוני דמו בשרת".

## בדיקות שבוצעו

- בוצע build:

```powershell
$env:DOTNET_ROLL_FORWARD='Major'
dotnet build AxiomaReporting.sln --no-restore
```

תוצאה: build עבר בהצלחה.

אזהרות ידועות שלא חוסמות פריסה:

- הפרויקט עדיין מכוון ל־`.NET 6`, שאינו נתמך רשמית.
- קיימת אזהרת אבטחה ידועה על חבילת `MailKit`.

- בוצעה סריקת קוד למציאת תווי ג׳יבריש/קידוד שבור בקוד האפליקציה:

תוצאה: `0` ממצאים בקבצי האפליקציה.

- בוצעה הרצה מקומית וטעינת מסכים מרכזיים כ־System Admin:

  - `/`
  - `/Dashboard`
  - `/Dashboard/Summary`
  - `/Employee`
  - `/Employee/Edit/7`
  - `/allocations`
  - `/Employee/7/Allocations`
  - `/Employee/7/Allocations/1/Edit`
  - `/Report?allocationId=1`
  - `/Lookup`
  - `/Admin/ReportingMonths`
  - `/Admin/DataMigration`
  - `/Admin/EmailTemplates`
  - `/Admin/EmailServerSettings`
  - `/Admin/NotificationLogs`
  - `/Admin/AuditLog`

תוצאה: כל המסכים נטענו עם HTTP 200.

- בוצעה סריקת HTML מרונדר למסכים אלה:

תוצאה: לא נמצאו טקסטי ג׳יבריש או אנגלית גלויה, למעט מונחים טכניים מותרים כגון `PDF`, `Excel`, `SMTP`, `CSV`.

## מה צריך להריץ בשרת כדי להתקין

### 1. גיבוי לפני התקנה

לפני החלפת גרסה:

- לבצע גיבוי מלא למסד הנתונים.
- לבצע גיבוי לתיקיית האתר הקיימת.
- לשמור בצד את קבצי ההגדרות של השרת, במיוחד:

```text
appsettings.Production.json
web.config
```

אם קיימים קבצי הגדרות נוספים ל־connection string, SMTP או נתיבי קבצים, יש לגבות גם אותם.

### 2. עצירת האתר

אם האתר רץ ב־IIS:

1. לפתוח IIS Manager.
2. לעצור את ה־Application Pool של האתר.
3. לעצור את האתר עצמו אם נדרש.

אפשר גם לעצור דרך PowerShell, עם שמות האתר וה־App Pool האמיתיים:

```powershell
Import-Module WebAdministration
Stop-WebAppPool -Name "AxiomaReporting"
Stop-Website -Name "AxiomaReporting"
```

### 3. פרסום/הכנת קבצי האפליקציה

אם מפרסמים מהקוד על מכונת build:

```powershell
cd C:\axioma-reporting
$env:DOTNET_ROLL_FORWARD='Major'
dotnet publish .\src\AxiomaReporting.Web\AxiomaReporting.Web.csproj -c Release -o .\publish\v1.2.7
```

לאחר מכן יש להעתיק את תוכן:

```text
C:\axioma-reporting\publish\v1.2.7
```

לתיקיית האתר בשרת.

חשוב:

- לא לדרוס את `appsettings.Production.json` של השרת בלי להשוות קודם.
- לא למחוק את תיקיות ההעלאה הקיימות.
- אם מחליפים תיקייה שלמה, לשמור ולהחזיר את:

```text
wwwroot\uploads
wwwroot\uploads\attachments
wwwroot\uploads\excel-errors
```

### 4. עדכון מסד נתונים

בגרסה זו אין שינוי סכימה חובה למסד הנתונים.

כלומר, אם השרת כבר כולל את שינויי הסכימה של הגרסאות הקודמות, אין צורך להריץ migration חדש עבור v1.2.7.

### 5. עדכון נתוני דמו בשרת, אם קיימים

אם בשרת קיימים נתוני דמו באנגלית כמו `Demo Employee`, `Demo Project`, או `May 2026`, מומלץ להריץ את הסקריפט הבא מול בסיס הנתונים:

```sql
UPDATE Users
SET FirstName = N'עובד',
    LastName = N'הדגמה'
WHERE FirstName = N'Demo'
  AND LastName = N'Employee';

UPDATE Projects
SET Description = N'פרויקט הדגמה'
WHERE Description = N'Demo Project';

UPDATE ReportingMonths
SET Description = N'מאי 2026'
WHERE Description = N'May 2026';

UPDATE Allocations
SET Notes = N'הקצאת הדגמה לבדיקת מסכי הקצאות, סינונים, דיווחים, התראות והעלאת קבצי אקסל'
WHERE Notes LIKE N'%Demo allocation%';
```

אם אלו אינם נתוני דמו אלא נתונים אמיתיים של לקוח, לא להריץ את הסקריפט בלי אישור.

### 6. הרשאות תיקיות

לוודא שלמשתמש שמריץ את האתר יש הרשאות כתיבה לתיקיות:

```text
wwwroot\uploads
wwwroot\uploads\attachments
wwwroot\uploads\excel-errors
```

### 7. הפעלת האתר

אם האתר רץ ב־IIS:

```powershell
Import-Module WebAdministration
Start-WebAppPool -Name "AxiomaReporting"
Start-Website -Name "AxiomaReporting"
```

לאחר מכן לבדוק שאין שגיאות ב־Event Viewer או בלוגים של האפליקציה.

## בדיקות אחרי התקנה בשרת

לאחר שהאתר עולה:

1. להתחבר כ־System Admin.
2. לפתוח `/Dashboard` ולוודא שכל הטקסט בעברית תקינה.
3. לפתוח `/Employee` ולוודא שאין שמות דמו באנגלית או ג׳יבריש.
4. לפתוח `/allocations` ולוודא ש־`Unlimited` מופיע כ־`ללא הגבלה`.
5. לפתוח `/Admin/EmailTemplates` ולוודא ששמות התבניות בעברית.
6. לפתוח `/Admin/NotificationLogs` ולוודא שסוגים וסטטוסים בעברית.
7. לפתוח `/Admin/AuditLog` ולוודא שפעולות וישויות מוצגות בעברית.
8. לפתוח דף שגיאה יזום או URL לא קיים ולוודא שדף השגיאה בעברית.

## הערות

- השינוי אינו משנה הרשאות, מודל נתונים או לוגיקת דיווחים.
- עיקר השינוי הוא ניקוי תצוגה, קידוד וטקסטים גלויים למשתמש.
- מומלץ בהמשך לבצע מעבר מלא מ־`.NET 6` לגרסת .NET נתמכת ולעדכן חבילות NuGet עם אזהרות אבטחה.
