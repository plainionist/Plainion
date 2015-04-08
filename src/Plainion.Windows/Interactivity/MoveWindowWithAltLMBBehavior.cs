using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    public class MoveWindowWithAltLMBBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;

            base.OnDetaching();
        }

        private void OnMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            if( Keyboard.Modifiers == ModifierKeys.Alt )
            {
                AssociatedObject.DragMove();
            }
        }
    }
}
