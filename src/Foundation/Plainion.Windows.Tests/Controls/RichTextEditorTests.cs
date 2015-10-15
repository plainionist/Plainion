using System;
using System.Linq;
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
        public void OnKeyDown_WithSpecialKey_SelectionIsCleared([Values(Key.Space, Key.Return, Key.Back)]Key key)
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy text")));
            editor.Selection.Select(editor.Document.ContentStart, editor.Document.ContentEnd);

            Assert.That(editor.Selection.IsEmpty, Is.False, "Failed to select some text");

            editor.RaiseKeyboardEvent(UIElement.KeyDownEvent, key);

            Assert.That(editor.Selection.IsEmpty, Is.True, "Selection not empty");
        }

        [Test]
        public void OnWordCompleted_AfterNonLink_NoHyperlinkInserted([Values(Key.Space, Key.Return)]Key key)
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy text")));
            editor.CaretPosition = editor.Document.ContentEnd;

            editor.RaiseKeyboardEvent(UIElement.KeyDownEvent, key);

            Assert.That(new FlowDocumentVisitor(e => e is Hyperlink).Results, Is.Empty);
        }

        [Test]
        public void OnWordCompleted_AfterHttpLink_HyperlinkInserted([Values(Key.Space, Key.Return)]Key key)
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy http://github.com/")));
            editor.CaretPosition = editor.Document.ContentEnd;

            editor.RaiseKeyboardEvent(UIElement.KeyDownEvent, key);

            var links = new FlowDocumentVisitor(e => e is Hyperlink).Results;
            Assert.That(links.Count, Is.EqualTo(1));

            var hyperlink = links.OfType<Hyperlink>().Single();
            Assert.That(hyperlink.Inlines.OfType<Run>().Single().Text, Is.EqualTo("http://github.com/"));
            Assert.That(hyperlink.NavigateUri.ToString(), Is.EqualTo("http://github.com/"));
        }

        // TODO: https, ftp://
    }
}
