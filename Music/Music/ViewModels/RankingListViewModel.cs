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
    public class RankingListViewModel : BindableBase
    {
        public RankingListViewModel()
        {
            DoubleClickCommand = new DelegateCommand(
            (obj) => { return true; },
            (obj) =>
            {
                CurrentMusicInfo = obj as MusicInfo;
                Infrastructure.Manager.MediaManager.CurrentMusic = CurrentMusicInfo;
                Infrastructure.Manager.MediaManager.SetMusicPlayList(SelectedPlayList());
                CurrentPlayListViewModel.CurrentPlayListViewContent.MusicInfos = new System.Collections.ObjectModel.ObservableCollection<MusicInfo>(Infrastructure.Manager.MediaManager.MusicPlayList ?? new List<MusicInfo>());
                Infrastructure.Manager.MediaManager.Play(CurrentMusicInfo.Songid, CurrentMusicInfo.Mp3url, RankingSelectedIndex.ToString());
            });

            MvSheetOpenedCommand = new DelegateCommand(
              (obj) => { return true; },
             async (obj) =>
             {
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

            GetBsPlayListInfo();
            GetXgPlayListInfo();
            GetRgPlayListInfo();
            GetDyPlayListInfo();
            GetDJPlayListInfo();
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

        private MusicInfo _currentMusicInfo;
        public MusicInfo CurrentMusicInfo
        {
            get { return _currentMusicInfo; }
            set { SetProperty(ref _currentMusicInfo, value); }
        }

        /// <summary>
        /// 飙升榜
        /// </summary>
        private ObservableCollection<MusicInfo> _bsPlayList;
        public ObservableCollection<MusicInfo> BsPlayList
        {
            get { return _bsPlayList; }
            set { SetProperty(ref _bsPlayList, value); }
        }


        /// <summary>
        /// 新歌榜
        /// </summary>
        private ObservableCollection<MusicInfo> _xgPlayList;
        public ObservableCollection<MusicInfo> XgPlayList
        {
            get { return _xgPlayList; }
            set { SetProperty(ref _xgPlayList, value); }
        }

        /// <summary>
        /// 热歌榜
        /// </summary>
        private ObservableCollection<MusicInfo> _rgPlayList;
        public ObservableCollection<MusicInfo> RgPlayList
        {
            get { return _rgPlayList; }
            set { SetProperty(ref _rgPlayList, value); }
        }

        /// <summary>
        /// 抖音歌曲榜
        /// </summary>
        private ObservableCollection<MusicInfo> _dyPlayList;
        public ObservableCollection<MusicInfo> DyPlayList
        {
            get { return _dyPlayList; }
            set { SetProperty(ref _dyPlayList, value); }
        }

        /// <summary>
        /// DJ榜
        /// </summary>
        private ObservableCollection<MusicInfo> _djPlayList;
        public ObservableCollection<MusicInfo> DJPlayList
        {
            get { return _djPlayList; }
            set { SetProperty(ref _djPlayList, value); }
        }

        private int _selectedIndex = 0;

        public int RankingSelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); }
        }

        List<MusicInfo> SelectedPlayList()
        {
            switch (RankingSelectedIndex)
            {
                case 0:
                    return BsPlayList.ToList();
                case 1:
                    return XgPlayList.ToList();
                case 2:
                    return RgPlayList.ToList();
                case 3:
                    return DyPlayList.ToList();
                case 4:
                    return DJPlayList.ToList();
                default:
                    return BsPlayList.ToList();
            }
        }

        /// <summary>飙升榜
        /// http://www.kuwo.cn/api/www/bang/bang/musicList?bangId=93&pn=1&rn=30&httpsStatus=1&reqId=01d4dce0-c14e-11ec-bd4f-97b819cbcbb6
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="bangId"></param>
        /// <param name="reqId"></param>
        public async void GetBsPlayListInfo(string pn = "1", string rn = "30", string bangId = "93", string reqId = "01d4dce0-c14e-11ec-bd4f-97b819cbcbb6")
        {
            var retList = await KWMusicAPI.GetBangMusicList(bangId, pn, rn, reqId);
            BsPlayList = new ObservableCollection<MusicInfo>(retList);
        }
        /// <summary>新歌榜
        /// http://www.kuwo.cn/api/www/bang/bang/musicList?bangId=17&pn=1&rn=30&httpsStatus=1&reqId=e96a40a0-c14d-11ec-bd4f-97b819cbcbb6
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="bangId"></param>
        /// <param name="reqId"></param>
        public async void GetXgPlayListInfo(string pn = "1", string rn = "30", string bangId = "17", string reqId = "e96a40a0-c14d-11ec-bd4f-97b819cbcbb6")
        {
            var retList = await KWMusicAPI.GetBangMusicList(bangId, pn, rn, reqId);
            XgPlayList = new ObservableCollection<MusicInfo>(retList);
        }
        /// <summary>热歌榜
        /// http://www.kuwo.cn/api/www/bang/bang/musicList?bangId=16&pn=1&rn=30&httpsStatus=1&reqId=d3577710-c14d-11ec-bd4f-97b819cbcbb6
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="bangId"></param>
        /// <param name="reqId"></param>
        public async void GetRgPlayListInfo(string pn = "1", string rn = "30", string bangId = "16", string reqId = "d3577710-c14d-11ec-bd4f-97b819cbcbb6")
        {
            var retList = await KWMusicAPI.GetBangMusicList(bangId, pn, rn, reqId);
            RgPlayList = new ObservableCollection<MusicInfo>(retList);
        }
        /// <summary>抖音歌曲榜
        /// http://www.kuwo.cn/api/www/bang/bang/musicList?bangId=158&pn=1&rn=30&httpsStatus=1&reqId=b9c15320-c14d-11ec-bd4f-97b819cbcbb6
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="bangId"></param>
        /// <param name="reqId"></param>
        public async void GetDyPlayListInfo(string pn = "1", string rn = "30", string bangId = "158", string reqId = "b9c15320-c14d-11ec-bd4f-97b819cbcbb6")
        {
            var retList = await KWMusicAPI.GetBangMusicList(bangId, pn, rn, reqId);
            DyPlayList = new ObservableCollection<MusicInfo>(retList);
        }

        /// <summary>DJ榜
        /// http://www.kuwo.cn/api/www/bang/bang/musicList?bangId=176&pn=1&rn=30&httpsStatus=1&reqId=84cb6ed0-c14d-11ec-bd4f-97b819cbcbb6
        /// </summary>
        /// <param name="bangId"></param>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="reqId"></param>
        public async void GetDJPlayListInfo(string pn = "1", string rn = "30", string bangId = "176", string reqId = "84cb6ed0-c14d-11ec-bd4f-97b819cbcbb6")
        {
            var retList = await KWMusicAPI.GetBangMusicList(bangId, pn, rn, reqId);
            DJPlayList = new ObservableCollection<MusicInfo>(retList);
        }

        public ICommand DoubleClickCommand { get; }
        public ICommand MvSheetOpenedCommand { get; }
    }
}
