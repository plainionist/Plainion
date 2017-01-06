using System;
using Plainion.Logging;

namespace Plainion.RI.Logging
{
    class CustomLogEntry : ILogEntry
    {
        public CustomLogEntry( string creator, string message, LogLevel level )
        {
            Creator = creator;
            Message = message;
            Level = level;

            Timestamp = DateTime.Now;
        }

        public string Creator { get; private set; }

        public string Message { get; private set; }

        public LogLevel Level { get; private set; }

        public DateTime Timestamp { get; private set; }
    }
}
