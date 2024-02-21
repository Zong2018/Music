using Music.ControlAssist;
using Music.Infrastructure.Extensions;
using Music.Infrastructure.Manager;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace Music.Controls
{
    /// <summary>
    /// 颜色调板
    /// </summary>
    [TemplatePart(Name = PART_ItemsControlPartName, Type = typeof(ColorPalette))]
    [TemplatePart(Name = PART_SliderPartName, Type = typeof(ColorPalette))]
    [TemplatePart(Name = PART_DepthColorSliderPartName, Type = typeof(ColorPalette))]
    public class ColorPalette: Control
    {
        public const string PART_ItemsControlPartName = "PART_ItemsControl";
        public const string PART_SliderPartName = "PART_Slider";
        public const string PART_DepthColorSliderPartName = "PART_DepthColorSlider";

        private ItemsControl _itemsControlColor { get; set; }
        private Slider _sliderColor { get; set; }

        private Slider _depthColorSlider { get; set; }

        bool _isCheckSliderColor = false;

        double _colorDeptProportion = 0;

        List<RadioButton> _radioButtons;
        public List<RadioButton> RadioButtons { get { return _radioButtons; } }

        /// <summary>
        ///     颜色范围集合
        /// </summary>
        private readonly List<ColorRange> _colorRangeList = new List<ColorRange>()
        {
            new ColorRange
            {
                Start = Color.FromRgb(255, 0, 0),
                End = Color.FromRgb(255,255,0)
            },
            new ColorRange
            {
                Start = Color.FromRgb(255,255,0),
                End = Color.FromRgb(0,255,0)
            },
            new ColorRange
            {
                Start = Color.FromRgb(0,255,0),
                End = Color.FromRgb(0,255,255)
            },
            new ColorRange
            {
                Start = Color.FromRgb(0, 255, 255),
                End = Color.FromRgb(0,0,255)
            },
            new ColorRange
            {
                Start = Color.FromRgb(0,0,255),
                End = Color.FromRgb(255,0,255)
            },
            new ColorRange
            {
                Start = Color.FromRgb(255,0,255),
                End = Color.FromRgb(255, 0, 0)
            }
        };

        static ColorPalette()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPalette), new FrameworkPropertyMetadata(typeof(ColorPalette)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _itemsControlColor = this.GetTemplateChild(PART_ItemsControlPartName) as ItemsControl;
            _sliderColor = this.GetTemplateChild(PART_SliderPartName) as Slider;
            _depthColorSlider = this.GetTemplateChild(PART_DepthColorSliderPartName) as Slider;

            if (_itemsControlColor != null && _itemsControlColor.Items != null && _itemsControlColor.Items.Count>0)
            {
                _radioButtons = this.VisualTreeDepthFirstTraversal().OfType<RadioButton>().ToList();
                _radioButtons.AddRange(_itemsControlColor.Items.Cast<RadioButton>());
                foreach (var rb in _radioButtons)
                {
                    rb.Checked += Rb_Checked;
                }
            }

            if (_sliderColor != null)
            {
                _sliderColor.ValueChanged += _sliderColor_ValueChanged;
                _sliderColor.Value = _sliderColor.Maximum / 2;
            }

            if (_depthColorSlider != null)
            {
                _depthColorSlider.ValueChanged += _depthColorSliderColor_ValueChanged;
                _depthColorSlider.Value = _depthColorSlider.Maximum;
            }

            SetSelectedItem();
        }

        private void _depthColorSliderColor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        { 
            var slider = sender as Slider;
            if(slider != null && _isCheckSliderColor)
            {
                _colorDeptProportion = 1 - e.NewValue / slider.Maximum;

                var colorTemp = (Color)ColorConverter.ConvertFromString(CustomerCurrentColor?.ToString()??Brushes.Pink.ToString());
                var newColor = GetDarkColor(colorTemp, _colorDeptProportion);
                SettingManager.GlobalSetting.ThemeColorTraSliderValue = _depthColorSlider.Value;
                SelectedItem = new SolidColorBrush(newColor);
            }
        }

        static public Color GetDarkColor(Color color, double level)
        {
            Color depthColor = new Color();
            depthColor.A = color.A;
            depthColor.R = (byte)Math.Floor(color.R * (1 - level));
            depthColor.G = (byte)Math.Floor(color.G * (1 - level));
            depthColor.B = (byte)Math.Floor(color.B * (1 - level));
            return depthColor;
        }

        public bool IsSlider { get; private set; }

        public void SetSelectedItem()
        {

            var rb = _radioButtons.FirstOrDefault(a => a.Background.ToString().ToUpper() == SettingManager.GlobalSetting.ThemeColor.ToUpper());
            if (rb != null)
                rb.IsChecked = true;
            else
            {
                if(SettingManager.GlobalSetting.ThemeType == "自定义")
                {
                    rb = _radioButtons.FirstOrDefault(a => a.Tag != null && a.Tag.ToString() == "sliderRadioButton");
                    if (rb != null)
                    {
                        rb.IsChecked = true;
                        _sliderColor.Value = SettingManager.GlobalSetting.ThemeColorSliderValue;
                        _depthColorSlider.Value = SettingManager.GlobalSetting.ThemeColorTraSliderValue;
                    }
                }
            }
        }

        private void Rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if(rb != null)
            {
                var color = (Color)ColorConverter.ConvertFromString(rb.Background.ToString());

                var tag = rb.Tag?.ToString();
                if (string.IsNullOrWhiteSpace(tag))
                {
                    _isCheckSliderColor = false;
                    SelectedItem = rb.Background;
                    IsSlider = false;
                    RoutedEventArgs args = new RoutedEventArgs(SelectionChangedRoutedEvent, this);
                    this.RaiseEvent(args);//UIElement及其派生类          
                }
                else
                {
                    _isCheckSliderColor = true;
                    IsSlider = true;
                    LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
                    GradientStop gradientStop1 = new GradientStop(GetDarkColor(color, 0.475), 0.25);
                    GradientStop gradientStop2 = new GradientStop(GetDarkColor(color,0.35), 0.5);
                    GradientStop gradientStop3 = new GradientStop(GetDarkColor(color, 0.225), 0.75);
                    GradientStop gradientStop4 = new GradientStop(color, 1);
                    linearGradientBrush.GradientStops.Add(gradientStop1);
                    linearGradientBrush.GradientStops.Add(gradientStop2);
                    linearGradientBrush.GradientStops.Add(gradientStop3);
                    linearGradientBrush.GradientStops.Add(gradientStop4);
                    ColorPaletteAssist.SetBarBrush(_depthColorSlider, linearGradientBrush);
                    var newColor = GetDarkColor(color, _colorDeptProportion);
                    SelectedItem = new SolidColorBrush(newColor);
                }
           
            }
        }

        private void _sliderColor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var index = Math.Min(5, (int)Math.Floor(e.NewValue));
            var sub = e.NewValue - index;
            var range = _colorRangeList[index];

            var color = range.GetColor(sub);
            CustomerCurrentColor = new SolidColorBrush(color);
            if (_isCheckSliderColor)
            {
                var colorTemp = (Color)ColorConverter.ConvertFromString(CustomerCurrentColor.ToString());
                LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
                GradientStop gradientStop1 = new GradientStop(GetDarkColor(colorTemp, 0.475), 0.25);
                GradientStop gradientStop2 = new GradientStop(GetDarkColor(colorTemp, 0.35), 0.5);
                GradientStop gradientStop3 = new GradientStop(GetDarkColor(colorTemp, 0.225), 0.75);
                GradientStop gradientStop4 = new GradientStop(color, 1);
                linearGradientBrush.GradientStops.Add(gradientStop1);
                linearGradientBrush.GradientStops.Add(gradientStop2);
                linearGradientBrush.GradientStops.Add(gradientStop3);
                linearGradientBrush.GradientStops.Add(gradientStop4);
                ColorPaletteAssist.SetBarBrush(_depthColorSlider, linearGradientBrush);
             
                var newColor = GetDarkColor(colorTemp, _colorDeptProportion);
                SettingManager.GlobalSetting.ThemeColorSliderValue = _sliderColor.Value;
                SelectedItem = new SolidColorBrush(newColor);
            }
        }

        /// <summary>
        ///     当前选中的颜色
        /// </summary>
        public static SolidColorBrush GetSelectedBrush(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(SelectedBrushProperty);
        }

        public static void SetSelectedBrush(DependencyObject obj, SolidColorBrush value)
        {
            obj.SetValue(SelectedBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.RegisterAttached("SelectedBrush", typeof(SolidColorBrush), typeof(ColorPalette), new PropertyMetadata(Brushes.White));



        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ColorPalette), new PropertyMetadata(null,new PropertyChangedCallback(SelectedItemPropertyChangedCallback)));

        static void SelectedItemPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorPalette palette = d as ColorPalette;
            if (palette != null && palette._isCheckSliderColor && e.NewValue != null)
            {
                RoutedEventArgs args = new RoutedEventArgs(SelectionChangedRoutedEvent, palette);
                palette.RaiseEvent(args);//UIElement及其派生类            
            }
        }

        public SolidColorBrush CustomerCurrentColor
        {
            get { return (SolidColorBrush)GetValue(CustomerCurrentColorProperty); }
            set { SetValue(CustomerCurrentColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomerCurrentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomerCurrentColorProperty =
            DependencyProperty.Register("CustomerCurrentColor", typeof(SolidColorBrush), typeof(ColorPalette), new PropertyMetadata(Brushes.Pink));



        /// <summary>
        /// 是否显示颜色拖动条
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsShowSlider(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsShowSliderProperty);
        }

        public static void SetIsShowSlider(DependencyObject obj, bool value)
        {
            obj.SetValue(IsShowSliderProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsShowSlider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowSliderProperty =
            DependencyProperty.RegisterAttached("IsShowSlider", typeof(bool), typeof(ColorPalette), new PropertyMetadata(true));


		public bool IsClearChecked
		{
			get { return (bool)GetValue(IsClearCheckedProperty); }
			set { SetValue(IsClearCheckedProperty, value); }
		}

		// Using a DependencyProperty as the backing store for IsClearChecked.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IsClearCheckedProperty =
			DependencyProperty.Register("IsClearChecked", typeof(bool), typeof(ColorPalette), new PropertyMetadata(false,new PropertyChangedCallback(IsClearCheckedPropertyChangedCallback)));

		static void IsClearCheckedPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var cp = d as ColorPalette;
			if ((bool)e.NewValue && cp != null && cp._itemsControlColor != null && cp._itemsControlColor.Items != null && cp._itemsControlColor.Items.Count > 0)
			{
				List<RadioButton> radioButtons = cp.VisualTreeDepthFirstTraversal().OfType<RadioButton>().ToList();
				radioButtons.AddRange(cp._itemsControlColor.Items.Cast<RadioButton>());
				foreach (var rb in radioButtons)
				{
					rb.IsChecked = false;
				}
			}
		}


		/// <summary>
		/// 选择改变事件
		/// </summary>
		public static readonly RoutedEvent SelectionChangedRoutedEvent =
          EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ColorPalette));
        //CLR事件包装
        public event RoutedEventHandler SelectionChanged
        {
            add { this.AddHandler(SelectionChangedRoutedEvent, value); }
            remove { this.RemoveHandler(SelectionChangedRoutedEvent, value); }
        }
    }
}
