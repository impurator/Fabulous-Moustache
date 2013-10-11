using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ReactiveDocs.Core.Model.DocumentPart;

namespace ReactiveDocs.Core.Model
{
    public class Document
    {
        public List<PartBase> Parts { get; set; }

        public Document()
        {
            Parts = new List<PartBase>();
        }
    }
}
