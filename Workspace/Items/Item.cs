using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using OfficeWebUI.Common;
using System.Web.UI.WebControls;
using System.Web;

namespace OfficeWebUI.Workspace
{
    [ToolboxData("<{0}:WorkspaceSectionItem runat=server></{0}:WorkspaceSectionItem>")]
    [PersistChildren(false)]
    public class Item
    {
        public event EventHandler Click;


        public String Text { get; set; }
        public String ID { get; set; }
        public String ImageUrl { get; set; }
        public String ClientClick { get; set; }
        public String NavigateUrl { get; set; }

        internal void InvokeClick()
        {
            if (this.Click != null)
            {
                Click(this, new EventArgs());
            }
        }
    }

    internal class ItemRenderer : ControlAncestor, INamingContainer, IPostBackEventHandler
    {
        private Item _src;
        private Panel _panel;

        //private String LastItem
        //{
        //    get { return (HttpContext.Current.Session["OfficeWebUI.Workspace.CurrentItemID"] != null) ? HttpContext.Current.Session["OfficeWebUI.Workspace.CurrentItemID"].ToString() : String.Empty; }
        //    set { HttpContext.Current.Session["OfficeWebUI.Workspace.CurrentItemID"] = value; }
        //}

        public ItemRenderer(Item Src)
        {
            _src = Src;
            this.ID = "ItemRenderer_" + _src.ID;
        }

        protected override void OnInit(EventArgs e)
        {
            PostBackOptions p = new PostBackOptions(this);

            this._panel = new Panel();
            this._panel.ID = _src.ID + "_p";
            this._panel.Attributes.Add("WorkspaceInternalID", _src.ID);
            this._panel.CssClass = "OfficeWebUI_WorkspaceItem";
            this.Controls.Add(_panel);

            this._panel.Attributes.Add("onclick", "OfficeWebUI.Workspace.SaveLastItem(\"" + _src.ID + "\"); " + _src.ClientClick + "; " + Page.ClientScript.GetPostBackEventReference(p));

            Table lTable = new Table();
            lTable.CellPadding = 2;
            lTable.CellSpacing = 0;
            this._panel.Controls.Add(lTable);

            TableRow lTableRow = new TableRow();
            lTable.Controls.Add(lTableRow);

            TableCell lTableCellImage = new TableCell();
            lTableRow.Controls.Add(lTableCellImage);

            Image lImage = new Image();
            lImage.ImageUrl = (!String.IsNullOrEmpty(_src.ImageUrl)) ? _src.ImageUrl : Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            lImage.Width = 16;
            lImage.Height = 16;
            lTableCellImage.Controls.Add(lImage);

            TableCell lTableCellText = new TableCell();
            lTableRow.Controls.Add(lTableCellText);

            Literal lText = new Literal();
            lText.Text = _src.Text;
            lTableCellText.Controls.Add(lText);

            

            //_panel.Controls.Add(new Literal { Text = _src.Text });

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
