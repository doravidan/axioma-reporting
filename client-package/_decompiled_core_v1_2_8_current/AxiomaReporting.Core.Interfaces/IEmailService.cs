using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AxiomaReporting.Core.Interfaces;

public interface IEmailService
{
	Task SendAsync(string toEmail, string toName, string templateType, Dictionary<string, string> tokens, IReadOnlyList<EmailAttachment>? attachments = null, CancellationToken cancellationToken = default(CancellationToken));
}
