using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;
using System.Web.UI.HtmlControls;
using OfficeWebUI.MessageBox;

namespace OfficeWebUI
{
    [DesignerAttribute(typeof(MessageBox_Designer))]
    [ToolboxData("<{0}:OfficeMessageBox runat=server></{0}:OfficeMessageBox>")]
    public class OfficeMessageBox : ControlAncestor, INamingContainer
    {
        #region Private

        private Panel _panelShadow;
        private Panel _panelPopupHolder;
        private Panel _panelPopup;
        private Panel _panelTitle;
        private Panel _panelClose;
        private Label _closeLabel;
        private Image _closeImage;
        private Panel _contentContainer;
        private Panel _panelFooter;
        private Label _LabelText;

        private OfficeButton _buttonOk;
        private OfficeButton _buttonCancel;
        private OfficeButton _buttonYes;
        private OfficeButton _buttonNo;        


        private String _CancelButtonText = "Cancel";
        private String _OkButtonText = "Ok";

        private String _text = String.Empty;
        private String _title = String.Empty;
        private Unit _width = 250;
        private MessageBoxReturnType _return;

        private MessageBoxType _displayType = MessageBoxType.Default;
        private MessageBoxButtonsType _buttonsType = MessageBoxButtonsType.OkCancel;

        public override Boolean Visible
        {
            get { return (ViewState["Visible"] == null) ? false : (Boolean)ViewState["Visible"]; }
            set { ViewState["Visible"] = value; }
        }

        #endregion

        #region Public

        public event EventHandler Return;

        public String OkButtonText
        {
            get { return this._OkButtonText; }
            set { this._OkButtonText = value; }
        }

        public String CancelButtonText
        {
            get { return this._CancelButtonText; }
            set { this._CancelButtonText = value; }
        }

        public MessageBoxReturnType ReturnValue
        {
            get { return this._return; }
        }

        public String Title
        {
            get { return this._title; }
            set { this._title = value; }
        }

        public String Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        public MessageBoxType MessageBoxType
        {
            get { return this._displayType; }
            set { this._displayType = value; }
        }

        public MessageBoxButtonsType ButtonsType
        {
            get { return this._buttonsType; }
            set { this._buttonsType = value; }
        }

        #endregion

        public OfficeMessageBox()
        {
            //(HttpContext.Current.Handler as Page).Controls.Add(this);
        }

        protected override void OnInit(EventArgs e)
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
                throw new Exception("You must include an OfficeWebUIManager on your page to use OfficeWebUI components");

            Manager Manager = (Manager)HttpContext.Current.Items["OfficeWebUI_ManagerObj"];

            //if (HttpContext.Current.Items.Contains("OfficeWebUI_MessageBoxManager"))
            //    throw new Exception("There are more than one OfficeWebUI.MessageBoxManager on the same page, a page can only have one OfficeWebUI.MessageBoxManager, please remove one of them");

            //HttpContext.Current.Items.Add("OfficeWebUI_MessageBoxManager", "true");

            //if (Parent.GetType().ToString().ToLower() != "system.web.ui.htmlcontrols.htmlform")
            //    throw new Exception("The OfficeWebUI.OfficePopupManager must be at the root of page's form");

            ScriptManager _scriptManager = ScriptManager.GetCurrent(Page);
            if (_scriptManager != null)
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.MessageBox.js", this.GetType().Assembly.GetName().Name));
            else
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.MessageBox.js"); 

            String lScript = "var " + this.ID + " = OfficeWebUI.MessageBox;";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OfficeWebUI.MessageBoxManager.Namer", lScript, true);


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

            _closeLabel = new Label();
            _closeLabel.CssClass = "PopupTitleSpan";
            _closeLabel.Text = this._title;
            _panelTitle.Controls.Add(_closeLabel);

            _panelClose = new Panel();
            _panelClose.CssClass = "OfficeWebUI_PopupClosePanel";
            _panelTitle.Controls.Add(_panelClose);

            //_closeImage = new Image();
            //_closeImage.Attributes.Add("onclick", "OfficeWebUI.Popup.Close();");
            //_closeImage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.Popup_CloseButton.png");
            //_panelClose.Controls.Add(_closeImage);


            Panel lPanelClear = new Panel();
            lPanelClear.Style.Add("clear", "both");
            _panelTitle.Controls.Add(lPanelClear);

            _contentContainer = new Panel();
            _contentContainer.ID = this.ID + "_PanelContainer";
            _contentContainer.CssClass = "OfficeWebUI_PopupContentContainer";
            _panelPopup.Controls.Add(_contentContainer);

            Table lTable = new Table();
            this._contentContainer.Controls.Add(lTable);

            TableRow lRow = new TableRow();
            lTable.Controls.Add(lRow);

            TableCell lCellImage = new TableCell();
            lCellImage.VerticalAlign = VerticalAlign.Top;
            lRow.Controls.Add(lCellImage);

            Image lMessageImage = new Image();
            lCellImage.Controls.Add(lMessageImage);

