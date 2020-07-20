using MimeKit;

namespace KahanuMailer
{
    public abstract class SmtpClientBase : ISmtpClient
    {
        public abstract void Send(MimeMessage mailMessage);

        public virtual void SendAsync(MimeMessage mailMessage)
        {
            const string userState = "userState";
            SendAsync(mailMessage, userState);
        }

        public abstract void SendAsync(MimeMessage mailMessage, object userState);
        public abstract void Dispose();
    }
}
