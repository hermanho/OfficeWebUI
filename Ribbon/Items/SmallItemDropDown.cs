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
    [ToolboxData("<{0}:SmallItemDropDown runat=server></{0}:SmallItemDropDown>")]
    [ParseChildren(true, "Items")]
    public class SmallItemDropDown : ControlAncestor, INamingContainer, IOfficeWebUIWebControl
    {
        #region Private

        private String _text = String.Empty;
        private String _imageurl = String.Empty;
        private List<IOfficeWebUIWebControl> _items = new List<IOfficeWebUIWebControl>();
        private String _tooltip = String.Empty;
        private Unit _width;
        private Panel _Panel;
        private Panel _DropDownMenu;
        private Boolean _enabled = true;


        #endregion

        #region Public

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<IOfficeWebUIWebControl> Items
        {
            get { return this._items; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String ImageUrl
        {
            get { return this._imageurl; }
            set { this._imageurl = value; }
        }
        
        [Browsable(true)]
        [Category("Appearance")]
        public String Tooltip
        {
            get { return this._tooltip; }
            set { this._tooltip = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Unit Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Boolean Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; _EnableState(value); }
        }

        #endregion 

        protected override void OnInit(EventArgs e)
        {
            _Panel = new Panel();
            _Panel.ID = this.ID + "_Container";
            _Panel.CssClass = "RibbonItems RibbonItems_SmallItem RibbonItemWithMenu";
            _Panel.Attributes.Add("ItemID", this.ID);
            if (this._width != null) _Panel.Style.Add("width", _width.ToString());
            this.Controls.Add(_Panel);

            _EnableState(this._enabled);

            if (!String.IsNullOrEmpty(this._tooltip))
            {
                _Panel.CssClass += " RibbonItemWithTooltip";
                _Panel.Attributes.Add("tooltip", this._tooltip);
            }

            Table lTable = new Table();
            lTable.Attributes.Add("align", "center");
            _Panel.Controls.Add(lTable);

            TableRow lTableRow = new TableRow();
            lTable.Controls.Add(lTableRow);

            TableCell lTableCell = new TableCell();
            lTableRow.Controls.Add(lTableCell);

            Image lImage = new Image();
            lImage.ImageUrl = (!String.IsNullOrEmpty(this._imageurl)) ? this._imageurl : Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            lImage.Width = 16;
            lImage.Height = 16;
            lTableCell.Controls.Add(lImage);


            TableCell lTableCellArrow = new TableCell();
            lTableRow.Controls.Add(lTableCellArrow);

            Image lArrow = new Image();
            lArrow.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.arrow_dr2.gif");
            lTableCellArrow.Controls.Add(lArrow);


            _DropDownMenu = new Panel();
            _DropDownMenu.ID = this.ID + "_Menu";
            _DropDownMenu.CssClass = "RibbonDropDownMenu";
            _DropDownMenu.Style.Add("display", "none");
            _Panel.Controls.Add(_DropDownMenu);

            

            /*
            lPanel.Attributes.Add("onclick", "OfficeWebUI.Ribbon.ShowMenu(this, '" + lDropDownMenu.ClientID + "')");
            lDropDownMenu.Attributes.Add("onmouseover", "OfficeWebUI.Ribbon.KeepMenu('" + lDropDownMenu.ClientID + "')");
            lDropDownMenu.Attributes.Add("onmouseout", "OfficeWebUI.Ribbon.HideMenu('" + lDropDownMenu.ClientID + "')");
            lDropDownMenu.Attributes.Add("MenuOpener", lPanel.ClientID);
            */

            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            foreach (Control lCtrl in this._items)
            {
                _DropDownMenu.Controls.Add(lCtrl);
            }

            base.CreateChildControls();
        }

        private void _EnableState(Boolean state)
        {
            if (_Panel != null)
            {
                if (!state)
                {
                    _Panel.CssClass += " Disabled";
                    _Panel.Attributes["onclick"] = "";
                }
                else
                {
                    _Panel.CssClass = _Panel.CssClass.Replace(" Disabled", "");
                }
            }
        }

    }
}
