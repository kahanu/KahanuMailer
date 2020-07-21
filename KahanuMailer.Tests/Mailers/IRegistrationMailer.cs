using Models;

namespace KahanuMailer.Tests.Mailers
{
    public interface IRegistrationMailer
    {
        EmailMessage Customer(RegistrationMailerContext context);
    }
}