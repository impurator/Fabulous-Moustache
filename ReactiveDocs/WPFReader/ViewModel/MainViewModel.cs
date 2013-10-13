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

        //loldemo
        public void FlipText()
        {
            DocumentVM.BoundValues["IsTaxPerVehicle"].Value = (int)DocumentVM.BoundValues["IsTaxPerVehicle"].Value == 0 ? 1 : 0;
            DocumentVM.BoundValues["AdmissionAppliesToEveryone"].Value = (int)DocumentVM.BoundValues["AdmissionAppliesToEveryone"].Value == 0 ? 1 : 0;

            DocumentVM.NotifyPropertyChanged("BoundValues");
        }

        public void CreateParkExampleDoc()
        {
            var doc = new Document();

            doc.AddText("Proposition 21:  Vehicle License Fee for State Parks");
            doc.Parts.Add(new ParagraphBreak());

            doc.AddText("The way it is now:");
            doc.Parts.Add(new ParagraphBreak());
            
            doc.AddText("California has" );
            doc.AddInt("ParkCount", 278, true);
            doc.AddText("state parks, including state beaches and historic parks. The current $");
            doc.AddInt("CurrentBudget", 400000000, true);
            doc.AddText("budget is insufficient to maintain these parks, and");
            doc.AddInt("CurrentClosingParks", 150, true);
            doc.AddText("parks will be shut down at least part-time.  Most parks charge $");
            doc.AddInt("CurrentAdmission", 12, true);
            doc.AddText("per vehicle for admission.");
            doc.Parts.Add(new ParagraphBreak());
            
            doc.AddText("What Prop 21 would do:");
            doc.Parts.Add(new ParagraphBreak());

            doc.AddText("Proposes to charge car owners an extra $18 on their annual registration bill, to go into the state park fund.  Cars that pay the charge would have free park admission.");
            doc.Parts.Add(new ParagraphBreak());

            doc.AddText("Analysis:");
            doc.Parts.Add(new ParagraphBreak());

            doc.AddText("Suppose that an extra $");
            doc.AddInt("ATax", 18, false);
            doc.AddText("was charged to");
            doc.AddInt("PercentCompliance", 100, false);
            doc.AddText("% of");
            doc.AddSwitchingTexts("IsTaxPerVehicle", 1, false, new string[] { "California taxpayers", "vehicle registrations" });
            doc.AddText(".  Park admission would be $");
            doc.AddInt("NewAdmission", 0, false);
            doc.AddText(" for ");
            doc.AddSwitchingTexts("AdmissionAppliesToEveryone", 0, true, new string[] { "those who paid the charge", "everyone" });
            doc.AddText(".");
            doc.Parts.Add(new ParagraphBreak());

            doc.AddText("This would lose/gain $");
            doc.AddSwitchingTexts("IsBudgetDeltaPositive", 0, true, new string[] { "lose $", "collect an extra $" });
            doc.AddInt("BudgetDelta", 0, true);
            doc.AddText(" ($ ");
            doc.AddInt("NewTaxCollected", 0, true);
            doc.AddText(" from the tax, plus/minus $");
            doc.AddInt("AdmissionRevenue", 0, true);
            doc.AddText("revenue from admission) for a total state park budget of $");
            doc.AddInt("NewBudget", 0, true);
            doc.AddText(".");

            //doc.AddSimpleRule("IsBudgetDeltaPositive", "ATax * PercentCompliance / 100");

            doc.AddSimpleRule("NewTaxCollected", "ATax * (PercentCompliance / 100) * 28000000");
            doc.AddSimpleRule("AdmissionRevenue", "NewAdmission * 140000" );
            doc.AddSimpleRule("BudgetDelta", "NewTaxCollected + AdmissionRevenue");
            doc.AddSimpleRule("NewBudget", "CurrentBudget + BudgetDelta");

            DocumentVM = new DocumentVM(doc);
        }
    }
}
