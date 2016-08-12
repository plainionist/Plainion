using System;
using System.ComponentModel.Composition;
using Plainion.Logging;

namespace Plainion.RI.Logging
{
    [Export]
    class CustomLoggerFactory : ILoggerFactory
    {
        private CompositeLoggingSink myRootSink;

        [ImportingConstructor]
        public CustomLoggerFactory( ILoggingSink sink )
        {
            myRootSink = new CompositeLoggingSink();
            myRootSink.Add( sink );
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
