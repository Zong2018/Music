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
    /// MusicToolBarUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MusicToolBarUserControl : UserControl
    {
        public MusicToolBarUserControl()
        {
            InitializeComponent();
        }

        private void txb_GotFocus(object sender, RoutedEventArgs e)
        {
            ToolBarViewModel viewModel = this.DataContext as ToolBarViewModel;
            if(viewModel != null)
            {
                viewModel.CanSearch = true;
            }
            else
            {
                viewModel.CanSearch = false;
            }
        }

        private void txb_LostFocus(object sender, RoutedEventArgs e)
        {
            ToolBarViewModel viewModel = this.DataContext as ToolBarViewModel;
            if (viewModel != null)
            {
                viewModel.CanSearch = false;
            }
            else
            {
                viewModel.CanSearch = true;
            }
        }

        private void searchBtn_GotFocus(object sender, RoutedEventArgs e)
        {
            ToolBarViewModel viewModel = this.DataContext as ToolBarViewModel;
            if (viewModel != null)
            {
                viewModel.CanSearch = true;
            }
            else
            {
                viewModel.CanSearch = false;
            }
        }

        private void searchBtn_LostFocus(object sender, RoutedEventArgs e)
        {
            ToolBarViewModel viewModel = this.DataContext as ToolBarViewModel;
            if (viewModel != null)
            {
                viewModel.CanSearch = false;
            }
            else
            {
                viewModel.CanSearch = true;
            }
        }

        private void ColorPalette_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ColorSolidColorPalette_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
