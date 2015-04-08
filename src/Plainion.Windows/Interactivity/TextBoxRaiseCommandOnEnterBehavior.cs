using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    public class TextBoxRaiseCommandOnEnterBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
             "Command", typeof( ICommand ), typeof( TextBoxRaiseCommandOnEnterBehavior ) );

        public ICommand Command
        {
            get { return ( ICommand )this.GetValue( CommandProperty ); }
            set { this.SetValue( CommandProperty, value ); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;

            base.OnDetaching();
        }

        private void OnPreviewKeyDown( object sender, KeyEventArgs e )
        {
            if( e.Key != Key.Enter )
            {
                return;
            }

            var binding = AssociatedObject.GetBindingExpression( TextBox.TextProperty );
            if( binding != null )
            {
                binding.UpdateSource();
            }

            if( Command != null )
            {
                Command.Execute( null );
            }
        }
    }
}
