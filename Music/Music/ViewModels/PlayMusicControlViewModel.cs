using Music.Infrastructure.Commands;
using Music.Infrastructure.Mvvm;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using WinFroms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using Music.Infrastructure.Manager;

namespace Music.ViewModels
{
    public class PlayMusicControlViewModel :BindableBase
    {
        public PlayMusicControlViewModel()
        {
            Mode = Infrastructure.Manager.MediaManager.Mode;
            CurrentMusic.State = PlayState.Stop;
            PlayModeCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) =>{
                    Infrastructure.Manager.MediaManager.SetPlayMode();
                    Mode = Infrastructure.Manager.MediaManager.Mode;
                });

			ViewShowCommand = new DelegateCommand(
				(obj) => { return true; },
				(obj) =>{
					CurrentPlayListViewModel.CurrentPlayListViewContent.MusicInfos = new System.Collections.ObjectModel.ObservableCollection<MusicInfo>(Infrastructure.Manager.MediaManager.MusicPlayList??new List<MusicInfo>());
	;			});

            SongWordCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) =>{
                    if (CurrentMusic.CurrentMusic == null || SongWordViewModel.HasShow) return;
                    SongWordViewModel songWordViewModel = new SongWordViewModel(CurrentMusic.CurrentMusic);
                    //RecordManager.RecordOperate(RecordManager.CreateRecord("Music.Views.SongWordView", new object[] { songWordViewModel, true }).CreateOperateFunction());
                    ViewModels.MainWindowViewModel.MainContent.SelectedItem = UserControlManager.CreateViewIntance("Music.Views.SongWordView", songWordViewModel);
                });

        }
        public static CurrentPlayMusic CurrentMusic { get; set; } = new CurrentPlayMusic();

        private PlayMode _mode;

        public PlayMode Mode
        {
            get { return _mode; }
            set { SetProperty(ref _mode, value); }
        }

        public ICommand  PlayModeCommand { get; }
		public ICommand ViewShowCommand { get; }
        public ICommand SongWordCommand { get; }

    }

    public class CurrentPlayMusic : BindableBase
    {
        public CurrentPlayMusic()
        {

            PlayCommand = new DelegateCommand(
              (obj) => { return true; },
              (obj) => {

                  if (State == PlayState.Stop)
                  {
                      Infrastructure.Manager.MediaManager.Play();
                      Infrastructure.Manager.MediaManager.MediaPlayer.Position = TimeSpan.FromSeconds(CurrentPosition);
                      if (State == PlayState.Play)
                      {
                          lock (lockObject)
                          {
                              if (_timer != null)
                              {
                                  _timer?.Start();
                                  //_time.Enabled = true;
                              }
                          }
                      }
             
                  }
                  else
                  {
                      Infrastructure.Manager.MediaManager.Stop();
                      lock (lockObject)
                      {
                          if (_timer != null)
                          {
                              _timer.Stop();
                              //_time.Enabled = false;
                          }
                      }
                  }

              }
          );

            PreCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                    Infrastructure.Manager.MediaManager.PreviousSong();
                }
            );
            
            NextCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
                    Infrastructure.Manager.MediaManager.NextSong();
                }
            );

            DragEnterCommand = new DelegateCommand(
                (obj) => { return true; },
                (obj) => {
					lock (lockObject)
					{   if(_timer != null)
						{
							_timer.Stop();
                            SongWordViewModel.Song.IsChangedCurrentIndex = false;
                            //_time.Enabled = false;
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
                           Infrastructure.Manager.MediaManager.MediaPlayer.Position = TimeSpan.FromSeconds(CurrentPosition);
                           SongWordViewModel.Song.IsChangedCurrentIndex = true;
                           _timer?.Start();
                           //_time.Enabled = true;
                       }
				   }
			   }
           );

            _timer = new DispatcherTimer();
            _timer.Tick += _time_Elapsed;
            _timer.Interval = TimeSpan.FromMilliseconds(100);

            _burreringTime = new DispatcherTimer();
            _burreringTime.Tick += _burreringTime_Tick; ;
            _burreringTime.Interval = TimeSpan.FromMilliseconds(90);
            //_time.Enabled = true;
        }

        private void _burreringTime_Tick(object sender, EventArgs e)
        {
            var buff = Infrastructure.Manager.MediaManager.GetBufferingProgress();
            _realBufferingProgress = TotalSecond * buff;
            //if (_realBufferingProgress < CurrentPosition)
            //{
            //    lock (lockObject)
            //    {
            //        State = PlayState.Play;
            //        PlayCommand.Execute(null);
            //    }
            //}
            //else if (State == PlayState.Stop)
            //{
            //    State = PlayState.Stop;
            //    //PlayCommand.Execute(null);
            //}
            Task.Run(() => {
                if (_realBufferingProgress == TotalSecond)
                {
                    if (CurrentPosition < 1)
                    {
                        var pTime = TotalSecond / 100;
                        for (int i = 0; i < 100; i++)
                        {
                            Thread.Sleep(20);
                            BufferingProgress += pTime;
                            Infrastructure.DispatcherHelper.DoEvents();
                        }
                    }
                }
            });
            if (_realBufferingProgress == TotalSecond)
            {
                if (CurrentPosition < 0.15)
                {
                    _burreringTime.Stop();
                }
            }
            else
            {
                BufferingProgress = _realBufferingProgress;
            }

        }

        private string _duration;

        public string Duration
        {
            get { return _duration; }
            set { SetProperty(ref _duration, value); }
        }

        private double _currentPosition=0;
        public double CurrentPosition
        {
            get { return _currentPosition; }
            set { SetProperty(ref _currentPosition, value);
                var ts = TimeSpan.FromSeconds(_currentPosition);
                string min = ts.Minutes.ToString();
                string sec = ts.Seconds.ToString();
                CurrentPositionString = $"{min.PadLeft(2,'0')}:{sec.PadLeft(2,'0')}";
                IsPlay = true;
            }
        }

        private string _currentPositionString;
        public string CurrentPositionString
        {
            get { return _currentPositionString; }
            set { SetProperty(ref _currentPositionString, value); }
        }

        private double _totalSecond=1;
        public double TotalSecond
        {
            get { return _totalSecond; }
            set { SetProperty(ref _totalSecond, value);
                CurrentPosition = 0;
                _timer?.Start();

                _realBufferingProgress = TotalSecond * Infrastructure.Manager.MediaManager.GetBufferingProgress();
                BufferingProgress = 0;
                _burreringTime.Start();
            }
        }

        private double _realBufferingProgress = 0;

        private double _bufferingProgress = 0;
        public double BufferingProgress
        {
            get { return _bufferingProgress; }
            set
            {
                SetProperty(ref _bufferingProgress, value);
            }
        }

        static readonly object lockObject = new object();
        private void _time_Elapsed(object sender, EventArgs e)
        {
            var seconds = Infrastructure.Manager.MediaManager.GetCurrentPosition().TotalSeconds;
            Task.Run(()=> {
                CurrentPosition += 0.1;

                if (seconds > CurrentPosition)
                {
                    CurrentPosition = seconds;
                }
                if (CurrentPosition >= TotalSecond)
                {
                    lock (lockObject)
                    {
                        _timer?.Stop();
                    }
                }
            });
           
        }
        DispatcherTimer _timer;

        DispatcherTimer _burreringTime;

        private PlayState _state;

        public PlayState State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); IsPlay = _state == PlayState.Play ? true : false; }
        }

        private MusicInfo _currentMusic;
        public MusicInfo CurrentMusic
        {
            get { return _currentMusic; }
            set { SetProperty(ref _currentMusic, value); }
        }

        public ICommand DragEnterCommand { get;}

        public ICommand DragLeaveCommand { get; }

        public ICommand PlayCommand { get; }

        public ICommand PreCommand { get; }
        public ICommand NextCommand { get; }

        private bool _isPlay = false;
        public bool IsPlay
        {
            get { return _isPlay; }
            set { SetProperty(ref _isPlay, value); }
        }

    }
}
