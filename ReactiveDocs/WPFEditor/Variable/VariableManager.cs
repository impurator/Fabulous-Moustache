using ReactiveDocs.Core.Model.Variable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFEditor.Helper;

namespace WPFEditor.Variable
{
    public class VariableManager 
    {
        public ObservableCollection<VariableInstance> Variables { get; set; }

        public VariableManager()
        {
            Variables = new ObservableCollection<VariableInstance>();
        }

        public VariableInstance RequestNewVariable(string forValue, string name = null)
        {
            var ret = GetVariableFromString(forValue);

            if (!string.IsNullOrEmpty(name))
                ret.VariableName = name;
            else
                ret.VariableName = GetVariableName(ret.Type);

            var instance = new VariableInstance() {Variable = ret};

            Variables.Add(instance);

            return instance;
        }

        private VariableBase GetVariableFromString(string forValue)
        {
            var value = forValue.Trim();

            int intVal = 0;
            double doubleVal = 0.0;

            if (int.TryParse(value, out intVal))
            {
                return new VariableInteger(intVal);
            }
            else if (double.TryParse(value, out doubleVal))
            {
                return new VariableFloat (doubleVal);
            }

            return new VariableStringSet { Value = new List<string>() { forValue } };
        }

        private int variableCount = 0;
        private string GetVariableName(VariableType forType)
        {
            return forType.ToString() + variableCount++;
        }

        
    }
}
