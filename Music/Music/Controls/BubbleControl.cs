using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Music.Controls
{
	[TemplatePart(Name = Part_CanvasPartName, Type = typeof(BubbleControl))]
	public class BubbleControl : Control
	{
		public const string Part_CanvasPartName = "Part_Canvas";
		static BubbleControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(BubbleControl), new FrameworkPropertyMetadata(typeof(BubbleControl)));
		}

		public bool IsStart
		{
			get { return (bool)GetValue(IsStartProperty); }
			set { SetValue(IsStartProperty, value); }
		}

		// Using a DependencyProperty as the backing store for IsStart.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsStartProperty =
			DependencyProperty.Register("IsStart", typeof(bool), typeof(BubbleControl), new PropertyMetadata(false, new PropertyChangedCallback(IsStartPropertyChangedCallback)));

		static void IsStartPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = d as BubbleControl;
			if(control != null && control.weaktimer != null && control.weaktimer.IsAlive)
            {
				if (e.NewValue != e.OldValue && (bool)e.NewValue)
                {
					var timer = control.weaktimer.Target as DispatcherTimer;
					timer.Start();
				}
				else
                {
					var timer = control.weaktimer.Target as DispatcherTimer;
					timer.Stop();
				}
			}
			
		}

		public override void OnApplyTemplate()
		{
			_canvas = this.GetTemplateChild(Part_CanvasPartName) as Canvas;
			this.Loaded += BubbleControl_Loaded;
            this.Unloaded += BubbleControl_Unloaded;
			base.OnApplyTemplate();
		}

        private void BubbleControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (weaktimer != null && weaktimer.IsAlive)
            {
				var timer = weaktimer.Target as DispatcherTimer;
				timer.Stop();
			}
        }

        Dictionary<string, List<Border>> keyValuePairs = new Dictionary<string, List<Border>>();

		Canvas _canvas;
		int count = 1;
		WeakReference weaktimer;
		private void BubbleControl_Loaded(object sender, RoutedEventArgs e)
		{
			_canvas.Children.Clear();
			List<Border> _borders = new List<Border>();
			Random r = new Random();
			for (int i = 0; i < 5; i++)
			{
				var border = SetParticle(new Border(), r);
				_borders.Add(border);
				_canvas.Children.Add(border);
				Canvas.SetLeft(border, 0);
				Canvas.SetBottom(border, 0);
				Console.WriteLine(r.NextDouble());
			}
			Storyboard _sb = new Storyboard() { Name = "name_1" };
			_borders.ForEach(a => {
				SetAnimation(a, r, _sb);
			});
			keyValuePairs.Add(_sb.Name, _borders);
			_sb.Completed += _sb_Completed;
			if (IsStart)
				_sb.Begin();
			weaktimer = new WeakReference(new DispatcherTimer());
			var timer = weaktimer.Target as DispatcherTimer;
			timer.Tick += timer_Tick;
			timer.Interval = TimeSpan.FromMilliseconds(500);
			if(IsStart)
				timer.Start();
		}

		private void _sb_Completed(object sender, EventArgs e)
		{
			ClockGroup cg = sender as ClockGroup;
			if (keyValuePairs.ContainsKey(cg.Timeline.Name))
            {
				foreach (var item in keyValuePairs[cg.Timeline.Name])
				{
					_canvas.Children.Remove(item);
				}
				keyValuePairs.Remove(cg.Timeline.Name);
			}
		}


		private void timer_Tick(object sender, EventArgs e)
		{
			count++;
			List<Border> _borders = new List<Border>();
			Random r = new Random();
			for (int i = 0; i < 5; i++)
			{
				var border = SetParticle(new Border(), r);
				_borders.Add(border);
				_canvas.Children.Add(border);
				Canvas.SetLeft(border, 0);
				Canvas.SetBottom(border, 0);
			}
			Storyboard _sb = new Storyboard() { Name = $"name_{count}" };
			_borders.ForEach(a => {
				SetAnimation(a, r, _sb);
			});
			keyValuePairs.Add(_sb.Name, _borders);
			_sb.Completed += _sb_Completed;
			_sb.Begin();
			Console.WriteLine("定时器");
		}



		public void SetAnimation(FrameworkElement control, Random r, Storyboard sb)
		{
			DoubleAnimation doubleAnimationBottom = new DoubleAnimation();
			doubleAnimationBottom.From = 0;
			doubleAnimationBottom.To = 180;

			var num = r.NextDouble();
			double life = 2000 + num * 1500;
			doubleAnimationBottom.Duration = new Duration(TimeSpan.FromMilliseconds(life));
			Storyboard.SetTarget(doubleAnimationBottom, control);
			Storyboard.SetTargetProperty(doubleAnimationBottom, new PropertyPath("(Canvas.Bottom)"));


			num = r.NextDouble();

			DoubleAnimation doubleAnimationLeft = new DoubleAnimation();
			double fromWidth = this.ActualWidth / 2;
			doubleAnimationLeft.From = 30+fromWidth * num;
			doubleAnimationLeft.To = (fromWidth-60) + num * (fromWidth + 60);

			num = r.NextDouble();
			life = 2000 + num * 1200;
			doubleAnimationLeft.Duration = new Duration(TimeSpan.FromMilliseconds(life));

			Storyboard.SetTarget(doubleAnimationLeft, control);
			Storyboard.SetTargetProperty(doubleAnimationLeft, new PropertyPath("(Canvas.Left)"));


			num = r.NextDouble();
			life = 2000+num * 1500;
			DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
			doubleAnimationHeight.From = control.Height;
			doubleAnimationHeight.To = 0;
			doubleAnimationHeight.Duration = new Duration(TimeSpan.FromMilliseconds(life));
			Storyboard.SetTarget(doubleAnimationHeight, control);
			Storyboard.SetTargetProperty(doubleAnimationHeight, new PropertyPath("Height"));

			DoubleAnimation doubleAnimationWidth = new DoubleAnimation();
			doubleAnimationWidth.From = control.Width;
			doubleAnimationWidth.To = 0;
			doubleAnimationWidth.Duration = new Duration(TimeSpan.FromMilliseconds(life));
			Storyboard.SetTarget(doubleAnimationWidth, control);
			Storyboard.SetTargetProperty(doubleAnimationWidth, new PropertyPath("Width"));

			DoubleAnimation doubleAnimationOpacity = new DoubleAnimation();
			doubleAnimationOpacity.From = 1;
			doubleAnimationOpacity.To = 0;
			doubleAnimationOpacity.Duration = new Duration(TimeSpan.FromMilliseconds(life));
			Storyboard.SetTarget(doubleAnimationOpacity, control);
			Storyboard.SetTargetProperty(doubleAnimationOpacity, new PropertyPath("Opacity"));

			sb.Children.Add(doubleAnimationBottom);
			sb.Children.Add(doubleAnimationLeft);
			sb.Children.Add(doubleAnimationHeight);
			sb.Children.Add(doubleAnimationWidth);
			sb.Children.Add(doubleAnimationOpacity);
		}

		public Border SetParticle(Border border, Random ran)
		{
			double RandKey = ran.NextDouble();
			double radius = 10 + RandKey * 20;

			//colors
			RandKey = ran.NextDouble();
			byte a = (byte)(RandKey * 255);

			RandKey = ran.NextDouble();
			byte r = (byte)(RandKey * 255);

			RandKey = ran.NextDouble();
			byte g = (byte)(RandKey * 255);

			RandKey = ran.NextDouble();
			byte b = (byte)(RandKey * 255);

			border.Width = radius;
			border.Height = radius;
			border.CornerRadius = new CornerRadius(radius / 2);
			//border.Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
			var p = new Path();
			var pathGeometry = (PathGeometry)this.FindResource("PathMusic");
			p.Fill = new SolidColorBrush(Color.FromArgb(a, r, g, b));
			p.Stretch = Stretch.Fill;
			p.Data = pathGeometry;
			border.Child = p;
			return border;
		}
	}
}
