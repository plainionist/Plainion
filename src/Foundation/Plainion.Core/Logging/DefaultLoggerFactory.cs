using System;

namespace Plainion.Logging
{
    /// <summary>
    /// Default implementation of <see cref="ILoggerFactory"/> which allows adding different kinds of <see cref="ILoggingSink"/>s.
    /// </summary>
    public class DefaultLoggerFactory : ILoggerFactory
    {
        private CompositeLoggingSink myRootSink;

        public DefaultLoggerFactory()
        {
            myRootSink = new CompositeLoggingSink();
            LogLevel = LogLevel.Warning;
        }

        public void AddSink( ILoggingSink sink )
        {
            myRootSink.Add( sink );
        }

        public ILogger GetLogger( Type loggingType )
        {
            return new DefaultLogger( myRootSink );
        }

        public LogLevel LogLevel
        {
            get { return DefaultLogger.LogLevel; }
            set { DefaultLogger.LogLevel = value; }
        }
    }
}
