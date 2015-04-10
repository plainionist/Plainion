using System;

namespace Plainion.Logging
{
    public class LoggerFactory
    {
        static LoggerFactory()
        {
            Implementation = new ConsoleLoggerFactoringImpl();
        }

        public static ILoggerFactoringImpl Implementation
        {
            get;
            set;
        }

        public static void LoadConfiguration( Uri uri )
        {
            Implementation.LoadConfiguration( uri );
        }

        public static void AddGuiAppender( ILoggingSink sink )
        {
            Implementation.AddGuiAppender( sink );
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
