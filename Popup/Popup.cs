using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;
using System.Web.UI.HtmlControls;
using OfficeWebUI.Popup;

namespace OfficeWebUI
{
    [DesignerAttribute(typeof(OfficePopup_Designer))]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxData("<{0}:OfficePopup runat=server></{0}:OfficePopup>")]
    public class OfficePopup : ControlAncestor, INamingContainer, ICompositeControlDesignerAccessor
    {
        #region Private

        private Panel _panelShadow;
        private Panel _panelPopupHolder;
        private Panel _panelPopup;
        private Panel _panelTitle;
        private Panel _panelClose;
        private Label _titleLabel = new Label();
        private Image _closeImage;
        private HtmlGenericControl _frame;
        private Panel _contentContainer;
        private ITemplate _content;
        private Panel _panelFooter;
        private OfficeButton _buttonOk;
        private OfficeButton _buttonCancel;

        private Boolean _ShowOkButton = true;
        private Boolean _showCancelButton = true;

        private String _title = String.Empty;
        private Unit _width = 600;
        private Unit _height = 500;

        #endregion

        #region Public

        public event EventHandler ClickOk;
        public event EventHandler ClickCancel;

        [PersistenceMode(PersistenceMode.InnerProperty),
         TemplateContainer(typeof(TemplateControl)),
         TemplateInstance(TemplateInstance.Single)]
        public ITemplate Content
        {
            get { return this._content; }
            set { this._content = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Unit Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Unit Height
        {
            get { return this._height; }
            set { this._height = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public String Title
        {
            get { return this._titleLabel.Text; }
            set { this._titleLabel.Text = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public override bool Visible
        {
            get { return (ViewState["Visible"] != null) ? Convert.ToBoolean(ViewState["Visible"]) : false; }
            set { ViewState["Visible"] = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Boolean ShowOkButton
        {
            get { return this._ShowOkButton; }
            set { this._ShowOkButton = value; }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Boolean ShowCancelButton
        {
            get { return this._showCancelButton; }
            set { this._showCancelButton = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
                throw new Exception("You must include an OfficeWebUIManager on your page to use OfficeWebUI components");

            Manager Manager = (Manager)HttpContext.Current.Items["OfficeWebUI_ManagerObj"];

            ScriptManager _scriptManager = ScriptManager.GetCurrent(Page);
            if (_scriptManager != null)
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.Popup.js", this.GetType().Assembly.GetName().Name));
            else
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.Popup.js");

            _panelShadow = new Panel();
            _panelShadow.ID = this.ID + "_PanelShadow";
            _panelShadow.CssClass = "OfficeWebUI_PopupShadow";
            this.Controls.Add(_panelShadow);

            _panelPopupHolder = new Panel();
            _panelPopupHolder.ID = this.ID + "_PanelHolder";
            _panelPopupHolder.CssClass = "OfficeWebUI_PopupHolder";
            this.Controls.Add(_panelPopupHolder);

            _panelPopup = new Panel();
            _panelPopup.ID = this.ID + "_PanelPopup";
            _panelPopup.CssClass = "OfficeWebUI_Popup _OfficeWebUI_Popup_" + this.ID;
            _panelPopup.Width = this._width;
            _panelPopupHolder.Controls.Add(_panelPopup);

            _panelTitle = new Panel();
            _panelTitle.CssClass = "OfficeWebUI_PopupTitle";
            _panelPopup.Controls.Add(_panelTitle);

            _titleLabel.CssClass = "PopupTitleSpan";
            _panelTitle.Controls.Add(_titleLabel);

            _panelClose = new Panel();
            _panelClose.CssClass = "OfficeWebUI_PopupClosePanel";
            _panelTitle.Controls.Add(_panelClose);

            _closeImage = new Image();
            _closeImage.Attributes.Add("onclick", "OfficeWebUI.Popup.Close();");
            _closeImage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.Popup_CloseButton.png");
            _panelClose.Controls.Add(_closeImage);


            Panel lPanelClear = new Panel();
            lPanelClear.Style.Add("clear", "both");
            _panelTitle.Controls.Add(lPanelClear);

            _contentContainer = new Panel();
            _contentContainer.ID = this.ID + "_PanelContainer";
            _contentContainer.CssClass = "OfficeWebUI_PopupContentContainer";
            _contentContainer.Height = this._height;
            _panelPopup.Controls.Add(_contentContainer);

            this._content.InstantiateIn(this._contentContainer);


            _panelFooter = new Panel();
            _panelFooter.ID = this.ID + "_FooterPanel";
            _panelFooter.CssClass = "OfficeWebUI_PopupFooterPanel";
            _panelPopup.Controls.Add(_panelFooter);

            _buttonOk = new OfficeButton();
            _buttonOk.ID = this.ID + "_ButtonOK";
            _buttonOk.DisplayType = ButtonDisplayType.TextOnly;
            _buttonOk.Text = Manager.Resources.Text_Ok;
            _buttonOk.Click += new EventHandler(_buttonOk_Click);
            _buttonOk.Visible = this._ShowOkButton;
            _panelFooter.Controls.Add(_buttonOk);

            _panelFooter.Controls.Add(new Literal { Text = "&nbsp;" });

            _buttonCancel = new OfficeButton();
            _buttonCancel.ID = this.ID + "_ButtonCancel";
            _buttonCancel.DisplayType = ButtonDisplayType.TextOnly;
            _buttonCancel.Text = Manager.Resources.Text_Cancel;
            _buttonCancel.Click += new EventHandler(_buttonCancel_Click);
            _buttonCancel.Visible = this._showCancelButton;
            _panelFooter.Controls.Add(_buttonCancel);

            //_frame = new HtmlGenericControl("iframe");
            //_frame.Attributes.Add("class", "OfficeWebUI_PopupFrame");
            //_frame.Attributes.Add("frameborder", "0");
            //_frame.Style.Add("height", "100%");
            //_frame.Style.Add("width", "100%");
            //_panelPopup.Controls.Add(_frame);

            

            base.OnInit(e);
        }

        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (this.ClickCancel != null)
            {
                this.ClickCancel(this, new EventArgs());
            }
        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            if (this.ClickOk != null)
            {
                this.ClickOk(this, new EventArgs());
            }
        }

        protected override void CreateChildControls()
        {
            _panelPopup.Visible = Visible;
            _panelShadow.Visible = Visible;

            if (this.Visible)
            {
                String lScript_Init = "$(document).ready(function() { OfficeWebUI.Popup.InitPopup(\"" + this.ID + "\"); });";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OfficeWebUI.Popup.InitPopup", lScript_Init, true);
            }

            base.CreateChildControls();
        }

        public void Show()
        {
            this.Visible = true;
        }

        public void Hide()
        {
            this.Visible = false;
        }

        #region ICompositeControlDesignerAccessor Membres

        public void RecreateChildControls()
        {
            base.ChildControlsCreated = true;
        }

        #endregion
    }

    
}
