﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Primitives;

namespace ReactiveDocs.WPFReader.Helper
{
    public static class TextLayoutHelper
    {
        public static Size MeasureString(string candidate, Typeface forTypeface, double fontSize)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                forTypeface,
                fontSize,
                Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }

        public static Size MeasureString(string candidate, InputBase textBox)
        {
            return MeasureString(candidate,
                            new Typeface(textBox.FontFamily, textBox.FontStyle, textBox.FontWeight, textBox.FontStretch),
                            textBox.FontSize);
        }
    }
}
