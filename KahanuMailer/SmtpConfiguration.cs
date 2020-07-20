namespace KahanuMailer
{
    public class SmtpConfiguration : ISmtpConfiguration
    {
        public const string Key = "SmtpConfiguration";

        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public bool UseAuthentication { get; set; }
    }
}
