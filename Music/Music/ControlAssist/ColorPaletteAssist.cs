using Music.Controls;
using Music.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Music.ControlAssist
{
    public static class ColorPaletteAssist
    {


        public static Brush GetBarBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BarBrushProperty);
        }

        public static void SetBarBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(BarBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for BarBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarBrushProperty =
            DependencyProperty.RegisterAttached("BarBrush", typeof(Brush), typeof(ColorPaletteAssist), new PropertyMetadata(Brushes.Pink));



        public static ICommand GetSelectedChangedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(SelectedChangedCommandProperty);
        }

        public static void SetSelectedChangedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(SelectedChangedCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedChangedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedChangedCommandProperty =
            DependencyProperty.RegisterAttached("SelectedChangedCommand", typeof(ICommand), typeof(ColorPaletteAssist), new PropertyMetadata(null, new PropertyChangedCallback(PropertyChangedCallback)));

        static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cp = d as ColorPalette;
            if (cp != null && e.NewValue is ICommand)
            {
                cp.SelectionChanged
                  += (obj, args) =>
                  {
                      if ((e.NewValue as ICommand).CanExecute(obj))
                      {
						  var tab = cp.LogicalTreeAncestory().OfType<TabControl>().FirstOrDefault();
						  var tabItem = cp.LogicalTreeAncestory().OfType<TabItem>().FirstOrDefault();
						  var array = new object [2];
						  if(tab != null && tabItem!= null)
						  {
							  array[0] = cp.IsSlider ? "自定义" : tabItem.Header.ToString();
							  array[1] = cp.SelectedItem;
						  }
						  //Array param = new arr
						  int index = tab.ItemContainerGenerator.IndexFromContainer(cp);
                          (e.NewValue as ICommand).Execute(array);
                      }
                  };
            }
        }

    }
}
