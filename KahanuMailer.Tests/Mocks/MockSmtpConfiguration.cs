using System;

namespace KahanuMailer.Tests.Mocks
{
    public class MockSmtpConfiguration : ISmtpConfiguration
    {
        public string Password { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Port { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Server { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseAuthentication { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
