using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree
{
    class NodeItem : NotifyingBase, IDropable, IDragable
    {
        private string myText;
        private IReadOnlyCollection<NodeItem> myChildren;
        private ICollectionView myVisibleChildren;
        private bool myShowChildrenCount;
        private bool myIsChecked;
        private bool myIsInEditMode;

        public NodeItem()
        {
            ShowChildrenCount = false;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NodeItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is NodeItem;
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

        // TODO: implement ("ProcessName (PID)")
        public string FormattedText
        {
            get { return Text; }
        }

        public IReadOnlyCollection<NodeItem> Children
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
                    myVisibleChildren.Filter = i => !((NodeItem)i).IsFilteredOut;
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

        public bool ShowChildrenCount
        {
            get { return myShowChildrenCount; }
            set
            {
                if (myShowChildrenCount == value)
                {
                    return;
                }

                myShowChildrenCount = value;

                foreach (var child in Children)
                {
                    child.ShowChildrenCount = myShowChildrenCount;
                }
            }
        }

        public string ChildrenCount
        {
            get
            {
                return ShowChildrenCount && Children.Count > 0
                    ? string.Format("[{0}]", Children.Count)
                    : string.Empty;
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

        void IDropable.Drop(object data, DropLocation location)
        {
            var droppedElement = data as NodeItem;

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
            get { return typeof(NodeItem); }
        }

        public object Model { get; set; }
    }
}
