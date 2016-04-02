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
                AddChildTo( request.DroppedNode, myRoot );
            }
            else
            {
                if( request.Location == DropLocation.Before || request.Location == DropLocation.After )
                {
                    MoveNode( request.DroppedNode, request.DropTarget,  request.Location );
                }
                else
                {
                    AddChildTo( request.DroppedNode, request.DropTarget );
                }
            }
        }

        private void MoveNode( INode nodeToMove, INode targetNode, DropLocation location )
        {
            MoveNode( ( Node )nodeToMove, ( Node )targetNode, location );

            //   IsDirty = true;
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
