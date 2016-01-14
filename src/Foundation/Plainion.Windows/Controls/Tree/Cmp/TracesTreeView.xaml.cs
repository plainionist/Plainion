using System.Windows;
using System.Windows.Controls;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    public partial class TracesTreeView : UserControl
    {
        public TracesTreeView()
        {
            InitializeComponent();
        }

        public TracesTree TracesSource
        {
            get { return ( TracesTree )GetValue( TracesSourceProperty ); }
            set { SetValue( TracesSourceProperty, value ); }
        }

        public static DependencyProperty TracesSourceProperty = DependencyProperty.Register( "TracesSource", typeof( TracesTree ), typeof( TracesTreeView ),
             new FrameworkPropertyMetadata( null ) );
    }
}
