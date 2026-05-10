using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AxiomaReporting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscussionCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationalPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationalStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailServerSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmtpServer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FromAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FromName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UseSsl = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailServerSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalCode = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalityDistrictNationals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalityDistrictNationals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportingMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    LastReportingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AllowFutureReporting = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportingMonths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConstants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConstants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frameworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionSymbol = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EducationalStageId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frameworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frameworks_EducationalStages_EducationalStageId",
                        column: x => x.EducationalStageId,
                        principalTable: "EducationalStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionSymbol = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LocalityId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    SectorId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    EducationalStageId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Institutions_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Institutions_EducationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EducationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Institutions_EducationalStages_EducationalStageId",
                        column: x => x.EducationalStageId,
                        principalTable: "EducationalStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Institutions_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Institutions_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    IsReportingEmployee = table.Column<bool>(type: "bit", nullable: false),
                    RestDay = table.Column<int>(type: "int", nullable: true),
                    AllowFutureReporting = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    LastPasswordChange = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AcceptedTermsOfUse = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_EmployeeRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EmployeeRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_UserStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "UserStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Allocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    AnnualEmploymentScope = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    MonthlyEmploymentScope = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    DailyEmploymentScope = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    MonthlyRowAllocation = table.Column<int>(type: "int", nullable: true),
                    AnnualRowAllocation = table.Column<int>(type: "int", nullable: true),
                    OutputDuration = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllowExcelUpload = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allocations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Allocations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InspectorAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectorUserId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    SectorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectorAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectorAssignments_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InspectorAssignments_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InspectorAssignments_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InspectorAssignments_Users_InspectorUserId",
                        column: x => x.InspectorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReportingMonthId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RejectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedBy = table.Column<int>(type: "int", nullable: true),
                    ImportedFromExcel = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_ReportStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ReportStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_ReportingMonths_ReportingMonthId",
                        column: x => x.ReportingMonthId,
                        principalTable: "ReportingMonths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_Users_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Users_RejectedBy",
                        column: x => x.RejectedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationClasses",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationClasses", x => new { x.AllocationId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_AllocationClasses_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationClasses_SchoolClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationDiscussionCodes",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    DiscussionCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationDiscussionCodes", x => new { x.AllocationId, x.DiscussionCodeId });
                    table.ForeignKey(
                        name: "FK_AllocationDiscussionCodes_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationDiscussionCodes_DiscussionCodes_DiscussionCodeId",
                        column: x => x.DiscussionCodeId,
                        principalTable: "DiscussionCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationDistricts",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationDistricts", x => new { x.AllocationId, x.DistrictId });
                    table.ForeignKey(
                        name: "FK_AllocationDistricts_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationDistricts_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationDomains",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    DomainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationDomains", x => new { x.AllocationId, x.DomainId });
                    table.ForeignKey(
                        name: "FK_AllocationDomains_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationDomains_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationEducationalPrograms",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    EducationalProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationEducationalPrograms", x => new { x.AllocationId, x.EducationalProgramId });
                    table.ForeignKey(
                        name: "FK_AllocationEducationalPrograms_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationEducationalPrograms_EducationalPrograms_EducationalProgramId",
                        column: x => x.EducationalProgramId,
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationFrameworks",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    FrameworkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationFrameworks", x => new { x.AllocationId, x.FrameworkId });
                    table.ForeignKey(
                        name: "FK_AllocationFrameworks_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationFrameworks_Frameworks_FrameworkId",
                        column: x => x.FrameworkId,
                        principalTable: "Frameworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationGradeLevels",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    GradeLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationGradeLevels", x => new { x.AllocationId, x.GradeLevelId });
                    table.ForeignKey(
                        name: "FK_AllocationGradeLevels_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationGradeLevels_GradeLevels_GradeLevelId",
                        column: x => x.GradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationLocalities",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    LocalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationLocalities", x => new { x.AllocationId, x.LocalityId });
                    table.ForeignKey(
                        name: "FK_AllocationLocalities_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationLocalities_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationLocalityDistrictNationals",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    LocalityDistrictNationalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationLocalityDistrictNationals", x => new { x.AllocationId, x.LocalityDistrictNationalId });
                    table.ForeignKey(
                        name: "FK_AllocationLocalityDistrictNationals_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationLocalityDistrictNationals_LocalityDistrictNationals_LocalityDistrictNationalId",
                        column: x => x.LocalityDistrictNationalId,
                        principalTable: "LocalityDistrictNationals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationPrograms",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationPrograms", x => new { x.AllocationId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_AllocationPrograms_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationPrograms_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationSectors",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationSectors", x => new { x.AllocationId, x.SectorId });
                    table.ForeignKey(
                        name: "FK_AllocationSectors_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationSectors_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocationSubjects",
                columns: table => new
                {
                    AllocationId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationSubjects", x => new { x.AllocationId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_AllocationSubjects_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    AllocationId = table.Column<int>(type: "int", nullable: true),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    MeetingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingDuration = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    LocalityId = table.Column<int>(type: "int", nullable: false),
                    FrameworkId = table.Column<int>(type: "int", nullable: false),
                    EducationalProgramId = table.Column<int>(type: "int", nullable: false),
                    DomainId = table.Column<int>(type: "int", nullable: false),
                    Subject1Id = table.Column<int>(type: "int", nullable: false),
                    Subject2Id = table.Column<int>(type: "int", nullable: true),
                    DiscussionCodeId = table.Column<int>(type: "int", nullable: true),
                    ConclusionClassId = table.Column<int>(type: "int", nullable: true),
                    ConclusionFrameworkId = table.Column<int>(type: "int", nullable: true),
                    ConclusionLocationId = table.Column<int>(type: "int", nullable: true),
                    GradeLevelId = table.Column<int>(type: "int", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportRows_Allocations_AllocationId",
                        column: x => x.AllocationId,
                        principalTable: "Allocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportRows_DiscussionCodes_DiscussionCodeId",
                        column: x => x.DiscussionCodeId,
                        principalTable: "DiscussionCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportRows_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportRows_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportRows_EducationalPrograms_EducationalProgramId",
                        column: x => x.EducationalProgramId,
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportRows_Frameworks_FrameworkId",
                        column: x => x.FrameworkId,
                        principalTable: "Frameworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportRows_GradeLevels_GradeLevelId",
                        column: x => x.GradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportRows_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportRows_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportRows_SchoolClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportRows_SchoolClasses_ConclusionClassId",
                        column: x => x.ConclusionClassId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportRows_Subjects_Subject1Id",
                        column: x => x.Subject1Id,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportRows_Subjects_Subject2Id",
                        column: x => x.Subject2Id,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ReportRowId = table.Column<int>(type: "int", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAttachments_ReportRows_ReportRowId",
                        column: x => x.ReportRowId,
                        principalTable: "ReportRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAttachments_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentAttachments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Body", "CreatedAt", "IsActive", "Subject", "TypeDescription", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} התקבל בהצלחה.\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "דיווח פעילות חודשית התקבל", "ReportReceived", null },
                    { 2, "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} אושר.\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "דיווח פעילות חודשית אושר", "ReportApproved", null },
                    { 3, "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} הוחזר לתיקון.\n\nסיבת ההחזרה: {{RejectionReason}}\n\nנא לתקן ולהגיש מחדש.\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "דיווח פעילות חודשית הוחזר לתיקון", "ReportRejected", null },
                    { 4, "שלום {{EmployeeName}},\n\nנא לשים לב שדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} טרם הוגש.\n\nהמועד האחרון להגשה: {{Deadline}}.\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "תזכורת: דיווח פעילות חודשית טרם הוגש", "ReminderNotSubmitted", null },
                    { 5, "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} הוחזר לתיקון וטרם תוקן.\n\nנא לתקן ולהגיש לפני: {{Deadline}}.\n\nבברכה,\nמערכת אקסיומא", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "תזכורת: דיווח פעילות חודשית ממתין לתיקון", "ReminderNeedsCorrection", null }
                });

            migrationBuilder.InsertData(
                table: "EmployeeRoles",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "מורה", true, null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "מנהל", true, null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "רכז", true, null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "יועץ", true, null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "מפקח", true, null }
                });

            migrationBuilder.InsertData(
                table: "ReportStatuses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "טיוטה - הדוח נוצר אך לא הוגש", "Draft" },
                    { 2, "בהקלדה - הדוח נמצא בתהליך הקלדה", "InEntry" },
                    { 3, "ממתין לאישור - הדוח הוגש וממתין לאישור", "PendingApproval" },
                    { 4, "מאושר - הדוח אושר", "Approved" },
                    { 5, "הוחזר לתיקון - הדוח הוחזר לעובד לתיקון", "ReturnedForCorrection" },
                    { 6, "נעול - הדוח נעול ואינו ניתן לעריכה", "Locked" }
                });

            migrationBuilder.InsertData(
                table: "SystemConstants",
                columns: new[] { "Id", "CreatedAt", "Description", "Key", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "מרווח בין תזכורות בימים", "ReminderIntervalDays", null, null, "3" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "כמה ימים לפני הדדליין מתחילות התזכורות", "ReminderStartDaysBeforeDeadline", null, null, "7" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "סף אחוז דמיון בהערות (Levenshtein normalized)", "NotesSimilarityThresholdPercent", null, null, "90" },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "מקסימום שעות יומי ברירת מחדל לשורת דיווח", "MaxDailyHoursDefault", null, null, "9" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "מנהל מערכת - גישה מלאה לכל הפונקציות", "SystemAdmin" },
                    { 2, "מנהל פרויקט - ניהול עובדים, הקצאות ופתיחת חודשים", "ProjectManager" },
                    { 3, "רכז פרויקט - יצירת עובדים, הקצאות ואישור דיווחים", "ProjectCoordinator" },
                    { 4, "מפקח צפייה - צפייה בלבד בהיקף מוגדר, ייצוא מאושרים", "InspectorView" },
                    { 5, "מפקח אישור - צפייה + אישור/דחיית דיווחים", "InspectorApproval" },
                    { 6, "עובד - צפייה בנתוניו האישיים ומילוי דיווחים", "Employee" }
                });

            migrationBuilder.InsertData(
                table: "UserStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Inactive" },
                    { 3, "Locked" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AcceptedTermsOfUse", "AllowFutureReporting", "CreatedAt", "CreatedBy", "Email", "EmployeeCode", "FailedLoginAttempts", "FirstName", "IdNumber", "IsReportingEmployee", "LastName", "LastPasswordChange", "MustChangePassword", "Notes", "PasswordHash", "Phone", "RestDay", "RoleId", "StatusId", "UpdatedAt", "UpdatedBy", "UserRoleId" },
                values: new object[] { 1, false, false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "ADMIN001", 0, "מנהל", "admin", false, "מערכת", null, true, null, "$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewdGADNUvDdAfY2.", null, null, 1, 1, null, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AllocationClasses_ClassId",
                table: "AllocationClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationDiscussionCodes_DiscussionCodeId",
                table: "AllocationDiscussionCodes",
                column: "DiscussionCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationDistricts_DistrictId",
                table: "AllocationDistricts",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationDomains_DomainId",
                table: "AllocationDomains",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationEducationalPrograms_EducationalProgramId",
                table: "AllocationEducationalPrograms",
                column: "EducationalProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationFrameworks_FrameworkId",
                table: "AllocationFrameworks",
                column: "FrameworkId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationGradeLevels_GradeLevelId",
                table: "AllocationGradeLevels",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationLocalities_LocalityId",
                table: "AllocationLocalities",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationLocalityDistrictNationals_LocalityDistrictNationalId",
                table: "AllocationLocalityDistrictNationals",
                column: "LocalityDistrictNationalId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationPrograms_ProgramId",
                table: "AllocationPrograms",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_ProjectId",
                table: "Allocations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_UserId_ProjectId",
                table: "Allocations",
                columns: new[] { "UserId", "ProjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllocationSectors_SectorId",
                table: "AllocationSectors",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationSubjects_SubjectId",
                table: "AllocationSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAttachments_ReportRowId",
                table: "DocumentAttachments",
                column: "ReportRowId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAttachments_UploadedBy",
                table: "DocumentAttachments",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAttachments_UserId",
                table: "DocumentAttachments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Frameworks_EducationalStageId",
                table: "Frameworks",
                column: "EducationalStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Frameworks_InstitutionSymbol_EducationalStageId",
                table: "Frameworks",
                columns: new[] { "InstitutionSymbol", "EducationalStageId" },
                unique: true,
                filter: "[EducationalStageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InspectorAssignments_DistrictId",
                table: "InspectorAssignments",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectorAssignments_InspectorUserId",
                table: "InspectorAssignments",
                column: "InspectorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectorAssignments_ProgramId",
                table: "InspectorAssignments",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectorAssignments_SectorId",
                table: "InspectorAssignments",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_DistrictId",
                table: "Institutions",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_EducationalStageId",
                table: "Institutions",
                column: "EducationalStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_InstitutionSymbol_EducationalStageId",
                table: "Institutions",
                columns: new[] { "InstitutionSymbol", "EducationalStageId" },
                unique: true,
                filter: "[EducationalStageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_LocalityId",
                table: "Institutions",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_SectorId",
                table: "Institutions",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_TypeId",
                table: "Institutions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordHistories_UserId",
                table: "PasswordHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_AllocationId",
                table: "ReportRows",
                column: "AllocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_ClassId",
                table: "ReportRows",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_ConclusionClassId",
                table: "ReportRows",
                column: "ConclusionClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_DiscussionCodeId",
                table: "ReportRows",
                column: "DiscussionCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_DistrictId",
                table: "ReportRows",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_DomainId",
                table: "ReportRows",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_EducationalProgramId",
                table: "ReportRows",
                column: "EducationalProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_FrameworkId",
                table: "ReportRows",
                column: "FrameworkId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_GradeLevelId",
                table: "ReportRows",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_LocalityId",
                table: "ReportRows",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_ReportId",
                table: "ReportRows",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_Subject1Id",
                table: "ReportRows",
                column: "Subject1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_Subject2Id",
                table: "ReportRows",
                column: "Subject2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ApprovedBy",
                table: "Reports",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_RejectedBy",
                table: "Reports",
                column: "RejectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportingMonthId",
                table: "Reports",
                column: "ReportingMonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_StatusId",
                table: "Reports",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId_ReportingMonthId",
                table: "Reports",
                columns: new[] { "UserId", "ReportingMonthId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemConstants_Key",
                table: "SystemConstants",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedBy",
                table: "Users",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdNumber",
                table: "Users",
                column: "IdNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedBy",
                table: "Users",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocationClasses");

            migrationBuilder.DropTable(
                name: "AllocationDiscussionCodes");

            migrationBuilder.DropTable(
                name: "AllocationDistricts");

            migrationBuilder.DropTable(
                name: "AllocationDomains");

            migrationBuilder.DropTable(
                name: "AllocationEducationalPrograms");

            migrationBuilder.DropTable(
                name: "AllocationFrameworks");

            migrationBuilder.DropTable(
                name: "AllocationGradeLevels");

            migrationBuilder.DropTable(
                name: "AllocationLocalities");

            migrationBuilder.DropTable(
                name: "AllocationLocalityDistrictNationals");

            migrationBuilder.DropTable(
                name: "AllocationPrograms");

            migrationBuilder.DropTable(
                name: "AllocationSectors");

            migrationBuilder.DropTable(
                name: "AllocationSubjects");

            migrationBuilder.DropTable(
                name: "Authorities");

            migrationBuilder.DropTable(
                name: "DocumentAttachments");

            migrationBuilder.DropTable(
                name: "EmailServerSettings");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "InspectorAssignments");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "PasswordHistories");

            migrationBuilder.DropTable(
                name: "SystemConstants");

            migrationBuilder.DropTable(
                name: "LocalityDistrictNationals");

            migrationBuilder.DropTable(
                name: "ReportRows");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "EducationTypes");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Allocations");

            migrationBuilder.DropTable(
                name: "DiscussionCodes");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "EducationalPrograms");

            migrationBuilder.DropTable(
                name: "Frameworks");

            migrationBuilder.DropTable(
                name: "GradeLevels");

            migrationBuilder.DropTable(
                name: "Localities");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SchoolClasses");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "EducationalStages");

            migrationBuilder.DropTable(
                name: "ReportStatuses");

            migrationBuilder.DropTable(
                name: "ReportingMonths");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserStatuses");
        }
    }
}
