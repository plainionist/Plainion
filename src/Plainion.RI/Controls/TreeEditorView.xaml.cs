using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.Controls
{
    [Export]
    public partial class TreeEditorView : UserControl
    {
        [ImportingConstructor]
        internal TreeEditorView(TreeEditorViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
