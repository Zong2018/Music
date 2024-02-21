using Music.Models;
using Music.ViewModels;
using System;
using System.Collections.Generic;
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
    /// PlayListView.xaml 的交互逻辑
    /// </summary>
    public partial class PlayListView : UserControl
    {
        public PlayListView(object viewModel)
        {
            InitializeComponent();
            PlayListViewModel playListViewModel = new PlayListViewModel(viewModel as SongSheetInfo);
            this.DataContext = playListViewModel;
            this.Unloaded += PlayListView_Unloaded;

            this.SetBinding(PlayListView.MainMaxHeightProperty, new Binding("MaxHeight")
            {
                Source = MainWindowViewModel.MainContent,
                Mode = BindingMode.TwoWay
            });

        }

        private void PlayListView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
        }

        public double MainMaxHeight
        {
            get { return (double)GetValue(MainMaxHeightProperty); }
            set { SetValue(MainMaxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainMaxHeightProperty =
            DependencyProperty.Register("MainMaxHeight", typeof(double), typeof(PlayListView), new PropertyMetadata(default(double),new PropertyChangedCallback(MainMaxHeightPropertyChangedCallback)));

        static void MainMaxHeightPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PlayListView;
            var mainWinMaxHeight = (double)e.NewValue;
            if (mainWinMaxHeight > 0 && control.songDataGrid != null)
            {
                control.songDataGrid.MaxHeight = mainWinMaxHeight - 131-270;
                control.scroll.InvalidateScrollInfo();
            }
        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(scroll.ExtentHeight - scroll.ViewportHeight == scroll.VerticalOffset)
            {
                songDataGrid.MaxHeight = songDataGrid.MaxHeight + 100;
            }
        }
    }
}
