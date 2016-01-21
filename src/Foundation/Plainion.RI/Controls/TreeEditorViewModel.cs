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
            Root = new Node
            {
                Text = "Root",
                Children = new[]
                {
                    new Node
                    {
                        Text="1",
                        Children = new[]
                        {
                            new Node{Text="a"},
                            new Node{Text="b"}
                        }
                    },
                    new Node
                    {
                        Text="2",
                        Children = new[]
                        {
                            new Node{Text="c"},
                            new Node{Text="d"}
                        }
                    }
                }
            };
        }

        public Node Root { get; private set; }
    }
}
