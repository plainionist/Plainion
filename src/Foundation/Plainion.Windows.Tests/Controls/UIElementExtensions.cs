using System.Windows;
using System.Windows.Input;

namespace Plainion.Windows.Tests.Controls
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
    }
}
