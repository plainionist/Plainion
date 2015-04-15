using System;
using System.Windows;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity.DragDrop
{
    public class DropSortableItemsBehavior : Behavior<FrameworkElement>
    {
        private string myDataFormat;
        private DropSortableItemsAdorner myAdorner;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;

            AssociatedObject.DragEnter += OnDragEnter;
            AssociatedObject.DragOver += OnDragOver;
            AssociatedObject.DragLeave += OnDragLeave;
            AssociatedObject.Drop += OnDrop;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DragEnter -= OnDragEnter;
            AssociatedObject.DragOver -= OnDragOver;
            AssociatedObject.DragLeave -= OnDragLeave;
            AssociatedObject.Drop -= OnDrop;
        
            base.OnDetaching();
        }

        private void OnDrop( object sender, DragEventArgs e )
        {
            if ( myDataFormat != null && e.Data.GetDataPresent( myDataFormat ) )
            {
                var target = AssociatedObject.DataContext as IDropable;
                target.Drop( e.Data.GetData( myDataFormat ), (DropLocation)e.Data.GetData( typeof( DropLocation ) ) );
            }

            myAdorner.Remove();

            e.Handled = true;
        }

        private void OnDragLeave( object sender, DragEventArgs e )
        {
            myAdorner.Remove();

            e.Handled = true;
        }

        private void OnDragOver( object sender, DragEventArgs e )
        {
            e.Effects = DragDropEffects.None;

            if ( myDataFormat != null )
            {
                if ( e.Data.GetDataPresent( myDataFormat ) )
                {
                    e.Effects = DragDropEffects.Move;

                    var location = GetDropLocation( e );

                    e.Data.SetData( typeof( DropLocation ), location );

                    myAdorner.Update( location );
                }
            }

            e.Handled = true;
        }

        private DropLocation GetDropLocation( DragEventArgs e )
        {
            var pos = e.GetPosition( AssociatedObject );

            if ( pos.Y < AssociatedObject.ActualHeight * 0.2 )
            {
                return DropLocation.Before;
            }
            else if ( pos.Y > AssociatedObject.ActualHeight * 0.8 )
            {
                return DropLocation.After;
            }
            else
            {
                return DropLocation.InPlace;
            }
        }

        private void OnDragEnter( object sender, DragEventArgs e )
        {
            //if the DataContext implements IDropable, record the data type that can be dropped
            if ( myDataFormat == null )
            {
                var dropObject = AssociatedObject.DataContext as IDropable;
                if ( dropObject != null )
                {
                    myDataFormat = dropObject.DataFormat;
                }

                myAdorner = new DropSortableItemsAdorner( AssociatedObject );
            }

            e.Handled = true;
        }
    }
}
