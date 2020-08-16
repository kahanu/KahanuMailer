namespace KahanuMailer
{
    public class SmtpConfiguration : ISmtpConfiguration
    {
        public const string Key = "SmtpConfiguration";

        public string Server { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool UseAuthentication { get; set; }
    }
}
