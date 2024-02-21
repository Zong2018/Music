using Music.Infrastructure.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
    public class MvSheetInfo : BindableBase
	{
		public string ID { get; set; }

		public string Pic { get; set; }

		public string Name { get; set; }

		public string Uname { get; set; }
		public string UnameId { get; set; }

		public string Duration { get; set; }

		public string Listen { get; set; }

		public string From { get; set; }

		public string Source { get; set; }
		
	}
}
