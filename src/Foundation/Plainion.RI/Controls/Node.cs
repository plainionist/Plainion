using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;

namespace Plainion.RI.Controls
{
    public class Node : BindableBase
    {
        private string myText;
        private IReadOnlyCollection<Node> myChildren;
        private bool myIsSelected;
        private bool myIsExpanded;
        private bool myIsChecked;
        private bool myIsInEditMode;

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
                if (SetProperty(ref myIsInEditMode, value))
                {
                    OnPropertyChanged(() => FormattedText);
                }
            }
        }

        public string FormattedText
        {
            get { return string.Format("{0} ({1})", Text, Text.Length); }
        }

        public IReadOnlyCollection<Node> Children
        {
            get { return myChildren; }
            set { SetProperty(ref myChildren, value); }
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

                OnPropertyChanged(() => IsChecked);
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
    }
}
