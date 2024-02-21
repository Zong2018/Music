using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Music.Infrastructure.Extensions;
using Music.ViewModels;

namespace Music.ControlAssist
{
    public static class TreeViewAssist
    {
        public static ICommand GetSelectedItemChangedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(SelectedItemChangedCommandProperty);
        }

        public static void SetSelectedItemChangedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(SelectedItemChangedCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItemChangedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemChangedCommandProperty =
            DependencyProperty.RegisterAttached("SelectedItemChangedCommand", typeof(ICommand), typeof(TreeViewAssist), new PropertyMetadata(null,new PropertyChangedCallback(PropertyChangedCallback)));

        static void  PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tr = d as TreeView;
            if (tr != null && e.NewValue is ICommand)
            {
                tr.SelectedItemChanged
                  += (obj, args) =>
                  {
                      var detail = tr.SelectedItem as MenuItemDetail;
                      if (detail != null && detail.IsSpecial) return;
                      if ((e.NewValue as ICommand).CanExecute(obj))
                      {
                          var selectedItem = tr.SelectedItem; 
                          if(selectedItem != null)
                          {
                             (e.NewValue as ICommand).Execute(selectedItem);
                          }
                      }
                  };
            }
        }



        public static object GetSelectedTreeViewItem(DependencyObject obj)
        {
            return (object)obj.GetValue(SelectedTreeViewItemProperty);
        }

        public static void SetSelectedTreeViewItem(DependencyObject obj, object value)
        {
            obj.SetValue(SelectedTreeViewItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedTreeViewItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTreeViewItemProperty =
            DependencyProperty.RegisterAttached("SelectedTreeViewItem", typeof(object), typeof(TreeViewAssist), new PropertyMetadata(null,new PropertyChangedCallback(SelectedTreeViewItemPropertyChangedCallback)));

        static void SelectedTreeViewItemPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tr = d as TreeView;
            if (tr != null && e.NewValue  != null)
            {
                tr.VisualTreeDepthFirstTraversal().OfType<TreeViewItem>().ToList().ForEach(a=> {
                    var detail = a.Header as MenuItemDetail;
                    if(detail != null)
                    {
                        if(detail.MenuWindowClassName == e.NewValue.GetType().FullName)
                        {
                            a.IsSelected = true;
                        }
                        else
                        {
                            a.IsSelected = false;
                        }
                    }
                    else
                    {
                        a.IsSelected = false;
                    }
                }); 

            }
        }

    }
}
