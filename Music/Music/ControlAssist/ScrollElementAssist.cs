using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Music.ControlAssist
{
    public static class ScrollElementAssist
    {
        public static Brush GetThumbForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ThumbForegroundProperty);
        }

        public static void SetThumbForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ThumbForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbForegroundProperty =
            DependencyProperty.RegisterAttached("ThumbForeground", typeof(System.Windows.Media.Brush), typeof(ScrollElementAssist), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF969696"))));

        public static Brush GetThumbBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ThumbBackgroundProperty);
        }

        public static void SetThumbBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ThumbBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbBackgroundProperty =
            DependencyProperty.RegisterAttached("ThumbBackground", typeof(Brush), typeof(ScrollElementAssist), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#33BDBDBD"))));



        public static double GetRadius(DependencyObject obj)
        {
            return (double)obj.GetValue(RadiusProperty);
        }

        public static void SetRadius(DependencyObject obj, double value)
        {
            obj.SetValue(RadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.RegisterAttached("Radius", typeof(double), typeof(ScrollElementAssist), new PropertyMetadata(3.0));



        public static double GetThumbSize(DependencyObject obj)
        {
            return (double)obj.GetValue(ThumbSizeProperty);
        }

        public static void SetThumbSize(DependencyObject obj, double value)
        {
            obj.SetValue(ThumbSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for ThumbSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbSizeProperty =
            DependencyProperty.RegisterAttached("ThumbSize", typeof(double), typeof(ScrollElementAssist), new PropertyMetadata(6.0));


    }
}
