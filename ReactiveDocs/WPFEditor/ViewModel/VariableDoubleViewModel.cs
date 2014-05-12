using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEditor.ViewModel
{
    public class VariableDoubleViewModel : ViewModelBase
    {
        public override string ToString()
        {
            return "Decimal";
        }

        private double minimumValue;
        public double MinimumValue
        {
            get { return minimumValue; }
            set
            {
                minimumValue = value;
                OnPropertyChanged("MinimumValue");
            }
        }

        private double maximumValue;
        public double MaximumValue
        {
            get { return maximumValue; }
            set
            {
                maximumValue = value;
                OnPropertyChanged("MaximumValue");
            }
        }

        private double defaultValue;
        public double Defaultvalue
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
