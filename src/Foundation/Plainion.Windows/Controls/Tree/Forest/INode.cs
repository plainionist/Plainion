using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Plainion.Windows.Controls.Tree.Forest
{
    public interface INode : INotifyPropertyChanged
    {
        INode Parent { get; }

        bool IsExpanded { get; set; }

        string Text { get; set; }

        ObservableCollection<INode> Children { get; }
    }
}
