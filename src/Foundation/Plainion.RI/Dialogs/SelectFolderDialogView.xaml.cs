using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.Dialogs
{
    [Export]
    public partial class SelectFolderDialogView : UserControl
    {
        [ImportingConstructor]
        internal SelectFolderDialogView( SelectFolderDialogViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
