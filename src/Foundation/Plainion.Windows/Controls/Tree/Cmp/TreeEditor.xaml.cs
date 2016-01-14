using System.Windows;
using System.Windows.Controls;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    public partial class TreeEditor : UserControl
    {
        public TreeEditor()
        {
            InitializeComponent();
        }

        public static DependencyProperty RootProperty = DependencyProperty.Register("Root", typeof(Node), typeof(TreeEditor),
            new FrameworkPropertyMetadata(null));

        public Node Root
        {
            get { return (Node)GetValue(RootProperty); }
            set { SetValue(RootProperty, value); }
        }

        public static DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(string), typeof(TreeEditor),
            new FrameworkPropertyMetadata(OnFilterChanged));

        private static void OnFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            ((TreeEditor)d).OnFilterChanged();
        }

        private void OnFilterChanged()
        {
            if (Root == null)
            {
                return;
            }

            Root.ApplyFilter(Filter);
        }

        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }
    }
}
