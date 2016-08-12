using System;

namespace Plainion.Logging
{
    /// <summary>
    /// Facade for the actual logger factory implementation. Provides simple access to the logging framework as kind of singleton.
    /// </summary>
    public class LoggerFactory
    {
        static LoggerFactory()
        {
            Implementation = new DefaultLoggerFactory();
        }

        public static ILoggerFactory Implementation { get; set; }

        public static void LoadConfiguration( Uri uri )
        {
            Implementation.LoadConfiguration( uri );
        }

        public static void AddSink( ILoggingSink sink )
        {
            Implementation.AddSink( sink );
        }

        public static ILogger GetLogger( Type loggingType )
        {
            return Implementation.GetLogger( loggingType );
        }

        public static LogLevel LogLevel
        {
            get { return Implementation.LogLevel; }
            set { Implementation.LogLevel = value; }
        }
    }
}
