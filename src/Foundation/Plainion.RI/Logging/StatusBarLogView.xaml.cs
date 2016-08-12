using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.Logging
{
    [Export]
    public partial class StatusBarLogView : UserControl
    {
        [ImportingConstructor]
        internal StatusBarLogView( StatusBarLogViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
