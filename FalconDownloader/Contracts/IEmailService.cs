using Microsoft.Exchange.WebServices.Data;
using System.Collections.Generic;
using System.Threading;

namespace FalconDownloader.Contracts
{
    public interface IEmailService
    {
        IResult<IEnumerable<EmailMessage>> FindUnreadEmails();
        IResult<IEnumerable<EmailMessage>> DownloadEmails(IEnumerable<EmailMessage> messages, CancellationToken token);
        IResult<bool> MarkAsRead(EmailMessage msg);
        IResult<bool> ForwardEmail(EmailMessage msg);
        IResult<bool> ForwardEmailToAdmin(EmailMessage msg, string error);
    }
}
