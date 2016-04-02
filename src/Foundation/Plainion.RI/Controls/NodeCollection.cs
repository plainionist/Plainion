using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Plainion.RI.Controls
{
    internal class NodeCollection : ObservableCollection<Node>
    {
        private Node myParent;

        public NodeCollection( Node parent )
        {
            myParent = parent;
        }

    }
}
