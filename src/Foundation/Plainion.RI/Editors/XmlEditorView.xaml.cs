using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.Editors
{
    [Export]
    public partial class XmlEditorView : UserControl
    {
        [ImportingConstructor]
        internal XmlEditorView(XmlEditorViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
