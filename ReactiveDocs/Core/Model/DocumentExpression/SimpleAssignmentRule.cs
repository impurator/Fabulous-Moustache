using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.DocumentExpression
{
    public class SimpleExpression : ExpressionBase
    {
        public string Expression { get; set; }

        public override object Evaluate(Dictionary<string, object> boundValues)
        {
            var expression = new Expression(Expression);
            expression.Parameters = boundValues;
            return expression.Evaluate();
        }
    }
}
