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
    [ToolboxData("<{0}:LargeItem runat=server></{0}:LargeItem>")]
    public class LargeItem : ControlAncestor, IPostBackEventHandler, IOfficeWebUIWebControl
    {
        #region Private 

        PostBackOptions _p;
        private String _text = String.Empty;
        private String _imageurl = String.Empty;
        private Boolean _checked = false;
        private Boolean _enabled = true;
        private String _clientClick = String.Empty;
        private String _navigateUrl = String.Empty;
        private String _target = String.Empty;
        private String _tooltip = String.Empty;
        private Unit _width;
        private Panel _Panel;
        private Boolean _performValidation = false;
        private String _validationGroup = String.Empty;

        #endregion

        #region Public

        public event EventHandler Click;

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
        public Boolean Checked
        {
            get { return this._checked; }
            set { this._checked = value; _CheckedState(value); }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Boolean Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; _EnableState(value); }
        }

        [Browsable(true)]
        public String ClientClick
        {
            get { return this._clientClick; }
            set { this._clientClick = value; }
        }

        [Browsable(true)]
        public String NavigateUrl
        {
            get { return this._navigateUrl; }
            set { this._navigateUrl = value; }
        }

        [Browsable(true)]
        public String Target
        {
            get { return this._target; }
            set { this._target = value; }
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
        public Boolean PerformValidation
        {
            get { return this._performValidation; }
            set { this._performValidation = value; _ValidationState(value); }
        }

        [Browsable(true)]
        public String ValidationGroup
        {
            get { return this._validationGroup; }
            set { this._validationGroup = value; _ValidationGroupState(value); }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String CssClass { get; set; }

        [Browsable(true)]
        [Category("Appearance")]
        public String LinkCssClass { get; set; }

        #endregion
        
        protected override void OnInit(EventArgs e)
        {
            _p = new PostBackOptions(this);
            _ValidationState(this._performValidation);
            _ValidationGroupState(this._validationGroup);

            //System.Web.UI.HtmlControls.HtmlAnchor clickLink = null;
            HyperLink clickLink = null;

            _Panel = new Panel();
            _Panel.ID = _Panel.UniqueID;
            _Panel.CssClass = "RibbonItems RibbonItems_LargeItem";
            if (!string.IsNullOrWhiteSpace(this.CssClass))
                _Panel.CssClass += " " + this.CssClass;
            _Panel.Attributes.Add("ItemID", this.ID);
            this.Controls.Add(_Panel);

            if (this._enabled)
            {
                //_Panel.Attributes.Add("onclick", this._clientClick + "; " + Page.ClientScript.GetPostBackEventReference(_p));
                string jsString = "";
                if (!string.IsNullOrWhiteSpace(this._clientClick))
                {
                    jsString += this._clientClick;
                    if (!jsString.TrimEnd().EndsWith(";"))
                        jsString += "; ";
                }
                if (!string.IsNullOrWhiteSpace(this._navigateUrl))
                {
                    string target = "_self";
                    if (!string.IsNullOrWhiteSpace(this._target))
                    {
                        target = _target;
                    }
                    //jsString += " window.location.href='" + ResolveUrl(this._navigateUrl) + "'; ";
                    //jsString += " window.open('" + ResolveUrl(this._navigateUrl) + "' , '" + target + "'); ";

                    //_Panel.Attributes.Add("href", ResolveUrl(this._navigateUrl));

                    if (clickLink == null)
                    {
                        clickLink = new HyperLink();
                        clickLink.ID = clickLink.UniqueID;
                    }
                    //clickLink.HRef = ResolveUrl(this._navigateUrl);
                    clickLink.NavigateUrl = ResolveUrl(this._navigateUrl);
                }
                else
                {
                    jsString += Page.ClientScript.GetPostBackEventReference(_p) + ";";
                }
                if (!string.IsNullOrWhiteSpace(jsString))
                {
                    //    _Panel.Attributes.Add("onclick", jsString);
                    if (clickLink == null)
                    {
                        //clickLink = new System.Web.UI.HtmlControls.HtmlAnchor();
                        clickLink = new HyperLink();
                        clickLink.ID = clickLink.UniqueID;
                        clickLink.Attributes["href"]= "#";
                    }
                    clickLink.Attributes.Add("onclick", jsString);
                }
            }
            //if (this._width != null)
            if (this._width != null && !string.IsNullOrWhiteSpace(this._width.ToString()))
                _Panel.Style.Add("width", _width.ToString());

            _EnableState(this._enabled);
            _CheckedState(this._checked); 

            if (!String.IsNullOrEmpty(this._tooltip))
            {
                _Panel.CssClass += " RibbonItemWithTooltip";
                _Panel.Attributes.Add("tooltip", this._tooltip);
            }

            Table lTable = new Table();
            lTable.CellPadding = 0;
            lTable.CellSpacing = 1;
            lTable.Style.Add("min-width", "60px");

            if (clickLink != null)
            {
                clickLink.Controls.Add(lTable);
                if (!string.IsNullOrEmpty(this.LinkCssClass))
                    clickLink.CssClass += " " + LinkCssClass;
                _Panel.Controls.Add(clickLink);
            }
            else
            {
                if (!string.IsNullOrEmpty(this.LinkCssClass))
                    _Panel.CssClass += " " + LinkCssClass;
                _Panel.Controls.Add(lTable);
            }

            TableRow lTableRow1 = new TableRow();
            lTable.Controls.Add(lTableRow1);

            TableCell lTableCellImage = new TableCell();
            lTableCellImage.HorizontalAlign = HorizontalAlign.Center;
            lTableRow1.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = this._imageurl;
            lImage.Width = 32;
            lImage.Height = 32;
            lTableCellImage.Controls.Add(lImage);

            TableRow lTableRow2 = new TableRow();
            lTableRow2.Style.Add("height", "33px");
            lTable.Controls.Add(lTableRow2);

            TableCell lTableCellText = new TableCell();
            lTableCellText.HorizontalAlign = HorizontalAlign.Center;
            lTableRow2.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._text;
            lTableCellText.Controls.Add(lText);

            base.OnInit(e);
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
                else {
                    _Panel.CssClass = _Panel.CssClass.Replace(" Disabled", "");
                }
            }
        }

        private void _CheckedState(Boolean state)
        {
            if (_Panel != null)
            {
                if (state)
                    _Panel.CssClass += " Checked";
                else
                    _Panel.CssClass = _Panel.CssClass.Replace(" Checked", "");
            }
        }

        private void _ValidationState(Boolean state)
        {
            if (_p != null)
            {
                _p.PerformValidation = state;
            }
        }

        private void _ValidationGroupState(String state)
        {
            if (_p != null)
            {
                _p.ValidationGroup = state;
            }
        }

        #region IPostBackEventHandler Membres

        public void RaisePostBackEvent(string eventArgument)
        {
            if (!String.IsNullOrEmpty(this._navigateUrl))
            {
                HttpContext.Current.Response.Redirect(this._navigateUrl);
                return;
            }

            if (this.Click != null)
            {
                Click(this, new EventArgs());
            }
        }

        #endregion
    }
}
