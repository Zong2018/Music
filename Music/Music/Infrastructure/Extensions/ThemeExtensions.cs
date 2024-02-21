using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Extensions
{
    public static class ThemeExtensions
    {

        public static void SetBaseTheme(this ITheme theme, IBaseTheme baseTheme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));
            if (baseTheme is null) throw new ArgumentNullException(nameof(baseTheme));

            theme.ValidationError = baseTheme.MusicDesignValidationErrorColor;
            theme.DepthBackground = baseTheme.MusicDesignDepthBackground;
            theme.Background = baseTheme.MusicDesignBackground;
            theme.Paper = baseTheme.MusicDesignPaper;
            theme.ToolBarBackground = baseTheme.MusicDesignToolBarBackground;
            theme.Body = baseTheme.MusicDesignBody;
            theme.BodyLight = baseTheme.MusicDesignBodyLight;
            theme.ColumnHeader = baseTheme.MusicDesignColumnHeader;
            theme.CheckBoxOff = baseTheme.MusicDesignCheckBoxOff;
            theme.CheckBoxDisabled = baseTheme.MusicDesignCheckBoxDisabled;
            theme.Divider = baseTheme.MusicDesignDivider;
            theme.Selection = baseTheme.MusicDesignSelection;
            theme.ToolForeground = baseTheme.MusicDesignToolForeground;
            theme.ToolBackground = baseTheme.MusicDesignToolBackground;
            theme.FlatButtonClick = baseTheme.MusicDesignFlatButtonClick;
            theme.FlatButtonRipple = baseTheme.MusicDesignFlatButtonRipple;
            theme.ToolTipBackground = baseTheme.MusicDesignToolTipBackground;
            theme.ChipBackground = baseTheme.MusicDesignChipBackground;
            theme.SnackbarBackground = baseTheme.MusicDesignSnackbarBackground;
            theme.SnackbarMouseOver = baseTheme.MusicDesignSnackbarMouseOver;
            theme.SnackbarRipple = baseTheme.MusicDesignSnackbarRipple;
            theme.TextBoxBorder = baseTheme.MusicDesignTextBoxBorder;
            theme.TextFieldBoxBackground = baseTheme.MusicDesignTextFieldBoxBackground;
            theme.TextFieldBoxHoverBackground = baseTheme.MusicDesignTextFieldBoxHoverBackground;
            theme.TextFieldBoxDisabledBackground = baseTheme.MusicDesignTextFieldBoxDisabledBackground;
            theme.TextAreaBorder = baseTheme.MusicDesignTextAreaBorder;
            theme.TextAreaInactiveBorder = baseTheme.MusicDesignTextAreaInactiveBorder;
            theme.DataGridRowHoverBackground = baseTheme.MusicDesignDataGridRowHoverBackground;
        }
    }
}
