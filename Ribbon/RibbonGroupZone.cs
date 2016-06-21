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
    [ToolboxData("<{0}:GroupZone runat=server></{0}:GroupZone>")]
    [ParseChildren(true, "Content")]
    [PersistChildren(false)]
    public class GroupZone : ControlAncestor, INamingContainer
    {
        #region Private

        private List<Control> _controls = new List<Control>();
        private String _text = String.Empty;

        internal Control _GroupZonesRow;

        #endregion

        #region Public

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Control> Content
        {
            get { return this._controls; }
        }

        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            TableCell lGroupZoneCell = new TableCell();
            lGroupZoneCell.VerticalAlign = VerticalAlign.Top;
            lGroupZoneCell.CssClass = "RibbonGroupColumn";
            _GroupZonesRow.Controls.Add(lGroupZoneCell);


            Panel lZoneContent = new Panel();
            lZoneContent.CssClass = "RibbonZoneContent";
            lGroupZoneCell.Controls.Add(lZoneContent);

            foreach (Control ctrl in this.Content)
            {
                lZoneContent.Controls.Add(ctrl);
            }

            base.OnInit(e);
        }
    }
}
