using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    public class FocusOnClickBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        private void OnPreviewMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            AssociatedObject.Focus();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;

            base.OnDetaching();
        }
    }
}
