using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeWebUI.Common;
using OfficeWebUI.Button;
using System.Web.UI.HtmlControls;

namespace OfficeWebUI
{
    [DefaultProperty("Text")]
    [ParseChildren(true, "Items")]
    [ToolboxData("<{0}:OfficeCombobox runat=server></{0}:OfficeCombobox>")]
    public class OfficeCombobox : ControlAncestor, INamingContainer, IPostBackEventHandler
    {

        #region Private

        private Panel _panel;
        private Panel _dropDown;
        private Panel _selectedItem;
        private List<ListItem> _items = new List<ListItem>();
        private Unit _width = 200;
        private HiddenField _selectedValue;
        private HiddenField _selectedText;

        #endregion

        #region Public

        public event EventHandler Click;

        [Browsable(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<ListItem> Items
        {
            get { return this._items; }
        }

        [Browsable(true)]
        public Unit Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        public String SelectedValue
        {
            get { return this._selectedValue.Value; }
        }

        public String SelectedText
        {
            get { return this._selectedText.Value; }
        }

        #endregion


        protected override void OnInit(EventArgs e)
        {
            if (!HttpContext.Current.Items.Contains("OfficeWebUI_Manager"))
                throw new Exception("You must include an OfficeWebUIManager on your page to use OfficeWebUI components");
            
            ScriptManager _scriptManager = ScriptManager.GetCurrent(Page);
            if (_scriptManager != null)
                _scriptManager.Scripts.Add(new ScriptReference("OfficeWebUI.Resources.Common.Javascript.Combobox.js", this.GetType().Assembly.GetName().Name));
            else
                Page.ClientScript.RegisterClientScriptResource(this.GetType(), "OfficeWebUI.Resources.Common.Javascript.Combobox.js");

            PostBackOptions p = new PostBackOptions(this);

            this._panel = new Panel();
            _panel.ID = this.ID + "_panel";
            _panel.CssClass = "OfficeWebUI_Combobox";
            _panel.Width = this._width;
            this.Controls.Add(_panel);

            this._selectedText = new HiddenField();
            _selectedText.ID = this.ID + "_selectedText";
            _panel.Controls.Add(_selectedText);


            this._selectedValue = new HiddenField();
            _selectedValue.ID = this.ID + "_selectedValue";
            _panel.Controls.Add(_selectedValue);


            _panel.Attributes.Add("Internal_SelectedTextID", _selectedText.ClientID);
            _panel.Attributes.Add("Internal_SelectedValueID", _selectedValue.ClientID);

            this._selectedItem = new Panel();
            _selectedItem.CssClass = "OfficeWebUI_Combobox_Selected";
            _panel.Controls.Add(_selectedItem);

            this._dropDown = new Panel();
            _dropDown.ID = this.ID + "_dropdown";
            _dropDown.Width = this._width;
            _dropDown.CssClass = "OfficeWebUI_Combobox_DropDown";
            _panel.Controls.Add(_dropDown);

            if (_items.Count != 0)
            {

                
                Boolean isFirstSelected = false;
                foreach (ListItem lItem in this._items)
                {
                    if ((isFirstSelected == false) && (lItem.Selected))
                    {
                        _selectedItem.Controls.Add(new Literal { Text = lItem.Text });
                        this._selectedValue.Value = lItem.Value;
                        this._selectedText.Value = lItem.Text;
                        isFirstSelected = true;
                    }

                    Panel lItemPanel = new Panel();
                    lItemPanel.Attributes.Add("InternalValue", lItem.Value);
                    lItemPanel.Attributes.Add("InternalText", lItem.Text);
                    lItemPanel.CssClass = "OfficeWebUI_Combobox_Item";
                    _dropDown.Controls.Add(lItemPanel);

                    lItemPanel.Controls.Add(new Literal { Text = lItem.Text });
                }

                if (!isFirstSelected)
                {
                    _selectedItem.Controls.Add(new Literal { Text = this.Items[0].Text });
                    this._selectedValue.Value = this.Items[0].Value;
                    this._selectedText.Value = this.Items[0].Text;
                    isFirstSelected = true;
                }

                

                
            }

            base.OnInit(e);
        }



        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                foreach (ListItem lItem in this._items)
                {
                    if (lItem.Value == this._selectedValue.Value)
                    {
                        _selectedItem.Controls.Clear();
                        _selectedItem.Controls.Add(new Literal { Text = lItem.Text });
                    }
                }
            }

            base.OnLoad(e);
        }

 

        #region IPostBackEventHandler Membres

        public void RaisePostBackEvent(string eventArgument)
        {
            if (this.Click != null)
            {
                Click(this, new EventArgs());
            }
        }

        #endregion

    }
}
