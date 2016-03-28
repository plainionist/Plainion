using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Mvvm;
using Plainion.Collections;
using Plainion.Windows.Controls.Tree;

namespace Plainion.RI.Controls
{
    public class Node : BindableBase, INode
    {
        private string myId;
        private string myName;
        private IList<Node> myChildren;
        private bool myIsSelected;
        private bool myIsExpanded;
        private bool myIsChecked;
        private bool myIsInEditMode;

        public string Id
        {
            get { return myId; }
            set { SetProperty( ref myId, value ); }
        }

        public string Name
        {
            get { return myName; }
            set { SetProperty( ref myName, value ); }
        }

        public bool IsInEditMode
        {
            get { return myIsInEditMode; }
            set
            {
                if( SetProperty( ref myIsInEditMode, value ) )
                {
                    // OnPropertyChanged(() => FormattedText);
                }
            }
        }

        IEnumerable<INode> INode.Children
        {
            get { return myChildren; }
        }

        public IList<Node> Children
        {
            get { return myChildren; }
            set { SetProperty( ref myChildren, value ); }
        }

        public bool? IsChecked
        {
            get
            {
                if( myChildren == null )
                {
                    return myIsChecked;
                }

                if( Children.All( t => t.IsChecked == true ) )
                {
                    return true;
                }

                if( Children.All( t => !t.IsChecked == true ) )
                {
                    return false;
                }

                return null;
            }
            set
            {
                if( myChildren == null )
                {
                    myIsChecked = value != null && value.Value;
                }
                else
                {
                    foreach( var t in Children )
                    {
                        t.IsChecked = value.HasValue && value.Value;
                    }
                }

                OnPropertyChanged( () => IsChecked );
            }
        }

        public bool IsSelected
        {
            get { return myIsSelected; }
            set { SetProperty( ref myIsSelected, value ); }
        }

        public bool IsExpanded
        {
            get { return myIsExpanded; }
            set { SetProperty( ref myIsExpanded, value ); }
        }
    }
}
