using AxiomaReporting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AxiomaReporting.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260510143000_AllowMultipleAllocationsPerEmployeeProject")]
    public partial class AllowMultipleAllocationsPerEmployeeProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Allocations_UserId_ProjectId",
                table: "Allocations");

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_UserId_ProjectId",
                table: "Allocations",
                columns: new[] { "UserId", "ProjectId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Allocations_UserId_ProjectId",
                table: "Allocations");

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_UserId_ProjectId",
                table: "Allocations",
                columns: new[] { "UserId", "ProjectId" },
                unique: true);
        }
    }
}
