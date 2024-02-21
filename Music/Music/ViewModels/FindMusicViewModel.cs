using Music.Infrastructure.Commands;
using Music.Infrastructure.Manager;
using Music.Infrastructure.Mvvm;
using Music.Models;
using Music.MusicApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Music.ViewModels
{
    public class FindMusicViewModel : BindableBase
    {
        public FindMusicViewModel()
        {
            RecommendViewModel = new RecommendViewModel();
            //RecommendViewModel.ClickHandler += (s,e) => {
            //    var selectedItem = s as SongSheetInfo;
            //    if (selectedItem != null)
            //    {
            //        ContentView = Infrastructure.Manager.UserControlManager.CreateViewIntance("Music.Views.PlayListView", selectedItem);
            //    }
            //};
            SongSheetOpenedCommand = new DelegateCommand(
                (obj) => { return true; },
                async(obj) => {
                    var selectedItem = obj as SongSheetInfo;
                    if (selectedItem != null)
                    {
                       await Task.Run(()=> {
                           RecordManager.RecordOperate(RecordManager.CreateRecord("Music.Views.PlayListView", new object[] {selectedItem, true}).CreateOperateFunction());
                           ContentView =  Infrastructure.Manager.UserControlManager.CreateViewIntance("Music.Views.PlayListView", selectedItem);
                        });
                    }
                }
            );

           ScrollEndCommand = new DelegateCommand(
           (obj) => { return true; },
           (obj) => {
               _currentPag++;
               ScrollEndLoadSongSheetInfos();
           });
            //异步获取zuixin歌单
            GetSongSheetList();
            GetCustromSongSheetInfos();
        }
        int _currentPag = 1;

        public async void ScrollEndLoadSongSheetInfos()
        {
            MainWindowViewModel.ShowLoading(true);
            var retList = await KWMusicAPI.GetSongSheetList("new", _currentPag, 32);
            foreach (var item in retList)
            {
                SongSheetInfos.Add(item);
            }
            MainWindowViewModel.ShowLoading(false);
        }
        public RecommendViewModel RecommendViewModel { get; set; }

        private ObservableCollection<SongSheetInfo> _songSheetInfos;

        public ObservableCollection<SongSheetInfo> SongSheetInfos
        {
            get { return _songSheetInfos; }
            set { SetProperty(ref _songSheetInfos, value); }
        }

        public async void GetSongSheetList(string order = "new", int start = 1, int size = 32)
        {
			MainWindowViewModel.ShowLoading(true);
            var retList = await KWMusicAPI.GetSongSheetList(order,start,size);
            SongSheetInfos = new ObservableCollection<SongSheetInfo>(retList);
			MainWindowViewModel.ShowLoading(false);
		}

        public async void GetCustromSongSheetInfos(string id = "rcm", string pn = "1", string rn = "32")
        {
            var retList = await KWMusicAPI.GetRecommendPlayList(id, pn, rn);
            CustromSongSheetInfos = new ObservableCollection<SongSheetInfo>(retList);
        }

        private ObservableCollection<SongSheetInfo> _custromSongSheetInfos;

        public ObservableCollection<SongSheetInfo> CustromSongSheetInfos
        {
            get { return _custromSongSheetInfos; }
            set { SetProperty(ref _custromSongSheetInfos, value); }
        }

        private object _contentView = null;

        public object ContentView
        {
            get { return _contentView ; }
            set { 
                SetProperty(ref _contentView, value); 
                ContentVisibility = value == null ? "Collapsed" : "Visible";
                MainContentVisibility = value == null? "Visible" : "Collapsed";
            }
        }

        private string _contentVisibility = "Collapsed";

        public string ContentVisibility
        {
            get { return  _contentVisibility; }
            set { SetProperty(ref _contentVisibility, value); }
        }

        private string _mainContentVisibility = "Visible";

        public string MainContentVisibility
        {
            get { return _mainContentVisibility; }
            set { SetProperty(ref _mainContentVisibility, value); }
        }

        public ICommand SongSheetOpenedCommand { get; }

        public ICommand ScrollEndCommand { get; }
    }
}
