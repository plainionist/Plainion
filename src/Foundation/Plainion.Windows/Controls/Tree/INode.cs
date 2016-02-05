
using System.Collections.Generic;

namespace Plainion.Windows.Controls.Tree
{
    public interface INode
    {
        IEnumerable<INode> Children { get; }
    }
}
