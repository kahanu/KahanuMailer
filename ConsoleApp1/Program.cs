using ConsoleApp1.Mailers;
using KahanuMailer;
using KahanuMailer.ServiceExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public IConfiguration Configuration { get; set; }
        static ServiceProvider _serviceProvider = null;

        static async Task Main(string[] args)
        {
            var config = ConfigurationSetup(args);
            RegisterServices(config);

            IServiceScope scope = _serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<Startup>().Run();

            DisposeServices();
        }

        static IConfiguration ConfigurationSetup(string[] args)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        static void RegisterServices(IConfiguration config)
        {
            var services = new ServiceCollection();
            //services.AddSingleton<ISmtpConfiguration>(config.GetSection("SmtpConfiguration").Get<SmtpConfiguration>());
            services.AddScoped<IRegistrationMailer, RegistrationMailer>();
            services.AddScoped<Startup>();

            services.AddKahanuMailer(config, options =>
            {
                options.UseConfig(db =>
                {
                    db.ConnectionStringName = "SampleConnection";
                    db.SmtpConfigTableName = "SmtpConfiguration";
                });
            });

            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
