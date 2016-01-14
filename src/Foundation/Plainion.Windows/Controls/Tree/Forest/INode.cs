using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Plainion.Windows.Controls.Tree.Forest
{
    public interface INode : INotifyPropertyChanged
    {
        INode Parent { get; }

        bool IsExpanded { get; set; }

        string Caption { get; set; }

        string Id { get; set; }

        string Origin { get; set; }

        ObservableCollection<INode> Children { get; }
    }
}
