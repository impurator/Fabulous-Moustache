using ReactiveDocs.Core.Model;
using ReactiveDocs.Core.Model.DocumentPart;
using ReactiveDocs.WPFReader.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace ReactiveDocs.WPFReader.ViewModel
{
    public class DocumentVM : ViewModelBase
    {
        public FlowDocument Document { get; private set; }

        public Dictionary<string, object> BoundValues
        {
            get
            {
                return reactiveDocument.Variables;
            }
        }

        private Document reactiveDocument;
        private Paragraph currentParagraph = null;
        private bool recalculating = false;

        public DocumentVM()
        {
            Document = new FlowDocument();
        }

        public DocumentVM(Document baseDoc)
        {
            reactiveDocument = baseDoc;
            Document = CreateFlowDocumentFromReactiveDoc(baseDoc);
            Document.LineHeight = 30;
            Document.ColumnWidth = 1024;
        }

        private FlowDocument CreateFlowDocumentFromReactiveDoc(Document fromDoc)
        {
            var ret = new FlowDocument();

            foreach (var part in fromDoc.Parts)
            {
                AddReactiveDocPartToFlowDocument(ret, part);
            }

            ret.Blocks.Add(currentParagraph);

            return ret;
        }

        private void AddReactiveDocPartToFlowDocument(FlowDocument toAddTo, PartBase toAdd)
        {
            if (currentParagraph == null)
                currentParagraph = new Paragraph();

            if (toAdd is StaticText)
            {
                var staticText = toAdd as StaticText;

                currentParagraph.Inlines.Add(new Run(staticText.Text));
            }
            if (toAdd is ParagraphBreak)
            {
                toAddTo.Blocks.Add(currentParagraph);
                currentParagraph = new Paragraph();
            }
            else if (toAdd is VariableInteger)
            {
                var variableInteger = toAdd as VariableInteger;

                if (string.IsNullOrEmpty(variableInteger.BindingName))
                    throw new Exception("Binding name cannot be null or empty for non-static document parts.");

                if (!BoundValues.ContainsKey(variableInteger.BindingName))
                    throw new Exception("Duplicate binding name not found: " + variableInteger.BindingName + ".");

                var textBox = new IntegerUpDown();
                var textSize = TextLayoutHelper.MeasureString(BoundValues[variableInteger.BindingName].ToString(), textBox);
                textBox.Width = textSize.Width + 50;
                textBox.Height = 24;
                textBox.Margin = new Thickness(6, 0, 6, 0);
                textBox.ValueChanged += textBox_ValueChanged;
                textBox.Tag = variableInteger.BindingName;

                //Dynamically generate the binding?  Yup, went there.
                //reactiveDocument.BoundValues.Add(variableInteger.BindingName, variableInteger.Value);
                var binding = new Binding("BoundValues[" + variableInteger.BindingName + "]");
                binding.Source = this;
                binding.Mode = BindingMode.TwoWay;
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                textBox.SetBinding(IntegerUpDown.ValueProperty, binding);

                currentParagraph.Inlines.Add(textBox);
            }
            else if (toAdd is VariableFloat)
            {
                var variableFloat = toAdd as VariableFloat;

                if (string.IsNullOrEmpty(variableFloat.BindingName))
                    throw new Exception("Binding name cannot be null or empty for non-static document parts.");

                if (!BoundValues.ContainsKey(variableFloat.BindingName))
                    throw new Exception("Duplicate binding name not found: " + variableFloat.BindingName + ".");

                var textBox = new DoubleUpDown();
                var textSize = TextLayoutHelper.MeasureString(BoundValues[variableFloat.BindingName].ToString(), textBox);
                textBox.Width = textSize.Width + 50;
                textBox.Height = 24;
                textBox.Margin = new Thickness(6, 0, 6, 0);
                textBox.ValueChanged += textBox_ValueChanged;
                textBox.Tag = variableFloat.BindingName;

                var binding = new Binding("BoundValues[" + variableFloat.BindingName + "]");
                binding.Source = this;
                binding.Mode = BindingMode.TwoWay;
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                textBox.SetBinding(DoubleUpDown.ValueProperty, binding);
            }
        }

        void textBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Don't call VariableChanged on initialize or when already performing a recalc
            if (!recalculating  && e.OldValue != null)
            {
                recalculating = true;

                OnVariableChanged((sender as FrameworkElement).Tag.ToString());

                recalculating = false;
            }
        }

        private void OnVariableChanged(string variableName)
        {
            reactiveDocument.RunRules(variableName);
            NotifyPropertyChanged("BoundValues");
        }
    }
}
