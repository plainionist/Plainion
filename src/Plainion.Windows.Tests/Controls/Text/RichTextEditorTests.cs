using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using NUnit.Framework;
using Plainion.Windows.Controls;
using Plainion.Windows.Controls.Text;

namespace Plainion.Windows.Tests.Controls.Text
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
        public void OnWordCompleted_AfterLink_HyperlinkInserted([Values(Key.Space, Key.Return)]Key key)
        {
            var url = "http://github.com/";
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy "+url)));
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
        public void OnWordContinued_AfterHttpLink_NoHyperlinkInserted()
        {
            var url = "http://github.com/";
            
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy " + url)));
            editor.CaretPosition = editor.Document.ContentEnd;

            editor.TriggerInput(Key.A);

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(editor.Document);
            Assert.That(visitor.Results, Is.Empty);
        }

        [Test]
        public void OnBackspace_AfterLink_HyperlinkRemoved()
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy http://github.org/")));
            editor.CaretPosition = editor.Document.ContentEnd;

            // we know from other tests that this works
            editor.TriggerInput(Key.Space);

            editor.TriggerInput(Key.Back);

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(editor.Document);
            Assert.That(visitor.Results, Is.Empty);
        }

        [Test]
        public void OnPaste_WithLink_HyperlinkInserted()
        {
            var url = "http://github.com/";

            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy ")));
            editor.CaretPosition = editor.Document.ContentEnd;

            Clipboard.SetData(DataFormats.Text, url);
            editor.Paste();

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(editor.Document);
            Assert.That(visitor.Results.Count, Is.EqualTo(1));

            var hyperlink = visitor.Results.OfType<Hyperlink>().Single();
            Assert.That(hyperlink.Inlines.OfType<Run>().Single().Text, Is.EqualTo(url));
            Assert.That(hyperlink.NavigateUri.ToString(), Is.EqualTo(url));
        }

        [Test]
        public void OnPaste_NonLink_NoHyperlinkInserted()
        {
            var editor = new RichTextEditor();
            editor.Document.Blocks.Add(new Paragraph(new Run("Some dummy ")));
            editor.CaretPosition = editor.Document.ContentEnd;

            Clipboard.SetData(DataFormats.Text, "some-other-text");
            editor.Paste();

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(editor.Document);
            Assert.That(visitor.Results, Is.Empty);
        }
    }
}
