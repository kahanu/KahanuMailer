namespace KahanuMailer
{
    public interface ISmtpConfiguration
    {
        string SmtpPassword { get; set; }
        int SmtpPort { get; set; }
        string SmtpServer { get; set; }
        string SmtpUsername { get; set; }
        bool UseAuthentication { get; set; }
    }
}
