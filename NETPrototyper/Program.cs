using NETProtoyper;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using NETProtoyper.Interfaces;



namespace NETPrototyper
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            bool runOneTime = true;

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostBuilderContext, configureBuilder) =>
                {
                    configureBuilder.Sources.Clear();

                    var name = Environment.GetEnvironmentVariable("PROTOTYPER_ENVT");
                    hostBuilderContext.HostingEnvironment.EnvironmentName = name;

                    Console.WriteLine("Environment: {0}", name);

                    var appsettingsFileName = string.Format("appsettings{0}.json", (name?.Length >0) ? $".{name.ToLower()}" : string.Empty);

                    Console.WriteLine("Using Configuration: {0}", appsettingsFileName);

                    var config = new ConfigurationBuilder()
                                    .AddEnvironmentVariables()
                                    .AddCommandLine(args)
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .AddJsonFile(appsettingsFileName, optional: false, reloadOnChange: true)
                                    .Build();

                    configureBuilder.AddConfiguration(config);


                })
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    ConfigureServices.Configure(services, hostBuilderContext.Configuration);
                })
                .Build();


            if (runOneTime)
            {
                Console.WriteLine("Run Process Once");
                
                var process = host.Services.GetRequiredService<IProcess>();
                await process.RunAsync();
            }
            else
            {
                Console.WriteLine("Run Process Once Every Interval");
                
                await host.RunAsync();
            }
        }
    }
}
