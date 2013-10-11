using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ReactiveDocs.Core.Model.DocumentPart;

namespace ReactiveDocs.WPFReader.Helper
{
    public class SwitchingTextValueConverter : IValueConverter
    {
        // Convert from int to string
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var switchingText = parameter as SwitchingText;
            var index = (int) value;

            return switchingText.GetText(index);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var switchingText = parameter as SwitchingText;
            var text = value.ToString();

            return switchingText.GetIndex(text);
        }
    }
}
