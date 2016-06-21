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
    [ToolboxData("<{0}:GalleryItem runat=server></{0}:GalleryItem>")]
    public class GalleryItem : ControlAncestor, IPostBackEventHandler
    {
        #region Private

        private String _text = String.Empty;
        private String _imageUrl = String.Empty;
        private String _clientClick = String.Empty;
        private String _navigateUrl = String.Empty;
        private Boolean _selected = false;
        private String _tooltip = String.Empty;

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
            get { return this._imageUrl; }
            set { this._imageUrl = value; }
        }

        [Browsable(true)]
        public String ClientClick
        {
            get { return this._clientClick; }
            set { this._clientClick = value; }
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
        public Boolean Selected
        {
            get { return this._selected; }
            set { this._selected = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String Tooltip
        {
            get { return this._tooltip; }
            set { this._tooltip = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            PostBackOptions p = new PostBackOptions(this);

            Panel lPanel = new Panel();
            lPanel.CssClass = "RibbonGalleryItem";
            lPanel.Attributes.Add("onclick", this._clientClick + "; " + Page.ClientScript.GetPostBackEventReference(p));
            this.Controls.Add(lPanel);

            if (!String.IsNullOrEmpty(this._tooltip))
            {
                lPanel.CssClass += " RibbonItemWithTooltip";
                lPanel.Attributes.Add("tooltip", this._tooltip);
            }

            Table lTable = new Table();
            lTable.Style.Add("margin-left", "auto");
            lTable.Style.Add("margin-right", "auto");
            lTable.CellSpacing = 0;
            lTable.CellPadding = 0;
            lPanel.Controls.Add(lTable);

            TableRow lTableRowImage = new TableRow();
            lTable.Controls.Add(lTableRowImage);

            TableCell lTableCellImage = new TableCell();
            lTableCellImage.HorizontalAlign = HorizontalAlign.Center;
            lTableRowImage.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = this._imageUrl;
            lTableCellImage.Controls.Add(lImage);

            TableRow lTableRowText = new TableRow();
            lTableRowText.HorizontalAlign = HorizontalAlign.Center;
            lTable.Controls.Add(lTableRowText);

            TableCell lTableCellText = new TableCell();
            lTableRowText.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._text;
            lTableCellText.Controls.Add(lText);

            base.OnInit(e);
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
