using System;

namespace Plainion.Logging
{
    /// <summary>
    /// Implementation of <see cref="ILoggingSink"/> which logs to System.Console in different colors.
    /// </summary>
    public class ConsoleLoggingSink : ILoggingSink
    {
        public void Write( ILogEntry entry )
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = GetColor( entry.Level );

            Console.WriteLine( entry.Message );

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
