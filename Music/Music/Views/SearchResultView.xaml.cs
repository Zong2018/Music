using Music.Infrastructure.Extensions;
using Music.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// SearchResultView.xaml 的交互逻辑
    /// </summary>
    public partial class SearchResultView : UserControl
    {
        public SearchResultView(SearchResultViewModel searchResultViewModel)
        {
            InitializeComponent();
            this.DataContext = searchResultViewModel;
            this.Unloaded += SearchResultView_Unloaded;
        }

        private void SearchResultView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
        }

        private void songDataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            var dgcp = dataGrid.Template.FindName("DG_ScrollViewer", dataGrid) as ScrollViewer;
            if (dgcp != null && dgcp.ExtentHeight - dgcp.ViewportHeight == dgcp.VerticalOffset)
            {
               var searchResultViewModel = this.DataContext as SearchResultViewModel;
                searchResultViewModel.GetMusicByPageAction();
            }
        }
    }
}
