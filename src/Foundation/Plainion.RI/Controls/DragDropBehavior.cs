using System;
using System.Collections.ObjectModel;
using Plainion.Windows.Controls.Tree;
using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.RI.Controls
{
    class DragDropBehavior
    {
        private INode myRoot;

        public DragDropBehavior( INode root )
        {
            Contract.RequiresNotNull( root, "root" );

            myRoot = root;
        }

        internal void ApplyDrop( NodeDropRequest request )
        {
            if( request.DropTarget == myRoot )
            {
                ChangeParent( ( Node )request.DroppedNode, n => ( ( Node )myRoot ).Children.Add( n ), myRoot );
            }
            else if( request.Location == DropLocation.Before || request.Location == DropLocation.After )
            {
                MoveNode( ( Node )request.DroppedNode, ( Node )request.DropTarget, request.Location );
            }
            else
            {
                ChangeParent( ( Node )request.DroppedNode, n => ( ( Node )request.DropTarget ).Children.Add( n ), request.DropTarget );
            }
        }

        private void MoveNode( Node nodeToMove, Node targetNode, DropLocation operation )
        {
            var siblings = ( ObservableCollection<Node> )targetNode.Parent.Children;
            var dropPos = siblings.IndexOf( targetNode );

            if( operation == DropLocation.After )
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
                    ChangeParent( nodeToMove, n => siblings.Insert( dropPos, n ), targetNode.Parent );
                }
                else
                {
                    ChangeParent( nodeToMove, n => siblings.Add( n ), targetNode.Parent );
                }
            }
        }

        private void ChangeParent( Node nodeToMove, Action<Node> insertionOperation, INode newParent )
        {
            var oldParent = ( Node )nodeToMove.Parent;

            oldParent.Children.Remove( nodeToMove );

            insertionOperation( nodeToMove );

            nodeToMove.Parent = newParent.Parent;
        }
    }
}
