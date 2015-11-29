using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class CustomNotificationView : UserControl
    {
        [ImportingConstructor]
        internal CustomNotificationView( CustomNotificationViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
