using KahanuMailer;
using Models;

namespace ConsoleApp1.Mailers
{

    public class RegistrationMailer : MailerBase<RegistrationMailerContext>, IRegistrationMailer
    {
        #region ctors

        public RegistrationMailer(ISmtpConfiguration configuration) : base(configuration)
        {
            LayoutName = "_Layout";
        }

        #endregion

        #region Public Methods

        public EmailMessage Customer(RegistrationMailerContext context)
        {
            return Populate(m =>
            {
                m.ViewName = "customer";
                m.Subject = context.Subject;
                m.To.AddRange(context.ToAddresses);
                m.Body = BuildBody(context);
                m.From.AddRange(context.From);
            });
        }

        #endregion
    }

}
