using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Music.Models
{
   public  class MusicDesignBaseTheme : IBaseTheme
    {
        public Color MusicDesignValidationErrorColor { get{return (Color)ColorConverter.ConvertFromString("#FD544E");}}
        public Color MusicDesignDepthBackground { get { return (Color)ColorConverter.ConvertFromString("#0A000000"); } }
        public Color MusicDesignBackground { get{return (Color)ColorConverter.ConvertFromString("#FFFFFFFF");}}
        public Color MusicDesignPaper { get{return (Color)ColorConverter.ConvertFromString("#FFFAFAFA");}}
        public Color MusicDesignCardBackground { get{return (Color)ColorConverter.ConvertFromString("#FFFFFFFF");}}
        public Color MusicDesignToolBarBackground { get{return (Color)ColorConverter.ConvertFromString("#FD544E");}}
        public Color MusicDesignBody { get{return (Color)ColorConverter.ConvertFromString("#DD000000");}}
        public Color MusicDesignBodyLight { get{return (Color)ColorConverter.ConvertFromString("#89000000");}}
        public Color MusicDesignColumnHeader { get{return (Color)ColorConverter.ConvertFromString("#BC000000");}}
        public Color MusicDesignCheckBoxOff { get{return (Color)ColorConverter.ConvertFromString("#89000000");}}
        public Color MusicDesignCheckBoxDisabled { get{return (Color)ColorConverter.ConvertFromString("#FFBDBDBD");}}
        public Color MusicDesignTextBoxBorder { get{return (Color)ColorConverter.ConvertFromString("#89000000");}}
        public Color MusicDesignDivider { get{return (Color)ColorConverter.ConvertFromString("#1F000000");}}
        public Color MusicDesignSelection { get{return (Color)ColorConverter.ConvertFromString("#FFDEDEDE");}}
        public Color MusicDesignToolForeground { get{return (Color)ColorConverter.ConvertFromString("#FF616161");}}
        public Color MusicDesignToolBackground { get{return (Color)ColorConverter.ConvertFromString("#FFE0E0E0");}}
        public Color MusicDesignFlatButtonClick { get{return (Color)ColorConverter.ConvertFromString("#FFDEDEDE");}}
        public Color MusicDesignFlatButtonRipple { get{return (Color)ColorConverter.ConvertFromString("#FFB6B6B6");}}
        public Color MusicDesignToolTipBackground { get{return (Color)ColorConverter.ConvertFromString("#757575");}}
        public Color MusicDesignChipBackground { get{return (Color)ColorConverter.ConvertFromString("#12000000");}}
        public Color MusicDesignSnackbarBackground { get{return (Color)ColorConverter.ConvertFromString("#FF323232");}}
        public Color MusicDesignSnackbarMouseOver { get{return (Color)ColorConverter.ConvertFromString("#FF464642");}}
        public Color MusicDesignSnackbarRipple { get{return (Color)ColorConverter.ConvertFromString("#FFB6B6B6");}}
        public Color MusicDesignTextFieldBoxBackground { get{return (Color)ColorConverter.ConvertFromString("#0F000000");}}
        public Color MusicDesignTextFieldBoxHoverBackground { get{return (Color)ColorConverter.ConvertFromString("#14000000");}}
        public Color MusicDesignTextFieldBoxDisabledBackground { get{return (Color)ColorConverter.ConvertFromString("#08000000");}}
        public Color MusicDesignTextAreaBorder { get{return (Color)ColorConverter.ConvertFromString("#BC000000");}}
        public Color MusicDesignTextAreaInactiveBorder { get{return (Color)ColorConverter.ConvertFromString("#29000000");}}
        public Color MusicDesignDataGridRowHoverBackground { get{return (Color)ColorConverter.ConvertFromString("#0A000000");}}
    }
}
