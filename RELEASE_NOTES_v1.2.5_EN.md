# Axioma Reporting - Release Notes v1.2.5

Prepared on: 2026-05-10

## Deployment Package

File:

`C:\axioma-reporting\AxiomaReporting-Delivery-v1.2.5.zip`

SHA256:

`35DF810723DDD326287C348C5A4251741C6A646A16203AAC779B7BDD73E6F41D`

## Fixes Included

- On the reports dashboard, the row edit button is now available only to `System Admin` users.
- Opening a report for editing from the reports dashboard now preserves filters, page, and sorting. The `Back to Reports Dashboard` button returns to the same dashboard state.
- The `ReturnedForCorrection` status is displayed in Hebrew as `הוחזר לתיקון`.
- The reports summary/approval screen no longer resets the selected status to a default status when “all statuses” is selected.
- Inline row editing on the report page now opens a detail row underneath the selected row, instead of squeezing all editable fields into the same table row.
- Document upload is now report-level, not per report row.
- Report document uploads are limited to PDF, Word, and Excel files.
- Excel import errors are displayed on screen, with an option to download a PDF error report.
- Password reuse/similarity errors are now shown clearly in Hebrew when the new password is identical or too similar to one of the last 5 passwords.
- Generic model-binding validation messages that previously appeared in English were replaced with Hebrew messages.
- Filter clear buttons were standardized to the text `נקה`.
- Several report-page messages and labels that appeared as garbled text were corrected.

## Validation Performed Before Packaging

- `dotnet build AxiomaReporting.sln` completed successfully.
- Focused tests passed:
  - `DashboardFilterServiceTests`
  - `ReportFlowTests`
- Manual HTTP load checks passed:
  - `/Dashboard`
  - `/Dashboard/Summary`
  - `/Report?userId=7&allocationId=1`
  - `/Employee/Edit/7`

## Server Installation Steps

### 1. Back Up Before Deployment

Before replacing the application:

- Take a full database backup.
- Back up the current application folder.
- Keep a copy of the server-specific `appsettings.Production.json` or any other environment-specific configuration file.

### 2. Stop the Application

Stop the site in IIS, or stop the service/process that runs the application.

### 3. Deploy Application Files

Extract:

`AxiomaReporting-Delivery-v1.2.5.zip`

Then replace the server application files with the contents of:

`app`

Important: do not overwrite the server-specific production configuration without comparing it to:

`config\appsettings.Production.template.json`

### 4. Database Update

Version `v1.2.5` does not introduce a new database schema change.

If the server already includes the `v1.2.3` database changes, no additional SQL script is required.

If the server is older than `v1.2.3`, run this first:

`database\scripts\upgrade-v1.2.3.sql`

After that, you may run:

`database\scripts\upgrade-v1.2.5.sql`

The `upgrade-v1.2.5.sql` file is informational only and states that no additional schema changes are required.

### 5. Folder Permissions

Make sure the application identity has write permissions to the upload folders, especially:

- `wwwroot\uploads`
- `wwwroot\uploads\attachments`
- `wwwroot\uploads\excel-errors`

### 6. Start the Application

Start the site again in IIS, or restart the relevant service/process.

### 7. Post-Deployment Checks

After startup:

- Log in as `System Admin`.
- Open `/Dashboard` and confirm the table loads.
- Open a report row using the `Edit` button and confirm the `Back to Reports Dashboard` button preserves the dashboard filters.
- Open `/Dashboard/Summary` and confirm report statuses are displayed in Hebrew.
- Open `/Report?userId=7&allocationId=1`, or another existing report on the server, and test row editing using the inline/detail-row edit option.
- Test uploading a PDF, Word, or Excel document to a report.
- Test an invalid Excel import and confirm errors appear on screen and the PDF error report can be downloaded.

## Notes

- There are known non-blocking build warnings:
  - The project targets `.NET 6`, which is out of support.
  - The current `MailKit` package version has a known security advisory.
- A future upgrade to a supported .NET target and updated NuGet packages is recommended.
