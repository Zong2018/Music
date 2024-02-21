using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Music.Infrastructure.Manager
{
	public static class UserControlManager
    {
		private static Hashtable _RegisterUserControl = new Hashtable();
		public static void Register<T>(string key)
		{
			if (!_RegisterUserControl.ContainsKey(key))
			{
				_RegisterUserControl.Add(key, typeof(T));
			}
		}

		public static void Register(string key, Type type)
		{
			if (!_RegisterUserControl.ContainsKey(key))
			{
				_RegisterUserControl.Add(key, type);
			}
		}

		public static void Remove(string key)
		{
			if (_RegisterUserControl.ContainsKey(key))
			{
				_RegisterUserControl.Remove(key);
			}
		}

		public static object CreateViewIntance(string key,params object[] param)
		{
			if (_RegisterUserControl.ContainsKey(key))
			{
				Type type = ((Type)_RegisterUserControl[key]);
				if (type == null)
				{
					return null;
				}
				var ct = type.GetConstructors().FirstOrDefault();
				return Application.Current.Dispatcher.Invoke<object>(() => {
					CurrentViewClassName = key;
					return ct.Invoke(param);
				},DispatcherPriority.Background);
				//return ct.Invoke(param);
			}
			return null;
		}

		/// <summary>
		/// 无参构造
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static object CreateIntanceAndNotifyUI(string key)
		{
			if (_RegisterUserControl.ContainsKey(key))
			{
				Type type = ((Type)_RegisterUserControl[key]);
				if (type == null)
				{
					ViewModels.MainWindowViewModel.MainContent.SelectedItem = null;
					return null;
				}
				var ct = type.GetConstructors().FirstOrDefault();
				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,new Action(()=> { 
					ViewModels.MainWindowViewModel.MainContent.SelectedItem = ct.Invoke(null);
					CurrentViewClassName = key;
				}));
				return ViewModels.MainWindowViewModel.MainContent.SelectedItem;
			}
			ViewModels.MainWindowViewModel.MainContent.SelectedItem = null;
			return null;
		}

		public static string CurrentViewClassName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		public static object CreateIntanceAndNotifyUI(string key,object viewModel,bool isClearSelectedMenu = false)
		{
			if (_RegisterUserControl.ContainsKey(key))
			{
				Type type = ((Type)_RegisterUserControl[key]);
				if (type == null)
				{
					ViewModels.MainWindowViewModel.MainContent.SelectedItem = null;
					return null;
				}
				var ct = type.GetConstructors().FirstOrDefault();
				if(isClearSelectedMenu)
                {
					ViewModels.MainWindowViewModel.MainContent.SelectedItem = "";
				}
				ViewModels.MainWindowViewModel.MainContent.SelectedItem = ct.Invoke(new object[] { viewModel });
				return ViewModels.MainWindowViewModel.MainContent.SelectedItem;
			}
			ViewModels.MainWindowViewModel.MainContent.SelectedItem = null;
			return null;
		}
	}
}
