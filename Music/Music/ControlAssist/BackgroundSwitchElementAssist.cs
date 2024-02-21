using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Music.ControlAssist
{
    /// <summary>
    /// 可切换背景的元素
    /// </summary>
    public static class BackgroundSwitchElementAssist
    {
        public static Brush GetMouseHoverBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseHoverBackgroundProperty);
        }

        public static void SetMouseHoverBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseHoverBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseHoverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseHoverBackgroundProperty =
            DependencyProperty.RegisterAttached("MouseHoverBackground", typeof(Brush), typeof(BackgroundSwitchElementAssist), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0))));


        public static Brush GetMouseDownBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseDownBackgroundProperty);
        }

        public static void SetMouseDownBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseDownBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseDownBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseDownBackgroundProperty =
            DependencyProperty.RegisterAttached("MouseDownBackground", typeof(Brush), typeof(BackgroundSwitchElementAssist), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0))));
    }
}
