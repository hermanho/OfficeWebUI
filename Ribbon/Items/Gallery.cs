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
    [ToolboxData("<{0}:Gallery runat=server></{0}:Gallery>")]
    [ParseChildren(true, "Items")]
    public class Gallery : ControlAncestor, INamingContainer
    {
        #region Private

        private List<GalleryItem> _items = new List<GalleryItem>();
        private List<IOfficeWebUIWebControl> _extraItems = new List<IOfficeWebUIWebControl>();
        private int _ItemsToShow = 3;
        private int _ItemsPerLine = 4;

        private Panel _Panel;
        private Panel _SubItemsContainer;
        private Panel _PanelFinalClear;
        private Panel _DropDown;

        #endregion

        #region Public

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<GalleryItem> Items
        {
            get { return this._items; }
        }

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<IOfficeWebUIWebControl> ExtraItems
        {
            get { return this._extraItems; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Int32 ItemsToShow
        {
            get { return this._ItemsToShow; }
            set { this._ItemsToShow = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Int32 ItemsPerLine
        {
            get { return this._ItemsPerLine; }
            set { this._ItemsPerLine = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            _Panel = new Panel();
            _Panel.CssClass = "RibbonItems RibbonGalleryContainer RibbonItemWithGallery";
            this.Controls.Add(_Panel);

            _SubItemsContainer = new Panel();
            _SubItemsContainer.Style.Add("display", "none");
            _SubItemsContainer.CssClass = "RibbonGallerySubMenu";
            this.Controls.Add(_SubItemsContainer);


            int counter = 0;
            Panel pnl = new Panel();
            foreach (Control obj in this._items)
            {
                if ((counter < _ItemsToShow) && (_Panel.Controls.Count < _ItemsToShow))
                {
                    _Panel.Controls.Add(obj);
                    counter++;

                    if (counter == _ItemsToShow) counter = 0;
                }
                else
                {
                    if (counter == 0)
                    {
                        pnl = new Panel();
                        pnl.Style.Add("clear", "both");
                        _SubItemsContainer.Controls.Add(pnl);
                    }

                    pnl.Controls.Add(obj);
                    counter++;
                    if (counter == _ItemsPerLine) { counter = 0; }
                }

            }

            _PanelFinalClear = new Panel();
            _PanelFinalClear.Style.Add("clear", "both");
            _SubItemsContainer.Controls.Add(_PanelFinalClear);

            if (this._extraItems.Count > 0)
            {
                Panel lGalleryExtrItems = new Panel();
                lGalleryExtrItems.CssClass = "GalleryExtraItems";
                _SubItemsContainer.Controls.Add(lGalleryExtrItems);

                foreach (Control lCtrl in this._extraItems)
                {
                    lGalleryExtrItems.Controls.Add(lCtrl);
                }
            }


            _DropDown = new Panel();
            _DropDown.CssClass = "RibbonGalleryDropDownArrow";
            this.Controls.Add(_DropDown);

            /*
            lDropDown.Attributes.Add("onclick", "OfficeWebUI.Ribbon.OpenGallery('" + lPanel.ClientID + "', '" + lSubItemsContainer.ClientID + "')");
            lSubItemsContainer.Attributes.Add("onmouseover", "OfficeWebUI.Ribbon.KeepMenu('" + lSubItemsContainer.ClientID + "')");
            lSubItemsContainer.Attributes.Add("onmouseout", "OfficeWebUI.Ribbon.HideMenu('" + lSubItemsContainer.ClientID + "')");
            lSubItemsContainer.Attributes.Add("MenuOpener", lSubItemsContainer.ClientID);
            */

            Image lArrow = new Image();
            lArrow.Style.Add("position", "relative");
            lArrow.Style.Add("top", "40%");
            lArrow.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.arrow_dr2.gif");
            _DropDown.Controls.Add(lArrow);

            base.OnInit(e);
        }

    }
}
