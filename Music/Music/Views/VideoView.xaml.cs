﻿using Music.ViewModels;
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
    /// VideoView.xaml 的交互逻辑
    /// </summary>
    public partial class VideoView : UserControl
    {
        public VideoView()
        {
            InitializeComponent();
            VideoViewModel videoViewModel = new VideoViewModel();
            this.DataContext = videoViewModel;
            this.Unloaded += VideoView_Unloaded;
        }

        private void VideoView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
        }
    }
}
