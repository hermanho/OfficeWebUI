using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Ribbon;
using OfficeWebUI.Common;
using System.IO;
using OfficeWebUI.ListView;
using System.Collections.ObjectModel;

namespace OfficeWebUI
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ListView runat=server></{0}:ListView>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class OfficeListView : ControlAncestor, INamingContainer, ICompositeControlDesignerAccessor
    {
        #region Private

        private List<OfficeWebUI.ListView.ListViewItem> _items = new List<OfficeWebUI.ListView.ListViewItem>();
        private Unit _width = new Unit(200);
        private Unit _height = new Unit(300);
        private ListViewDisplayMode _mode = ListViewDisplayMode.List;

        private Panel _PanelList;

        #endregion

        #region Public

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<OfficeWebUI.ListView.ListViewItem> Items
        {
            get { return this._items; }
        }

        public Unit Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        public Unit Height
        {
            get { return this._height; }
            set { this._height = value; }
        }

        public ListViewDisplayMode DisplayMode
        {
            get { return this._mode; }
            set { this._mode = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
                throw new Exception("You must include an OfficeWebUIManager on your page to use OfficeWebUI components");

            ScriptManager _scriptManager = ScriptManager.GetCurrent(Page);
            if (_scriptManager != null)
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.Ribbon.js", this.GetType().Assembly.GetName().Name));
            else
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.Ribbon.js");
            

            _PanelList = new Panel();
            _PanelList.Width = this._width;
            _PanelList.Height = this._height;
            
            this.Controls.Add(_PanelList);


            switch (this._mode)
            {
                case ListViewDisplayMode.List:
                    _PanelList.CssClass = "OfficeWebUI_ListView_List";
                    break;
                case ListViewDisplayMode.Gallery:
                    _PanelList.CssClass = "OfficeWebUI_ListView_Gallery";
                    break;
                default:
                    break;
            }

            DataBind();

            base.OnInit(e);
        }


        public override void DataBind()
        {
            _PanelList.Controls.Clear();

            foreach (OfficeWebUI.ListView.ListViewItem lItem in this._items)
            {
                ControlAncestor lCtrl;
                switch (this._mode)
                {
                    case ListViewDisplayMode.List:
                        lCtrl = new ListViewItemRenderer_List(lItem);
                        break;
                    case ListViewDisplayMode.Gallery:
                        lCtrl = new ListViewItemRenderer_Gallery(lItem);
                        break;
                    default:
                        lCtrl = new ListViewItemRenderer_List(lItem);
                        break;
                }
                _PanelList.Controls.Add(lCtrl);
            }

        }
        

        #region ICompositeControlDesignerAccessor Membres

        public void RecreateChildControls()
        {
            base.ChildControlsCreated = true;
            //EnsureChildControls();
        }

        #endregion
    }

}


