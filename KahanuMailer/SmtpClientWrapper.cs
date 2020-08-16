using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace KahanuMailer
{
    public class SmtpClientWrapper : SmtpClientBase
    {
        public SmtpClientWrapper(ISmtpConfiguration config)
        {
            InnerSmtpClient = new SmtpClient();
            InnerSmtpClient.Connected += InnerSmtpClient_Connected;
            InnerSmtpClient.Authenticated += InnerSmtpClient_Authenticated;

            if (!InnerSmtpClient.IsConnected)
                InnerSmtpClient.Connect(config.Server, config.Port);

            if (config.UseAuthentication)
            {
                if (!InnerSmtpClient.IsAuthenticated)
                    InnerSmtpClient.Authenticate(config.UserName, config.Password);
            }
        }

        private void InnerSmtpClient_Authenticated(object sender, MailKit.AuthenticatedEventArgs e)
        {
            Console.WriteLine("Server authenticated: {0}", e.Message);
        }

        private void InnerSmtpClient_Connected(object sender, MailKit.ConnectedEventArgs e)
        {
            Console.WriteLine("Server connected: {0} {1}", e.Host, e.Port);
        }


        public SmtpClient InnerSmtpClient { get; set; }

        public override void Send(MimeMessage mailMessage)
        {
            using (InnerSmtpClient)
            {
                InnerSmtpClient.Send(mailMessage);
            }
        }

        public override void SendAsync(MimeMessage mailMessage, object userState)
        {
            InnerSmtpClient.SendAsync(mailMessage);
        }

        public override void Dispose()
        {
            InnerSmtpClient.Dispose();
        }
    }
}