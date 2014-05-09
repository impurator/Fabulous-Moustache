using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPFEditor.Helper
{
    public static class FlowDocumentHelper
    {
        public static Block FindBlockAtPosition(this FlowDocument self, TextPointer position)
        {
            return self.Blocks.FirstOrDefault(x => x.ContentStart.CompareTo(position) == -1 && x.ContentEnd.CompareTo(position) == 1);
        }

    }
}
