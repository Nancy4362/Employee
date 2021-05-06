using System;
using System.Collections.Generic;
using System.Text;
using Employee.Core.Interfaces;
using Serilog;

namespace Employee.Core
{
    public class ApplicationLogger : IApplicationLogger
    {
        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Verbose(string message)
        {
            Log.Verbose(message);
        }
    }
}
