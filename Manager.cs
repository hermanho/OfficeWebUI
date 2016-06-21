using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;
using System.Web.UI.Design.WebControls;
using System.Web.UI.HtmlControls;

#region Common
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.RTL.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.Ribbon.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.ListView.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.Chrome.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.Workspace.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.Chrome.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.Button.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.Popup.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.Combobox.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Common.Style.ColorPicker.css", "text/css", PerformSubstitution = true)]

[assembly: WebResource("OfficeWebUI.Resources.Common.Image.help.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Image.blank.gif", "image/gif")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Image.arrow_dr2.gif", "image/gif")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Image.Popup_CloseButton.png", "image/png")]

[assembly: WebResource("OfficeWebUI.Resources.Common.Image.MessageBox_Error.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Image.MessageBox_Info.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Image.MessageBox_Question.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Image.MessageBox_Warn.png", "image/png")]

[assembly: WebResource("OfficeWebUI.Resources.Common.Javascript.JQuery.js", "text/javascript")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Javascript.OfficeWebUI.js", "text/javascript")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Javascript.Ribbon.js", "text/javascript")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Javascript.Ribbon_CollapseGroups.js", "text/javascript")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Javascript.Workspace.js", "text/javascript")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Javascript.Button.js", "text/javascript")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Javascript.Popup.js", "text/javascript")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Javascript.Combobox.js", "text/javascript")]
[assembly: WebResource("OfficeWebUI.Resources.Common.Javascript.MessageBox.js", "text/javascript")]
#endregion

#region Wave
[assembly: WebResource("OfficeWebUI.Resources.Wave.Style.Ribbon.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Wave.Image.Ribbon_Background_AppMenu.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Wave.Image.Ribbon_BackgroundContext.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Wave.Image.Ribbon_Background.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Wave.Image.Ribbon_Tabline.png", "image/png")]
#endregion

#region Office 2010 Silver
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Style.Ribbon.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Style.ListView.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Style.Chrome.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Style.Workspace.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Style.Button.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Style.Popup.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Style.Combobox.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Style.ColorPicker.css", "text/css", PerformSubstitution = true)]

