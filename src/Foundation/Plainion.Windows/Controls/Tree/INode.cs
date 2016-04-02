
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Plainion.Windows.Controls.Tree
{
    public interface INode
    {
        INode Parent { get; }

        IEnumerable<INode> Children { get; }

        bool Matches(string pattern);
    }
}
