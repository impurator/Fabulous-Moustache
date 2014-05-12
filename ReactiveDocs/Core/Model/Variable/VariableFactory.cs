using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.Variable
{
    public static class VariableFactory
    {
        public static VariableBase CreateVariable(VariableType variableType)
        {
            switch (variableType)
            {
                case VariableType.Integer:
                    return new VariableInteger();
                case VariableType.Float:
                    return new VariableFloat();
                case VariableType.Boolean:
                    return new VariableBool();
                case VariableType.Basic:
                    return new VariableBasic();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
