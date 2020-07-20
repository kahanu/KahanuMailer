using MimeKit;
using System.Threading.Tasks;

namespace KahanuMailer
{
    public class EmailMessage : MimeMessage
    {
        #region ctors
        private readonly ISmtpConfiguration configuration;
        public EmailMessage(ISmtpConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The name of the HTML view for this mailer, by convention it will match this method name.
        /// </summary>
        public string ViewName { get; set; }

        #endregion

        #region Public Methods

        public virtual void Send(ISmtpClient smtpClient = null)
        {
            smtpClient = smtpClient ?? GetSmtpClient();
            using (smtpClient)
            {
                smtpClient.Send(this);
            }
        }

        public virtual async Task SendAsync(object userState = null, ISmtpClient smtpClient = null)
        {
            await Task.Run(() =>
            {
                smtpClient = smtpClient ?? GetSmtpClient();
                smtpClient.SendAsync(this, userState);
            });
        }

        public virtual ISmtpClient GetSmtpClient()
        {
            return new SmtpClientWrapper(configuration);
        }
        #endregion

    }
}
