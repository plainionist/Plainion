using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree.Forest
{
    public class Node : BindableBase, IDropable, IDragable
    {
        private string myText;
        private bool myIsSelected;
        private ObservableCollection<Node> myChildren;
        private ICollectionView myVisibleChildren;
        private bool myShowContentHint;
        private bool myIsExpanded;

        internal Node()
        {
            NewCommand = new DelegateCommand(OnAddNewChild);
            ExpandAllCommand = new DelegateCommand(ExpandAll);
            CollapseAllCommand = new DelegateCommand(CollapseAll);

            ShowContentHint = false;

            MouseDownCommand = new DelegateCommand<MouseButtonEventArgs>(OnMouseDown);
            EditNodeCommand = new DelegateCommand(OnEditNode);
            DeleteCommand = new DelegateCommand(DeleteChild);
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

        public ObservableCollection<Node> Children
        {
            get { return myChildren; }
            set
            {
                if (SetProperty(ref myChildren, value))
                {
                }

                myVisibleChildren = null;
                OnPropertyChanged(() => VisibleChildren);
            }
        }

        public ICollectionView VisibleChildren
        {
            get
            {
                if (myVisibleChildren == null)
                {
                    myVisibleChildren = CollectionViewSource.GetDefaultView(Children);
                    myVisibleChildren.Filter = i => !((Node)i).IsFilteredOut;

                    OnPropertyChanged("VisibleChildren");
                }
                return myVisibleChildren;
            }
        }

        public ICommand EditNodeCommand { get; private set; }

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

        public ICommand DeleteCommand
        {
            get;
            private set;
        }

        public string Text
        {
            get { return myText; }
            set { myText=value;}
        }

        public bool IsSelected
        {
            get { return myIsSelected; }
            set
            {
                if (myIsSelected == value)
                {
                    return;
                }

                myIsSelected = value;
                OnPropertyChanged("IsSelected");

                if (myIsSelected)
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
            get
            {
                return typeof(Node);
            }
        }

        public bool IsFilteredOut
        {
            get;
            private set;
        }

        internal void ApplyFilter(string filter)
        {
            IsFilteredOut = Text.IndexOf(filter, StringComparison.OrdinalIgnoreCase) < 0;

            foreach (var child in Children)
            {
                child.ApplyFilter(filter);

                if (!child.IsFilteredOut)
                {
                    IsFilteredOut = false;
                }
            }

            VisibleChildren.Refresh();
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
            //EventAggregator.GetEvent<NodeActivatedEvent>().Publish( Node );
        }
    }
}
