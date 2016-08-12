using System;

namespace Plainion.Logging
{
    /// <summary>
    /// Main interface of the logging framework. Provides APIs for configuration as well as creation of <see cref="ILogger"/> instances.
    /// </summary>
    public interface ILoggerFactory
    {
        void LoadConfiguration( Uri uri );

        void AddSink( ILoggingSink sink );

        ILogger GetLogger( Type loggingType );

        LogLevel LogLevel { get; set; }
    }
}
