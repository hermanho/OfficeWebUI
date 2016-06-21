using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;

namespace OfficeWebUI.ListView
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ListViewItem runat=server></{0}:ListViewItem>")]
    public class ListViewItem
    {
        public event EventHandler Click;

        public String Text { get; set; }
        public String ExtraText { get; set; }
        public String ImageUrl { get; set; }
        public String NavigateUrl { get; set; }
        public String ClientClick { get; set; }
        public String ID { get; set; }
        public Boolean Selected { get; set; }

        internal void InvokeClick()
        {
            if (this.Click != null)
            {
                Click(this, new EventArgs());
            }
        }
        
    }


    internal class ListViewItemRenderer_List : ControlAncestor, IPostBackEventHandler, IOfficeWebUIWebControl
    {
        private ListViewItem _src;
        private Panel _Panel;

        public ListViewItemRenderer_List(ListViewItem src)
        {
            this._src = src;
        }

        protected override void OnInit(EventArgs e)
        {
            PostBackOptions p = new PostBackOptions(this);

            _Panel = new Panel();
            _Panel.CssClass = "OfficeWebUI_ListViewItem";
            _Panel.Attributes.Add("ItemID", this.ID);
            _Panel.Attributes.Add("onclick", this._src.ClientClick + "; " + Page.ClientScript.GetPostBackEventReference(p));
            this.Controls.Add(_Panel);

            if (this._src.Selected)
                _Panel.CssClass += " OfficeWebUI_ListViewItemSelected";

            Table lTable = new Table();
            lTable.CellPadding = 2;
            lTable.CellSpacing = 0;
            _Panel.Controls.Add(lTable);

            TableRow lTableRow = new TableRow();
            lTable.Controls.Add(lTableRow);

            TableCell lTableCellImage = new TableCell();
            lTableRow.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = (!String.IsNullOrEmpty(this._src.ImageUrl)) ? this._src.ImageUrl : Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            //lImage.Width = 16;
            //lImage.Height = 16;
            lTableCellImage.Controls.Add(lImage);

            TableCell lTableCellText = new TableCell();
            lTableRow.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._src.Text;
            lTableCellText.Controls.Add(lText);

            if (!String.IsNullOrEmpty(this._src.ExtraText))
            {
                Label lExtraText = new Label();
                lExtraText.CssClass = "OfficeWebUI_ListViewItem_ExtraText";
                lExtraText.Text = this._src.ExtraText;
                lTableCellText.Controls.Add(lExtraText);
            }

            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }


        #region IPostBackEventHandler Membres

        public void RaisePostBackEvent(string eventArgument)
        {
            if (!String.IsNullOrEmpty(_src.NavigateUrl))
                Page.Response.Redirect(_src.NavigateUrl, true);

            _src.InvokeClick();
        }

        #endregion
    }

    internal class ListViewItemRenderer_Gallery : ControlAncestor, IPostBackEventHandler, IOfficeWebUIWebControl
    {
        private ListViewItem _src;
        private Panel _Panel;

        public ListViewItemRenderer_Gallery(ListViewItem src)
        {
            this._src = src;
        }

        protected override void OnInit(EventArgs e)
        {
            PostBackOptions p = new PostBackOptions(this);

            _Panel = new Panel();
            _Panel.CssClass = "OfficeWebUI_ListViewItem";
            _Panel.Attributes.Add("ItemID", this.ID);
            _Panel.Attributes.Add("onclick", this._src.ClientClick + "; " + Page.ClientScript.GetPostBackEventReference(p));
            this.Controls.Add(_Panel);

            if (this._src.Selected)
                _Panel.CssClass += " OfficeWebUI_ListViewItemSelected";

            Table lTable = new Table();
            lTable.Attributes.Add("align", "center");
            lTable.CellPadding = 2;
            lTable.CellSpacing = 0;
            _Panel.Controls.Add(lTable);

            TableRow lTableRowImage = new TableRow();
            lTable.Controls.Add(lTableRowImage);

            TableCell lTableCellImage = new TableCell();
            lTableRowImage.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = (!String.IsNullOrEmpty(this._src.ImageUrl)) ? this._src.ImageUrl : Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            //lImage.Width = 16;
            //lImage.Height = 16;
            lTableCellImage.Controls.Add(lImage);

            TableRow lTableRowText = new TableRow();
            lTable.Controls.Add(lTableRowText);

            TableCell lTableCellText = new TableCell();
            lTableRowText.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._src.Text;
            lTableCellText.Controls.Add(lText);

            if (!String.IsNullOrEmpty(this._src.ExtraText))
            {
                Label lExtraText = new Label();
                lExtraText.CssClass = "OfficeWebUI_ListViewItem_ExtraText";
                lExtraText.Text = "<br/>" + this._src.ExtraText;
                lTableCellText.Controls.Add(lExtraText);
            }

            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }


        #region IPostBackEventHandler Membres

        public void RaisePostBackEvent(string eventArgument)
        {
            if (!String.IsNullOrEmpty(_src.NavigateUrl))
                Page.Response.Redirect(_src.NavigateUrl, true);

            _src.InvokeClick();
        }

        #endregion
    }

}
