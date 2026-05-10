using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTermsAuditNotificationLogsAndConcurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Users",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Reports",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ReportRows",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Allocations",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ActorUserId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Before = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    After = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_ActorUserId",
                        column: x => x.ActorUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TemplateType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecipientUserId = table.Column<int>(type: "int", nullable: true),
                    RecipientEmail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RelatedReportId = table.Column<int>(type: "int", nullable: true),
                    RelatedReportingMonthId = table.Column<int>(type: "int", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    AttemptCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LastAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextRetryAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailureReason = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationLogs_ReportingMonths_RelatedReportingMonthId",
                        column: x => x.RelatedReportingMonthId,
                        principalTable: "ReportingMonths",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationLogs_Reports_RelatedReportId",
                        column: x => x.RelatedReportId,
                        principalTable: "Reports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationLogs_Users_RecipientUserId",
                        column: x => x.RecipientUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TermsOfUseVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    BodyHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedByUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermsOfUseVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TermsOfUseVersions_Users_PublishedByUserId",
                        column: x => x.PublishedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TermsOfUseAcceptances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermsOfUseAcceptances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TermsOfUseAcceptances_TermsOfUseVersions_VersionId",
                        column: x => x.VersionId,
                        principalTable: "TermsOfUseVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TermsOfUseAcceptances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TermsOfUseVersions",
                columns: new[] { "Id", "BodyHtml", "CreatedAt", "EffectiveFrom", "PublishedByUserId", "UpdatedAt", "VersionNumber" },
                values: new object[] { 1, "תנאי שימוש — יסופקו על ידי הלקוח", new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, 1 });

            migrationBuilder.InsertData(
                table: "TermsOfUseAcceptances",
                columns: new[] { "Id", "AcceptedAt", "IpAddress", "UserId", "VersionId" },
                values: new object[] { 1, new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Action_Timestamp",
                table: "AuditLogs",
                columns: new[] { "Action", "Timestamp" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ActorUserId_Timestamp",
                table: "AuditLogs",
                columns: new[] { "ActorUserId", "Timestamp" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityType_EntityId",
                table: "AuditLogs",
                columns: new[] { "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Timestamp",
                table: "AuditLogs",
                column: "Timestamp",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_RecipientUserId_CreatedAt",
                table: "NotificationLogs",
                columns: new[] { "RecipientUserId", "CreatedAt" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_RelatedReportId",
                table: "NotificationLogs",
                column: "RelatedReportId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_RelatedReportingMonthId",
                table: "NotificationLogs",
                column: "RelatedReportingMonthId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_Status_NextRetryAt",
                table: "NotificationLogs",
                columns: new[] { "Status", "NextRetryAt" });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLogs_TemplateType_CreatedAt",
                table: "NotificationLogs",
                columns: new[] { "TemplateType", "CreatedAt" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_TermsOfUseAcceptances_UserId_VersionId",
                table: "TermsOfUseAcceptances",
                columns: new[] { "UserId", "VersionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TermsOfUseAcceptances_VersionId",
                table: "TermsOfUseAcceptances",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_TermsOfUseVersion_VersionNumber",
                table: "TermsOfUseVersions",
                column: "VersionNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TermsOfUseVersions_PublishedByUserId",
                table: "TermsOfUseVersions",
                column: "PublishedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "NotificationLogs");

            migrationBuilder.DropTable(
                name: "TermsOfUseAcceptances");

            migrationBuilder.DropTable(
                name: "TermsOfUseVersions");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ReportRows");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Allocations");
        }
    }
}
