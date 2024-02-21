using Music.Models;
using Music.MusicApi;
using Music.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Music.Views
{
    /// <summary>
    /// FindMusicView.xaml 的交互逻辑
    /// </summary>
    public partial class FindMusicView : UserControl
    {
        public FindMusicView()
        {
            InitializeComponent();
            FindMusicViewModel findMusicViewModel = new FindMusicViewModel();
            this.DataContext = findMusicViewModel;
            this.Unloaded += FindMusicView_Unloaded;
        }

        private void FindMusicView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
        }
    }
}
