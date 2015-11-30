using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class DefaultWindowWithViewModelAsContentView : UserControl
    {
        [ImportingConstructor]
        internal DefaultWindowWithViewModelAsContentView( DefaultWindowWithViewModelAsContentViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
