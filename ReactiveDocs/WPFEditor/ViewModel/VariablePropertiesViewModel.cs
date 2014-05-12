using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveDocs.Core.Model.Variable;
using WPFEditor.Helper;

namespace WPFEditor.ViewModel
{
    public class VariablePropertiesViewModel : ViewModelBase
    {
        public Visibility VMVisibility
        {
            get { return SelectedVariable == null ? Visibility.Hidden : Visibility.Visible; }
        }

        private VariableInstance selectedVariableInstance;
        public VariableInstance SelectedVariableInstance
        {
            get { return selectedVariableInstance; }
            set
            {
                selectedVariableInstance = value;
                OnPropertyChanged("SelectedVariableInstance");
                OnPropertyChanged("SelectedVariable");
                OnPropertyChanged("VMVisibility");
                OnPropertyChanged("VarName");
                OnPropertyChanged("SelectedVariableType");
                OnPropertyChanged("SelectedVariableTypeViewModel");
            }
        }

        public VariableBase SelectedVariable
        {
            get { return SelectedVariableInstance == null ? null : SelectedVariableInstance.Variable; }
            set
            {
                SelectedVariableInstance.Variable = value;
                OnPropertyChanged("SelectedVariable");
                OnPropertyChanged("VMVisibility");
                OnPropertyChanged("VarName");
                OnPropertyChanged("SelectedVariableType");
                OnPropertyChanged("SelectedVariableTypeViewModel");
            }
        }

        private VariableType selectedVariableType;
        public VariableType SelectedVariableType
        {
            get { return SelectedVariable == null ? VariableType.Integer : SelectedVariable.Type; }
            set
            {
                if (value != SelectedVariable.Type)
                {
                    var newVariable = VariableFactory.CreateVariable(value);
                    newVariable.VariableName = SelectedVariable.VariableName;
                    SelectedVariable = newVariable;
                    OnPropertyChanged("SelectedVariableType");
                }
            }
        }

        public string VarName
        {
            get { return SelectedVariable == null ? "" : SelectedVariable.VariableName; }
            set 
            {
                SelectedVariable.VariableName = value;
                OnPropertyChanged("VarName");
            }
        }

        public ViewModelBase SelectedVariableTypeViewModel
        {
            get { return GetViewModelForVariable(SelectedVariable); }
        }

        private VariableBaseViewModel GetViewModelForVariable(VariableBase var)
        {
            if (var == null)
                return intVM;

            VariableBaseViewModel ret = null;

            switch (var.Type)
            {
                case VariableType.Basic:
                    ret = basicVM;
                    break;

                case VariableType.Integer:
                    ret = intVM;
                    break;

                case VariableType.Float:
                    ret = doubleVM;
                    break;

                case VariableType.StringSet:
                    ret = stringSetVM;
                    break;
            }

            ret.Variable = var;
            return ret;
        }

        public VariablePropertiesViewModel()
        {
        }

        private VariableIntViewModel intVM = new VariableIntViewModel();
        private VariableDoubleViewModel doubleVM = new VariableDoubleViewModel();
        private VariableStringSetViewModel stringSetVM = new VariableStringSetViewModel();
        private VariableBasicViewModel basicVM = new VariableBasicViewModel();
    }
}
