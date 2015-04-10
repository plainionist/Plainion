using System;

namespace Plainion.Logging
{
    public interface ILoggerFactoringImpl
    {
        void LoadConfiguration( Uri uri );
        void AddGuiAppender( ILoggingSink sink );
        ILogger GetLogger( Type loggingType );
        LogLevel LogLevel { get; set; }
    }
}
