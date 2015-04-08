using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Plainion.Windows.Interactivity
{
    public class TextBoxUpdateModelOnEnterBehavior : Behavior<TextBox>
    {
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
            if ( e.Key != Key.Enter )
            {
                return;
            }

            var binding = AssociatedObject.GetBindingExpression( TextBox.TextProperty );
            if ( binding != null )
            {
                binding.UpdateSource();
            }
        }
    }
}
