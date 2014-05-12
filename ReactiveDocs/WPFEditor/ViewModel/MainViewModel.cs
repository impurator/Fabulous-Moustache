using ReactiveDocs.Core.Model.Variable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using WPFEditor.Variable;
using WPFEditor.Helper;

namespace WPFEditor.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RichTextBox MainText { get; set; }
        public VariableManager VariableManager { get; set; }
        public DocumentVM DocumentVm { get; set; }
        public VariablePropertiesViewModel VariablePropertiesVM { get; set; }

        private VariableBase selectedVariable = null;
        public VariableBase SelectedVariable
        {
            get
            {
                return selectedVariable;
            }
            set
            {
                if (value != selectedVariable)
                {
                    selectedVariable = value;
                    OnPropertyChanged("SelectedVariable");
                }
            }
        }

        public MainViewModel()
        {
            DocumentVm = new DocumentVM();
            VariableManager = new VariableManager();
            VariablePropertiesVM = new VariablePropertiesViewModel();
        }

        public MainViewModel(RichTextBox mainText) : this()
        {
            MainText = mainText;
        }

        public void InsertVariable()
        {
            var button = MainText.InsertVariableFromSelection();
            var variableAdded = VariableManager.RequestNewVariable(button.Tag as string);
            button.Content = variableAdded.VariableName;
            button.Tag = variableAdded;
            button.Click += variableButton_Click;
        }

        void variableButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedVariable = (sender as Button).Tag as VariableBase;
        }
    }
}
