using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.DocumentPart
{
    public class VariableBool : PartBase
    {
        public override VariableType Type
        {
            get
            {
                return VariableType.Boolean;
            }
        }
    }
}
