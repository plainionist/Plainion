using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree
{
    /// <summary>
    /// TODO:
    /// - how to handle modification commands?
    /// - how to sync with model?
    /// - how to separate aspects?
    /// </summary>
    public class Node : NotifyingBase, IDropable, IDragable
    {
        private string myText;
        private IReadOnlyCollection<Node> myChildren;
        private ICollectionView myVisibleChildren;
        private bool myShowContentHint;
        private bool myIsSelected;
        private bool myIsExpanded;
        private bool myIsChecked;
        private bool myIsInEditMode;

        public Node()
        {
            ExpandAllCommand = new DelegateCommand(ExpandAll);
            CollapseAllCommand = new DelegateCommand(CollapseAll);

            ShowContentHint = false;

            MouseDownCommand = new DelegateCommand<MouseButtonEventArgs>(OnMouseDown);

            NewCommand = new DelegateCommand(OnAddNewChild);
            EditNodeCommand = new DelegateCommand(OnEditNode);
            DeleteCommand = new DelegateCommand(DeleteChild);
        }

        public string Text
        {
            get { return myText; }
            set { SetProperty(ref myText, value); }
        }

        public bool IsInEditMode
        {
            get { return myIsInEditMode; }
            set
            {
                if (Text == null && value == true)
                {
                    // we first need to set some dummy text so that the EditableTextBlock control becomes visible again
                    Text = "<empty>";
                }

                if (SetProperty(ref myIsInEditMode, value))
                {
                    if (!myIsInEditMode && Text == "<empty>")
                    {
                        Text = null;
                    }
                }
            }
        }

        public ICommand EditNodeCommand { get; private set; }

        public bool IsFilteredOut { get; private set; }

        // TODO: implement
        public string FormattedText
        {
            get { return Text; }
        }

        public IReadOnlyCollection<Node> Children
        {
            get { return myChildren; }
            set
            {
                if (SetProperty(ref myChildren, value))
                {
                    if (myChildren != null)
                    {
                        foreach (var t in myChildren)
                        {
                            PropertyBinding.Observe(() => t.IsChecked, OnChildIsCheckedChanged);
                        }
                    }
                }

                VisibleChildren = null;
            }
        }

        private void OnChildIsCheckedChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public bool? IsChecked
        {
            get
            {
                if (myChildren == null)
                {
                    return myIsChecked;
                }

                if (Children.All(t => t.IsChecked == true))
                {
                    return true;
                }

                if (Children.All(t => !t.IsChecked == true))
                {
                    return false;
                }

                return null;
            }
            set
            {
                if (myChildren == null)
                {
                    myIsChecked = value != null && value.Value;
                }
                else
                {
                    foreach (var t in Children)
                    {
                        t.IsChecked = value.HasValue && value.Value;
                    }
                }

                OnPropertyChanged();
            }
        }

        public ICollectionView VisibleChildren
        {
            get
            {
                if (myVisibleChildren == null && myChildren != null)
                {
                    myVisibleChildren = CollectionViewSource.GetDefaultView(Children);
                    myVisibleChildren.Filter = i => !((Node)i).IsFilteredOut;
                }
                return myVisibleChildren;
            }
            set { SetProperty(ref myVisibleChildren, value); }
        }

        internal void ApplyFilter(string filter)
        {
            var tokens = filter.Split('|');
            filter = tokens[0];
            var threadFilter = tokens.Length > 1 ? tokens[1] : filter;

            if (string.IsNullOrWhiteSpace(filter))
            {
                IsFilteredOut = false;
            }
            else if (filter == "*")
            {
                IsFilteredOut = Text == null;
            }
            else
            {
                IsFilteredOut = (Text == null || !Text.Contains(filter, StringComparison.OrdinalIgnoreCase))
                    && !4242.ToString().Contains(filter);
            }

            foreach (var thread in Children)
            {
                thread.ApplyFilter(threadFilter);

                if (!thread.IsFilteredOut && tokens.Length == 1)
                {
                    IsFilteredOut = false;
                }
            }

            VisibleChildren.Refresh();
        }

        public ICommand ExpandAllCommand { get; private set; }

        public ICommand CollapseAllCommand { get; private set; }

        private void ExpandAll()
        {
            IsExpanded = true;

            foreach (var child in Children)
            {
                child.ExpandAll();
            }
        }

        private void CollapseAll()
        {
            IsExpanded = false;

            foreach (var child in Children)
            {
                child.CollapseAll();
            }
        }

        private void OnAddNewChild()
        {
            //var child = ProjectService.Project.CreateChild( Node );
            //EventAggregator.GetEvent<NodeActivatedEvent>().Publish( child );
        }

        public ICommand NewCommand { get; private set; }

        public bool ShowContentHint
        {
            get { return myShowContentHint; }
            set
            {
                if (myShowContentHint == value)
                {
                    return;
                }

                myShowContentHint = value;

                foreach (var child in Children)
                {
                    child.ShowContentHint = myShowContentHint;
                }
            }
        }

        public string ContentHint
        {
            get
            {
                return ShowContentHint && Children.Count > 0
                    ? string.Format("[{0}]", Children.Count)
                    : string.Empty;
            }
        }

        //private void OnActivated(INode node)
        //{
        //    if (Node == node)
        //    {
        //        Node.Parent.IsExpanded = true;
        //        IsSelected = true;
        //    }
        //}

        //private void OnSelected(INode node)
        //{
        //    if (Node == node)
        //    {
        //        IsSelected = true;
        //    }
        //}

        private void DeleteChild()
        {
            //ProjectService.Project.DeleteNode( Node );
        }

        public ICommand DeleteCommand { get; private set; }

        public bool IsSelected
        {
            get { return myIsSelected; }
            set
            {
                if (SetProperty(ref myIsSelected, value))
                {
                    // EventAggregator.GetEvent<NodeSelectedEvent>().Publish( Node );
                }
            }
        }

        public bool IsExpanded
        {
            get { return myIsExpanded; }
            set { myIsExpanded = value; }
        }

        string IDropable.DataFormat
        {
            get { return typeof(Node).FullName; }
        }

        bool IDropable.IsDropAllowed(object data, DropLocation location)
        {
            return true;
        }

        void IDropable.Drop(object data, DropLocation location)
        {
            var droppedElement = data as Node;

            if (droppedElement == null)
            {
                return;
            }

            if (object.ReferenceEquals(droppedElement, this))
            {
                //if dragged and dropped yourself, don't need to do anything
                return;
            }

            //if( location == DropLocation.Before || location == DropLocation.After )
            //{
            //    var operation = location == DropLocation.Before ? MoveOperation.MoveBefore : MoveOperation.MoveAfter;
            //    ProjectService.Project.MoveNode( droppedElement.Node, Node, operation );
            //}
            //else
            //{
            //    ProjectService.Project.AddChildTo( droppedElement.Node, Node );
            //}

            IsExpanded = true;
        }

        Type IDragable.DataType
        {
            get { return typeof(Node); }
        }

        public ICommand MouseDownCommand { get; private set; }

        private void OnMouseDown(MouseButtonEventArgs args)
        {
            if (args.ClickCount == 2)
            {
                //EventAggregator.GetEvent<NodeActivatedEvent>().Publish( Node );
            }
        }

        private void OnEditNode()
        {
            IsInEditMode = true;
            //EventAggregator.GetEvent<NodeActivatedEvent>().Publish( Node );
        }
    }
}
