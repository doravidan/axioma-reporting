# Axioma Reporting v1.2.11 - Production Redirect Fixes

## Fixed
- Fixed production redirects after Excel upload when the app is hosted under a virtual path or reverse proxy path.
- Fixed approve and reject buttons on the reports summary dashboard so they preserve the real browser URL and return to the existing dashboard page after the action.
- Fixed the row approve, reject modal, and bulk approve forms so their form action is adjusted in the browser when the app is served under a visible base path.

## Verification
- Focused tests passed:
  - `ReportControllerGapsTests.UploadExcel`
  - `ReportFlowTests.Coordinator_CanApproveReport`
  - `ReportFlowTests.Coordinator_CanRejectReport`
- Full automated test suite passed:
  - `488 passed, 0 failed, 0 skipped`
- Release publish completed successfully.

## Database
- No database schema change.
- No migration is required for this version.

## Server Install Steps
1. Stop the IIS site or the `AxiomaReporting` Application Pool.
2. Back up the current application folder and database.
3. Extract `AxiomaReporting-Delivery-v1.2.11.zip`.
4. Deploy the contents of `delivery-staging-v1.2.11/publish` to the production application folder.
5. Keep the existing production `appsettings.json` / connection string if it differs from the package.
6. Start the IIS site or Application Pool.
7. Verify in production:
   - Upload a valid Excel file and confirm it returns to the report page.
   - Upload an invalid Excel file and confirm it returns to the report page with the error message and PDF error link.
   - Approve a report from the dashboard and confirm it returns to the dashboard page.
   - Reject a report from the dashboard and confirm it returns to the dashboard page.
