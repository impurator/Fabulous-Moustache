using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.DocumentRule
{
    public class SimpleAssignmentRule : RuleBase
    {
        public string Expression { get; set; }

        public override double Evaluate(Dictionary<string, object> boundValues)
        {
            var workingString = Expression;

            // replace variables with their values.
            // inefficient but I'm hacking, yo
            foreach (var boundValue in boundValues)
            {
                workingString = Regex.Replace(workingString, @"\b" + boundValue.Key + @"\b", boundValue.Value.ToString());
            }

            var expression = new Expression(workingString);;

            var ret = expression.Evaluate();
            return Convert.ToDouble(ret);
        }


    }
}
