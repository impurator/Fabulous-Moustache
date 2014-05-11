using ReactiveDocs.Core.Model.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFEditor.Helper
{
    public static class RichTextBoxHelper
    {
        public static Block FindBlockAtCaret(this RichTextBox self)
        {
            return self.Document.FindBlockAtPosition(self.CaretPosition);
        }

        public static Block FindBlockAtSelection(this RichTextBox self)
        {
            var curSelection = self.Selection;
            var startBlock = self.Document.FindBlockAtPosition(curSelection.Start);
            var endBlock = self.Document.FindBlockAtPosition(curSelection.End);
            
            // TODO: Handle selection across blocks (or don't)
            if (!startBlock.Equals(endBlock))
                return null;

            return startBlock;
        }

        public static Run FindRunAtSelection(this RichTextBox self)
        {
            var curSelection = self.Selection;
            var startRun = self.Document.FindRunAtPosition(curSelection.Start);
            var endRun = self.Document.FindRunAtPosition(curSelection.End);

            // TODO: Handle selection across runs (or don't)
            if (!startRun.Equals(endRun))
                return null;

            return startRun;
        }

        public static Button InsertVariableFromSelection(this RichTextBox self)
        {
            var run = self.FindRunAtSelection();
            var startIndex = run.Text.IndexOf(self.Selection.Text);
            return run.InsertVariableByTextPosition(startIndex, startIndex + self.Selection.Text.Length);
        }
    }
}
