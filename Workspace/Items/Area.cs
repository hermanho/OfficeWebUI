using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using OfficeWebUI.Common;
using System.Web.UI.WebControls;

namespace OfficeWebUI.Workspace
{
    [ToolboxData("<{0}:WorkspaceArea runat=server></{0}:WorkspaceArea>")]
    [PersistChildren(false)]
    public class Area
    {
        #region Private

        private List<Section> _sections = new List<Section>();

        #endregion

        #region Public

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Section> Sections
        {
            get { return this._sections; }
        }

        public String Text { get; set; }
        public String ID { get; set; }
        public String ImageUrl { get; set; }

        #endregion

    }

    internal class AreaRenderer : ControlAncestor, IPostBackEventHandler
    {
        public Area _src;
        private Panel _panelAreaButton;
        private Panel _panelAreaContainer;
        private Control _sectionsContainer;
        public event EventHandler Click;

        public AreaRenderer(Area Src, Control SectionsContainer)
        {
            _src = Src;
            _sectionsContainer = SectionsContainer;
            this.ID = "AreaRenderer_" + _src.ID;
        }

        //public void unselect()
        //{
        //    this._panelAreaButton.Style.Add("background", "transparent");
        //    this._src.Selected = false;
        //}

        //public void select()
        //{
        //    this._panelAreaButton.Style.Add("background", "red");
        //}

        protected override void OnInit(EventArgs e)
        {
            //PostBackOptions p = new PostBackOptions(this);

            this._panelAreaButton = new Panel();
            this._panelAreaButton.ID = _src.ID;
            this._panelAreaButton.CssClass = "OfficeWebUI_WorkspaceAreaButton";
            this._panelAreaButton.Attributes.Add("AssociatedArea", _src.ID);
            this._panelAreaButton.Attributes.Add("AreaId", _src.ID);
            //this._panel.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(p));
            this.Controls.Add(_panelAreaButton);

            //_panelAreaButton.Controls.Add(new Literal { Text = _src.Text });


            Table lTable = new Table();
            lTable.CellPadding = 2;
            lTable.CellSpacing = 0;
            this._panelAreaButton.Controls.Add(lTable);

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



            this._panelAreaContainer = new Panel();
            this._panelAreaContainer.CssClass = "OfficeWebUI_WorkspaceAreaZone";
            this._panelAreaContainer.Attributes.Add("AssociatedArea", _src.ID);
            _sectionsContainer.Controls.Add(_panelAreaContainer);


            foreach (Section lSectionSrc in _src.Sections)
            {
                SectionRenderer lSection = new SectionRenderer(lSectionSrc);
                _panelAreaContainer.Controls.Add(lSection);
            }

            base.OnInit(e);
        }


        #region IPostBackEventHandler Membres

        public void RaisePostBackEvent(string eventArgument)
        {
            if (this.Click != null)
            {
                this.Click(_src, new EventArgs());
            }

            //this._src.Selected = true;
            
        }

        #endregion
    }
}
