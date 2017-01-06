using System.Linq;
using System.Windows.Documents;
using NUnit.Framework;
using Plainion.Windows.Controls;
using Plainion.Windows.Controls.Text;
using List = System.Windows.Documents.List;


namespace Plainion.Windows.Tests.Controls.Text
{
    [TestFixture]
    class FlowDocumentVisitorTests
    {
        [Test]
        public void Accept_EmptyDocument_EmptyResult()
        {
            var document = new FlowDocument();

            var visitor = new FlowDocumentVisitor(e => true);
            visitor.Accept(document);

            Assert.That(visitor.Results, Is.Empty);
        }

        [Test]
        public void Accept_SingleRunElement_ReturnsRunElement()
        {
            var document = new FlowDocument();
            document.Blocks.Add(new Paragraph(new Run("Some dummy ")));

            var visitor = new FlowDocumentVisitor(e => e is Run);
            visitor.Accept(document);

            Assert.That(visitor.Results.OfType<Run>().Count(), Is.EqualTo(1));
        }

        [Test]
        public void Accept_MultipleRunElementsAndContinueAfterMatchIsTrue_AllRunElementsReturned()
        {
            var document = new FlowDocument();
            var para = new Paragraph();
            para.Inlines.Add(new Run("a"));
            para.Inlines.Add(new Run("b"));
            para.Inlines.Add(new Run("c"));
            document.Blocks.Add(para);

            var visitor = new FlowDocumentVisitor(e => e is Run) { ContinueAfterMatch = true };
            visitor.Accept(document);

            Assert.That(visitor.Results.OfType<Run>().Count(), Is.EqualTo(3));
        }

        [Test]
        public void Accept_MultipleRunElementsAndContinueAfterMatchIsFalse_OnlyFirstRunElementReturned()
        {
            var document = new FlowDocument();
            var para = new Paragraph();
            para.Inlines.Add(new Run("a"));
            para.Inlines.Add(new Run("b"));
            para.Inlines.Add(new Run("c"));
            document.Blocks.Add(para);

            var visitor = new FlowDocumentVisitor(e => e is Run) { ContinueAfterMatch = false };
            visitor.Accept(document);

            Assert.That(visitor.Results.OfType<Run>().Count(), Is.EqualTo(1));
            Assert.That(visitor.Results.OfType<Run>().Single().Text, Is.EqualTo("a"));
        }

        [Test]
        public void Accept_HyperlinkInLists_AllHyperlinksFound()
        {
            var document = new FlowDocument();
            var list = new List();
            list.ListItems.Add(new ListItem(new Paragraph(new Hyperlink(new Run("http://github.com")))));
            list.ListItems.Add(new ListItem(new Paragraph(new Hyperlink(new Run("http://github.com")))));
            list.ListItems.Add(new ListItem(new Paragraph(new Hyperlink(new Run("http://github.com")))));
            document.Blocks.Add(list);

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink) { ContinueAfterMatch = true };
            visitor.Accept(document);

            Assert.That(visitor.Results.OfType<Hyperlink>().Count(), Is.EqualTo(3));
        }
    }
}
