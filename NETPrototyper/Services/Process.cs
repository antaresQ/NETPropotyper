using NETProtoyper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProtoyper.Services
{
    public class Process: IProcess
    {
        private readonly IPrototypeLogger _prototypeLogger;

        public Process(IPrototypeLogger prototypeLogger)
        {
            _prototypeLogger = prototypeLogger;
        }

        public async Task RunAsync()
        {
            try
            {
                #region: Add methods to test/protoype here

                Console.WriteLine("Run Methods!");

                #endregion
            }
            catch (Exception ex)
            {
                _prototypeLogger.LogToConsoleAndFile(ex);
            }
        }
    }
}
