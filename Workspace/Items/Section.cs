using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using OfficeWebUI.Common;
using System.Web.UI.WebControls;

namespace OfficeWebUI.Workspace
{
    [ToolboxData("<{0}:WorkspaceSection runat=server></{0}:WorkspaceSection>")]
    [PersistChildren(false)]
    public class Section
    {
        #region Private

        private List<Item> _items = new List<Item>();

        #endregion

        #region Public

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Item> Items
        {
            get { return this._items; }
        }

        public String Text { get; set; }

        #endregion
    }

    internal class SectionRenderer : ControlAncestor
    {
        private Section _src;
        private Panel _panel;
        private Panel _sectionTitle;
        private Panel _itemsContainer;

        public SectionRenderer(Section Src)
        {
            _src = Src;
        }

        protected override void OnInit(EventArgs e)
        {

            this._sectionTitle = new Panel();
            this._sectionTitle.CssClass = "OfficeWebUI_WorkspaceSectionTitle";
            this.Controls.Add(this._sectionTitle);

            this._sectionTitle.Controls.Add(new Literal { Text = _src.Text });

            this._itemsContainer = new Panel();
            this.Controls.Add(_itemsContainer);

            foreach (Item lItemSrc in _src.Items)
            {
                ItemRenderer lItem = new ItemRenderer(lItemSrc);
                _itemsContainer.Controls.Add(lItem);
            }

            base.OnInit(e);
        }

    }
}
