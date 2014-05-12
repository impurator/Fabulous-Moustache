using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using ReactiveDocs.Core.Model.Variable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using WPFEditor.Export;
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

            button.Content = variableAdded.Variable.VariableName;

            button.Tag = variableAdded;
            button.Click += variableButton_Click;

            VariablePropertiesVM.SelectedVariableInstance = variableAdded;
        }

        void variableButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            VariablePropertiesVM.SelectedVariableInstance = (sender as Button).Tag as VariableInstance;
        }

        public void Export()
        {
            var convertedDoc = CopyDocument(MainText.Document);
            ConvertVariables(convertedDoc);
            var convertedRichTextBox = new RichTextBox() {Document = convertedDoc};
            var html = RtfToHtmlConverter.ConvertRtfToHtml(convertedRichTextBox);
            var tangledHtml = TangleHtml(html);
            using (var fs = new StreamWriter(@"C:\Tangle-0.1.0\TangleTest.html", false))
            {
                fs.Write(tangledHtml);
            }
        }

        private void ConvertVariables(FlowDocument fromDocument)
        {
            var paragraphs = fromDocument.Blocks.Where(x => x is Paragraph).ToList();
            foreach (var paragraph in paragraphs)
            {
                var para = paragraph as Paragraph;
                var uiContainers = para.Inlines.Where(x => x is InlineUIContainer).ToList();

                foreach (var container in uiContainers)
                {
                    //Get the variable name
                    var button = (container as InlineUIContainer).Child as Button;
                    var variableName = "{{" + (button.Tag as VariableInstance).Variable.VariableName + "}}";
                    para.Inlines.InsertBefore(container, new Run(variableName));
                    para.Inlines.Remove(container);
                }
            }
        }

        private FlowDocument CopyDocument(FlowDocument from)
        {
            string xaml = XamlWriter.Save(from);

            return (FlowDocument) XamlReader.Parse(xaml);
        }

        private string TangleHtml(string htmlString)
        {
            var ret = new StringBuilder();
            ret.AppendLine("<!DOCTYPE html>");
            ret.AppendLine("<html>");
            ret.AppendLine("<head>");
            ret.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            ret.AppendLine("    <title>Tangle document</title>");
            ret.AppendLine("");
            ret.AppendLine("    <script type=\"text/javascript\" src=\"Tangle.js\"></script>");
            ret.AppendLine("");
            ret.AppendLine("    <link rel=\"stylesheet\" href=\"TangleKit/TangleKit.css\" type=\"text/css\">");
            ret.AppendLine("    <script type=\"text/javascript\" src=\"TangleKit/mootools.js\"></script>");
            ret.AppendLine("    <script type=\"text/javascript\" src=\"TangleKit/sprintf.js\"></script>");
            ret.AppendLine("    <script type=\"text/javascript\" src=\"TangleKit/BVTouchable.js\"></script>");
            ret.AppendLine("    <script type=\"text/javascript\" src=\"TangleKit/TangleKit.js\"></script>");
            ret.AppendLine("");
            ret.AppendLine("    <script type=\"text/javascript\">");
            ret.AppendLine("");
            ret.AppendLine("        function setUpTangle () {");
            ret.AppendLine("            var element = document.getElementById(\"exporteddoc\");");
            ret.AppendLine("");
            ret.AppendLine("            var tangle = new Tangle(element, {");
            ret.AppendLine("                initialize: function () {");

            foreach (var variableInstance in VariableManager.Variables.Where(x => x.Variable is VariableInteger || x.Variable is VariableFloat))
            {
                ret.AppendLine("                    this." + variableInstance.Variable.VariableName + " = " +  variableInstance.Variable.GetDefaultValue() + ";");
            }

            ret.AppendLine("                },");
            ret.AppendLine("                update: function () {");

            foreach (var variableInstance in VariableManager.Variables.Where(x => x.Variable is VariableBasic))
            {
                ret.AppendLine("                    this." + variableInstance.Variable.VariableName + " = " + (variableInstance.Variable as VariableBasic).DecodedEvaluationString + ";");                
            }

            ret.AppendLine("                }");
            ret.AppendLine("            });");
            ret.AppendLine("        }");
            ret.AppendLine("");
            ret.AppendLine("    </script>");
            ret.AppendLine("</head>");
            ret.AppendLine("");
            ret.AppendLine("<body onload=\"setUpTangle();\">");
            ret.AppendLine("");

            ret.AppendLine(ConvertHtml(htmlString));
            
            ret.AppendLine("");
            ret.AppendLine("</body>");
            ret.AppendLine("</html>");

            return ret.ToString();
        }

        private string ConvertHtml(string htmlString)
        {
            string ret = htmlString;

            ret = Regex.Replace(ret, "<DIV ", "<DIV id=\"exporteddoc\"");

            var matches = Regex.Matches(htmlString, "<SPAN>{{(\\w*)}}</SPAN>");

            foreach (var match in matches)
            {
                var capture = (match as Match).Groups[1];
                var variable = VariableManager.Variables.First(x => x.Variable.VariableName == capture.Value);
                var replacement = new StringBuilder();
                replacement.Append("<span data-var=\"");
                replacement.Append(variable.Variable.VariableName);
                replacement.Append("\" ");

                if (variable.Variable.Type == VariableType.Integer || variable.Variable.Type == VariableType.Float)
                {
                    replacement.Append("class=\"TKAdjustableNumber\" ");

                    if (variable.Variable.Type == VariableType.Integer)
                    {
                        var intVar = variable.Variable as VariableInteger;
                        replacement.Append("data-min=\"" + intVar.Minimum + "\" ");
                        replacement.Append("data-max=\"" + intVar.Maximum + "\" ");
                        replacement.Append("data-format=\"" + intVar.FormatString + "\" ");
                    }
                    else if (variable.Variable.Type == VariableType.Float)
                    {
                        var floatVar = variable.Variable as VariableFloat;
                        replacement.Append("data-min=\"" + floatVar.Minimum + "\" ");
                        replacement.Append("data-max=\"" + floatVar.Maximum + "\" ");
                        replacement.Append("data-format=\"" + floatVar.FormatString + "\" ");
                    }
                }
                else if (variable.Variable.Type == VariableType.Basic)
                {
                    var basicVar = variable.Variable as VariableBasic;
                    replacement.Append("data-format=\"" + basicVar.FormatString + "\" ");
                }

                replacement.Append("></span>");
                ret = Regex.Replace(ret, "<SPAN>({{" + variable.Variable.VariableName + "}})</SPAN>",
                    replacement.ToString());
            }

            return ret;
        }
    }
}
