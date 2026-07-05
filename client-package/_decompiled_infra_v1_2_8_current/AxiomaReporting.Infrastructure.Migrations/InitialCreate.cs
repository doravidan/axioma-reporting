using System;
using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace AxiomaReporting.Infrastructure.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260412124943_InitialCreate")]
public class InitialCreate : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable("Authorities", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id31 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt17 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt17 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength29 = 500;
			return new
			{
				Id = id31,
				CreatedAt = createdAt17,
				UpdatedAt = updatedAt17,
				Description = table.Column<string>("nvarchar(500)", null, maxLength29),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Authorities", x => x.Id);
		});
		migrationBuilder.CreateTable("DiscussionCodes", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id30 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt16 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt16 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength28 = 500;
			return new
			{
				Id = id30,
				CreatedAt = createdAt16,
				UpdatedAt = updatedAt16,
				Description = table.Column<string>("nvarchar(500)", null, maxLength28),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_DiscussionCodes", x => x.Id);
		});
		migrationBuilder.CreateTable("Districts", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id29 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt15 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt15 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength27 = 500;
			return new
			{
				Id = id29,
				CreatedAt = createdAt15,
				UpdatedAt = updatedAt15,
				Description = table.Column<string>("nvarchar(500)", null, maxLength27),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Districts", x => x.Id);
		});
		migrationBuilder.CreateTable("Domains", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id28 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt14 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt14 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength26 = 500;
			return new
			{
				Id = id28,
				CreatedAt = createdAt14,
				UpdatedAt = updatedAt14,
				Description = table.Column<string>("nvarchar(500)", null, maxLength26),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Domains", x => x.Id);
		});
		migrationBuilder.CreateTable("EducationalPrograms", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id27 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt13 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt13 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength25 = 500;
			return new
			{
				Id = id27,
				CreatedAt = createdAt13,
				UpdatedAt = updatedAt13,
				Description = table.Column<string>("nvarchar(500)", null, maxLength25),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_EducationalPrograms", x => x.Id);
		});
		migrationBuilder.CreateTable("EducationalStages", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id26 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt12 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt12 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength24 = 500;
			return new
			{
				Id = id26,
				CreatedAt = createdAt12,
				UpdatedAt = updatedAt12,
				Description = table.Column<string>("nvarchar(500)", null, maxLength24),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_EducationalStages", x => x.Id);
		});
		migrationBuilder.CreateTable("EducationTypes", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id25 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt11 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt11 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength23 = 500;
			return new
			{
				Id = id25,
				CreatedAt = createdAt11,
				UpdatedAt = updatedAt11,
				Description = table.Column<string>("nvarchar(500)", null, maxLength23),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_EducationTypes", x => x.Id);
		});
		migrationBuilder.CreateTable("EmailServerSettings", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id24 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			int? maxLength22 = 500;
			OperationBuilder<AddColumnOperation> smtpServer = table.Column<string>("nvarchar(500)", null, maxLength22);
			OperationBuilder<AddColumnOperation> port = table.Column<int>("int");
			maxLength22 = 500;
			OperationBuilder<AddColumnOperation> username = table.Column<string>("nvarchar(500)", null, maxLength22);
			maxLength22 = 500;
			OperationBuilder<AddColumnOperation> password = table.Column<string>("nvarchar(500)", null, maxLength22);
			maxLength22 = 500;
			OperationBuilder<AddColumnOperation> fromAddress = table.Column<string>("nvarchar(500)", null, maxLength22);
			maxLength22 = 500;
			return new
			{
				Id = id24,
				SmtpServer = smtpServer,
				Port = port,
				Username = username,
				Password = password,
				FromAddress = fromAddress,
				FromName = table.Column<string>("nvarchar(500)", null, maxLength22, rowVersion: false, null, nullable: true),
				UseSsl = table.Column<bool>("bit"),
				CreatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()"),
				UpdatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_EmailServerSettings", x => x.Id);
		});
		migrationBuilder.CreateTable("EmailTemplates", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id23 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			int? maxLength21 = 200;
			OperationBuilder<AddColumnOperation> typeDescription = table.Column<string>("nvarchar(200)", null, maxLength21);
			maxLength21 = 500;
			return new
			{
				Id = id23,
				TypeDescription = typeDescription,
				Subject = table.Column<string>("nvarchar(500)", null, maxLength21),
				Body = table.Column<string>("nvarchar(max)"),
				IsActive = table.Column<bool>("bit"),
				CreatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()"),
				UpdatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_EmailTemplates", x => x.Id);
		});
		migrationBuilder.CreateTable("EmployeeRoles", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id22 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt10 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt10 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength20 = 500;
			return new
			{
				Id = id22,
				CreatedAt = createdAt10,
				UpdatedAt = updatedAt10,
				Description = table.Column<string>("nvarchar(500)", null, maxLength20),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_EmployeeRoles", x => x.Id);
		});
		migrationBuilder.CreateTable("GradeLevels", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id21 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt9 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt9 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength19 = 500;
			return new
			{
				Id = id21,
				CreatedAt = createdAt9,
				UpdatedAt = updatedAt9,
				Description = table.Column<string>("nvarchar(500)", null, maxLength19),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_GradeLevels", x => x.Id);
		});
		migrationBuilder.CreateTable("Localities", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id20 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> nationalCode = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> createdAt8 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt8 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength18 = 500;
			return new
			{
				Id = id20,
				NationalCode = nationalCode,
				CreatedAt = createdAt8,
				UpdatedAt = updatedAt8,
				Description = table.Column<string>("nvarchar(500)", null, maxLength18),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Localities", x => x.Id);
		});
		migrationBuilder.CreateTable("LocalityDistrictNationals", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id19 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt7 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt7 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength17 = 500;
			return new
			{
				Id = id19,
				CreatedAt = createdAt7,
				UpdatedAt = updatedAt7,
				Description = table.Column<string>("nvarchar(500)", null, maxLength17),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_LocalityDistrictNationals", x => x.Id);
		});
		migrationBuilder.CreateTable("Programs", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id18 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt6 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt6 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength16 = 500;
			return new
			{
				Id = id18,
				CreatedAt = createdAt6,
				UpdatedAt = updatedAt6,
				Description = table.Column<string>("nvarchar(500)", null, maxLength16),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Programs", x => x.Id);
		});
		migrationBuilder.CreateTable("Projects", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id17 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt5 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt5 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength15 = 500;
			return new
			{
				Id = id17,
				CreatedAt = createdAt5,
				UpdatedAt = updatedAt5,
				Description = table.Column<string>("nvarchar(500)", null, maxLength15),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Projects", x => x.Id);
		});
		migrationBuilder.CreateTable("ReportingMonths", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id16 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			int? maxLength14 = 500;
			return new
			{
				Id = id16,
				Description = table.Column<string>("nvarchar(500)", null, maxLength14),
				Month = table.Column<int>("int"),
				Year = table.Column<int>("int"),
				LastReportingDate = table.Column<DateTime>("datetime2"),
				IsActive = table.Column<bool>("bit"),
				AllowFutureReporting = table.Column<bool>("bit"),
				CreatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()"),
				UpdatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_ReportingMonths", x => x.Id);
		});
		migrationBuilder.CreateTable("ReportStatuses", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id15 = table.Column<int>("int");
			int? maxLength13 = 100;
			OperationBuilder<AddColumnOperation> name2 = table.Column<string>("nvarchar(100)", null, maxLength13);
			maxLength13 = 500;
			return new
			{
				Id = id15,
				Name = name2,
				Description = table.Column<string>("nvarchar(500)", null, maxLength13, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_ReportStatuses", x => x.Id);
		});
		migrationBuilder.CreateTable("SchoolClasses", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id14 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt4 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt4 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength12 = 500;
			return new
			{
				Id = id14,
				CreatedAt = createdAt4,
				UpdatedAt = updatedAt4,
				Description = table.Column<string>("nvarchar(500)", null, maxLength12),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_SchoolClasses", x => x.Id);
		});
		migrationBuilder.CreateTable("Sectors", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id13 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt3 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt3 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength11 = 500;
			return new
			{
				Id = id13,
				CreatedAt = createdAt3,
				UpdatedAt = updatedAt3,
				Description = table.Column<string>("nvarchar(500)", null, maxLength11),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Sectors", x => x.Id);
		});
		migrationBuilder.CreateTable("Subjects", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id12 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> createdAt2 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt2 = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			int? maxLength10 = 500;
			return new
			{
				Id = id12,
				CreatedAt = createdAt2,
				UpdatedAt = updatedAt2,
				Description = table.Column<string>("nvarchar(500)", null, maxLength10),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Subjects", x => x.Id);
		});
		migrationBuilder.CreateTable("SystemConstants", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id11 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			int? maxLength9 = 200;
			OperationBuilder<AddColumnOperation> key = table.Column<string>("nvarchar(200)", null, maxLength9);
			maxLength9 = 1000;
			OperationBuilder<AddColumnOperation> value = table.Column<string>("nvarchar(1000)", null, maxLength9);
			maxLength9 = 500;
			return new
			{
				Id = id11,
				Key = key,
				Value = value,
				Description = table.Column<string>("nvarchar(500)", null, maxLength9, rowVersion: false, null, nullable: true),
				UpdatedBy = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
				CreatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()"),
				UpdatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_SystemConstants", x => x.Id);
		});
		migrationBuilder.CreateTable("UserRoles", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id10 = table.Column<int>("int");
			int? maxLength8 = 100;
			OperationBuilder<AddColumnOperation> name = table.Column<string>("nvarchar(100)", null, maxLength8);
			maxLength8 = 500;
			return new
			{
				Id = id10,
				Name = name,
				Description = table.Column<string>("nvarchar(500)", null, maxLength8, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_UserRoles", x => x.Id);
		});
		migrationBuilder.CreateTable("UserStatuses", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id9 = table.Column<int>("int");
			int? maxLength7 = 100;
			return new
			{
				Id = id9,
				Name = table.Column<string>("nvarchar(100)", null, maxLength7)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_UserStatuses", x => x.Id);
		});
		migrationBuilder.CreateTable("Frameworks", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id8 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			int? maxLength6 = 100;
			OperationBuilder<AddColumnOperation> institutionSymbol2 = table.Column<string>("nvarchar(100)", null, maxLength6);
			OperationBuilder<AddColumnOperation> educationalStageId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> createdAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()");
			OperationBuilder<AddColumnOperation> updatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			maxLength6 = 500;
			return new
			{
				Id = id8,
				InstitutionSymbol = institutionSymbol2,
				EducationalStageId = educationalStageId,
				CreatedAt = createdAt,
				UpdatedAt = updatedAt,
				Description = table.Column<string>("nvarchar(500)", null, maxLength6),
				IsActive = table.Column<bool>("bit")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Frameworks", x => x.Id);
			table.ForeignKey("FK_Frameworks_EducationalStages_EducationalStageId", x => x.EducationalStageId, "EducationalStages", "Id", null, ReferentialAction.NoAction, ReferentialAction.SetNull);
		});
		migrationBuilder.CreateTable("Institutions", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id7 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> institutionSymbol = table.Column<int>("int");
			int? maxLength5 = 500;
			return new
			{
				Id = id7,
				InstitutionSymbol = institutionSymbol,
				Name = table.Column<string>("nvarchar(500)", null, maxLength5),
				IsActive = table.Column<bool>("bit"),
				LocalityId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
				DistrictId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
				SectorId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
				TypeId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
				EducationalStageId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
				CreatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()"),
				UpdatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Institutions", x => x.Id);
			table.ForeignKey("FK_Institutions_Districts_DistrictId", x => x.DistrictId, "Districts", "Id", null, ReferentialAction.NoAction, ReferentialAction.SetNull);
			table.ForeignKey("FK_Institutions_EducationTypes_TypeId", x => x.TypeId, "EducationTypes", "Id", null, ReferentialAction.NoAction, ReferentialAction.SetNull);
			table.ForeignKey("FK_Institutions_EducationalStages_EducationalStageId", x => x.EducationalStageId, "EducationalStages", "Id", null, ReferentialAction.NoAction, ReferentialAction.SetNull);
			table.ForeignKey("FK_Institutions_Localities_LocalityId", x => x.LocalityId, "Localities", "Id", null, ReferentialAction.NoAction, ReferentialAction.SetNull);
			table.ForeignKey("FK_Institutions_Sectors_SectorId", x => x.SectorId, "Sectors", "Id", null, ReferentialAction.NoAction, ReferentialAction.SetNull);
		});
		migrationBuilder.CreateTable("Users", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id6 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			int? maxLength4 = 50;
			OperationBuilder<AddColumnOperation> employeeCode = table.Column<string>("nvarchar(50)", null, maxLength4);
			maxLength4 = 20;
			OperationBuilder<AddColumnOperation> idNumber = table.Column<string>("nvarchar(20)", null, maxLength4);
			maxLength4 = 100;
			OperationBuilder<AddColumnOperation> firstName = table.Column<string>("nvarchar(100)", null, maxLength4);
			maxLength4 = 100;
			OperationBuilder<AddColumnOperation> lastName = table.Column<string>("nvarchar(100)", null, maxLength4);
			maxLength4 = 500;
			OperationBuilder<AddColumnOperation> passwordHash = table.Column<string>("nvarchar(500)", null, maxLength4);
			OperationBuilder<AddColumnOperation> roleId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> userRoleId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> statusId2 = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> isReportingEmployee = table.Column<bool>("bit");
			OperationBuilder<AddColumnOperation> restDay = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> allowFutureReporting = table.Column<bool>("bit");
			maxLength4 = 1000;
			OperationBuilder<AddColumnOperation> notes = table.Column<string>("nvarchar(1000)", null, maxLength4, rowVersion: false, null, nullable: true);
			maxLength4 = 500;
			OperationBuilder<AddColumnOperation> email = table.Column<string>("nvarchar(500)", null, maxLength4, rowVersion: false, null, nullable: true);
			maxLength4 = 50;
			return new
			{
				Id = id6,
				EmployeeCode = employeeCode,
				IdNumber = idNumber,
				FirstName = firstName,
				LastName = lastName,
				PasswordHash = passwordHash,
				RoleId = roleId,
				UserRoleId = userRoleId,
				StatusId = statusId2,
				IsReportingEmployee = isReportingEmployee,
				RestDay = restDay,
				AllowFutureReporting = allowFutureReporting,
				Notes = notes,
				Email = email,
				Phone = table.Column<string>("nvarchar(50)", null, maxLength4, rowVersion: false, null, nullable: true),
				MustChangePassword = table.Column<bool>("bit"),
				FailedLoginAttempts = table.Column<int>("int"),
				LastPasswordChange = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true),
				AcceptedTermsOfUse = table.Column<bool>("bit"),
				CreatedBy = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
				UpdatedBy = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
				CreatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()"),
				UpdatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Users", x => x.Id);
			table.ForeignKey("FK_Users_EmployeeRoles_RoleId", x => x.RoleId, "EmployeeRoles", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_Users_UserRoles_UserRoleId", x => x.UserRoleId, "UserRoles", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_Users_UserStatuses_StatusId", x => x.StatusId, "UserStatuses", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_Users_Users_CreatedBy", x => x.CreatedBy, "Users", "Id");
			table.ForeignKey("FK_Users_Users_UpdatedBy", x => x.UpdatedBy, "Users", "Id");
		});
		migrationBuilder.CreateTable("Allocations", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id5 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> userId4 = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> projectId = table.Column<int>("int");
			int? precision2 = 18;
			int? scale2 = 4;
			OperationBuilder<AddColumnOperation> annualEmploymentScope = table.Column<decimal>("decimal(18,4)", null, null, rowVersion: false, null, nullable: true, null, null, null, null, null, null, precision2, scale2);
			scale2 = 18;
			precision2 = 4;
			OperationBuilder<AddColumnOperation> monthlyEmploymentScope = table.Column<decimal>("decimal(18,4)", null, null, rowVersion: false, null, nullable: true, null, null, null, null, null, null, scale2, precision2);
			precision2 = 18;
			scale2 = 4;
			OperationBuilder<AddColumnOperation> dailyEmploymentScope = table.Column<decimal>("decimal(18,4)", null, null, rowVersion: false, null, nullable: true, null, null, null, null, null, null, precision2, scale2);
			OperationBuilder<AddColumnOperation> monthlyRowAllocation = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> annualRowAllocation = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			scale2 = 500;
			OperationBuilder<AddColumnOperation> outputDuration = table.Column<string>("nvarchar(500)", null, scale2, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> allowExcelUpload = table.Column<bool>("bit");
			scale2 = 1000;
			return new
			{
				Id = id5,
				UserId = userId4,
				ProjectId = projectId,
				AnnualEmploymentScope = annualEmploymentScope,
				MonthlyEmploymentScope = monthlyEmploymentScope,
				DailyEmploymentScope = dailyEmploymentScope,
				MonthlyRowAllocation = monthlyRowAllocation,
				AnnualRowAllocation = annualRowAllocation,
				OutputDuration = outputDuration,
				AllowExcelUpload = allowExcelUpload,
				Notes = table.Column<string>("nvarchar(1000)", null, scale2, rowVersion: false, null, nullable: true),
				IsActive = table.Column<bool>("bit"),
				CreatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()"),
				UpdatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Allocations", x => x.Id);
			table.ForeignKey("FK_Allocations_Projects_ProjectId", x => x.ProjectId, "Projects", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_Allocations_Users_UserId", x => x.UserId, "Users", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("InspectorAssignments", (ColumnsBuilder table) => new
		{
			Id = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1"),
			InspectorUserId = table.Column<int>("int"),
			ProgramId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
			DistrictId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
			SectorId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true)
		}, null, table =>
		{
			table.PrimaryKey("PK_InspectorAssignments", x => x.Id);
			table.ForeignKey("FK_InspectorAssignments_Districts_DistrictId", x => x.DistrictId, "Districts", "Id");
			table.ForeignKey("FK_InspectorAssignments_Programs_ProgramId", x => x.ProgramId, "Programs", "Id");
			table.ForeignKey("FK_InspectorAssignments_Sectors_SectorId", x => x.SectorId, "Sectors", "Id");
			table.ForeignKey("FK_InspectorAssignments_Users_InspectorUserId", x => x.InspectorUserId, "Users", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
		});
		migrationBuilder.CreateTable("PasswordHistories", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id4 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> userId3 = table.Column<int>("int");
			int? maxLength3 = 500;
			return new
			{
				Id = id4,
				UserId = userId3,
				PasswordHash = table.Column<string>("nvarchar(500)", null, maxLength3),
				CreatedAt = table.Column<DateTime>("datetime2")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_PasswordHistories", x => x.Id);
			table.ForeignKey("FK_PasswordHistories_Users_UserId", x => x.UserId, "Users", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
		});
		migrationBuilder.CreateTable("Reports", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id3 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> userId2 = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> reportingMonthId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> statusId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> submittedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> approvedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> approvedBy = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			int? maxLength2 = 1000;
			return new
			{
				Id = id3,
				UserId = userId2,
				ReportingMonthId = reportingMonthId,
				StatusId = statusId,
				SubmittedAt = submittedAt,
				ApprovedAt = approvedAt,
				ApprovedBy = approvedBy,
				RejectionReason = table.Column<string>("nvarchar(1000)", null, maxLength2, rowVersion: false, null, nullable: true),
				RejectedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true),
				RejectedBy = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true),
				ImportedFromExcel = table.Column<bool>("bit"),
				CreatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()"),
				UpdatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_Reports", x => x.Id);
			table.ForeignKey("FK_Reports_ReportStatuses_StatusId", x => x.StatusId, "ReportStatuses", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_Reports_ReportingMonths_ReportingMonthId", x => x.ReportingMonthId, "ReportingMonths", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_Reports_Users_ApprovedBy", x => x.ApprovedBy, "Users", "Id");
			table.ForeignKey("FK_Reports_Users_RejectedBy", x => x.RejectedBy, "Users", "Id");
			table.ForeignKey("FK_Reports_Users_UserId", x => x.UserId, "Users", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationClasses", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			ClassId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationClasses", x => new { x.AllocationId, x.ClassId });
			table.ForeignKey("FK_AllocationClasses_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationClasses_SchoolClasses_ClassId", x => x.ClassId, "SchoolClasses", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationDiscussionCodes", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			DiscussionCodeId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationDiscussionCodes", x => new { x.AllocationId, x.DiscussionCodeId });
			table.ForeignKey("FK_AllocationDiscussionCodes_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationDiscussionCodes_DiscussionCodes_DiscussionCodeId", x => x.DiscussionCodeId, "DiscussionCodes", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationDistricts", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			DistrictId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationDistricts", x => new { x.AllocationId, x.DistrictId });
			table.ForeignKey("FK_AllocationDistricts_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationDistricts_Districts_DistrictId", x => x.DistrictId, "Districts", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationDomains", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			DomainId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationDomains", x => new { x.AllocationId, x.DomainId });
			table.ForeignKey("FK_AllocationDomains_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationDomains_Domains_DomainId", x => x.DomainId, "Domains", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationEducationalPrograms", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			EducationalProgramId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationEducationalPrograms", x => new { x.AllocationId, x.EducationalProgramId });
			table.ForeignKey("FK_AllocationEducationalPrograms_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationEducationalPrograms_EducationalPrograms_EducationalProgramId", x => x.EducationalProgramId, "EducationalPrograms", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationFrameworks", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			FrameworkId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationFrameworks", x => new { x.AllocationId, x.FrameworkId });
			table.ForeignKey("FK_AllocationFrameworks_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationFrameworks_Frameworks_FrameworkId", x => x.FrameworkId, "Frameworks", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationGradeLevels", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			GradeLevelId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationGradeLevels", x => new { x.AllocationId, x.GradeLevelId });
			table.ForeignKey("FK_AllocationGradeLevels_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationGradeLevels_GradeLevels_GradeLevelId", x => x.GradeLevelId, "GradeLevels", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationLocalities", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			LocalityId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationLocalities", x => new { x.AllocationId, x.LocalityId });
			table.ForeignKey("FK_AllocationLocalities_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationLocalities_Localities_LocalityId", x => x.LocalityId, "Localities", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationLocalityDistrictNationals", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			LocalityDistrictNationalId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationLocalityDistrictNationals", x => new { x.AllocationId, x.LocalityDistrictNationalId });
			table.ForeignKey("FK_AllocationLocalityDistrictNationals_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationLocalityDistrictNationals_LocalityDistrictNationals_LocalityDistrictNationalId", x => x.LocalityDistrictNationalId, "LocalityDistrictNationals", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationPrograms", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			ProgramId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationPrograms", x => new { x.AllocationId, x.ProgramId });
			table.ForeignKey("FK_AllocationPrograms_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationPrograms_Programs_ProgramId", x => x.ProgramId, "Programs", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationSectors", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			SectorId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationSectors", x => new { x.AllocationId, x.SectorId });
			table.ForeignKey("FK_AllocationSectors_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationSectors_Sectors_SectorId", x => x.SectorId, "Sectors", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("AllocationSubjects", (ColumnsBuilder table) => new
		{
			AllocationId = table.Column<int>("int"),
			SubjectId = table.Column<int>("int")
		}, null, table =>
		{
			table.PrimaryKey("PK_AllocationSubjects", x => new { x.AllocationId, x.SubjectId });
			table.ForeignKey("FK_AllocationSubjects_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_AllocationSubjects_Subjects_SubjectId", x => x.SubjectId, "Subjects", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
		});
		migrationBuilder.CreateTable("ReportRows", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id2 = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> reportId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> allocationId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> sequenceNumber = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> meetingDate = table.Column<DateTime>("datetime2");
			int? precision = 18;
			int? scale = 4;
			OperationBuilder<AddColumnOperation> meetingDuration = table.Column<decimal>("decimal(18,4)", null, null, rowVersion: false, null, nullable: false, null, null, null, null, null, null, precision, scale);
			OperationBuilder<AddColumnOperation> districtId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> localityId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> frameworkId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> educationalProgramId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> domainId = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> subject1Id = table.Column<int>("int");
			OperationBuilder<AddColumnOperation> subject2Id = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> discussionCodeId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> conclusionClassId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> conclusionFrameworkId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> conclusionLocationId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> gradeLevelId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> classId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			scale = 2000;
			return new
			{
				Id = id2,
				ReportId = reportId,
				AllocationId = allocationId,
				SequenceNumber = sequenceNumber,
				MeetingDate = meetingDate,
				MeetingDuration = meetingDuration,
				DistrictId = districtId,
				LocalityId = localityId,
				FrameworkId = frameworkId,
				EducationalProgramId = educationalProgramId,
				DomainId = domainId,
				Subject1Id = subject1Id,
				Subject2Id = subject2Id,
				DiscussionCodeId = discussionCodeId,
				ConclusionClassId = conclusionClassId,
				ConclusionFrameworkId = conclusionFrameworkId,
				ConclusionLocationId = conclusionLocationId,
				GradeLevelId = gradeLevelId,
				ClassId = classId,
				Notes = table.Column<string>("nvarchar(2000)", null, scale, rowVersion: false, null, nullable: true),
				CreatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: false, null, "GETUTCDATE()"),
				UpdatedAt = table.Column<DateTime>("datetime2", null, null, rowVersion: false, null, nullable: true)
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_ReportRows", x => x.Id);
			table.ForeignKey("FK_ReportRows_Allocations_AllocationId", x => x.AllocationId, "Allocations", "Id");
			table.ForeignKey("FK_ReportRows_DiscussionCodes_DiscussionCodeId", x => x.DiscussionCodeId, "DiscussionCodes", "Id");
			table.ForeignKey("FK_ReportRows_Districts_DistrictId", x => x.DistrictId, "Districts", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_ReportRows_Domains_DomainId", x => x.DomainId, "Domains", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_ReportRows_EducationalPrograms_EducationalProgramId", x => x.EducationalProgramId, "EducationalPrograms", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_ReportRows_Frameworks_FrameworkId", x => x.FrameworkId, "Frameworks", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_ReportRows_GradeLevels_GradeLevelId", x => x.GradeLevelId, "GradeLevels", "Id");
			table.ForeignKey("FK_ReportRows_Localities_LocalityId", x => x.LocalityId, "Localities", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_ReportRows_Reports_ReportId", x => x.ReportId, "Reports", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_ReportRows_SchoolClasses_ClassId", x => x.ClassId, "SchoolClasses", "Id");
			table.ForeignKey("FK_ReportRows_SchoolClasses_ConclusionClassId", x => x.ConclusionClassId, "SchoolClasses", "Id");
			table.ForeignKey("FK_ReportRows_Subjects_Subject1Id", x => x.Subject1Id, "Subjects", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_ReportRows_Subjects_Subject2Id", x => x.Subject2Id, "Subjects", "Id");
		});
		migrationBuilder.CreateTable("DocumentAttachments", delegate(ColumnsBuilder table)
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("int").Annotation("SqlServer:Identity", "1, 1");
			OperationBuilder<AddColumnOperation> userId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			OperationBuilder<AddColumnOperation> reportRowId = table.Column<int>("int", null, null, rowVersion: false, null, nullable: true);
			int? maxLength = 500;
			OperationBuilder<AddColumnOperation> fileName = table.Column<string>("nvarchar(500)", null, maxLength);
			maxLength = 1000;
			OperationBuilder<AddColumnOperation> filePath = table.Column<string>("nvarchar(1000)", null, maxLength);
			OperationBuilder<AddColumnOperation> fileSize = table.Column<long>("bigint");
			maxLength = 200;
			return new
			{
				Id = id,
				UserId = userId,
				ReportRowId = reportRowId,
				FileName = fileName,
				FilePath = filePath,
				FileSize = fileSize,
				MimeType = table.Column<string>("nvarchar(200)", null, maxLength),
				UploadedAt = table.Column<DateTime>("datetime2"),
				UploadedBy = table.Column<int>("int")
			};
		}, null, table =>
		{
			table.PrimaryKey("PK_DocumentAttachments", x => x.Id);
			table.ForeignKey("FK_DocumentAttachments_ReportRows_ReportRowId", x => x.ReportRowId, "ReportRows", "Id", null, ReferentialAction.NoAction, ReferentialAction.Cascade);
			table.ForeignKey("FK_DocumentAttachments_Users_UploadedBy", x => x.UploadedBy, "Users", "Id", null, ReferentialAction.NoAction, ReferentialAction.Restrict);
			table.ForeignKey("FK_DocumentAttachments_Users_UserId", x => x.UserId, "Users", "Id");
		});
		migrationBuilder.InsertData("EmailTemplates", new string[7] { "Id", "Body", "CreatedAt", "IsActive", "Subject", "TypeDescription", "UpdatedAt" }, new object[5, 7]
		{
			{
				1,
				"שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} התקבל בהצלחה.\n\nבברכה,\nמערכת סייט אנד סאונד",
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				true,
				"דיווח פעילות חודשית התקבל",
				"ReportReceived",
				null
			},
			{
				2,
				"שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} אושר.\n\nבברכה,\nמערכת סייט אנד סאונד",
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				true,
				"דיווח פעילות חודשית אושר",
				"ReportApproved",
				null
			},
			{
				3,
				"שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} הוחזר לתיקון.\n\nסיבת ההחזרה: {{RejectionReason}}\n\nנא לתקן ולהגיש מחדש.\n\nבברכה,\nמערכת סייט אנד סאונד",
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				true,
				"דיווח פעילות חודשית הוחזר לתיקון",
				"ReportRejected",
				null
			},
			{
				4,
				"שלום {{EmployeeName}},\n\nנא לשים לב שדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} טרם הוגש.\n\nהמועד האחרון להגשה: {{Deadline}}.\n\nבברכה,\nמערכת סייט אנד סאונד",
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				true,
				"תזכורת: דיווח פעילות חודשית טרם הוגש",
				"ReminderNotSubmitted",
				null
			},
			{
				5,
				"שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} הוחזר לתיקון וטרם תוקן.\n\nנא לתקן ולהגיש לפני: {{Deadline}}.\n\nבברכה,\nמערכת סייט אנד סאונד",
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				true,
				"תזכורת: דיווח פעילות חודשית ממתין לתיקון",
				"ReminderNeedsCorrection",
				null
			}
		});
		migrationBuilder.InsertData("EmployeeRoles", new string[5] { "Id", "CreatedAt", "Description", "IsActive", "UpdatedAt" }, new object[5, 5]
		{
			{
				1,
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				"מורה",
				true,
				null
			},
			{
				2,
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				"מנהל",
				true,
				null
			},
			{
				3,
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				"רכז",
				true,
				null
			},
			{
				4,
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				"יועץ",
				true,
				null
			},
			{
				5,
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				"מפקח",
				true,
				null
			}
		});
		migrationBuilder.InsertData("ReportStatuses", new string[3] { "Id", "Description", "Name" }, new object[6, 3]
		{
			{ 1, "טיוטה - הדוח נוצר אך לא הוגש", "Draft" },
			{ 2, "בהקלדה - הדוח נמצא בתהליך הקלדה", "InEntry" },
			{ 3, "ממתין לאישור - הדוח הוגש וממתין לאישור", "PendingApproval" },
			{ 4, "מאושר - הדוח אושר", "Approved" },
			{ 5, "הוחזר לתיקון - הדוח הוחזר לעובד לתיקון", "ReturnedForCorrection" },
			{ 6, "נעול - הדוח נעול ואינו ניתן לעריכה", "Locked" }
		});
		migrationBuilder.InsertData("SystemConstants", new string[7] { "Id", "CreatedAt", "Description", "Key", "UpdatedAt", "UpdatedBy", "Value" }, new object[4, 7]
		{
			{
				1,
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				"מרווח בין תזכורות בימים",
				"ReminderIntervalDays",
				null,
				null,
				"3"
			},
			{
				2,
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				"כמה ימים לפני הדדליין מתחילות התזכורות",
				"ReminderStartDaysBeforeDeadline",
				null,
				null,
				"7"
			},
			{
				3,
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				"סף אחוז דמיון בהערות (Levenshtein normalized)",
				"NotesSimilarityThresholdPercent",
				null,
				null,
				"90"
			},
			{
				4,
				new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				"מקסימום שעות יומי ברירת מחדל לשורת דיווח",
				"MaxDailyHoursDefault",
				null,
				null,
				"9"
			}
		});
		migrationBuilder.InsertData("UserRoles", new string[3] { "Id", "Description", "Name" }, new object[6, 3]
		{
			{ 1, "מנהל מערכת - גישה מלאה לכל הפונקציות", "SystemAdmin" },
			{ 2, "מנהל פרויקט - ניהול עובדים, הקצאות ופתיחת חודשים", "ProjectManager" },
			{ 3, "רכז פרויקט - יצירת עובדים, הקצאות ואישור דיווחים", "ProjectCoordinator" },
			{ 4, "מפקח צפייה - צפייה בלבד בהיקף מוגדר, ייצוא מאושרים", "InspectorView" },
			{ 5, "מפקח אישור - צפייה + אישור/דחיית דיווחים", "InspectorApproval" },
			{ 6, "עובד - צפייה בנתוניו האישיים ומילוי דיווחים", "Employee" }
		});
		migrationBuilder.InsertData("UserStatuses", new string[2] { "Id", "Name" }, new object[3, 2]
		{
			{ 1, "Active" },
			{ 2, "Inactive" },
			{ 3, "Locked" }
		});
		migrationBuilder.InsertData("Users", new string[23]
		{
			"Id", "AcceptedTermsOfUse", "AllowFutureReporting", "CreatedAt", "CreatedBy", "Email", "EmployeeCode", "FailedLoginAttempts", "FirstName", "IdNumber",
			"IsReportingEmployee", "LastName", "LastPasswordChange", "MustChangePassword", "Notes", "PasswordHash", "Phone", "RestDay", "RoleId", "StatusId",
			"UpdatedAt", "UpdatedBy", "UserRoleId"
		}, new object[23]
		{
			1,
			false,
			false,
			new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
			null,
			null,
			"ADMIN001",
			0,
			"מנהל",
			"admin",
			false,
			"מערכת",
			null,
			true,
			null,
			"$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewdGADNUvDdAfY2.",
			null,
			null,
			1,
			1,
			null,
			null,
			1
		});
		migrationBuilder.CreateIndex("IX_AllocationClasses_ClassId", "AllocationClasses", "ClassId");
		migrationBuilder.CreateIndex("IX_AllocationDiscussionCodes_DiscussionCodeId", "AllocationDiscussionCodes", "DiscussionCodeId");
		migrationBuilder.CreateIndex("IX_AllocationDistricts_DistrictId", "AllocationDistricts", "DistrictId");
		migrationBuilder.CreateIndex("IX_AllocationDomains_DomainId", "AllocationDomains", "DomainId");
		migrationBuilder.CreateIndex("IX_AllocationEducationalPrograms_EducationalProgramId", "AllocationEducationalPrograms", "EducationalProgramId");
		migrationBuilder.CreateIndex("IX_AllocationFrameworks_FrameworkId", "AllocationFrameworks", "FrameworkId");
		migrationBuilder.CreateIndex("IX_AllocationGradeLevels_GradeLevelId", "AllocationGradeLevels", "GradeLevelId");
		migrationBuilder.CreateIndex("IX_AllocationLocalities_LocalityId", "AllocationLocalities", "LocalityId");
		migrationBuilder.CreateIndex("IX_AllocationLocalityDistrictNationals_LocalityDistrictNationalId", "AllocationLocalityDistrictNationals", "LocalityDistrictNationalId");
		migrationBuilder.CreateIndex("IX_AllocationPrograms_ProgramId", "AllocationPrograms", "ProgramId");
		migrationBuilder.CreateIndex("IX_Allocations_ProjectId", "Allocations", "ProjectId");
		migrationBuilder.CreateIndex("IX_Allocations_UserId_ProjectId", "Allocations", new string[2] { "UserId", "ProjectId" }, null, unique: true);
		migrationBuilder.CreateIndex("IX_AllocationSectors_SectorId", "AllocationSectors", "SectorId");
		migrationBuilder.CreateIndex("IX_AllocationSubjects_SubjectId", "AllocationSubjects", "SubjectId");
		migrationBuilder.CreateIndex("IX_DocumentAttachments_ReportRowId", "DocumentAttachments", "ReportRowId");
		migrationBuilder.CreateIndex("IX_DocumentAttachments_UploadedBy", "DocumentAttachments", "UploadedBy");
		migrationBuilder.CreateIndex("IX_DocumentAttachments_UserId", "DocumentAttachments", "UserId");
		migrationBuilder.CreateIndex("IX_Frameworks_EducationalStageId", "Frameworks", "EducationalStageId");
		migrationBuilder.CreateIndex("IX_Frameworks_InstitutionSymbol_EducationalStageId", "Frameworks", new string[2] { "InstitutionSymbol", "EducationalStageId" }, null, unique: true, "[EducationalStageId] IS NOT NULL");
		migrationBuilder.CreateIndex("IX_InspectorAssignments_DistrictId", "InspectorAssignments", "DistrictId");
		migrationBuilder.CreateIndex("IX_InspectorAssignments_InspectorUserId", "InspectorAssignments", "InspectorUserId");
		migrationBuilder.CreateIndex("IX_InspectorAssignments_ProgramId", "InspectorAssignments", "ProgramId");
		migrationBuilder.CreateIndex("IX_InspectorAssignments_SectorId", "InspectorAssignments", "SectorId");
		migrationBuilder.CreateIndex("IX_Institutions_DistrictId", "Institutions", "DistrictId");
		migrationBuilder.CreateIndex("IX_Institutions_EducationalStageId", "Institutions", "EducationalStageId");
		migrationBuilder.CreateIndex("IX_Institutions_InstitutionSymbol_EducationalStageId", "Institutions", new string[2] { "InstitutionSymbol", "EducationalStageId" }, null, unique: true, "[EducationalStageId] IS NOT NULL");
		migrationBuilder.CreateIndex("IX_Institutions_LocalityId", "Institutions", "LocalityId");
		migrationBuilder.CreateIndex("IX_Institutions_SectorId", "Institutions", "SectorId");
		migrationBuilder.CreateIndex("IX_Institutions_TypeId", "Institutions", "TypeId");
		migrationBuilder.CreateIndex("IX_PasswordHistories_UserId", "PasswordHistories", "UserId");
		migrationBuilder.CreateIndex("IX_ReportRows_AllocationId", "ReportRows", "AllocationId");
		migrationBuilder.CreateIndex("IX_ReportRows_ClassId", "ReportRows", "ClassId");
		migrationBuilder.CreateIndex("IX_ReportRows_ConclusionClassId", "ReportRows", "ConclusionClassId");
		migrationBuilder.CreateIndex("IX_ReportRows_DiscussionCodeId", "ReportRows", "DiscussionCodeId");
		migrationBuilder.CreateIndex("IX_ReportRows_DistrictId", "ReportRows", "DistrictId");
		migrationBuilder.CreateIndex("IX_ReportRows_DomainId", "ReportRows", "DomainId");
		migrationBuilder.CreateIndex("IX_ReportRows_EducationalProgramId", "ReportRows", "EducationalProgramId");
		migrationBuilder.CreateIndex("IX_ReportRows_FrameworkId", "ReportRows", "FrameworkId");
		migrationBuilder.CreateIndex("IX_ReportRows_GradeLevelId", "ReportRows", "GradeLevelId");
		migrationBuilder.CreateIndex("IX_ReportRows_LocalityId", "ReportRows", "LocalityId");
		migrationBuilder.CreateIndex("IX_ReportRows_ReportId", "ReportRows", "ReportId");
		migrationBuilder.CreateIndex("IX_ReportRows_Subject1Id", "ReportRows", "Subject1Id");
		migrationBuilder.CreateIndex("IX_ReportRows_Subject2Id", "ReportRows", "Subject2Id");
		migrationBuilder.CreateIndex("IX_Reports_ApprovedBy", "Reports", "ApprovedBy");
		migrationBuilder.CreateIndex("IX_Reports_RejectedBy", "Reports", "RejectedBy");
		migrationBuilder.CreateIndex("IX_Reports_ReportingMonthId", "Reports", "ReportingMonthId");
		migrationBuilder.CreateIndex("IX_Reports_StatusId", "Reports", "StatusId");
		migrationBuilder.CreateIndex("IX_Reports_UserId_ReportingMonthId", "Reports", new string[2] { "UserId", "ReportingMonthId" }, null, unique: true);
		migrationBuilder.CreateIndex("IX_SystemConstants_Key", "SystemConstants", "Key", null, unique: true);
		migrationBuilder.CreateIndex("IX_Users_CreatedBy", "Users", "CreatedBy");
		migrationBuilder.CreateIndex("IX_Users_IdNumber", "Users", "IdNumber", null, unique: true);
		migrationBuilder.CreateIndex("IX_Users_RoleId", "Users", "RoleId");
		migrationBuilder.CreateIndex("IX_Users_StatusId", "Users", "StatusId");
		migrationBuilder.CreateIndex("IX_Users_UpdatedBy", "Users", "UpdatedBy");
		migrationBuilder.CreateIndex("IX_Users_UserRoleId", "Users", "UserRoleId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable("AllocationClasses");
		migrationBuilder.DropTable("AllocationDiscussionCodes");
		migrationBuilder.DropTable("AllocationDistricts");
		migrationBuilder.DropTable("AllocationDomains");
		migrationBuilder.DropTable("AllocationEducationalPrograms");
		migrationBuilder.DropTable("AllocationFrameworks");
		migrationBuilder.DropTable("AllocationGradeLevels");
		migrationBuilder.DropTable("AllocationLocalities");
		migrationBuilder.DropTable("AllocationLocalityDistrictNationals");
		migrationBuilder.DropTable("AllocationPrograms");
		migrationBuilder.DropTable("AllocationSectors");
		migrationBuilder.DropTable("AllocationSubjects");
		migrationBuilder.DropTable("Authorities");
		migrationBuilder.DropTable("DocumentAttachments");
		migrationBuilder.DropTable("EmailServerSettings");
		migrationBuilder.DropTable("EmailTemplates");
		migrationBuilder.DropTable("InspectorAssignments");
		migrationBuilder.DropTable("Institutions");
		migrationBuilder.DropTable("PasswordHistories");
		migrationBuilder.DropTable("SystemConstants");
		migrationBuilder.DropTable("LocalityDistrictNationals");
		migrationBuilder.DropTable("ReportRows");
		migrationBuilder.DropTable("Programs");
		migrationBuilder.DropTable("EducationTypes");
		migrationBuilder.DropTable("Sectors");
		migrationBuilder.DropTable("Allocations");
		migrationBuilder.DropTable("DiscussionCodes");
		migrationBuilder.DropTable("Districts");
		migrationBuilder.DropTable("Domains");
		migrationBuilder.DropTable("EducationalPrograms");
		migrationBuilder.DropTable("Frameworks");
		migrationBuilder.DropTable("GradeLevels");
		migrationBuilder.DropTable("Localities");
		migrationBuilder.DropTable("Reports");
		migrationBuilder.DropTable("SchoolClasses");
		migrationBuilder.DropTable("Subjects");
		migrationBuilder.DropTable("Projects");
		migrationBuilder.DropTable("EducationalStages");
		migrationBuilder.DropTable("ReportStatuses");
		migrationBuilder.DropTable("ReportingMonths");
		migrationBuilder.DropTable("Users");
		migrationBuilder.DropTable("EmployeeRoles");
		migrationBuilder.DropTable("UserRoles");
		migrationBuilder.DropTable("UserStatuses");
	}

	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
		modelBuilder.HasAnnotation("ProductVersion", "8.0.25").HasAnnotation("Relational:MaxIdentifierLength", 128);
		modelBuilder.UseIdentityColumns(1L);
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Allocation", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<bool>("AllowExcelUpload").HasColumnType("bit");
			b.Property<decimal?>("AnnualEmploymentScope").HasPrecision(18, 4).HasColumnType("decimal(18,4)");
			b.Property<int?>("AnnualRowAllocation").HasColumnType("int");
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<decimal?>("DailyEmploymentScope").HasPrecision(18, 4).HasColumnType("decimal(18,4)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<decimal?>("MonthlyEmploymentScope").HasPrecision(18, 4).HasColumnType("decimal(18,4)");
			b.Property<int?>("MonthlyRowAllocation").HasColumnType("int");
			b.Property<string>("Notes").HasMaxLength(1000).HasColumnType("nvarchar(1000)");
			b.Property<string>("OutputDuration").HasMaxLength(500).HasColumnType("nvarchar(500)");
			b.Property<int>("ProjectId").HasColumnType("int");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.Property<int>("UserId").HasColumnType("int");
			b.HasKey("Id");
			b.HasIndex("ProjectId");
			b.HasIndex("UserId", "ProjectId").IsUnique();
			b.ToTable("Allocations", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationClass", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("ClassId").HasColumnType("int");
			b.HasKey("AllocationId", "ClassId");
			b.HasIndex("ClassId");
			b.ToTable("AllocationClasses", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationDiscussionCode", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("DiscussionCodeId").HasColumnType("int");
			b.HasKey("AllocationId", "DiscussionCodeId");
			b.HasIndex("DiscussionCodeId");
			b.ToTable("AllocationDiscussionCodes", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationDistrict", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("DistrictId").HasColumnType("int");
			b.HasKey("AllocationId", "DistrictId");
			b.HasIndex("DistrictId");
			b.ToTable("AllocationDistricts", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationDomain", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("DomainId").HasColumnType("int");
			b.HasKey("AllocationId", "DomainId");
			b.HasIndex("DomainId");
			b.ToTable("AllocationDomains", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationEducationalProgram", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("EducationalProgramId").HasColumnType("int");
			b.HasKey("AllocationId", "EducationalProgramId");
			b.HasIndex("EducationalProgramId");
			b.ToTable("AllocationEducationalPrograms", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationFramework", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("FrameworkId").HasColumnType("int");
			b.HasKey("AllocationId", "FrameworkId");
			b.HasIndex("FrameworkId");
			b.ToTable("AllocationFrameworks", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationGradeLevel", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("GradeLevelId").HasColumnType("int");
			b.HasKey("AllocationId", "GradeLevelId");
			b.HasIndex("GradeLevelId");
			b.ToTable("AllocationGradeLevels", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationLocality", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("LocalityId").HasColumnType("int");
			b.HasKey("AllocationId", "LocalityId");
			b.HasIndex("LocalityId");
			b.ToTable("AllocationLocalities", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationLocalityDistrictNational", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("LocalityDistrictNationalId").HasColumnType("int");
			b.HasKey("AllocationId", "LocalityDistrictNationalId");
			b.HasIndex("LocalityDistrictNationalId");
			b.ToTable("AllocationLocalityDistrictNationals", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationProgram", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("ProgramId").HasColumnType("int");
			b.HasKey("AllocationId", "ProgramId");
			b.HasIndex("ProgramId");
			b.ToTable("AllocationPrograms", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationSector", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("SectorId").HasColumnType("int");
			b.HasKey("AllocationId", "SectorId");
			b.HasIndex("SectorId");
			b.ToTable("AllocationSectors", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationSubject", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("AllocationId").HasColumnType("int");
			b.Property<int>("SubjectId").HasColumnType("int");
			b.HasKey("AllocationId", "SubjectId");
			b.HasIndex("SubjectId");
			b.ToTable("AllocationSubjects", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Authority", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("Authorities", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.DiscussionCode", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("DiscussionCodes", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.District", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("Districts", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.DocumentAttachment", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<string>("FileName").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<string>("FilePath").IsRequired().HasMaxLength(1000)
				.HasColumnType("nvarchar(1000)");
			b.Property<long>("FileSize").HasColumnType("bigint");
			b.Property<string>("MimeType").IsRequired().HasMaxLength(200)
				.HasColumnType("nvarchar(200)");
			b.Property<int?>("ReportRowId").HasColumnType("int");
			b.Property<DateTime>("UploadedAt").HasColumnType("datetime2");
			b.Property<int>("UploadedBy").HasColumnType("int");
			b.Property<int?>("UserId").HasColumnType("int");
			b.HasKey("Id");
			b.HasIndex("ReportRowId");
			b.HasIndex("UploadedBy");
			b.HasIndex("UserId");
			b.ToTable("DocumentAttachments", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Domain", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("Domains", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.EducationType", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("EducationTypes", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.EducationalProgram", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("EducationalPrograms", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.EducationalStage", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("EducationalStages", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.EmailServerSetting", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("FromAddress").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<string>("FromName").HasMaxLength(500).HasColumnType("nvarchar(500)");
			b.Property<string>("Password").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<int>("Port").HasColumnType("int");
			b.Property<string>("SmtpServer").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.Property<bool>("UseSsl").HasColumnType("bit");
			b.Property<string>("Username").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.HasKey("Id");
			b.ToTable("EmailServerSettings", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.EmailTemplate", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<string>("Body").IsRequired().HasColumnType("nvarchar(max)");
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<string>("Subject").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<string>("TypeDescription").IsRequired().HasMaxLength(200)
				.HasColumnType("nvarchar(200)");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("EmailTemplates", (string?)null);
			b.HasData(new
			{
				Id = 1,
				Body = "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} התקבל בהצלחה.\n\nבברכה,\nמערכת סייט אנד סאונד",
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				IsActive = true,
				Subject = "דיווח פעילות חודשית התקבל",
				TypeDescription = "ReportReceived"
			}, new
			{
				Id = 2,
				Body = "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} אושר.\n\nבברכה,\nמערכת סייט אנד סאונד",
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				IsActive = true,
				Subject = "דיווח פעילות חודשית אושר",
				TypeDescription = "ReportApproved"
			}, new
			{
				Id = 3,
				Body = "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית שלך לחודש {{Month}}/{{Year}} הוחזר לתיקון.\n\nסיבת ההחזרה: {{RejectionReason}}\n\nנא לתקן ולהגיש מחדש.\n\nבברכה,\nמערכת סייט אנד סאונד",
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				IsActive = true,
				Subject = "דיווח פעילות חודשית הוחזר לתיקון",
				TypeDescription = "ReportRejected"
			}, new
			{
				Id = 4,
				Body = "שלום {{EmployeeName}},\n\nנא לשים לב שדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} טרם הוגש.\n\nהמועד האחרון להגשה: {{Deadline}}.\n\nבברכה,\nמערכת סייט אנד סאונד",
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				IsActive = true,
				Subject = "תזכורת: דיווח פעילות חודשית טרם הוגש",
				TypeDescription = "ReminderNotSubmitted"
			}, new
			{
				Id = 5,
				Body = "שלום {{EmployeeName}},\n\nדיווח הפעילות החודשית לחודש {{Month}}/{{Year}} הוחזר לתיקון וטרם תוקן.\n\nנא לתקן ולהגיש לפני: {{Deadline}}.\n\nבברכה,\nמערכת סייט אנד סאונד",
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				IsActive = true,
				Subject = "תזכורת: דיווח פעילות חודשית ממתין לתיקון",
				TypeDescription = "ReminderNeedsCorrection"
			});
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.EmployeeRole", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("EmployeeRoles", (string?)null);
			b.HasData(new
			{
				Id = 1,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				Description = "מורה",
				IsActive = true
			}, new
			{
				Id = 2,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				Description = "מנהל",
				IsActive = true
			}, new
			{
				Id = 3,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				Description = "רכז",
				IsActive = true
			}, new
			{
				Id = 4,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				Description = "יועץ",
				IsActive = true
			}, new
			{
				Id = 5,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				Description = "מפקח",
				IsActive = true
			});
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Framework", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<int?>("EducationalStageId").HasColumnType("int");
			b.Property<string>("InstitutionSymbol").IsRequired().HasMaxLength(100)
				.HasColumnType("nvarchar(100)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.HasIndex("EducationalStageId");
			b.HasIndex("InstitutionSymbol", "EducationalStageId").IsUnique().HasFilter("[EducationalStageId] IS NOT NULL");
			b.ToTable("Frameworks", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.GradeLevel", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("GradeLevels", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.InspectorAssignment", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<int?>("DistrictId").HasColumnType("int");
			b.Property<int>("InspectorUserId").HasColumnType("int");
			b.Property<int?>("ProgramId").HasColumnType("int");
			b.Property<int?>("SectorId").HasColumnType("int");
			b.HasKey("Id");
			b.HasIndex("DistrictId");
			b.HasIndex("InspectorUserId");
			b.HasIndex("ProgramId");
			b.HasIndex("SectorId");
			b.ToTable("InspectorAssignments", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Institution", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<int?>("DistrictId").HasColumnType("int");
			b.Property<int?>("EducationalStageId").HasColumnType("int");
			b.Property<int>("InstitutionSymbol").HasColumnType("int");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<int?>("LocalityId").HasColumnType("int");
			b.Property<string>("Name").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<int?>("SectorId").HasColumnType("int");
			b.Property<int?>("TypeId").HasColumnType("int");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.HasIndex("DistrictId");
			b.HasIndex("EducationalStageId");
			b.HasIndex("LocalityId");
			b.HasIndex("SectorId");
			b.HasIndex("TypeId");
			b.HasIndex("InstitutionSymbol", "EducationalStageId").IsUnique().HasFilter("[EducationalStageId] IS NOT NULL");
			b.ToTable("Institutions", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Locality", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<int?>("NationalCode").HasColumnType("int");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("Localities", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.LocalityDistrictNational", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("LocalityDistrictNationals", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.PasswordHistory", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").HasColumnType("datetime2");
			b.Property<string>("PasswordHash").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<int>("UserId").HasColumnType("int");
			b.HasKey("Id");
			b.HasIndex("UserId");
			b.ToTable("PasswordHistories", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Program", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("Programs", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Project", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("Projects", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Report", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime?>("ApprovedAt").HasColumnType("datetime2");
			b.Property<int?>("ApprovedBy").HasColumnType("int");
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<bool>("ImportedFromExcel").HasColumnType("bit");
			b.Property<DateTime?>("RejectedAt").HasColumnType("datetime2");
			b.Property<int?>("RejectedBy").HasColumnType("int");
			b.Property<string>("RejectionReason").HasMaxLength(1000).HasColumnType("nvarchar(1000)");
			b.Property<int>("ReportingMonthId").HasColumnType("int");
			b.Property<int>("StatusId").HasColumnType("int");
			b.Property<DateTime?>("SubmittedAt").HasColumnType("datetime2");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.Property<int>("UserId").HasColumnType("int");
			b.HasKey("Id");
			b.HasIndex("ApprovedBy");
			b.HasIndex("RejectedBy");
			b.HasIndex("ReportingMonthId");
			b.HasIndex("StatusId");
			b.HasIndex("UserId", "ReportingMonthId").IsUnique();
			b.ToTable("Reports", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.ReportRow", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<int?>("AllocationId").HasColumnType("int");
			b.Property<int?>("ClassId").HasColumnType("int");
			b.Property<int?>("ConclusionClassId").HasColumnType("int");
			b.Property<int?>("ConclusionFrameworkId").HasColumnType("int");
			b.Property<int?>("ConclusionLocationId").HasColumnType("int");
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<int?>("DiscussionCodeId").HasColumnType("int");
			b.Property<int>("DistrictId").HasColumnType("int");
			b.Property<int>("DomainId").HasColumnType("int");
			b.Property<int>("EducationalProgramId").HasColumnType("int");
			b.Property<int>("FrameworkId").HasColumnType("int");
			b.Property<int?>("GradeLevelId").HasColumnType("int");
			b.Property<int>("LocalityId").HasColumnType("int");
			b.Property<DateTime>("MeetingDate").HasColumnType("datetime2");
			b.Property<decimal>("MeetingDuration").HasPrecision(18, 4).HasColumnType("decimal(18,4)");
			b.Property<string>("Notes").HasMaxLength(2000).HasColumnType("nvarchar(2000)");
			b.Property<int>("ReportId").HasColumnType("int");
			b.Property<int>("SequenceNumber").HasColumnType("int");
			b.Property<int>("Subject1Id").HasColumnType("int");
			b.Property<int?>("Subject2Id").HasColumnType("int");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.HasIndex("AllocationId");
			b.HasIndex("ClassId");
			b.HasIndex("ConclusionClassId");
			b.HasIndex("DiscussionCodeId");
			b.HasIndex("DistrictId");
			b.HasIndex("DomainId");
			b.HasIndex("EducationalProgramId");
			b.HasIndex("FrameworkId");
			b.HasIndex("GradeLevelId");
			b.HasIndex("LocalityId");
			b.HasIndex("ReportId");
			b.HasIndex("Subject1Id");
			b.HasIndex("Subject2Id");
			b.ToTable("ReportRows", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.ReportStatus", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").HasColumnType("int");
			b.Property<string>("Description").HasMaxLength(500).HasColumnType("nvarchar(500)");
			b.Property<string>("Name").IsRequired().HasMaxLength(100)
				.HasColumnType("nvarchar(100)");
			b.HasKey("Id");
			b.ToTable("ReportStatuses", (string?)null);
			b.HasData(new
			{
				Id = 1,
				Description = "טיוטה - הדוח נוצר אך לא הוגש",
				Name = "Draft"
			}, new
			{
				Id = 2,
				Description = "בהקלדה - הדוח נמצא בתהליך הקלדה",
				Name = "InEntry"
			}, new
			{
				Id = 3,
				Description = "ממתין לאישור - הדוח הוגש וממתין לאישור",
				Name = "PendingApproval"
			}, new
			{
				Id = 4,
				Description = "מאושר - הדוח אושר",
				Name = "Approved"
			}, new
			{
				Id = 5,
				Description = "הוחזר לתיקון - הדוח הוחזר לעובד לתיקון",
				Name = "ReturnedForCorrection"
			}, new
			{
				Id = 6,
				Description = "נעול - הדוח נעול ואינו ניתן לעריכה",
				Name = "Locked"
			});
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.ReportingMonth", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<bool>("AllowFutureReporting").HasColumnType("bit");
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime>("LastReportingDate").HasColumnType("datetime2");
			b.Property<int>("Month").HasColumnType("int");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.Property<int>("Year").HasColumnType("int");
			b.HasKey("Id");
			b.ToTable("ReportingMonths", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.SchoolClass", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("SchoolClasses", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Sector", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("Sectors", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Subject", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<bool>("IsActive").HasColumnType("bit");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.HasKey("Id");
			b.ToTable("Subjects", (string?)null);
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.SystemConstant", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<string>("Description").HasMaxLength(500).HasColumnType("nvarchar(500)");
			b.Property<string>("Key").IsRequired().HasMaxLength(200)
				.HasColumnType("nvarchar(200)");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.Property<int?>("UpdatedBy").HasColumnType("int");
			b.Property<string>("Value").IsRequired().HasMaxLength(1000)
				.HasColumnType("nvarchar(1000)");
			b.HasKey("Id");
			b.HasIndex("Key").IsUnique();
			b.ToTable("SystemConstants", (string?)null);
			b.HasData(new
			{
				Id = 1,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				Description = "מרווח בין תזכורות בימים",
				Key = "ReminderIntervalDays",
				Value = "3"
			}, new
			{
				Id = 2,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				Description = "כמה ימים לפני הדדליין מתחילות התזכורות",
				Key = "ReminderStartDaysBeforeDeadline",
				Value = "7"
			}, new
			{
				Id = 3,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				Description = "סף אחוז דמיון בהערות (Levenshtein normalized)",
				Key = "NotesSimilarityThresholdPercent",
				Value = "90"
			}, new
			{
				Id = 4,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				Description = "מקסימום שעות יומי ברירת מחדל לשורת דיווח",
				Key = "MaxDailyHoursDefault",
				Value = "9"
			});
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.User", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
			b.Property<int>("Id").UseIdentityColumn(1L);
			b.Property<bool>("AcceptedTermsOfUse").HasColumnType("bit");
			b.Property<bool>("AllowFutureReporting").HasColumnType("bit");
			b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2")
				.HasDefaultValueSql("GETUTCDATE()");
			b.Property<int?>("CreatedBy").HasColumnType("int");
			b.Property<string>("Email").HasMaxLength(500).HasColumnType("nvarchar(500)");
			b.Property<string>("EmployeeCode").IsRequired().HasMaxLength(50)
				.HasColumnType("nvarchar(50)");
			b.Property<int>("FailedLoginAttempts").HasColumnType("int");
			b.Property<string>("FirstName").IsRequired().HasMaxLength(100)
				.HasColumnType("nvarchar(100)");
			b.Property<string>("IdNumber").IsRequired().HasMaxLength(20)
				.HasColumnType("nvarchar(20)");
			b.Property<bool>("IsReportingEmployee").HasColumnType("bit");
			b.Property<string>("LastName").IsRequired().HasMaxLength(100)
				.HasColumnType("nvarchar(100)");
			b.Property<DateTime?>("LastPasswordChange").HasColumnType("datetime2");
			b.Property<bool>("MustChangePassword").HasColumnType("bit");
			b.Property<string>("Notes").HasMaxLength(1000).HasColumnType("nvarchar(1000)");
			b.Property<string>("PasswordHash").IsRequired().HasMaxLength(500)
				.HasColumnType("nvarchar(500)");
			b.Property<string>("Phone").HasMaxLength(50).HasColumnType("nvarchar(50)");
			b.Property<int?>("RestDay").HasColumnType("int");
			b.Property<int>("RoleId").HasColumnType("int");
			b.Property<int>("StatusId").HasColumnType("int");
			b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
			b.Property<int?>("UpdatedBy").HasColumnType("int");
			b.Property<int>("UserRoleId").HasColumnType("int");
			b.HasKey("Id");
			b.HasIndex("CreatedBy");
			b.HasIndex("IdNumber").IsUnique();
			b.HasIndex("RoleId");
			b.HasIndex("StatusId");
			b.HasIndex("UpdatedBy");
			b.HasIndex("UserRoleId");
			b.ToTable("Users", (string?)null);
			b.HasData(new
			{
				Id = 1,
				AcceptedTermsOfUse = false,
				AllowFutureReporting = false,
				CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
				EmployeeCode = "ADMIN001",
				FailedLoginAttempts = 0,
				FirstName = "מנהל",
				IdNumber = "admin",
				IsReportingEmployee = false,
				LastName = "מערכת",
				MustChangePassword = true,
				PasswordHash = "$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewdGADNUvDdAfY2.",
				RoleId = 1,
				StatusId = 1,
				UserRoleId = 1
			});
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.UserRole", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").HasColumnType("int");
			b.Property<string>("Description").HasMaxLength(500).HasColumnType("nvarchar(500)");
			b.Property<string>("Name").IsRequired().HasMaxLength(100)
				.HasColumnType("nvarchar(100)");
			b.HasKey("Id");
			b.ToTable("UserRoles", (string?)null);
			b.HasData(new
			{
				Id = 1,
				Description = "מנהל מערכת - גישה מלאה לכל הפונקציות",
				Name = "SystemAdmin"
			}, new
			{
				Id = 2,
				Description = "מנהל פרויקט - ניהול עובדים, הקצאות ופתיחת חודשים",
				Name = "ProjectManager"
			}, new
			{
				Id = 3,
				Description = "רכז פרויקט - יצירת עובדים, הקצאות ואישור דיווחים",
				Name = "ProjectCoordinator"
			}, new
			{
				Id = 4,
				Description = "מפקח צפייה - צפייה בלבד בהיקף מוגדר, ייצוא מאושרים",
				Name = "InspectorView"
			}, new
			{
				Id = 5,
				Description = "מפקח אישור - צפייה + אישור/דחיית דיווחים",
				Name = "InspectorApproval"
			}, new
			{
				Id = 6,
				Description = "עובד - צפייה בנתוניו האישיים ומילוי דיווחים",
				Name = "Employee"
			});
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.UserStatus", delegate(EntityTypeBuilder b)
		{
			b.Property<int>("Id").HasColumnType("int");
			b.Property<string>("Name").IsRequired().HasMaxLength(100)
				.HasColumnType("nvarchar(100)");
			b.HasKey("Id");
			b.ToTable("UserStatuses", (string?)null);
			b.HasData(new
			{
				Id = 1,
				Name = "Active"
			}, new
			{
				Id = 2,
				Name = "Inactive"
			}, new
			{
				Id = 3,
				Name = "Locked"
			});
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Allocation", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Project", "Project").WithMany().HasForeignKey("ProjectId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.User", "User").WithMany("Allocations").HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Project");
			b.Navigation("User");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationClass", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationClasses").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.SchoolClass", "SchoolClass").WithMany().HasForeignKey("ClassId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("SchoolClass");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationDiscussionCode", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationDiscussionCodes").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.DiscussionCode", "DiscussionCode").WithMany().HasForeignKey("DiscussionCodeId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("DiscussionCode");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationDistrict", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationDistricts").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.District", "District").WithMany().HasForeignKey("DistrictId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("District");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationDomain", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationDomains").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Domain", "Domain").WithMany().HasForeignKey("DomainId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("Domain");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationEducationalProgram", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationEducationalPrograms").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.EducationalProgram", "EducationalProgram").WithMany().HasForeignKey("EducationalProgramId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("EducationalProgram");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationFramework", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationFrameworks").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Framework", "Framework").WithMany().HasForeignKey("FrameworkId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("Framework");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationGradeLevel", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationGradeLevels").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.GradeLevel", "GradeLevel").WithMany().HasForeignKey("GradeLevelId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("GradeLevel");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationLocality", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationLocalities").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Locality", "Locality").WithMany().HasForeignKey("LocalityId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("Locality");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationLocalityDistrictNational", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationLocalityDistrictNationals").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.LocalityDistrictNational", "LocalityDistrictNational").WithMany().HasForeignKey("LocalityDistrictNationalId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("LocalityDistrictNational");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationProgram", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationPrograms").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Program", "Program").WithMany().HasForeignKey("ProgramId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("Program");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationSector", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationSectors").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Sector", "Sector").WithMany().HasForeignKey("SectorId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("Sector");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.AllocationSubject", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany("AllocationSubjects").HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Subject", "Subject").WithMany().HasForeignKey("SubjectId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Allocation");
			b.Navigation("Subject");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.DocumentAttachment", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.ReportRow", "ReportRow").WithMany().HasForeignKey("ReportRowId")
				.OnDelete(DeleteBehavior.Cascade);
			b.HasOne("AxiomaReporting.Core.Entities.User", "UploadedByUser").WithMany().HasForeignKey("UploadedBy")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.User", "User").WithMany().HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.NoAction);
			b.Navigation("ReportRow");
			b.Navigation("UploadedByUser");
			b.Navigation("User");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Framework", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.EducationalStage", "EducationalStage").WithMany().HasForeignKey("EducationalStageId")
				.OnDelete(DeleteBehavior.SetNull);
			b.Navigation("EducationalStage");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.InspectorAssignment", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.District", "District").WithMany().HasForeignKey("DistrictId")
				.OnDelete(DeleteBehavior.NoAction);
			b.HasOne("AxiomaReporting.Core.Entities.User", "Inspector").WithMany().HasForeignKey("InspectorUserId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Program", "Program").WithMany().HasForeignKey("ProgramId")
				.OnDelete(DeleteBehavior.NoAction);
			b.HasOne("AxiomaReporting.Core.Entities.Sector", "Sector").WithMany().HasForeignKey("SectorId")
				.OnDelete(DeleteBehavior.NoAction);
			b.Navigation("District");
			b.Navigation("Inspector");
			b.Navigation("Program");
			b.Navigation("Sector");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Institution", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.District", "District").WithMany().HasForeignKey("DistrictId")
				.OnDelete(DeleteBehavior.SetNull);
			b.HasOne("AxiomaReporting.Core.Entities.EducationalStage", "EducationalStage").WithMany().HasForeignKey("EducationalStageId")
				.OnDelete(DeleteBehavior.SetNull);
			b.HasOne("AxiomaReporting.Core.Entities.Locality", "Locality").WithMany().HasForeignKey("LocalityId")
				.OnDelete(DeleteBehavior.SetNull);
			b.HasOne("AxiomaReporting.Core.Entities.Sector", "Sector").WithMany().HasForeignKey("SectorId")
				.OnDelete(DeleteBehavior.SetNull);
			b.HasOne("AxiomaReporting.Core.Entities.EducationType", "Type").WithMany().HasForeignKey("TypeId")
				.OnDelete(DeleteBehavior.SetNull);
			b.Navigation("District");
			b.Navigation("EducationalStage");
			b.Navigation("Locality");
			b.Navigation("Sector");
			b.Navigation("Type");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.PasswordHistory", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.User", "User").WithMany("PasswordHistories").HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.Navigation("User");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Report", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.User", "ApprovedByUser").WithMany().HasForeignKey("ApprovedBy")
				.OnDelete(DeleteBehavior.NoAction);
			b.HasOne("AxiomaReporting.Core.Entities.User", "RejectedByUser").WithMany().HasForeignKey("RejectedBy")
				.OnDelete(DeleteBehavior.NoAction);
			b.HasOne("AxiomaReporting.Core.Entities.ReportingMonth", "ReportingMonth").WithMany("Reports").HasForeignKey("ReportingMonthId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.ReportStatus", "Status").WithMany().HasForeignKey("StatusId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.User", "User").WithMany("Reports").HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("ApprovedByUser");
			b.Navigation("RejectedByUser");
			b.Navigation("ReportingMonth");
			b.Navigation("Status");
			b.Navigation("User");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.ReportRow", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.Allocation", "Allocation").WithMany().HasForeignKey("AllocationId")
				.OnDelete(DeleteBehavior.NoAction);
			b.HasOne("AxiomaReporting.Core.Entities.SchoolClass", "Class").WithMany().HasForeignKey("ClassId")
				.OnDelete(DeleteBehavior.NoAction)
				.HasConstraintName("FK_ReportRows_SchoolClasses_ClassId");
			b.HasOne("AxiomaReporting.Core.Entities.SchoolClass", "ConclusionClass").WithMany().HasForeignKey("ConclusionClassId")
				.OnDelete(DeleteBehavior.NoAction)
				.HasConstraintName("FK_ReportRows_SchoolClasses_ConclusionClassId");
			b.HasOne("AxiomaReporting.Core.Entities.DiscussionCode", "DiscussionCode").WithMany().HasForeignKey("DiscussionCodeId")
				.OnDelete(DeleteBehavior.NoAction);
			b.HasOne("AxiomaReporting.Core.Entities.District", "District").WithMany().HasForeignKey("DistrictId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Domain", "Domain").WithMany().HasForeignKey("DomainId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.EducationalProgram", "EducationalProgram").WithMany().HasForeignKey("EducationalProgramId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Framework", "Framework").WithMany().HasForeignKey("FrameworkId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.GradeLevel", "GradeLevel").WithMany().HasForeignKey("GradeLevelId")
				.OnDelete(DeleteBehavior.NoAction);
			b.HasOne("AxiomaReporting.Core.Entities.Locality", "Locality").WithMany().HasForeignKey("LocalityId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Report", "Report").WithMany("ReportRows").HasForeignKey("ReportId")
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.Subject", "Subject1").WithMany().HasForeignKey("Subject1Id")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired()
				.HasConstraintName("FK_ReportRows_Subjects_Subject1Id");
			b.HasOne("AxiomaReporting.Core.Entities.Subject", "Subject2").WithMany().HasForeignKey("Subject2Id")
				.OnDelete(DeleteBehavior.NoAction)
				.HasConstraintName("FK_ReportRows_Subjects_Subject2Id");
			b.Navigation("Allocation");
			b.Navigation("Class");
			b.Navigation("ConclusionClass");
			b.Navigation("DiscussionCode");
			b.Navigation("District");
			b.Navigation("Domain");
			b.Navigation("EducationalProgram");
			b.Navigation("Framework");
			b.Navigation("GradeLevel");
			b.Navigation("Locality");
			b.Navigation("Report");
			b.Navigation("Subject1");
			b.Navigation("Subject2");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.User", delegate(EntityTypeBuilder b)
		{
			b.HasOne("AxiomaReporting.Core.Entities.User", null).WithMany().HasForeignKey("CreatedBy")
				.OnDelete(DeleteBehavior.NoAction);
			b.HasOne("AxiomaReporting.Core.Entities.EmployeeRole", "Role").WithMany().HasForeignKey("RoleId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.UserStatus", "Status").WithMany().HasForeignKey("StatusId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.HasOne("AxiomaReporting.Core.Entities.User", null).WithMany().HasForeignKey("UpdatedBy")
				.OnDelete(DeleteBehavior.NoAction);
			b.HasOne("AxiomaReporting.Core.Entities.UserRole", "UserRole").WithMany().HasForeignKey("UserRoleId")
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();
			b.Navigation("Role");
			b.Navigation("Status");
			b.Navigation("UserRole");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Allocation", delegate(EntityTypeBuilder b)
		{
			b.Navigation("AllocationClasses");
			b.Navigation("AllocationDiscussionCodes");
			b.Navigation("AllocationDistricts");
			b.Navigation("AllocationDomains");
			b.Navigation("AllocationEducationalPrograms");
			b.Navigation("AllocationFrameworks");
			b.Navigation("AllocationGradeLevels");
			b.Navigation("AllocationLocalities");
			b.Navigation("AllocationLocalityDistrictNationals");
			b.Navigation("AllocationPrograms");
			b.Navigation("AllocationSectors");
			b.Navigation("AllocationSubjects");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.Report", delegate(EntityTypeBuilder b)
		{
			b.Navigation("ReportRows");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.ReportingMonth", delegate(EntityTypeBuilder b)
		{
			b.Navigation("Reports");
		});
		modelBuilder.Entity("AxiomaReporting.Core.Entities.User", delegate(EntityTypeBuilder b)
		{
			b.Navigation("Allocations");
			b.Navigation("PasswordHistories");
			b.Navigation("Reports");
		});
	}
}
