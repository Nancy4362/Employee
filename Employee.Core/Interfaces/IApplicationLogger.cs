using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Core.Interfaces
{
    public interface IApplicationLogger
    {
        void Information(string message);
        void Debug(string message);
        void Error(string message);
        void Verbose(string message);
    }
}
