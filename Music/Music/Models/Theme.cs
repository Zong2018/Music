using Music.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Music.Models
{
    public class Theme : ITheme
    {
        /// <summary>
        /// 获取
        /// Based on ControlzEx
        /// </summary>
        /// <returns></returns>

        public static Theme Create(IBaseTheme baseTheme)
        {
            if (baseTheme is null) throw new ArgumentNullException(nameof(baseTheme));
            var theme = new Theme();
            theme.SetBaseTheme(baseTheme);

            return theme;
        }
		public Color Level1Foreground { get; set; }
		public Color Level2Foreground { get; set; }
		public Color Level3Foreground { get; set; }
		public Color EnableForeground { get; set; }

		public Color ValidationError { get; set; }
        public Color DepthBackground { get; set; }
        public Color Background { get; set; }
        public Color Paper { get; set; }
        public Color CardBackground { get; set; }
        public Color ToolBarBackground { get; set; }
        public Color Body { get; set; }
		public Color SecondBody { get; set; }
		public Color ThridBody { get; set; }
		public Color BodyLight { get; set; }
        public Color ColumnHeader { get; set; }
		public Color TabItemSelectedMark { get; set; }
        public Color DataGridRowAlternationIndex0 { get; set; }
        public Color DataGridRowAlternationIndex1 { get; set; }
        public Color DataGridRowHoverBackground { get; set; }
        public Color DataGridRowSelectedBackground { get; set; }
        public Color TreeViewItemHoverBackground { get; set; }
        public Color TreeViewItemSelectedBackground { get; set; }

        public Color ButtonPlayBackground { get; set; }
        public Color ButtonPlayHoverBackground { get; set; }

        public Color CheckBoxOff { get; set; }
        public Color CheckBoxDisabled { get; set; }
        public Color Divider { get; set; }
        public Color Selection { get; set; }
        public Color ToolForeground { get; set; }
		public Color ToolMouseHoverForeground { get; set; }
		public Color ToolMousePressedForeground { get; set; }
		public Color ToolBackground { get; set; }
        public Color FlatButtonClick { get; set; }
        public Color FlatButtonRipple { get; set; }
        public Color ToolTipBackground { get; set; }
        public Color ChipBackground { get; set; }
        public Color SnackbarBackground { get; set; }
        public Color SnackbarMouseOver { get; set; }
        public Color SnackbarRipple { get; set; }
        public Color TextBoxBorder { get; set; }
        public Color TextBoxTipForeground { get; set; }
        public Color TextFieldBoxBackground { get; set; }
        public Color TextFieldBoxHoverBackground { get; set; }
        public Color TextFieldBoxDisabledBackground { get; set; }
        public Color TextAreaBorder { get; set; }
        public Color TextAreaInactiveBorder { get; set; }
        public Color MainBorderBrush { get; set; }
        public LinearGradientBrush MainLinearGradientBrush { get; set; }
    }
}
