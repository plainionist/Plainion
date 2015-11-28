using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class DefaultConfirmationView : UserControl
    {
        [ImportingConstructor]
        internal DefaultConfirmationView(DefaultConfirmationViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
