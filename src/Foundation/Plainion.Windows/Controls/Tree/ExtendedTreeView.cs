
using System.Windows;
using System.Windows.Controls;


namespace Plainion.Windows.Controls.Tree
{
    class ExtendedTreeView : TreeView
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NodeItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is NodeItem;
        }
    }
}
