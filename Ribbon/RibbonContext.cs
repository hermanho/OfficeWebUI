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
    [ToolboxData("<{0}:RibbonContext runat=server></{0}:RibbonContext>")]
    [ParseChildren(true, "Tabs")]
    [PersistChildren(false)]
    public class RibbonContext : ControlAncestor, INamingContainer, ICompositeControlDesignerAccessor
    {
        #region Private

        private List<RibbonTab> _tabs = new List<RibbonTab>();
        private String _contextColor = "#ffffff";
        private String _text = String.Empty;
        private String _id = String.Empty;

        internal Panel _Context;

        internal Control _ContextBar;
        internal Control _TabsContainer;

        #endregion

        #region Public

        public String ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<RibbonTab> Tabs
        {
            get { return this._tabs; }
        }

        public String ContextColor
        {
            get { return this._contextColor; }
            set { this._contextColor = value; }
        }

        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        #endregion

        

        protected override void OnInit(EventArgs e)
        {
            _ContextBar = (Parent as OfficeRibbon)._ContextBar;
            _TabsContainer = (Parent as OfficeRibbon)._TabsContainer;

            _Context = new Panel();
            _Context.Attributes.Add("ContextID", this.ID);
            _Context.CssClass = "RibbonContext " + this.ID;
            _Context.Style.Add("background-color", "" + this.ContextColor + "");
            _Context.Style.Add("border-top", "3px solid " + this.ContextColor);
            _Context.Style.Add("border-left", "1px solid " + this.ContextColor);
            _Context.Style.Add("border-right", "1px solid " + this.ContextColor);
            _Context.Style.Add("float", "left");

            if (this.ContextColor.ToLower() == "transparent")
            {
                //_Context.Style.Add("background-image", "none");
                _Context.Style.Remove("background-image");
            }

            if (!String.IsNullOrEmpty(this.Text)) {
                _Context.Style.Add("display", "none");
            }

            Panel lContextTitle = new Panel();
            lContextTitle.CssClass = "RibbonContextTitle";
            lContextTitle.Style.Add("clear", "both");
            _Context.Controls.Add(lContextTitle);

            lContextTitle.Controls.Add(new Literal { Text = this.Text });
            
            //lContext.Controls.Add(context);
            _ContextBar.Controls.Add(_Context);

            (Parent as OfficeRibbon)._RebuildContextBar();

            //CreateChildControls();
            EnsureChildControls();
            
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            foreach (RibbonTab tab in this.Tabs)
            {
                tab._TabsContainer = (Parent as OfficeRibbon)._TabsContainer;
                tab._Context = _Context;
                this.Controls.Add(tab);
            }

            base.CreateChildControls();
        }

        public void RecreateChildControls()
        {
            base.ChildControlsCreated = true;
            //EnsureChildControls();
        }
    }
}
