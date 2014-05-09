using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPFEditor.ViewModel
{
    public class MainViewModel
    {
        public DocumentVM DocumentVm { get; set; }

        public MainViewModel()
        {
            DocumentVm = new DocumentVM();
        }

        public void DoShit(TextSelection selection)
        {

        }
    }
}
