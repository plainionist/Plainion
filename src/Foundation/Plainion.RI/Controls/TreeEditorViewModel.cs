using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Mvvm;
using Plainion.Windows.Controls.Tree;

namespace Plainion.RI.Controls
{
    [Export]
    class TreeEditorViewModel : BindableBase
    {
        public TreeEditorViewModel()
        {
            Root = new Node { Text = "Root" };
            Root.Children = new[]
            {
                new Node{Text="C1"},
                new Node{Text="C2"}
            };
        }

        public Node Root { get; private set; }
    }
}
