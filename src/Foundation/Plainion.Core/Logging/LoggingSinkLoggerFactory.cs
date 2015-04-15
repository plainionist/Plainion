using System;
using System.Collections.Generic;
using Plainion.Logging;

namespace Plainion.Logging
{
    public class LoggingSinkLoggerFactory : ILoggerFactoringImpl
    {
        private CompositeLoggingSink myRootSink;

        public LoggingSinkLoggerFactory()
        {
            myRootSink = new CompositeLoggingSink();
        }

        public void LoadConfiguration( Uri uri )
        {
            LogLevel = LogLevel.Notice;
        }

        public void AddGuiAppender( ILoggingSink sink )
        {
            myRootSink.Add( sink );
        }

        public ILogger GetLogger( Type loggingType )
        {
            return new LoggingSinkLogger( myRootSink );
        }

        public LogLevel LogLevel
        {
            get { return LoggingSinkLogger.LogLevel; }
            set { LoggingSinkLogger.LogLevel = value; }
        }
    }
}
