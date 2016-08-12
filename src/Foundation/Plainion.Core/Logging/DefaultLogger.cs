using System;

namespace Plainion.Logging
{
    /// <summary>
    /// Default implementation of <see cref="ILogger"/>.
    /// </summary>
    public class DefaultLogger : LoggerBase
    {
        private ILoggingSink mySink;

        public DefaultLogger( ILoggingSink sink )
        {
            Contract.RequiresNotNull( sink, "sink" );

            mySink = sink;
        }

        public static LogLevel LogLevel { get; set; }

        protected override LogLevel ConfiguredLogLevel
        {
            get { { return LogLevel; } }
        }

        protected override void WriteCore( LogLevel level, string msg )
        {
            mySink.Write( new LogEntry( level, msg ) );
        }
    }
}