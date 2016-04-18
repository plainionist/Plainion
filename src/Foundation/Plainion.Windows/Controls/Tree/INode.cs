
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Plainion.Windows.Controls.Tree
{
    /// <summary>
    /// Required interface of nodes in <see cref="TreeEditor"/>.
    /// </summary>
    public interface INode
    {
        INode Parent { get; }

        IEnumerable<INode> Children { get; }

        bool Matches(string pattern);
    }
}
