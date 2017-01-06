using System;
using Plainion.Logging;

namespace Plainion.RI.Logging
{
    class CustomLoggerFactory : ILoggerFactory
    {
        private CompositeLoggingSink myRootSink;

        public CustomLoggerFactory()
        {
            myRootSink = new CompositeLoggingSink();
        }

        public ILoggingSink Sink { get { return myRootSink; } }

        public void AddSink( ILoggingSink sink )
        {
            myRootSink.Add( sink );
        }

        public ILogger GetLogger( Type loggingType )
        {
            return new CustomLogger( this, loggingType );
        }

        public LogLevel LogLevel { get; set; }
    }
}
