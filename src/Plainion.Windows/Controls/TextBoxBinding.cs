using System.Windows.Controls;
using System.Windows.Input;

namespace Plainion.Windows.Controls
{
    public class TextBoxBinding
    {
        /// <summary>
        /// Textboxs update model on focus lost. Enforce update now - e.g. to ensure that change/dirty tracking is working properly.
        /// </summary>
        public static void ForceSourceUpdate()
        {
            var textBox = Keyboard.FocusedElement as TextBox;
            if ( textBox == null )
            {
                return;
            }

            var be = textBox.GetBindingExpression( TextBox.TextProperty );
            if ( be != null && !textBox.IsReadOnly && textBox.IsEnabled )
            {
                be.UpdateSource();
            }
        }
    }
}
