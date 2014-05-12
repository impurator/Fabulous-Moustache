using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.Variable
{
    public class VariableBasic : VariableBase
    {
        public override VariableType Type
        {
            get { return VariableType.Basic; }
        }

        public string EvaluationString { get; set; }
        public string FormatString { get; set; }

        public VariableBasic()
        {
            FormatString = "%d";
        }

        public string DecodedEvaluationString
        {
            get
            {
                return Regex.Replace(EvaluationString, "\\$(\\w*)", "this.$1");
            }
        }
    }
}
