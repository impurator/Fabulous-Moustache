using ReactiveDocs.Core.Model.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using WPFEditor.Variable;

namespace WPFEditor.Helper
{
    public static class FlowDocumentHelper
    {
        public static Run FindRunAtPosition(this FlowDocument self, TextPointer position)
        {
            var block = FindBlockAtPosition(self, position);
            var paragraph = block as Paragraph;
            if (paragraph == null)
                return null;

            return paragraph.Inlines.FirstOrDefault(x => x.ContentStart.CompareTo(position) == -1 && x.ContentEnd.CompareTo(position) == 1) as Run;
        }

        public static Block FindBlockAtPosition(this FlowDocument self, TextPointer position)
        {
            return self.Blocks.FirstOrDefault(x => x.ContentStart.CompareTo(position) == -1 && x.ContentEnd.CompareTo(position) == 1);
        }

        private static void ReplaceInline(this InlineCollection self, Inline target, List<Inline> replaceWith)
        {
            if (!self.Contains(target))
                return;

            foreach (var replacement in replaceWith)
            {
                self.InsertBefore(target, replacement);
            }

            self.Remove(target);
        }

        public static Button InsertVariableByTextPosition(this Run toSplit, int startSplit, int endSplit)
        {
            var inlines = new List<Inline>();

            inlines.Add(new Run(toSplit.Text.Substring(0, startSplit)));
            var variableValue = "";
            if (startSplit != endSplit)
                variableValue = toSplit.Text.Substring(startSplit, endSplit - startSplit);

            var vButton = new Button();

            vButton.Width = 100;
            vButton.Height = 24;
            vButton.Margin = new System.Windows.Thickness(6, 0, 6, 0);
            vButton.Tag = variableValue;

            inlines.Add(new InlineUIContainer(vButton));

            inlines.Add(new Run(toSplit.Text.Substring(endSplit)));

            var parentParagraph = toSplit.Parent as Paragraph;
            var parentInline = toSplit.Parent as Inline;

            if (parentParagraph != null)
            {
                parentParagraph.Inlines.ReplaceInline(toSplit, inlines);
            }
            else if (parentInline != null)
            {
                parentInline.SiblingInlines.ReplaceInline(toSplit, inlines);
            }
            else
            {
                return null;
            }

            return vButton;
        }
    }
}
