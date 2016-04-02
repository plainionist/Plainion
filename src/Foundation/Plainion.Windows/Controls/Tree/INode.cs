
using System.Collections.Generic;

namespace Plainion.Windows.Controls.Tree
{
    public interface INode
    {
        INode Parent { get; }

        /// <summary>
        /// Returned collection must implement INotifyCollectionChanged if Drag and Drop should be supported
        /// </summary>
        IEnumerable<INode> Children { get; }

        bool Matches(string pattern);
    }
}
