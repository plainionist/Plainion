using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Plainion.Windows.Tests.Controls.Text
{
    static class UIElementExtensions
    {
        public static void RaiseKeyboardEvent(this UIElement self, RoutedEvent evt, Key key)
        {
            self.RaiseEvent(new KeyEventArgs(Keyboard.PrimaryDevice, new FakePresentationSource(), 0, key)
            {
                RoutedEvent = evt
            });
        }

        public static void TriggerInput(this TextBoxBase self, Key key)
        {
            self.RaiseKeyboardEvent(UIElement.KeyDownEvent, key);
            self.RaiseEvent(new TextChangedEventArgs(TextBox.TextChangedEvent, UndoAction.None));
        }
    }
}
