using Music.Infrastructure.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
    /// <summary>
    /// 歌单信息
    /// </summary>
    public class SongSheetInfo : BindableBase
	{
		public string ID { get; set; }

		public string Pic { get; set; }

		public string Name { get; set; }

		public string Uname { get; set; }

		public string Speak { get; set; }

		public string Favnum { get; set; }

		public string Duration { get; set; }

		public string Listen { get; set; }

		public string Total { get; set; }

		private string _info;

		public string Info
		{
			get { return _info; }
			set { SetProperty(ref _info, value); }
		}

		private string _tag;
		public string Tag
		{
			get { return _tag; }
			set { SetProperty(ref _tag, value); }
		}
	}
}
