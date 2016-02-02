using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree
{
    public partial class TreeEditor : UserControl, IDropable
    {
        public TreeEditor()
        {
            InitializeComponent();

            TreeEditorCommands.RegisterCommandBindings(this);
        }

        public static DependencyProperty FilterLabelProperty = DependencyProperty.Register("FilterLabel", typeof(string), typeof(TreeEditor),
            new FrameworkPropertyMetadata(null));

        public string FilterLabel
        {
            get { return (string)GetValue(FilterLabelProperty); }
            set { SetValue(FilterLabelProperty, value); }
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

        void IDropable.Drop(object data, DropLocation location)
        {
            var droppedElement = data as Node;

            if (droppedElement == null)
            {
                return;
            }

            //ProjectService.Project.AddChildTo( droppedElement.Node, Node );
        }

        string IDropable.DataFormat
        {
            get
            {
                return typeof(Node).FullName;
            }
        }

        bool IDropable.IsDropAllowed(object data, DropLocation location)
        {
            return true;
        }

        public static DependencyProperty ExpandAllCommandProperty = DependencyProperty.Register("ExpandAllCommand", typeof(ICommand), typeof(TreeEditor),
            new FrameworkPropertyMetadata(TreeEditorCommands.ExpandAll));

        public ICommand ExpandAllCommand
        {
            get { return (ICommand)GetValue(ExpandAllCommandProperty); }
            set { SetValue(ExpandAllCommandProperty, value); }
        }

        public static DependencyProperty CollapseAllCommandProperty = DependencyProperty.Register("CollapseAllCommand", typeof(ICommand), typeof(TreeEditor),
            new FrameworkPropertyMetadata(TreeEditorCommands.CollapseAll));

        public ICommand CollapseAllCommand
        {
            get { return (ICommand)GetValue(CollapseAllCommandProperty); }
            set { SetValue(CollapseAllCommandProperty, value); }
        }

        public static DependencyProperty AddChildCommandProperty = DependencyProperty.Register("AddChildCommand", typeof(ICommand), typeof(TreeEditor),
            new FrameworkPropertyMetadata(null));

        public ICommand AddChildCommand
        {
            get { return (ICommand)GetValue(AddChildCommandProperty); }
            set { SetValue(AddChildCommandProperty, value); }
        }

        public static DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(TreeEditor),
            new FrameworkPropertyMetadata(null));

        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public static DependencyProperty EditCommandProperty = DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(TreeEditor),
            new FrameworkPropertyMetadata(TreeEditorCommands.Edit));

        public ICommand EditCommand
        {
            get { return (ICommand)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }

        /// <summary>
        /// Parameter will be of type <see cref="NodeDropRequest"/>.
        /// </summary>
        public static DependencyProperty DropCommandProperty = DependencyProperty.Register("DropCommand", typeof(ICommand), typeof(TreeEditor),
            new FrameworkPropertyMetadata(null));

        public ICommand DropCommand
        {
            get { return (ICommand)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }
    }
}
