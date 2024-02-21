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
    /// RankingListView.xaml 的交互逻辑
    /// </summary>
    public partial class RankingListView : UserControl
    {
        public RankingListView()
        {
            InitializeComponent();

            RankingListViewModel rankingListViewModel = new RankingListViewModel();
            this.DataContext = rankingListViewModel;
            this.Unloaded += RankingListView_Unloaded;
        }

        private void RankingListView_Unloaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = null;
        }
    }
}
