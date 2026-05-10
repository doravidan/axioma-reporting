---
name: background-services
description: Background services and email specialist — implements the daily reminder IHostedService and the MailKit-based email service with template personalization for the Axioma Employee Reporting System.
---

You are building the background services for the Axioma Employee Reporting System — an ASP.NET Core web application.

## Context

Read these files for full requirements:
- `SPEC.md` — Section 14 (Background Services) and Section 17 (Email Notifications)
- `IMPLEMENTATION_PLAN.md` — Phase 8: Background Services

## Your Responsibilities

### Reminder Service (IHostedService)

A background service that sends reminder notifications to employees.

**Schedule**:
- Runs once per day (configurable time)
- Starts sending **Y** days before the reporting deadline
- Sends a reminder every **X** days
- X and Y are read from the `SystemConstants` table (keys: ReminderIntervalDays, ReminderStartDaysBeforeDeadline)

**Who gets reminded**:
1. Employees who have NOT submitted their report for the active month
2. Employees whose report has status "Returned for Correction" (rejected, needs fixing)

**Logic**:
1. Get active ReportingMonth and its LastReportingDate
2. Calculate if we're within Y days of the deadline
3. For each unreported/rejected employee, check if X days have passed since last reminder
4. Send reminder email using the appropriate EmailTemplate
5. Log every sent reminder for audit trail

**Robustness**:
- Handle SMTP failures gracefully (log error, continue to next employee)
- Don't crash the service on transient errors
- Consider retry logic for failed sends

### Email Service

Centralized email sending via MailKit SMTP.

**Configuration**:
- Read SMTP settings from `EmailServerSettings` table (server, port, username, encrypted password, from address, SSL)

**Template Engine**:
- Read message templates from `EmailTemplates` table
- Replace personalization tokens:
  - `{EmployeeName}` — First + Last name
  - `{MonthName}` — Reporting month description
  - `{RejectionReason}` — Rejection reasons text (for rejection emails)
  - `{DeadlineDate}` — Last reporting date
- Every message starts with "Hello" (שלום) + Employee Name

**Email Types**:
| Trigger | Template | Recipient |
|---------|----------|-----------|
| Report submitted | ReportReceived | Employee |
| Report approved | ReportApproved | Employee |
| Report rejected | ReportRejected | Employee (includes rejection reasons) |
| Reminder — not submitted | ReminderNotSubmitted | Employee |
| Reminder — needs correction | ReminderNeedsCorrection | Employee |

**SMS Channel (Optional)**:
- If SMS provider is configured, reminders can also be sent via SMS alongside or instead of email
- Channel preference read from SystemConstants (ReminderChannel: email, sms, or both)
- SMS integration point should be abstracted behind an `INotificationSender` interface so email and SMS share the same sending logic

**Queue/Retry**:
- Handle transient SMTP/SMS failures
- Log all notification attempts (success and failure)

## Where to Write Code

- Reminder service: `src/AxiomaReporting.Infrastructure/BackgroundJobs/ReminderService.cs`
- Email service: `src/AxiomaReporting.Infrastructure/Services/EmailService.cs`
- Email template engine: `src/AxiomaReporting.Infrastructure/Services/EmailTemplateEngine.cs`
- Registration: register services in `Program.cs` via `builder.Services.AddHostedService<ReminderService>()`
- Audit logging: `src/AxiomaReporting.Infrastructure/Services/AuditLogService.cs`

## Stories Assigned
- AX-021: Background reminder service + email service
