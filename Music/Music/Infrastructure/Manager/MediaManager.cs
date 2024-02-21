using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Music.Infrastructure.Manager
{
    public class MediaManager
    {
        static readonly object _lockObject = new object();

        static MediaElement _mediaPlayer = null;
        public static MediaElement MediaPlayer
        {
            get
            {
                if (_mediaPlayer == null)  //先判断实例是否存在，不存在再加锁处理
                {
                    lock (_lockObject)
                    {
                        _mediaPlayer = new MediaElement();
                        _mediaPlayer.LoadedBehavior = MediaState.Manual;
						_mediaPlayer.Volume = 0.2;
						_mediaPlayer.MediaOpened += _mediaPlayer_MediaOpened;
                        _mediaPlayer.MediaEnded += _mediaPlayer_MediaEnded;
                    }
                }
                return _mediaPlayer;
            }
        }

        public static MusicInfo CurrentMusic { get; set; }

        private static void _mediaPlayer_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            Stop();
            MediaPlayer.Position = TimeSpan.Zero;
            switch (Mode)
            {
                case PlayMode.Order:
                    OrderPlay();
                    break;
                case PlayMode.Loop:
                    LoopPlay();
                    break;
                case PlayMode.SingleLoop:
                    SingleLoopPlay();
                    break;
                case PlayMode.Random:
                    RandomPlay();
                    break;
            }
        }
        
        public static void SetPlayMode()
        {
            int index = 0;
            int currentIndex = PlayModes.ToList().IndexOf(Mode);
            if(currentIndex == PlayModes.Count() - 1)
            {
                index = 0;
            }
            else
            {
                index = currentIndex + 1;
            }
            Mode = PlayModes[index];
        }

        static PlayMode[] PlayModes = new PlayMode[4] {PlayMode.Order,PlayMode.Loop,PlayMode.SingleLoop,PlayMode.Random };

        static void OrderPlay()
        {
            NextSong();
        }

        static void LoopPlay()
        {
            NextSong(true);
        }

        static void SingleLoopPlay()
        {
            MediaPlayer.Position =TimeSpan.FromSeconds(0);
            ViewModels.PlayMusicControlViewModel.CurrentMusic.TotalSecond = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            Play();
            ViewModels.SongWordViewModel.Song.CurrentMusicInfo = CurrentMusic;
        }

        static void RandomPlay()
        {
            Random rd = new Random();
            var current = MusicPlayList.Find(a=>a.Songid == SongId);
            int needPlayIndex = 0;
            if (MusicPlayList == null || MusicPlayList.Count == 0) return;
            if(current == null)
            {
                needPlayIndex = rd.Next(0, MusicPlayList.Count - 1);
                CurrentMusic = MusicPlayList[needPlayIndex];
                Play(MusicPlayList[needPlayIndex].Songid, MusicPlayList[needPlayIndex].Mp3url);
            }
            else
            {
                if (MusicPlayList.Count == 1) return;
                needPlayIndex = rd.Next(0, MusicPlayList.Count - 2);
                var playList = MusicPlayList.Where(a => a.Songid != SongId).ToList();
                CurrentMusic = playList[needPlayIndex];
                Play(playList[needPlayIndex].Songid, playList[needPlayIndex].Mp3url);
            }
        }

        static string SongId { get; set; }
        public static PlayMode Mode { get; private set; } = PlayMode.Order;

		public static List<MusicInfo> MusicPlayList { get; set; }

		public static string Albumid { get; private set; }


		public static void SetMusicPlayList(List<MusicInfo> musicPlayList)
        {
            MusicPlayList = musicPlayList;
        }

        private static void _mediaPlayer_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModels.SongWordViewModel.Song.SongWord?.Clear();
            string min = MediaPlayer.NaturalDuration.TimeSpan.Minutes.ToString();
            string sec = MediaPlayer.NaturalDuration.TimeSpan.Seconds.ToString();
            ViewModels.PlayMusicControlViewModel.CurrentMusic.Duration = $"{min.PadLeft(2,'0')}:{sec.PadLeft(2, '0')}";
            ViewModels.PlayMusicControlViewModel.CurrentMusic.TotalSecond = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            ViewModels.PlayMusicControlViewModel.CurrentMusic.CurrentMusic = CurrentMusic;
            ViewModels.SongWordViewModel.Song.CurrentMusicInfo = CurrentMusic;
        }

        public static TimeSpan GetCurrentPosition()
        {
            return MediaPlayer.Position;
        }

        public static double GetBufferingProgress()
        {
            return MediaPlayer.BufferingProgress;
        }

        public async static void Play(string songId,string source, string albumid = "")
        {
            string playUrl = await MusicApi.KWMusicAPI.GetPlayMusicSource(source);
            if (string.IsNullOrWhiteSpace(playUrl)) return;
            MusicPlayList?.ForEach(a=> { 
                
                if (a.Songid == songId) 
                {
                    a.IsPlaying = true;
                }
                else
                {
                    a.IsPlaying = false;
                }
            
            });
			Albumid = albumid;
			SongId = songId;
            MediaPlayer.Source = new Uri(playUrl);
            MediaPlayer.Play();
            ViewModels.PlayMusicControlViewModel.CurrentMusic.State = PlayState.Play;
        }

        public static void Play()
        {
            if (CurrentMusic == null) return;
            MediaPlayer.Play();
            ViewModels.PlayMusicControlViewModel.CurrentMusic.State = PlayState.Play;
        }

        public static void Stop()
        {
            if(MediaPlayer.Source != null && !string.IsNullOrWhiteSpace(MediaPlayer.Source.ToString()))
            {
                MediaPlayer.Stop();
                ViewModels.PlayMusicControlViewModel.CurrentMusic.State = PlayState.Stop;
            }
        }

        public static void NextSong(bool canLoop = false)
        {
            if (MusicPlayList != null && MusicPlayList.Count > 0)
            {
                var current = MusicPlayList.Find(a => a.Songid == SongId);
                if (current == null) return;
                int currentIndex = MusicPlayList.IndexOf(current);
                if (!canLoop)
                {
                    if (currentIndex == MusicPlayList.Count - 1) return;
                    var needPlayItem = MusicPlayList[currentIndex + 1];
                    CurrentMusic = needPlayItem;
                    Play(needPlayItem.Songid, needPlayItem.Mp3url);
                }
                else
                {
                    if (currentIndex == MusicPlayList.Count - 1)
                    {
                        var needPlayItem = MusicPlayList.First();
                        CurrentMusic = needPlayItem;
                        Play(needPlayItem.Songid, needPlayItem.Mp3url);
                    }
                    else
                    {
                        var needPlayItem = MusicPlayList[currentIndex + 1];
                        CurrentMusic = needPlayItem;
                        Play(needPlayItem.Songid, needPlayItem.Mp3url);
                    }
                }
            }
        }

        public static void PreviousSong(bool canLoop = false)
        {
            if (MusicPlayList != null && MusicPlayList.Count > 0)
            {
                var current = MusicPlayList.Find(a => a.Songid == SongId);
                if (current == null) return;
                int currentIndex = MusicPlayList.IndexOf(current);
                
                if(!canLoop)
                {
                    if (currentIndex == 0) return;
                    var needPlayItem = MusicPlayList[currentIndex - 1];
                    CurrentMusic = needPlayItem;
                    Play(needPlayItem.Songid, needPlayItem.Mp3url);
                }
                else
                {
                    if (currentIndex == 0)
                    {
                        var needPlayItem = MusicPlayList.Last();
                        CurrentMusic = needPlayItem;
                        Play(needPlayItem.Songid, needPlayItem.Mp3url);
                    }
                    else
                    {
                        var needPlayItem = MusicPlayList[currentIndex - 1];
                        CurrentMusic = needPlayItem;
                        Play(needPlayItem.Songid, needPlayItem.Mp3url);
                    }
                    
                }
                
            
            }
        }
    }
}
