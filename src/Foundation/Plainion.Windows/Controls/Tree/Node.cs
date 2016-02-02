using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree
{
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
            ShowContentHint = false;
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

        public void ExpandAll()
        {
            IsExpanded = true;

            if (Children == null)
            {
                return;
            }

            foreach (var child in Children)
            {
                child.ExpandAll();
            }
        }

        public void CollapseAll()
        {
            IsExpanded = false;

            if (Children == null)
            {
                return;
            }

            foreach (var child in Children)
            {
                child.CollapseAll();
            }
        }

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

        public bool IsSelected
        {
            get { return myIsSelected; }
            set { SetProperty(ref myIsSelected, value); } 
        }

        public bool IsExpanded
        {
            get { return myIsExpanded; }
            set { SetProperty(ref myIsExpanded, value); }
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

            var arg = new NodeDropRequest
            {
                DroppedNode = droppedElement.Model,
                DropTarget = Model,
                Operation = location
            };

            IsExpanded = true;
        }

        Type IDragable.DataType
        {
            get { return typeof(Node); }
        }

        public object Model { get; set; }
    }
}
