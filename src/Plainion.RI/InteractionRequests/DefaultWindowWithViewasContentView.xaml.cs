using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class DefaultWindowWithViewAsContentView : UserControl
    {
        [ImportingConstructor]
        internal DefaultWindowWithViewAsContentView( DefaultWindowWithViewAsContentViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
