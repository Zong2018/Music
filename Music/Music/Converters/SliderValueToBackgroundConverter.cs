using Music.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Music.Converters
{
    public class SliderValueToBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double value = (double)values[0];
            double Maxvalue = (double)values[1];

            var paletteHelper = PaletteHelper.Instance;
            var theme = paletteHelper.GetTheme();
            return value == Maxvalue ? new SolidColorBrush(theme.Body) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4a4a4d")) ;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SliderValueToMouseOverBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double value = (double)values[0];
            double Maxvalue = (double)values[1];
            bool IsMouseOver = (bool)values[2];
            var paletteHelper = PaletteHelper.Instance;
            var theme = paletteHelper.GetTheme();
            return value == Maxvalue ? new SolidColorBrush(theme.Body) : (IsMouseOver? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4a4a4d")): new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ec4141")));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SliderValueToCornerRadiusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double value = (double)values[0];
            double Maxvalue = (double)values[1];
            bool IsMouseOver = (bool)values[2];
            if(IsMouseOver) return new CornerRadius(4, 0, 0, 4);
            return value == Maxvalue ? new CornerRadius(4) : new CornerRadius(4,0,0,4);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

	public class MvSliderValueToMouseOverBackgroundConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			double value = (double)values[0];
			double Maxvalue = (double)values[1];
			bool IsMouseOver = (bool)values[2];
			return value == Maxvalue ? Brushes.Black : (IsMouseOver ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4a4a4d")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ec4141")));
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
