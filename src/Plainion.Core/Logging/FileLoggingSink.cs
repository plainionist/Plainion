using System;
using System.IO;

namespace Plainion.Logging
{
    /// <summary>
    /// Implementation of <see cref="ILoggingSink"/> which logs to a file.
    /// It requires calling Open() before the first entry can be written and Close() when the logging session ends.
    /// </summary>
    public class FileLoggingSink : ILoggingSink
    {
        private string myLogFile;
        private StreamWriter myWriter;

        public FileLoggingSink( string logFile )
        {
            Contract.RequiresNotNullNotEmpty( logFile, "logFile" );

            myLogFile = logFile;
        }

        public void Open()
        {
            Contract.Invariant( myWriter == null, "Open can only be called once" );

            myWriter = new StreamWriter( myLogFile );
        }

        public void Close()
        {
            if( myWriter != null )
            {
                myWriter.Dispose();
                myWriter = null;
            }
        }

        public void Write( ILogEntry entry )
        {
            Contract.Invariant( myWriter != null, "Open must be called before entries can be written" );

            myWriter.WriteLine( string.Format( "{0}|{1}|{2}", entry.Timestamp, entry.Level, entry.Message ) );
        }
    }
}
