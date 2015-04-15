using System.Collections.Generic;
using Plainion;

namespace Plainion.Logging
{
    public class CompositeLoggingSink : ILoggingSink
    {
        private IList<ILoggingSink> mySinks;

        public CompositeLoggingSink()
        {
            mySinks = new List<ILoggingSink>();
        }

        public void Add( ILoggingSink sink )
        {
            Contract.RequiresNotNull( sink, "sink" );

            mySinks.Add( sink );
        }

        public void Remove( ILoggingSink sink )
        {
            Contract.RequiresNotNull( sink, "sink" );

            mySinks.Remove( sink );
        }

        public void Write( ILogEntry entry )
        {
            foreach( var sink in mySinks )
            {
                sink.Write( entry );
            }
        }
    }
}
