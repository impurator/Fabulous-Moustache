using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEditor.ViewModel
{
    public class VariableIntViewModel : ViewModelBase
    {
        public override string ToString()
        {
            return "Integer";
        }

        private int minimumValue;
        public int MinimumValue
        {
            get { return minimumValue; }
            set
            {
                minimumValue = value;
                OnPropertyChanged("MinimumValue");
            }
        }

        private int maximumValue;
        public int MaximumValue
        {
            get { return maximumValue; }
            set
            {
                maximumValue = value;
                OnPropertyChanged("MaximumValue");
            }
        }

        private int defaultValue;
        public int Defaultvalue
        {
            get { return defaultValue; }
            set
            {
                defaultValue = value;
                OnPropertyChanged("Defaultvalue");
            }
        }

        private string formatString;
        public string FormatString
        {
            get { return formatString; }
            set
            {
                formatString = value;
                OnPropertyChanged("FormatString");
            }
        }
    }
}
