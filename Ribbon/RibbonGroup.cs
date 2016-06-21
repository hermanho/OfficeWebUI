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
    [ToolboxData("<{0}:RibbonGroup runat=server></{0}:RibbonGroup>")]
    [ParseChildren(true, "Zones")]
    [PersistChildren(false)]
    public class RibbonGroup : ControlAncestor, INamingContainer
    {
        #region Private

        private List<GroupZone> _zones = new List<GroupZone>();
        private String _text = String.Empty;
        private String _id = String.Empty;
        private Boolean _showExpander = false;
        private String _onClientClick = String.Empty;
        private Boolean _visible = true;

        private Panel _GroupCollapsedContainer;
        private Panel _GroupCollapsedContainerDropDown;
        private Panel _GroupContent;
        private Table _GroupTable;
        private TableRow _GroupZonesRow;

        internal Control _TabContent;

        #endregion

        #region Public

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<GroupZone> Zones
        {
            get { return this._zones; }
        }

        [Browsable(true)]
        public String Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        [Browsable(true)]
        public String ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [Browsable(true)]
        public override Boolean Visible
        {
            get { return (this._GroupContent != null) ?  this._GroupContent.Visible : _visible; }
            set {
                if (this._GroupContent != null) {
                    this._GroupContent.Visible = value;
                }
                _visible = value;
            }
        }

        [Browsable(true)]
        public Boolean ShowExpander
        {
            get { return this._showExpander; }
            set { this._showExpander = value; }
        }

        [Browsable(true)]
        public String OnExpanderClick
        {
            get { return this._onClientClick; }
            set { this._onClientClick = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {

            /**/

            _GroupCollapsedContainer = new Panel();
            _GroupCollapsedContainer.CssClass = "RibbonGroupCollapsedContainer";
            _GroupCollapsedContainer.Visible = _visible;
            _GroupCollapsedContainer.ID = this.ID + "_GroupCollapsedContainer";
            //_GroupCollapsedContainer.Attributes.Add("objType", "Element");
            _TabContent.Controls.Add(_GroupCollapsedContainer);

            Table lTableCollapse = new Table();
            lTableCollapse.CssClass = "RibbonGroupCollapsedContainerTable";
            lTableCollapse.CellPadding = 2;
            lTableCollapse.CellSpacing = 0;
            _GroupCollapsedContainer.Controls.Add(lTableCollapse);

            TableRow lTableCollapseRowImage = new TableRow();
            lTableCollapse.Controls.Add(lTableCollapseRowImage);

            TableCell lTableCellImage = new TableCell();
            lTableCellImage.Controls.Add(new Image { Width = 32, Height = 32, ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif") });
            lTableCollapseRowImage.Controls.Add(lTableCellImage);

            TableRow lTableCollapseRowText = new TableRow();
            lTableCollapse.Controls.Add(lTableCollapseRowText);

            TableCell lTableCellText = new TableCell();
            lTableCellText.Controls.Add(new Literal { Text = this._text });
            lTableCollapseRowText.Controls.Add(lTableCellText);

            TableRow lTableCollapseRowArrow = new TableRow();
            lTableCollapse.Controls.Add(lTableCollapseRowArrow);

            TableCell lTableCellArrow = new TableCell();
            lTableCellArrow.Style.Add("background", "url('" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.arrow_dr2.gif") + "') bottom center no-repeat");
            lTableCollapseRowArrow.Controls.Add(lTableCellArrow);

            Image lArrow = new Image();
            lArrow.Style.Add("height", "10px");
            lArrow.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            lTableCellArrow.Controls.Add(lArrow);


            /**/




            _GroupCollapsedContainerDropDown = new Panel();
            _GroupCollapsedContainerDropDown.CssClass = "RibbonGroupCollapsedContainerDropDown";
            _GroupCollapsedContainer.Controls.Add(_GroupCollapsedContainerDropDown);

            _GroupContent = new Panel();
            _GroupContent.ID = this.ID + "_GroupContent";
            _GroupContent.CssClass = "RibbonGroupContent";
            _GroupContent.Visible = _visible;
            _GroupContent.Attributes.Add("objType", "Element");
            _TabContent.Controls.Add(_GroupContent);

            _GroupCollapsedContainer.Attributes.Add("AssociatedElement", _GroupContent.ClientID); 

            _GroupTable = new Table();
            _GroupTable.CellPadding = 0;
            _GroupTable.CellSpacing = 0;
            _GroupContent.Controls.Add(_GroupTable);

            _GroupZonesRow = new TableRow();
            _GroupTable.Controls.Add(_GroupZonesRow);

            foreach (GroupZone zone in this.Zones)
            {
                zone._GroupZonesRow = _GroupZonesRow;
                this.Controls.Add(zone);

            }

            TableRow lGroupTableTitleRow = new TableRow();
            _GroupTable.Controls.Add(lGroupTableTitleRow);

            TableCell lGroupTableTitleCell = new TableCell();
            lGroupTableTitleCell.ColumnSpan = this.Zones.Count;
            lGroupTableTitleCell.CssClass = "RibbonGroupTitle";
            lGroupTableTitleRow.Controls.Add(lGroupTableTitleCell);

            lGroupTableTitleCell.Controls.Add(new Literal() { Text = this.Text });

            if (this._showExpander)
            {
                Panel lExpandPanel = new Panel();
                lExpandPanel.CssClass = "GroupExpandPanel";

                if (!String.IsNullOrEmpty(this._onClientClick))
                    lExpandPanel.Attributes.Add("onclick", this._onClientClick);

                Image lExpandImage = new Image();
                lExpandImage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_GroupExpand.png");
                lExpandPanel.Controls.Add(lExpandImage);

                lGroupTableTitleCell.Controls.Add(lExpandPanel);
            }


            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }
    }
}
