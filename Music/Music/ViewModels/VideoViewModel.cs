using Music.Infrastructure.Commands;
using Music.Infrastructure.Mvvm;
using Music.Models;
using Music.MusicApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Music.ViewModels
{
    public class VideoViewModel: BindableBase
    {
        public VideoViewModel()
        {
            ScrollEndCommand = new DelegateCommand(
               (obj) => { return true; },
               (obj) => {
                   _currentPag++;
                   ScrollEndLoadMvSheetList();
               }
           );
            MvSheetOpenedCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                    var selectedItem = obj as MvSheetInfo;
                    if (selectedItem != null)
                    {
                        //VideoPlayerViewModel videoPlayerViewModel = new VideoPlayerViewModel() { CurrentMv = selectedItem};
                        MainWindowViewModel.MainContent.CurrentVideoPlayerViewModel.CurrentMv = selectedItem;
                        MainWindowViewModel.MainContent.MvViewVisibility = "Visible";
                        MainWindowViewModel.MainContent.CurrentVideoPlayerViewModel.Play(selectedItem.ID, selectedItem.Source);
                    }
                }
           );

           MvDetailCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                    MainWindowViewModel.MainContent.MvViewVisibility = "Collapsed";
                }
           );
           // AllMvSheetInfos = new List<MvSheetInfo>();
            GetMvSheetList(_currentPag.ToString(), "61");
            MvSheetInfos = new ObservableCollection<MvSheetInfo>();
            ChineseMvSheetInfos = new ObservableCollection<MvSheetInfo>();
            RhMvSheetInfos = new ObservableCollection<MvSheetInfo>();
            NetMvSheetInfos = new ObservableCollection<MvSheetInfo>();
        }

        private ObservableCollection<MvSheetInfo> _mvSheetInfos;

        public ObservableCollection<MvSheetInfo> MvSheetInfos
        {
            get { return _mvSheetInfos; }
            set { SetProperty(ref _mvSheetInfos, value); }
        }

        private int _currentViewIndex=0;

        public int CurrentViewIndex
        {
            get { return _currentViewIndex; }
            set { SetProperty(ref _currentViewIndex, value); _currentPag = 1; ScrollEndLoadMvSheetList(); }
        }

        /// <summary>
        /// 华语
        /// </summary>
        private ObservableCollection<MvSheetInfo> _chineseMvSheetInfos;

        public ObservableCollection<MvSheetInfo> ChineseMvSheetInfos
        {
            get { return _chineseMvSheetInfos; }
            set { SetProperty(ref _chineseMvSheetInfos, value); }
        }

        /// <summary>
        /// 日韩
        /// </summary>
        private ObservableCollection<MvSheetInfo> _rhMvSheetInfos;

        public ObservableCollection<MvSheetInfo> RhMvSheetInfos
        {
            get { return _rhMvSheetInfos; }
            set { SetProperty(ref _rhMvSheetInfos, value); }
        }

        /// <summary>
        /// 日韩
        /// </summary>
        private ObservableCollection<MvSheetInfo> _netMvSheetInfos;

        public ObservableCollection<MvSheetInfo> NetMvSheetInfos
        {
            get { return _netMvSheetInfos; }
            set { SetProperty(ref _netMvSheetInfos, value); }
        }

        private  List<MvSheetInfo> AllMvSheetInfos { get; set; }

        private Dictionary<string, string> _mvTypeDic = new Dictionary<string, string>() { 
            { "236682871", "c828b740-bb1d-11ec-b19a-0199d95b6f89" },//推荐
            { "236682731", "b86724e0-bc6c-11ec-b949-7b65fe418cca" },//华语
            { "236742444", "3f0ff3f0-bc6d-11ec-b949-7b65fe418cca" },//日韩
            { "236682773", "5bbfb3f0-bc6d-11ec-b949-7b65fe418cca" },//网络
            { "236682735", "74954700-bc6d-11ec-b949-7b65fe418cca" },//欧美
            { "236742576", "8907a700-bc6d-11ec-b949-7b65fe418cca" },//现场
            { "236682777", "a99e3e20-bc6d-11ec-b949-7b65fe418cca" },//热舞
            { "236742508", "bc21ed80-bc6d-11ec-b949-7b65fe418cca" },//伤感
            { "236742578", "ce171880-bc6d-11ec-b949-7b65fe418cca" },//剧情
        };
        int _currentPag = 1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="type"></param>
        public async void GetMvSheetList(string pn = "1", string rn = "33", string type= "236682871")
        {
            MainWindowViewModel.ShowLoading(true);

            List<MvSheetInfo> retList = null;
            await Task.Run(()=> {
                for (int i = 0; i < 3; i++)
                {
                    if (retList == null)
                    {
                        var task =  KWMusicAPI.GetMvListInfo(pn, rn, type, _mvTypeDic[type]);
                        retList = task.Result;
                        if(retList == null)
                        {
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            });

            if (retList != null)
            {
                switch (CurrentViewIndex)
                {
                    case 0:
                        foreach (var item in retList)
                        {
                            MvSheetInfos.Add(item);
                        }
                        break;
                    case 1:
                        foreach (var item in retList)
                        {
                            ChineseMvSheetInfos.Add(item);
                        }
                        break;
                    case 2:
                        foreach (var item in retList)
                        {
                            RhMvSheetInfos.Add(item);
                        }
                        break;
                    case 3:
                        foreach (var item in retList)
                        {
                            NetMvSheetInfos.Add(item);
                        }
                        break;
                    case 4:
                        foreach (var item in retList)
                        {
                            MvSheetInfos.Add(item);
                        }
                        break;
                    case 5:
                        foreach (var item in retList)
                        {
                            MvSheetInfos.Add(item);
                        }
                        break;
                    case 6:
                        foreach (var item in retList)
                        {
                            MvSheetInfos.Add(item);
                        }
                        break;
                    case 7:
                        foreach (var item in retList)
                        {
                            MvSheetInfos.Add(item);
                        }
                        break;
                    case 8:
                        foreach (var item in retList)
                        {
                            MvSheetInfos.Add(item);
                        }
                        break;
                    default:
                        break;
                }
               
                //AllMvSheetInfos.AddRange(retList);
            }
           
            MainWindowViewModel.ShowLoading(false);
        }


        public void ScrollEndLoadMvSheetList()
        {
            if(_currentPag == 1)
            {
                MvSheetInfos = new ObservableCollection<MvSheetInfo>();
                ChineseMvSheetInfos = new ObservableCollection<MvSheetInfo>();
                RhMvSheetInfos = new ObservableCollection<MvSheetInfo>();
                NetMvSheetInfos = new ObservableCollection<MvSheetInfo>();
            }
            switch (CurrentViewIndex)
            {
                case 0:
                    GetMvSheetList(_currentPag.ToString(), "61");
                    break;
                case 1:
                    GetMvSheetList(_currentPag.ToString(), "61", "236682731");
                    break;
                case 2:
                    GetMvSheetList(_currentPag.ToString(), "61", "236742444");
                    break;
                case 3:
                    GetMvSheetList(_currentPag.ToString(), "61", "236682773");
                    break;
                case 4:
                    GetMvSheetList(_currentPag.ToString(), "61", "236682735");
                    break;
                case 5:
                    GetMvSheetList(_currentPag.ToString(), "61", "236742576");
                    break;
                case 6:
                    GetMvSheetList(_currentPag.ToString(), "61", "236682777");
                    break;
                case 7:
                    GetMvSheetList(_currentPag.ToString(), "61", "236742508");
                    break;
                case 8:
                    GetMvSheetList(_currentPag.ToString(), "61", "236742578");
                    break;
                default:
                    break;
            }
        }

        public ICommand ScrollEndCommand {get;}

        public ICommand MvSheetOpenedCommand { get; }

        public ICommand MvDetailCommand { get; }
    }
}
