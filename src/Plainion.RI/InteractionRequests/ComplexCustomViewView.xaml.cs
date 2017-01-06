using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.InteractionRequests
{
    [Export]
    public partial class ComplexCustomViewView : UserControl
    {
        [ImportingConstructor]
        internal ComplexCustomViewView( ComplexCustomViewViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
