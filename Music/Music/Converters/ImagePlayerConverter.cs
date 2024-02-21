using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Music.Converters
{

	public class HeightImagePlayerConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var value1 = (string)values[0];
			var value2 = (double)values[1];
			double height = 0;
			if (value1 == "center")
			{
				height = value2 / 6 * 5;
			}
			else if (value1 == "right")
			{
				height = value2 / 3*2;
			}
			else if (value1 == "left")
			{
				height = value2 / 3 * 2;
			}
			else if (value1 == "last")
			{
				height = value2 / 4 * 3;
			}
			return height;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class WidthImagePlayerConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var value1 = (string)values[0];
			var value2 =(double)values[1];
			double width = 0;
			if(value1 == "center")
			{
				width = value2/38*27;
			}
			else if (value1 == "right")
			{
				width = value2/2;
			}
			else if (value1 == "left")
			{
				width = value2/2;
			}
			else if (value1 == "last")
			{
				width = value2/38*11;
			}
			return width;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class LeftImagePlayerConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var value1 = (string)values[0];
			var value2 = (double)values[1];
			var value3 = (double)values[2];
			double left = 0;
			if (value1 == "center")
			{
				left = (value2 - value3)/2;
			}
			else if (value1 == "right")
			{
				left = value2 / 2;
			}
			else if (value1 == "left")
			{
				left = 0;
			}
			else if (value1 == "last")
			{
				left = (value2 - value3) / 2;
			}
			return left;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

    public class ParentToWidthImagePlayerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			double width = (double)parameter;
			return width * 0.8;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ParentToFontSizeImagePlayerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
			string state = (string)values[0];
			double size = (double)values[1];
			if(state == "center")
            {
				return size;
			}
			return size * 0.8;
		}

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
