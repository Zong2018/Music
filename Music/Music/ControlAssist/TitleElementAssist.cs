using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace Music.ControlAssist
{
    /// <summary>
    /// 标题元素
    /// </summary>
    public static class TitleElementAssist
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
            DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(TitleElementAssist), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))));


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
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(TitleElementAssist), new PropertyMetadata(new Thickness(0, 0, 0, 0)));



        public static Dock GetTitlePlacement(DependencyObject obj)
        {
            return (Dock)obj.GetValue(TitlePlacementProperty);
        }

        public static void SetTitlePlacement(DependencyObject obj, Dock value)
        {
            obj.SetValue(TitlePlacementProperty, value);
        }

        // Using a DependencyProperty as the backing store for TitlePlacement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitlePlacementProperty =
            DependencyProperty.RegisterAttached("TitlePlacement", typeof(Dock), typeof(TitleElementAssist), new PropertyMetadata(Dock.Left));






        public static double GetTitleWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(TitleWidthProperty);
        }

        public static void SetTitleWidth(DependencyObject obj, double value)
        {
            obj.SetValue(TitleWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for TitleHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleWidthProperty =
            DependencyProperty.RegisterAttached("TitleWidth", typeof(double), typeof(TitleElementAssist), new PropertyMetadata(30.0));

        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(TitleProperty, value);
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached("Title", typeof(string), typeof(TitleElementAssist), new PropertyMetadata(""));



    }
}
