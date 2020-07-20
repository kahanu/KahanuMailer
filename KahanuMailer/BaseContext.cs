using MimeKit;
using System.Collections.Generic;

namespace KahanuMailer
{
    public class BaseContext
    {
        public string Subject { get; set; }
        public List<MailboxAddress> ToAddresses { get; set; } = new List<MailboxAddress>();
        public List<MailboxAddress> From { get; set; } = new List<MailboxAddress>
        {
            MailboxAddress.Parse("no-reply@info.org")
        };
        public List<MimeEntity> AttachmentCollection { get; set; } = new List<MimeEntity>();

    }
}
