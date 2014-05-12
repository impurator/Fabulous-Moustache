using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ReactiveDocs.Core.Helper;
using ReactiveDocs.Core.Model.Variable;

namespace WPFEditor.Helper
{
    public class VariableTypeToStringValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))  // This is for databinding
                return VariableType.Integer;
            return (StringToEnum<VariableType>(value.ToString())).GetDescription(); // <-- The extension method
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString())) // This is for databinding
                return VariableType.Integer;
            return StringToEnum<VariableType>(value.ToString());
        }

        public static T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        #endregion
    }
}
