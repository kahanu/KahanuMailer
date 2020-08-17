using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace KahanuMailer.ServiceExtensions
{
    public class MailerOptions
    {
        private readonly IServiceCollection services;
        private readonly IConfiguration config;

        public MailerOptions(IServiceCollection services, IConfiguration config)
        {
            this.services = services;
            this.config = config;
        }

        /// <summary>
        /// Use the appSettings to get the Smtp configuration.
        /// </summary>
        public void UseConfig()
        {
            services.TryAddSingleton<ISmtpConfiguration>(config.GetSection("SmtpConfiguration").Get<SmtpConfiguration>());
        }

        /// <summary>
        /// Use the database to get the Smtp configuration.
        /// </summary>
        /// <param name="options"></param>
        public void UseConfig(Action<DbOptions> options)
        {
            var dbOptions = new DbOptions();
            options(dbOptions);

            var connString = config.GetConnectionString(dbOptions.ConnectionStringName);
            ISmtpConfiguration smtpConfig = new SmtpConfiguration();

            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    var sqlTemplate = "select top 1 * from {0}";
                    var sql = string.Format(sqlTemplate, dbOptions.SmtpConfigTableName);
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                smtpConfig.Server = reader["server"].ToString();
                                smtpConfig.Port = Convert.ToInt32(reader["port"]);
                                smtpConfig.UseAuthentication = Convert.ToBoolean(reader["UseAuthentication"]);
                                if (smtpConfig.UseAuthentication)
                                {
                                    smtpConfig.UserName = reader["username"].ToString();
                                    smtpConfig.Password = reader["password"].ToString();
                                }
                            }
                        }
                    }
                }

                services.TryAddSingleton(smtpConfig);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
