using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree
{
    class NodeState
    {
        private NodeItem myAttachedView;
        private readonly StateContainer myContainer;
        private bool myIsFilteredOut;
        private bool myIsExpanded;

        public NodeState(INode dataContext, StateContainer container)
        {
            DataContext = dataContext;
            myContainer = container;
        }

        public INode DataContext { get; private set; }

        public bool IsFilteredOut
        {
            get { return myIsFilteredOut; }
            set { SetProperty(ref myIsFilteredOut, value); }
        }

        public bool IsExpanded
        {
            get { return myIsExpanded; }
            set
            {
                // always update - we may not have latest state
                myIsExpanded = value;
                SetViewProperty(myIsExpanded);
            }
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;

            SetViewProperty(storage, propertyName);

            return true;
        }

        private void SetViewProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (myAttachedView == null)
            {
                return;
            }

            var dependencyPropertyField = myAttachedView.GetType()
                .GetField(propertyName + "Property", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            if (dependencyPropertyField != null)
            {
                var expr = myAttachedView.GetBindingExpression((DependencyProperty)dependencyPropertyField.GetValue(myAttachedView));
                if (expr != null)
                {
                    expr.ResolvedSource.GetType().GetProperty(expr.ResolvedSourcePropertyName).SetValue(expr.ResolvedSource, myIsExpanded);
                    expr.UpdateTarget();

                    return;
                }
            }

            myAttachedView.GetType().GetProperty(propertyName).SetValue(myAttachedView, value);
        }

        public void Attach(NodeItem nodeItem)
        {
            myAttachedView = nodeItem;

            myAttachedView.IsFilteredOut = IsFilteredOut;

            var expr = myAttachedView.GetBindingExpression(TreeViewItem.IsExpandedProperty);
            if (expr != null)
            {
                // property bound to INode impl --> this is the master
                IsExpanded = myAttachedView.IsExpanded;
            }
            else
            {
                // no binding --> we are the master
                myAttachedView.IsExpanded = IsExpanded;
            }
        }

        public void ApplyFilter(string filter)
        {
            string[] tokens = null;

            if (filter == null)
            {
                IsFilteredOut = false;
            }
            else
            {
                // if this has no parent it is Root - no need to filter root
                if (GetParent(this) != null)
                {
                    tokens = filter.Split('/');
                    var levelFilter = tokens.Length == 1 ? filter : tokens[GetDepth()];
                    if (string.IsNullOrWhiteSpace(levelFilter))
                    {
                        IsFilteredOut = false;
                    }
                    else
                    {
                        IsFilteredOut = !DataContext.Matches(levelFilter);
                    }
                }
            }

            foreach (var child in GetChildren())
            {
                child.ApplyFilter(filter);

                if (!child.IsFilteredOut && tokens != null && tokens.Length == 1)
                {
                    IsFilteredOut = false;
                }
            }
        }

        private int GetDepth()
        {
            int depth = 0;

            var parent = GetParent(this);
            while (parent != null)
            {
                parent = GetParent(parent);
                depth++;
            }

            // ignore invisible root
            return depth - 1;
        }

        private NodeState GetParent(NodeState state)
        {
            return state.DataContext.Parent == null ? null : myContainer.GetOrCreate(state.DataContext.Parent);
        }

        private IEnumerable<NodeState> GetChildren()
        {
            if (DataContext.Children == null)
            {
                return Enumerable.Empty<NodeState>();
            }

            return DataContext.Children
                .Select(myContainer.GetOrCreate);
        }

        public void ExpandAll()
        {
            IsExpanded = true;

            foreach (var child in GetChildren())
            {
                child.ExpandAll();
            }
        }

        public void CollapseAll()
        {
            IsExpanded = false;

            foreach (var child in GetChildren())
            {
                child.CollapseAll();
            }
        }

        internal bool IsDropAllowed(DropLocation location)
        {
            if (location == DropLocation.InPlace)
            {
                var dragDropSupport = DataContext as IDragDropSupport;
                if (dragDropSupport != null)
                {
                    return dragDropSupport.IsDropAllowed;
                }
            }
            else
            {
                var dragDropSupport = DataContext.Parent as IDragDropSupport;
                if (dragDropSupport != null)
                {
                    return dragDropSupport.IsDropAllowed;
                }
            }

            return true;
        }
    }
}
