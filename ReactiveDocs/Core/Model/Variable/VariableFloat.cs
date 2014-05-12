using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.Variable
{
    public class VariableFloat : VariableBase
    {
        public override VariableType Type
        {
            get
            {
                return VariableType.Float;
            }
        }

        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public double Default { get; set; }
        public string FormatString { get; set; }

        public VariableFloat()
        {
        }

        public VariableFloat(double defaultValue)
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
            FormatString = "%.1f";
        }

        public override object GetDefaultValue()
        {
            return Default;
        }
    }
}
