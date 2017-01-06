using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.Controls
{
    [Export]
    public partial class NotePadView : UserControl
    {
        public NotePadView()
        {
            InitializeComponent();
        }
    }
}
