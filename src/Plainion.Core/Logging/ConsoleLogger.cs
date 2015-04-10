using System;

namespace Plainion.Logging
{
    /// <summary>
    /// Simple logger which logs different message types to the console.
    /// <remarks>
    /// Logs the different message types in different colors. Esp. Errors = Red
    /// and Warnings = Yellow.
    /// </remarks>
    /// </summary>
    class ConsoleLogger : ILogger
    {
        private static LogLevel myLevel = LogLevel.Warning;

        public void Debug( string format, params object[] args )
        {
            WriteLine( LogLevel.Debug, format, args );
        }

        public void Info( string format, params object[] args )
        {
            WriteLine( LogLevel.Info, format, args );
        }

        public void Notice( string format, params object[] args )
        {
            WriteLine( LogLevel.Notice, format, args );
        }

        public void Warning( string format, params object[] args )
        {
            WriteLine( LogLevel.Warning, format, args );
        }

        public void Warning( Exception exception, string format, params object[] args )
        {
            var msg = string.Format( format, args );
            Warning( "{0}{1}{2}", msg, Environment.NewLine, exception.Dump() );
        }

        public void Error( string format, params object[] args )
        {
            WriteLine( LogLevel.Error, format, args );
        }

        public void Error( Exception exception, string format, params object[] args )
        {
            var msg = string.Format( format, args );
            Error( "{0}{1}{2}", msg, Environment.NewLine, exception.Dump() );
        }

        public static LogLevel LogLevel
        {
            get
            {
                return myLevel;
            }
            set
            {
                myLevel = value;
            }
        }

        /// <summary>
        /// Logs the given message if the given logging fits the current logging level.
        /// </summary>
        public static void WriteLine( LogLevel level, string msg, params object[] args )
        {
            if( myLevel > level )
            {
                return;
            }

            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = GetColor( level );

            if( args.Length == 0 )
            {
                // sometimes we find "{" in exceptions (e.g. deserialization issues from Xaml). In this case we usually call
                // ConsoleLogger.Error( exception.ToString() );
                // So dont pass the args to Console.WriteLine() to avoid format exceptions
                Console.WriteLine( msg );
            }
            else
            {
                Console.WriteLine( msg, args );
            }

            Console.ForegroundColor = oldColor;
        }

        private static ConsoleColor GetColor( LogLevel level )
        {
            if( level == LogLevel.Error )
            {
                return ConsoleColor.Red;
            }
            else if( level == LogLevel.Warning )
            {
                return ConsoleColor.Yellow;
            }
            else
            {
                return ConsoleColor.Gray;
            }
        }
    }
}