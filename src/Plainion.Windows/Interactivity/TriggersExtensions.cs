using System.Windows;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    /// <summary>
    /// Extension to allow adding triggers via Styles
    /// </summary>
    // http://www.biggle.de/blog/interaction-event-trigger-als-style-auslagern
    public static class TriggersExtension
    {
        public static Triggers GetTriggers( DependencyObject obj )
        {
            return (Triggers)obj.GetValue( TriggersProperty );
        }

        public static void SetTriggers( DependencyObject obj, Triggers value )
        {

            obj.SetValue( TriggersProperty, value );
        }

        public static readonly DependencyProperty TriggersProperty =
            DependencyProperty.RegisterAttached( "Triggers", typeof( Triggers ), typeof( TriggersExtension ), new UIPropertyMetadata( null, OnTriggersChanged ) );

        private static void OnTriggersChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var triggers = Interaction.GetTriggers( d );
            foreach ( var trigger in e.NewValue as Triggers )
            {
                triggers.Add( trigger );
            }
        }
    }
}
