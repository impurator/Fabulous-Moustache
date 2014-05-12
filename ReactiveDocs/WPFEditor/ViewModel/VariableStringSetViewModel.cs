using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEditor.ViewModel
{
    public class VariableStringSetViewModel : VariableBaseViewModel
    {
        public override string ToString()
        {
            return "Set of Strings";
        }

        private ObservableCollection<StringItem> strings = new ObservableCollection<StringItem>();
        public ObservableCollection<StringItem> Strings
        {
            get { return strings; }
            set { strings = value; }
        }
        
    }

    public class StringItem : ViewModelBase
    {
        private string stringValue;
        public string StringValue
        {
            get { return stringValue; }
            set
            {
                stringValue = value;
                OnPropertyChanged("StringValue");
            }
        }
    }
}
