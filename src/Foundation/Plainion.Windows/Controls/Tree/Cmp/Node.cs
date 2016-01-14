using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    public class Node : NodeBase
    {
        private IReadOnlyCollection<Node> myChildren;
        private ICollectionView myVisibleChildren;

        // only used if here are no threads
        private bool myIsChecked;

        public Node()
        {
            // default if there are no threads
            myIsChecked = false;
        }

        public string DisplayText
        {
            get { return Name == null ? string.Format("Pid={0}", 4242) : string.Format("(Pid={0})", 4242); }
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
                            PropertyChangedEventManager.AddHandler(t, OnThreadIsCheckedChanged, "IsChecked");
                        }
                    }
                }

                myVisibleChildren = null;
                OnPropertyChanged(() => VisibleChildren);
            }
        }

        private void OnThreadIsCheckedChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(() => IsChecked);
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
                    myIsChecked = value == null ? false : value.Value;
                }
                else
                {
                    foreach (var t in Children)
                    {
                        t.IsChecked = value.HasValue && value.Value;
                    }
                }

                OnPropertyChanged("IsChecked");
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

                    OnPropertyChanged("VisibleThreads");
                }
                return myVisibleChildren;
            }
        }

        internal override void ApplyFilter(string filter)
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
                IsFilteredOut = Name == null;
            }
            else
            {
                IsFilteredOut = (Name == null || !Name.Contains(filter, StringComparison.OrdinalIgnoreCase))
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
    }
}
