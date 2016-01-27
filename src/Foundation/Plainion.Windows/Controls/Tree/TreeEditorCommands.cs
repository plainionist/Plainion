using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree
{
    public class TreeEditorCommands
    {
        public static readonly RoutedCommand ExpandAll = new RoutedCommand();
        public static readonly RoutedCommand CollapseAll = new RoutedCommand();

        public static void RegisterCommandBindings(TreeEditor editor)
        {
            editor.CommandBindings.Add(new CommandBinding(ExpandAll, (sender,e)=>OnExpandAll(editor,e)));
            editor.CommandBindings.Add(new CommandBinding(CollapseAll, (sender,e)=>OnCollapseAll(editor,e)));
        }

        private static void OnExpandAll(object sender, ExecutedRoutedEventArgs e)
        {
            var editor = (TreeEditor)sender;
            if (e.Parameter != null)
            {
                ((Node)e.Parameter).ExpandAll();
            }
            else
            {
               editor. Root.ExpandAll();
            }
        }

        private static void OnCollapseAll(object sender, ExecutedRoutedEventArgs e)
        {
            var editor = (TreeEditor)sender;
            if (e.Parameter != null)
            {
                ((Node)e.Parameter).CollapseAll();
            }
            else
            {
                editor.Root.CollapseAll();
            }
        }
    }
}
