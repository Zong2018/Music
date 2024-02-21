using Music.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Music.ControlAssist
{
    public static class DataGridAssist
    {
        public static bool GetCanScroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanScrollProperty);
        }

        public static void SetCanScroll(DependencyObject obj, bool value)
        {
            obj.SetValue(CanScrollProperty, value);
        }

        // Using a DependencyProperty as the backing store for CanScroll.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanScrollProperty =
            DependencyProperty.RegisterAttached("CanScroll", typeof(bool), typeof(DataGridAssist), new PropertyMetadata(true,new PropertyChangedCallback(CanScrollPropertyChangedCallback)));

        static void CanScrollPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dg = d as DataGrid;
            if(dg != null)
            {
                if ((bool)e.NewValue)
                {
                    dg.PreviewMouseWheel -= Dg_PreviewMouseWheel;
                }
                else
                {
                    dg.PreviewMouseWheel += Dg_PreviewMouseWheel;

                }
            }
        }

        private static void Sp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private static void Dg_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            dg.RaiseEvent(eventArg);

           
        }

        public static bool GetCanClickScroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanClickScrollProperty);
        }

        public static void SetCanClickScroll(DependencyObject obj, bool value)
        {
            obj.SetValue(CanClickScrollProperty, value);
        }

        // Using a DependencyProperty as the backing store for CanClickScroll.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanClickScrollProperty =
            DependencyProperty.RegisterAttached("CanClickScroll", typeof(bool), typeof(DataGridAssist), new PropertyMetadata(true,new PropertyChangedCallback(CanClickScrollPropertyChangedCallback)));

        static void CanClickScrollPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dg = d as DataGrid;
            if (dg != null)
            {
                if ((bool)e.NewValue)
                {
                    var sp = dg.VisualTreeDepthFirstTraversal().OfType<DataGridRowsPresenter>().FirstOrDefault();
                    if (sp == null) return;
                    sp.MouseDown -= Sp_MouseDown;
                  
                    var sv = dg.VisualTreeDepthFirstTraversal().OfType<ScrollViewer>().FirstOrDefault();
                    if (sv == null) return;
                    sv.MouseDown -= Sp_MouseDown;
                }
                else
                {
                    var sp = dg.VisualTreeDepthFirstTraversal().OfType<DataGridRowsPresenter>().FirstOrDefault();
                    if (sp == null) return;
                    sp.MouseDown += Sp_MouseDown;

                    var sv = dg.VisualTreeDepthFirstTraversal().OfType<ScrollViewer>().FirstOrDefault();
                    if (sv == null) return;
                    sv.MouseDown += Sp_MouseDown;
                }
            }
        }



        public static ICommand GetDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DoubleClickCommandProperty);
        }

        public static void SetDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleClickCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for DoubleClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(DataGridAssist), new PropertyMetadata(null, new PropertyChangedCallback(DoubleClickCommandPropertyChangedCallback)));

        static void DoubleClickCommandPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dg = d as DataGrid;
            if (dg != null && e.NewValue is ICommand)
            {
                dg.MouseDoubleClick
                  += (obj, args) =>
                  {
                      Point aP = args.GetPosition(dg);
                      IInputElement element = dg.InputHitTest(aP);
                      DependencyObject target = element as DependencyObject;
                      var row = target.VisualTreeAncestory().OfType<DataGridRow>().FirstOrDefault();

					  if (row != null && (e.NewValue as ICommand).CanExecute(obj))
                      {
						  var items = dg.Items;

                          //var rows = dg.VisualTreeDepthFirstTraversal().OfType<DataGridRow>().ToList();
                          if(items.Count>= 0)
                          {
                              int index = items.IndexOf(row.Item);
                              dg.SelectedItem = dg.Items[index];
                          }
                          //dg.SelectedItem = 
                          (e.NewValue as ICommand).Execute(dg.SelectedItem);
                      }
                  };
            }
        }
    }
}
