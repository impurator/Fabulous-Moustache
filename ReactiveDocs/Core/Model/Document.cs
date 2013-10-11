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

        public void AddFloat(string name, double value)
        {
            AddVariable(VariableType.Float, name, value);
        }

        public void AddInt(string name, int value)
        {
            AddVariable(VariableType.Integer, name, value);
        }

        public void AddVariable(VariableType variableType, string name, object value)
        {
            Parts.Add(new VariableTextBox { BindingName = name, ForType = variableType });

            var variableBase = VariableFactory.CreateVariable(variableType);

            Variables.Add(name, value);
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

                var boundValue = Variables[rule.BindingName];
                if (boundValue is int)
                {
                    Variables[rule.BindingName] = (int)Math.Round(rule.Evaluate(Variables));
                }
                else if (boundValue is double)
                {
                    Variables[rule.BindingName] = rule.Evaluate(Variables);
                }
            }
        }

    }
}