[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Chrome_RibbonBackgroundLayer.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_BackgroundTooltip.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_Background_AppMenu.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_BackgroundContext.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_Background.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_Tabline.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_BackgroundBackstage.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_BackgroundBackstagePageHover.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_BackgroundBackstagePageActive.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.ToggleRibbon_Down.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.ToggleRibbon_Up.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.DropDownMenu_Arrow.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_GroupExpand.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Workspace_StatusBar.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Combobox_Arrow.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Combobox_ArrowHover.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Button_Background.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Silver.Image.Ribbon_GroupCollapsed_Icon.png", "image/png")]
#endregion

#region Office 2010 Blue
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Style.Ribbon.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Style.ListView.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Style.Chrome.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Style.Workspace.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Style.Button.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Style.Popup.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Style.Combobox.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Style.ColorPicker.css", "text/css", PerformSubstitution = true)]

[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Chrome_RibbonBackgroundLayer.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_BackgroundTooltip.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_Background_AppMenu.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_BackgroundContext.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_Background.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_Tabline.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_BackgroundBackstage.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_BackgroundBackstagePageHover.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_BackgroundBackstagePageActive.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.ToggleRibbon_Down.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.ToggleRibbon_Up.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.DropDownMenu_Arrow.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_GroupExpand.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Workspace_StatusBar.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Combobox_Arrow.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Combobox_ArrowHover.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Button_Background.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Office2010Blue.Image.Ribbon_GroupCollapsed_Icon.png", "image/png")]
#endregion

#region Windows 7
[assembly: WebResource("OfficeWebUI.Resources.Windows7.Style.Ribbon.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("OfficeWebUI.Resources.Windows7.Image.Ribbon_Background_AppMenu.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Windows7.Image.Ribbon_BackgroundContext.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Windows7.Image.Ribbon_Background.png", "image/png")]
[assembly: WebResource("OfficeWebUI.Resources.Windows7.Image.Ribbon_Tabline.png", "image/png")]
#endregion

namespace OfficeWebUI
{
    [ToolboxData("<{0}:Manager runat=server></{0}:Manager>")]
    [DesignerAttribute(typeof(Manager_Designer))]
    public class Manager : ControlAncestor, INamingContainer
    {
        private Theme _theme = Theme.Office2010Silver;
        private String _customCSS = String.Empty;
        private Boolean _chomeUI = false;
        private Boolean _jquery = false;
        private DocumentDirection _dir = DocumentDirection.LTR;
        private Resources _resources = new Resources();
        private ScriptManager _scriptManager;

        [Category("Appearance")]
        public Theme UITheme
        {
            get { return this._theme; }
            set { this._theme = value; }
        }

        [Category("Appearance")]
        public String CustomCSSFile
        {
            get { return this._customCSS; }
            set { this._customCSS = value; }
        }

        [Category("Appearance")]
        public DocumentDirection DirectionMode
        {
            get { return this._dir; }
            set { this._dir = value; }
        }

        public Boolean ChromeUI
        {
            get { return this._chomeUI; }
            set { this._chomeUI = value; if (value) _ActivateChrome(); }
        }

        public Boolean IncludeJQuery
        {
            get { return this._jquery; }
            set { this._jquery = value; }
        }

        public Resources Resources
        {
            get { return this._resources; }
        }

        protected override void OnInit(EventArgs e)
        {
            _scriptManager = ScriptManager.GetCurrent(Page);

            //if (HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
            //    throw new Exception("There are more than one OfficeWebUI.Manager on the same page, a page can only have one OfficeWebUI.Manager, please remove one of them");
            if (HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
            {
                HttpContext.Current.Items.Remove("OfficeWebUI_Manager");
                HttpContext.Current.Items.Remove("OfficeWebUI_Manager_UITheme");
                HttpContext.Current.Items.Remove("OfficeWebUI_ManagerObj");
            }
            HttpContext.Current.Items.Add("OfficeWebUI_Manager", "true");
            HttpContext.Current.Items.Add("OfficeWebUI_Manager_UITheme", this._theme);
            HttpContext.Current.Items.Add("OfficeWebUI_ManagerObj", this);


            // Import StyleSheets
            Utils.AddStylesheetResource(this, "OfficeWebUI.Resources.Common.Style.Ribbon.css");
            Utils.AddStylesheetResource(this, "OfficeWebUI.Resources.Common.Style.ListView.css");
            Utils.AddStylesheetResource(this, "OfficeWebUI.Resources.Common.Style.Workspace.css");
            Utils.AddStylesheetResource(this, "OfficeWebUI.Resources.Common.Style.Button.css");
            Utils.AddStylesheetResource(this, "OfficeWebUI.Resources.Common.Style.Popup.css");
            Utils.AddStylesheetResource(this, "OfficeWebUI.Resources.Common.Style.Combobox.css");
            Utils.AddStylesheetResource(this, "OfficeWebUI.Resources.Common.Style.ColorPicker.css");

            if (_chomeUI)
                Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.Common.Style.Chrome.css", this._theme.ToString()));

            if (this._theme != Theme.Custom)
            {
                Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.{0}.Style.Ribbon.css", this._theme.ToString()));
                Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.{0}.Style.ListView.css", this._theme.ToString()));
                Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.{0}.Style.Workspace.css", this._theme.ToString()));
                Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.{0}.Style.Button.css", this._theme.ToString()));
                Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.{0}.Style.Popup.css", this._theme.ToString()));
                Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.{0}.Style.Combobox.css", this._theme.ToString()));
                Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.{0}.Style.ColorPicker.css", this._theme.ToString()));

                if (_chomeUI)
                    Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.{0}.Style.Chrome.css", this._theme.ToString()));
            }
            else
            {
                Utils.AddStylesheetFile(this, this._customCSS);
            }


            if (this._jquery)
            {
                if (_scriptManager != null)
                    _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.JQuery.js", this.GetType().Assembly.GetName().Name));
                else
                    Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.JQuery.js");
            }
            //Page.Header.Controls.Add(new Literal() { Text = "<script src=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.JQuery.js") + "\"></script>" });

            //Page.Header.Controls.Add(new Literal() { Text = "<script src=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.OfficeWebUI.js") + "\"></script>" });
            if (_scriptManager != null)
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.OfficeWebUI.js", this.GetType().Assembly.GetName().Name));
            else
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.OfficeWebUI.js");

            if (this._dir == DocumentDirection.RTL)
                Utils.AddStylesheetResource(this, "OfficeWebUI.Resources.Common.Style.RTL.css");

            base.OnInit(e);
        }


        private void _ActivateChrome()
        {
            if (this.Page != null)
            {
                Utils.AddStylesheetResource(this, "OfficeWebUI.Resources.Common.Style.Chrome.css");
                Utils.AddStylesheetResource(this, String.Format("OfficeWebUI.Resources.{0}.Style.Chrome.css", this._theme.ToString()));
            }
        }

        /// <summary>
        /// Returns the page's Ribbon if there is any
        /// </summary>
        /// <returns></returns>
        public OfficeRibbon getPageRibbon()
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_RibbonObj"))
                return null;
            else
                return (OfficeRibbon)HttpContext.Current.Items["OfficeWebUI_RibbonObj"];
        }

        /// <summary>
        /// Returns the page's Workspace if there is any
        /// </summary>
        /// <returns></returns>
        public OfficeWorkspace getPageWorkspace()
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_WorkspaceObj"))
                return null;
            else
                return (OfficeWorkspace)HttpContext.Current.Items["OfficeWebUI_WorkspaceObj"];
        }

        //public void AddInitScript(String scr)
        //{
        //    _AddInitScript(scr);
        //}

        //private void _AddStyleSheet(Type ObjectType, String URI)
        //{
        //    String includeLocation = Page.ClientScript.GetWebResourceUrl(ObjectType, URI);

        //    HtmlLink HtmlLink = new HtmlLink();
        //    HtmlLink.Href = includeLocation;
        //    HtmlLink.Attributes.Add("rel", "stylesheet");
        //    HtmlLink.Attributes.Add("type", "text/css");

        //    // Add the HtmlLink to the Head section of the page.
        //    Page.Header.Controls.Add(HtmlLink);
        //}

        //private void _AddScriptFile(Type ObjectType, String URI)
        //{
        //    String includeLocation = Page.ClientScript.GetWebResourceUrl(ObjectType, URI);

        //    Literal HtmlLiteral = new Literal();
        //    HtmlLiteral.Text = "<script src=\"" + includeLocation + "\"></script>";

        //    Page.Header.Controls.Add(HtmlLiteral);
        //}

        //private void _AddInitScript(String script)
        //{

        //    Literal HtmlLiteral = new Literal();
        //    HtmlLiteral.Text = "<script>$(document).ready(function() {%script%});</script>".Replace("%script%", script);

        //    Page.Header.Controls.Add(HtmlLiteral);
        //}
    }


    public class Manager_Designer : CompositeControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            String lReturn = String.Empty;
            Manager lControl = (Component as Manager);

            return "<div style='background:#B0B8C0; display:inline-block; padding:5px; border:1px solid #777; font-family:Verdana; font-size:8pt; color:#fff;'><b>OfficeWebUI:Manager</b> [" + lControl.UITheme + "]</div>";
        }

        // Do not allow direct resizing of the control
        public override bool AllowResize
        {
            get { return false; }
        }

    }
}
