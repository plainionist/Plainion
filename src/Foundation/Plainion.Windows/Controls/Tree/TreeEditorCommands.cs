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
            editor.CommandBindings.Add(new CommandBinding(ExpandAll, (sender, e) => OnExpandAll(editor, (Node)e.Parameter)));
            editor.CommandBindings.Add(new CommandBinding(CollapseAll, (sender, e) => OnCollapseAll(editor, (Node)e.Parameter)));

            editor.CommandBindings.Add(new CommandBinding(Edit, (sender, e) => OnEdit(editor, (Node)e.Parameter)));
        }

        private static void OnExpandAll(TreeEditor editor, Node node)
        {
            if (node != null)
            {
                node.ExpandAll();
            }
            else
            {
                editor.Root.ExpandAll();
            }
        }

        private static void OnCollapseAll(TreeEditor editor, Node node)
        {
            if (node != null)
            {
                node.CollapseAll();
            }
            else
            {
                editor.Root.CollapseAll();
            }
        }

        private static void OnEdit(TreeEditor editor, Node node)
        {
            node.IsInEditMode = true;
        }
    }
}
