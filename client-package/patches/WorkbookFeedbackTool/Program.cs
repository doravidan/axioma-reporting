using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

Console.OutputEncoding = Encoding.UTF8;

if (args.Length < 2)
{
    Console.Error.WriteLine("Usage: WorkbookFeedbackTool dump|set <xlsx-path> [row=value ...]");
    return 2;
}

var command = args[0];
var path = args[1];

if (command.Equals("dump", StringComparison.OrdinalIgnoreCase))
{
    Dump(path);
    return 0;
}

if (command.Equals("rows", StringComparison.OrdinalIgnoreCase))
{
    var rowIndexes = args.Skip(2)
        .SelectMany(arg => arg.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        .Where(arg => uint.TryParse(arg, out _))
        .Select(uint.Parse)
        .ToHashSet();
    DumpRows(path, rowIndexes);
    return 0;
}

if (command.Equals("set", StringComparison.OrdinalIgnoreCase))
{
    var updates = args.Skip(2)
        .Select(arg => arg.Split('=', 2))
        .Where(parts => parts.Length == 2 && int.TryParse(parts[0], out _))
        .ToDictionary(parts => int.Parse(parts[0]), parts => parts[1]);
    SetFixes(path, updates);
    return 0;
}

Console.Error.WriteLine($"Unknown command: {command}");
return 2;

static void Dump(string path)
{
    using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
    using var doc = SpreadsheetDocument.Open(stream, false);
    var workbookPart = doc.WorkbookPart ?? throw new InvalidOperationException("Missing workbook part.");
    var sharedStrings = LoadSharedStrings(workbookPart);

    foreach (var sheet in workbookPart.Workbook.Sheets!.Elements<Sheet>())
    {
        var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id!);
        var rows = worksheetPart.Worksheet.GetFirstChild<SheetData>()!.Elements<Row>().ToList();
        Console.WriteLine($"SHEET\t{sheet.Name}\tROWS\t{rows.Count}");
        foreach (var row in rows)
        {
            var cells = row.Elements<Cell>()
                .Select(cell => $"{cell.CellReference}:{ReadCell(cell, sharedStrings)}");
            Console.WriteLine($"ROW\t{row.RowIndex}\t{string.Join("\t", cells)}");
        }
    }
}

static void DumpRows(string path, HashSet<uint> rowIndexes)
{
    using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
    using var doc = SpreadsheetDocument.Open(stream, false);
    var workbookPart = doc.WorkbookPart ?? throw new InvalidOperationException("Missing workbook part.");
    var sharedStrings = LoadSharedStrings(workbookPart);

    foreach (var sheet in workbookPart.Workbook.Sheets!.Elements<Sheet>())
    {
        var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id!);
        var rows = worksheetPart.Worksheet.GetFirstChild<SheetData>()!.Elements<Row>()
            .Where(row => row.RowIndex != null && rowIndexes.Contains(row.RowIndex.Value));

        foreach (var row in rows)
        {
            var cells = row.Elements<Cell>()
                .Select(cell => $"{cell.CellReference}:{ReadCell(cell, sharedStrings)}");
            Console.WriteLine($"SHEET\t{sheet.Name}\tROW\t{row.RowIndex}\t{string.Join("\t", cells)}");
        }
    }
}

static void SetFixes(string path, Dictionary<int, string> updates)
{
    using var doc = SpreadsheetDocument.Open(path, true);
    var workbookPart = doc.WorkbookPart ?? throw new InvalidOperationException("Missing workbook part.");
    var firstSheet = workbookPart.Workbook.Sheets!.Elements<Sheet>().First();
    var worksheetPart = (WorksheetPart)workbookPart.GetPartById(firstSheet.Id!);
    var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>()!;

    EnsureCell(sheetData, 1, "G").CellValue = new CellValue("מה תוקן");
    EnsureCell(sheetData, 1, "G").DataType = CellValues.String;

    var rowOne = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == 1);
    rowOne?.Elements<Cell>().FirstOrDefault(c => c.CellReference == "G1")?.Remove();

    var header = EnsureCell(sheetData, 2, "G");
    header.CellValue = new CellValue("\u05de\u05d4 \u05ea\u05d5\u05e7\u05df");
    header.DataType = CellValues.String;

    foreach (var (rowIndex, text) in updates)
    {
        var cell = EnsureCell(sheetData, (uint)rowIndex, "G");
        cell.CellValue = new CellValue(text);
        cell.DataType = CellValues.String;
    }

    worksheetPart.Worksheet.Save();
}

static List<string> LoadSharedStrings(WorkbookPart workbookPart)
{
    var table = workbookPart.SharedStringTablePart?.SharedStringTable;
    return table == null
        ? new List<string>()
        : table.Elements<SharedStringItem>().Select(item => item.InnerText).ToList();
}

static string ReadCell(Cell cell, List<string> sharedStrings)
{
    if (cell.CellValue == null && cell.InlineString == null)
    {
        return string.Empty;
    }

    if (cell.DataType?.Value == CellValues.SharedString)
    {
        var index = int.Parse(cell.CellValue!.Text);
        return index >= 0 && index < sharedStrings.Count ? sharedStrings[index] : string.Empty;
    }

    if (cell.DataType?.Value == CellValues.InlineString)
    {
        return cell.InlineString?.InnerText ?? cell.InnerText;
    }

    return cell.CellValue?.Text ?? cell.InnerText;
}

static Cell EnsureCell(SheetData sheetData, uint rowIndex, string columnName)
{
    var row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
    if (row == null)
    {
        row = new Row { RowIndex = rowIndex };
        sheetData.Append(row);
    }

    var cellReference = columnName + rowIndex;
    var existing = row.Elements<Cell>().FirstOrDefault(c => c.CellReference == cellReference);
    if (existing != null)
    {
        return existing;
    }

    var newCell = new Cell { CellReference = cellReference };
    var nextCell = row.Elements<Cell>().FirstOrDefault(c => string.Compare(c.CellReference?.Value, cellReference, StringComparison.OrdinalIgnoreCase) > 0);
    if (nextCell == null)
    {
        row.Append(newCell);
    }
    else
    {
        row.InsertBefore(newCell, nextCell);
    }

    return newCell;
}