            switch (this._displayType)
            {
                case MessageBoxType.Default:
                    break;
                case MessageBoxType.Warn:
                    lMessageImage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.MessageBox_Warn.png");
                    break;
                case MessageBoxType.Info:
                    lMessageImage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.MessageBox_Info.png");
                    break;
                case MessageBoxType.Question:
                    lMessageImage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.MessageBox_Question.png");
                    break;
                case MessageBoxType.Error:
                    lMessageImage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OfficeWebUI.Resources.Common.Image.MessageBox_Error.png");
                    break;
                default:
                    break;
            }

            TableCell lCellText = new TableCell();
            lRow.Controls.Add(lCellText);

            _LabelText = new Label();
            _LabelText.Text = this._text;
            lCellText.Controls.Add(_LabelText);


            _panelFooter = new Panel();
            _panelFooter.ID = this.ID + "_FooterPanel";
            _panelFooter.CssClass = "OfficeWebUI_PopupFooterPanel";
            _panelPopup.Controls.Add(_panelFooter);



            _buttonOk = new OfficeButton();
            _buttonOk.ID = this.ID + "_ButtonOK";
            _buttonOk.DisplayType = ButtonDisplayType.TextOnly;
            _buttonOk.Text = Manager.Resources.Text_Ok;
            _buttonOk.Click += new EventHandler(_buttonOk_Click);
            

            _buttonYes = new OfficeButton();
            _buttonYes.ID = this.ID + "_ButtonYES";
            _buttonYes.DisplayType = ButtonDisplayType.TextOnly;
            _buttonYes.Text = Manager.Resources.Text_Yes;
            _buttonYes.Click += new EventHandler(_buttonYes_Click);

            _buttonNo = new OfficeButton();
            _buttonNo.ID = this.ID + "_ButtonNO";
            _buttonNo.DisplayType = ButtonDisplayType.TextOnly;
            _buttonNo.Text = Manager.Resources.Text_No;
            _buttonNo.Click += new EventHandler(_buttonNo_Click);

            _buttonCancel = new OfficeButton();
            _buttonCancel.ID = this.ID + "_ButtonCANCEL";
            _buttonCancel.DisplayType = ButtonDisplayType.TextOnly;
            _buttonCancel.Text = Manager.Resources.Text_Cancel;
            _buttonCancel.Click += new EventHandler(_buttonCancel_Click);

            switch (this._buttonsType)
            {
                case MessageBoxButtonsType.Ok:

                    _panelFooter.Controls.Add(_buttonOk);
                    _panelFooter.Controls.Add(new Literal { Text = "&nbsp;" });

                    break;
                case MessageBoxButtonsType.YesNo:

                    _panelFooter.Controls.Add(_buttonYes);
                    _panelFooter.Controls.Add(new Literal { Text = "&nbsp;" });
                    _panelFooter.Controls.Add(_buttonNo);

                    break;
                case MessageBoxButtonsType.YesNoCancel:

                    _panelFooter.Controls.Add(_buttonYes);
                    _panelFooter.Controls.Add(new Literal { Text = "&nbsp;" });
                    _panelFooter.Controls.Add(_buttonNo);
                    _panelFooter.Controls.Add(new Literal { Text = "&nbsp;" });
                    _panelFooter.Controls.Add(_buttonCancel);

                    break;
                case MessageBoxButtonsType.OkCancel:

                    _panelFooter.Controls.Add(_buttonOk);
                    _panelFooter.Controls.Add(new Literal { Text = "&nbsp;" });
                    _panelFooter.Controls.Add(_buttonCancel);

                    break;
                default:
                    break;
            }


            base.OnInit(e);
        }

        void _buttonYes_Click(object sender, EventArgs e)
        {
            this._return = MessageBoxReturnType.Yes;
            this.Visible = false;

            if (this.Return != null)
            {
                this.Return(this, new EventArgs());
            }
        }

        void _buttonNo_Click(object sender, EventArgs e)
        {
            this._return = MessageBoxReturnType.No;
            this.Visible = false;

            if (this.Return != null)
            {
                this.Return(this, new EventArgs());
            }
        }

        void _buttonCancel_Click(object sender, EventArgs e)
        {
            this._return = MessageBoxReturnType.Cancel;
            this.Visible = false;

            if (this.Return != null)
            {
                this.Return(this, new EventArgs());
            }
        }

        void _buttonOk_Click(object sender, EventArgs e)
        {
            this._return = MessageBoxReturnType.Ok;
            this.Visible = false;

            if (this.Return != null)
            {
                this.Return(this, new EventArgs());
            }
        }

        protected override void CreateChildControls()
        {
            _panelPopup.Visible = this.Visible;
            _panelShadow.Visible = Visible;


            
            if (this.Visible)
            {
                String lScript_Init = "$(document).ready(function() { OfficeWebUI.MessageBox.InitPopup(\"" + this.ID + "\"); });";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OfficeWebUI.MessageBox.InitPopup", lScript_Init, true);
            }

            base.CreateChildControls();
        }

       

        public void Show()
        {
            this.Visible = true;
        }


    }

    
}
