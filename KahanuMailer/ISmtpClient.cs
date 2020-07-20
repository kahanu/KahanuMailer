using MimeKit;
using System;

namespace KahanuMailer
{
    public interface ISmtpClient : IDisposable
    {
        void Send(MimeMessage mailMessage);
        void SendAsync(MimeMessage mailMessage);
        void SendAsync(MimeMessage mailMessage, object userState);
    }
}
