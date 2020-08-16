namespace KahanuMailer
{
    public interface ISmtpConfiguration
    {
        string Password { get; set; }
        int Port { get; set; }
        string Server { get; set; }
        string UserName { get; set; }
        bool UseAuthentication { get; set; }
    }
}
