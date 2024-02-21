using Music.Infrastructure.Commands;
using Music.Infrastructure.Manager;
using Music.Infrastructure.Mvvm;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Music.ViewModels
{
    public class VideoPlayerViewModel : BindableBase
    {
        public VideoPlayerViewModel()
        {
            PlayCommand = new DelegateCommand(
             (obj) => { return true; },
             (obj) => {
                 MvViewTipVisibility = "Collapsed";
                 Play(CurrentMv.ID,CurrentMv.Source);
             });

            MvDetailCommand = new DelegateCommand(
              (obj) => { return true; },
              (obj) => {
              if (State == PlayState.Play)
              {
                  VlcMediaManager.MediaPlayer.SetPause(true);
                  VlcMediaManager.MediaPlayer.ResetMedia();
              }
                  State = PlayState.Stop;
                  MvUrl = null;
                  MvViewTipVisibility = "Visible";
                  MainWindowViewModel.MainContent.MvViewVisibility = "Collapsed";
              });

            DragEnterCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                    lock (lockObject)
                    {
                        if (_timer != null)
                        {
                            _timer.Stop();
                        }
                    }
                }
            );

            DragLeaveCommand = new DelegateCommand(
               (obj) => { return true; },
               (obj) => {
                   if (State == PlayState.Stop) return;
                   lock (lockObject)
                   {
                       if (_timer != null)
                       {
                           VlcMediaManager.MediaPlayer.Position = (float)(CurrentPosition);
                           _timer?.Start();
                       }
                   }
               }
           );

            VlcMediaManager.MediaPlayer.Opening += MediaPlayer_Opening;

            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Elapsed;
            _timer.Interval = TimeSpan.FromMilliseconds(100);
        }

       public DispatcherTimer _timer;

        static readonly object lockObject = new object();
        private void MediaPlayer_Opening(object sender, Vlc.DotNet.Core.VlcMediaPlayerOpeningEventArgs e)
        {
            var task = Task.Run(()=> { 
               Thread.Sleep(1000);
            });
            task.ContinueWith((t)=> { 
               MvViewTipVisibility = "Collapsed";
               TotalSecond = 1;
            });
        }

        private void _timer_Elapsed(object sender, EventArgs e)
        {
            var seconds = VlcMediaManager.MediaPlayer.Position;
            Task.Run(() => {
                //CurrentPosition += 0.01;
                CurrentPosition = seconds;
                //if (seconds > CurrentPosition)
                //{
                //    CurrentPosition = seconds;
                //}
                if (CurrentPosition >= TotalSecond)
                {
                    lock (lockObject)
                    {
                        _timer?.Stop();
                    }
                }
            });

        }

        private double _totalSecond = 100;
        public double TotalSecond
        {
            get { return _totalSecond; }
            set
            {
                SetProperty(ref _totalSecond, value);
                CurrentPosition = 0;
                _timer?.Start();
            }
        }

        private MvSheetInfo _currentMv;
		public MvSheetInfo CurrentMv
		{
			get { return _currentMv; }
			set { SetProperty(ref _currentMv, value); }
		}

        private string _mvUrl;
        public string MvUrl
        {
            get { return _mvUrl; }
            set { SetProperty(ref _mvUrl, value); }
        }

        public async void Play(string mvId, string source, string albumid = "")
        {
            if (State == PlayState.Play)
            {
                CurrentPosition = VlcMediaManager.MediaPlayer.Position;
                VlcMediaManager.MediaPlayer.SetPause(true);
                State = PlayState.Stop;
                _timer?.Stop();
            }
            else
            {
               
                if (PlayMusicControlViewModel.CurrentMusic.State == PlayState.Play)
                {
                    PlayMusicControlViewModel.CurrentMusic.PlayCommand?.Execute(null);
                }
                if (string.IsNullOrWhiteSpace(MvUrl))
                {
                    string playUrl = await MusicApi.KWMusicAPI.GetPlayMusicSource(source);
                    MvUrl = playUrl;
                    //https://www.kuwo.cn/api/v1/www/music/playUrl?mid=143975801&type=mv&httpsStatus=1&reqId=75184e30-0068-11ee-a4e1-5d07eea72f72
                   
                    if (!string.IsNullOrWhiteSpace(MvUrl)&& !MvUrl.EndsWith(".mp4"))
                    {
                        source = "https://www.kuwo.cn/api/v1/www/music/playUrl?mid="+ mvId + "&type=mv&httpsStatus=1&reqId=75184e30-0068-11ee-a4e1-5d07eea72f72";
                        string json = await MusicApi.KWMusicAPI.GetPlayMusicSource(source);
                        var jobj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                        if (jobj.ContainsKey("data"))
                        {
                            var dataObj = jobj["data"] as Newtonsoft.Json.Linq.JObject;
                            if (dataObj != null && dataObj.ContainsKey("url"))
                            {
                                MvUrl = dataObj["url"].ToString();
                            }
                        }
                    }
                    if (MvUrl == "res not found")
                    {
                        MvUrl = "";
                        return;
                    }
                    VlcMediaManager.MediaPlayer.Play(new Uri(MvUrl, UriKind.Absolute));
                    CurrentPosition = 0;
                }
                else
                {
                    VlcMediaManager.MediaPlayer.Position = (float)CurrentPosition;
                    VlcMediaManager.MediaPlayer.Play();
                }
                State = PlayState.Play;
                if (TotalSecond > 1)
                    _timer?.Start();
            }
        }

        private string _mvViewTipVisibility = "Visible";
        public string MvViewTipVisibility
        {
            get { return _mvViewTipVisibility; }
            set { SetProperty(ref _mvViewTipVisibility, value); }
        }

        private PlayState _state = PlayState.Stop;
        public PlayState State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private double _currentPosition = 0;
        public double CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                SetProperty(ref _currentPosition, value);
            }
        }

		private double _buffingCurrentPosition = 0;
		public double BuffingCurrentPosition
		{
			get { return _buffingCurrentPosition; }
			set
			{
				SetProperty(ref _buffingCurrentPosition, value);
			}
		}

		public ICommand PlayCommand { get; }

        public ICommand MvDetailCommand { get; }

        public ICommand DragEnterCommand { get; }

        public ICommand DragLeaveCommand { get; }
    }
}
