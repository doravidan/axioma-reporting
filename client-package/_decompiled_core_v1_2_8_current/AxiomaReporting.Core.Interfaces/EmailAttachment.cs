namespace AxiomaReporting.Core.Interfaces;

public sealed record EmailAttachment(string FileName, byte[] Content, string MimeType);
