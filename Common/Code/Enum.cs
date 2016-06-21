namespace OfficeWebUI
{
    /// <summary>
    /// Supported themes
    /// </summary>
    public enum Theme { Custom, Office2010Silver, Office2010Blue, Windows7,Wave }

    /// <summary>
    /// Supported types of Ribbon menus
    /// </summary>
    public enum ApplicationMenuType { Default, Backstage, None }

    /// <summary>
    /// Supported types of buttons
    /// </summary>
    public enum ButtonDisplayType { ImageAboveText, ImageBeforeText, TextOnly, ImageOnly }

    /// <summary>
    /// Document Text Direction
    /// </summary>
    public enum DocumentDirection { LTR, RTL }

    /// <summary>
    /// Display modes for a ListView
    /// </summary>
    public enum ListViewDisplayMode { List, Gallery }

    /// <summary>
    /// Message type for MessageBox
    /// </summary>
    public enum MessageBoxType { Default, Warn, Info, Question, Error }

    /// <summary>
    /// Buttons for MessageBox
    /// </summary>
    public enum MessageBoxButtonsType { Ok, YesNo, YesNoCancel, OkCancel }
    
    /// <summary>
    /// Return value of MessageBox
    /// </summary>
    public enum MessageBoxReturnType { Ok, Yes, No, Cancel }

}