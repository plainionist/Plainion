using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Plainion.Windows.Controls.Tree;
using Plainion.Windows.Interactivity.DragDrop;
using Prism.Commands;
using Prism.Mvvm;

namespace Plainion.RI.Controls
{
    [Export]
    class TreeEditorViewModel : BindableBase
    {
        public TreeEditorViewModel()
        {
            Root = new Node();

            foreach( var process in Process.GetProcesses() )
            {
                var processNode = new Node() { Parent = Root, Id = process.Id.ToString(), Name = process.ProcessName };
                Root.Children.Add( processNode );

                processNode.Children.AddRange( process.Threads
                    .OfType<ProcessThread>()
                    .Select( t => new Node { Parent = processNode, Id = t.Id.ToString(), Name = "unknown" } ) );
            }

            DropCommand = new DelegateCommand<NodeDropRequest>( OnDrop );
        }

        public Node Root { get; private set; }

        public ICommand DropCommand { get; private set; }

        public enum MoveOperation
        {
            MoveBefore,
            MoveAfter
        }

        private void OnDrop( NodeDropRequest request )
        {
            if( request.DropTarget == Root )
            {
                AddChildTo( request.DroppedNode, Root );
            }
            else
            {
                if( request.Location == DropLocation.Before || request.Location == DropLocation.After )
                {
                    var operation = request.Location == DropLocation.Before ? MoveOperation.MoveBefore : MoveOperation.MoveAfter;
                    MoveNode( request.DroppedNode, request.DropTarget, operation );
                }
                else
                {
                    AddChildTo( request.DroppedNode, request.DropTarget );
                }
            }
        }

        public void MoveNode( INode nodeToMove, INode targetNode, MoveOperation operation )
        {
            MoveNode( ( Node )nodeToMove, ( Node )targetNode, operation );

            //   IsDirty = true;
        }

        private void MoveNode( Node nodeToMove, Node targetNode, MoveOperation operation )
        {
            var siblings = ( ObservableCollection<Node> )targetNode.Parent.Children;
            var dropPos = siblings.IndexOf( targetNode );

            if( operation == MoveOperation.MoveAfter )
            {
                dropPos++;
            }

            if( siblings.Contains( nodeToMove ) )
            {
                var oldPos = siblings.IndexOf( nodeToMove );
                if( oldPos < dropPos )
                {
                    // ObservableCollection first removes the item and then reinserts which invalidates the index
                    dropPos--;
                }

                siblings.Move( oldPos, dropPos );
            }
            else
            {
                if( dropPos < siblings.Count )
                {
                    siblings.Insert( dropPos, nodeToMove );
                }
                else
                {
                    siblings.Add( nodeToMove );
                }
            }

            //IsDirty = true;
        }

        public void AddChildTo( INode child, INode newParent )
        {
            ( ( Node )newParent ).Children.Add( ( Node )child );

            // IsDirty = true;
        }
    }
}
