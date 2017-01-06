using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree
{
    /// <summary>
    /// Send as parameter with the <see cref="TreeEditor.DropCommand"/> to specify the requested DragDrop action.
    /// </summary>
    public class NodeDropRequest
    {
        public INode DroppedNode { get; internal set; }

        public INode DropTarget { get; internal set; }

        public DropLocation Location { get; internal set; }
    }
}
