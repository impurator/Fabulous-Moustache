using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.DocumentExpression
{
    public abstract class ExpressionBase
    {
        public string BindingName { get; set; }

        public abstract object Evaluate(Dictionary<string, object> boundValues);
    }
}
