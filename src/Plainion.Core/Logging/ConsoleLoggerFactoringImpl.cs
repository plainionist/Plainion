using System;

namespace Plainion.Logging
{
    public class ConsoleLoggerFactoringImpl : ILoggerFactoringImpl
    {
        public void LoadConfiguration( Uri uri )
        {
            ConsoleLogger.LogLevel = LogLevel.Warning;
        }

        public void AddGuiAppender( ILoggingSink sink )
        {
            // nothing to do -> ignore this call
        }

        public ILogger GetLogger( Type loggingType )
        {
            return new ConsoleLogger();
        }

        public LogLevel LogLevel
        {
            get { return ConsoleLogger.LogLevel; }
            set { ConsoleLogger.LogLevel = value; }
        }
    }
}
