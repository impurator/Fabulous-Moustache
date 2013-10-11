using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.DocumentPart
{
    public class SwitchingText : PartBase
    {
        public List<string> Texts { get; set; }
        public bool IsReadOnly { get; set; }

        public SwitchingText(params string[] texts)
        {
            Texts = new List<string>(texts);
        }

        public string GetText(int fromIndex)
        {
            return Texts[fromIndex];
        }

        public int GetIndex(string fromText)
        {
            return Texts.FindIndex(x => x == fromText);
        }
    }
}
