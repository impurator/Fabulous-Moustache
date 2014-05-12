using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.Variable
{
    public class VariableInteger : VariableBase
    {
        public override VariableType Type
        {
            get
            {
                return VariableType.Integer;
            }
        }

        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int Default { get; set; }
        public string FormatString { get; set; }

        public VariableInteger()
        {
        }

        public VariableInteger(int defaultValue)
        {
            if (defaultValue > 0)
            {
                Minimum = 0;
                Maximum = defaultValue*10;
            }
            else
            {
                Minimum = defaultValue*10;
                Maximum = 0;
            }
            Default = defaultValue;
            FormatString = "%d";
        }

        public override object GetDefaultValue()
        {
            return Default;
        }
    }
}
