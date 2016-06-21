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
    [ToolboxData("<{0}:LargeItemDropDown runat=server></{0}:LargeItemDropDown>")]
    [ParseChildren(true, "Items")]
    public class LargeItemDropDown : ControlAncestor, INamingContainer, IOfficeWebUIWebControl
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
            _Panel.CssClass = "RibbonItems RibbonItems_LargeItem RibbonItemWithMenu";
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
            lTable.CellPadding = 0;
            lTable.CellSpacing = 1;
            _Panel.Controls.Add(lTable);

            TableRow lTableRow1 = new TableRow();
            lTable.Controls.Add(lTableRow1);

            TableCell lTableCellImage = new TableCell();
            lTableCellImage.HorizontalAlign = HorizontalAlign.Center;
            lTableRow1.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = (!String.IsNullOrEmpty(this._imageurl)) ? this._imageurl : Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            lImage.Width = 32;
            lImage.Height = 32;
            lTableCellImage.Controls.Add(lImage);

            TableRow lTableRow2 = new TableRow();
            lTableRow2.Style.Add("height", "22px");
            lTable.Controls.Add(lTableRow2);

            TableCell lTableCellText = new TableCell();
            lTableCellText.HorizontalAlign = HorizontalAlign.Center;
            lTableRow2.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._text;
            lTableCellText.Controls.Add(lText);

            TableRow lTableRowArrow = new TableRow();
            lTable.Controls.Add(lTableRowArrow);

            TableCell lTableCellArrow = new TableCell();
            lTableCellArrow.Style.Add("background", "url('" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.arrow_dr2.gif") + "') bottom center no-repeat");
            lTableRowArrow.Controls.Add(lTableCellArrow);

            Image lArrow = new Image();
            lArrow.Style.Add("height", "10px");
            lArrow.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
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
