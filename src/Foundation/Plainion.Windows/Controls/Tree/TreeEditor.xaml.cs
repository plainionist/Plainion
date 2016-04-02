using System.Collections.Generic;
using System.Linq;
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

            if (NodeStyle == null)
            {
                NodeStyle = (Style)Resources["DefaultNodeStyle"];
            }

            myTree.ItemContainerStyle = NodeStyle;
        }

        public static DependencyProperty FilterLabelProperty = DependencyProperty.Register("FilterLabel", typeof(string), typeof(TreeEditor),
            new FrameworkPropertyMetadata(null));

        public string FilterLabel
        {
            get { return (string)GetValue(FilterLabelProperty); }
            set { SetValue(FilterLabelProperty, value); }
        }

        public static DependencyProperty RootProperty = DependencyProperty.Register("Root", typeof(INode), typeof(TreeEditor),
            new FrameworkPropertyMetadata(null, OnRootChanged));

        private static void OnRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (TreeEditor)d;
            self.myTree.StateContainer.DataContext = self.Root;
        }

        public INode Root
        {
            get { return (INode)GetValue(RootProperty); }
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

            myTree.StateContainer.GetRoot().ApplyFilter(Filter);
        }

        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        void IDropable.Drop(object data, DropLocation location)
        {
            var droppedElement = data as NodeItem;

            if (droppedElement == null)
            {
                return;
            }

            var arg = new NodeDropRequest
            {
                DroppedNode = droppedElement.State.DataContext,
                DropTarget = Root,
                Location = location
            };

            if (DropCommand != null && DropCommand.CanExecute(arg))
            {
                DropCommand.Execute(arg);
            }
        }

        string IDropable.DataFormat
        {
            get { return typeof(NodeItem).FullName; }
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

        public static readonly DependencyProperty NodeStyleProperty = DependencyProperty.Register("NodeStyle", typeof(Style), typeof(TreeEditor),
            new FrameworkPropertyMetadata(null, OnNodeStyleChanged));

        private static void OnNodeStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (TreeEditor)d;
            self.myTree.ItemContainerStyle = self.NodeStyle;
        }

        public Style NodeStyle
        {
            get { return (Style)GetValue(NodeStyleProperty); }
            set { SetValue(NodeStyleProperty, value); }
        }
    }
}
