using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    public class PanOnLMBBehavior : Behavior<ScrollViewer>
    {
        private bool myWaitForDrag;
        private bool myDragging;
        private Point myDragStart;
        private Point myPanOrigin;
        
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Focusable = false;
            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.LostMouseCapture += OnLostMouseCapture;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.LostMouseCapture -= OnLostMouseCapture;

            base.OnDetaching();
        }
        
        private void OnMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            if( Keyboard.Modifiers != ModifierKeys.None)
            {
                return;
            }
            
            var point = e.GetPosition( AssociatedObject );
            if( point.X < AssociatedObject.ViewportWidth && point.Y < AssociatedObject.ViewportHeight )
            {
                myWaitForDrag = true;
                myDragStart = point;
                myPanOrigin = new Point( AssociatedObject.HorizontalOffset, AssociatedObject.VerticalOffset );
            }
        }

        private void OnMouseMove( object sender, MouseEventArgs e )
        {
            if( Keyboard.Modifiers != ModifierKeys.None )
            {
                return;
            }
            
            if( myWaitForDrag && ( e.GetPosition( AssociatedObject ) - myDragStart ).Length > 1 )
            {
                myWaitForDrag = false;
                myDragging = AssociatedObject.CaptureMouse();
            }

            if( myDragging )
            {
                e.Handled = true;

                var offset = myPanOrigin - ( e.GetPosition( AssociatedObject ) - myDragStart );
                AssociatedObject.ScrollToHorizontalOffset( offset.X );
                AssociatedObject.ScrollToVerticalOffset( offset.Y );
            }
        }

        private void OnMouseLeftButtonUp( object sender, MouseButtonEventArgs e )
        {
            if( Keyboard.Modifiers != ModifierKeys.None )
            {
                return;
            }
            
            myWaitForDrag = false;

            if( myDragging )
            {
                e.Handled = true;
                myDragging = false;
                AssociatedObject.ReleaseMouseCapture();
            }
        }

        private void OnLostMouseCapture( object sender, MouseEventArgs e )
        {
            myDragging = false;
        }
    }
}
