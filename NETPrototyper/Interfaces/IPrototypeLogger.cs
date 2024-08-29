using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProtoyper.Interfaces
{
    public interface IPrototypeLogger
    {
        bool CheckAndCreateLogFolderPath();
        void LogToConsoleAndFile(string message);
        void LogToConsoleAndFile(Exception ex);
    }
}
