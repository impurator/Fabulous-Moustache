using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEditor.ViewModel
{
    public class VariablePropertiesViewModel : ViewModelBase
    {
        public List<ViewModelBase> AvailableVariableTypes { get; set; }

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
            AvailableVariableTypes = new List<ViewModelBase>
            {
                
            };
        }
    }
}
