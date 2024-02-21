using Music.Controls;
using Music.Extensions;
using Music.Infrastructure.Configuration;
using Music.Infrastructure.Manager;
using Music.Models;
using Music.ViewModels;
using Music.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Music
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            var theme = PaletteHelper.Instance.GetTheme();
            var hexString = string.IsNullOrWhiteSpace(SettingManager.GlobalSetting.ThemeColor) ? "#FFEC4141" : SettingManager.GlobalSetting.ThemeColor;
            theme.Background = (Color)ColorConverter.ConvertFromString(hexString);
            PaletteHelper.Instance.SetTheme(theme);

            ToolBarViewModel toolBarViewModel = new ToolBarViewModel();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(toolBarViewModel);
            
            //ColorPalette c = new ColorPalette();

			var menu = Infrastructure.Manager.MenuManager.GetMenuTree();

			mainWindowViewModel.MenuItems = new System.Collections.ObjectModel.ObservableCollection<MenuItemDetail>(menu);
			UserControlManager.CreateIntanceAndNotifyUI("Music.Views.FindMusicView");
            RecordManager.RecordOperate(RecordManager.CreateRecord("Music.Views.FindMusicView").CreateOperateAction());
            this.DataContext = mainWindowViewModel;

		}

        private void play_Click(object sender, RoutedEventArgs e)
        {
            //MediaPlayer player = new MediaPlayer();
            // player.Open(new Uri(@"http://rl01.sycdn.kuwo.cn/6a723ef61926c20411a4a6aa219bcc5d/622955b6/resource/n1/3/76/295106251.mp3", UriKind.RelativeOrAbsolute));
            //VideoDrawing aVideoDrawing = new VideoDrawing();
            //aVideoDrawing.Rect = new Rect(0, 0, 100, 100);
            //aVideoDrawing.Player = player;
            //player.Play();

            // McMediaElement.Source = new Uri(@"http://rl01.sycdn.kuwo.cn/6a723ef61926c20411a4a6aa219bcc5d/622955b6/resource/n1/3/76/295106251.mp3", UriKind.RelativeOrAbsolute);
            // McMediaElement.Play();
        }

        private void GridVolume_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Mvplay_Click(object sender, RoutedEventArgs e)
        {
            //McMvMediaElement.Source = new Uri(@"http://win.web.nf03.sycdn.kuwo.cn/0b9d65a4759129697644d39a24949396/622985eb/resource/m2/55/79/333017354.mp4", UriKind.RelativeOrAbsolute);
            //McMvMediaElement.Play();
        }

        private void BarBlank_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!mainBorder.IsFocused)
            {
                mainBorder.Focus();
            }
            this.DragMove();
        
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();//Keyboard.ClearFocus 方法    清除焦点
            mainBorder.Focus();
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
