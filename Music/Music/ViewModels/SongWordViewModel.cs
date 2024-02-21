using Music.Infrastructure.Mvvm;
using Music.Models;
using Music.MusicApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Music.ViewModels
{
    public class SongWordViewModel : BindableBase
    {
        public SongWordViewModel(MusicInfo musicInfo)
        {
        }
        public static bool HasShow { get; set; }

        public static CurrentSong Song { get; set; } = new CurrentSong();
    }

    public class CurrentSong : BindableBase
    {
        private ObservableCollection<SongWord> _songWord = new ObservableCollection<SongWord>();

        public ObservableCollection<SongWord> SongWord
        {
            get { return _songWord; }
            set { SetProperty(ref _songWord, value); }
        }

        private MusicInfo _currentMusicInfo;

        public MusicInfo CurrentMusicInfo
        {
            get { return _currentMusicInfo; }
            set
            {
                SetProperty(ref _currentMusicInfo, value);
                GetPlayListInfo(_currentMusicInfo?.Songid);
            }
        }


        private bool _IsChangedCurrentIndex;

        public bool IsChangedCurrentIndex
        {
            get { return _IsChangedCurrentIndex; }
            set
            {
                SetProperty(ref _IsChangedCurrentIndex, value);
            }
        }

        public async void GetPlayListInfo(string musicId)
        {
            //MainWindowViewModel.ShowLoading(true);
            var swList = await KWMusicAPI.GetSongWord(musicId.Replace("MUSIC_", ""));
            if (swList == null || swList.Count == 0) swList = new List<SongWord>() { new Models.SongWord() { LineLyric = "暂无歌词" } };
            for (var i = 0; i < 3; i++)
            {
                SongWord blankSongWord = new SongWord() { LineLyric = "", Time = $"0.00" };
                swList.Insert(0, blankSongWord);
            }
            var last = swList.Last();
            var lastTime = double.Parse(last.Time??"0");
            var addWordTime = (lastTime + 2).ToString("0.00");
            for (var i = 0; i <= 3; i++)
            {
                SongWord blankSongWord = new SongWord() { LineLyric = "", Time = addWordTime };
                swList.Add(blankSongWord);
            }
            var maxWidth = swList.Max(a => a.LineLyric.Length) * 14;
            int index = 0;
            swList.ForEach(a => {
                a.SongWordWidth = maxWidth;
                var time = double.Parse(a.Time??"0.0");
                if (string.IsNullOrWhiteSpace(a.LineLyric))
                {
                    a.LineLyric = "-";
                }
                if (index == swList.Count - 1)
                {
                    a.SongWordDuration = new Duration(TimeSpan.FromSeconds(1.5));
                }
                else if (index >= 3)
                {
                    a.SongWordDuration = new Duration(TimeSpan.FromSeconds(double.Parse(swList[index + 1].Time) - time));
                }
                a.MinTime = time * 1000;
                a.MaxTime = a.SongWordDuration.TimeSpan.TotalMilliseconds + a.MinTime;
                index++;
            });
            SongWord?.Clear();
            SongWord = new ObservableCollection<SongWord>(swList);
            //MainWindowViewModel.ShowLoading(false);
        }
    }
}
