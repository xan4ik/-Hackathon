using System;
using System.Collections.Generic;
using System.Text;

namespace CommandDLL.Exceptions
{
    public class UnhandledException : Exception
    {
        public UnhandledException(Exception innerException) : base("unhandled exception", innerException)
        { }
    }
}
