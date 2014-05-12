using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveDocs.Core.Model.Variable;

namespace WPFEditor.ViewModel
{
    public class VariableDoubleViewModel : VariableBaseViewModel
    {
        public override string ToString()
        {
            return "Decimal";
        }

        private VariableFloat variable
        {
            get { return Variable == null ? new VariableFloat() : Variable as VariableFloat; }
        }

        public double MinimumValue
        {
            get { return variable.Minimum; }
            set
            {
                variable.Minimum = value;
                OnPropertyChanged("MinimumValue");
            }
        }

        public double MaximumValue
        {
            get { return variable.Maximum; }
            set
            {
                variable.Maximum = value;
                OnPropertyChanged("MaximumValue");
            }
        }

        public double DefaultValue
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
