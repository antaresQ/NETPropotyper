using NETProtoyper.Interfaces;
using Sample.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProtoyper.Services
{
    public class PrototypeLogger: IPrototypeLogger
    {
        private readonly WorkerSettings _workerSettings;
        private readonly ILogger<Worker> _logger;

        public PrototypeLogger(WorkerSettings workerSettings, ILogger<Worker> logger)
        {
            _workerSettings = workerSettings;
            _logger = logger;
        }


        public bool CheckAndCreateLogFolderPath()
        {
            if (!Directory.Exists(_workerSettings.LogPath))
            {
                Directory.CreateDirectory(_workerSettings.LogPath);
            }

            return Directory.Exists(_workerSettings.LogPath);
        }

        public void LogToConsoleAndFile(string message)
        {
            string LogFolderPath = _workerSettings.LogPath;

            if (!Directory.Exists(_workerSettings.LogPath))
            {
                Directory.CreateDirectory(_workerSettings.LogPath);
            }

            string processLogFileName = string.Format("{0}_processLog.txt", DateTime.Now.ToString("yyyyMMddHH"));
            string loggingFilePath = string.Format("{0}{1}", LogFolderPath, processLogFileName);

            if (!File.Exists(loggingFilePath))
            {
                var stream = File.Create(loggingFilePath);
                stream.DisposeAsync();
            }

            message = string.Format("{0}: {1}", DateTime.Now.ToString(@"dd-MM-yyyy HH:mm:ss"), message);

            _logger.LogInformation(string.Concat(message, Environment.NewLine));

            File.AppendAllText(loggingFilePath, string.Concat(message, Environment.NewLine));
        }

        public void LogToConsoleAndFile(Exception ex)
        {
            // to consider adding additonal objects in ex or use serilog
            LogToConsoleAndFile(ex.Message);
        }
    }
}
