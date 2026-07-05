using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AxiomaReporting.Infrastructure.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260510143000_AllowMultipleAllocationsPerEmployeeProject")]
public class AllowMultipleAllocationsPerEmployeeProject : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex("IX_Allocations_UserId_ProjectId", "Allocations");
		migrationBuilder.CreateIndex("IX_Allocations_UserId_ProjectId", "Allocations", new string[2] { "UserId", "ProjectId" });
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex("IX_Allocations_UserId_ProjectId", "Allocations");
		migrationBuilder.CreateIndex("IX_Allocations_UserId_ProjectId", "Allocations", new string[2] { "UserId", "ProjectId" }, null, unique: true);
	}
}
