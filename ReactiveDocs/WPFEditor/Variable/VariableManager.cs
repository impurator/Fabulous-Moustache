using ReactiveDocs.Core.Model.Variable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEditor.Variable
{
    public class VariableManager 
    {
        public ObservableCollection<VariableBase> Variables { get; set; }

        public VariableManager()
        {
            Variables = new ObservableCollection<VariableBase>() { new VariableFloat{ Value = 2.0}, new VariableInteger{ Value = 45 } };
        }

        public VariableBase RequestNewVariable(string forValue, string name = null)
        {
            var ret = GetVariableFromString(forValue);

            if (!string.IsNullOrEmpty(name))
                ret.VariableName = name;
            else
                ret.VariableName = GetVariableName(ret.Type);

            Variables.Add(ret);

            return ret;
        }

        private VariableBase GetVariableFromString(string forValue)
        {
            var value = forValue.Trim();

            int intVal = 0;
            double doubleVal = 0.0;

            if (int.TryParse(value, out intVal))
            {
                return new VariableInteger { Value = intVal };
            }
            else if (double.TryParse(value, out doubleVal))
            {
                return new VariableFloat { Value = doubleVal };
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
