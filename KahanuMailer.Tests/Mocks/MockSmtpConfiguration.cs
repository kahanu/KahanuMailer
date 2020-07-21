using System;

namespace KahanuMailer.Tests.Mocks
{
    public class MockSmtpConfiguration : ISmtpConfiguration
    {
        public string SmtpPassword { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SmtpPort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SmtpServer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SmtpUsername { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseAuthentication { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
