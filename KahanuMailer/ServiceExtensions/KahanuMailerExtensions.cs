using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KahanuMailer.ServiceExtensions
{
    public static class KahanuMailerExtensions
    {
        /// <summary>
        /// Add Smtp Configuration to the Kahanu Mailer.
        /// </summary>
        /// <param name="services">The Service Collection</param>
        /// <param name="config">The global appSettings configuration.</param>
        /// <param name="options">MailerOptions</param>
        /// <returns>ServiceCollection</returns>
        public static IServiceCollection AddKahanuMailer(this IServiceCollection services, IConfiguration config, Action<MailerOptions> options)
        {
            var opts = new MailerOptions(services, config);
            options(opts);

            return services;
        }
    }
}
