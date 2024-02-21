using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Music.Infrastructure.Manager
{
    public static class WindowManager
    {
        private static Hashtable _RegisterWindow = new Hashtable();

        public static void Register<T>(string key)
        {
            if (!_RegisterWindow.ContainsKey(key))
            {
                _RegisterWindow.Add(key,typeof(T));
            }
        }

        public static void Remove(string key)
        {
            if (_RegisterWindow.ContainsKey(key))
            {
                _RegisterWindow.Remove(key);
            }
        }

        public static void Show(string key,object viewModel)
        {
            if (_RegisterWindow.ContainsKey(key))
            {
                var win = (Window)Activator.CreateInstance((Type)_RegisterWindow[key]);
                if(win != null)
                {
                    win.DataContext = viewModel;
                    win.Show();
                }
            }
        }

        public static void ShowDialog(string key, object viewModel)
        {
            if (_RegisterWindow.ContainsKey(key))
            {
                var win = (Window)Activator.CreateInstance((Type)_RegisterWindow[key]);
                if (win != null)
                {
                    win.DataContext = viewModel;
                    win.ShowDialog();
                }
            }
        }
    }
}
