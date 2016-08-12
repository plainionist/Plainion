using System;
using Plainion.Logging;

namespace Plainion.RI.Logging
{
    class CustomLogger : ILogger
    {
        private CustomLoggerFactory myFactory;
        private Type myOwnerType;

        public CustomLogger( CustomLoggerFactory factory, Type owner )
        {
            myFactory = factory;
            myOwnerType = owner;
        }

        public void Debug( string format, params object[] args )
        {
            Write( LogLevel.Debug, format, args );
        }

        private void Write( LogLevel level, string format, object[] args )
        {
            if( myFactory.LogLevel <= level )
            {
                myFactory.Sink.Write( new CustomLogEntry( myOwnerType.Namespace, string.Format( format, args ), level ) );
            }
        }

        public void Info( string format, params object[] args )
        {
            Write( LogLevel.Info, format, args );
        }

        public void Notice( string format, params object[] args )
        {
            Write( LogLevel.Notice, format, args );
        }

        public void Warning( string format, params object[] args )
        {
            Write( LogLevel.Warning, format, args );
        }

        public void Warning( Exception exception, string format, params object[] args )
        {
            var msg = string.Format( format, args );
            Warning( "{0}{1}{2}", msg, Environment.NewLine, exception.Dump() );
        }

        public void Error( string format, params object[] args )
        {
            Write( LogLevel.Error, format, args );
        }

        public void Error( Exception exception, string format, params object[] args )
        {
            var msg = string.Format( format, args );
            Error( "{0}{1}{2}", msg, Environment.NewLine, exception.Dump() );
        }
    }
}
