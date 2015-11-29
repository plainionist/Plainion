using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.Dialogs
{
    [Export]
    public partial class SelectFolderInteractionRequestView : UserControl
    {
        [ImportingConstructor]
        internal SelectFolderInteractionRequestView( SelectFolderInteractionRequestViewModel viewModel )
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
