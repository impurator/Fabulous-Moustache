using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Helper
{
    public static class EnumGetDescription
    {
        public static string GetDescription(this Enum enumObj)
        {
            var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                var attrib = attribArray[0] as DescriptionAttribute;
                return attrib.Description;
            }
        }
    }
}
