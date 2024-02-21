using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Music.Converters
{
    class DurationToDurationStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string durationString = (string)value;
                double duration = 0;
                double.TryParse(durationString,out duration);
                TimeSpan timeSpan = TimeSpan.FromSeconds(duration);
                string min = timeSpan.Minutes.ToString();
                string sec = timeSpan.Seconds.ToString();
                return $"{min.PadLeft(2,'0')}:{sec.PadLeft(2, '0')}";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
