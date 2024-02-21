using Music.Infrastructure.Manager;
using Music.Models;
using Music.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Music.Views
{
    /// <summary>
    /// VideoPlayerView.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPlayerView : UserControl
    {
        public VideoPlayerView()
        {
            InitializeComponent();
			Grid.SetRow(VlcMediaManager.VlcControl,0);
			grid.Children.Add(VlcMediaManager.VlcControl);
            this.Unloaded += VideoPlayerView_Unloaded;
        }

        static VideoPlayerView()
        {
            Control.VisibilityProperty.OverrideMetadata(typeof(VideoPlayerView), new FrameworkPropertyMetadata(VisibilityPropertyChangedCallback));
        }

        static void VisibilityPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as VideoPlayerView;
            if ((Visibility)e.NewValue != (Visibility)e.OldValue && ((Visibility)e.NewValue == Visibility.Collapsed || (Visibility)e.NewValue == Visibility.Hidden))
            {
                var viewModle = control.DataContext as VideoPlayerViewModel;
                if (viewModle != null)
                {
                    viewModle._timer?.Stop();
                    VlcMediaManager.MediaPlayer.ResetMedia();
                }
            }
        }

        private void VideoPlayerView_Unloaded(object sender, RoutedEventArgs e)
        {
            var viewModle = this.DataContext as VideoPlayerViewModel;
            if(viewModle != null)
            {
                viewModle._timer?.Stop();
            }
        }
    }
}
