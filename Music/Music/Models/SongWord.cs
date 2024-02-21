using Music.Infrastructure.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Music.Models
{
    /// <summary>
    /// 歌词
    /// </summary>
    public class SongWord : BindableBase
    {
        public string LineLyric{get;set;}
        public string Time { get;set; }


        private Duration _songWordDuration = new Duration(TimeSpan.FromSeconds(0));

        public Duration SongWordDuration
        {
            get { return _songWordDuration; }
            set { SetProperty(ref _songWordDuration, value); }
        }


        private bool _isPlaying = false;

        public double SongWordWidth { get; set; }

        public double Diff { get; set; }

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                SetProperty(ref _isPlaying, value);
            }
        }

        public double MinTime { get; set; }
        public double MaxTime { get; set; }
    }
}
