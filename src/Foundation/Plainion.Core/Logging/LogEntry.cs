
namespace Plainion.Logging
{
    public class LogEntry : ILogEntry
    {
        public LogEntry( LogLevel level, string message )
        {
            Level = level;
            Message = message;
        }

        public LogLevel Level { get; private set; }
        public string Message { get; private set; }
    }
}
