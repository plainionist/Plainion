using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Plainion.Windows.Controls.Tree;
using Prism.Mvvm;

namespace Plainion.RI.Controls
{
    class Node : BindableBase, INode
    {
        private string myId;
        private string myName;
        private bool myIsSelected;
        private bool myIsExpanded;
        private bool myIsChecked;
        private bool myIsInEditMode;
        private Node myParent;

        public Node()
        {
            Children = new NodeCollection( this );
        }

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
            get { return Children; }
        }

        public NodeCollection Children { get; private set; }

        public INode Parent
        {
            get { return myParent; }
            internal set
            {
                if( myParent == value )
                {
                    return;
                }

                if( myParent != null )
                {
                    myParent.Children.Remove( this );
                }

                SetProperty( ref myParent, (Node)value );
            }
        }

        public bool? IsChecked
        {
            get
            {
                if( !Children.Any() )
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
                if( !Children.Any() )
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

        bool INode.Matches( string pattern )
        {
            if( pattern == "*" )
            {
                return Name != null;
            }

            return ( Name != null && Name.Contains( pattern, StringComparison.OrdinalIgnoreCase ) )
                || ( Id != null && Id.Contains( pattern, StringComparison.OrdinalIgnoreCase ) );
        }
    }
}
