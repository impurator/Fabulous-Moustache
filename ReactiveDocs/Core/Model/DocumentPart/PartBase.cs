using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.DocumentPart
{
    public abstract class PartBase
    {
        public string BindingName { get; set; }
        public abstract VariableType Type { get; }
    }
}
