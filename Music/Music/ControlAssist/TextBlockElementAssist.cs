using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Music.ControlAssist
{
    public static class TextBlockElementAssist
    {
        public static Brush GetForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ForegroundProperty);
        }

        public static void SetForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(TextBlockElementAssist), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))));


        public static Thickness GetMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarginProperty);
        }

        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(TextBlockElementAssist), new PropertyMetadata(new Thickness(0, 0, 0, 0)));



        public static string GetExText(DependencyObject obj)
        {
            return (string)obj.GetValue(ExTextProperty);
        }

        public static void SetExText(DependencyObject obj, string value)
        {
            obj.SetValue(ExTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for ExText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExTextProperty =
            DependencyProperty.RegisterAttached("ExText", typeof(string), typeof(TextBlockElementAssist), new PropertyMetadata(""));


    }
}
