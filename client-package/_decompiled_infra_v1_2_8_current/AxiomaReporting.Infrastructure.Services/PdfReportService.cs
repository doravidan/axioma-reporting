using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AxiomaReporting.Infrastructure.Services;

public class PdfReportService : IPdfReportService
{
	private sealed record ErrorRow(string RowNumber, string Message);

	private static readonly Regex RowRefRegex = new Regex("^\\s*(?:שורה|Row)\\s+(\\d+)\\s*[:\\-–]\\s*(.*)$", RegexOptions.Compiled);

	private static bool _fontRegistered;

	private static bool _fontAvailable;

	private static readonly object _fontLock = new object();

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
		List<ErrorRow> rows = (errors ?? Array.Empty<string>()).Where((string e) => !string.IsNullOrWhiteSpace(e)).Select(ParseRow).ToList();
		if (rows.Count == 0)
		{
			rows.Add(new ErrorRow(string.Empty, "אין שגיאות."));
		}
		string fontFamily = (_fontAvailable ? "Noto Sans Hebrew" : "Calibri");
		string generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
		return Document.Create(delegate(IDocumentContainer container)
		{
			container.Page(delegate(PageDescriptor page)
			{
				page.Size(PageSizes.A4);
				page.Margin(30f);
				page.ContentFromRightToLeft();
				page.DefaultTextStyle((TextStyle x) => x.FontFamily(fontFamily).FontSize(11f));
				page.Header().Row(delegate(RowDescriptor row)
				{
					row.RelativeItem().AlignRight().Column(delegate(ColumnDescriptor col)
					{
						col.Item().Text("מערכת דיווח עובדים אקסיומא").SemiBold()
							.FontSize(13f);
						col.Item().Text("הופק: " + generatedAt).FontSize(9f)
							.FontColor(Colors.Grey.Darken1);
					});
				});
				page.Content().PaddingVertical(10f).Column(delegate(ColumnDescriptor col)
				{
					col.Spacing(10f);
					col.Item().AlignRight().Text("דוח שגיאות בהעלאת אקסל")
						.FontSize(16f)
						.SemiBold();
					col.Item().Table(delegate(TableDescriptor table)
					{
						table.ColumnsDefinition(delegate(TableColumnsDefinitionDescriptor c)
						{
							c.ConstantColumn(80f);
							c.RelativeColumn();
						});
						table.Header(delegate(TableCellDescriptor h)
						{
							h.Cell().Background(Colors.Grey.Lighten2).Padding(5f)
								.AlignRight()
								.Text("שורה בקובץ")
								.SemiBold();
							h.Cell().Background(Colors.Grey.Lighten2).Padding(5f)
								.AlignRight()
								.Text("הודעת שגיאה")
								.SemiBold();
						});
						for (int i = 0; i < rows.Count; i++)
						{
							ErrorRow errorRow = rows[i];
							Color color = ((i % 2 == 0) ? Colors.White : Colors.Grey.Lighten4);
							table.Cell().Background(color).Padding(5f)
								.AlignRight()
								.Text(errorRow.RowNumber);
							table.Cell().Background(color).Padding(5f)
								.AlignRight()
								.Text(errorRow.Message);
						}
					});
				});
				page.Footer().AlignRight().Text(delegate(TextDescriptor text)
				{
					text.Span("עמוד ");
					text.CurrentPageNumber();
					text.Span(" מתוך ");
					text.TotalPages();
				});
			});
		}).GeneratePdf();
	}

	private static ErrorRow ParseRow(string raw)
	{
		Match match = RowRefRegex.Match(raw);
		if (match.Success)
		{
			return new ErrorRow(match.Groups[1].Value, match.Groups[2].Value.Trim());
		}
		return new ErrorRow(string.Empty, raw.Trim());
	}

	private void EnsureFontRegistered()
	{
		if (_fontRegistered)
		{
			return;
		}
		lock (_fontLock)
		{
			if (_fontRegistered)
			{
				return;
			}
			List<string> list = new List<string>();
			if (!string.IsNullOrWhiteSpace(_fontPath))
			{
				list.Add(_fontPath);
			}
			list.Add(Path.Combine(AppContext.BaseDirectory, "wwwroot", "fonts", "NotoSansHebrew-Regular.ttf"));
			list.Add(Path.Combine(AppContext.BaseDirectory, "fonts", "NotoSansHebrew-Regular.ttf"));
			foreach (string item in list)
			{
				if (string.IsNullOrWhiteSpace(item) || !File.Exists(item))
				{
					continue;
				}
				try
				{
					using FileStream stream = File.OpenRead(item);
					FontManager.RegisterFont(stream);
					_fontAvailable = true;
				}
				catch (Exception exception)
				{
					_logger?.LogWarning(exception, "Failed to register Hebrew font from {Path}", item);
					continue;
				}
				break;
			}
			if (!_fontAvailable)
			{
				_logger?.LogWarning("Hebrew font not found; PDF error reports will fall back to default QuestPDF font. Expected file: wwwroot/fonts/NotoSansHebrew-Regular.ttf");
			}
			_fontRegistered = true;
		}
	}
}
