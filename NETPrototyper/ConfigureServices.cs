using NETProtoyper.Interfaces;
using NETProtoyper.Services;
using Sample.Core.Constants;
using Sample.Core.Interfaces;
using Sample.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace NETProtoyper
{
    public class ConfigureServices
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                services.AddSingleton(configuration.GetSection("WorkerSettings").Get<WorkerSettings>());
                services.AddTransient<IPrototypeLogger, PrototypeLogger>();
                services.AddTransient<IProcess, Process>();

                services.AddSingleton(new DBConnectionSettings()
                {
                    MSSQLConnectionString = Environment.GetEnvironmentVariable("DBConnectionSettings:MSSQLConnectionString"),
                    MSSQLCommandTimeOut = Environment.GetEnvironmentVariable("DBConnectionSettings:MSSQLCommandTimeOut").Length > 0 ?
                                            Convert.ToInt32(Environment.GetEnvironmentVariable("DBConnectionSettings:MSSQLCommandTimeOut")) :
                                            null,
                    MongoDBConnection = Environment.GetEnvironmentVariable("DBConnectionSettings:MongoDBConnection"),
                    MongoDBName = Environment.GetEnvironmentVariable("DBConnectionSettings:MongoDBName"),
                    MongoDBTokenValidity = Convert.ToInt32(Environment.GetEnvironmentVariable("DBConnectionSettings:MongoDBTokenValidity"))
                });

                services.AddTransient<IMSSQLDBAcess, MSSQLDBContext>();

                services.AddHostedService<Worker>();
            }
            catch (Exception ex) 
            { 
                LogConfigureServicesException(ex, configuration);
            }
        }


        private static void LogConfigureServicesException(Exception ex, IConfiguration _configuration)
        {
            string logsFolderPath = _configuration.GetSection("WorkerSettings").Get<WorkerSettings>().LogPath;

            if (!Directory.Exists(logsFolderPath))
            {
                Directory.CreateDirectory(logsFolderPath);
            }

            string processLogFileName = string.Format("{0}_processLog.txt", DateTime.Now.ToString("yyyyMMddHHmmss"));
            string loggingFilePath = string.Format("{0}{1}", logsFolderPath, processLogFileName);

            if (!File.Exists(loggingFilePath))
            {
                File.Create(loggingFilePath);
            }

            File.AppendAllText(loggingFilePath, string.Concat(ex.Message, Environment.NewLine));
            File.AppendAllText(loggingFilePath, JsonSerializer.Serialize(ex.Data));
        }
    }
}
