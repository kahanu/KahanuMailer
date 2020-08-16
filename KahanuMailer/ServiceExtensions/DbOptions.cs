namespace KahanuMailer.ServiceExtensions
{
    public class DbOptions
    {
        public string ConnectionStringName { get; set; }
        public string SmtpConfigTableName { get; set; } = "SmtpConfiguration";
    }
}
