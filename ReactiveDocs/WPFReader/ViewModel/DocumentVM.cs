using ReactiveDocs.Core.Model;
using ReactiveDocs.Core.Model.DocumentPart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit;

namespace ReactiveDocs.WPFReader.ViewModel
{
    public class DocumentVM : ViewModelBase
    {
        public FlowDocument Document { get; private set; }

        public Dictionary<string, object> BoundValues { get; private set; }

        private Paragraph currentParagraph = null;

        public DocumentVM()
        {
            Document = new FlowDocument();
        }

        public DocumentVM(Document baseDoc)
        {
            BoundValues = new Dictionary<string, object>();
            Document = CreateFlowDocumentFromReactiveDoc(baseDoc);
            Document.LineHeight = 20;
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

                if (BoundValues.ContainsKey(variableInteger.BindingName))
                    throw new Exception("Duplicate binding name found: " + variableInteger.BindingName + ".");

                var textBox = new IntegerUpDown();
                textBox.Width = 60;
                textBox.Height = 20;
                textBox.Margin = new Thickness(6, 0, 6, 0);
                textBox.ValueChanged += textBox_ValueChanged;
                textBox.Tag = variableInteger.BindingName;

                //Dynamically generate the binding?  Yup, went there.
                BoundValues.Add(variableInteger.BindingName, variableInteger.Value);
                var binding = new Binding("BoundValues[" + variableInteger.BindingName + "]");
                binding.Source = this;
                binding.Mode = BindingMode.TwoWay;
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                textBox.SetBinding(IntegerUpDown.TextProperty, binding);

                currentParagraph.Inlines.Add(textBox);
            }
            else if (toAdd is VariableFloat)
            {
                var variableFloat = toAdd as VariableFloat;

                if (string.IsNullOrEmpty(variableFloat.BindingName))
                    throw new Exception("Binding name cannot be null or empty for non-static document parts.");

                if (BoundValues.ContainsKey(variableFloat.BindingName))
                    throw new Exception("Duplicate binding name found: " + variableFloat.BindingName + ".");

                var textBox = new DoubleUpDown();
                textBox.Width = 60;
                textBox.Height = 20;
                textBox.Margin = new Thickness(6, 0, 6, 0);
                textBox.ValueChanged += textBox_ValueChanged;
                textBox.Tag = variableFloat.BindingName;

                BoundValues.Add(variableFloat.BindingName, variableFloat.Value);
                var binding = new Binding("BoundValues[" + variableFloat.BindingName + "]");
                binding.Source = this;
                binding.Mode = BindingMode.TwoWay;
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                textBox.SetBinding(IntegerUpDown.TextProperty, binding);
            }
        }

        void textBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            OnVariableChanged();
        }

        private void OnVariableChanged()
        {
            int i = 0;
        }
    }
}
