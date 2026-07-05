using Microsoft.EntityFrameworkCore.Migrations;

namespace AxiomaReporting.Infrastructure.Migrations;

public class AddDescriptionToDocumentAttachments : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		int? maxLength = 1000;
		migrationBuilder.AddColumn<string>("Description", "DocumentAttachments", "nvarchar(1000)", null, maxLength, rowVersion: false, null, nullable: true);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn("Description", "DocumentAttachments");
	}
}
