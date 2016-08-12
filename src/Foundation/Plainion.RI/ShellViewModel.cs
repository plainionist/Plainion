using System.ComponentModel.Composition;
using Plainion.Logging;
using Prism.Mvvm;

namespace Plainion.RI
{
    [Export]
    class ShellViewModel : BindableBase
    {
        private static readonly ILogger myLogger = LoggerFactory.GetLogger( typeof( ShellViewModel ) );

        private int mySelectedIndex;

        public int SelectedIndex
        {
            get { return mySelectedIndex; }
            set
            {
                if( SetProperty( ref mySelectedIndex, value ) )
                {
                    myLogger.Notice( "You selected tab number '{0}'", value + 1 );
                }
            }
        }
    }
}
