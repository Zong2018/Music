using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Music.Models
{
    public interface IBaseTheme
    {
        Color MusicDesignValidationErrorColor { get; }
        Color MusicDesignDepthBackground { get; }
        Color MusicDesignBackground { get; }
        Color MusicDesignPaper { get; }
        Color MusicDesignCardBackground { get; }
        Color MusicDesignToolBarBackground { get; }
        Color MusicDesignBody { get; }
        Color MusicDesignBodyLight { get; }
        Color MusicDesignColumnHeader { get; }
        Color MusicDesignCheckBoxOff { get; }
        Color MusicDesignCheckBoxDisabled { get; }
        Color MusicDesignTextBoxBorder { get; }
        Color MusicDesignDivider { get; }
        Color MusicDesignSelection { get; }
        Color MusicDesignToolForeground { get; }
        Color MusicDesignToolBackground { get; }
        Color MusicDesignFlatButtonClick { get; }
        Color MusicDesignFlatButtonRipple { get; }
        Color MusicDesignToolTipBackground { get; }
        Color MusicDesignChipBackground { get; }
        Color MusicDesignSnackbarBackground { get; }
        Color MusicDesignSnackbarMouseOver { get; }
        Color MusicDesignSnackbarRipple { get; }
        Color MusicDesignTextFieldBoxBackground { get; }
        Color MusicDesignTextFieldBoxHoverBackground { get; }
        Color MusicDesignTextFieldBoxDisabledBackground { get; }
        Color MusicDesignTextAreaBorder { get; }
        Color MusicDesignTextAreaInactiveBorder { get; }
        Color MusicDesignDataGridRowHoverBackground { get; }
    }
}
