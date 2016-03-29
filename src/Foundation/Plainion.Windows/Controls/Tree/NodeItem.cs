using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Plainion.Windows.Interactivity.DragDrop;

namespace Plainion.Windows.Controls.Tree
{
    public class NodeItem : TreeViewItem, IDropable, IDragable
    {
        private bool myShowChildrenCount;
        //private bool myIsChecked;
        private bool myIsInEditMode;

        public NodeItem()
        {
            ShowChildrenCount = false;

            Loaded += OnLoaded;
        }

        private void OnLoaded( object sender, RoutedEventArgs e )
        {
            Loaded -= OnLoaded;

            if( BindingOperations.GetBindingExpression( this, FormattedTextProperty ) == null
                && BindingOperations.GetMultiBindingExpression( this, FormattedTextProperty ) == null )
            {
                SetBinding( FormattedTextProperty, new Binding() { Path = new PropertyPath( "Text" ), Source = this } );
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NodeItem();
        }

        protected override bool IsItemItsOwnContainerOverride( object item )
        {
            return item is NodeItem;
        }

        public static DependencyProperty TextProperty = DependencyProperty.Register( "Text", typeof( string ), typeof( TreeViewItem ),
            new FrameworkPropertyMetadata( null ) );

        public string Text
        {
            get { return ( string )GetValue( TextProperty ); }
            set { SetValue( TextProperty, value ); }
        }

        public static DependencyProperty FormattedTextProperty = DependencyProperty.Register( "FormattedText", typeof( string ), typeof( TreeViewItem ),
            new FrameworkPropertyMetadata( null ) );

        public string FormattedText
        {
            get { return ( string )GetValue( FormattedTextProperty ); }
            set { SetValue( FormattedTextProperty, value ); }
        }

        public bool IsInEditMode
        {
            get { return myIsInEditMode; }
            set
            {
                if( Text == null && value == true )
                {
                    // we first need to set some dummy text so that the EditableTextBlock control becomes visible again
                    Text = "<empty>";
                }

                if( SetProperty( ref myIsInEditMode, value ) )
                {
                    if( !myIsInEditMode && Text == "<empty>" )
                    {
                        Text = null;
                    }
                }
            }
        }

        private bool SetProperty<T>( ref T storage, T value )
        {
            return true;
        }

        public static DependencyProperty IsFilteredOutProperty = DependencyProperty.Register( "IsFilteredOut", typeof( bool ), typeof( TreeViewItem ),
            new FrameworkPropertyMetadata( false, OnIsFilteredOutChanged ) );

        private static void OnIsFilteredOutChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var self = ( NodeItem )d;
            self.Visibility = self.IsFilteredOut ? Visibility.Collapsed : Visibility.Visible;

            Debug.WriteLine( self.Text + " - " + self.Visibility );
        }

        public bool IsFilteredOut
        {
            get { return ( bool )GetValue( IsFilteredOutProperty ); }
            set { SetValue( IsFilteredOutProperty, value ); }
        }

        public IEnumerable<NodeItem> Children
        {
            get
            {
                return Items
                    .OfType<object>()
                    .Select( item => ItemContainerGenerator.ContainerFromItem( item ) )
                    .OfType<NodeItem>();
            }
            //set
            //{
            //    if( SetProperty( ref myChildren, value ) )
            //    {
            //        if( myChildren != null )
            //        {
            //            foreach( var t in myChildren )
            //            {
            //                PropertyBinding.Observe( () => t.IsChecked, OnChildIsCheckedChanged );
            //            }
            //        }
            //    }
            //}
        }

        private void OnChildIsCheckedChanged( object sender, PropertyChangedEventArgs e )
        {
            OnPropertyChanged( e.PropertyName );
        }

        private void OnPropertyChanged( [CallerMemberName]string p = null )
        {
        }

        //public bool? IsChecked
        //{
        //    get
        //    {
        //        if( myChildren == null )
        //        {
        //            return myIsChecked;
        //        }

        //        if( Children.All( t => t.IsChecked == true ) )
        //        {
        //            return true;
        //        }

        //        if( Children.All( t => !t.IsChecked == true ) )
        //        {
        //            return false;
        //        }

        //        return null;
        //    }
        //    set
        //    {
        //        if( myChildren == null )
        //        {
        //            myIsChecked = value != null && value.Value;
        //        }
        //        else
        //        {
        //            foreach( var t in Children )
        //            {
        //                t.IsChecked = value.HasValue && value.Value;
        //            }
        //        }

        //        OnPropertyChanged();
        //    }
        //}

        internal void ApplyFilter( string filter )
        {
            if( string.IsNullOrWhiteSpace( filter ) )
            {
                IsFilteredOut = false;
            }
            else if( filter == "*" )
            {
                IsFilteredOut = Text == null;
            }
            else
            {
                var node = (INode) DataContext;

                IsFilteredOut = Text == null || !node.Matches(filter);
            }

            foreach( var child in Children )
            {
                child.ApplyFilter( filter );

                if( !child.IsFilteredOut )
                {
                    IsFilteredOut = false;
                }
            }
        }

        public void ExpandAll()
        {
            IsExpanded = true;

            if( Children == null )
            {
                return;
            }

            foreach( var child in Children )
            {
                child.ExpandAll();
            }
        }

        public void CollapseAll()
        {
            IsExpanded = false;

            if( Children == null )
            {
                return;
            }

            foreach( var child in Children )
            {
                child.CollapseAll();
            }
        }

        public bool ShowChildrenCount
        {
            get { return myShowChildrenCount; }
            set
            {
                if( myShowChildrenCount == value )
                {
                    return;
                }

                myShowChildrenCount = value;

                foreach( var child in Children )
                {
                    child.ShowChildrenCount = myShowChildrenCount;
                }
            }
        }

        //public string ChildrenCount
        //{
        //    get
        //    {
        //        return ShowChildrenCount && Children.Count > 0
        //            ? string.Format( "[{0}]", Children.Count )
        //            : string.Empty;
        //    }
        //}

        string IDropable.DataFormat
        {
            get { return typeof( NodeItem ).FullName; }
        }

        bool IDropable.IsDropAllowed( object data, DropLocation location )
        {
            return true;
        }

        void IDropable.Drop( object data, DropLocation location )
        {
            var droppedElement = data as NodeItem;

            if( droppedElement == null )
            {
                return;
            }

            if( object.ReferenceEquals( droppedElement, this ) )
            {
                //if dragged and dropped yourself, don't need to do anything
                return;
            }

            var arg = new NodeDropRequest
            {
                DroppedNode = droppedElement.DataContext,
                DropTarget = DataContext,
                Operation = location
            };

            var editor = ( TreeEditor )LogicalTreeHelper.GetParent( this );
            if( editor.DropCommand != null && editor.DropCommand.CanExecute( arg ) )
            {
                editor.DropCommand.Execute( arg );
            }

            IsExpanded = true;
        }

        Type IDragable.DataType
        {
            get { return typeof( NodeItem ); }
        }
    }
}
