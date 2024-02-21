using Music.Infrastructure.Commands;
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
    public class SearchResultViewModel : BindableBase
    {
        public SearchResultViewModel()
        {
            DoubleClickCommand = new DelegateCommand(
    (obj) => { return true; },
    (obj) => {
        CurrentMusicInfo = obj as MusicInfo;
        Infrastructure.Manager.MediaManager.CurrentMusic = CurrentMusicInfo;
        Infrastructure.Manager.MediaManager.SetMusicPlayList(ResultMusicList.ToList());
        CurrentPlayListViewModel.CurrentPlayListViewContent.MusicInfos = new System.Collections.ObjectModel.ObservableCollection<MusicInfo>(Infrastructure.Manager.MediaManager.MusicPlayList ?? new List<MusicInfo>());
        Infrastructure.Manager.MediaManager.Play(CurrentMusicInfo.Songid, CurrentMusicInfo.Mp3url, "search");
    }
);

            MvSheetOpenedCommand = new DelegateCommand(
              (obj) => { return true; },
             async (obj) => {
                 var selectedItem = obj as MusicInfo;
                 if (selectedItem != null)
                 {
                     MvSheetInfo musicInfo = new MvSheetInfo();
                     musicInfo.ID = selectedItem.Mvid;
                     musicInfo.Name = selectedItem.Songname;
                     musicInfo.Uname = selectedItem.Singer;
                     musicInfo.UnameId = selectedItem.Singerid;
                     musicInfo.Duration = selectedItem.Duration;
                     musicInfo.Pic = selectedItem.PicPath;
                     musicInfo.Listen = "";
                     musicInfo.Source = selectedItem.Mvurl;
                     musicInfo.From = "酷我音乐";
                     var flag = await CheckCanPlayMv(musicInfo.Source);
                     if (flag)
                     {
                         MainWindowViewModel.MainContent.CurrentVideoPlayerViewModel.CurrentMv = musicInfo;
                         MainWindowViewModel.MainContent.MvViewVisibility = "Visible";
                         MainWindowViewModel.MainContent.CurrentVideoPlayerViewModel.Play(musicInfo.ID, musicInfo.Source);
                     }
                 }
             }
         );

            GetMusicByPageAction += GetMusic;
        }

        async void GetMusic()
        {
            MainWindowViewModel.ShowLoading(true);
            ++currentPage;
            var resultList = await KWMusicAPI.SearchSongList(ToolBarViewModel.SearchKeyText, currentPage, 30);
            foreach (var item in resultList)
            {
                ResultMusicList.Add(item);
            }
            Infrastructure.Manager.MediaManager.SetMusicPlayList(ResultMusicList.ToList());
            CurrentPlayListViewModel.CurrentPlayListViewContent.MusicInfos = new System.Collections.ObjectModel.ObservableCollection<MusicInfo>(Infrastructure.Manager.MediaManager.MusicPlayList ?? new List<MusicInfo>());
            MainWindowViewModel.ShowLoading(false);

        }

        async Task<bool> CheckCanPlayMv(string source)
        {
            string playUrl = await MusicApi.KWMusicAPI.GetPlayMusicSource(source);
            if (playUrl == "res not found")
            {
                return false;
            }
            return true;
          
        }

        public Action GetMusicByPageAction;

        int currentPage = 1;

        private MusicInfo _currentMusicInfo;

        public MusicInfo CurrentMusicInfo
        {
            get { return _currentMusicInfo; }
            set { SetProperty(ref _currentMusicInfo, value); }
        }

        private ObservableCollection<MusicInfo> _resultMusicList;
        public ObservableCollection<MusicInfo> ResultMusicList
        {
            get { return _resultMusicList; }
            set { SetProperty(ref _resultMusicList, value); }
        }

        public ICommand DoubleClickCommand { get; }

        public ICommand MvSheetOpenedCommand { get; }
    }
}
