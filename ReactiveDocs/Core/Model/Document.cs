using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ReactiveDocs.Core.Model.DocumentPart;
using ReactiveDocs.Core.Model.DocumentRule;

namespace ReactiveDocs.Core.Model
{
    public class Document
    {
        public Dictionary<string, object> Variables { get; set; }
        public List<PartBase> Parts { get; set; }
        public List<RuleBase> Rules { get; set; }

        public Document()
        {
            Variables = new Dictionary<string, object>();
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

        // There's gotta be a less shitty way to handle numeric types.
        // Like inferring the type or using double for everything, etc.
        // No time to refactor!
        public void AddVariable(VariableType variableType, string name, object value)
        {
            switch (variableType)
            {
                case VariableType.Integer:
                    Parts.Add(new VariableInteger { BindingName = name });
                    break;
                case VariableType.Float:
                    Parts.Add(new VariableFloat { BindingName = name });
                    break;
                default:
                    throw new NotImplementedException();
            }

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
