using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree
{
    public class NodeDropRequest
    {
        public INode DroppedNode { get; internal set; }

        public INode DropTarget { get; internal set; }

        public DropLocation Location { get; internal set; }
    }
}
