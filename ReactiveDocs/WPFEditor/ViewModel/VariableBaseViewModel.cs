using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveDocs.Core.Model.Variable;

namespace WPFEditor.ViewModel
{
    public class VariableBaseViewModel : ViewModelBase
    {
        private VariableBase variable;
        public VariableBase Variable
        {
            get { return variable; }
            set
            {
                variable = value;
                RefreshUI();
            }
        }

        public virtual void RefreshUI()
        {
        }
    }
}
