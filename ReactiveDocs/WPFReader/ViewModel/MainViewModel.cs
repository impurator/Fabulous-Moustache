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

        public void CreateParkExampleDoc()
        {
            var doc = new Document();

            doc.AddText("Proposition 21:  Vehicle License Fee for State Parks");
            doc.Parts.Add(new ParagraphBreak());

            doc.AddText("The way it is now:");
            doc.Parts.Add(new ParagraphBreak());
            
            doc.AddText("California has" );
            doc.AddInt("ParkCount", 278);
            doc.AddText("state parks, including state beaches and historic parks. The current");
            doc.AddInt("CurrentBudget", 400000000);
            doc.AddText("budget is insufficient to maintain these parks, and");
            doc.AddInt("CurrentClosingParks", 150);
            doc.AddText("parks will be shut down at least part-time.  Most parks charge");
            doc.AddInt("CurrentAdmission", 12);
            doc.AddText("per vehicle for admission.");
            doc.Parts.Add(new ParagraphBreak());
            
            doc.AddText("What Prop 21 would do:");
            doc.Parts.Add(new ParagraphBreak());

            doc.AddText("Proposes to charge car owners an extra $18 on their annual registration bill, to go into the state park fund.  Cars that pay the charge would have free park admission.");
            doc.Parts.Add(new ParagraphBreak());

            doc.AddText("Analysis:");
            doc.Parts.Add(new ParagraphBreak());

            doc.AddText("Suppose that an extra");
            doc.AddInt("Tax", 18);

            doc.AddSimpleRule("Tax", "CurrentAdmission + 5");
            doc.AddSimpleRule("CurrentClosingParks", "ParkCount - 100");

            DocumentVM = new DocumentVM(doc);
        }
    }
}
