using Music.Infrastructure.Extensions;
using Music.Infrastructure.Manager;
using Music.Models;
using Music.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Music.Views
{
    /// <summary>
    /// SongWordView.xaml 的交互逻辑
    /// </summary>
    public partial class SongWordView : UserControl
    {
        public static bool GetIsChangedCurrentIndex(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsChangedCurrentIndexProperty);
        }

        public static void SetIsChangedCurrentIndex(DependencyObject obj, bool value)
        {
            obj.SetValue(IsChangedCurrentIndexProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsChangedCurrentIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChangedCurrentIndexProperty =
            DependencyProperty.RegisterAttached("IsChangedCurrentIndex", typeof(bool), typeof(SongWordView), new PropertyMetadata(false, new PropertyChangedCallback(HasItemsPropertyChangedCallback)));


        public static bool GetHasItems(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasItemsProperty);
        }

        public static void SetHasItems(DependencyObject obj, bool value)
        {
            obj.SetValue(HasItemsProperty, value);
        }

        // Using a DependencyProperty as the backing store for HasItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasItemsProperty =
            DependencyProperty.RegisterAttached("HasItems", typeof(bool), typeof(SongWordView), new PropertyMetadata(false, new PropertyChangedCallback(HasItemsPropertyChangedCallback)));


        static void HasItemsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                var listBox = d as ListBox;
				if (listBox.ItemsSource == null) return;
                var list = listBox.ItemsSource.Cast<SongWord>().ToList();
                var tempList = list.Skip(3).ToList();
                var s = MediaManager.MediaPlayer.Position.TotalMilliseconds;
                if (s > 0)
                {
                    var curr = tempList.FirstOrDefault(a => a.MinTime <= s && s < a.MaxTime);
                    int index = 3;
                    if (curr == null)
                    {
                        if (tempList[0].MinTime > s)
                        {
                            index = 3;
                        }
                    }
                    else
                    {
                        index = list.IndexOf(curr);
                    }
                    listBox.SelectedIndex = index;
                    var sv = listBox.VisualTreeDepthFirstTraversal().OfType<ScrollViewer>().FirstOrDefault();
                    if (sv != null)
                    {
                        listBox.Visibility = Visibility.Collapsed;
                        listBox.Visibility = Visibility.Visible;

                        var offset = (listBox.SelectedIndex - 2) * 30;
                        sv.ScrollToVerticalOffset(offset);
                    }
                    else
                    {
                        listBox.ScrollIntoView(listBox.SelectedItem);
                    }


                }
                else
                {
                    listBox.SelectedIndex = 0; 
                }
            }
        }

        public static bool GetIsChildSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsChildSelectedProperty);
        }

        public static void SetIsChildSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsChildSelectedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsChildSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChildSelectedProperty =
            DependencyProperty.RegisterAttached("IsChildSelected", typeof(bool), typeof(SongWordView), new PropertyMetadata(false,new PropertyChangedCallback(IsChildSelectedPropertyChangedCallback)));


        public static bool GetIsStart(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsStartProperty);
        }

        public static void SetIsStart(DependencyObject obj, bool value)
        {
            obj.SetValue(IsStartProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsStart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsStartProperty =
            DependencyProperty.RegisterAttached("IsStart", typeof(bool), typeof(SongWordView), new PropertyMetadata(false, new PropertyChangedCallback(IsStartPropertyChangedCallback)));

        static void IsStartPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListBox;
            var sv = control.VisualTreeAncestory().OfType<SongWordView>().FirstOrDefault();
            if (sv != null && SongWordView.sb != null)
            {
                if (e.NewValue != e.OldValue && (bool)e.NewValue)
                {
                    SongWordView.sb.Resume();
                }
                else
                {
                    SongWordView.sb.Pause();
                }
            }

        }


        static Storyboard sb = null;
        static void IsChildSelectedPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = d as Canvas;
            var listBoxs = canvas.VisualTreeAncestory().OfType<ListBox>().ToList();
            if (listBoxs == null || listBoxs.Count == 0) return;
            var listBox = listBoxs.First();

            var sv = listBox.VisualTreeDepthFirstTraversal().OfType<ScrollViewer>().FirstOrDefault();
            if (sv != null)
            {
                var offset = (listBox.SelectedIndex - 2) * 30;
                var listBoxItem = listBox.VisualTreeDepthFirstTraversal().OfType<ListBoxItem>().FirstOrDefault();
                if (listBoxItem != null)
                {
                    var maxHovOffset = listBoxItem.ActualWidth - sv.ActualWidth;
                    if (maxHovOffset > 0)
                    {
                        sv.ScrollToHorizontalOffset(maxHovOffset / 2);
                    }
                }
            }

            if ((bool)e.NewValue)
            {
                var listBoxItems = canvas.VisualTreeAncestory().OfType<ListBoxItem>().ToList();
                if (listBoxItems == null || listBoxItems.Count == 0) return;
                var listBoxItem = listBoxItems.First();
                var txb = listBoxItem.Template.FindName("wordTxb", listBoxItem) as TextBlock;
                if (txb == null) return;
                sb = new Storyboard();
                DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
                doubleAnimationWidth.From = canvas.Width;
                doubleAnimationWidth.To = txb.ActualWidth;
                if (listBox.SelectedIndex == -1) return;
                var currentItem = listBox.Items[listBox.SelectedIndex] as SongWord;
                doubleAnimationWidth.Duration = currentItem.SongWordDuration;
                var ms = MediaManager.MediaPlayer.Position.TotalMilliseconds;
                var diffMs = ms - currentItem.MinTime;
                if (currentItem.MaxTime <= ms)
                {
                    doubleAnimationWidth.Duration = new Duration(TimeSpan.FromMilliseconds(1));
                }
                else
                {
                    var drnTime = Math.Abs(doubleAnimationWidth.Duration.TimeSpan.TotalMilliseconds - diffMs);
                    doubleAnimationWidth.Duration = new Duration(TimeSpan.FromMilliseconds((drnTime > 1 ? drnTime : 1)));

                }
                Storyboard.SetTarget(doubleAnimationWidth, canvas);
                Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));
                sb.Children.Add(doubleAnimationWidth);
                sb.Completed += (s, o) => {
                    if (listBox.SelectedIndex + 1 < SongWordViewModel.Song.SongWord.Count)
                    {
                        listBox.SelectedIndex = listBox.SelectedIndex + 1;
                    }
                };
                if (!PlayMusicControlViewModel.CurrentMusic.IsPlay)
                {
                    sb.Begin();
                    sb.Pause();
                }
                else
                    sb.Begin();
            }
            else
            {
                var listBoxItems = canvas.VisualTreeAncestory().OfType<ListBoxItem>().ToList();
                if (listBoxItems == null || listBoxItems.Count == 0) return;
                var listBoxItem = listBoxItems.First();
                var txb = listBoxItem.Template.FindName("wordTxb", listBoxItem) as TextBlock;
                if (txb == null) return;
                Storyboard sb = new Storyboard();
                DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
                doubleAnimationWidth.From = canvas.Width;
                doubleAnimationWidth.To = 0;
                doubleAnimationWidth.Duration = new Duration(TimeSpan.FromSeconds(0));
                //doubleAnimationWidth.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
                Storyboard.SetTarget(doubleAnimationWidth, canvas);
                Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));
                sb.Children.Add(doubleAnimationWidth);
                sb.Begin();
            }
        }


        static bool ExcuteTimer(Func<bool> func, int ms)
        {
            bool isOk = true;

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            while (true)
            {
                bool isok = func.Invoke();
                if (isok)
                {
                    isOk = true;
                    break;
                }

                if (watch.ElapsedMilliseconds > ms) //超时
                {
                    isOk = false;
                    break;
                }
            }

            watch.Stop();
            return isOk;
        }

        public SongWordView(SongWordViewModel songWordViewModel)
        {
            InitializeComponent();
            this.DataContext = songWordViewModel;
            this.Loaded += SongWordView_Loaded;
            this.wordList.Loaded += WordList_Loaded;
            this.Unloaded += SongWordView_Unloaded;
            this.wordList.SelectionChanged += WordList_SelectionChanged;
        }

        private void SongWordView_Unloaded(object sender, RoutedEventArgs e)
        {
            SongWordView.sb.Pause();
            SongWordView.sb = null;
            this.wordList.ItemsSource = null;
        }

        private void WordList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sv = wordList.VisualTreeDepthFirstTraversal().OfType<ScrollViewer>().FirstOrDefault();
            if (sv != null)
            {
                var offset = (wordList.SelectedIndex - 2) * 30;
                sv.ScrollToVerticalOffset(offset);
                //var listBoxItem = wordList.VisualTreeDepthFirstTraversal().OfType<ListBoxItem>().FirstOrDefault();
                //if (listBoxItem != null)
                //{
                //    var maxHovOffset = listBoxItem.ActualWidth - sv.ActualWidth;
                //    if (maxHovOffset > 0)
                //    {
                //        sv.ScrollToHorizontalOffset(maxHovOffset / 2);
                //    }
                //}
            }
        }

        private void WordList_Loaded(object sender, RoutedEventArgs e)
        {
            var sv = wordList.VisualTreeDepthFirstTraversal().OfType<ScrollViewer>().FirstOrDefault();
            if (sv != null)
            {
                var offset = (wordList.SelectedIndex - 2) * 30;
                sv.ScrollToVerticalOffset(offset);
                var listBoxItem = wordList.VisualTreeDepthFirstTraversal().OfType<ListBoxItem>().FirstOrDefault();
                if (listBoxItem != null)
                {
                    var maxHovOffset = listBoxItem.ActualWidth - sv.ActualWidth;
                    if (maxHovOffset > 0)
                    {
                        sv.ScrollToHorizontalOffset(maxHovOffset / 2);
                    }
                }
            }
        }

        private void SongWordView_Loaded(object sender, RoutedEventArgs e)
        {
          

            //var item = wordList.VisualTreeDepthFirstTraversal().OfType<ListBoxItem>().First();
            //var child = wordList.ItemTemplate.FindName("coverCanvas", item);
        }

        private void wordList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
          
        }
    }
}
