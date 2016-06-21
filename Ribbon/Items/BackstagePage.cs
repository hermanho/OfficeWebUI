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
    [ToolboxData("<{0}:BackstagePage runat=server></{0}:BackstagePage>")]
    [PersistChildren(false)]
    public class BackstagePage
    {
        #region Private

        private String _id = String.Empty;
        private String _text = String.Empty;
        private String _usercontrol = null;

        #endregion

        #region Public

        [Browsable(true)]
        [Category("Appearance")]
        public String UserControl
        {
            get { return this._usercontrol; }
            set { this._usercontrol = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        #endregion
    }

    internal class BackstagePageRenderer : ControlAncestor
    {
        private BackstagePage _src = null;
        private Control _pagesColumn = null;
        private Control _contentColumn = null;
        private String _id = String.Empty;

        public BackstagePageRenderer(BackstagePage src, Control pagesColumn, Control contentColumn)
        {
            this._src = src;
            this._pagesColumn = pagesColumn;
            this._contentColumn = contentColumn;
            this._id = src.ID;
        }

        protected override void OnInit(EventArgs e)
        {
            if (String.IsNullOrEmpty(this._id))
                throw new Exception("Please set ID for all backstagePage, if you don't, server events will not fire.");

            Panel lPanelPageTitle = new Panel();
            lPanelPageTitle.CssClass = "BackstagePageTitle";
            lPanelPageTitle.Attributes.Add("AssociatedPage", this._id);
            _pagesColumn.Controls.Add(lPanelPageTitle);

            Literal lPageTitle = new Literal();
            lPageTitle.Text = _src.Text;
            lPanelPageTitle.Controls.Add(lPageTitle);



            Panel lPanelPageContent = new Panel();
            lPanelPageContent.CssClass = "BackstagePageContent";
            lPanelPageContent.Attributes.Add("AssociatedPage", this._id);
            _contentColumn.Controls.Add(lPanelPageContent);


            Control lctrl;
            try
            {
                lctrl = Page.LoadControl(_src.UserControl);
                lctrl.ID = this._id + "_backstagePage";
                lPanelPageContent.Controls.Add(lctrl);
            }
            catch
            {
                Literal lError = new Literal() { Text = "Unable to load the specified control :" + _src.UserControl };
                lPanelPageContent.Controls.Add(lError);
            }

            base.OnInit(e);
        }


    }
}
