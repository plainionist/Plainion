using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using NUnit.Framework;
using Plainion.Windows.Controls;

namespace Plainion.Windows.Tests.Controls
{
    [RequiresSTA]
    [TestFixture]
    class RichTextEditorTests
    {
        public class FakePresentationSource : PresentationSource
        {
            protected override CompositionTarget GetCompositionTargetCore()
            {
                return null;
            }

            public override Visual RootVisual { get; set; }

            public override bool IsDisposed { get { return false; } }
        }

        [Test]
        public void KeyDown_WhenCalled_Throws()
        {
            var editor = new RichTextEditor();

            editor.RaiseEvent(
                new KeyEventArgs(Keyboard.PrimaryDevice, new FakePresentationSource(), 0, Key.Space)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                });
        }
    }
}
