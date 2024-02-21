using Music.Infrastructure.Commands;
using Music.Infrastructure.Manager;
using Music.Infrastructure.Mvvm;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Music.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(ToolBarViewModel toolBarViewModel)
        {
            ToolBarViewModel = toolBarViewModel;
            MenuSelectedItemChangedCommand = new DelegateCommand(
                (obj) => { return true; },
                async (obj) =>
                {
                    var selectedItem = obj as MenuItemDetail;
                    if (selectedItem != null)
                    {
                        ShowLoading(true);
                        await Task.Run(() =>
                        {
                            if (MainContent.SelectedItem != null && selectedItem.MenuWindowClassName == MainContent.SelectedItem.GetType().FullName)
                            {
                                return;
                            }
                            RecordManager.RecordOperate(RecordManager.CreateRecord(selectedItem.MenuWindowClassName).CreateOperateAction());
                            Infrastructure.Manager.UserControlManager.CreateIntanceAndNotifyUI(selectedItem.MenuWindowClassName);
                        });
                        ShowLoading(false);
                    }
                }
            );

        }
        public ToolBarViewModel ToolBarViewModel { get; set; }

        public ObservableCollection<MenuItemDetail> MenuItems { get; set; }

        public static MainContent MainContent { get; set; } = new MainContent() { CurrentVideoPlayerViewModel = new VideoPlayerViewModel() };

        public ICommand MenuSelectedItemChangedCommand { get; }

        public static void ShowLoading(bool stateflag)
        {
            if (stateflag)
            {
                MainContent.LoadingVisibility = "Visible";
                MainContent.LoadingIsStart = true;
            }
            else
            {
                DispatcherTimer _time = new DispatcherTimer();
                _time.Tick += _time_Tick; ;
                _time.Interval = TimeSpan.FromMilliseconds(1000);
                _time.Start();
            }
        }
        private static void _time_Tick(object sender, EventArgs e)
        {
            var timer = sender as DispatcherTimer;
            MainContent.LoadingVisibility = "Collapsed";
            MainContent.LoadingIsStart = false;
            timer.Stop();
        }
    }

    public class MenuItemDetail : BindableBase
    {
        private string _MenuItemName;

        public string MenuItemName
        {
            get { return _MenuItemName; }
            set { SetProperty(ref _MenuItemName, value); }
        }

        private string _MenuWindowClassName;
        public string MenuWindowClassName
        {
            get { return _MenuWindowClassName; }
            set { SetProperty(ref _MenuWindowClassName, value); }
        }

        private bool _isSpecial;

        public bool IsSpecial
        {
            get { return _isSpecial; }
            set { SetProperty(ref _isSpecial, value); }
        }

        public ObservableCollection<MenuItemDetail> Childrens { get; set; }
    }

    public class MainContent : BindableBase
    {
        private object _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                if (value.GetType().FullName == "Music.Views.SongWordView")
                {
                    SongWordViewModel.HasShow = true;
                }
                else
                {
                    SongWordViewModel.HasShow = false;

                }
            }
        }

        private string _mvViewVisibility = "Collapsed";
        public string MvViewVisibility
        {
            get { return _mvViewVisibility; }
            set { SetProperty(ref _mvViewVisibility, value); }
        }

        private VideoPlayerViewModel _currentVideoPlayerViewModel;
        public VideoPlayerViewModel CurrentVideoPlayerViewModel
        {
            get { return _currentVideoPlayerViewModel; }
            set { SetProperty(ref _currentVideoPlayerViewModel, value); }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); }
        }

        private string _currentPlayListVisibility = "Collapsed";

        public string CurrentPlayListVisibility
        {
            get { return _currentPlayListVisibility; }
            set { SetProperty(ref _currentPlayListVisibility, value); }
        }

        private string _loadingVisibility = "Collapsed";

        public string LoadingVisibility
        {
            get { return _loadingVisibility; }
            set { SetProperty(ref _loadingVisibility, value); }
        }

        private bool _loadingIsStart = false;

        public bool LoadingIsStart
        {
            get { return _loadingIsStart; }
            set { SetProperty(ref _loadingIsStart, value); }
        }

        private double _maxHeight;
        public double MaxHeight
        {
            get { return _maxHeight; }
            set { SetProperty(ref _maxHeight, value); }
        }
    }
}
