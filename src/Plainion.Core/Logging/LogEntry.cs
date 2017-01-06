using System;

namespace Plainion.Logging
{
    public class LogEntry : ILogEntry
    {
        public LogEntry( LogLevel level, string message )
        {
            Timestamp = DateTime.Now;

            Level = level;
            Message = message;
        }

        public DateTime Timestamp { get; private set; }

        public LogLevel Level { get; private set; }

        public string Message { get; private set; }
    }
}
