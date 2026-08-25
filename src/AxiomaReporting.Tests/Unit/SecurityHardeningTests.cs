using System.IO.Compression;
using System.Text;
using AxiomaReporting.Web.Security;
using FluentAssertions;

namespace AxiomaReporting.Tests.Unit;

public class SecurityHardeningTests
{
  [Fact]
  public async Task AttachmentValidation_UsesFileSignatureInsteadOfExtensionAlone()
  {
    await using var validPdf = new MemoryStream(Encoding.UTF8.GetBytes("%PDF-1.7\ncontent"));
    (await AttachmentFileSecurity.ValidateAsync(validPdf, ".pdf")).IsValid.Should().BeTrue();

    await using var disguisedExecutable = new MemoryStream(Encoding.UTF8.GetBytes("MZ executable"));
    (await AttachmentFileSecurity.ValidateAsync(disguisedExecutable, ".pdf")).IsValid.Should().BeFalse();
  }

  [Fact]
  public async Task AttachmentValidation_RequiresExpectedOpenXmlPackagePart()
  {
    await using var stream = new MemoryStream();
    using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen: true))
    {
      archive.CreateEntry("[Content_Types].xml");
      archive.CreateEntry("xl/workbook.xml");
    }

    stream.Position = 0;
    (await AttachmentFileSecurity.ValidateAsync(stream, ".xlsx")).IsValid.Should().BeTrue();
    stream.Position = 0;
    (await AttachmentFileSecurity.ValidateAsync(stream, ".docx")).IsValid.Should().BeFalse();
  }

  [Fact]
  public void StoredPathResolver_AllowsKnownLegacyAndPrivateRootsButRejectsTraversal()
  {
    var root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));

    AttachmentFileSecurity.ResolveStoredPath(root, "/uploads/attachments/file.pdf")
      .Should().NotBeNull();
    AttachmentFileSecurity.ResolveStoredPath(root, "/uploads/private/report-attachments/file.pdf")
      .Should().NotBeNull();
    AttachmentFileSecurity.ResolveStoredPath(root, "/uploads/attachments/../../appsettings.json")
      .Should().BeNull();
    AttachmentFileSecurity.ResolveStoredPath(root, "/uploads/branding/site-logo.svg")
      .Should().BeNull();
  }

  [Fact]
  public void HtmlSanitizer_RemovesExecutableMarkupAndPreservesFormatting()
  {
    var sut = new HtmlContentSanitizer();

    var result = sut.Sanitize("<p class='notice' onclick='alert(1)'>Safe <strong>text</strong></p><script>alert(2)</script>");

    result.Should().Contain("<p").And.Contain("<strong>text</strong>");
    result.Should().NotContain("onclick").And.NotContain("<script").And.NotContain("alert(");
  }

  [Fact]
  public void RequestLimiter_StopsAtLimitAndCanResetSubject()
  {
    var sut = new SecurityRequestLimiter();

    sut.TryAcquire("login", "user", 2, TimeSpan.FromMinutes(1)).Should().BeTrue();
    sut.TryAcquire("login", "user", 2, TimeSpan.FromMinutes(1)).Should().BeTrue();
    sut.TryAcquire("login", "user", 2, TimeSpan.FromMinutes(1)).Should().BeFalse();
    sut.TryAcquire("login", "another-user", 2, TimeSpan.FromMinutes(1)).Should().BeTrue();

    sut.Reset("login", "user");
    sut.TryAcquire("login", "user", 2, TimeSpan.FromMinutes(1)).Should().BeTrue();
  }
}
