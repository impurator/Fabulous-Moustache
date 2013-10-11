using ReactiveDocs.Core.Model;
using ReactiveDocs.Core.Model.DocumentPart;
using ReactiveDocs.Core.Model.Variable;
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

        public Dictionary<string, VariableBase> BoundValues
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

            reactiveDocument.RunRules(string.Empty);
            NotifyPropertyChanged("BoundValues");
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
            else if (toAdd is VariableTextBox)
            {
                var variableTextBox = toAdd as VariableTextBox;

                if (string.IsNullOrEmpty(toAdd.BindingName))
                    throw new Exception("Binding name cannot be null or empty for non-static document parts.");

                if (!BoundValues.ContainsKey(toAdd.BindingName))
                    throw new Exception("Duplicate binding name not found: " + toAdd.BindingName + ".");

                var variable = reactiveDocument.Variables[toAdd.BindingName];
                
                //Dynamically generate the binding?  Yup, went there.
                var binding = new Binding
                {
                    Path = new PropertyPath("BoundValues[" + toAdd.BindingName + "].Value", new object[]{}),
                    Source = this,
                    Mode = variableTextBox.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                };

                if (variableTextBox.IsReadOnly)
                {
                    var run = new Run();
                    run.SetBinding(Run.TextProperty, binding);
                    currentParagraph.Inlines.Add(new Run(" "));
                    currentParagraph.Inlines.Add(run);
                    currentParagraph.Inlines.Add(new Run(" "));
                }
                else
                {
                    switch (variableTextBox.ForType)
                    {
                        case VariableType.Integer:
                        {
                            var textBox = new IntegerUpDown();
                            var textSize = TextLayoutHelper.MeasureString(variable.Value.ToString(), textBox);

                            textBox.Width = textSize.Width + 50;
                            textBox.Height = 24;
                            textBox.Margin = new Thickness(6, 0, 6, 0);
                            textBox.ValueChanged += textBox_ValueChanged;
                            textBox.Tag = toAdd.BindingName;

                            textBox.SetBinding(IntegerUpDown.ValueProperty, binding);
                            currentParagraph.Inlines.Add(textBox);
                            break;
                        }
                        case VariableType.Float:
                        {
                            var textBox = new DoubleUpDown();
                            var textSize = TextLayoutHelper.MeasureString(variable.Value.ToString(), textBox);

                            textBox.Width = textSize.Width + 50;
                            textBox.Height = 24;
                            textBox.Margin = new Thickness(6, 0, 6, 0);
                            textBox.ValueChanged += textBox_ValueChanged;
                            textBox.Tag = toAdd.BindingName;

                            textBox.SetBinding(DoubleUpDown.ValueProperty, binding);
                            currentParagraph.Inlines.Add(textBox);
                            break;
                        }
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            else if (toAdd is SwitchingText)
            {
                var switchingText = toAdd as SwitchingText;

                if (string.IsNullOrEmpty(toAdd.BindingName))
                    throw new Exception("Binding name cannot be null or empty for non-static document parts.");

                if (!BoundValues.ContainsKey(toAdd.BindingName))
                    throw new Exception("Duplicate binding name not found: " + toAdd.BindingName + ".");

                var variable = reactiveDocument.Variables[toAdd.BindingName];

                //Dynamically generate the binding?  Yup, went there.
                var binding = new Binding
                {
                    Path = new PropertyPath("BoundValues[" + toAdd.BindingName + "].Value", new object[] { }),
                    Source = this,
                    Mode = switchingText.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    Converter = new SwitchingTextValueConverter(),
                    ConverterParameter = switchingText
                };

                if (switchingText.IsReadOnly)
                {
                    var run = new Run();
                    run.SetBinding(Run.TextProperty, binding);
                    currentParagraph.Inlines.Add(new Run(" "));
                    currentParagraph.Inlines.Add(run);
                    currentParagraph.Inlines.Add(new Run(" "));
                }
                else
                {
                    var button = new Button();
                    var textSize = TextLayoutHelper.MeasureString(variable.Value.ToString(), new Typeface(button.FontFamily, button.FontStyle, button.FontWeight, button.FontStretch), button.FontSize);

                    button.Width = textSize.Width + 20;
                    button.Height = 24;
                    button.Margin = new Thickness(6, 0, 6, 0);
                    button.Click += ButtonOnClick;
                    button.Tag = toAdd.BindingName;

                    //textBox.SetBinding(IntegerUpDown.ValueProperty, binding);
                    //currentParagraph.Inlines.Add(textBox);
                }
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            throw new NotImplementedException();
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
