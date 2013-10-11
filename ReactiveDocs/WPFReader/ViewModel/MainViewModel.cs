using ReactiveDocs.Core.Model;
using ReactiveDocs.Core.Model.DocumentPart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.WPFReader.ViewModel
{
    public class MainViewModel
    {
        public DocumentVM DocumentVM { get; set; }

        public MainViewModel()
        {
            //DocumentVM = new DocumentVM();
            CreateParkExampleDoc();
        }

        public void CreateTestDoc()
        {
            var doc = new Document();
            doc.Parts.Add(new StaticText { Text = "Since the meaning of life is" });
            doc.Parts.Add(new VariableInteger { Value = 42, BindingName = "MeaningOfLifeInt" });
            doc.Parts.Add(new StaticText { Text = "this means that we can infer that the meaning of death is" });
            doc.Parts.Add(new VariableInteger { Value = 24, BindingName = "MeaningOfDeathInt" });
            doc.Parts.Add(new StaticText { Text = ".  However, I've often been told that you only can do what you know how to do well, and that's be you, be what you're like, be like yourself, you know.  So I'm having a wonderful time but I'd rather be whistling in the dark." });

            DocumentVM = new DocumentVM(doc);
        }

        public void CreateParkExampleDoc()
        {
            var doc = new Document();
            doc.Parts.Add(new StaticText { Text = "Proposition 21:  Vehicle License Fee for State Parks" });
            doc.Parts.Add(new ParagraphBreak());

            doc.Parts.Add(new StaticText { Text = "The way it is now:" });
            doc.Parts.Add(new ParagraphBreak());
            
            doc.Parts.Add(new StaticText { Text = "California has" });
            doc.Parts.Add(new VariableInteger { Value = 278, BindingName = "ParkCount" });
            doc.Parts.Add(new StaticText { Text = "state parks, including state beaches and historic parks. The current" });
            doc.Parts.Add(new VariableInteger { Value = 400000000, BindingName = "CurrentBudget" });
            doc.Parts.Add(new StaticText { Text = "budget is insufficient to maintain these parks, and" });
            doc.Parts.Add(new VariableInteger { Value = 150, BindingName = "CurrentClosingParks" });
            doc.Parts.Add(new StaticText { Text = "parks will be shut down at least part-time.  Most parks charge" });
            doc.Parts.Add(new VariableInteger { Value = 12, BindingName = "CurrentAdmission" });
            doc.Parts.Add(new StaticText { Text = "per vehicle for admission." });
            doc.Parts.Add(new ParagraphBreak());
            
            doc.Parts.Add(new StaticText { Text = "What Prop 21 would do:" });
            doc.Parts.Add(new ParagraphBreak());

            doc.Parts.Add(new StaticText { Text = "Proposes to charge car owners an extra $18 on their annual registration bill, to go into the state park fund.  Cars that pay the charge would have free park admission." });
            doc.Parts.Add(new ParagraphBreak());

            doc.Parts.Add(new StaticText { Text = "Analysis:" });
            doc.Parts.Add(new ParagraphBreak());

            DocumentVM = new DocumentVM(doc);
        }
    }
}
