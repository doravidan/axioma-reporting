# Implementation Status

Updated: 2026-05-18

## Implemented

- Section 1: branding cleanup applied.
  - Updated deployed SVG logo, home heading, email server placeholder, and seeded email template bodies to `סייט אנד סאונד`.
  - Active source/static sweep no longer finds user-facing `אקסיומא` text in the checked paths.

- Section 2: archive/hide stale reports implemented.
  - Added `Reports.IsArchived bit NOT NULL DEFAULT(0)`.
  - Backfilled inactive-month, non-completed reports as archived.
  - Dashboard/report queries hide archived reports by default.
  - `includeArchived=true` works for roles 1-3 and the dashboard now has a visible include-archived checkbox.
  - Archived report direct URLs are blocked for employee-level users, and archived reports cannot be edited.

- Sections 3 and 5: project/program lookup scoping implemented.
  - Added and backfilled `ProjectProgramSubjects`, `ProjectProgramDomains`, `ProjectProgramFrameworks`, `ProjectProgramEducationalPrograms`, `ProjectProgramDiscussionCodes`, `ProjectProgramGradeLevels`, and `ProjectProgramClasses`.
  - Added `GET /allocations/ScopedLookups?projectId={id}&programIds={id}`.
  - Allocation create/edit validates scoped lookup IDs server-side.
  - Allocation create/edit renders scoped dropdowns and now reloads downstream scoped multi-selects in the browser when project/program selections change.
  - Reused the existing `Admin/ProjectPrograms` page and added per project-program scope editors plus `POST /Admin/ProjectPrograms/SaveScope`.
  - Added `GET /Report/ScopedForProgram?allocationId={id}&programId={id}` for report-row scoped intersections.

- Section 4: employee personal Excel export added.
  - New endpoint: `GET /Report/ExportMine?allocationId={id}&reportingMonthId={id}`.
  - Enforces current-user ownership of the allocation.
  - Exports employee/report metadata and rows to RTL XLSX.
  - Records `Report.ExportMine` audit entry.

- Section 6: approve/reject hardening implemented.
  - Status changes are committed before email sending.
  - Approval/rejection email failures are logged with `EMAIL:` and do not surface to the approver.
  - Approve/reject short-circuit already-target-state submissions.
  - Optional row-version handling catches stale writes and returns a friendly message.
  - Approve/reject posts now support JSON responses for browser `fetch`.
  - Dashboard summary approve/reject buttons now submit asynchronously, disable during the request, update the row, and show a page message without a full reload.

- Section 7: searchable/type-ahead multi-selects verified.
  - Existing global Choices.js setup applies to all `select[multiple]` controls in shared layouts.

- Sections 8 and 9: password reset/change bugs fixed.
  - Successful reset clears `MustChangePassword`.
  - Reset records `Auth.PasswordReset`.
  - Regular `ChangePassword` blocks using the same current password.
  - Password-history blocking remains in place.

## Verified

- `dotnet build _decompiled_infra_v1_2_8_current/AxiomaReporting.Infrastructure.csproj -c Release -v:minimal` completed with warnings only.
- `dotnet build _decompiled_v1_2_8_current/AxiomaReporting.Web.csproj -c Release -v:minimal` completed with warnings only after the final allocation-form scoped lookup change.
- Deployed rebuilt `AxiomaReporting.Infrastructure.dll`, `AxiomaReporting.Web.dll`, and matching `.pdb` files.
- `AxiomaReporting` and `Bigabay` app pools are started.
- Live checks on `https://postybell.co.il` passed:
  - `/Account/Login` returns 200.
  - Admin dashboard is authenticated and renders the include-archived UI.
  - `/Dashboard?IncludeArchived=true` returns 200 and the checkbox is checked.
  - `/allocations/ScopedLookups?projectId=1&programIds=1` returns JSON.
  - `/Employee/15/Allocations/14/Edit` renders the scoped lookup browser script.
  - `/Admin/ProjectPrograms` returns 200 and renders `SaveScope`.
  - `/` renders `סייט אנד סאונד` and not `אקסיומא`.
  - `/images/site-logo.svg` serves UTF-8 SVG containing `סייט אנד סאונד`.
  - `/Report?userId=15&allocationId=14&reportId=12` returns 200.
  - `/Report/ExportMine?allocationId=14&reportingMonthId=4` returns XLSX.
  - `/Report/ScopedForProgram?allocationId=14&programId=1` returns JSON.
  - `/Report/ScopedForProgram?allocationId=14&programId=3` returns JSON through the educational-program fallback path.
  - `ChangePassword` posting the same current password stays on the form and is blocked.
- Remaining UI completion checks:
  - `/js/site.js` returns 200 and contains the approve/reject async handler.
  - `/js/site.js` returns 200 and contains the report-row scoped lookup handler.
  - `/Dashboard/Summary` no longer contains inline duplicate action JS; the behavior is loaded once from `site.js`.
- Database verification:
  - Project/program scope tables are populated.
  - `Reports.IsArchived` counts verified after backfill: 6 active/unarchived reports, 18 archived reports.

## Notes

- The app is currently maintained from decompiled source folders in this deployment, not the original `src/` layout referenced by the plan.
- Dependency manifest files were intentionally not redeployed.
- Latest backups:
  - `C:\webprojects\Exioma\_backup-plan2-20260518-160528`
  - `C:\webprojects\Exioma\_backup-plan2-web-20260518-161245`
  - `C:\webprojects\Exioma\_backup-remaining-ui-20260518-162216`
