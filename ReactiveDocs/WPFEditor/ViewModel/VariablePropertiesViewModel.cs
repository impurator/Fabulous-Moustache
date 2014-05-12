using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEditor.ViewModel
{
    public class VariablePropertiesViewModel : ViewModelBase
    {
        public ObservableCollection<ViewModelBase> AvailableVariableTypes { get; set; }

        private ViewModelBase selectedVariableType;
        public ViewModelBase SelectedVariableType
        {
            get { return selectedVariableType; }
            set 
            { 
                selectedVariableType = value;
                OnPropertyChanged("SelectedVariableType");
            }
        }

        private string varName;
        public string VarName
        {
            get { return varName; }
            set 
            { 
                varName = value;
                OnPropertyChanged("VarName");
            }
        }

        public VariablePropertiesViewModel()
        {
            AvailableVariableTypes = new ObservableCollection<ViewModelBase>
            {
                new VariableIntViewModel(),
                new VariableDoubleViewModel(),
                new VariableStringSetViewModel()
            };
        }
    }
}
