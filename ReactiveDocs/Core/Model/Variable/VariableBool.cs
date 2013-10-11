using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.Variable
{
    public class VariableBool : VariableBase
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
