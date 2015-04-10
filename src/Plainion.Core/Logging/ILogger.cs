using System;

namespace Plainion.Logging
{
    public interface ILogger
    {
        void Debug( string format, params object[] args );

        void Info( string format, params object[] args );
        
        void Notice( string format, params object[] args );
        
        void Warning( string format, params object[] args );
        void Warning( Exception exception, string format, params object[] args );

        void Error( string format, params object[] args );
        void Error( Exception exception, string format, params object[] args );
    }
}
