using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.NotePad
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
