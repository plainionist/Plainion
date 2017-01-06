using System.ComponentModel.Composition;
using Plainion.Logging;
using Prism.Mvvm;

namespace Plainion.RI.Logging
{
    [Export( typeof( StatusBarLogViewModel ) )]
    [Export( typeof( ILoggingSink ) )]
    class StatusBarLogViewModel : BindableBase, ILoggingSink
    {
        private CustomLogEntry myLastLog;

        public CustomLogEntry LastLog
        {
            get { return myLastLog; }
            private set { SetProperty( ref myLastLog, value ); }
        }

        public void Write( ILogEntry entry )
        {
            LastLog = ( CustomLogEntry )entry;
        }
    }
}
