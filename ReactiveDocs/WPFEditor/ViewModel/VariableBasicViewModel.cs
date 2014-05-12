using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveDocs.Core.Model.Variable;

namespace WPFEditor.ViewModel
{
    public class VariableBasicViewModel : VariableBaseViewModel
    {
        private VariableBasic variable
        {
            get { return Variable == null ? new VariableBasic() : Variable as VariableBasic; }
        }

        public string EvaluationString
        {
            get { return variable.EvaluationString; }
            set
            {
                variable.EvaluationString = value;
                OnPropertyChanged("EvaluationString");
            }
        }

        public string FormatString
        {
            get { return variable.FormatString; }
            set
            {
                variable.FormatString = value;
                OnPropertyChanged("FormatString");
            }
        }

        public override void RefreshUI()
        {
            OnPropertyChanged("EvaluationString");
            OnPropertyChanged("FormatString");
        }
    }
}
