
using System.Collections.Generic;

namespace Plainion.Windows.Controls.Tree
{
    public interface INode
    {
        INode Parent { get; }
        IEnumerable<INode> Children { get; }

        bool Matches(string pattern);
    }
}
