using System;

namespace Plainion.Logging
{
    /// <summary>
    /// Base class for <see cref="ILogger"/> implementations.
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
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
            var msg = FormatMessage( format, args );
            Warning( "{0}{1}{2}", msg, Environment.NewLine, exception.Dump() );
        }

        public void Error( string format, params object[] args )
        {
            Write( LogLevel.Error, format, args );
        }

        public void Error( Exception exception, string format, params object[] args )
        {
            var msg = FormatMessage( format, args );
            Error( "{0}{1}{2}", msg, Environment.NewLine, exception.Dump() );
        }

        /// <summary>
        /// UT only
        /// </summary>
        internal void Write( LogLevel level, string msg, params object[] args )
        {
            if( ConfiguredLogLevel > level )
            {
                return;
            }

            WriteCore( level, FormatMessage( msg, args ) );
        }

        private string FormatMessage( string msg, params object[] args )
        {
            if( args.Length == 0 )
            {
                // sometimes we find "{" in exceptions (e.g. deserialization issues from Xaml). In this case usually the ILogger
                // API without "arguments" is used.
                // So dont pass the args to string.Format() to avoid format exceptions
                return msg;
            }
            else
            {
                return string.Format( msg, args );
            }
        }

        /// <summary>
        /// Derived classes must implement this SPI to provide 
        /// </summary>
        protected abstract LogLevel ConfiguredLogLevel { get; }

        /// <summary>
        /// Called from all log APIs if the configured log level allows it.
        /// Derived classes must implement this SPI to perform the actual logging.
        /// </summary>
        protected abstract void WriteCore( LogLevel level, string msg );
    }
}