/*
  Axioma Reporting v1.2.7

  No schema change is required for this release.
  The statements below only clean demo English data if it exists.
  Do not run them if these are real customer values.
*/

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
