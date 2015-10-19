using System.Linq;
using System.Windows.Documents;
using NUnit.Framework;
using Plainion.Windows.Controls;

namespace Plainion.Windows.Tests.Controls
{
    [TestFixture]
    class DocumentFacadeTests
    {
        [Test]
        public void TryMakeHyperlinks_AfterLink_HyperlinkInserted([Values("http://github.com/", "https://github.com/", "ftp://github.com/")]string url)
        {
            var document = new FlowDocument(new Paragraph(new Run("Some dummy " + url)));

            DocumentFacade.TryMakeHyperlinks(new TextRange(document.ContentEnd, document.ContentEnd));

            var visitor = new FlowDocumentVisitor(e => e is Hyperlink);
            visitor.Accept(document);
            Assert.That(visitor.Results.Count, Is.EqualTo(1));

            var hyperlink = visitor.Results.OfType<Hyperlink>().Single();
            Assert.That(hyperlink.Inlines.OfType<Run>().Single().Text, Is.EqualTo(url));
            Assert.That(hyperlink.NavigateUri.ToString(), Is.EqualTo(url));
        }
    }
}
