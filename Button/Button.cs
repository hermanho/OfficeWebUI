using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;
using OfficeWebUI.Button;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

namespace OfficeWebUI
{
    [DefaultProperty("Text")]
    [ParseChildren(true, "Items")]
    [DesignerAttribute(typeof(Button_Designer))]
    [ToolboxData("<{0}:OfficeButton runat=server></{0}:OfficeButton>")]
    public class OfficeButton : Panel, INamingContainer, IPostBackEventHandler
    {

        #region Private

        private Panel _buttonPanel;
        private Panel _DropDownMenu;
        private List<OfficeWebUI.Button.MenuItem> _items = new List<OfficeWebUI.Button.MenuItem>();
        private String _text = String.Empty;
        private String _extraText = String.Empty;
        private String _imageUrl = String.Empty;
        private String _navigateUrl = String.Empty;
        private String _clientClick = String.Empty;
        private Boolean _enabled = true;
        private ButtonDisplayType _display = ButtonDisplayType.ImageBeforeText;

        #endregion

        #region Public

        public event EventHandler Click;

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<OfficeWebUI.Button.MenuItem> Items
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
            get { return this._imageUrl; }
            set { this._imageUrl = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String ExtraText
        {
            get { return this._extraText; }
            set { this._extraText = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public ButtonDisplayType DisplayType
        {
            get { return this._display; }
            set { this._display = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String NavigateUrl
        {
            get { return this._navigateUrl; }
            set { this._navigateUrl = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String ClientClick
        {
            get { return this._clientClick; }
            set { this._clientClick = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public override Boolean Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; }
        }

        #endregion


        protected override void OnInit(EventArgs e)
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
                throw new Exception("You must include an OfficeWebUIManager on your page to use OfficeWebUI components");

            ScriptManager _scriptManager = ScriptManager.GetCurrent(Page);
            if (_scriptManager != null)
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.Button.js", this.GetType().Assembly.GetName().Name));
            else
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.Button.js");

            PostBackOptions p = new PostBackOptions(this);

            this.CssClass = "OfficeWebUI_Button";

            this._buttonPanel = new Panel();
            //_buttonPanel.Style.Add("display", "inline");
            _buttonPanel.Style.Add("text-align", "center");
            this._buttonPanel.ID = this.ID + "_ButtonPanel";
            //this._buttonPanel.CssClass = "OfficeWebUI_ButtonInner";
            this.Controls.Add(this._buttonPanel);



            if ((this._enabled) && (this._items.Count == 0))
                this.Attributes.Add("onclick", this._clientClick + "; " + Page.ClientScript.GetPostBackEventReference(p));

            _EnableState(this._enabled);


            switch (this._display)
            {
                case ButtonDisplayType.ImageAboveText:
                    _ImageAboveText();
                    break;
                case ButtonDisplayType.ImageBeforeText:
                    _ImageBeforeText();
                    break;
                case ButtonDisplayType.TextOnly:
                    _TextOnly();
                    break;
                case ButtonDisplayType.ImageOnly:
                    _ImageOnly();
                    break;
                default:
                    break;
            }


            if (_items.Count > 0)
            {
                this.CssClass += " ButtonWithMenu";
                _DropDownMenu = new Panel();
                _DropDownMenu.ID = this.ID + "_Menu";
                _DropDownMenu.CssClass = "ButtonDropDownMenu";
                _DropDownMenu.Style.Add("display", "none");
                _buttonPanel.Controls.Add(_DropDownMenu);

                foreach (OfficeWebUI.Button.MenuItem lItem in this._items)
                {
                    MenuItemRenderer lCtrl = new MenuItemRenderer(lItem);
                    _DropDownMenu.Controls.Add(lCtrl);
                }
            }


            base.OnInit(e);
        }

        private void _EnableState(Boolean state)
        {
            if (_buttonPanel != null)
            {
                if (!state)
                {
                    _buttonPanel.CssClass += " Disabled";
                    _buttonPanel.Attributes["onclick"] = "";
                }
                else
                {
                    _buttonPanel.CssClass = _buttonPanel.CssClass.Replace(" Disabled", "");                                        
                }
            }
        }

        protected override void CreateChildControls()
        {          
            base.CreateChildControls();
        }

        private void _ImageAboveText()
        {
            Table lTable = new Table();
            lTable.CssClass = "OfficeWebUI_ButtonTable";
            lTable.CellPadding = 0;
            lTable.CellSpacing = 1;
            _buttonPanel.Controls.Add(lTable);

            TableRow lTableRow1 = new TableRow();
            lTable.Controls.Add(lTableRow1);

            TableCell lTableCellImage = new TableCell();
            lTableCellImage.HorizontalAlign = HorizontalAlign.Center;
            lTableRow1.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = this._imageUrl;
            lTableCellImage.Controls.Add(lImage);

            TableRow lTableRow2 = new TableRow();
            //lTableRow2.Style.Add("height", "33px");
            lTable.Controls.Add(lTableRow2);

            TableCell lTableCellText = new TableCell();
            lTableCellText.HorizontalAlign = HorizontalAlign.Center;
            lTableRow2.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._text;
            lTableCellText.Controls.Add(lText);

            if (!String.IsNullOrEmpty(_extraText))
            {
                Label lExtraText = new Label();
                lExtraText.CssClass = "OfficeWebUI_Button_ExtraText";
                lExtraText.Text = "<br/>" + this._extraText;
                lTableCellText.Controls.Add(lExtraText);
            }

            if (this._items.Count > 0)
            {
                TableRow lTableRowArrow = new TableRow();
                lTable.Controls.Add(lTableRowArrow);

                TableCell lTableCellArrow = new TableCell();
                lTableCellArrow.Style.Add("padding-top", "10px");
                lTableCellArrow.Style.Add("background", "url('" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.arrow_dr2.gif") + "') center center no-repeat");
                lTableRowArrow.Controls.Add(lTableCellArrow);
            }
        }

        private void _ImageBeforeText()
        {
            Table lTable = new Table();
            lTable.CssClass = "OfficeWebUI_ButtonTable";
            lTable.CellPadding = 2;
            lTable.CellSpacing = 0;
            _buttonPanel.Controls.Add(lTable);

            TableRow lTableRow = new TableRow();
            lTable.Controls.Add(lTableRow);

            TableCell lTableCellImage = new TableCell();
            lTableRow.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = (!String.IsNullOrEmpty(this._imageUrl)) ? this._imageUrl : Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            lTableCellImage.Controls.Add(lImage);

            TableCell lTableCellText = new TableCell();
            lTableRow.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._text;
            lTableCellText.Controls.Add(lText);

            if (!String.IsNullOrEmpty(_extraText))
            {
                Label lExtraText = new Label();
                lExtraText.CssClass = "OfficeWebUI_Button_ExtraText";
                lExtraText.Text = "<br/>" + this._extraText;
                lTableCellText.Controls.Add(lExtraText);
            }

            if (this._items.Count > 0)
            {

                TableCell lTableCellArrow = new TableCell();
                lTableRow.Controls.Add(lTableCellArrow);

                Image lArrow = new Image();
                lArrow.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.arrow_dr2.gif");
                lTableCellArrow.Controls.Add(lArrow);

            }
        }

        private void _TextOnly()
        {
            Table lTable = new Table();
            lTable.CssClass = "OfficeWebUI_ButtonTable";
            lTable.CellPadding = 2;
            lTable.CellSpacing = 1;
            _buttonPanel.Controls.Add(lTable);

            TableRow lTableRow = new TableRow();
            lTable.Controls.Add(lTableRow);

            TableCell lTableCellText = new TableCell();
            lTableCellText.HorizontalAlign = HorizontalAlign.Center;
            lTableRow.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._text;
            lTableCellText.Controls.Add(lText);

            if (!String.IsNullOrEmpty(_extraText))
            {
                Label lExtraText = new Label();
                lExtraText.CssClass = "OfficeWebUI_Button_ExtraText";
                lExtraText.Text = "<br/>" + this._extraText;
                lTableCellText.Controls.Add(lExtraText);
            }

            if (this._items.Count > 0)
            {

                TableCell lTableCellArrow = new TableCell();
                lTableRow.Controls.Add(lTableCellArrow);

                Image lArrow = new Image();
                lArrow.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.arrow_dr2.gif");
                lTableCellArrow.Controls.Add(lArrow);

            }
        }

        private void _ImageOnly()
        {
            Table lTable = new Table();
            lTable.CssClass = "OfficeWebUI_ButtonTable";
            lTable.CellPadding = 0;
            lTable.CellSpacing = 1;
            _buttonPanel.Controls.Add(lTable);

            TableRow lTableRow = new TableRow();
            lTable.Controls.Add(lTableRow);

            TableCell lTableCellImage = new TableCell();
            lTableCellImage.HorizontalAlign = HorizontalAlign.Center;
            lTableRow.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = this._imageUrl;
            lTableCellImage.Controls.Add(lImage);


            if (this._items.Count > 0)
            {

                TableCell lTableCellArrow = new TableCell();
                lTableRow.Controls.Add(lTableCellArrow);

                Image lArrow = new Image();
                lArrow.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.arrow_dr2.gif");
                lTableCellArrow.Controls.Add(lArrow);

            }
        }

        #region IPostBackEventHandler Membres

        public void RaisePostBackEvent(string eventArgument)
        {
            if (!String.IsNullOrEmpty(this._navigateUrl))
            {
                HttpContext.Current.Response.Redirect(this._navigateUrl, true);
            }

            if (this.Click != null)
            {
                Click(this, new EventArgs());
            }
        }

        #endregion

        
    }

    
}
