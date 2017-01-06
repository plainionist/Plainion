using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class RegionOnPopupWindowActionView : UserControl
    {
        [ImportingConstructor]
        internal RegionOnPopupWindowActionView( RegionOnPopupWindowActionViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
