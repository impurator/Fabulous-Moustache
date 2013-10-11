using MathParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            // Bug : this probably screws up if one bound name is a subset of another
            // should capture the tokens with a greedy algo and then replace them.
            foreach (var boundValue in boundValues)
            {
                workingString = workingString.Replace(boundValue.Key, boundValue.Value.ToString());
            }

            var parser = new Parser();
            var parsingResult = parser.Parse(workingString);
            
            return parsingResult.Evaluate();
        }
    }
}
