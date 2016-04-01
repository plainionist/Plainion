
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            return new NodeItem( StateContainer );
        }

        protected override bool IsItemItsOwnContainerOverride( object item )
        {
            return item is NodeItem;
        }

        public static DependencyProperty ItemForContextMenuProperty = DependencyProperty.Register( "ItemForContextMenu", typeof( NodeItem ), typeof( ExtendedTreeView ),
            new FrameworkPropertyMetadata( null ) );

        public NodeItem ItemForContextMenu
        {
            get { return ( NodeItem )GetValue( ItemForContextMenuProperty ); }
            set { SetValue( ItemForContextMenuProperty, value ); }
        }

        // TODO: into behavior?
        protected override void OnPreviewMouseRightButtonDown( MouseButtonEventArgs e )
        {
            ItemForContextMenu = null;

            var nodeItem = FindContainer( e.OriginalSource as DependencyObject );

            if( nodeItem != null )
            {
                ItemForContextMenu = nodeItem;

                nodeItem.Focus();
                e.Handled = true;
            }
        }

        private NodeItem FindContainer( DependencyObject source )
        {
            while( source != null && !( source is NodeItem ) )
            {
                source = VisualTreeHelper.GetParent( source );
            }
            return source as NodeItem;
        }
    }
}
