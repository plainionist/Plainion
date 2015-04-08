using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Plainion.Windows.Interactivity.DragDrop
{
    internal class DropSortableItemsAdorner : Adorner
    {
        private AdornerLayer myAdornerLayer;
        private FrameworkElement myOwner;
        private DropLocation myLocation;

        public DropSortableItemsAdorner( FrameworkElement adornedElement )
            : base( adornedElement )
        {
            myOwner = adornedElement;

            IsHitTestVisible = false;
            myLocation = DropLocation.InPlace;

            myAdornerLayer = AdornerLayer.GetAdornerLayer( AdornedElement );
            myAdornerLayer.Add( this );
        }

        internal void Update( DropLocation location )
        {
            myLocation = location;

            myAdornerLayer.Update( AdornedElement );
            Visibility = Visibility.Visible;
        }

        public void Remove()
        {
            Visibility = Visibility.Collapsed;
        }

        protected override void OnRender( DrawingContext dc )
        {
            Rect rect;

            if ( myLocation == DropLocation.InPlace )
            {
                rect = new Rect( new Size( myOwner.ActualWidth, myOwner.ActualHeight ) );
                rect.Inflate( -1, -1 );
            }
            else
            {
                rect = new Rect( new Size( myOwner.ActualWidth, myOwner.ActualHeight * 0.2 ) );
                if ( myLocation == DropLocation.Before )
                {
                    rect.Offset( 0, -myOwner.ActualHeight * 0.1 );
                }
                else if ( myLocation == DropLocation.After )
                {
                    rect.Offset( 0, myOwner.ActualHeight * 0.9 );
                }
                rect.Inflate( -1, 0 );
            }

            var brush = new SolidColorBrush( Colors.LightSkyBlue );
            brush.Opacity = 0.5;

            var pen = new Pen( new SolidColorBrush( Colors.White ), 1 );

            dc.DrawRectangle( brush, pen, rect );
        }
    }
}
