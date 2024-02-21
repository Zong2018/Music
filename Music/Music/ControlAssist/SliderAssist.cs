using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Music.Infrastructure.Extensions;

namespace Music.ControlAssist
{
    public static class SliderAssist
    {


        public static ICommand GetDragEnterCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DragEnterCommandProperty);
        }

        public static void SetDragEnterCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DragEnterCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragEnterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragEnterCommandProperty =
            DependencyProperty.RegisterAttached("DragEnterCommand", typeof(ICommand), typeof(SliderAssist), new PropertyMetadata(null,new PropertyChangedCallback(DragEnterCommandPropertyChangedCallback)));
        static void DragEnterCommandPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tb = d as Thumb;
            if (tb != null && e.NewValue is ICommand)
            {
                tb.DragStarted
                  += (obj, args) =>
                  {
                      if ((e.NewValue as ICommand).CanExecute(obj))
                      {
                          (e.NewValue as ICommand).Execute(obj);
                      }
                  };
            }
        }

        public static ICommand GetDragLeaveCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DragLeaveCommandProperty);
        }

        public static void SetDragLeaveCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DragLeaveCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragLeaveCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragLeaveCommandProperty =
            DependencyProperty.RegisterAttached("DragLeaveCommand", typeof(ICommand), typeof(SliderAssist), new PropertyMetadata(null,new PropertyChangedCallback(DragLeaveCommandPropertyChangedCallback)));

        static void DragLeaveCommandPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tb = d as Thumb;
            if (tb != null && e.NewValue is ICommand)
            {
                tb.DragCompleted
                  += (obj, args) =>
                  {
                      if ((e.NewValue as ICommand).CanExecute(obj))
                      {
                          Slider sd = tb.VisualTreeAncestory().OfType<Slider>().FirstOrDefault();
                          if (sd == null) return;
                          (e.NewValue as ICommand).Execute(obj);
                         
                      }
                  };
            }
        }

    }
}
