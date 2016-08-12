using System.ComponentModel.Composition;
using Plainion.Logging;
using Prism.Mvvm;

namespace Plainion.RI.Logging
{
    [Export( typeof( StatusBarLogViewModel ) )]
    [Export( typeof( ILoggingSink ) )]
    class StatusBarLogViewModel : BindableBase, ILoggingSink
    {
        public CustomLogEntry LastLog { get; private set; }

        public void Write( ILogEntry entry )
        {
            LastLog = ( CustomLogEntry )entry;
        }
    }
}
