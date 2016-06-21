using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace OfficeWebUI.Common
{
    public class Resources
    {
        private String _ok = "Ok";    
        public String Text_Ok { get { return this._ok; } set { this._ok = value; } }
        
        private String _cancel = "Cancel";
        public String Text_Cancel { get { return this._cancel; } set{ this._cancel = value; } }
        
        private String _yes = "Yes";
        public String Text_Yes { get { return this._yes; } set { this._yes = value; } }
        
        private String _no = "No";
        public String Text_No { get { return this._no; } set { this._no = value; } }
        
        private String _close = "Close";
        public String Text_Close { get { return this._close; } set { this._close = value; } }
        
        private String _itemDisabled = "This item is disabled"; 
        public String Text_ItemDisabled { get { return this._itemDisabled; } set { this._itemDisabled = value; } }

    }


}
