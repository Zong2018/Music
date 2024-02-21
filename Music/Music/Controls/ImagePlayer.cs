using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Music.Controls
{
	[TemplatePart(Name = PART_CenterControlPartName, Type = typeof(ImagePlayer))]
	[TemplatePart(Name = PART_LeftControlPartName, Type = typeof(ImagePlayer))]
	[TemplatePart(Name = PART_RightControlPartName, Type = typeof(ImagePlayer))]
	[TemplatePart(Name = PART_LastControlPartName, Type = typeof(ImagePlayer))]
	[TemplatePart(Name = PART_RightButtonPartName, Type = typeof(ImagePlayer))]
	[TemplatePart(Name = PART_LeftButtonPartName, Type = typeof(ImagePlayer))]
	[TemplatePart(Name = PART_ListBoxPartName, Type = typeof(ImagePlayer))]
	public class ImagePlayer : Control
	{
		public const string PART_CenterControlPartName = "PART_CenterControl";
		public const string PART_LeftControlPartName = "PART_LeftControl";
		public const string PART_RightControlPartName = "PART_RightControl";
		public const string PART_LastControlPartName = "PART_LastControl";
		public const string PART_RightButtonPartName = "PART_RightButton";
		public const string PART_LeftButtonPartName = "PART_LeftButton";
		public const string PART_ListBoxPartName = "PART_ListBox";
		CircularLinkedList<ImageNode> _circularLinkedList;

		ContentControl _centerContent;
		ContentControl _leftContent;
		ContentControl _rightContent;
		ContentControl _lastContent;
		ListBox _listBox;

		List<ContentControl> contentControls;
		double _centerContentWidth;
		double _leftContentWidth;
		double _rightContentWidth;
		double _lastContentWidth;
		double _centerContentLeft;
		double _leftContentLeft;
		double _rightContentLeft;
		double _lastContentLeft;

		Button _rightButton;
		Button _leftButton;

		Node<ImageNode> CenterImageNode;

		DispatcherTimer _playerTimer;

		static ImagePlayer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ImagePlayer), new FrameworkPropertyMetadata(typeof(ImagePlayer)));
			Control.WidthProperty.OverrideMetadata(typeof(ImagePlayer), new FrameworkPropertyMetadata(OnWidthChanged)); 
		}

		static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var player = d as ImagePlayer;
			var width = (double)e.NewValue;
			if (width == 0) return;
			if(player._centerContent != null)
			{
				RetSetProperty(player, player._centerContent,width);
				RetSetProperty(player, player._leftContent, width);
				RetSetProperty(player, player._rightContent, width);
				RetSetProperty(player, player._lastContent, width);
			}
		}

		/// <summary>
		/// 参考动画后修改属性值https://docs.microsoft.com/zh-cn/previous-versions/dotnet/netframework-4.0/aa970493(v=vs.100)?redirectedfrom=MSDN
		/// </summary>
		/// <param name="player"></param>
		/// <param name="contentControl"></param>
		/// <param name="width"></param>
		static void RetSetProperty(ImagePlayer player, ContentControl contentControl ,double width)
		{
			var ccState = contentControl.Tag.ToString();
			if (ccState == "center")
			{
				player._centerContentWidth = width / 38 * 27;
				player._centerContentLeft = (width - player._centerContentWidth) / 2;
				contentControl.BeginAnimation(ContentControl.WidthProperty, null);
				contentControl.BeginAnimation(ContentControl.HeightProperty, null);
				contentControl.BeginAnimation(Canvas.LeftProperty, null);
				contentControl.Width = player._centerContentWidth;
				contentControl.Height = 200;
				Canvas.SetLeft(contentControl, player._centerContentLeft);
			}
			else if (ccState == "left")
			{
				player._leftContentWidth = width / 2;
				player._leftContentLeft = 0;
				contentControl.BeginAnimation(ContentControl.WidthProperty, null);
				contentControl.BeginAnimation(ContentControl.HeightProperty, null);
				contentControl.BeginAnimation(Canvas.LeftProperty, null);

				contentControl.Width = player._leftContentWidth;
				contentControl.Height = 160;
				Canvas.SetLeft(contentControl, player._leftContentLeft);
			}
			else if (ccState == "right")
			{
				player._rightContentWidth = width / 2;
				player._rightContentLeft = width / 2;
				contentControl.BeginAnimation(ContentControl.WidthProperty, null);
				contentControl.BeginAnimation(Canvas.LeftProperty, null);
				contentControl.BeginAnimation(ContentControl.HeightProperty, null);
				contentControl.Width = player._rightContentWidth;
				contentControl.Height = 160;
				Canvas.SetLeft(contentControl, player._rightContentLeft);
			}
			else if (ccState == "last")
			{
				player._lastContentWidth = width / 38 * 11;
				player._lastContentLeft = (width - player._lastContentWidth) / 2;
				contentControl.BeginAnimation(ContentControl.WidthProperty, null);
				contentControl.BeginAnimation(ContentControl.HeightProperty, null);
				contentControl.BeginAnimation(Canvas.LeftProperty, null);
				contentControl.Width = player._lastContentWidth;
				contentControl.Height = 180;
				Canvas.SetLeft(contentControl, player._lastContentLeft);
			}
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			_centerContent = this.GetTemplateChild(PART_CenterControlPartName) as ContentControl;
			_centerContent.Tag = "center";
		
			_leftContent = this.GetTemplateChild(PART_LeftControlPartName) as ContentControl;
			_leftContent.Tag = "left";
			
			_rightContent = this.GetTemplateChild(PART_RightControlPartName) as ContentControl;
			_rightContent.Tag = "right";
			
			_lastContent = this.GetTemplateChild(PART_LastControlPartName) as ContentControl;
			_lastContent.Tag = "last";

			_listBox = this.GetTemplateChild(PART_ListBoxPartName) as ListBox;
			_listBox.SelectionMode = SelectionMode.Single;

			_rightButton = this.GetTemplateChild(PART_RightButtonPartName) as Button;
			_leftButton = this.GetTemplateChild(PART_LeftButtonPartName) as Button;
			_rightButton.Click += _rightButton_Click;
			_leftButton.Click += _leftButton_Click;

			contentControls = new List<ContentControl>();
			contentControls.Add(_centerContent);
			contentControls.Add(_leftContent);
			contentControls.Add(_rightContent);
			contentControls.Add(_lastContent);

			this.Loaded += ImagePlayer_Loaded;
            this.Unloaded += ImagePlayer_Unloaded;
            this.MouseEnter += ImagePlayer_MouseEnter;
            this.MouseLeave += ImagePlayer_MouseLeave;
			var player = this;
			if (player._centerContent != null)
			{
				player._centerContent.DataContext = player._circularLinkedList.head;
				player.CenterImageNode = player._circularLinkedList.head;
			}
			if (player._leftContent != null)
			{
				player._leftContent.DataContext = player._circularLinkedList.head.previous;
			}
			if (player._rightContent != null)
			{
				player._rightContent.DataContext = player._circularLinkedList.head.next;
			}
			if (player._lastContent != null)
			{
				player._lastContent.DataContext = player._circularLinkedList.head.next.next;
			}

		}

        private void ImagePlayer_Unloaded(object sender, RoutedEventArgs e)
        {
			_playerTimer?.Stop();
			_playerTimer = null;
		}

        private void ImagePlayer_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
			if (_playerTimer != null)
			{
				_playerTimer.Start();
			}
		}

        private void ImagePlayer_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           if(_playerTimer != null)
            {
				_playerTimer.Stop();
			}
        }

        private void ImagePlayer_Loaded(object sender, RoutedEventArgs e)
		{
			var player = sender as ImagePlayer;

			CalculatePropertyValue(this.ActualWidth);
			if(_listBox.Items.Count == 0)
            {
				for (int i = 0; i < player.ItemSource.Count; i++)
				{
					ListBoxItem listBoxItem = new ListBoxItem();
					listBoxItem.MouseEnter += ListBoxItem_MouseEnter;
					player._listBox.Items.Add(listBoxItem);
				}
				player.SelectedIndex = 0;
				OldMouseOverIndex = 0;
			}

			if(_playerTimer == null)
            {
				_playerTimer = new DispatcherTimer();
				_playerTimer.Interval = TimeSpan.FromSeconds(3);
                _playerTimer.Tick += _playerTimer_Tick;
				_playerTimer.Start();
			}
		}

        private void _playerTimer_Tick(object sender, EventArgs e)
        {
			this._rightButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this));
		}

		/// <summary>
		/// 
		/// </summary>
        static bool _isMouseOverRight = false;
        static bool _isMouseOverLeft = false;
		int OldMouseOverIndex;
        private void ListBoxItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
			ListBoxItem listBoxItem = sender as  ListBoxItem;
			
			int index = _listBox.ItemContainerGenerator.IndexFromContainer(listBoxItem);
			int newIndex = 0;
			if(index >= OldMouseOverIndex)
            {
				//右边
				_isMouseOverRight = true;
				_isMouseOverLeft = false;
				newIndex = index - 1 < 0 ? this.ItemSource.Count - 1 : index - 1;
			}
			else
            {
				//左边
				_isMouseOverRight = false;
				_isMouseOverLeft = true;
				newIndex = index + 1 > this.ItemSource.Count - 1 ? 0 : index + 1;
			}
			OldMouseOverIndex = index;
			if (this.ItemSource.Count() == 0) return;
			var current = this.ItemSource.Cast<ImageNode>().ToList()[newIndex];
            CenterImageNode = _circularLinkedList.Find(current);

			listBoxItem.IsSelected = true;
			_listBox.SelectedItem = listBoxItem;
		}

		static void RightButtonChangedImage(ImagePlayer player)
		{
			var center = player.contentControls.Find(a=>a.Tag.ToString()=="center");
			var left = player.contentControls.Find(a => a.Tag.ToString() == "left");
			var right = player.contentControls.Find(a => a.Tag.ToString() == "right");
			var last = player.contentControls.Find(a => a.Tag.ToString() == "last");

			center.DataContext = player.CenterImageNode.next;
			left.DataContext = player.CenterImageNode;
			right.DataContext = player.CenterImageNode.next.next;
			player.CenterImageNode = player.CenterImageNode.next;
		}

		static void LeftButtonChangedImage(ImagePlayer player)
		{
			var center = player.contentControls.Find(a => a.Tag.ToString() == "center");
			var left = player.contentControls.Find(a => a.Tag.ToString() == "left");
			var right = player.contentControls.Find(a => a.Tag.ToString() == "right");
			var last = player.contentControls.Find(a => a.Tag.ToString() == "last");

			center.DataContext = player.CenterImageNode.previous;
			left.DataContext = player.CenterImageNode.previous.previous;
			right.DataContext = player.CenterImageNode;
			player.CenterImageNode = player.CenterImageNode.previous;
		}

		void CalculatePropertyValue(double width)
		{
			_centerContentWidth = width / 38 * 27;
			_centerContentLeft = (width - _centerContentWidth) / 2;

			_leftContentWidth = width / 2;
			_leftContentLeft = 0;

			_rightContentWidth = width / 2;
			_rightContentLeft = width / 2;

			_lastContentWidth = width / 38 * 11;
			_lastContentLeft = (width - _lastContentWidth) / 2;
		}

		private void _leftButton_Click(object sender, RoutedEventArgs e)
		{
			int newIndex = this.SelectedIndex - 1;
			this.SelectedIndex = newIndex < 0 ? this.ItemSource.Count - 1 : newIndex;
		}

		private void _rightButton_Click(object sender, RoutedEventArgs e)
		{
			int newIndex = this.SelectedIndex + 1;
			this.SelectedIndex = newIndex > this.ItemSource.Count - 1? 0: newIndex;
		}

		static void RightButtonAnimationAction(Storyboard sb,ImagePlayer player,ContentControl control)
		{
			var ccState = control.Tag.ToString();
			if (ccState == "center")
			{
				control.Tag = "left";

				//宽变220， 高边160， 位置变为 top 边20 left 变0 zindex 变 2
				DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
				doubleAnimationHeight.From = control.Height;
				doubleAnimationHeight.To = 160;
				doubleAnimationHeight.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationHeight.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationHeight, control);
				Storyboard.SetTargetProperty(doubleAnimationHeight, new PropertyPath("Height"));

				DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
				doubleAnimationWidth.From = control.Width;
				doubleAnimationWidth.To = player._leftContentWidth;
				doubleAnimationWidth.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationWidth.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationWidth, control);
				Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));

				DoubleAnimation doubleAnimationTop = new DoubleAnimation();
				doubleAnimationTop.From = Canvas.GetTop(control);
				doubleAnimationTop.To = 20;
				doubleAnimationTop.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationTop.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationTop, control);
				Storyboard.SetTargetProperty(doubleAnimationTop, new PropertyPath("(Canvas.Top)"));

				DoubleAnimation doubleAnimationLeft = new DoubleAnimation();
				doubleAnimationLeft.From = Canvas.GetLeft(control);
				doubleAnimationLeft.To = player._leftContentLeft;
				doubleAnimationLeft.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				Storyboard.SetTarget(doubleAnimationLeft, control);
				Storyboard.SetTargetProperty(doubleAnimationLeft, new PropertyPath("(Canvas.Left)"));


				Int32Animation zIndexAnimation = new Int32Animation
				{
					To = 2,
					Duration = TimeSpan.FromSeconds(0.6),
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
				};
				Storyboard.SetTarget(zIndexAnimation, control);
				Storyboard.SetTargetProperty(zIndexAnimation, new PropertyPath("(Panel.ZIndex)"));

				sb.Children.Add(doubleAnimationHeight);
				sb.Children.Add(doubleAnimationWidth);
				sb.Children.Add(doubleAnimationTop);
				sb.Children.Add(doubleAnimationLeft);
				//sb.Children.Add(keyFramesZIndex);
				sb.Children.Add(zIndexAnimation);

				//sb.Begin();
			}
			else if (ccState == "left")
			{
				control.Tag = "last";

				DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
				doubleAnimationHeight.From = control.Height;
				doubleAnimationHeight.To = 180;
				doubleAnimationHeight.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationHeight.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationHeight, control);
				Storyboard.SetTargetProperty(doubleAnimationHeight, new PropertyPath("Height"));

				DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
				doubleAnimationWidth.From = control.Width;
				doubleAnimationWidth.To = player._lastContentWidth;
				doubleAnimationWidth.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationWidth.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationWidth, control);
				Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));

				DoubleAnimation doubleAnimationTop = new DoubleAnimation();
				doubleAnimationTop.From = Canvas.GetTop(control);
				doubleAnimationTop.To = 10;
				doubleAnimationTop.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationTop.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationTop, control);
				Storyboard.SetTargetProperty(doubleAnimationTop, new PropertyPath("(Canvas.Top)"));

				DoubleAnimation doubleAnimationLeft = new DoubleAnimation();
				doubleAnimationLeft.From = Canvas.GetLeft(control);
				doubleAnimationLeft.To = player._lastContentLeft;
				doubleAnimationLeft.Duration = new Duration(TimeSpan.FromSeconds(0.8));
				doubleAnimationLeft.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationLeft, control);
				Storyboard.SetTargetProperty(doubleAnimationLeft, new PropertyPath("(Canvas.Left)"));

				Int32Animation zIndexAnimation = new Int32Animation
				{
					To = 1,
					Duration = TimeSpan.FromSeconds(0.6),
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
				};
				Storyboard.SetTarget(zIndexAnimation, control);
				Storyboard.SetTargetProperty(zIndexAnimation, new PropertyPath("(Panel.ZIndex)"));

				sb.Children.Add(doubleAnimationHeight);
				sb.Children.Add(doubleAnimationWidth);
				sb.Children.Add(doubleAnimationTop);
				sb.Children.Add(doubleAnimationLeft);
				sb.Children.Add(zIndexAnimation);
			}
			else if (ccState == "last")
			{
				control.Tag = "right";
                DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
                doubleAnimationHeight.From = control.Height;
                doubleAnimationHeight.To = 160;
                doubleAnimationHeight.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationHeight.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationHeight, control);
                Storyboard.SetTargetProperty(doubleAnimationHeight, new PropertyPath("Height"));

                DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
				doubleAnimationWidth.From = control.Width;
				doubleAnimationWidth.To = player._rightContentWidth;
				doubleAnimationWidth.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationWidth.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationWidth, control);
				Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));

                DoubleAnimation doubleAnimationTop = new DoubleAnimation();
                doubleAnimationTop.From = Canvas.GetTop(control);
                doubleAnimationTop.To = 20;
                doubleAnimationTop.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationTop.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationTop, control);
                Storyboard.SetTargetProperty(doubleAnimationTop, new PropertyPath("(Canvas.Top)"));

				DoubleAnimation doubleAnimationLeft = new DoubleAnimation();
				doubleAnimationLeft.From = Canvas.GetLeft(control);
				doubleAnimationLeft.To = player._rightContentLeft;
				doubleAnimationLeft.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationLeft.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationLeft, control);
				Storyboard.SetTargetProperty(doubleAnimationLeft, new PropertyPath("(Canvas.Left)"));

				Int32Animation zIndexAnimation = new Int32Animation
				{
					To = 2,
					Duration = TimeSpan.FromSeconds(0.6),
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
				};
				Storyboard.SetTarget(zIndexAnimation, control);
				Storyboard.SetTargetProperty(zIndexAnimation, new PropertyPath("(Panel.ZIndex)"));

				sb.Children.Add(doubleAnimationHeight);
                sb.Children.Add(doubleAnimationWidth);
                sb.Children.Add(doubleAnimationTop);
                sb.Children.Add(doubleAnimationLeft);
				sb.Children.Add(zIndexAnimation);
			}
			else if (ccState == "right")
			{
				control.Tag = "center";
				DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
				doubleAnimationHeight.From = control.Height;
				doubleAnimationHeight.To = 200;
				doubleAnimationHeight.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationHeight.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationHeight, control);
				Storyboard.SetTargetProperty(doubleAnimationHeight, new PropertyPath("Height"));

				DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
				doubleAnimationWidth.From = control.Width;
				doubleAnimationWidth.To = player._centerContentWidth;
				doubleAnimationWidth.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationWidth.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationWidth, control);
				Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));

				DoubleAnimation doubleAnimationTop = new DoubleAnimation();
				doubleAnimationTop.From = Canvas.GetTop(control);
				doubleAnimationTop.To = 0;
				doubleAnimationTop.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationTop.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationTop, control);
				Storyboard.SetTargetProperty(doubleAnimationTop, new PropertyPath("(Canvas.Top)"));

				DoubleAnimation doubleAnimationLeft = new DoubleAnimation();
				doubleAnimationLeft.From = Canvas.GetLeft(control);
				doubleAnimationLeft.To = player._centerContentLeft;
				doubleAnimationLeft.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationLeft.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationLeft, control);
				Storyboard.SetTargetProperty(doubleAnimationLeft, new PropertyPath("(Canvas.Left)"));

				Int32Animation zIndexAnimation = new Int32Animation
				{
					To = 3,
					Duration = TimeSpan.FromSeconds(0.6),
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
				};
				Storyboard.SetTarget(zIndexAnimation, control);
				Storyboard.SetTargetProperty(zIndexAnimation, new PropertyPath("(Panel.ZIndex)"));

				sb.Children.Add(doubleAnimationHeight);
				sb.Children.Add(doubleAnimationWidth);
				sb.Children.Add(doubleAnimationTop);
				sb.Children.Add(doubleAnimationLeft);
				sb.Children.Add(zIndexAnimation);

			}
		}

		static void LeftButtonAnimationAction(Storyboard sb, ImagePlayer player, ContentControl control)
		{
			var ccState = control.Tag.ToString();
			if (ccState == "center")
			{
				control.Tag = "right";

				DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
				doubleAnimationHeight.From = control.Height;
				doubleAnimationHeight.To = 160;
				doubleAnimationHeight.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationHeight.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationHeight, control);
				Storyboard.SetTargetProperty(doubleAnimationHeight, new PropertyPath("Height"));

				DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
				doubleAnimationWidth.From = control.Width;
				doubleAnimationWidth.To = player._rightContentWidth;
				doubleAnimationWidth.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationWidth.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationWidth, control);
				Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));

				DoubleAnimation doubleAnimationTop = new DoubleAnimation();
				doubleAnimationTop.From = Canvas.GetTop(control);
				doubleAnimationTop.To = 20;
				doubleAnimationTop.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationTop.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationTop, control);
				Storyboard.SetTargetProperty(doubleAnimationTop, new PropertyPath("(Canvas.Top)"));

				DoubleAnimation doubleAnimationLeft = new DoubleAnimation();
				doubleAnimationLeft.From = Canvas.GetLeft(control);
				doubleAnimationLeft.To = player._rightContentLeft;
				doubleAnimationLeft.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationLeft.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationLeft, control);
				Storyboard.SetTargetProperty(doubleAnimationLeft, new PropertyPath("(Canvas.Left)"));

				Int32Animation zIndexAnimation = new Int32Animation
				{
					To = 2,
					Duration = TimeSpan.FromSeconds(0.6),
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
				};
				Storyboard.SetTarget(zIndexAnimation, control);
				Storyboard.SetTargetProperty(zIndexAnimation, new PropertyPath("(Panel.ZIndex)"));

				sb.Children.Add(doubleAnimationHeight);
				sb.Children.Add(doubleAnimationWidth);
				sb.Children.Add(doubleAnimationTop);
				sb.Children.Add(doubleAnimationLeft);
				sb.Children.Add(zIndexAnimation);
			}
			else if (ccState == "left")
			{
				control.Tag = "center";

				DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
				doubleAnimationHeight.From = control.Height;
				doubleAnimationHeight.To = 200;
				doubleAnimationHeight.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationHeight.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationHeight, control);
				Storyboard.SetTargetProperty(doubleAnimationHeight, new PropertyPath("Height"));

				DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
				doubleAnimationWidth.From = control.Width;
				doubleAnimationWidth.To = player._centerContentWidth;
				doubleAnimationWidth.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationWidth.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationWidth, control);
				Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));

				DoubleAnimation doubleAnimationTop = new DoubleAnimation();
				doubleAnimationTop.From = Canvas.GetTop(control);
				doubleAnimationTop.To = 0;
				doubleAnimationTop.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationTop.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationTop, control);
				Storyboard.SetTargetProperty(doubleAnimationTop, new PropertyPath("(Canvas.Top)"));

				DoubleAnimation doubleAnimationLeft = new DoubleAnimation();
				doubleAnimationLeft.From = Canvas.GetLeft(control);
				doubleAnimationLeft.To = player._centerContentLeft;
				doubleAnimationLeft.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationLeft.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationLeft, control);
				Storyboard.SetTargetProperty(doubleAnimationLeft, new PropertyPath("(Canvas.Left)"));

				Int32Animation zIndexAnimation = new Int32Animation
				{
					To = 3,
					Duration = TimeSpan.FromSeconds(0.6),
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
				};
				Storyboard.SetTarget(zIndexAnimation, control);
				Storyboard.SetTargetProperty(zIndexAnimation, new PropertyPath("(Panel.ZIndex)"));

				sb.Children.Add(doubleAnimationHeight);
				sb.Children.Add(doubleAnimationWidth);
				sb.Children.Add(doubleAnimationTop);
				sb.Children.Add(doubleAnimationLeft);
				sb.Children.Add(zIndexAnimation);

			}
			else if (ccState == "last")
			{
				control.Tag = "left";

				//宽变220， 高边160， 位置变为 top 边20 left 变0 zindex 变 2
				DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
				doubleAnimationHeight.From = control.Height;
				doubleAnimationHeight.To = 160;
				doubleAnimationHeight.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationHeight.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationHeight, control);
				Storyboard.SetTargetProperty(doubleAnimationHeight, new PropertyPath("Height"));

				DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
				doubleAnimationWidth.From = control.Width;
				doubleAnimationWidth.To = player._leftContentWidth;
				doubleAnimationWidth.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationWidth.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationWidth, control);
				Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));

				DoubleAnimation doubleAnimationTop = new DoubleAnimation();
				doubleAnimationTop.From = Canvas.GetTop(control);
				doubleAnimationTop.To = 20;
				doubleAnimationTop.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationTop.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationTop, control);
				Storyboard.SetTargetProperty(doubleAnimationTop, new PropertyPath("(Canvas.Top)"));

				DoubleAnimation doubleAnimationLeft = new DoubleAnimation();
				doubleAnimationLeft.From = Canvas.GetLeft(control);
				doubleAnimationLeft.To = player._leftContentLeft;
				doubleAnimationLeft.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationLeft.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationLeft, control);
				Storyboard.SetTargetProperty(doubleAnimationLeft, new PropertyPath("(Canvas.Left)"));

				Int32Animation zIndexAnimation = new Int32Animation
				{
					To = 2,
					Duration = TimeSpan.FromSeconds(0.6),
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
				};
				Storyboard.SetTarget(zIndexAnimation, control);
				Storyboard.SetTargetProperty(zIndexAnimation, new PropertyPath("(Panel.ZIndex)"));

				sb.Children.Add(doubleAnimationHeight);
				sb.Children.Add(doubleAnimationWidth);
				sb.Children.Add(doubleAnimationTop);
				sb.Children.Add(doubleAnimationLeft);
				sb.Children.Add(zIndexAnimation);

			}
			else if (ccState == "right")
			{
				control.Tag = "last";

				DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
				doubleAnimationHeight.From = control.Height;
				doubleAnimationHeight.To = 180;
				doubleAnimationHeight.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationHeight.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationHeight, control);
				Storyboard.SetTargetProperty(doubleAnimationHeight, new PropertyPath("Height"));

				DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
				doubleAnimationWidth.From = control.Width;
				doubleAnimationWidth.To = player._lastContentWidth;
				doubleAnimationWidth.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationWidth.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationWidth, control);
				Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));

				DoubleAnimation doubleAnimationTop = new DoubleAnimation();
				doubleAnimationTop.From = Canvas.GetTop(control);
				doubleAnimationTop.To = 10;
				doubleAnimationTop.Duration = new Duration(TimeSpan.FromSeconds(0.5));
				doubleAnimationTop.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationTop, control);
				Storyboard.SetTargetProperty(doubleAnimationTop, new PropertyPath("(Canvas.Top)"));

				DoubleAnimation doubleAnimationLeft = new DoubleAnimation();
				doubleAnimationLeft.From = Canvas.GetLeft(control);
				doubleAnimationLeft.To = player._lastContentLeft;
				doubleAnimationLeft.Duration = new Duration(TimeSpan.FromSeconds(0.8));
				doubleAnimationLeft.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut };
				Storyboard.SetTarget(doubleAnimationLeft, control);
				Storyboard.SetTargetProperty(doubleAnimationLeft, new PropertyPath("(Canvas.Left)"));

				Int32Animation zIndexAnimation = new Int32Animation
				{
					To = 1,
					Duration = TimeSpan.FromSeconds(0.6),
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
				};
				Storyboard.SetTarget(zIndexAnimation, control);
				Storyboard.SetTargetProperty(zIndexAnimation, new PropertyPath("(Panel.ZIndex)"));

				sb.Children.Add(doubleAnimationHeight);
				sb.Children.Add(doubleAnimationWidth);
				sb.Children.Add(doubleAnimationTop);
				sb.Children.Add(doubleAnimationLeft);
				sb.Children.Add(zIndexAnimation);

			}
		}

		public List<ImageNode> ItemSource
		{
			get { return (List<ImageNode>)GetValue(ItemSourceProperty); }
			set { SetValue(ItemSourceProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemSourceProperty =
			DependencyProperty.Register("ItemSource", typeof(List<ImageNode>), typeof(ImagePlayer), new PropertyMetadata(null,new PropertyChangedCallback(ItemSourcePropertyChangedCallback)));

		static void ItemSourcePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var player = d as ImagePlayer;
			var newList = e.NewValue as List<ImageNode>;
			if(newList != null && newList.Count > 0)
			{
				if(player._circularLinkedList != null && player._circularLinkedList.head != null)
				{
					player._circularLinkedList.Clear();
				}
				else
				{
					player._circularLinkedList = new CircularLinkedList<ImageNode>();
				}
				
				foreach(var item in newList)
				{
					player._circularLinkedList.Add(item);
				}
				if (player._centerContent != null)
				{
					player._centerContent.DataContext = player._circularLinkedList.head;
					player.CenterImageNode = player._circularLinkedList.head;
				}
				if (player._leftContent != null)
				{
					player._leftContent.DataContext = player._circularLinkedList.head.previous;
				}
				if (player._rightContent != null)
				{
					player._rightContent.DataContext = player._circularLinkedList.head.next;
				}
				if (player._lastContent != null)
				{
					player._lastContent.DataContext = player._circularLinkedList.head.next.next;
				}

			}
		}

		public int SelectedIndex
		{
			get { return (int)GetValue(SelectedIndexProperty); }
			set { SetValue(SelectedIndexProperty, value); }
		}

		// Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SelectedIndexProperty =
			DependencyProperty.Register("SelectedIndex", typeof(int), typeof(ImagePlayer), new PropertyMetadata(0, new PropertyChangedCallback(SelectedIndexPropertyChangedCallback)));

		static void SelectedIndexPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var player = d as ImagePlayer;
			int oldValue = (int)e.OldValue;
			int newValue = (int)e.NewValue;
			if (player._centerContent == null || player.ItemSource.Count == 0) return;
			if ((newValue > oldValue && !(newValue == player.ItemSource.Count - 1 && oldValue == 0)) || (newValue == 0 && oldValue == player.ItemSource.Count - 1) || _isMouseOverRight)
			{
				Storyboard sb = new Storyboard();
				if (_isMouseOverRight)
                {
					_isMouseOverRight = false;
				}
				var ccState = player._centerContent.Tag.ToString();
				if (ccState == "center")
				{
					RightButtonAnimationAction(sb,player, player._centerContent);
				}
				else if (ccState == "left")
				{
					RightButtonAnimationAction(sb, player, player._centerContent);
				}
				else if (ccState == "last")
				{
					RightButtonAnimationAction(sb, player, player._centerContent);
				}
				else if (ccState == "right")
				{
					RightButtonAnimationAction(sb, player, player._centerContent);
				}

				var rcState = player._rightContent.Tag.ToString();
				if (rcState == "center")
				{
					RightButtonAnimationAction(sb, player, player._rightContent);
				}
				else if (rcState == "left")
				{
					RightButtonAnimationAction(sb, player, player._rightContent);
				}
				else if (rcState == "last")
				{
					RightButtonAnimationAction(sb, player, player._rightContent);
				}
				else if (rcState == "right")
				{
					RightButtonAnimationAction(sb, player, player._rightContent);
				}

				var lcState = player._leftContent.Tag.ToString();
				if (lcState == "center")
				{
					RightButtonAnimationAction(sb, player, player._leftContent);
				}
				else if (lcState == "left")
				{
					RightButtonAnimationAction(sb, player, player._leftContent);
				}
				else if (lcState == "last")
				{
					RightButtonAnimationAction(sb, player, player._leftContent);
				}
				else if (lcState == "right")
				{
					RightButtonAnimationAction(sb, player, player._leftContent);
				}

				var lastcState = player._lastContent.Tag.ToString();
				if (lastcState == "center")
				{
					RightButtonAnimationAction(sb, player, player._lastContent);
				}
				else if (lastcState == "left")
				{
					RightButtonAnimationAction(sb, player, player._lastContent);
				}
				else if (lastcState == "last")
				{
					RightButtonAnimationAction(sb, player, player._lastContent);
				}
				else if (lastcState == "right")
				{
					RightButtonAnimationAction(sb, player, player._lastContent);
				}

				RightButtonChangedImage(player);
				sb.Begin();
				return;
			}
			else 
			{
				Storyboard sb = new Storyboard();
				var ccState = player._centerContent.Tag.ToString();
				if (ccState == "center")
				{
					LeftButtonAnimationAction(sb,player, player._centerContent);
				}
				else if (ccState == "left")
				{
					LeftButtonAnimationAction(sb, player, player._centerContent);
				}
				else if (ccState == "last")
				{
					LeftButtonAnimationAction(sb, player, player._centerContent);
				}
				else if (ccState == "right")
				{
					LeftButtonAnimationAction(sb, player, player._centerContent);
				}

				var rcState = player._rightContent.Tag.ToString();
				if (rcState == "center")
				{
					LeftButtonAnimationAction(sb, player, player._rightContent);
				}
				else if (rcState == "left")
				{
					LeftButtonAnimationAction(sb, player, player._rightContent);
				}
				else if (rcState == "last")
				{
					LeftButtonAnimationAction(sb, player, player._rightContent);
				}
				else if (rcState == "right")
				{
					LeftButtonAnimationAction(sb, player, player._rightContent);
				}

				var lcState = player._leftContent.Tag.ToString();
				if (lcState == "center")
				{
					LeftButtonAnimationAction(sb, player, player._leftContent);
				}
				else if (lcState == "left")
				{
					LeftButtonAnimationAction(sb, player, player._leftContent);
				}
				else if (lcState == "last")
				{
					LeftButtonAnimationAction(sb, player, player._leftContent);
				}
				else if (lcState == "right")
				{
					LeftButtonAnimationAction(sb, player, player._leftContent);
				}

				var lastcState = player._lastContent.Tag.ToString();
				if (lastcState == "center")
				{
					LeftButtonAnimationAction(sb, player, player._lastContent);
				}
				else if (lastcState == "left")
				{
					LeftButtonAnimationAction(sb, player, player._lastContent);
				}
				else if (lastcState == "last")
				{
					LeftButtonAnimationAction(sb, player, player._lastContent);
				}
				else if (lastcState == "right")
				{
					LeftButtonAnimationAction(sb, player, player._lastContent);
				}
				LeftButtonChangedImage(player);
				sb.Begin();
			}
		}
	}
}
