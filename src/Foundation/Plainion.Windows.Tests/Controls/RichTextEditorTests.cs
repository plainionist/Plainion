using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
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

            editor.TriggerInput(key);

            Assert.That(editor.Selection.IsEmpty, Is.True, "Selection not empty");
        }

        [Test]
        public void OnWordCompleted_AfterNonLink_NoHyperlinkInserted([Values(Key.Space, Key.Return)]Key key)
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy text")));
            editor.CaretPosition = editor.Document.ContentEnd;

            editor.TriggerInput(key);

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(editor.Document);
            Assert.That(visitor.Results, Is.Empty);
        }

        [Test]
        public void OnWordCompleted_AfterLink_HyperlinkInserted([Values(Key.Space, Key.Return)]Key key,
            [Values("http://github.com/", "https://github.com/", "ftp://github.com/")]string url)
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy " + url)));
            editor.CaretPosition = editor.Document.ContentEnd;

            editor.TriggerInput(key);

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(editor.Document);
            Assert.That(visitor.Results.Count, Is.EqualTo(1));

            var hyperlink = visitor.Results.OfType<Hyperlink>().Single();
            Assert.That(hyperlink.Inlines.OfType<Run>().Single().Text, Is.EqualTo(url));
            Assert.That(hyperlink.NavigateUri.ToString(), Is.EqualTo(url));
        }

        [Test]
        public void OnWordCompleted_AfterIncompleteWwwLink_HyperlinkWithHttpPrefixInserted([Values(Key.Space, Key.Return)]Key key)
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy www.host.org")));
            editor.CaretPosition = editor.Document.ContentEnd;

            editor.TriggerInput(key);

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(editor.Document);
            Assert.That(visitor.Results.Count, Is.EqualTo(1));

            var hyperlink = visitor.Results.OfType<Hyperlink>().Single();
            Assert.That(hyperlink.Inlines.OfType<Run>().Single().Text, Is.EqualTo("www.host.org"));
            Assert.That(hyperlink.NavigateUri.ToString(), Is.EqualTo("http://www.host.org/"));
        }

        [Test]
        public void OnWordContinued_AfterNonLink_NoHyperlinkInserted()
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy text")));
            editor.CaretPosition = editor.Document.ContentEnd;

            editor.TriggerInput(Key.A);

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(editor.Document);
            Assert.That(visitor.Results, Is.Empty);
        }

        [Test]
        public void OnWordContinued_AfterHttpLink_NoHyperlinkInserted([Values("http://github.com", "https://github.com", "ftp://github.com", "www.host.org")]string url)
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy " + url)));
            editor.CaretPosition = editor.Document.ContentEnd;

            editor.TriggerInput(Key.A);

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(editor.Document);
            Assert.That(visitor.Results, Is.Empty);
        }

        // TODO: backspace
    }
}
