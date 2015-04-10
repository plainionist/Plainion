using System;
using Plainion.Logging;
using Plainion;

namespace Plainion.Logging
{
    class LoggingSinkLogger : ILogger
    {
        private ILoggingSink mySink;

        public LoggingSinkLogger( ILoggingSink sink )
        {
            Contract.RequiresNotNull( sink, "sink" );

            mySink = sink;
        }

        public void Debug( string format, params object[] args )
        {
            Write( LogLevel.Debug, format, args );
        }

        public void Info( string format, params object[] args )
        {
            Write( LogLevel.Info, format, args );
        }

        public void Notice( string format, params object[] args )
        {
            Write( LogLevel.Notice, format, args );
        }

        public void Warning( string format, params object[] args )
        {
            Write( LogLevel.Warning, format, args );
        }

        public void Warning( Exception exception, string format, params object[] args )
        {
            var msg = string.Format( format, args );
            Warning( "{0}{1}{2}", msg, Environment.NewLine, exception.Dump() );
        }

        public void Error( string format, params object[] args )
        {
            Write( LogLevel.Error, format, args );
        }

        public void Error( Exception exception, string format, params object[] args )
        {
            var msg = string.Format( format, args );
            Error( "{0}{1}{2}", msg, Environment.NewLine, exception.Dump() );
        }

        public static LogLevel LogLevel { get; set; }

        /// <summary>
        /// Logs the given message if the given logging fits the current logging level.
        /// </summary>
        public void Write( LogLevel level, string msg, params object[] args )
        {
            if( LogLevel > level )
            {
                return;
            }

            mySink.Write( new LogEntry( level, string.Format( msg, args ) ) );
        }
    }
}