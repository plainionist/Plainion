using System;

namespace Plainion.Logging
{
    /// <summary>
    /// Defines the minimum contract of a log entry which can be handled by <see cref="ILoggingSink"/>
    /// </summary>
    public interface ILogEntry
    {
        DateTime Timestamp { get; }

        LogLevel Level { get; }

        string Message { get; }
    }
}
