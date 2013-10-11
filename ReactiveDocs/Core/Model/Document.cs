using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ReactiveDocs.Core.Model.DocumentPart;
using ReactiveDocs.Core.Model.DocumentRule;
using ReactiveDocs.Core.Model.Variable;

namespace ReactiveDocs.Core.Model
{
    public class Document
    {
        public Dictionary<string, VariableBase> Variables { get; set; }
        public List<PartBase> Parts { get; set; }
        public List<RuleBase> Rules { get; set; }

        public Dictionary<string, object> VariablePairs
        {
            get
            {
                return Variables.Select(x => new KeyValuePair<string, object>(x.Key, x.Value.Value)).ToDictionary(x => x.Key, y => y.Value);
            }
        }

        public Document()
        {
            Variables = new Dictionary<string, VariableBase>();
            Parts = new List<PartBase>();
            Rules = new List<RuleBase>();
        }

        public void AddText(string text)
        {
            Parts.Add(new StaticText { Text = text });
        }

        public void AddFloat(string name, double value, bool isReadOnly)
        {
            AddVariable(VariableType.Float, name, value, isReadOnly);
        }

        public void AddInt(string name, int value, bool isReadOnly)
        {
            AddVariable(VariableType.Integer, name, value, isReadOnly);
        }

        public void AddVariable(VariableType variableType, string name, object value, bool isReadOnly)
        {
            Parts.Add(new VariableTextBox { BindingName = name, ForType = variableType, IsReadOnly = isReadOnly });

            var variableBase = VariableFactory.CreateVariable(variableType);
            variableBase.Value = value;
            
            Variables.Add(name, variableBase);
        }

        //This sort of acts like an enum
        public void AddSwitchingTexts(string bindingName, int defaultValue, bool isReadOnly, params string[] texts)
        {
            var newSwitchingText = new SwitchingText
            {
                BindingName = bindingName,
                Texts = new List<string>(texts),
                IsReadOnly = isReadOnly
            };

            Parts.Add(newSwitchingText);

            var variableInt = VariableFactory.CreateVariable(VariableType.Integer);
            variableInt.Value = defaultValue;

            Variables.Add(bindingName, variableInt);
        }

        public void AddSimpleRule(string bindingName, string expressionString)
        {
            Rules.Add(new SimpleAssignmentRule { BindingName = bindingName, Expression = expressionString });
        }

        public void RunRules(string excludedVariable)
        {
            foreach (var rule in Rules)
            {
                if (rule.BindingName == excludedVariable)
                    continue;

                var boundVariable = Variables[rule.BindingName];
                if (boundVariable.Type == VariableType.Integer)
                {
                    Variables[rule.BindingName].Value = (int)Math.Round(rule.Evaluate(VariablePairs));
                }
                else if (boundVariable.Type == VariableType.Float)
                {
                    Variables[rule.BindingName].Value = rule.Evaluate(VariablePairs);
                }
            }
        }

    }
}
