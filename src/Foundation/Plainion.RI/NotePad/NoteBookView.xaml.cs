using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Plainion.RI.NotePad
{
    [Export]
    public partial class NoteBookView : UserControl
    {
        public NoteBookView()
        {
            InitializeComponent();
        }
    }
}
