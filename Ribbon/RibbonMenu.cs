using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;

namespace OfficeWebUI.Ribbon
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RibbonMenu runat=server></{0}:RibbonMenu>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class RibbonMenu
    {
        #region Private

        private List<AppMenuItem> _menuItems = new List<AppMenuItem>();
        private List<BackstagePage> _backstagePages = new List<BackstagePage>();

        #endregion

        #region Public

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<BackstagePage> BackstagePages
        {
            get { return this._backstagePages; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<AppMenuItem> MenuItems
        {
            get { return this._menuItems; }
        }

        #endregion

        
    }
}
