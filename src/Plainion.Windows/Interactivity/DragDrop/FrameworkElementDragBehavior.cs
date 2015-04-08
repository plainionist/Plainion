using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity.DragDrop
{
    public class FrameworkElementDragBehavior : Behavior<FrameworkElement>
    {
        private bool myIsMouseClicked = false;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
            AssociatedObject.MouseLeave += OnMouseLeave;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            AssociatedObject.MouseLeave -= OnMouseLeave;

            base.OnDetaching();
        }
        
        private void OnMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            myIsMouseClicked = true;
        }

        private void OnMouseLeftButtonUp( object sender, MouseButtonEventArgs e )
        {
            myIsMouseClicked = false;
        }

        private void OnMouseLeave( object sender, MouseEventArgs e )
        {
            if ( myIsMouseClicked )
            {
                var dragObject = AssociatedObject.DataContext as IDragable;
                if ( dragObject != null )
                {
                    var data = new DataObject();
                    data.SetData( dragObject.DataType, AssociatedObject.DataContext );

                    System.Windows.DragDrop.DoDragDrop( AssociatedObject, data, DragDropEffects.Move);
                }
            }

            myIsMouseClicked = false;
        }
    }
}
