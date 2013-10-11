using ReactiveDocs.Core.Model.DocumentPart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.Variable
{
    public abstract class VariableBase
    {
        public string VariableName { get; set; }
        public object Value { get; set; }
        public abstract VariableType Type { get; }
    }
}
