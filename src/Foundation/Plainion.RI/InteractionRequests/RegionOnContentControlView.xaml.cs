using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class RegionOnContentControlView : UserControl
    {
        [ImportingConstructor]
        internal RegionOnContentControlView( RegionOnContentControlViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
