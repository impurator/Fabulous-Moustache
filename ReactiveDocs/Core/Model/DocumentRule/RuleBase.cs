using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.DocumentRule
{
    public abstract class RuleBase
    {
        public string BindingName { get; set; }

        public abstract void Evaluate();
    }
}
