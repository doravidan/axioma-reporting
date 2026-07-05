using System.Text;
using ClosedXML.Excel;

Console.OutputEncoding = Encoding.UTF8;

if (args.Length == 0 || !Directory.Exists(args[0]))
{
    Console.Error.WriteLine("Usage: FwWorkbookInspector <folder>");
    return 2;
}

var files = Directory.EnumerateFiles(args[0], "*.xlsx", SearchOption.TopDirectoryOnly)
    .OrderBy(Path.GetFileName, StringComparer.CurrentCulture)
    .ToList();

foreach (var file in files)
{
    Console.WriteLine($"FILE\t{Path.GetFileName(file)}\tBYTES\t{new FileInfo(file).Length}");
    try
    {
        using var workbook = new XLWorkbook(file);
        foreach (var ws in workbook.Worksheets)
        {
            var range = ws.RangeUsed();
            if (range == null)
            {
                Console.WriteLine($"SHEET\t{ws.Name}\tEMPTY");
                continue;
            }

            var firstRow = range.FirstRow().RowNumber();
            var lastRow = range.LastRow().RowNumber();
            var firstCol = range.FirstColumn().ColumnNumber();
            var lastCol = range.LastColumn().ColumnNumber();
            var headerRow = DetectHeaderRow(ws, firstRow, lastRow, firstCol, lastCol);
            var headers = GetCells(ws, headerRow, firstCol, lastCol);
            var dataRowCount = CountDataRows(ws, headerRow + 1, lastRow, firstCol, lastCol);

            Console.WriteLine($"SHEET\t{ws.Name}\tROWS\t{lastRow - firstRow + 1}\tCOLS\t{lastCol - firstCol + 1}\tHEADER_ROW\t{headerRow}\tDATA_ROWS\t{dataRowCount}");
            Console.WriteLine($"HEADERS\t{string.Join("\t", headers.Select(NormalizeForTsv))}");

            var samples = 0;
            for (var row = headerRow + 1; row <= lastRow && samples < 5; row++)
            {
                var values = GetCells(ws, row, firstCol, lastCol);
                if (values.All(string.IsNullOrWhiteSpace))
                {
                    continue;
                }
                Console.WriteLine($"SAMPLE\t{row}\t{string.Join("\t", values.Select(NormalizeForTsv))}");
                samples++;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR\t{ex.GetType().Name}\t{NormalizeForTsv(ex.Message)}");
    }
}

return 0;

static int DetectHeaderRow(IXLWorksheet ws, int firstRow, int lastRow, int firstCol, int lastCol)
{
    var bestRow = firstRow;
    var bestScore = -1;
    var maxInspectRow = Math.Min(lastRow, firstRow + 20);
    for (var row = firstRow; row <= maxInspectRow; row++)
    {
        var cells = GetCells(ws, row, firstCol, lastCol);
        var nonEmpty = cells.Count(v => !string.IsNullOrWhiteSpace(v));
        var textLike = cells.Count(v => !string.IsNullOrWhiteSpace(v) && v.Any(ch => char.IsLetter(ch) || ch >= 0x0590 && ch <= 0x05FF));
        var unique = cells.Where(v => !string.IsNullOrWhiteSpace(v)).Distinct(StringComparer.OrdinalIgnoreCase).Count();
        var score = nonEmpty * 2 + textLike + unique;
        if (score > bestScore)
        {
            bestScore = score;
            bestRow = row;
        }
    }
    return bestRow;
}

static int CountDataRows(IXLWorksheet ws, int firstRow, int lastRow, int firstCol, int lastCol)
{
    var count = 0;
    for (var row = firstRow; row <= lastRow; row++)
    {
        if (GetCells(ws, row, firstCol, lastCol).Any(v => !string.IsNullOrWhiteSpace(v)))
        {
            count++;
        }
    }
    return count;
}

static List<string> GetCells(IXLWorksheet ws, int row, int firstCol, int lastCol)
{
    var values = new List<string>();
    for (var col = firstCol; col <= lastCol; col++)
    {
        values.Add(ReadCell(ws.Cell(row, col)));
    }
    return values;
}

static string ReadCell(IXLCell cell)
{
    if (cell.IsEmpty())
    {
        return string.Empty;
    }
    if (cell.DataType == XLDataType.DateTime && cell.TryGetValue<DateTime>(out var date))
    {
        return date.ToString("yyyy-MM-dd");
    }
    if (cell.DataType == XLDataType.Number && cell.TryGetValue<double>(out var number))
    {
        return number.ToString("0.################", System.Globalization.CultureInfo.InvariantCulture);
    }
    return cell.GetFormattedString().Trim();
}

static string NormalizeForTsv(string value)
{
    return (value ?? string.Empty).Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Trim();
}
