using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Music.Converters
{
    public class WidthToLeftSongWordConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var childWidth = (double)values[0];
            var parentWidth = (double)values[1];
            if (childWidth <= 0 || parentWidth <= 0) return new Thickness(0, 0, 0, 0);

            return new Thickness((parentWidth-childWidth) / 2, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
