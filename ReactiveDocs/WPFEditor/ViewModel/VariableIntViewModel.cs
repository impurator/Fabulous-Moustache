using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveDocs.Core.Model.Variable;

namespace WPFEditor.ViewModel
{
    public class VariableIntViewModel : VariableBaseViewModel
    {
        public override string ToString()
        {
            return "Integer";
        }

        private VariableInteger variable
        {
            get { return Variable == null ? new VariableInteger() : Variable as VariableInteger; }
        }

        public int MinimumValue
        {
            get { return variable.Minimum; }
            set
            {
                variable.Minimum = value;
                OnPropertyChanged("MinimumValue");
            }
        }

        public int MaximumValue
        {
            get { return variable.Maximum; }
            set
            {
                variable.Maximum = value;
                OnPropertyChanged("MaximumValue");
            }
        }

        public int DefaultValue
        {
            get { return variable.Default; }
            set
            {
                variable.Default = value;
                OnPropertyChanged("DefaultValue");
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
            OnPropertyChanged("MinimumValue");
            OnPropertyChanged("MaximumValue");
            OnPropertyChanged("DefaultValue");
            OnPropertyChanged("FormatString");
        }
    }
}
