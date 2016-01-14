using System.Windows.Controls;

namespace Plainion.Windows.Controls.Tree.Forest
{
    public partial class ExtendedTreeView : UserControl
    {
        public ExtendedTreeView( TreeViewModel model )
        {
            InitializeComponent();

            Model = model;
            DataContext = model;
        }

        public TreeViewModel Model
        {
            get;
            private set;
        }
    }
}
