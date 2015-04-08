using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    // initial version from: http://www.codeproject.com/Articles/412417/Managing-Multiple-selection-in-View-Model-NET-Metr
    public class MultiSelectBehavior : Behavior<ListBox>
    {
        public MultiSelectBehavior()
        {
            SelectedItems = new ObservableCollection<object>();
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            (( INotifyCollectionChanged )SelectedItems).CollectionChanged -= OnSelectedItemsCollectionChanged;
            
            foreach( var item in e.RemovedItems )
            {
                SelectedItems.Remove( item );
            }

            foreach( var item in e.AddedItems )
            {
                SelectedItems.Add( item );
            }

            ( ( INotifyCollectionChanged )SelectedItems ).CollectionChanged += OnSelectedItemsCollectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.SelectionChanged -= OnSelectionChanged;
        }

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register( "SelectedItems",
            typeof( IList ), typeof( MultiSelectBehavior ),
            new FrameworkPropertyMetadata( new ObservableCollection<object>(), OnSelectedItemsChanged ) );

        public IList SelectedItems
        {
            get { return ( IList )GetValue( SelectedItemsProperty ); }
            set { SetValue( SelectedItemsProperty, value ); }
        }

        private static void OnSelectedItemsChanged( DependencyObject sender, DependencyPropertyChangedEventArgs e )
        {
            var owner =  ( MultiSelectBehavior )sender ;
            
            (( INotifyCollectionChanged )e.OldValue).CollectionChanged -= owner.OnSelectedItemsCollectionChanged;

            (( INotifyCollectionChanged )e.NewValue).CollectionChanged += owner.OnSelectedItemsCollectionChanged;
        }

        private void OnSelectedItemsCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            AssociatedObject.SelectionChanged -= OnSelectionChanged;

            if( e.OldItems != null )
            {
                foreach( var item in e.OldItems )
                {
                    AssociatedObject.SelectedItems.Remove( item );
                }
            }

            if( e.NewItems != null )
            {
                foreach( var item in e.NewItems )
                {
                    AssociatedObject.SelectedItems.Add( item );
                }
            }

            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }
    }
}
