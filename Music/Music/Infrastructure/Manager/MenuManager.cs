using Music.Infrastructure.Configuration;
using Music.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Music.Infrastructure.Manager
{
    public static class MenuManager
    {
		public const string MenuFileName = "MenuConfig.xml";
		public static string FullMenuFilePath = "";
		static MenuManager()
		{
			string exeFullName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
			var path = Path.GetDirectoryName(exeFullName);
			FullMenuFilePath = Path.Combine(path,MenuFileName);
			
		}

		public static List<MenuItemDetail> GetMenuTree()
		{
			List<MenuItemDetail> menuItems = new List<MenuItemDetail>();
		    XmlHelper xmlHelper = new XmlHelper();
			var nodesList = xmlHelper.GetXmlNodeListByXpath(FullMenuFilePath, "//Menu//MenuItem");
			foreach (XmlNode p in nodesList)
			{
				if (p.ParentNode == null || p.ParentNode.Name != "Menu") continue;
				MenuItemDetail menuItem = new MenuItemDetail();
				menuItem.MenuItemName = p.Attributes["Name"].Value;
				menuItem.MenuWindowClassName = p.Attributes["ViewClassName"].Value;
				menuItem.IsSpecial = Convert.ToBoolean(p.Attributes["IsSpecial"].Value);
				menuItem.Childrens = new ObservableCollection<MenuItemDetail>();
				GetChildren(p.ChildNodes, menuItem.Childrens);
				menuItems.Add(menuItem);
				Type type = GetTypeByClassName(menuItem.MenuWindowClassName);
				UserControlManager.Register(menuItem.MenuWindowClassName,type);
			}

			Type type1 = GetTypeByClassName("Music.Views.SettingView");
			UserControlManager.Register("Music.Views.SettingView", type1);

			Type type2 = GetTypeByClassName("Music.Views.PlayListView");
			UserControlManager.Register("Music.Views.PlayListView", type2);


			Type type3 = GetTypeByClassName("Music.Views.VideoPlayerView");
			UserControlManager.Register("Music.Views.VideoPlayerView", type3);

			Type type4 = GetTypeByClassName("Music.Views.SearchResultView");
			UserControlManager.Register("Music.Views.SearchResultView", type4);

			Type type5 = GetTypeByClassName("Music.Views.SongWordView");
			UserControlManager.Register("Music.Views.SongWordView", type5);

			return menuItems;
		}

		static Type GetTypeByClassName(string typeName)
		{
			Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
			int assemblyArrayLength = assemblyArray.Length;
			Type type = null;
			for (int i = 0; i < assemblyArrayLength; ++i)
			{
				type = assemblyArray[i].GetType(typeName);
				if (type != null)
				{
					return type;
				}
			}
			return type;
		}

		static void GetChildren(XmlNodeList childNodes, ObservableCollection<MenuItemDetail> menuItemDetails)
		{
			if (childNodes == null || childNodes.Count == 0) return;
			foreach (XmlNode p in childNodes)
			{
				MenuItemDetail menuItem = new MenuItemDetail();
				menuItem.MenuItemName = p.Attributes["Name"].Value;
				menuItem.MenuWindowClassName = p.Attributes["ViewClassName"].Value;
				menuItem.IsSpecial = Convert.ToBoolean(p.Attributes["IsSpecial"].Value);
				menuItem.Childrens = new ObservableCollection<MenuItemDetail>();
				GetChildren(p.ChildNodes, menuItem.Childrens);
				menuItemDetails.Add(menuItem);
				Type type = GetTypeByClassName(menuItem.MenuWindowClassName);
				UserControlManager.Register(menuItem.MenuWindowClassName, type);
			}
		}
	}
}
