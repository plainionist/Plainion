
using System.Windows;
using System.Windows.Controls;

namespace Plainion.Windows.Controls.Tree
{
    class ExtendedTreeView : TreeView
    {
        public ExtendedTreeView()
        {
            StateContainer = new StateContainer();
        }

        public StateContainer StateContainer { get; private set; }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NodeItem(StateContainer);
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is NodeItem;
        }
    }
}
