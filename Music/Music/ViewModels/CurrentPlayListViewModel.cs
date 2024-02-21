using Music.Infrastructure.Commands;
using Music.Infrastructure.Mvvm;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Music.ViewModels
{
	public class CurrentPlayListViewModel:BindableBase
	{
		public CurrentPlayListViewModel()
		{
			CurrentPlayListViewContent.MusicInfos = new ObservableCollection<MusicInfo>(Infrastructure.Manager.MediaManager.MusicPlayList??new List<MusicInfo>());
			DoubleClickCommand = new DelegateCommand(
			   (obj) => { return true; },
			   (obj) => {
				   var CurrentMusicInfo = obj as MusicInfo;
				   Infrastructure.Manager.MediaManager.CurrentMusic = CurrentMusicInfo;
				   Infrastructure.Manager.MediaManager.Play(CurrentMusicInfo.Songid, CurrentMusicInfo.Mp3url);
			   }
		   );
		}
		//public ObservableCollection<SongSheetInfo> SongSheetInfos { get; set; }


		public static CurrentPlayListViewContent CurrentPlayListViewContent { get; set; } = new CurrentPlayListViewContent();

		public ICommand DoubleClickCommand { get; }
	}

	public class CurrentPlayListViewContent : BindableBase
	{
		private ObservableCollection<MusicInfo> _musicInfos;

		public ObservableCollection<MusicInfo> MusicInfos
		{
			get { return _musicInfos; }
			set { SetProperty(ref _musicInfos, value); }
		}

	}
}
