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
    public class PlayListViewModel : BindableBase
    {
        public PlayListViewModel(SongSheetInfo songSheetInfo)
        {
			SongSheetInfo = songSheetInfo;
			DoubleClickCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                    CurrentMusicInfo = obj as MusicInfo;
                    Infrastructure.Manager.MediaManager.CurrentMusic = CurrentMusicInfo;
                    Infrastructure.Manager.MediaManager.SetMusicPlayList(CurrentOpenedPlayList.ToList());
					CurrentPlayListViewModel.CurrentPlayListViewContent.MusicInfos = new System.Collections.ObjectModel.ObservableCollection<MusicInfo>(Infrastructure.Manager.MediaManager.MusicPlayList ?? new List<MusicInfo>());
					Infrastructure.Manager.MediaManager.Play(CurrentMusicInfo.Songid, CurrentMusicInfo.Mp3url, SongSheetInfo.ID);
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
            GetPlayListInfo(songSheetInfo.ID,"1",songSheetInfo.Total, SongSheetInfo);
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

		private SongSheetInfo _songSheetInfo;
		public SongSheetInfo SongSheetInfo
		{
			get { return _songSheetInfo; }
			set { SetProperty(ref _songSheetInfo, value); }
		}

		private ObservableCollection<MusicInfo> _currentOpenedPlayList;
        public ObservableCollection<MusicInfo> CurrentOpenedPlayList
        {
            get { return _currentOpenedPlayList; }
            set { SetProperty(ref _currentOpenedPlayList, value); }
        }

        private MusicInfo _currentMusicInfo;

        public MusicInfo CurrentMusicInfo
        {
            get { return _currentMusicInfo; }
            set { SetProperty(ref _currentMusicInfo, value); }
        }

        public async void GetPlayListInfo(string pid, string pn, string rn, SongSheetInfo songSheetInfo)
        {
			MainWindowViewModel.ShowLoading(true);
            if (SongSheetInfo != null && Infrastructure.Manager.MediaManager.CurrentMusic != null && Infrastructure.Manager.MediaManager.MediaPlayer.CanPause)
            {
                if (SongSheetInfo.ID == Infrastructure.Manager.MediaManager.Albumid)
                {
                    CurrentOpenedPlayList = new System.Collections.ObjectModel.ObservableCollection<MusicInfo>(Infrastructure.Manager.MediaManager.MusicPlayList ?? new List<MusicInfo>());
                }
                else
                {
                    var retList = await KWMusicAPI.GetPlayListInfo(pid, pn, rn, songSheetInfo);
                    CurrentOpenedPlayList = new ObservableCollection<MusicInfo>(retList);
                }
            }
            else
            {
                var retList = await KWMusicAPI.GetPlayListInfo(pid, pn, rn, songSheetInfo);
                CurrentOpenedPlayList = new ObservableCollection<MusicInfo>(retList);
            }

            MainWindowViewModel.ShowLoading(false);
		}

        public ICommand DoubleClickCommand { get; }

        public ICommand MvSheetOpenedCommand { get; }
    }
}
