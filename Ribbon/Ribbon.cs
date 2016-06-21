using System;
using System.Web.UI.Design;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Ribbon;
using OfficeWebUI.Common;
using System.Web.UI.Design.WebControls;
using System.ComponentModel.Design;
using System.IO;

namespace OfficeWebUI
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:OfficeRibbon runat=server></{0}:OfficeRibbon>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [DesignerAttribute(typeof(OfficeRibbon_Designer))]
    public class OfficeRibbon : Panel, INamingContainer, ICompositeControlDesignerAccessor
    {
        #region Private

        private List<RibbonContext> _contexts = new List<RibbonContext>();
        private RibbonMenu _ribbonMenu = new RibbonMenu();
        private String _extraText = String.Empty;
        private List<Control> _extraControls = new List<Control>();
        private HiddenField _ActiveTab;
        private Boolean _showHelpButton = false;
        private Boolean _showToggleButton = false;

        private String _appMenuColor = "green";
        private String _appMenuText = "File";
        private ApplicationMenuType _appMenuType = ApplicationMenuType.Default;

        private String _helpClientClick = String.Empty;

        internal Panel _TabsContainer;
        internal Panel _ContextBar;
        internal Panel _baseContext;

        #endregion

        #region Public

        public delegate void HelpClickHandler(object sender, EventArgs e);
        public event HelpClickHandler HelpButtonServerClick;

        [Browsable(true)]
        [Category("Appearance")]
        public String ApplicationMenuColor
        {
            get { return this._appMenuColor; }
            set { this._appMenuColor = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String ApplicationMenuText
        {
            get { return this._appMenuText; }
            set { this._appMenuText = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public ApplicationMenuType ApplicationMenuType
        {
            get { return this._appMenuType; }
            set { this._appMenuType = value; }
        }

        public String HelpButtonClientClick
        {
            get { return this._helpClientClick; }
            set { this._helpClientClick = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<RibbonContext> Contexts
        {
            get { return this._contexts; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public RibbonMenu ApplicationMenu
        {
            get { return this._ribbonMenu; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String ExtraText
        {
            get { return this._extraText; }
            set { this._extraText = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Boolean ShowHelpButton
        {
            get { return this._showHelpButton; }
            set { this._showHelpButton = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Boolean ShowToggleButton
        {
            get { return this._showToggleButton; }
            set { this._showToggleButton = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Control> ExtraControlsArea
        {
            get { return this._extraControls; }
        }

        [Browsable(true)]
        public String SelectTabID
        {
            get { return this._ActiveTab.Value; }
            set { this._ActiveTab.Value = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
                throw new Exception("You must include an OfficeWebUIManager on your page to use OfficeWebUI components");
            
            ScriptManager _scriptManager = ScriptManager.GetCurrent(Page);
            if (_scriptManager != null)
            {
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.Ribbon.js", this.GetType().Assembly.GetName().Name));
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.Ribbon_CollapseGroups.js", this.GetType().Assembly.GetName().Name));
            }
            else
            {
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.Ribbon.js");
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.Ribbon_CollapseGroups.js");
            }

            this.ID = "RibbonBaseContainer";
            this.CssClass = "RibbonBaseContainer";
            //this.Parent.Controls.Add(_baseContext);
            //_baseContext.Controls.Add(this);

            _ContextBar = new Panel();
            _ContextBar.ID = "RibbonContextsContainer";
            _ContextBar.CssClass = "RibbonContextsContainer";
            this.Controls.Add(_ContextBar);

            // Persist current Tab
            _ActiveTab = new HiddenField();
            _ActiveTab.ID = "OfficeWebUI_Ribbon_LastTab";
            this.Controls.Add(_ActiveTab);

            String lScript_ActiveTab = "var OfficeWebUI_Ribbon_LastTab = \"" + _ActiveTab.ClientID + "\";\n";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OfficeWebUI.Ribbon.LastTab", lScript_ActiveTab, true);

            // Persist current Backstage Page
            //HiddenField lActiveBackstagePage = new HiddenField();
            //lActiveBackstagePage.ID = "OfficeWebUI_Ribbon_LastBackstagePage";
            //this.Controls.Add(lActiveBackstagePage);

            //String lScript_ActiveBackstagePage = "var OfficeWebUI_Ribbon_LastBackstagePage = \"" + lActiveBackstagePage.ClientID + "\";\n";
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OfficeWebUI.Ribbon.LastBackstagePage", lScript_ActiveBackstagePage, true);

            //_CreateRibbonElements();
            //EnsureChildControls();

  
            base.OnInit(e);
        }

        //private void _CreateRibbonElements()
        protected override void CreateChildControls()
        {
            

            if ((this._ribbonMenu != null) && (this._appMenuType == ApplicationMenuType.Backstage))
            {
                Panel lBackstageContainer = new Panel();
                lBackstageContainer.CssClass = "RibbonBackstageContainer";
                lBackstageContainer.Style.Add("border-top", "2px solid " + this._appMenuColor);
                this.Controls.Add(lBackstageContainer);
                _BuildBackstage(lBackstageContainer);
            }
            else
            {
                Panel lMenuContainer = new Panel();
                lMenuContainer.CssClass = "RibbonApplicationMenuContainer";
                this.Controls.Add(lMenuContainer);
                _BuildMenu(lMenuContainer);
            }

            _TabsContainer = new Panel();
            _TabsContainer.ID = "RibbonTabsContainer";
            _TabsContainer.CssClass = "RibbonTabsContainer";
            this.Controls.Add(_TabsContainer);
            
            // Add Application menu
            if ((this._appMenuType != ApplicationMenuType.None) && (this._ribbonMenu != null))
                _AddApplicationMenu();

            // Add Help Button
            if (this._showHelpButton)
                _AddHelpButton();

            // Add Toogle Button
            if (this._showToggleButton)
                _AddToogleButton();

            // Show extra text
            if (!String.IsNullOrEmpty(this._extraText))
                _AddExtraText();

            if (this._extraControls.Count > 0)
                _AddExtraControls();


            foreach (RibbonContext context in this._contexts)
            {
                //context._ContextBar = _ContextBar;
                //context._TabsContainer = _TabsContainer;

                this.Controls.Add(context);
            }

            Panel lClear = new Panel();
            lClear.CssClass = "clear";
            lClear.Attributes.Add("k", "clearPanel");
            _ContextBar.Controls.Add(lClear);

            Panel lPanelTooltipContainer = new Panel();
            lPanelTooltipContainer.CssClass = "RibbonTooltipContainer";
            lPanelTooltipContainer.Style.Add("display", "none");
            this.Controls.Add(lPanelTooltipContainer);
           
            //base.CreateChildControls();
        }



        internal void _RebuildContextBar()
        {

            List<Control> lPanelsToRemove = new List<Control>();

            foreach (Control lCtrl in _ContextBar.Controls)
            {
                if ((lCtrl is Panel) && ((lCtrl as Panel).Attributes["k"] == "clearPanel"))
                {
                    lPanelsToRemove.Add(lCtrl);
                }
            }

            foreach (Control lPanelToRemove in lPanelsToRemove)
            {
                _ContextBar.Controls.Remove(lPanelToRemove);
            }

            Panel lClear = new Panel();
            lClear.Attributes.Add("k", "clearPanel");
            lClear.CssClass = "clear";
            _ContextBar.Controls.Add(lClear);
        }

        private void _AddHelpButton()
        {
            Panel lHelpButtonContainer = new Panel();
            lHelpButtonContainer.ID = "RibbonHelpButtonContainer";
            lHelpButtonContainer.CssClass = "RibbonHelpButtonContainer";
            _ContextBar.Controls.Add(lHelpButtonContainer);

            ImageButton lHelpButton = new ImageButton();
            lHelpButton.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.help.png");
            lHelpButtonContainer.Controls.Add(lHelpButton);

            if (!String.IsNullOrEmpty(this._helpClientClick))
            {
                lHelpButton.Attributes.Add("onclick", this._helpClientClick);
            }

            lHelpButton.Click += new ImageClickEventHandler(lHelpButton_Click);
        }

        private void _AddToogleButton()
        {
            Panel lToggleButtonContainer = new Panel();
            lToggleButtonContainer.ID = "RibbonToggleButtonContainer";
            lToggleButtonContainer.CssClass = "RibbonToggleButtonContainer";
            _ContextBar.Controls.Add(lToggleButtonContainer);

            //ImageButton lToggleButton = new ImageButton();
            //lToggleButton.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            //lToggleButton.CssClass = "RibbonToggleButtonUp";

            Image lToggleButton = new Image();
            lToggleButton.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.blank.gif");
            lToggleButton.CssClass = "RibbonToggleButtonUp";
            lToggleButtonContainer.Controls.Add(lToggleButton);

            lToggleButton.Attributes.Add("onclick", "OfficeWebUI.Ribbon.ToggleRibbon(true); return false;");
        }

        private void _AddExtraControls()
        {
            Panel lExtraControlsContainer = new Panel();
            lExtraControlsContainer.ID = "RibbonExtraControlsContainer";
            lExtraControlsContainer.CssClass = "RibbonExtraControlsContainer";
            _ContextBar.Controls.Add(lExtraControlsContainer);

            foreach (Control lItem in this._extraControls)
            {
                lExtraControlsContainer.Controls.Add(lItem);
            }
        }

        private void _AddExtraText()
        {
            Panel lExtraTextContainer = new Panel();
            lExtraTextContainer.ID = "RibbonExtraTextContainer";
            lExtraTextContainer.CssClass = "RibbonExtraTextContainer";
            _ContextBar.Controls.Add(lExtraTextContainer);

            lExtraTextContainer.Controls.Add(new Literal() { Text = this._extraText });
        }

        private void _AddApplicationMenu()
        {
            String lScript = "var OfficeWebUI_Ribbon_AppMenuColor = \"" + this._appMenuColor + "\";";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OfficeWebUI.Ribbon.AppMenuColor", lScript, true);

            Panel lApplicationMenuContainer = new Panel();
            lApplicationMenuContainer.CssClass = "ApplicationButtonContainer";
            lApplicationMenuContainer.Style.Add("float", "left");
            _ContextBar.Controls.Add(lApplicationMenuContainer);

            Panel lApplicationMenu = new Panel();
            lApplicationMenu.Style.Add("background-color", this._appMenuColor);
            lApplicationMenu.Style.Add("border", "1px solid " + this._appMenuColor);
            lApplicationMenu.CssClass = "ApplicationButton";
            lApplicationMenu.Controls.Add(new Literal() { Text = this._appMenuText });
            lApplicationMenuContainer.Controls.Add(lApplicationMenu);

            if (this._appMenuType == ApplicationMenuType.Backstage)
            {
                lApplicationMenu.Attributes.Add("onclick", "OfficeWebUI.Ribbon.ToggleBackstage()");
            }
            else
            {
                lApplicationMenu.Attributes.Add("onclick", "OfficeWebUI.Ribbon.ToggleMenu()");
            }
        }

        private void _BuildMenu(Control container)
        {
            if (this._ribbonMenu != null)
                foreach (AppMenuItem lItem in this._ribbonMenu.MenuItems)
                {
                    AppMenuItemRenderer lItemRenderer = new AppMenuItemRenderer(lItem);
                    container.Controls.Add(lItemRenderer);
                }
        }

        private void _BuildBackstage(Control container)
        {
            Panel lPanelPagesColumn = new Panel();
            lPanelPagesColumn.CssClass = "BackstagePagesColumn";
            container.Controls.Add(lPanelPagesColumn);

            Panel lPanelContentColumn = new Panel();
            lPanelContentColumn.CssClass = "BackstageContentColumn";
            container.Controls.Add(lPanelContentColumn);

            if (this._ribbonMenu != null)
                foreach (BackstagePage lItem in this._ribbonMenu.BackstagePages)
                {
                    BackstagePageRenderer lItemRenderer = new BackstagePageRenderer(lItem, lPanelPagesColumn, lPanelContentColumn);
                    container.Controls.Add(lItemRenderer);
                }
        }

        
        #region public methods to work with the ribbon

        public GroupZone getGroupZone(String id)
        {
            foreach (RibbonContext lContext in this.Contexts)
            {
                foreach (RibbonTab lTab in lContext.Tabs)
                {
                    foreach (RibbonGroup lGroup in lTab.Groups)
                    {
                        foreach (GroupZone lZone in lGroup.Zones)
                        {
                            if (lZone.ID == id)
                            {
                                return lZone;
                            }
                        }
                    }
                }                   
            }
            return null;
        }

        public RibbonGroup getRibbonGroup(String id)
        {
            foreach (RibbonContext lContext in this.Contexts)
            {
                foreach (RibbonTab lTab in lContext.Tabs)
                {
                    foreach (RibbonGroup lGroup in lTab.Groups)
                    {
                        if (lGroup.ID == id)
                        {
                            return lGroup;
                        }
                    }
                }
            }
            return null;
        }

        public RibbonTab getRibbonTab(String id)
        {
            foreach (RibbonContext lContext in this.Contexts)
            {
                foreach (RibbonTab lTab in lContext.Tabs)
                {
                    if (lTab.ID == id)
                    {
                        return lTab;
                    }
                }
            }
            return null;
        }

        public RibbonContext getRibbonContext(String id)
        {
            foreach (RibbonContext lContext in this.Contexts)
            {
                if (lContext.ID == id)
                {
                    return lContext;
                }
            }
            return null;
        }

        public T getRibbonItem<T>(String id) where T : Control
        {
            foreach (RibbonContext lContext in this.Contexts)
            {
                foreach (RibbonTab lTab in lContext.Tabs)
                {
                    foreach (RibbonGroup lGroup in lTab.Groups)
                    {
                        foreach (GroupZone lZone in lGroup.Zones)
                        {
                            foreach (Control lCtrl in lZone.Content)
                            {
                                if ((lCtrl is T) && (lCtrl.ID == id))
                                    return (lCtrl as T);
                            }
                        }
                    }
                }
            }
            return null;
        }


        #endregion

        protected void lHelpButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.HelpButtonServerClick != null)
            {
                HelpButtonServerClick(sender, e);
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


