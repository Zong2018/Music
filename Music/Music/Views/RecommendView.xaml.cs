using Music.Models;
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
	/// RecommendView.xaml 的交互逻辑
	/// </summary>
	public partial class RecommendView : UserControl
	{
		public RecommendView()
		{
			InitializeComponent();
			ImageNode node1 = new ImageNode() { Num=1, Tag="新歌首发", ImageSource = "http://img1.kuwo.cn/star/userpl2015/61/28/1648860863567_556832161_500.jpg" };
			ImageNode node2= new ImageNode() { Num = 2, Tag = "广告", ImageSource = "http://img1.kuwo.cn/star/userpl2015/30/92/1648201954981_544783430_500.jpg" };
			ImageNode node3 = new ImageNode() { Num = 3, Tag = "新歌首发", ImageSource = "http://img1.kwcdn.kuwo.cn/star/userpl2015/95/19/1648912109004_554818195_500.jpg" };
			ImageNode node4 = new ImageNode() { Num = 4, Tag = "广告", ImageSource = "http://img1.kuwo.cn/star/userpl2015/24/2/1648809347915_544280024_500.jpg" };
			ImageNode node5 = new ImageNode() { Num = 5, Tag = "新碟首发", ImageSource = "http://img1.kwcdn.kuwo.cn/star/userpl2015/83/91/1648278940332_518632183_500.jpg" };
			ImageNode node6 = new ImageNode() { Num = 6, Tag = "新歌首发", ImageSource = "http://img1.kuwo.cn/star/userpl2015/89/72/1648950627443_559071189_500.jpg" };
			ImageNode node7 = new ImageNode() { Num = 7, Tag = "新碟首发", ImageSource = "http://img1.kuwo.cn/star/userpl2015/34/85/1645614558249_448366234_500.jpg" };
			ImageNode node8 = new ImageNode() { Num = 8, Tag = "广告", ImageSource = "http://img1.kwcdn.kuwo.cn/star/userpl2015/19/23/1647947479189_516097319_500.jpg" };

			List<ImageNode> imageNodes = new List<ImageNode>();
			imageNodes.Add(node1);
			imageNodes.Add(node2);
			imageNodes.Add(node3);
			imageNodes.Add(node4);
			imageNodes.Add(node5);
			imageNodes.Add(node6);
			imageNodes.Add(node7);
			imageNodes.Add(node8);
			imagePlayer.ItemSource = imageNodes;

            this.Unloaded += RecommendView_Unloaded;
		}

        private void RecommendView_Unloaded(object sender, RoutedEventArgs e)
        {
			imagePlayer.ItemSource = new List<ImageNode>();
		}
    }
}
