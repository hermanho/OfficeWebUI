using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;

namespace OfficeWebUI.Button
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:MenuItem runat=server></{0}:MenuItem>")]
    public class MenuItem
    {
        public event EventHandler Click;
        
        public String Text { get; set; }
        public String ExtraText { get; set; }
        public String ImageUrl { get; set; }
        public String NavigateUrl { get; set; }
        public String ClientClick { get; set; }
        public String ID { get; set; }
        public String CssClass { get; set; }

        internal void InvokeClick()
        {
            if (this.Click != null)
            {
                Click(this, new EventArgs());
            }
        }
    }

    internal class MenuItemRenderer : ControlAncestor, INamingContainer, IPostBackEventHandler
    {
        private MenuItem _src;
        private Panel _Panel;

        public MenuItemRenderer(MenuItem src)
        {
            _src = src;
        }

        protected override void OnInit(EventArgs e)
        {
            PostBackOptions p = new PostBackOptions(this);

            if (this._src.Text == "-")
            {
                _Panel = new Panel();
                _Panel.ID = _src.ID + "_p";
                _Panel.CssClass = "ButtonMenuSeparator";
                this.Controls.Add(_Panel);

                return;
            }

            _Panel = new Panel();
            _Panel.ID = _src.ID + "_p";
            _Panel.CssClass = "ButtonMenuItem";
            if (!string.IsNullOrWhiteSpace(_src.CssClass))
                _Panel.CssClass += " " + _src.CssClass;
            this.Controls.Add(_Panel);

            _Panel.Attributes.Add("onclick", this._src.ClientClick + "; " + Page.ClientScript.GetPostBackEventReference(p));

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
            lTableCellImage.Controls.Add(lImage);

            TableCell lTableCellText = new TableCell();
            lTableRow.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = this._src.Text;
            lTableCellText.Controls.Add(lText);

            if (!String.IsNullOrEmpty(this._src.ExtraText))
            {
                Label lExtraText = new Label();
                lExtraText.CssClass = "OfficeWebUI_Button_ExtraText";
                lExtraText.Text = "<br/>" + this._src.ExtraText;
                lTableCellText.Controls.Add(lExtraText);
            }


            base.OnInit(e);
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
