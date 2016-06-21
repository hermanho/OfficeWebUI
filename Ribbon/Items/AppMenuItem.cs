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
    [ToolboxData("<{0}:AppMenuItem runat=server></{0}:AppMenuItem>")]
    public class AppMenuItem : IPostBackEventHandler
    {
        #region Private

        private String _imageUrl = String.Empty;
        private String _text = String.Empty;
        private String _id = String.Empty;

        #endregion

        #region Public

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
        public String ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public event EventHandler Click;
        public void RaisePostBackEvent(String eventArgument)
        {
            if (this.Click != null)
                Click(this, new EventArgs());
        }



        #endregion

    }

    internal class AppMenuItemRenderer : ControlAncestor, IPostBackEventHandler
    {
        #region Private

        private AppMenuItem _src = null;
        private String _clientClick = String.Empty;
        private String _id = String.Empty;

        #endregion

        #region Public

        public event EventHandler Click;

        [Browsable(true)]
        public String ClientClick
        {
            get { return this._clientClick; }
            set { this._clientClick = value; }
        }

        #endregion

        public AppMenuItemRenderer(AppMenuItem src)
        {
            this._src = src;
            this.ID = _id;
        }

        protected override void OnInit(EventArgs e)
        {
            EnsureChildControls();
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            PostBackOptions p = new PostBackOptions(this);

            Panel lPanel = new Panel();
            lPanel.ID = this.ID + "_AppMenuItem";
            lPanel.CssClass = "RibbonAppMenuItem";
            lPanel.Attributes.Add("onclick", this._clientClick + "; " + Page.ClientScript.GetPostBackEventReference(p));
            this.Controls.Add(lPanel);

            Table lTable = new Table();
            lTable.CellPadding = 2;
            lTable.CellSpacing = 0;
            lPanel.Controls.Add(lTable);

            TableRow lTableRow = new TableRow();
            lTable.Controls.Add(lTableRow);

            TableCell lTableCellImage = new TableCell();
            lTableRow.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = (!String.IsNullOrEmpty(this._src.ImageUrl)) ? this._src.ImageUrl : Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            lImage.Width = 32;
            lImage.Height = 32;
            lTableCellImage.Controls.Add(lImage);

            TableCell lTableCellText = new TableCell();
            lTableRow.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._src.Text;
            lTableCellText.Controls.Add(lText);

            base.CreateChildControls();
        }

        #region IPostBackEventHandler Membres

        public void RaisePostBackEvent(string eventArgument)
        {
            if (this.Click != null)
            {
                Click(this, new EventArgs());
            }
            this._src.RaisePostBackEvent(eventArgument);

        }



        #endregion
    }

}
