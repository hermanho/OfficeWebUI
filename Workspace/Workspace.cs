using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;
using OfficeWebUI.Workspace;
using System.Web.UI.Design.WebControls;
using System.ComponentModel.Design;

namespace OfficeWebUI
{
    [ToolboxData("<{0}:OfficeWorkspace runat=server></{0}:OfficeWorkspace>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [DesignerAttribute(typeof(OfficeWorkspace_Designer))]
    public class OfficeWorkspace : ControlAncestor, INamingContainer, ICompositeControlDesignerAccessor
    {
        #region Private

        private Panel _mainPanel;
        private Table _mainTable;
        private TableRow _mainRow;
        private Panel _statusBar;
        private TableCell _leftPanel;
        private TableCell _rightPanel;
        private TableCell _contentCell;
        private Panel _contentPanel;
        private HiddenField _ActiveItem;
        private HiddenField _ActiveArea;
        
        private Panel _areasContainer;
        private Panel _navContainer;

        private Boolean _showLeftPanel = true;
        private Boolean _showRightPanel = false;
        private Boolean _showStatusBar = false;

        private Unit _leftPanelWidth = 180;
        private Unit _rightPanelWidth = 180;

        private List<Area> _areas = new List<Area>();
        private ITemplate _content;

        #endregion

        #region Public

        public event EventHandler ItemClick;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Area> Areas
        {
            get { return this._areas; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty),
         TemplateContainer(typeof(TemplateControl)),
         TemplateInstance(TemplateInstance.Single)]
        public ITemplate Content
        {
            get { return this._content; }
            set { this._content = value; }
        }

        public Boolean ShowLeftPanel
        {
            get { return this._showLeftPanel; }
            set { this._showLeftPanel = value; this._leftPanel.Visible = value; }
        }

        public Boolean ShowRightPanel
        {
            get { return this._showRightPanel; }
            set { this._showRightPanel = value; this._rightPanel.Visible = value; }
        }

        public Unit LeftPanelWidth
        {
            get { return this._leftPanelWidth; }
            set { this._leftPanelWidth = value; this._leftPanel.Width = value; }
        }

        public Unit RightPanelWidth
        {
            get { return this._rightPanelWidth; }
            set { this._rightPanelWidth = value; this._rightPanel.Width = value; }
        }

        //public Boolean ShowStatusBar
        //{
        //    get { return this._showStatusBar; }
        //    set { this._showStatusBar = value; }
        //}

        public String SelectedAreaID
        {
            get { return this._ActiveArea.Value; }
            set { this._ActiveArea.Value = value; }
        }

        public String SelectedItemID
        {
            get { return this._ActiveItem.Value; }
            set { this._ActiveItem.Value = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
                throw new Exception("You must include an OfficeWebUIManager on your page to use OfficeWebUI components");
           
            ScriptManager _scriptManager = ScriptManager.GetCurrent(Page);
            if (_scriptManager != null)
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.Workspace.js", this.GetType().Assembly.GetName().Name));
            else
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.Workspace.js");

            _mainPanel = new Panel();
            _mainTable = new Table();
            _mainRow = new TableRow();
            _leftPanel = new TableCell();
            _contentCell = new TableCell();
            _statusBar = new Panel();
            _rightPanel = new TableCell();
            _areasContainer = new Panel();
            _navContainer = new Panel();
            _contentPanel = new Panel();
            _ActiveItem = new HiddenField();
            _ActiveArea = new HiddenField();

            this._mainPanel.ID = this.ID + "_mainPanel";
            this._mainTable.ID = this.ID + "_mainTable";
            this._leftPanel.ID = this.ID + "_leftPanel";
            this._contentCell.ID = this.ID + "_contentCell";
            this._areasContainer.ID = this.ID + "_areasContainer";
            this._navContainer.ID = this.ID + "_navContainer";
            this._contentPanel.ID = this.ID + "_contentPanel";


            this.Controls.Add(_mainPanel);
            this._mainPanel.Controls.Add(_mainTable);
            this._mainTable.Controls.Add(_mainRow);
            this._mainRow.Controls.Add(_leftPanel);
            this._mainRow.Controls.Add(_contentCell);
            this._mainRow.Controls.Add(_rightPanel);
            this._leftPanel.Controls.Add(_navContainer);
            this._leftPanel.Controls.Add(_areasContainer);
            this._contentCell.Controls.Add(_contentPanel);
            this.Controls.Add(_statusBar);

            this._leftPanel.Visible = this._showLeftPanel;
            this._rightPanel.Visible = this._showRightPanel;
            this._statusBar.Visible = this._showStatusBar;

            this._leftPanel.Width = this._leftPanelWidth;
            this._rightPanel.Width = this._rightPanelWidth;

            this._mainTable.CellSpacing = 0;


            this._mainPanel.CssClass = "OfficeWebUI_WorkspaceContainer";
            this._mainTable.CssClass = "OfficeWebUI_Workspace";
            this._leftPanel.CssClass = "OfficeWebUI_WorkspaceLeftPanel";
            this._leftPanel.VerticalAlign = VerticalAlign.Top;
            this._rightPanel.CssClass = "OfficeWebUI_WorkspaceRightPanel";
            this._rightPanel.VerticalAlign = VerticalAlign.Top;
            this._contentCell.CssClass = "OfficeWebUI_WorkspaceContentCell";
            this._contentPanel.CssClass = "OfficeWebUI_WorkspaceContentPanel";
            this._contentCell.VerticalAlign = VerticalAlign.Top;
            this._areasContainer.CssClass = "OfficeWebUI_WorkspaceAreaContainer";
            this._navContainer.CssClass = "OfficeWebUI_WorkspaceNavContainer";
            this._statusBar.CssClass = "OfficeWebUI_WorkspaceStatusBar";

            this._content.InstantiateIn(this._contentPanel);


            Panel ltemp = new Panel();
            ltemp.Style.Add("padding", "2px");
            this._statusBar.Controls.Add(ltemp);

            ltemp.Controls.Add(new Literal { Text="Hello world" });

            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            

            // Persist last item
            
            _ActiveItem.ID = "OfficeWebUI_Workspace_LastItem";
            this.Controls.Add(_ActiveItem);

            String lScript_ActiveItem = "var OfficeWebUI_Workspace_LastItem = \"" + _ActiveItem.ClientID + "\";\n";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OfficeWebUI.Workspace.LastItem", lScript_ActiveItem, true);


            // Persist last area
            
            _ActiveArea.ID = "OfficeWebUI_Workspace_LastArea";
            this.Controls.Add(_ActiveArea);

            String lScript_ActiveArea = "var OfficeWebUI_Workspace_LastArea = \"" + _ActiveArea.ClientID + "\";\n";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OfficeWebUI.Workspace.LastArea", lScript_ActiveArea, true);


            

            foreach (Area lAreaSrc in this._areas)
            {
                AreaRenderer lArea = new AreaRenderer(lAreaSrc, this._navContainer);
                _areasContainer.Controls.Add(lArea);                
            }



            /* Support Async page load caused by Ajax, so try to find a ScriptManager */

            Boolean isUsingAjax = false;
            foreach (Control lctrl in Page.Form.Controls)
            {
                if (lctrl.GetType().ToString() == "System.Web.UI.ScriptManager")
                    isUsingAjax = true;
            }

            if (isUsingAjax)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OfficeWebUI.Workspace.AjaxPageLoadSupport", "<script>try { Sys.Application.add_load(OfficeWebUI.Workspace._AjaxLoadSupport); } catch(e) { alert(e); }</script>");
        }

        

        #region ICompositeControlDesignerAccessor Membres

        public void RecreateChildControls()
        {
            base.ChildControlsCreated = true;
        }

        #endregion
    }

    public class OfficeWorkspace_Designer : CompositeControlDesigner
    {
        private DesignerActionListCollection _actionLists = null;

        public override string GetDesignTimeHtml()
        {
            String lReturn = String.Empty;
            OfficeWorkspace lControl = (Component as OfficeWorkspace);

            return "<div style='height:200px; padding:5px; border:1px solid #C0C0C0; font-family:Verdana; font-size:8pt;'><b>OfficeWebUI:Workspace</b> [" + lControl.ID + "]</div>";
        }

        // Do not allow direct resizing of the control
        public override bool AllowResize
        {
            get { return false; }
        }
    }
}
