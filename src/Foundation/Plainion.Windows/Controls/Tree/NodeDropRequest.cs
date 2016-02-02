using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree
{
    public class NodeDropRequest
    {
        public object DroppedNode { get; internal set; }

        public object DropTarget { get; internal set; }

        public DropLocation Operation { get; internal set; }
    }
}
