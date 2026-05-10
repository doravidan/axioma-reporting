using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AxiomaReporting.Infrastructure.Services;

public interface IPdfReportService
{
  byte[] CreateErrorReport(IEnumerable<string> errors);
}

public class PdfReportService : IPdfReportService
{
  private static readonly Regex RowRefRegex =
    new(@"^\s*(?:שורה|Row)\s+(\d+)\s*[:\-–]\s*(.*)$", RegexOptions.Compiled);

  private static bool _fontRegistered;
  private static bool _fontAvailable;
  private static readonly object _fontLock = new();

  private readonly ILogger<PdfReportService>? _logger;
  private readonly string? _fontPath;

  public PdfReportService()
  {
  }

  public PdfReportService(ILogger<PdfReportService> logger, PdfFontOptions? options = null)
  {
    _logger = logger;
    _fontPath = options?.HebrewFontPath;
  }

  public byte[] CreateErrorReport(IEnumerable<string> errors)
  {
    EnsureFontRegistered();

    var rows = (errors ?? Array.Empty<string>())
      .Where(e => !string.IsNullOrWhiteSpace(e))
      .Select(ParseRow)
      .ToList();

    if (rows.Count == 0)
    {
      rows.Add(new ErrorRow(string.Empty, "אין שגיאות."));
    }

    var fontFamily = _fontAvailable ? "Noto Sans Hebrew" : Fonts.Calibri;
    var generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

    var document = Document.Create(container =>
    {
      container.Page(page =>
      {
        page.Size(PageSizes.A4);
        page.Margin(30);
        page.ContentFromRightToLeft();
        page.DefaultTextStyle(x => x.FontFamily(fontFamily).FontSize(11));

        page.Header().Row(row =>
        {
          row.RelativeItem().AlignRight().Column(col =>
          {
            col.Item().Text("מערכת דיווח עובדים אקסיומא")
              .SemiBold().FontSize(13);
            col.Item().Text($"הופק: {generatedAt}").FontSize(9).FontColor(Colors.Grey.Darken1);
          });
        });

        page.Content().PaddingVertical(10).Column(col =>
        {
          col.Spacing(10);
          col.Item().AlignRight().Text("דוח שגיאות בהעלאת אקסל")
            .FontSize(16).SemiBold();

          col.Item().Table(table =>
          {
            table.ColumnsDefinition(c =>
            {
              c.ConstantColumn(80);
              c.RelativeColumn();
            });

            table.Header(h =>
            {
              h.Cell().Background(Colors.Grey.Lighten2).Padding(5)
                .AlignRight().Text("שורה בקובץ").SemiBold();
              h.Cell().Background(Colors.Grey.Lighten2).Padding(5)
                .AlignRight().Text("הודעת שגיאה").SemiBold();
            });

            for (var i = 0; i < rows.Count; i++)
            {
              var r = rows[i];
              var bg = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten4;
              table.Cell().Background(bg).Padding(5).AlignRight().Text(r.RowNumber);
              table.Cell().Background(bg).Padding(5).AlignRight().Text(r.Message);
            }
          });
        });

        page.Footer().AlignRight().Text(text =>
        {
          text.Span("עמוד ");
          text.CurrentPageNumber();
          text.Span(" מתוך ");
          text.TotalPages();
        });
      });
    });

    return document.GeneratePdf();
  }

  private static ErrorRow ParseRow(string raw)
  {
    var match = RowRefRegex.Match(raw);
    if (match.Success)
    {
      return new ErrorRow(match.Groups[1].Value, match.Groups[2].Value.Trim());
    }
    return new ErrorRow(string.Empty, raw.Trim());
  }

  private void EnsureFontRegistered()
  {
    if (_fontRegistered) return;
    lock (_fontLock)
    {
      if (_fontRegistered) return;
      var candidatePaths = new List<string>();
      if (!string.IsNullOrWhiteSpace(_fontPath)) candidatePaths.Add(_fontPath!);
      candidatePaths.Add(Path.Combine(AppContext.BaseDirectory, "wwwroot", "fonts", "NotoSansHebrew-Regular.ttf"));
      candidatePaths.Add(Path.Combine(AppContext.BaseDirectory, "fonts", "NotoSansHebrew-Regular.ttf"));

      foreach (var path in candidatePaths)
      {
        if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
        {
          try
          {
            using var stream = File.OpenRead(path);
            FontManager.RegisterFont(stream);
            _fontAvailable = true;
            break;
          }
          catch (Exception ex)
          {
            _logger?.LogWarning(ex, "Failed to register Hebrew font from {Path}", path);
          }
        }
      }

      if (!_fontAvailable)
      {
        _logger?.LogWarning(
          "Hebrew font not found; PDF error reports will fall back to default QuestPDF font. Expected file: wwwroot/fonts/NotoSansHebrew-Regular.ttf");
      }
      _fontRegistered = true;
    }
  }

  private sealed record ErrorRow(string RowNumber, string Message);
}

public class PdfFontOptions
{
  public string? HebrewFontPath { get; set; }
}
