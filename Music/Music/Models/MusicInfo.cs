using Music.Infrastructure.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
	public class MusicInfo : BindableBase
	{
		public string Songid { get; set; }
		public string Songname { get; set; }

		public string Subname { get; set; }

		public string Singer { get; set; }

		public string Singerid { get; set; }

		public string Albumname { get; set; }

		public string Albumid { get; set; }

		public string Duration { get; set; }

		public string Mvid { get; set; }

		public string From { get; set; }

		public string Mp3url { get; set; }

		public string Mvurl { get; set; }

		public string Pturl { get; set; }

		public string Lrcurl { get; set; }

		public bool Hasmv { get; set; }

		public bool HasLossless { get; set; }

		public bool Fee { get; set; }

		public string Likedate { get; set; }

		public string Eext { get; set; }

		public string Data { get; set; }

		public string Path { get; set; }

		public string SongPath { get; set; }

		public string PicPath { get; set; }

		public string Suffix { get; set; }

		public string State { get; set; }

		public string Quality { get; set; }

		public int Progress { get; set; }

        private bool _isPlaying;

        public bool IsPlaying
		{
            get { return _isPlaying; }
            set { SetProperty(ref _isPlaying, value); }
        }
	}
}
