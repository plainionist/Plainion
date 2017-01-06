using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Plainion.Windows.Controls.Text
{
    static class DocumentFacade
    {
        public static void TryMakeHyperlinks(TextRange range)
        {
            var navigator = range.Start;
            while (navigator != null && navigator.CompareTo(range.End) <= 0)
            {
                var wordRange = GetWordRange(navigator);
                if (wordRange == null || wordRange.IsEmpty)
                {
                    // No more words in the document.
                    break;
                }

                string wordText = wordRange.Text;
                var url = TryCreateUrl(wordText);
                if ( url !=null &&
                    !IsInHyperlinkScope(wordRange.Start) &&
                    !IsInHyperlinkScope(wordRange.End))
                {
                    var hyperlink = new Hyperlink(wordRange.Start, wordRange.End);
                    hyperlink.NavigateUri = url;
                    WeakEventManager<Hyperlink, RequestNavigateEventArgs>.AddHandler(hyperlink, "RequestNavigate", OnHyperlinkRequestNavigate);

                    navigator = hyperlink.ElementEnd.GetNextInsertionPosition(LogicalDirection.Forward);
                }
                else
                {
                    navigator = wordRange.End.GetNextInsertionPosition(LogicalDirection.Forward);
                }
            }
        }

        /// <summary>
        /// Returns a TextRange covering a word containing or following this TextPointer.
        /// </summary>
        /// <remarks>
        /// If this TextPointer is within a word or at start of word, the containing word range is returned.
        /// If this TextPointer is between two words, the following word range is returned.
        /// If this TextPointer is at trailing word boundary, the following word range is returned.
        /// </remarks>
        private static TextRange GetWordRange(TextPointer position)
        {
            TextRange wordRange = null;
            TextPointer wordStartPosition = null;
            TextPointer wordEndPosition = null;

            // Go forward first, to find word end position.
            wordEndPosition = GetPositionAtWordBoundary(position, /*wordBreakDirection*/LogicalDirection.Forward);

            if (wordEndPosition != null)
            {
                // Then travel backwards, to find word start position.
                wordStartPosition = GetPositionAtWordBoundary(wordEndPosition, /*wordBreakDirection*/LogicalDirection.Backward);
            }

            if (wordStartPosition != null && wordEndPosition != null)
            {
                wordRange = new TextRange(wordStartPosition, wordEndPosition);
            }

            return wordRange;
        }

        /// <summary>
        /// 1.  When wordBreakDirection = Forward, returns a position at the end of the word,
        ///     i.e. a position with a wordBreak character (space) following it.
        /// 2.  When wordBreakDirection = Backward, returns a position at the start of the word,
        ///     i.e. a position with a wordBreak character (space) preceeding it.
        /// 3.  Returns null when there is no workbreak in the requested direction.
        /// </summary>
        private static TextPointer GetPositionAtWordBoundary(TextPointer position, LogicalDirection wordBreakDirection)
        {
            if (!position.IsAtInsertionPosition)
            {
                position = position.GetInsertionPosition(wordBreakDirection);
            }

            TextPointer navigator = position;
            while (navigator != null && !IsPositionNextToWordBreak(navigator, wordBreakDirection))
            {
                navigator = navigator.GetNextInsertionPosition(wordBreakDirection);
            }

            return navigator;
        }

        // Helper for GetPositionAtWordBoundary.
        // Returns true when passed TextPointer is next to a wordBreak in requested direction.
        private static bool IsPositionNextToWordBreak(TextPointer position, LogicalDirection wordBreakDirection)
        {
            bool isAtWordBoundary = false;

            // Skip over any formatting.
            if (position.GetPointerContext(wordBreakDirection) != TextPointerContext.Text)
            {
                position = position.GetInsertionPosition(wordBreakDirection);
            }

            if (position.GetPointerContext(wordBreakDirection) == TextPointerContext.Text)
            {
                LogicalDirection oppositeDirection = (wordBreakDirection == LogicalDirection.Forward) ?
                    LogicalDirection.Backward : LogicalDirection.Forward;

                char[] runBuffer = new char[1];
                char[] oppositeRunBuffer = new char[1];

                position.GetTextInRun(wordBreakDirection, runBuffer, /*startIndex*/0, /*count*/1);
                position.GetTextInRun(oppositeDirection, oppositeRunBuffer, /*startIndex*/0, /*count*/1);

                if (runBuffer[0] == ' ' && !(oppositeRunBuffer[0] == ' '))
                {
                    isAtWordBoundary = true;
                }
            }
            else
            {
                // If we're not adjacent to text then we always want to consider this position a "word break".  
                // In practice, we're most likely next to an embedded object or a block boundary.
                isAtWordBoundary = true;
            }

            return isAtWordBoundary;
        }

        // Helper that returns true when passed TextPointer is within the scope of a Hyperlink element.
        private static bool IsInHyperlinkScope(TextPointer position)
        {
            return GetHyperlinkAncestor(position) != null;
        }

        // Helper that returns a Hyperlink ancestor of passed TextPointer.
        private static Hyperlink GetHyperlinkAncestor(TextPointer position)
        {
            var parent = position.Parent as Inline;
            while (parent != null && !(parent is Hyperlink))
            {
                parent = parent.Parent as Inline;
            }

            return parent as Hyperlink;
        }
        
        private static void OnHyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }

        private static Uri TryCreateUrl(string wordText)
        {
            if (wordText.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                wordText.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                wordText.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase))
            {
                return new Uri(wordText);
            }

            if (wordText.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
            {
                return new Uri("http://" + wordText);
            }

            return null;
        }

        public static TextPointer RemoveHyperlink(TextPointer start)
        {
            var backspacePosition = start.GetNextInsertionPosition(LogicalDirection.Backward);
            Hyperlink hyperlink;
            if (backspacePosition == null || !IsHyperlinkBoundaryCrossed(start, backspacePosition, out hyperlink))
            {
                return null;
            }

            // Remember caretPosition with forward gravity. This is necessary since we are going to delete 
            // the hyperlink element preceeding caretPosition and after deletion current caretPosition 
            // (with backward gravity) will follow content preceeding the hyperlink. 
            // We want to remember content following the hyperlink to set new caret position at.

            var newCaretPosition = start.GetPositionAtOffset(0, LogicalDirection.Forward);

            // Deleting the hyperlink is done using logic below.

            // 1. Copy its children Inline to a temporary array.
            var hyperlinkChildren = hyperlink.Inlines;
            var inlines = new Inline[hyperlinkChildren.Count];
            hyperlinkChildren.CopyTo(inlines, 0);

            // 2. Remove each child from parent hyperlink element and insert it after the hyperlink.
            for (int i = inlines.Length - 1; i >= 0; i--)
            {
                hyperlinkChildren.Remove(inlines[i]);
                hyperlink.SiblingInlines.InsertAfter(hyperlink, inlines[i]);
            }

            // 3. Apply hyperlink's local formatting properties to inlines (which are now outside hyperlink scope).
            var localProperties = hyperlink.GetLocalValueEnumerator();
            var inlineRange = new TextRange(inlines[0].ContentStart, inlines[inlines.Length - 1].ContentEnd);

            while (localProperties.MoveNext())
            {
                var property = localProperties.Current;
                var dp = property.Property;
                object value = property.Value;

                if (!dp.ReadOnly &&
                    dp != Inline.TextDecorationsProperty && // Ignore hyperlink defaults.
                    dp != TextElement.ForegroundProperty &&
                    dp != BaseUriHelper.BaseUriProperty &&
                    !IsHyperlinkProperty(dp))
                {
                    inlineRange.ApplyPropertyValue(dp, value);
                }
            }

            // 4. Delete the (empty) hyperlink element.
            hyperlink.SiblingInlines.Remove(hyperlink);

            return newCaretPosition;
        }

        // Helper that returns true if passed caretPosition and backspacePosition cross a hyperlink end boundary
        // (under the assumption that caretPosition and backSpacePosition are adjacent insertion positions).
        private static bool IsHyperlinkBoundaryCrossed(TextPointer caretPosition, TextPointer backspacePosition, out Hyperlink backspacePositionHyperlink)
        {
            var caretPositionHyperlink = GetHyperlinkAncestor(caretPosition);
            backspacePositionHyperlink = GetHyperlinkAncestor(backspacePosition);

            return (caretPositionHyperlink == null && backspacePositionHyperlink != null) ||
                (caretPositionHyperlink != null && backspacePositionHyperlink != null && caretPositionHyperlink != backspacePositionHyperlink);
        }
        
        // Helper that returns true when passed property applies to Hyperlink only.
        private static bool IsHyperlinkProperty(DependencyProperty dp)
        {
            return dp == Hyperlink.CommandProperty ||
                dp == Hyperlink.CommandParameterProperty ||
                dp == Hyperlink.CommandTargetProperty ||
                dp == Hyperlink.NavigateUriProperty ||
                dp == Hyperlink.TargetNameProperty;
        }
    }
}
