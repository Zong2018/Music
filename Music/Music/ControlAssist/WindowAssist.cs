using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Music.ControlAssist
{
    public static class WindowAssist
    {
        public static bool GetWindowResult(DependencyObject obj)
        {
            return (bool)obj.GetValue(WindowResultProperty);
        }

        public static void SetWindowResult(DependencyObject obj, bool value)
        {
            obj.SetValue(WindowResultProperty, value);
        }

        // Using a DependencyProperty as the backing store for WindowResult.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowResultProperty =
            DependencyProperty.RegisterAttached("WindowResult", typeof(bool), typeof(WindowAssist), new PropertyMetadata(false,new PropertyChangedCallback(WindowResultPropertyChangedCallback)));
        static void WindowResultPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if((bool)e.NewValue)
            {
                Window win = d as Window;
                if (win != null)
                {
                    win.Close();
                    //SetWindowResult(win,false);
                }
            }
        }
    }
}
