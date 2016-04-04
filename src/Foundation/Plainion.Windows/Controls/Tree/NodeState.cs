using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Plainion.Windows.Controls.Tree
{
    class NodeState
    {
        private NodeItem myAttachedView;
        private readonly StateContainer myContainer;
        private bool myIsFilteredOut;
        private bool myIsExpanded;

        public NodeState( INode dataContext, StateContainer container )
        {
            DataContext = dataContext;
            myContainer = container;
        }

        public INode DataContext { get; private set; }

        public bool IsFilteredOut
        {
            get { return myIsFilteredOut; }
            set { SetProperty( ref myIsFilteredOut, value ); }
        }

        public bool IsExpanded
        {
            get { return myIsExpanded; }
            set { SetProperty( ref myIsExpanded, value ); }
        }

        private bool SetProperty<T>( ref T storage, T value, [CallerMemberName] string propertyName = null )
        {
            if( object.Equals( storage, value ) )
            {
                return false;
            }

            storage = value;

            if( myAttachedView != null )
            {
                myAttachedView.GetType().GetProperty( propertyName ).SetValue( myAttachedView, storage );
            }

            return true;
        }

        public void Attach( NodeItem nodeItem )
        {
            myAttachedView = nodeItem;

            myAttachedView.IsFilteredOut = IsFilteredOut;
        }

        public void ApplyFilter( string filter )
        {
            string[] tokens = null;

            if( filter == null )
            {
                IsFilteredOut = false;
            }
            else
            {
                // if this has no parent it is Root - no need to filter root
                if( GetParent( this ) != null )
                {
                    tokens = filter.Split( '/' );
                    var levelFilter = tokens.Length == 1 ? filter : tokens[ GetDepth() ];
                    if( string.IsNullOrWhiteSpace( levelFilter ) )
                    {
                        IsFilteredOut = false;
                    }
                    else
                    {
                        IsFilteredOut = !DataContext.Matches( levelFilter );
                    }
                }
            }

            foreach( var child in GetChildren() )
            {
                child.ApplyFilter( filter );

                if( !child.IsFilteredOut && tokens != null && tokens.Length == 1 )
                {
                    IsFilteredOut = false;
                }
            }
        }

        private int GetDepth()
        {
            int depth = 0;

            var parent = GetParent( this );
            while( parent != null )
            {
                parent = GetParent( parent );
                depth++;
            }

            // ignore invisible root
            return depth - 1;
        }

        private NodeState GetParent( NodeState state )
        {
            return state.DataContext.Parent == null ? null : myContainer.GetOrCreate( state.DataContext.Parent );
        }

        private IEnumerable<NodeState> GetChildren()
        {
            if( DataContext.Children == null )
            {
                return Enumerable.Empty<NodeState>();
            }

            return DataContext.Children
                .Select( myContainer.GetOrCreate );
        }

        public void ExpandAll()
        {
            IsExpanded = true;

            foreach( var child in GetChildren() )
            {
                child.ExpandAll();
            }
        }

        public void CollapseAll()
        {
            IsExpanded = false;

            foreach( var child in GetChildren() )
            {
                child.CollapseAll();
            }
        }
    }
}
