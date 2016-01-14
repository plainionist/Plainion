using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    // TODO: how to connect model?
    public class Node : BindableBase
    {
        private IReadOnlyCollection<Node> myChildren;
        private ICollectionView myVisibleChildren;

        private bool myIsChecked;
        private bool myIsInEditMode;
        private string myText;

        public Node()
        {
            EditNodeCommand = new DelegateCommand(() => IsInEditMode = true);
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
                            PropertyChangedEventManager.AddHandler(t, OnChildIsCheckedChanged, "IsChecked");
                        }
                    }
                }

                myVisibleChildren = null;
                OnPropertyChanged(() => VisibleChildren);
            }
        }

        private void OnChildIsCheckedChanged(object sender, PropertyChangedEventArgs e)
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

                OnPropertyChanged(() => IsChecked);
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

                    OnPropertyChanged(() => VisibleChildren);
                }
                return myVisibleChildren;
            }
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
    }
}
