using KahanuMailer;
using Models;

namespace ConsoleApp1.Mailers
{
    public interface IRegistrationMailer
    {
        EmailMessage Customer(RegistrationMailerContext context);
    }
}
