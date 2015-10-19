using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Plainion.Windows.Controls
{
    class FlowDocumentVisitor
    {
        private Func<TextElement, bool> myAction;
        private List<TextElement> myResults;

        public FlowDocumentVisitor(Func<TextElement, bool> action)
        {
            myAction = action;

            myResults = new List<TextElement>();
            ContinueAfterMatch = true;
        }

        public bool ContinueAfterMatch { get; set; }

        public IReadOnlyCollection<TextElement> Results
        {
            get { return myResults; }
        }

        public void Accept(FlowDocument document)
        {
            foreach (var block in document.Blocks)
            {
                if (!Accept(block))
                {
                    return;
                }
            }
        }

        private bool Accept(Block block)
        {
            if (!TryMatch(block)) return false;

            if (block is Table)
            {
                foreach (var inner in ((Table) block).RowGroups
                    .SelectMany(x => x.Rows)
                    .SelectMany(x => x.Cells)
                    .SelectMany(x => x.Blocks))
                {
                    if (!Accept(inner)) return false;
                }

                return true;
            }

            if (block is Paragraph)
            {
                foreach (var inner in  ((Paragraph) block).Inlines)
                {
                    if (!TryMatch(inner)) return false;
                }

                return true;
            }

            if (block is BlockUIContainer)
            {
                // ignore children
                return true;
            }

            if (block is List)
            {
                foreach (var inner in  ((List) block).ListItems.SelectMany(listItem => listItem.Blocks))
                {
                    if (!Accept(inner)) return false;
                }

                return true;
            }

            throw new InvalidOperationException("Unknown block type: " + block.GetType());
        }

        private bool TryMatch(TextElement element)
        {
            if (myAction(element))
            {
                myResults.Add(element);

                if (!ContinueAfterMatch)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
