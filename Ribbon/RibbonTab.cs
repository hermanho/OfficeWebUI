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

    [ToolboxData("<{0}:RibbonTab runat=server></{0}:RibbonTab>")]
    [ParseChildren(true, "Groups")]
    [PersistChildren(false)]
    public class RibbonTab : ControlAncestor, INamingContainer
    {
        #region Private

        private List<RibbonGroup> _groups = new List<RibbonGroup>();
        private String _text = String.Empty;
        private String _imageUrl = String.Empty;
        private Boolean _visible = true;

        private Panel _Tab;
        private Table _TabTable;
        private TableRow _TabTableRow;
        private Panel _TabContent;

        internal Control _Context;
        internal Control _TabsContainer;
        #endregion

        #region Public

        public Guid Uid = System.Guid.NewGuid();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<RibbonGroup> Groups
        {
            get { return this._groups; }
        }

        [Browsable(true)]
        public String Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        [Browsable(true)]
        public String ImageUrl
        {
            get { return this._imageUrl; }
            set { this._imageUrl = value; }
        }

        [Browsable(true)]
        public override Boolean Visible
        {
            get { return (this._Tab != null) ? this._Tab.Visible : _visible; }
            set
            {
                if (this._Tab != null)
                {
                    this._Tab.Visible = value;
                }
                _visible = value;
            }
        }

        #endregion


        protected override void OnInit(EventArgs e)
        {
            //this._TabsContainer = (Parent as RibbonContext)._TabsContainer;
            //this._Context = (Parent as RibbonContext)._Context;


            _Tab = new Panel();
            _Tab.Attributes.Add("TabID", this.ID);
            _Tab.Attributes.Add("AssociatedTab", this.Uid.ToString());
            _Tab.CssClass = "RibbonTab";
            _Tab.Visible = _visible;
            _Context.Controls.Add(_Tab);

            _TabTable = new Table();
            _TabTable.CellPadding = 0;
            _TabTable.CellSpacing = 0;
            _Tab.Controls.Add(_TabTable);

            _TabTableRow = new TableRow();
            _TabTable.Controls.Add(_TabTableRow);


            if (!String.IsNullOrEmpty(this.ImageUrl))
            {
                TableCell lTableCell1 = new TableCell();
                lTableCell1.Style.Add("padding-right", "3px");
                _TabTableRow.Controls.Add(lTableCell1);

                Image lTabIcon = new Image();
                lTabIcon.ImageUrl = this.ImageUrl;
                lTableCell1.Controls.Add(lTabIcon);
            }

            TableCell lTableCell2 = new TableCell();
            _TabTableRow.Controls.Add(lTableCell2);

            Literal lTabTitle = new Literal() { Text = this.Text };
            lTableCell2.Controls.Add(lTabTitle);


            _TabContent = new Panel();
            _TabContent.ID = this.ID + "_TabContent";
            _TabContent.Attributes.Add("TabContentID", this.ID);
            _TabContent.Attributes.Add("AssociatedTab", this.Uid.ToString());
            _TabContent.CssClass = "RibbonTabContent";
            _TabsContainer.Controls.Add(_TabContent);


            EnsureChildControls();

            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            foreach (RibbonGroup group in this.Groups)
            {
                group._TabContent = _TabContent;

                this.Controls.Add(group);
            }

            base.CreateChildControls();
        }

    }
}
