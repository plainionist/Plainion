using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    // http://stackoverflow.com/questions/23316274/inputbindings-work-only-when-focused
    public class PropagateInputBindingsToWindowBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += OnAssociatedObjectLoaded;
        }

       private void OnAssociatedObjectLoaded( object sender, RoutedEventArgs e )
        {
            var frameworkElement = ( FrameworkElement )sender;

            var window = Window.GetWindow( frameworkElement );
            if( window == null )
            {
                return;
            }

            // Move input bindings from the UIElement to the window.
            for( int i = frameworkElement.InputBindings.Count - 1; i >= 0; i-- )
            {
                var inputBinding = ( InputBinding )frameworkElement.InputBindings[ i ];
                window.InputBindings.Add( inputBinding );
                frameworkElement.InputBindings.Remove( inputBinding );
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= OnAssociatedObjectLoaded;

            base.OnDetaching();
        }
    }
}
