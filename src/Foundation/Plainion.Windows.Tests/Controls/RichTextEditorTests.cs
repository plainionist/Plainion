using System;
using System.Windows;
using System.Windows.Documents;
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
        [Test]
        public void KeyDown_WithSpecialKey_SelectionIsCleared([Values(Key.Space, Key.Return, Key.Back)]Key key)
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy text")));
            editor.Selection.Select(editor.Document.ContentStart, editor.Document.ContentEnd);

            Assert.That(editor.Selection.IsEmpty, Is.False, "Failed to select some text");

            editor.RaiseKeyboardEvent(UIElement.KeyDownEvent, key);

            Assert.That(editor.Selection.IsEmpty, Is.True, "Selection not empty");
        }
    }
}
