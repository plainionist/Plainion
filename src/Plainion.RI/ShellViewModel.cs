using System.ComponentModel.Composition;
using System.Windows.Controls;
using Plainion.Logging;
using Prism.Mvvm;

namespace Plainion.RI
{
    [Export]
    class ShellViewModel : BindableBase
    {
        private static readonly ILogger myLogger = LoggerFactory.GetLogger( typeof( ShellViewModel ) );

        private object mySelectedItem;

        public object SelectedItem
        {
            get { return mySelectedItem; }
            set
            {
                if( SetProperty( ref mySelectedItem, value ) )
                {
                    myLogger.Notice( "You selected tab '{0}'", ((TabItem)value).Header );
                }
            }
        }
    }
}
