using ClosedXML.Excel;

string output = args.FirstOrDefault() ?? Path.GetFullPath("postybell-valid-import.xlsx");

using var workbook = new XLWorkbook();
var ws = workbook.Worksheets.Add("Import");

string[] headers =
{
	"MeetingDate",
	"MeetingDuration",
	"DistrictId",
	"LocalityId",
	"FrameworkId",
	"EducationalProgramId",
	"DomainId",
	"Subject1Id",
	"Subject2Id",
	"DiscussionCodeId",
	"ConclusionClassId",
	"ConclusionFrameworkId",
	"ConclusionLocationId",
	"GradeLevelId",
	"ClassId",
	"Notes"
};

for (int i = 0; i < headers.Length; i++)
{
	ws.Cell(1, i + 1).Value = headers[i];
}

ws.Cell(2, 1).Value = new DateTime(2026, 1, 15);
ws.Cell(2, 1).Style.DateFormat.Format = "dd/MM/yyyy";
ws.Cell(2, 2).Value = 1m;
ws.Cell(2, 3).Value = 1;
ws.Cell(2, 4).Value = 1;
ws.Cell(2, 5).Value = 1;
ws.Cell(2, 6).Value = 1;
ws.Cell(2, 7).Value = 1;
ws.Cell(2, 8).Value = 1;
ws.Cell(2, 16).Value = "Playwright import smoke test";

workbook.SaveAs(output);
Console.WriteLine(output);
