using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree
{
    public class TreeEditorCommands
    {
        public static readonly RoutedCommand ExpandAll = new RoutedCommand();
        public static readonly RoutedCommand CollapseAll = new RoutedCommand();

        public static readonly RoutedCommand Edit = new RoutedCommand();

        public static void RegisterCommandBindings(TreeEditor editor)
        {
            editor.CommandBindings.Add(new CommandBinding(ExpandAll, (sender, e) => OnExpandAll(editor, (NodeItem)e.Parameter)));
            editor.CommandBindings.Add(new CommandBinding(CollapseAll, (sender, e) => OnCollapseAll(editor, (NodeItem)e.Parameter)));

            editor.CommandBindings.Add(new CommandBinding(Edit, (sender, e) => OnEdit(editor, (NodeItem)e.Parameter)));
        }

        private static void OnExpandAll(TreeEditor editor, NodeItem node)
        {
            //if (node != null)
            //{
            //    node.ExpandAll();
            //}
            //else
            //{
            //    foreach( var item in editor.GetRootItems() )
            //    {
            //        item.ExpandAll();
            //    }
            //}
        }

        private static void OnCollapseAll(TreeEditor editor, NodeItem node)
        {
            //if (node != null)
            //{
            //    node.CollapseAll();
            //}
            //else
            //{
            //    foreach( var item in editor.GetRootItems() )
            //    {
            //        item.CollapseAll();
            //    }
            //}
        }

        private static void OnEdit(TreeEditor editor, NodeItem node)
        {
            node.IsInEditMode = true;
        }
    }
}
