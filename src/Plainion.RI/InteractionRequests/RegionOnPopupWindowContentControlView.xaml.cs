using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class RegionOnPopupWindowContentControlView : UserControl
    {
        [ImportingConstructor]
        internal RegionOnPopupWindowContentControlView( RegionOnPopupWindowContentControlViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
