using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AxiomaReporting.Core.Interfaces;

public interface ISmtpSender
{
	Task SendRenderedAsync(string toEmail, string toName, string subject, string bodyHtml, IReadOnlyList<EmailAttachment>? attachments = null, CancellationToken cancellationToken = default(CancellationToken));
}
