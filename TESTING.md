# Testing

Run the in-process automated suite:

```powershell
dotnet test AxiomaReporting.sln --no-build -m:1 --filter "FullyQualifiedName!~UI.Playwright"
```

Current result: **240 passing / 0 failing**.

Run the Playwright browser suite separately after starting the web app at `https://localhost:7021`:

```powershell
dotnet run --project src\AxiomaReporting.Web\AxiomaReporting.Web.csproj --launch-profile https
dotnet test AxiomaReporting.sln --no-build -m:1 --filter "FullyQualifiedName~UI.Playwright"
```

Run the unit-test coverage gate:

```powershell
.\scripts\test-unit-coverage.ps1
```

The unit coverage gate currently enforces **80% line coverage** on the service layer covered by unit tests. It excludes SMTP delivery and HTTP current-user plumbing because those are integration/environment concerns rather than deterministic unit-test targets.

The suite is organized into these layers:

| Folder | Purpose |
|--------|---------|
| `src/AxiomaReporting.Tests/Unit` | Fast service-level tests for password policy, authentication, PDF generation, and report validation |
| `src/AxiomaReporting.Tests/Integration` | Full MVC test-host flows with an isolated in-memory database and fake email sender |
| `src/AxiomaReporting.Tests/Ui` | Rendered HTML UI smoke tests for RTL login, forgot password, and anonymous redirect behavior |
| `src/AxiomaReporting.Tests/Ui/Playwright` | Browser tests against `https://localhost:7021`; requires the app to be running before the test command |
| `src/AxiomaReporting.Tests/Stress` | Concurrent HTTP request checks against the ASP.NET test host |

## Test Infrastructure

- `CustomWebApplicationFactory` boots the real MVC app with:
  - isolated EF Core in-memory database per test factory
  - background reminder service disabled
  - fake email sender for asserting password reset and TFA email behavior
- `TestData` seeds roles, statuses, system constants, and users used by the integration and UI tests.
- `HtmlForm` extracts antiforgery tokens from rendered forms so POST tests exercise the real MVC pipeline.

## Current Coverage Highlights

- Password hashing, strength rules, expiry rules.
- Login success, failed-login lockout, password history update.
- Generic lookup CRUD behavior.
- Employee CRUD, reset password, allocation create/update/delete, and allocation junction sync.
- Dashboard report filtering, missing-report logic including Draft/In Entry, inspector scope, sorting, and lookup filtering.
- Report status draft/submit/approve/reject transitions and email notification triggers.
- Excel import validation, blocked statuses, row replacement, imported-report status update, and import-success email notification.
- Allocation output-duration validation and unlimited daily scope behavior.
- Forgot-password token creation and reset email sending.
- Email-only TFA code creation and TFA email sending.
- Login page RTL/form rendering.
- Forgot-password form rendering.
- Anonymous protected-page redirect.
- Public page concurrent request health.
