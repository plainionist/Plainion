using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Plainion.Windows.Controls.Tree;
using Prism.Commands;
using Prism.Mvvm;

namespace Plainion.RI.Controls
{
    [Export]
    class TreeEditorViewModel : BindableBase
    {
        private DragDropBehavior myDragDropBehavior;

        public TreeEditorViewModel()
        {
            Root = new Node();
            Root.DragAllowed = false;
            Root.DropAllowed = false;

            foreach( var process in Process.GetProcesses() )
            {
                var processNode = new Node
                {
                    Parent = Root,
                    Id = process.Id.ToString(),
                    Name = process.ProcessName,
                    DragAllowed = false
                };
                Root.Children.Add( processNode );

                processNode.Children.AddRange( process.Threads
                    .OfType<ProcessThread>()
                    .Select( t => new Node
                    {
                        Parent = processNode,
                        Id = t.Id.ToString(),
                        Name = "unknown",
                        DropAllowed = false
                    } ) );
            }

            myDragDropBehavior = new DragDropBehavior( Root );
            DropCommand = new DelegateCommand<NodeDropRequest>( myDragDropBehavior.ApplyDrop );
        }

        public Node Root { get; private set; }

        public ICommand DropCommand { get; private set; }
    }
}
