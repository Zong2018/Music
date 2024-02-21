using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Music.Models
{
    public interface ITheme
    {
        Color Level1Foreground { get; set; }
        Color Level2Foreground { get; set; }
        Color Level3Foreground { get; set; }
        Color EnableForeground { get; set; }
        Color ValidationError { get; set; }
        Color DepthBackground { get; set; }
        Color Background { get; set; }
        Color Paper { get; set; }
        Color ToolBarBackground { get; set; }
        Color Body { get; set; }
        Color SecondBody { get; set; }
        Color ThridBody { get; set; }
        Color BodyLight { get; set; }
        Color ColumnHeader { get; set; }

        Color TabItemSelectedMark { get; set; }

        Color DataGridRowAlternationIndex0 { get; set; }
        Color DataGridRowAlternationIndex1 { get; set; }
        Color DataGridRowHoverBackground { get; set; }
        Color DataGridRowSelectedBackground { get; set; }

        Color TreeViewItemHoverBackground { get; set; }
        Color TreeViewItemSelectedBackground { get; set; }
        Color ButtonPlayBackground { get; set; }
        Color ButtonPlayHoverBackground { get; set; }
        Color CheckBoxOff { get; set; }
        Color CheckBoxDisabled { get; set; }

        Color Divider { get; set; }
        Color Selection { get; set; }

        Color ToolForeground { get; set; }
        Color ToolMouseHoverForeground { get; set; }
        Color ToolMousePressedForeground { get; set; }
        Color ToolBackground { get; set; }

        Color FlatButtonClick { get; set; }
        Color FlatButtonRipple { get; set; }

        Color ToolTipBackground { get; set; }
        Color ChipBackground { get; set; }

        Color SnackbarBackground { get; set; }
        Color SnackbarMouseOver { get; set; }
        Color SnackbarRipple { get; set; }

        Color TextBoxBorder { get; set; }

        Color TextBoxTipForeground { get; set; }

        Color TextFieldBoxBackground { get; set; }
        Color TextFieldBoxHoverBackground { get; set; }
        Color TextFieldBoxDisabledBackground { get; set; }
        Color TextAreaBorder { get; set; }
        Color TextAreaInactiveBorder { get; set; }
        Color MainBorderBrush { get; set; }
        LinearGradientBrush MainLinearGradientBrush {get;set;}
    }
}
