using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class RegionWithPopupWindowActionExtensionsView : UserControl
    {
        [ImportingConstructor]
        internal RegionWithPopupWindowActionExtensionsView( RegionWithPopupWindowActionExtensionsViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
