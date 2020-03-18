using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Topshelf;
using Topshelf.Logging;
using WSP.Service;

namespace WSP.App
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        private static readonly LogWriter logger = HostLogger.Get("root");

        static void Main(string[] args)
        {
            ConfigureDependecies();
            ConfigureService();

            Console.WriteLine("Hello World!");
        }

        private static IServiceCollection ConfigureDependecies()
        {
            IServiceCollection services = new ServiceCollection();
            
            var config = LoadConfiguration();
            services.AddSingleton(config);

            //services.AddTransient<ITestService, TestService>();
            // IMPORTANT! Register our application entry point
            //services.AddTransient<SampleService>();

            serviceProvider = services.BuildServiceProvider();
            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true,
                             reloadOnChange: true);
            return builder.Build();
        }

        public static void ConfigureService()
        {
            HostFactory.Run(x =>
            {

                //pass command line arg "-resetstats:true" for clearing stats counters
                //x.AddCommandLineDefinition("resetstats", v => resetStats = bool.Parse(v));
                x.ApplyCommandLine();

                x.Service<SampleService>(s =>
                {
                    s.ConstructUsing(name => new SampleService(serviceProvider));
                    s.WhenStarted((tc, hostControl) => tc.Start(hostControl));
                    s.WhenStopped((tc, hostControl) => tc.Stop(hostControl));
                });
                x.RunAsLocalSystem();

                x.SetInstanceName("Sample Service");
                x.SetDescription("Sample Service");
                x.SetDisplayName("Sample Service");
                x.SetServiceName("Sample Service");

                x.UseLog4Net("log4net.config");
                x.OnException(ex =>
                {
                    //HostLogger.Get<TransmissionService>().Error("Service Error", ex);
                    logger.Error("Service Error", ex);
                });

            });
        }
    }
}
