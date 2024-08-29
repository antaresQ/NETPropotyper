using NETProtoyper.Interfaces;
using Sample.Core.Constants;

namespace NETProtoyper
{
    public class Worker : BackgroundService
    {
        private readonly IPrototypeLogger _prototypeLogger;
        private readonly ILogger<Worker> _logger;
        private readonly IHostEnvironment _hostEnvt;
        private readonly WorkerSettings _workerSettings;
        private readonly IServiceProvider _serviceProvider;

        public Worker(
                IPrototypeLogger prototypeLogger, 
                ILogger<Worker> logger, 
                IHostEnvironment hostEnvt,
                WorkerSettings workerSettings,
                IServiceProvider serviceProvider)
        {
            _prototypeLogger = prototypeLogger;
            _logger = logger;
            _hostEnvt = hostEnvt;
            _workerSettings = workerSettings;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _prototypeLogger.CheckAndCreateLogFolderPath();

            Console.WriteLine("PrototyperWorker Start!");

            _prototypeLogger.LogToConsoleAndFile(string.Format("Running in Environment: {0}", _hostEnvt.EnvironmentName));


            while (!stoppingToken.IsCancellationRequested)
            {
                _prototypeLogger.LogToConsoleAndFile(string.Format("Worker running at: {0}", DateTimeOffset.Now));

                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    // Get the required service from the DI services.
                    var selectedService = scope.ServiceProvider.GetRequiredService<IProcess>();

                    // Run the service
                    await selectedService.RunAsync();
                }

                int milliSeconds = (int)Math.Ceiling(_workerSettings.IntervalInMins * 60 * 1000);
                await Task.Delay(milliSeconds, stoppingToken);
            }
        }
    }
}
