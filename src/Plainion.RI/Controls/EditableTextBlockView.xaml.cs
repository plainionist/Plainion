using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.Controls
{
    [Export]
    public partial class EditableTextBlockView : UserControl
    {
        [ImportingConstructor]
        internal EditableTextBlockView( EditableTextBlockViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
