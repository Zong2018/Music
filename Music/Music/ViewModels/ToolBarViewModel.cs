using Music.Extensions;
using Music.Infrastructure.Commands;
using Music.Infrastructure.Manager;
using Music.Infrastructure.Mvvm;
using Music.MusicApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Music.ViewModels
{
    public class ToolBarViewModel: BindableBase
    {

        public ToolBarViewModel()
        {
            ChangedThemeCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                  
					var param = obj as object[];
					if(param != null)
					{
                        var paletteHelper = PaletteHelper.Instance;
                        var theme = paletteHelper.GetTheme();
						theme.Background = (param[1] as SolidColorBrush)?.Color ?? Color.FromRgb(220, 20, 60);
						paletteHelper.SetTheme(theme);
						string mark = param[0].ToString();
						if (!string.IsNullOrWhiteSpace(mark))
						{
							if(mark == "主题")
							{
								ThemeColorPaletteIsClearChecked = false;
								SampleColorPaletteIsClearChecked = false;
								SampleColorPaletteIsClearChecked = true;
							}
							else
							{
								SampleColorPaletteIsClearChecked = false;
								ThemeColorPaletteIsClearChecked = false;
								ThemeColorPaletteIsClearChecked = true;
							}
						}

                        //(Color)ColorConverter.ConvertFromString("#FF333333");
                        var hexString = (param[1] as SolidColorBrush).ToString();
                        SettingManager.GlobalSetting.ThemeType = mark;
                        SettingManager.GlobalSetting.ThemeColor = hexString;
                        SettingManager.SaveSetting();
                    }

                }
            );

            SearchCommand = new DelegateCommand(
                (obj) => {
                    return true;
                },
                (obj) => {
                    if (!CanSearch || obj == null) return;
                    ToolBarViewModel.SearchKeyText = obj.ToString();
                    // KWMusicAPI.SearchSongList("周杰伦");
                    //ViewModels.MainWindowViewModel.MainContent.SelectedItem;
                    GetAndShowSerachResult(obj.ToString());
                }
            );

            ClosedCommand = new DelegateCommand(
                (obj)=> { return true; },
                (obj) => {
                    IsClosed = true;
                }
            );

            MinCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                    ToolBarWindowState = WindowState.Minimized;
                }
            );

            MaxCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                   if(ToolBarWindowState == WindowState.Maximized)
                    {
                        WindowMargin = new Thickness(32);
                        ToolBarWindowState = WindowState.Normal;
                    }
                    else
                    {
                        WindowMargin = new Thickness(0);
                        ToolBarWindowState = WindowState.Maximized;
                    }
                    //MainWindowViewModel.MainContent.MaxHeight = Application.Current.MainWindow.ActualHeight;
                }
            );

		SettingCommand = new DelegateCommand(
				(obj) => { return true; },
			    async (obj) => {
                        await Task.Run(()=> {
                            RecordManager.RecordOperate(RecordManager.CreateRecord("Music.Views.SettingView").CreateOperateAction());
                            var uc = UserControlManager.CreateIntanceAndNotifyUI("Music.Views.SettingView");
                        });
				}
			);

            ForwardCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
               RecordManager.Forward();
            });

            BackCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                RecordManager.Back();
            });
        }
        public static string SearchKeyText { get; set; }

        public ICommand ChangedThemeCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ClosedCommand { get; }

        public ICommand MinCommand { get; }

        public ICommand MaxCommand { get; }

		public ICommand SettingCommand { get; }

        public ICommand ForwardCommand { get; }

        public ICommand BackCommand { get; }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private bool _canSearch;
        public bool CanSearch
        {
            get { return _canSearch; }
            set { SetProperty(ref _canSearch, value); }
        }


        private bool _IsClosed;

        public bool IsClosed
        {
            get { return _IsClosed; }
            set { SetProperty(ref _IsClosed, value); }
        }

		private bool _themeColorPaletteIsClearChecked;

		public bool ThemeColorPaletteIsClearChecked
		{
			get { return _themeColorPaletteIsClearChecked; }
			set { SetProperty(ref _themeColorPaletteIsClearChecked, value); }
		}

		private bool _sampleColorPaletteIsClearChecked;

		public bool SampleColorPaletteIsClearChecked
		{
			get { return _sampleColorPaletteIsClearChecked; }
			set { SetProperty(ref _sampleColorPaletteIsClearChecked, value); }
		}

		private WindowState _toolBarWindowState;

        public WindowState ToolBarWindowState
        {
            get { return _toolBarWindowState; }
            set { SetProperty(ref _toolBarWindowState, value); }
        }

        private Thickness _WindowMargin= new Thickness(32);

        public Thickness WindowMargin
        {
            get { return _WindowMargin; }
            set { SetProperty(ref _WindowMargin, value); }
        }


        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                SetProperty(ref _value, value);
            }
        }

        public static RecordButtonViewModel RecordButtonViewModel { get; set; } = new RecordButtonViewModel();


        public async void GetAndShowSerachResult(string searchText,int pn = 1,int count = 30)
        {
            MainWindowViewModel.ShowLoading(true);
            var resultList = await KWMusicAPI.SearchSongList(searchText, pn, count);
            SearchResultViewModel searchResultViewModel = new SearchResultViewModel() {ResultMusicList = new System.Collections.ObjectModel.ObservableCollection<Models.MusicInfo>(resultList) };

            RecordManager.RecordOperate(RecordManager.CreateRecord("Music.Views.SearchResultView", new object[] {searchResultViewModel,true}).CreateOperateFunction());
            ViewModels.MainWindowViewModel.MainContent.SelectedItem = UserControlManager.CreateViewIntance("Music.Views.SearchResultView", searchResultViewModel);
            MainWindowViewModel.ShowLoading(false);
        }
    }

    public class RecordButtonViewModel: BindableBase
    {
        private bool _canForward;

        public bool CanForward
        {
            get { return _canForward; }
            set { SetProperty(ref _canForward, value);}
        }

        private bool _canBack;

        public bool CanBack
        {
            get { return _canBack; }
            set { SetProperty(ref _canBack, value); }
        }
    }
}
