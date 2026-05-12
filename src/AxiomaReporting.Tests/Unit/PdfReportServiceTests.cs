using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;
using QuestPDF.Infrastructure;
using System.Text;

namespace AxiomaReporting.Tests.Unit;

public class PdfReportServiceTests
{
  public PdfReportServiceTests()
  {
    // QuestPDF requires a license to be set before generating documents.
    QuestPDF.Settings.License = LicenseType.Community;
  }

  [Fact]
  public void CreateErrorReport_ReturnsPdfBytes()
  {
    var sut = new PdfReportService();

    var bytes = sut.CreateErrorReport(new[] { "Row 2: missing district", "Row 3: bad duration" });

    bytes.Should().NotBeEmpty();
    bytes.Take(5).Should().Equal(Encoding.UTF8.GetBytes("%PDF-"));
  }

  [Fact]
  public void CreateErrorReport_WithHebrewMessages_ProducesValidPdf()
  {
    var sut = new PdfReportService();

    var errors = new[]
    {
      "שורה 5: חסר שדה מחוז",
      "שורה 7: משך תפוקה חורג מהיקף ההעסקה היומי",
      "הודעת שגיאה כללית ללא מספר שורה"
    };

    var bytes = sut.CreateErrorReport(errors);

    bytes.Should().NotBeEmpty();
    bytes.Take(5).Should().Equal(Encoding.UTF8.GetBytes("%PDF-"));
    bytes.Length.Should().BeGreaterThan(500, "a real PDF with header/table/footer is more than a stub");
  }

  [Fact]
  public void CreateErrorReport_EmptyList_StillProducesPdf()
  {
    var sut = new PdfReportService();

    var bytes = sut.CreateErrorReport(Array.Empty<string>());

    bytes.Should().NotBeEmpty();
    bytes.Take(5).Should().Equal(Encoding.UTF8.GetBytes("%PDF-"));
  }
}
