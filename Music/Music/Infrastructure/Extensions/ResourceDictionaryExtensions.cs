using Music.Controls.EventArg;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Music.Extensions
{
    public static class ResourceDictionaryExtensions
    {
        private const string CurrentThemeKey = nameof(Music) + "." + nameof(CurrentThemeKey);
        private const string ThemeManagerKey = nameof(Music) + "." + nameof(ThemeManagerKey);

        public static void SetTheme(this ResourceDictionary resourceDictionary, ITheme theme)
        {
            SetTheme(resourceDictionary, theme, null);
        }

        public static void SetTheme(this ResourceDictionary resourceDictionary, ITheme theme, object obj)
        {
            if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
            string bk = theme.Background.ToString();
            theme.DepthBackground = GetDarkColor(theme.Background, 0.05);
            if (bk == "#FFFFFFFF")
            {
                theme.Background = GetDarkColor(theme.Background, 0.03);
                theme.TextBoxTipForeground = GetDarkColor(theme.Background, 0.2);
                theme.ToolForeground = Color.FromRgb(16,16,16);
				theme.ToolMouseHoverForeground = (Color)ColorConverter.ConvertFromString("#CC000000");
				theme.ToolMousePressedForeground = (Color)ColorConverter.ConvertFromString("#EE000000");

				theme.Body = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
				theme.SecondBody = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");

				theme.TabItemSelectedMark = (Color)ColorConverter.ConvertFromString("#FFec4141");

				theme.Level1Foreground = (Color)ColorConverter.ConvertFromString("#FF333333");
				theme.Level2Foreground = (Color)ColorConverter.ConvertFromString("#FF666666");
				theme.Level3Foreground = (Color)ColorConverter.ConvertFromString("#FF999999");
				theme.EnableForeground = (Color)ColorConverter.ConvertFromString("#CC999999");

				theme.TreeViewItemHoverBackground = (Color)ColorConverter.ConvertFromString("#FFF6F6F6");
				theme.TreeViewItemSelectedBackground = (Color)ColorConverter.ConvertFromString("#FFF6F6F6");

                theme.DataGridRowAlternationIndex0 = (Color)ColorConverter.ConvertFromString("#FFF9F9F9");
                theme.DataGridRowAlternationIndex1 = (Color)ColorConverter.ConvertFromString("#FFFFFFFF"); 
                theme.DataGridRowHoverBackground = (Color)ColorConverter.ConvertFromString("#FFF6F6F6");
                theme.DataGridRowSelectedBackground = (Color)ColorConverter.ConvertFromString("#FFE5E5E5");

                theme.ButtonPlayBackground = (Color)ColorConverter.ConvertFromString("#FFF4F4F4");
                theme.ButtonPlayHoverBackground = (Color)ColorConverter.ConvertFromString("#FFE5E5E5");

                theme.MainBorderBrush = (Color)ColorConverter.ConvertFromString("#FFE8E8E8");
            }
			else if (bk == "#FF232326")
			{
				theme.TextBoxTipForeground = (Color)ColorConverter.ConvertFromString("#99FFFFFF");
				theme.ToolForeground = (Color)ColorConverter.ConvertFromString("#AAFFFFFF");
				theme.ToolMouseHoverForeground = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
				theme.ToolMousePressedForeground = (Color)ColorConverter.ConvertFromString("#FFFFFF");

				theme.Body = (Color)ColorConverter.ConvertFromString("#FF363636");
				theme.SecondBody = (Color)ColorConverter.ConvertFromString("#FF2b2b2b");
				theme.TabItemSelectedMark = (Color)ColorConverter.ConvertFromString("#FFec4141");

				theme.Level1Foreground = (Color)ColorConverter.ConvertFromString("#FFCCCCCC");
				theme.Level2Foreground = (Color)ColorConverter.ConvertFromString("#FFD0D0D0");
				theme.Level3Foreground = (Color)ColorConverter.ConvertFromString("#FF7C7C7C");
				theme.EnableForeground = (Color)ColorConverter.ConvertFromString("#FF7C7C7C");

				theme.TreeViewItemHoverBackground = (Color)ColorConverter.ConvertFromString("#FF333333");
				theme.TreeViewItemSelectedBackground = (Color)ColorConverter.ConvertFromString("#FF333333");

                theme.DataGridRowAlternationIndex0 = (Color)ColorConverter.ConvertFromString("#FF2B2B2B");
                theme.DataGridRowAlternationIndex1 = (Color)ColorConverter.ConvertFromString("#FF2E2E2E");
                theme.DataGridRowHoverBackground = (Color)ColorConverter.ConvertFromString("#FF333333");
                theme.DataGridRowSelectedBackground = (Color)ColorConverter.ConvertFromString("#FF373737");

                theme.ButtonPlayBackground = (Color)ColorConverter.ConvertFromString("#FF4A4A4D");
                theme.ButtonPlayHoverBackground = (Color)ColorConverter.ConvertFromString("#FF464646");

                theme.MainBorderBrush = (Color)ColorConverter.ConvertFromString("#FF444444");

                theme.DepthBackground = (Color)ColorConverter.ConvertFromString("#FF2B2B2E");
            }
            else
            {
                theme.TextBoxTipForeground = (Color)ColorConverter.ConvertFromString("#99FFFFFF");
                theme.ToolForeground = (Color)ColorConverter.ConvertFromString("#AAFFFFFF");
				theme.ToolMouseHoverForeground = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
				theme.ToolMousePressedForeground = (Color)ColorConverter.ConvertFromString("#FFFFFF");

				theme.Body = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
				theme.SecondBody = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");

				theme.TabItemSelectedMark = theme.Background;

				theme.Level1Foreground = (Color)ColorConverter.ConvertFromString("#FF333333");
				theme.Level2Foreground = (Color)ColorConverter.ConvertFromString("#FF666666");
				theme.Level3Foreground = (Color)ColorConverter.ConvertFromString("#FF999999");
				theme.EnableForeground = (Color)ColorConverter.ConvertFromString("#CC999999");

				theme.TreeViewItemHoverBackground = (Color)ColorConverter.ConvertFromString("#FFF6F6F6");
				theme.TreeViewItemSelectedBackground = (Color)ColorConverter.ConvertFromString("#FFF6F6F6");

                theme.DataGridRowAlternationIndex0 = (Color)ColorConverter.ConvertFromString("#FFF9F9F9");
                theme.DataGridRowAlternationIndex1 = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
                theme.DataGridRowHoverBackground = (Color)ColorConverter.ConvertFromString("#FFF6F6F6");
                theme.DataGridRowSelectedBackground = (Color)ColorConverter.ConvertFromString("#FFE5E5E5");

                theme.ButtonPlayBackground = (Color)ColorConverter.ConvertFromString("#FFF4F4F4");
                theme.ButtonPlayHoverBackground = (Color)ColorConverter.ConvertFromString("#FFE5E5E5");

                theme.MainBorderBrush = (Color)ColorConverter.ConvertFromString("#FFE8E8E8");
            }

            theme.MainLinearGradientBrush = new LinearGradientBrush() { StartPoint=new Point(0.5,0),EndPoint=new Point(0.5,1), GradientStops=new GradientStopCollection() { 
               new GradientStop((Color)ColorConverter.ConvertFromString("#FFFFFFFF"),0),
               new GradientStop(theme.TabItemSelectedMark,1)
            } };

            SetLinearGradientBrush(resourceDictionary, "MusicDesignMainLinearGradientBrush", theme.MainLinearGradientBrush);

            SetSolidColorBrush(resourceDictionary, "MusicDesignLevel1Foreground", theme.Level1Foreground);
			SetSolidColorBrush(resourceDictionary, "MusicDesignLevel2Foreground", theme.Level2Foreground);
			SetSolidColorBrush(resourceDictionary, "MusicDesignLevel3Foreground", theme.Level3Foreground);
			SetSolidColorBrush(resourceDictionary, "MusicDesignEnableForeground", theme.EnableForeground);
			SetSolidColorBrush(resourceDictionary, "MusicDesignValidationErrorBrush", theme.ValidationError);
            resourceDictionary["MusicDesignValidationErrorColor"] = theme.ValidationError;
            SetSolidColorBrush(resourceDictionary, "MusicDesignDepthBackground", theme.DepthBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignBackground", theme.Background);
            SetSolidColorBrush(resourceDictionary, "MusicDesignPaper", theme.Paper);
            //SetSolidColorBrush(resourceDictionary, "MusicDesignCardBackground", theme.CardBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignToolBarBackground", theme.ToolBarBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignBody", theme.Body);
			SetSolidColorBrush(resourceDictionary, "MusicDesignSecondBody", theme.SecondBody);
			SetSolidColorBrush(resourceDictionary, "MusicDesignThridBody", theme.ThridBody);
			SetSolidColorBrush(resourceDictionary, "MusicDesignBodyLight", theme.BodyLight);
            SetSolidColorBrush(resourceDictionary, "MusicDesignColumnHeader", theme.ColumnHeader);
			SetSolidColorBrush(resourceDictionary, "MusicDesignTabItemSelectedMark", theme.TabItemSelectedMark);
			SetSolidColorBrush(resourceDictionary, "MusicDesignTreeViewItemHoverBackground", theme.TreeViewItemHoverBackground);
			SetSolidColorBrush(resourceDictionary, "MusicDesignTreeViewItemSelectedBackground", theme.TreeViewItemSelectedBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignDataGridRowAlternationIndex0", theme.DataGridRowAlternationIndex0);
            SetSolidColorBrush(resourceDictionary, "MusicDesignDataGridRowAlternationIndex1", theme.DataGridRowAlternationIndex1);
            SetSolidColorBrush(resourceDictionary, "MusicDesignDataGridRowHoverBackground", theme.DataGridRowHoverBackground); 
            SetSolidColorBrush(resourceDictionary, "MusicDesignDataGridRowSelectedBackground", theme.DataGridRowSelectedBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignButtonPlayBackground", theme.ButtonPlayBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignButtonPlayHoverBackground", theme.ButtonPlayHoverBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignCheckBoxOff", theme.CheckBoxOff);
            SetSolidColorBrush(resourceDictionary, "MusicDesignCheckBoxDisabled", theme.CheckBoxDisabled);
            SetSolidColorBrush(resourceDictionary, "MusicDesignTextBoxBorder", theme.TextBoxBorder);
            SetSolidColorBrush(resourceDictionary, "MusicDesignTextBoxTipForeground", theme.TextBoxTipForeground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignDivider", theme.Divider);
            SetSolidColorBrush(resourceDictionary, "MusicDesignSelection", theme.Selection);
            SetSolidColorBrush(resourceDictionary, "MusicDesignToolForeground", theme.ToolForeground);
			SetSolidColorBrush(resourceDictionary, "MusicDesignToolMouseHoverForeground", theme.ToolMouseHoverForeground);
			SetSolidColorBrush(resourceDictionary, "MusicDesignToolMousePressedForeground", theme.ToolMousePressedForeground);
			SetSolidColorBrush(resourceDictionary, "MusicDesignToolBackground", theme.ToolBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignFlatButtonClick", theme.FlatButtonClick);
            SetSolidColorBrush(resourceDictionary, "MusicDesignFlatButtonRipple", theme.FlatButtonRipple);
            SetSolidColorBrush(resourceDictionary, "MusicDesignToolTipBackground", theme.ToolTipBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignChipBackground", theme.ChipBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignSnackbarBackground", theme.SnackbarBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignSnackbarMouseOver", theme.SnackbarMouseOver);
            SetSolidColorBrush(resourceDictionary, "MusicDesignSnackbarRipple", theme.SnackbarRipple);
            SetSolidColorBrush(resourceDictionary, "MusicDesignTextFieldBoxBackground", theme.TextFieldBoxBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignTextFieldBoxHoverBackground", theme.TextFieldBoxHoverBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignTextFieldBoxDisabledBackground", theme.TextFieldBoxDisabledBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignTextAreaBorder", theme.TextAreaBorder);
            SetSolidColorBrush(resourceDictionary, "MusicDesignTextAreaInactiveBorder", theme.TextAreaInactiveBorder);
            SetSolidColorBrush(resourceDictionary, "MusicDesignDataGridRowHoverBackground", theme.DataGridRowHoverBackground);
            SetSolidColorBrush(resourceDictionary, "MusicDesignMainBorderBrush", theme.MainBorderBrush);
            if (!(resourceDictionary.GetThemeManager() is ThemeManager themeManager))
            {
                resourceDictionary[ThemeManagerKey] = themeManager = new ThemeManager(resourceDictionary);
            }
            ITheme oldTheme = resourceDictionary.GetTheme();
            resourceDictionary[CurrentThemeKey] = theme;

            //themeManager.OnThemeChange(oldTheme, theme);
        }

        public static ITheme GetTheme(this ResourceDictionary resourceDictionary)
        {
            if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
            if (resourceDictionary[CurrentThemeKey] is ITheme theme)
            {
                return theme;
            }


            //Attempt to simply look up the appropriate resources
            return new Theme
            {

                Background = GetColor("MusicDesignBackground"),
                Body = GetColor("MusicDesignBody"),
                BodyLight = GetColor("MusicDesignBodyLight"),
                CardBackground = GetColor("MusicDesignCardBackground"),
                CheckBoxDisabled = GetColor("MusicDesignCheckBoxDisabled"),
                CheckBoxOff = GetColor("MusicDesignCheckBoxOff"),
                ChipBackground = GetColor("MusicDesignChipBackground"),
                ColumnHeader = GetColor("MusicDesignColumnHeader"),
                DataGridRowHoverBackground = GetColor("MusicDesignDataGridRowHoverBackground"),
                Divider = GetColor("MusicDesignDivider"),
                FlatButtonClick = GetColor("MusicDesignFlatButtonClick"),
                FlatButtonRipple = GetColor("MusicDesignFlatButtonRipple"),
                Selection = GetColor("MusicDesignSelection"),
                SnackbarBackground = GetColor("MusicDesignSnackbarBackground"),
                SnackbarMouseOver = GetColor("MusicDesignSnackbarMouseOver"),
                SnackbarRipple = GetColor("MusicDesignSnackbarRipple"),
                TextAreaBorder = GetColor("MusicDesignTextAreaBorder"),
                TextAreaInactiveBorder = GetColor("MusicDesignTextAreaInactiveBorder"),
                TextBoxBorder = GetColor("MusicDesignTextBoxBorder"),
                TextBoxTipForeground = GetColor("MusicDesignTextBoxTipForeground"),
                TextFieldBoxBackground = GetColor("MusicDesignTextFieldBoxBackground"),
                TextFieldBoxDisabledBackground = GetColor("MusicDesignTextFieldBoxDisabledBackground"),
                TextFieldBoxHoverBackground = GetColor("MusicDesignTextFieldBoxHoverBackground"),
                ToolBackground = GetColor("MusicDesignToolBackground"),
                ToolBarBackground = GetColor("MusicDesignToolBarBackground"),
                ToolForeground = GetColor("MusicDesignToolForeground"),
                ToolTipBackground = GetColor("MusicDesignToolTipBackground"),
                Paper = GetColor("MusicDesignPaper"),
                ValidationError = GetColor("MusicDesignValidationErrorBrush"),
                ToolMouseHoverForeground = GetColor("MusicDesignToolMouseHoverForeground"),
                ToolMousePressedForeground = GetColor("MusicDesignToolMousePressedForeground"),
                SecondBody = GetColor("MusicDesignSecondBody"),
                ThridBody = GetColor("MusicDesignThridBody"),
                TabItemSelectedMark = GetColor("MusicDesignTabItemSelectedMark"),
                Level1Foreground = GetColor("MusicDesignLevel1Foreground"),
                Level2Foreground = GetColor("MusicDesignLevel2Foreground"),
                Level3Foreground = GetColor("MusicDesignLevel3Foreground"),
                TreeViewItemHoverBackground = GetColor("MusicDesignTreeViewItemHoverBackground"),
                TreeViewItemSelectedBackground = GetColor("MusicDesignTreeViewItemSelectedBackground"),
                DataGridRowAlternationIndex0 = GetColor("MusicDesignDataGridRowAlternationIndex0"),
                DataGridRowAlternationIndex1 = GetColor("MusicDesignDataGridRowAlternationIndex1"),
                DataGridRowSelectedBackground = GetColor("MusicDesignDataGridRowSelectedBackground"),
                DepthBackground = GetColor("MusicDesignDepthBackground"),
                ButtonPlayHoverBackground = GetColor("MusicDesignButtonPlayHoverBackground"),
                ButtonPlayBackground = GetColor("MusicDesignButtonPlayBackground"),
                MainBorderBrush = GetColor("MusicDesignMainBorderBrush"),
                MainLinearGradientBrush = GetLinearGradientBrushColor("MusicDesignMainLinearGradientBrush")
            };

            Color GetColor(params string[] keys)
            {
                foreach (string key in keys)
                {
                    if (TryGetColor(key, out Color color))
                    {
                        return color;
                    }
                }
                throw new InvalidOperationException($"Could not locate required resource with key(s) '{string.Join(", ", keys)}'");
            }

            bool TryGetColor(string key, out Color color)
            {
                if (resourceDictionary[key] is SolidColorBrush brush)
                {
                    color = brush.Color;
                    return true;
                }
                color = default;
                return false;
            }

            LinearGradientBrush GetLinearGradientBrushColor(params string[] keys)
            {
                foreach (string key in keys)
                {
                    if (TryGetLinearGradientBrushColor(key, out LinearGradientBrush colorBrsh))
                    {
                        return colorBrsh;
                    }
                }
                throw new InvalidOperationException($"Could not locate required resource with key(s) '{string.Join(", ", keys)}'");
            }

            bool TryGetLinearGradientBrushColor(string key, out LinearGradientBrush colorBrsh)
            {
                if (resourceDictionary[key] is LinearGradientBrush brush)
                {
                    colorBrsh = brush;
                    return true;
                }
                colorBrsh = default;
                return false;
            }
        }

        static public Color GetDarkColor(Color color, double level)
        {
            Color depthColor = new Color();
            depthColor.A = color.A;
            depthColor.R = (byte) Math.Floor(color.R * (1 - level));
            depthColor.G = (byte)Math.Floor(color.G * (1 - level));
            depthColor.B = (byte)Math.Floor(color.B * (1 - level));
            return depthColor;
        }
        public static IThemeManager GetThemeManager(this ResourceDictionary resourceDictionary)
        {
            if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
            return resourceDictionary[ThemeManagerKey] as IThemeManager;
        }

        internal static void SetSolidColorBrush(this ResourceDictionary sourceDictionary, string name, Color value)
        {
            if (sourceDictionary is null) throw new ArgumentNullException(nameof(sourceDictionary));
            if (name is null) throw new ArgumentNullException(nameof(name));

            //sourceDictionary[name + "Color"] = value;

            if (sourceDictionary[name] is SolidColorBrush brush)
            {
                if (brush.Color == value) return;

                if (!brush.IsFrozen)
                {
                    var animation = new ColorAnimation
                    {
                        From = brush.Color,
                        To = value,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                    return;
                }
            }

            var newBrush = new SolidColorBrush(value);
            newBrush.Freeze();
            sourceDictionary[name] = newBrush;
        }

        internal static void SetLinearGradientBrush(this ResourceDictionary sourceDictionary, string name, LinearGradientBrush value)
        {
            if (sourceDictionary is null) throw new ArgumentNullException(nameof(sourceDictionary));
            if (name is null) throw new ArgumentNullException(nameof(name));

            value.Freeze();
            sourceDictionary[name] = value;
        }

        public interface IThemeManager
        {
            event EventHandler<ThemeChangedEventArgs> ThemeChanged;
        }

        private class ThemeManager : IThemeManager
        {
            private ResourceDictionary _ResourceDictionary;

            public ThemeManager(ResourceDictionary resourceDictionary)
                => _ResourceDictionary = resourceDictionary ?? throw new ArgumentNullException(nameof(resourceDictionary));

            public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

            public void OnThemeChange(ITheme oldTheme, ITheme newTheme)
                => ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(_ResourceDictionary, oldTheme, newTheme));
        }
    }
}
