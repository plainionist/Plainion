using System;
using Plainion.Logging;

namespace Plainion.RI.Logging
{
    class CustomLogger : LoggerBase
    {
        private CustomLoggerFactory myFactory;
        private Type myOwnerType;

        public CustomLogger( CustomLoggerFactory factory, Type owner )
        {
            myFactory = factory;
            myOwnerType = owner;
        }

        protected override LogLevel ConfiguredLogLevel
        {
            get { return myFactory.LogLevel; }
        }

        protected override void WriteCore( LogLevel level, string msg )
        {
            myFactory.Sink.Write( new CustomLogEntry( myOwnerType.FullName, msg, level ) );
        }
    }
}
